using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime.Tree;
using Choop.Compiler.ChoopModel;
using Choop.Compiler.TranslationUtils;
using EventHandler = Choop.Compiler.ChoopModel.EventHandler;

namespace Choop.Compiler
{
    /// <summary>
    /// Used for converting a raw Choop AST into a structured memory form.
    /// </summary>
    internal class ChoopBuilder : ChoopBaseListener
    {
        #region Fields

        /// <summary>
        /// The current active sprite declaration.
        /// </summary>
        private ISpriteDeclaration _currentSprite;

        /// <summary>
        /// The stack of current code blocks.
        /// </summary>
        private readonly Stack<IHasBody> _currentBlocks;

        /// <summary>
        /// The current active expression.
        /// </summary>
        private readonly Stack<IExpression> _currentExpressions;

        /// <summary>
        /// The collection of compiler errors.
        /// </summary>
        private readonly Collection<CompilerError> _compilerErrors;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the internal representation of the Choop project.
        /// </summary>
        public Project Project { get; }

        /// <summary>
        /// Gets or sets the current file name.
        /// </summary>
        public string FileName { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ChoopBuilder"/> class.
        /// </summary>
        /// <param name="projectName">The name of the Choop project to create.</param>
        /// <param name="compilerErrors">The collection of compiler errors to add to.</param>
        public ChoopBuilder(string projectName, Collection<CompilerError> compilerErrors)
        {
            _compilerErrors = compilerErrors;
            Project = new Project(projectName);

            _currentSprite = Project;
            _currentExpressions = new Stack<IExpression>();
            _currentBlocks = new Stack<IHasBody>();
        }

        #endregion

        #region Listener

        #region Sprites

        public override void EnterSprite(ChoopParser.SpriteContext context)
        {
            base.EnterSprite(context);

            // Get basic info
            string name = context.Name.Text;
            string metaFile = name + ".sm";

            if (context.MetaAttr != null)
                metaFile = context.MetaAttr.FileName.Text;

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) != null)
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
                return;
            }

            // Create declaration
            SpriteDeclaration sprite = new SpriteDeclaration(name, metaFile, FileName, context.Name);

            Project.Sprites.Add(sprite);

            // Get imported modules
            foreach (ChoopParser.UsingStmtContext usingStmtAst in context.usingStmt())
            {
                string moduleName = usingStmtAst.Module.Text;

                if (!sprite.ImportedModules.Contains(moduleName))
                    sprite.ImportedModules.Add(moduleName);
                else
                    _compilerErrors.Add(new CompilerError($"Module '{moduleName}' already imported",
                        ErrorType.ModuleAlreadyImported, usingStmtAst.Module, FileName));
            }

            // Set sprite as current
            _currentSprite = sprite;
        }

        public override void EnterModule(ChoopParser.ModuleContext context)
        {
            base.EnterModule(context);

            // Get basic info
            string name = context.Name.Text;

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ModuleDeclaration module = new ModuleDeclaration(name, FileName, context.Name);

                Project.Modules.Add(module);

                // Set module as current
                _currentSprite = module;
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        #endregion

        #region Global Statements

        public override void EnterGlobalStmt(ChoopParser.GlobalStmtContext context)
        {
            base.EnterGlobalStmt(context);

            // Expressions list should be empty
            if (_currentExpressions.Count > 0) throw new InvalidOperationException();
        }

        public override void ExitConstDeclaration(ChoopParser.ConstDeclarationContext context)
        {
            base.ExitConstDeclaration(context);

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // Get terminal expression
            TerminalExpression expression = _currentExpressions.Pop() as TerminalExpression;
            if (expression == null) throw new InvalidOperationException();

            // TODO: move to after module imports
            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ConstDeclaration constDeclaration =
                    new ConstDeclaration(name, type, expression, FileName, context.Name);

                _currentSprite.Constants.Add(constDeclaration);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        public override void ExitVarGlobalDeclaration(ChoopParser.VarGlobalDeclarationContext context)
        {
            base.ExitVarGlobalDeclaration(context);

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // Check if default value specified
            TerminalExpression expression = null;
            if (_currentExpressions.Count > 0)
            {
                // Get terminal expression
                expression = _currentExpressions.Pop() as TerminalExpression;
                if (expression == null) throw new InvalidOperationException();
            }

            // TODO: move to after module imports
            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                GlobalVarDeclaration varDeclaration =
                    new GlobalVarDeclaration(name, type, expression, FileName, context.Name);

                _currentSprite.Variables.Add(varDeclaration);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        public override void ExitArrayGlobalDeclaration(ChoopParser.ArrayGlobalDeclarationContext context)
        {
            base.ExitArrayGlobalDeclaration(context);

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // TODO: move to after module imports
            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                GlobalListDeclaration arrayDeclaration =
                    new GlobalListDeclaration(name, type, true, FileName, context.Name);

                _currentSprite.Lists.Add(arrayDeclaration);

                // Get bounds
                int bounds = int.Parse(context.Bounds.Text);

                if (bounds == 0)
                {
                    // Syntax error - bound should be greater than 0
                    _compilerErrors.Add(new CompilerError("Array length must be greater than 0",
                        ErrorType.InvalidArgument, context.Bounds, FileName));
                    return;
                }

                if (_currentExpressions.Count > 0)
                {
                    // Check bounds match supplied values
                    if (bounds != _currentExpressions.Count)
                    {
                        // Syntax error - bounds should match
                        _compilerErrors.Add(new CompilerError("Array bounds does not match length of supplied values",
                            ErrorType.InvalidArgument, context.Bounds, FileName));
                        return;
                    }

                    // Get the default values
                    while (_currentExpressions.Count > 0)
                        arrayDeclaration.Value.Insert(0, _currentExpressions.Pop() as TerminalExpression);
                }
                else
                {
                    for (int i = 0; i < bounds; i++)
                        arrayDeclaration.Value.Add(
                            new TerminalExpression("\"\"", TerminalType.String));
                }
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        public override void ExitListGlobalDeclaration(ChoopParser.ListGlobalDeclarationContext context)
        {
            base.ExitListGlobalDeclaration(context);

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // TODO: move to after module imports
            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                GlobalListDeclaration listDeclaration =
                    new GlobalListDeclaration(name, type, false, FileName, context.Name);

                _currentSprite.Lists.Add(listDeclaration);

                // Get the default values
                while (_currentExpressions.Count > 0)
                    listDeclaration.Value.Add(_currentExpressions.Pop() as TerminalExpression);

                // Get bounds
                if (context.Bounds == null)
                    return;

                int bounds = int.Parse(context.Bounds.Text);

                if (listDeclaration.Value.Count > 0)
                {
                    // Check bounds match supplied values
                    if (bounds != listDeclaration.Value.Count)
                        _compilerErrors.Add(new CompilerError("List bounds does not match length of supplied values",
                            ErrorType.InvalidArgument, context.Bounds, FileName));
                }
                else
                {
                    for (int i = 0; i < bounds; i++)
                        listDeclaration.Value.Add(
                            new TerminalExpression("\"\"", TerminalType.String));
                }
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.InvalidArgument, context.Name, FileName));
            }
        }

        #endregion

        #region Methods

        public override void EnterMethodDeclaration(ChoopParser.MethodDeclarationContext context)
        {
            base.EnterMethodDeclaration(context);

            // Expressions list should be empty
            if (_currentExpressions.Count > 0) throw new InvalidOperationException();

            // Get basic info
            string name = context.Name.Text;
            ITerminalNode[] inlineTags = context.InlineTag();
            ITerminalNode[] atomicTags = context.AtomicTag();
            ITerminalNode[] unsafeTags = context.UnsafeTag();
            bool hasReturn = context.Void == null;
            DataType type = context.Type.ToDataType();

            // Validate modifiers
            ValidateModifier(inlineTags, "inline");
            ValidateModifier(atomicTags, "atomic");
            ValidateModifier(unsafeTags, "unsafe");

            // Create declaration
            MethodDeclaration method = new MethodDeclaration(
                name,
                type,
                hasReturn,
                unsafeTags.Length > 0,
                inlineTags.Length > 0,
                atomicTags.Length > 0,
                FileName,
                context.Name
            );

            _currentSprite.Methods.Add(method);
            _currentBlocks.Push(method);
        }

        public override void ExitParameter(ChoopParser.ParameterContext context)
        {
            base.ExitParameter(context);

            // As we are defining parameters, we can assume the current block is
            // the method declaration
            MethodDeclaration method = _currentBlocks.Peek() as MethodDeclaration;
            if (method == null) throw new InvalidOperationException();

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // TODO: move to after module imports
            // Check not already defined
            if (Project.GetDeclaration(name) == null)
            {
                // Create parameter declaration
                ParamDeclaration param = new ParamDeclaration(name, type, FileName, context.Name);

                method.Params.Add(param);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        public override void ExitOptionalParameter(ChoopParser.OptionalParameterContext context)
        {
            base.ExitOptionalParameter(context);

            // As we are defining parameters, we can assume the current block is
            // the method declaration
            MethodDeclaration method = _currentBlocks.Peek() as MethodDeclaration;
            if (method == null) throw new InvalidOperationException();

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();
            TerminalExpression expression = _currentExpressions.Pop() as TerminalExpression;
            if (expression == null) throw new InvalidOperationException();

            // TODO: move to after module imports
            // Check not already defined
            if (Project.GetDeclaration(name) == null)
            {
                // Create parameter declaration
                ParamDeclaration param = new ParamDeclaration(name, type, FileName, context.Name, expression.Literal);

                method.Params.Add(param);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.ExtraneousToken, context.Name, FileName));
            }
        }

        public override void EnterEventHead(ChoopParser.EventHeadContext context)
        {
            base.EnterEventHead(context);

            // Expressions list should be empty
            if (_currentExpressions.Count > 0) throw new InvalidOperationException();
        }

        public override void ExitEventHead(ChoopParser.EventHeadContext context)
        {
            base.ExitEventHead(context);

            // Get basic info
            string eventName = context.Event.Text;
            ITerminalNode[] atomicTags = context.AtomicTag();
            ITerminalNode[] unsafeTags = context.UnsafeTag();

            // Validate modifiers
            ValidateModifier(atomicTags, "atomic");
            ValidateModifier(unsafeTags, "unsafe");

            // Check if event has a parameter
            TerminalExpression expression = null;
            if (context.constant() != null)
            {
                // Get parameter
                expression = _currentExpressions.Pop() as TerminalExpression;
                if (expression == null) throw new InvalidOperationException();
            }

            // Create declaration
            EventHandler eventHandler =
                new EventHandler(
                    eventName,
                    expression,
                    unsafeTags.Length > 0,
                    atomicTags.Length > 0,
                    FileName,
                    context.Event
                );

            _currentSprite.EventHandlers.Add(eventHandler);
            _currentBlocks.Push(eventHandler);
        }

        /// <summary>
        /// Validates that a modifier was not specified multiple times.
        /// </summary>
        /// <param name="modifier">The modifier to validate.</param>
        /// <param name="name">The display name of the modifier.</param>
        private void ValidateModifier(ITerminalNode[] modifier, string name)
        {
            // Only allow max 1
            if (modifier.Length <= 1) return;

            for (int i = 1; i < modifier.Length; i++)
                _compilerErrors.Add(new CompilerError($"Duplicate '{name}' modifier",
                    ErrorType.ExtraneousToken, modifier[i].Symbol, FileName));
        }

        #endregion

        #region Statements

        #region Declarations

        public override void ExitVarDeclaration(ChoopParser.VarDeclarationContext context)
        {
            base.ExitVarDeclaration(context);

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // Check if initial value specified
            IExpression expression = null;
            if (_currentExpressions.Count > 0)
                expression = _currentExpressions.Pop();

            // TODO: move to after module imports
            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ScopedVarDeclaration varDeclaration =
                    new ScopedVarDeclaration(name, type, expression, FileName, context.Name);

                _currentBlocks.Peek().Statements.Add(varDeclaration);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        public override void ExitArrayDeclaration(ChoopParser.ArrayDeclarationContext context)
        {
            base.ExitArrayDeclaration(context);

            // Get basic info
            string name = context.Name.Text;
            DataType type = context.Type.ToDataType();

            // TODO: move to after module imports
            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ScopedArrayDeclaration arrayDeclaration =
                    new ScopedArrayDeclaration(name, type, FileName, context.Name);
                _currentBlocks.Peek().Statements.Add(arrayDeclaration);

                // Get bounds

                int bounds = int.Parse(context.Bounds.Text);

                if (bounds == 0)
                {
                    // Syntax error - bound should be greater than 0
                    _compilerErrors.Add(new CompilerError("Array length must be greater than 0",
                        ErrorType.InvalidArgument, context.Bounds, FileName));
                    return;
                }

                if (_currentExpressions.Count > 0)
                {
                    // Check bounds match supplied values
                    if (bounds != _currentExpressions.Count)
                    {
                        // Syntax error - bounds should match
                        _compilerErrors.Add(new CompilerError("Array bounds does not match length of supplied values",
                            ErrorType.InvalidArgument, context.Bounds, FileName));
                        return;
                    }

                    // Get the default values
                    while (_currentExpressions.Count > 0)
                        arrayDeclaration.Value.Insert(0, _currentExpressions.Pop());
                }
                else
                {
                    for (int i = 0; i < bounds; i++)
                        arrayDeclaration.Value.Add(
                            new TerminalExpression("\"\"", TerminalType.String));
                }
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError($"Project already contains a definition for '{name}'",
                    ErrorType.DuplicateDeclaration, context.Name, FileName));
            }
        }

        #endregion

        #region Assignments

        public override void ExitAssignVar(ChoopParser.AssignVarContext context)
        {
            base.ExitAssignVar(context);

            ITerminalNode identifier = context.Identifier();
            string variable = identifier.GetText();
            AssignOperator op = context.assignOp().ToAssignOperator();
            IExpression expression = _currentExpressions.Pop();

            VarAssignStmt stmt = new VarAssignStmt(variable, op, FileName, identifier.Symbol, expression);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignVarInc(ChoopParser.AssignVarIncContext context)
        {
            base.EnterAssignVarInc(context);

            ITerminalNode identifier = context.Identifier();
            string variable = identifier.GetText();

            VarAssignStmt stmt = new VarAssignStmt(variable, AssignOperator.PlusPlus, FileName, identifier.Symbol);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignVarDec(ChoopParser.AssignVarDecContext context)
        {
            base.EnterAssignVarDec(context);

            ITerminalNode identifier = context.Identifier();
            string variable = identifier.GetText();

            VarAssignStmt stmt = new VarAssignStmt(variable, AssignOperator.MinusMinus, FileName, identifier.Symbol);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void ExitAssignArray(ChoopParser.AssignArrayContext context)
        {
            base.ExitAssignArray(context);

            ITerminalNode identifier = context.Identifier();
            string array = identifier.GetText();
            AssignOperator op = context.assignOp().ToAssignOperator();
            IExpression expression = _currentExpressions.Pop();
            IExpression index = _currentExpressions.Pop();

            ArrayAssignStmt stmt = new ArrayAssignStmt(array, index, op, expression, FileName, identifier.Symbol);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignArrayInc(ChoopParser.AssignArrayIncContext context)
        {
            base.EnterAssignArrayInc(context);

            ITerminalNode identifier = context.Identifier();
            string array = identifier.GetText();
            IExpression index = _currentExpressions.Pop();

            ArrayAssignStmt stmt = new ArrayAssignStmt(array, index, AssignOperator.PlusPlus, null, FileName,
                identifier.Symbol);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignArrayDec(ChoopParser.AssignArrayDecContext context)
        {
            base.EnterAssignArrayDec(context);

            ITerminalNode identifier = context.Identifier();
            string array = identifier.GetText();
            IExpression index = _currentExpressions.Pop();

            ArrayAssignStmt stmt = new ArrayAssignStmt(array, index, AssignOperator.MinusMinus, null, FileName,
                identifier.Symbol);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void ExitArrayFullAssignment(ChoopParser.ArrayFullAssignmentContext context)
        {
            base.ExitArrayFullAssignment(context);

            string array = context.Name.Text;

            ArrayReAssignStmt stmt = new ArrayReAssignStmt(array, FileName, context.Name);

            // Get the new values
            while (_currentExpressions.Count > 0)
                stmt.Items.Insert(0, _currentExpressions.Pop() as TerminalExpression);

            _currentBlocks.Peek().Statements.Add(stmt);
        }

        #endregion

        #region Selection

        #region Switch

        public override void ExitSwitchHead(ChoopParser.SwitchHeadContext context)
        {
            base.ExitSwitchHead(context);

            IExpression variable = _currentExpressions.Pop();

            SwitchStmt stmt = new SwitchStmt(variable, FileName, variable.ErrorToken);

            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterCaseHead(ChoopParser.CaseHeadContext context)
        {
            base.EnterCaseHead(context);

            // Current expressions should be empty
            if (_currentExpressions.Count > 0) throw new InvalidOperationException();
        }

        public override void ExitCaseHead(ChoopParser.CaseHeadContext context)
        {
            base.ExitCaseHead(context);

            SwitchStmt stmt = _currentBlocks.Peek().Statements.Last() as SwitchStmt;
            if (stmt == null) throw new InvalidOperationException();

            // Create case block
            ConditionalBlock caseBlock = new ConditionalBlock(FileName, context.Start);

            // Add all conditions
            while (_currentExpressions.Count > 0)
                caseBlock.Conditions.Add(_currentExpressions.Pop());

            stmt.Blocks.Add(caseBlock);
            _currentBlocks.Push(caseBlock);
        }

        public override void ExitDefaultCaseHead(ChoopParser.DefaultCaseHeadContext context)
        {
            base.ExitDefaultCaseHead(context);

            SwitchStmt stmt = _currentBlocks.Peek().Statements.Last() as SwitchStmt;
            if (stmt == null) throw new InvalidOperationException();

            // Create case block
            ConditionalBlock caseBlock = new ConditionalBlock(FileName, context.Start);

            // No conditions (default)

            stmt.Blocks.Add(caseBlock);
            _currentBlocks.Push(caseBlock);
        }

        public override void ExitCaseBody(ChoopParser.CaseBodyContext context)
        {
            base.ExitCaseBody(context);

            _currentBlocks.Pop();
        }

        #endregion

        #region If

        public override void ExitIfHead(ChoopParser.IfHeadContext context)
        {
            base.ExitIfHead(context);

            IfStmt stmt = new IfStmt(FileName, context.Start);

            ConditionalBlock mainBlock = new ConditionalBlock(FileName, context.Start);
            mainBlock.Conditions.Add(_currentExpressions.Pop());

            stmt.Blocks.Add(mainBlock);

            _currentBlocks.Peek().Statements.Add(stmt);
            _currentBlocks.Push(mainBlock);
        }

        public override void ExitElseIfHead(ChoopParser.ElseIfHeadContext context)
        {
            base.ExitElseIfHead(context);

            IfStmt ifStmt = _currentBlocks.Peek().Statements.Last() as IfStmt;
            if (ifStmt == null) throw new InvalidOperationException();

            ConditionalBlock elseBlock = new ConditionalBlock(FileName, context.Start);
            elseBlock.Conditions.Add(_currentExpressions.Pop());

            ifStmt.Blocks.Add(elseBlock);
            _currentBlocks.Push(elseBlock);
        }

        public override void EnterElseBlock(ChoopParser.ElseBlockContext context)
        {
            base.EnterElseBlock(context);

            IfStmt ifStmt = _currentBlocks.Peek().Statements.Last() as IfStmt;
            if (ifStmt == null) throw new InvalidOperationException();

            ConditionalBlock elseBlock = new ConditionalBlock(FileName, context.Start);

            // No conditions - default

            ifStmt.Blocks.Add(elseBlock);
            _currentBlocks.Push(elseBlock);
        }

        #endregion

        #endregion

        #region Iteration

        public override void ExitRepeatHead(ChoopParser.RepeatHeadContext context)
        {
            base.ExitRepeatHead(context);

            IExpression expression = _currentExpressions.Pop();

            RepeatLoop loop = new RepeatLoop(context.Inline != null, expression, FileName, context.Start);
            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void ExitForHead(ChoopParser.ForHeadContext context)
        {
            base.ExitForHead(context);

            // Get counter variable details
            string varName = context.Variable.Text;
            ChoopParser.TypeSpecifierContext typeSpecifier = context.typeSpecifier();
            DataType varType = typeSpecifier.ToDataType();
            if (!varType.IsCompatible(DataType.Number))
                _compilerErrors.Add(new CompilerError("Variable type must be object or number",
                    ErrorType.TypeMismatch, typeSpecifier.Start, FileName));

            // Get expressions
            TerminalExpression step = null;
            if (context.Step != null)
            {
                // Step value was specified
                step = _currentExpressions.Pop() as TerminalExpression;
                if (step == null) throw new InvalidOperationException();
                if (!(step.LiteralType == TerminalType.Int || step.LiteralType == TerminalType.Decimal ||
                      step.LiteralType == TerminalType.Hex || step.LiteralType == TerminalType.Scientific))
                    _compilerErrors.Add(new CompilerError("Step value must be a number", ErrorType.InvalidArgument,
                        context.Step.start, FileName));
            }

            IExpression end = _currentExpressions.Pop();
            IExpression start = _currentExpressions.Pop();

            // Create for loop
            ForLoop loop = new ForLoop(varName, varType, start, end, step, FileName, context.Variable);
            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void EnterForeachLoop(ChoopParser.ForeachLoopContext context)
        {
            base.EnterForeachLoop(context);

            ForeachLoop loop = new ForeachLoop(
                context.Variable.Text,
                context.Type.ToDataType(),
                context.List.Text,
                FileName,
                context.Variable
            );

            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void EnterForeverLoop(ChoopParser.ForeverLoopContext context)
        {
            base.EnterForeverLoop(context);

            ForeverLoop loop = new ForeverLoop(FileName, context.Start);

            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void ExitWhileHead(ChoopParser.WhileHeadContext context)
        {
            base.ExitWhileHead(context);

            WhileLoop loop = new WhileLoop(_currentExpressions.Pop(), FileName, context.Start);

            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        #endregion

        #region Other

        public override void EnterStatement(ChoopParser.StatementContext context)
        {
            base.EnterStatement(context);

            // Expressions list should be empty
            if (_currentExpressions.Count > 0) throw new InvalidOperationException();
        }

        public override void ExitStmtMethodCall(ChoopParser.StmtMethodCallContext context)
        {
            base.ExitStmtMethodCall(context);

            MethodCall stmt = _currentExpressions.Pop() as MethodCall;

            if (stmt == null) throw new InvalidOperationException();

            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void ExitReturnStmt(ChoopParser.ReturnStmtContext context)
        {
            base.ExitReturnStmt(context);

            ReturnStmt stmt = context.Expression != null
                ? new ReturnStmt(_currentExpressions.Pop(), FileName, context.Start)
                : new ReturnStmt(null, FileName, context.Start);

            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterStmtScope(ChoopParser.StmtScopeContext context)
        {
            base.EnterStmtScope(context);

            ScopeDeclaration scope = new ScopeDeclaration(context.Unsafe != null, FileName, context.Start);
            _currentBlocks.Peek().Statements.Add(scope);
            _currentBlocks.Push(scope);
        }

        public override void ExitScopeBody(ChoopParser.ScopeBodyContext context)
        {
            base.ExitScopeBody(context);

            _currentBlocks.Pop();
        }

        #endregion

        #endregion

        #region Expressions

        #region Terminals

        #region UConstants

        public override void EnterUConstantTrue(ChoopParser.UConstantTrueContext context)
        {
            base.EnterUConstantTrue(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), TerminalType.Bool, FileName,
                context.Start));
        }

        public override void EnterUConstantFalse(ChoopParser.UConstantFalseContext context)
        {
            base.EnterUConstantFalse(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), TerminalType.Bool, FileName,
                context.Start));
        }

        public override void EnterUConstantString(ChoopParser.UConstantStringContext context)
        {
            base.EnterUConstantString(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.String, FileName, context.Start));
        }

        public override void EnterUConstantInt(ChoopParser.UConstantIntContext context)
        {
            base.EnterUConstantInt(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Int, FileName, context.Start));
        }

        public override void EnterUConstantDec(ChoopParser.UConstantDecContext context)
        {
            base.EnterUConstantDec(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Decimal, FileName, context.Start));
        }

        public override void EnterUConstantHex(ChoopParser.UConstantHexContext context)
        {
            base.EnterUConstantHex(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Hex, FileName, context.Start));
        }

        public override void EnterUConstantSci(ChoopParser.UConstantSciContext context)
        {
            base.EnterUConstantSci(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Scientific, FileName, context.Start));
        }

        #endregion

        #region Constants

        public override void EnterConstantTrue(ChoopParser.ConstantTrueContext context)
        {
            base.EnterConstantTrue(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), TerminalType.Bool, FileName,
                context.Start));
        }

        public override void EnterConstantFalse(ChoopParser.ConstantFalseContext context)
        {
            base.EnterConstantFalse(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), TerminalType.Bool, FileName,
                context.Start));
        }

        public override void EnterConstantString(ChoopParser.ConstantStringContext context)
        {
            base.EnterConstantString(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), TerminalType.String, FileName,
                context.Start));
        }

        public override void EnterConstantInt(ChoopParser.ConstantIntContext context)
        {
            base.EnterConstantInt(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Int, FileName, context.Start));
        }

        public override void EnterConstantDec(ChoopParser.ConstantDecContext context)
        {
            base.EnterConstantDec(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Decimal, FileName, context.Start));
        }

        public override void EnterConstantHex(ChoopParser.ConstantHexContext context)
        {
            base.EnterConstantHex(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Hex, FileName, context.Start));
        }

        public override void EnterConstantSci(ChoopParser.ConstantSciContext context)
        {
            base.EnterConstantSci(context);

            _currentExpressions.Push(
                new TerminalExpression(context.GetText(), TerminalType.Scientific, FileName, context.Start));
        }

        #endregion

        #endregion

        #region Primary Expressions

        public override void ExitMethodCall(ChoopParser.MethodCallContext context)
        {
            base.ExitMethodCall(context);

            MethodCall methodCall = new MethodCall(context.Method.Text, FileName, context.Method);

            int paramCount = context.expression().Length;

            for (int i = 0; i < paramCount; i++)
                methodCall.Parameters.Insert(0, _currentExpressions.Pop());

            _currentExpressions.Push(methodCall);
        }

        public override void ExitPrimaryVarLookup(ChoopParser.PrimaryVarLookupContext context)
        {
            base.ExitPrimaryVarLookup(context);

            ITerminalNode identifier = context.Identifier();

            _currentExpressions.Push(new LookupExpression(identifier.GetText(), FileName, identifier.Symbol));
        }

        public override void ExitPrimaryArrayLookup(ChoopParser.PrimaryArrayLookupContext context)
        {
            base.ExitPrimaryArrayLookup(context);

            ITerminalNode identifier = context.Identifier();
            IExpression index = _currentExpressions.Pop();
            ArrayLookupExpression lookup =
                new ArrayLookupExpression(identifier.GetText(), index, FileName, identifier.Symbol);

            _currentExpressions.Push(lookup);
        }

        #endregion

        #region Unary Expressions

        public override void ExitUnaryMinus(ChoopParser.UnaryMinusContext context)
        {
            base.ExitUnaryMinus(context);

            IExpression expression = _currentExpressions.Pop();
            _currentExpressions.Push(new UnaryExpression(expression, UnaryOperator.Minus, FileName, context.Start));
        }

        public override void ExitUnaryNot(ChoopParser.UnaryNotContext context)
        {
            base.ExitUnaryNot(context);

            IExpression expression = _currentExpressions.Pop();
            _currentExpressions.Push(new UnaryExpression(expression, UnaryOperator.Not, FileName, context.Start));
        }

        #endregion

        #region Binary Expressions

        public override void ExitExpressionPow(ChoopParser.ExpressionPowContext context)
        {
            base.ExitExpressionPow(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Pow, first, second, FileName,
                context.OpPow().Symbol));
        }

        public override void ExitExpressionMult(ChoopParser.ExpressionMultContext context)
        {
            base.ExitExpressionMult(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Multiply, first, second, FileName,
                context.OpMult().Symbol));
        }

        public override void ExitExpressionDivide(ChoopParser.ExpressionDivideContext context)
        {
            base.ExitExpressionDivide(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Divide, first, second, FileName,
                context.OpDivide().Symbol));
        }

        public override void ExitExpressionMod(ChoopParser.ExpressionModContext context)
        {
            base.ExitExpressionMod(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Mod, first, second, FileName,
                context.OpMod().Symbol));
        }

        public override void ExitExpressionConcat(ChoopParser.ExpressionConcatContext context)
        {
            base.ExitExpressionConcat(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Concat, first, second, FileName,
                context.OpConcat().Symbol));
        }

        public override void ExitExpressionPlus(ChoopParser.ExpressionPlusContext context)
        {
            base.ExitExpressionPlus(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Plus, first, second, FileName,
                context.OpPlus().Symbol));
        }

        public override void ExitExpressionMinus(ChoopParser.ExpressionMinusContext context)
        {
            base.ExitExpressionMinus(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Minus, first, second, FileName,
                context.OpMinus().Symbol));
        }

        public override void ExitExpressionLShift(ChoopParser.ExpressionLShiftContext context)
        {
            base.ExitExpressionLShift(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.LShift, first, second, FileName,
                context.OpLShift().Symbol));
        }

        public override void ExitExpressionRShift(ChoopParser.ExpressionRShiftContext context)
        {
            base.ExitExpressionRShift(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.RShift, first, second, FileName,
                context.OpRShift().Symbol));
        }

        public override void ExitExpressionLT(ChoopParser.ExpressionLTContext context)
        {
            base.ExitExpressionLT(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.LessThan, first, second, FileName,
                context.OpLT().Symbol));
        }

        public override void ExitExpressionGT(ChoopParser.ExpressionGTContext context)
        {
            base.ExitExpressionGT(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.GreaterThan, first, second, FileName,
                context.OpGT().Symbol));
        }

        public override void ExitExpressionLTE(ChoopParser.ExpressionLTEContext context)
        {
            base.ExitExpressionLTE(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.LessThanEq, first, second, FileName,
                context.OpLTE().Symbol));
        }

        public override void ExitExpressionGTE(ChoopParser.ExpressionGTEContext context)
        {
            base.ExitExpressionGTE(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.GreaterThanEq, first, second, FileName,
                context.OpGTE().Symbol));
        }

        public override void ExitExpressionEquals(ChoopParser.ExpressionEqualsContext context)
        {
            base.ExitExpressionEquals(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Equal, first, second, FileName,
                context.OpEquals().Symbol));
        }

        public override void ExitExpressionNEquals(ChoopParser.ExpressionNEqualsContext context)
        {
            base.ExitExpressionNEquals(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.NotEqual, first, second, FileName,
                context.OpNEquals().Symbol));
        }

        public override void ExitExpressionAnd(ChoopParser.ExpressionAndContext context)
        {
            base.ExitExpressionAnd(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.And, first, second, FileName,
                context.OpAnd().Symbol));
        }

        public override void ExitExpressionOr(ChoopParser.ExpressionOrContext context)
        {
            base.ExitExpressionOr(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompoundOperator.Or, first, second, FileName,
                context.OpOr().Symbol));
        }

        #endregion

        #endregion

        #endregion
    }
}