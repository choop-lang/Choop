using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime.Tree;
using Choop.Compiler.ChoopModel;

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
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            string metaFile = name + ".sm";

            ChoopParser.MetaAttributeContext metaAttribute = context.metaAttribute();
            if (metaAttribute != null)
            {
                // Meta attribute used to specify non-standard meta file path
                metaFile = metaAttribute.StringLiteral().GetText();
            }

            // Create declaration object
            SpriteBaseDeclaration sprite;

            if (name.Equals("stage", StringComparison.CurrentCultureIgnoreCase))
            {
                // Stage

                // Check stage hasn't been declared before
                if (Project.Stage == null)
                {
                    sprite = new StageDeclaration(metaFile);

                    Project.Stage = (StageDeclaration) sprite;
                }
                else
                {
                    // Syntax error - stage declared twice
                    _compilerErrors.Add(new CompilerError(identifier.Symbol, 
                        $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
                    return;
                }
            }
            else
            {
                // Sprite

                // Check anything with same name hasn't already been declared
                if (Project.GetDeclaration(name) == null)
                {
                    sprite = new SpriteDeclaration(name, metaFile);

                    Project.Sprites.Add((SpriteDeclaration) sprite);
                }
                else
                {
                    // Syntax error - definition already exists
                    _compilerErrors.Add(new CompilerError(identifier.Symbol,
                        $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
                    return;
                }
            }

            // Get imported modules
            foreach (ChoopParser.UsingStmtContext usingStmtAst in context.usingStmt())
            {
                ITerminalNode module = usingStmtAst.Identifier();
                string moduleName = module.GetText();

                if (!sprite.ImportedModules.Contains(moduleName))
                {
                    sprite.ImportedModules.Add(moduleName);
                }
                else
                {
                    // Module already included, raise error
                    _compilerErrors.Add(new CompilerError(module.Symbol,
                        $"Module '{moduleName}' already imported", ErrorType.ModuleAlreadyImported));
                }
            }

            // Set sprite as current
            _currentSprite = sprite;
        }

        public override void EnterModule(ChoopParser.ModuleContext context)
        {
            base.EnterModule(context);

            // Get basic info
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ModuleDeclaration module = new ModuleDeclaration(name);

                Project.Modules.Add(module);

                // Set module as current
                _currentSprite = module;
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol,
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
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
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();

            // Get terminal expression
            TerminalExpression expression = _currentExpressions.Pop() as TerminalExpression;
            if (expression == null) throw new InvalidOperationException();

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ConstDeclaration constDeclaration = new ConstDeclaration(name, type, expression);

                _currentSprite.Constants.Add(constDeclaration);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol, 
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
            }
        }

        public override void ExitVarGlobalDeclaration(ChoopParser.VarGlobalDeclarationContext context)
        {
            base.ExitVarGlobalDeclaration(context);

            // Get basic info
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();

            // Check if default value specified
            TerminalExpression expression = null;
            if (_currentExpressions.Count > 0)
            {
                // Get terminal expression
                expression = _currentExpressions.Pop() as TerminalExpression;
                if (expression == null) throw new InvalidOperationException();
            }

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                GlobalVarDeclaration varDeclaration = new GlobalVarDeclaration(name, type, expression);

                _currentSprite.Variables.Add(varDeclaration);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol,
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
            }
        }

        public override void ExitArrayGlobalDeclaration(ChoopParser.ArrayGlobalDeclarationContext context)
        {
            base.ExitArrayGlobalDeclaration(context);

            // Get basic info
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                GlobalListDeclaration arrayDeclaration = new GlobalListDeclaration(name, type, true);

                _currentSprite.Lists.Add(arrayDeclaration);

                // Get bounds
                ITerminalNode boundSpecifier = context.UInteger();
                if (boundSpecifier == null)
                    return;

                int bounds = int.Parse(boundSpecifier.GetText());

                if (bounds == 0)
                {
                    // Syntax error - bound should be greater than 0
                    _compilerErrors.Add(new CompilerError(identifier.Symbol,
                        "Array length must be greater than 0", ErrorType.InvalidArgument));
                    return;
                }

                if (_currentExpressions.Count > 0)
                {
                    // Check bounds match supplied values
                    if (bounds != _currentExpressions.Count)
                    {
                        // Syntax error - bounds should match
                        _compilerErrors.Add(new CompilerError(identifier.Symbol,
                            "Array bounds does not match length of supplied values", ErrorType.InvalidArgument));
                        return;
                    }

                    // Get the default values
                    while (_currentExpressions.Count > 0)
                        arrayDeclaration.Value.Insert(0, _currentExpressions.Pop() as TerminalExpression);
                }
                else
                {
                    for (int i = 0; i < bounds; i++)
                    {
                        arrayDeclaration.Value.Add(new TerminalExpression("", DataType.String));
                    }
                }
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol,
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
            }
        }

        public override void EnterListGlobalDeclaration(ChoopParser.ListGlobalDeclarationContext context)
        {
            base.EnterListGlobalDeclaration(context);

            // Ensure current expressions is empty
            // (Should be anyway)
            if (_currentExpressions.Count > 0) throw new InvalidOperationException();
        }

        public override void ExitListGlobalDeclaration(ChoopParser.ListGlobalDeclarationContext context)
        {
            base.ExitListGlobalDeclaration(context);

            // Get basic info
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                GlobalListDeclaration listDeclaration = new GlobalListDeclaration(name, type, false);

                _currentSprite.Lists.Add(listDeclaration);
                
                // Get the default values
                while (_currentExpressions.Count > 0)
                    listDeclaration.Value.Add(_currentExpressions.Pop() as TerminalExpression);

                // Get bounds
                ITerminalNode boundSpecifier = context.UInteger();
                if (boundSpecifier == null)
                    return;

                int bounds = int.Parse(boundSpecifier.GetText());
                
                if (listDeclaration.Value.Count > 0)
                {
                    // Check bounds match supplied values
                    if (bounds != listDeclaration.Value.Count)
                    {
                        // Syntax error - bounds should match
                        _compilerErrors.Add(new CompilerError(boundSpecifier.Symbol,
                            "List bounds does not match length of supplied values", ErrorType.InvalidArgument));
                    }

                }
                else
                {
                    for (int i = 0; i < bounds; i++)
                    {
                        listDeclaration.Value.Add(new TerminalExpression("", DataType.String));
                    }
                }
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol,
                    $"Project already contains a definition for '{name}'", ErrorType.InvalidArgument));
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
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            ITerminalNode[] inlineTags = context.InlineTag();
            ITerminalNode[] atomicTags = context.AtomicTag();
            ITerminalNode[] unsafeTags = context.UnsafeTag();
            bool hasReturn = context.VoidTag() != null;
            DataType type = context.typeSpecifier().ToDataType();

            // Validate modifiers
            ValidateModifier(inlineTags, "inline", _compilerErrors);
            ValidateModifier(atomicTags, "atomic", _compilerErrors);
            ValidateModifier(unsafeTags, "unsafe", _compilerErrors);

            // Check method signature doesn't already exist
            // Todo

            // Create declaration
            MethodDeclaration method = new MethodDeclaration(
                name,
                type,
                hasReturn,
                unsafeTags.Length > 0,
                inlineTags.Length > 0,
                atomicTags.Length > 0
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
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();


            // Check not already defined
            // TODO
            if (Project.GetDeclaration(name) == null)
            {
                // Create parameter declaration
                ParamDeclaration param = new ParamDeclaration(name, type);

                method.Params.Add(param);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol, 
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
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
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();
            TerminalExpression expression = _currentExpressions.Pop() as TerminalExpression;
            if (expression == null) throw new InvalidOperationException();

            // Check not already defined
            // TODO
            if (Project.GetDeclaration(name) == null)
            {
                // Create parameter declaration
                ParamDeclaration param = new ParamDeclaration(name, type, expression);

                method.Params.Add(param);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol,
                    $"Project already contains a definition for '{name}'", ErrorType.ExtraneousToken));
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
            ITerminalNode eventTag = context.Identifier();
            string eventName = eventTag.GetText();
            ITerminalNode[] atomicTags = context.AtomicTag();
            ITerminalNode[] unsafeTags = context.UnsafeTag();

            // Validate modifiers
            ValidateModifier(atomicTags, "atomic", _compilerErrors);
            ValidateModifier(unsafeTags, "unsafe", _compilerErrors);

            // Check if event has a parameter
            TerminalExpression expression = null;
            if (context.constant() != null)
            {
                // Get parameter
                expression = _currentExpressions.Pop() as TerminalExpression;
                if (expression == null) throw new InvalidOperationException();
            }

            // Create declaration
            ChoopModel.EventHandler eventHandler =
                new ChoopModel.EventHandler(
                    eventName,
                    expression,
                    unsafeTags.Length > 0,
                    atomicTags.Length > 0
                );

            _currentSprite.EventHandlers.Add(eventHandler);
            _currentBlocks.Push(eventHandler);
        }

        /// <summary>
        /// Validates that a modifier was not specified multiple times.
        /// </summary>
        /// <param name="modifier">The modifier to validate.</param>
        /// <param name="name">The display name of the modifier.</param>
        /// <param name="parser">The parser to report errors to.</param>
        private static void ValidateModifier(ITerminalNode[] modifier, string name, Collection<CompilerError> compilerErrors)
        {
            // Only allow max 1
            if (modifier.Length <= 1) return;

            for (int i = 1; i < modifier.Length; i++)
                compilerErrors.Add(new CompilerError(modifier[i].Symbol,
                    $"Duplicate '{name}' modifier", ErrorType.ExtraneousToken));
        }

        #endregion

        #region Statements

        #region Declarations

        public override void ExitVarDeclaration(ChoopParser.VarDeclarationContext context)
        {
            base.ExitVarDeclaration(context);

            // Get basic info
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();

            // Check if initial value specified
            IExpression expression = null;
            if (_currentExpressions.Count > 0)
            {
                // Get initial value
                expression = _currentExpressions.Pop();
            }

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ScopedVarDeclaration varDeclaration = new ScopedVarDeclaration(name, type, expression);

                _currentBlocks.Peek().Statements.Add(varDeclaration);
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol, 
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
            }
        }

        public override void ExitArrayDeclaration(ChoopParser.ArrayDeclarationContext context)
        {
            base.ExitArrayDeclaration(context);

            // Get basic info
            ITerminalNode identifier = context.Identifier();
            string name = identifier.GetText();
            DataType type = context.typeSpecifier().ToDataType();

            // Check anything with same name hasn't already been declared
            if (Project.GetDeclaration(name) == null)
            {
                // Create declaration
                ScopedArrayDeclaration arrayDeclaration = new ScopedArrayDeclaration(name, type);
                _currentBlocks.Peek().Statements.Add(arrayDeclaration);

                // Get bounds
                ITerminalNode boundSpecifier = context.UInteger();
                if (boundSpecifier == null)
                    return;

                int bounds = int.Parse(boundSpecifier.GetText());

                if (bounds == 0)
                {
                    // Syntax error - bound should be greater than 0
                    _compilerErrors.Add(new CompilerError(identifier.Symbol,
                        "Array length must be greater than 0", ErrorType.InvalidArgument));
                    return;
                }

                if (_currentExpressions.Count > 0)
                {
                    // Check bounds match supplied values
                    if (bounds != _currentExpressions.Count)
                    {
                        // Syntax error - bounds should match
                        _compilerErrors.Add(new CompilerError(identifier.Symbol,
                            "Array bounds does not match length of supplied values", ErrorType.InvalidArgument));
                        return;
                    }

                    // Get the default values
                    while (_currentExpressions.Count > 0)
                        arrayDeclaration.Value.Insert(0, _currentExpressions.Pop() as TerminalExpression);
                }
                else
                {
                    for (int i = 0; i < bounds; i++)
                    {
                        arrayDeclaration.Value.Add(new TerminalExpression("", DataType.String));
                    }
                }
            }
            else
            {
                // Syntax error - definition already exists
                _compilerErrors.Add(new CompilerError(identifier.Symbol, 
                    $"Project already contains a definition for '{name}'", ErrorType.DuplicateDeclaration));
            }
        }

        #endregion

        #region Assignments

        public override void ExitAssignVar(ChoopParser.AssignVarContext context)
        {
            base.ExitAssignVar(context);

            string variable = context.Identifier().GetText();
            AssignOperator op = context.assignOp().ToAssignOperator();
            IExpression expression = _currentExpressions.Pop();

            VarAssignStmt stmt = new VarAssignStmt(variable, op, expression);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignVarInc(ChoopParser.AssignVarIncContext context)
        {
            base.EnterAssignVarInc(context);

            string variable = context.Identifier().GetText();

            VarAssignStmt stmt = new VarAssignStmt(variable, AssignOperator.PlusPlus);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignVarDec(ChoopParser.AssignVarDecContext context)
        {
            base.EnterAssignVarDec(context);

            string variable = context.Identifier().GetText();

            VarAssignStmt stmt = new VarAssignStmt(variable, AssignOperator.PlusPlus);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void ExitAssignArray(ChoopParser.AssignArrayContext context)
        {
            base.ExitAssignArray(context);

            string array = context.Identifier().GetText();
            AssignOperator op = context.assignOp().ToAssignOperator();
            IExpression expression = _currentExpressions.Pop();
            IExpression index = _currentExpressions.Pop();

            ArrayAssignStmt stmt = new ArrayAssignStmt(array, index, op, expression);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignArrayInc(ChoopParser.AssignArrayIncContext context)
        {
            base.EnterAssignArrayInc(context);

            string array = context.Identifier().GetText();
            IExpression index = _currentExpressions.Pop();

            ArrayAssignStmt stmt = new ArrayAssignStmt(array, index, AssignOperator.PlusPlus);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterAssignArrayDec(ChoopParser.AssignArrayDecContext context)
        {
            base.EnterAssignArrayDec(context);

            string array = context.Identifier().GetText();
            IExpression index = _currentExpressions.Pop();

            ArrayAssignStmt stmt = new ArrayAssignStmt(array, index, AssignOperator.MinusMinus);
            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void ExitArrayFullAssignment(ChoopParser.ArrayFullAssignmentContext context)
        {
            base.ExitArrayFullAssignment(context);

            string array = context.Identifier().GetText();

            ArrayReAssignStmt stmt = new ArrayReAssignStmt(array);

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

            SwitchStmt stmt = new SwitchStmt(variable);

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
            ConditionalBlock caseBlock = new ConditionalBlock();

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
            ConditionalBlock caseBlock = new ConditionalBlock();

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

            IfStmt stmt = new IfStmt();

            ConditionalBlock mainBlock = new ConditionalBlock();
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

            ConditionalBlock elseBlock = new ConditionalBlock();
            elseBlock.Conditions.Add(_currentExpressions.Pop());

            ifStmt.Blocks.Add(elseBlock);
            _currentBlocks.Push(elseBlock);
        }

        public override void EnterElseBlock(ChoopParser.ElseBlockContext context)
        {
            base.EnterElseBlock(context);

            IfStmt ifStmt = _currentBlocks.Peek().Statements.Last() as IfStmt;
            if (ifStmt == null) throw new InvalidOperationException();

            ConditionalBlock elseBlock = new ConditionalBlock();

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

            ITerminalNode inlineTag = context.InlineTag();
            IExpression expression = _currentExpressions.Pop();

            if (inlineTag != null && !(expression is TerminalExpression))
            {
                // Loop cannot be inlined
                _compilerErrors.Add(new CompilerError(inlineTag.Symbol, "Loop cannot be inlined", ErrorType.InvalidArgument));
                return;
            }

            RepeatLoop loop = new RepeatLoop(inlineTag != null, expression);
            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void ExitForHead(ChoopParser.ForHeadContext context)
        {
            base.ExitForHead(context);

            // Get name of counter variable
            ITerminalNode[] identifiers = context.Identifier();
            string counterVar = identifiers[0].GetText();

            if (counterVar != identifiers[1].GetText())
                _compilerErrors.Add(new CompilerError(identifiers[1].Symbol, 
                    "Assignment must be to the counter variable", ErrorType.InvalidArgument));

            // Get assign statement
            VarAssignStmt assignStmt;
            if (context.AssignInc() != null)
                assignStmt = new VarAssignStmt(counterVar, AssignOperator.PlusPlus);
            else if (context.AssignDec() != null)
                assignStmt = new VarAssignStmt(counterVar, AssignOperator.MinusMinus);
            else
                assignStmt = new VarAssignStmt(counterVar, context.assignOp().ToAssignOperator(),
                    _currentExpressions.Pop());

            // Get condition
            IExpression condition = _currentExpressions.Pop();

            // Get declaration
            ChoopParser.TypeSpecifierContext typeSpecifier = context.typeSpecifier();
            DataType type = typeSpecifier.ToDataType();
            if (type == DataType.Boolean || type == DataType.String)
                _compilerErrors.Add(new CompilerError(typeSpecifier.Start, 
                    "Variable type must be object or number", ErrorType.TypeMismatch));

            ScopedVarDeclaration varDeclaration = new ScopedVarDeclaration(counterVar, type, _currentExpressions.Pop());

            // Create for loop
            ForLoop loop = new ForLoop(varDeclaration, condition, assignStmt);
            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void EnterForeachLoop(ChoopParser.ForeachLoopContext context)
        {
            base.EnterForeachLoop(context);

            ITerminalNode[] identifiers = context.Identifier();

            ForeachLoop loop = new ForeachLoop(
                identifiers[0].GetText(),
                context.typeSpecifier().ToDataType(),
                identifiers[1].GetText()
            );

            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void EnterForeverLoop(ChoopParser.ForeverLoopContext context)
        {
            base.EnterForeverLoop(context);

            ForeverLoop loop = new ForeverLoop();

            _currentBlocks.Peek().Statements.Add(loop);
            _currentBlocks.Push(loop);
        }

        public override void ExitWhileHead(ChoopParser.WhileHeadContext context)
        {
            base.ExitWhileHead(context);

            WhileLoop loop = new WhileLoop(_currentExpressions.Pop());

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

            ReturnStmt stmt = context.expression() != null ? new ReturnStmt(_currentExpressions.Pop()) : new ReturnStmt(null);

            _currentBlocks.Peek().Statements.Add(stmt);
        }

        public override void EnterStmtScope(ChoopParser.StmtScopeContext context)
        {
            base.EnterStmtScope(context);

            ScopeDeclaration scope = new ScopeDeclaration();
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

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Boolean));
        }

        public override void EnterUConstantFalse(ChoopParser.UConstantFalseContext context)
        {
            base.EnterUConstantFalse(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Boolean));
        }

        // TODO: Remove string quote marks
        public override void EnterUConstantString(ChoopParser.UConstantStringContext context)
        {
            base.EnterUConstantString(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.String));
        }

        public override void EnterUConstantInt(ChoopParser.UConstantIntContext context)
        {
            base.EnterUConstantInt(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        public override void EnterUConstantDec(ChoopParser.UConstantDecContext context)
        {
            base.EnterUConstantDec(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        public override void EnterUConstantHex(ChoopParser.UConstantHexContext context)
        {
            base.EnterUConstantHex(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        public override void EnterUConstantSci(ChoopParser.UConstantSciContext context)
        {
            base.EnterUConstantSci(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        #endregion

        #region Constants

        public override void EnterConstantTrue(ChoopParser.ConstantTrueContext context)
        {
            base.EnterConstantTrue(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Boolean));
        }

        public override void EnterConstantFalse(ChoopParser.ConstantFalseContext context)
        {
            base.EnterConstantFalse(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Boolean));
        }

        // TODO: Remove string quote marks
        public override void EnterConstantString(ChoopParser.ConstantStringContext context)
        {
            base.EnterConstantString(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.String));
        }

        public override void EnterConstantInt(ChoopParser.ConstantIntContext context)
        {
            base.EnterConstantInt(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        public override void EnterConstantDec(ChoopParser.ConstantDecContext context)
        {
            base.EnterConstantDec(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        public override void EnterConstantHex(ChoopParser.ConstantHexContext context)
        {
            base.EnterConstantHex(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        public override void EnterConstantSci(ChoopParser.ConstantSciContext context)
        {
            base.EnterConstantSci(context);

            _currentExpressions.Push(new TerminalExpression(context.GetText(), DataType.Number));
        }

        #endregion

        #endregion

        #region Primary Expressions

        public override void ExitMethodCall(ChoopParser.MethodCallContext context)
        {
            base.ExitMethodCall(context);

            ITerminalNode identifier = context.Identifier();

            MethodCall methodCall = new MethodCall(identifier.GetText());
            
            while (_currentExpressions.Count > 0)
                methodCall.Parameters.Insert(0, _currentExpressions.Pop());

            _currentExpressions.Push(methodCall);
        }
        
        public override void ExitPrimaryVarLookup(ChoopParser.PrimaryVarLookupContext context)
        {
            base.ExitPrimaryVarLookup(context);

            _currentExpressions.Push(new LookupExpression(context.Identifier().GetText()));
        }

        public override void ExitPrimaryArrayLookup(ChoopParser.PrimaryArrayLookupContext context)
        {
            base.ExitPrimaryArrayLookup(context);

            IExpression index = _currentExpressions.Pop();
            ArrayLookupExpression lookup = new ArrayLookupExpression(context.Identifier().GetText(), index);

            _currentExpressions.Push(lookup);
        }

        #endregion

        #region Unary Expressions

        // TODO optimisations inline

        public override void ExitUnaryMinus(ChoopParser.UnaryMinusContext context)
        {
            base.ExitUnaryMinus(context);

            IExpression expression = _currentExpressions.Pop();
            _currentExpressions.Push(new UnaryExpression(expression, UnaryOperator.Minus));
        }

        public override void ExitUnaryNot(ChoopParser.UnaryNotContext context)
        {
            base.ExitUnaryNot(context);

            IExpression expression = _currentExpressions.Pop();
            _currentExpressions.Push(new UnaryExpression(expression, UnaryOperator.Not));
        }

        #endregion

        #region Binary Expressions

        public override void ExitExpressionPow(ChoopParser.ExpressionPowContext context)
        {
            base.ExitExpressionPow(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Pow, first, second));
        }

        public override void ExitExpressionMult(ChoopParser.ExpressionMultContext context)
        {
            base.ExitExpressionMult(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Multiply, first, second));
        }

        public override void ExitExpressionDivide(ChoopParser.ExpressionDivideContext context)
        {
            base.ExitExpressionDivide(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Divide, first, second));
        }

        public override void ExitExpressionMod(ChoopParser.ExpressionModContext context)
        {
            base.ExitExpressionMod(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Mod, first, second));
        }

        public override void ExitExpressionConcat(ChoopParser.ExpressionConcatContext context)
        {
            base.ExitExpressionConcat(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Concat, first, second));
        }

        public override void ExitExpressionPlus(ChoopParser.ExpressionPlusContext context)
        {
            base.ExitExpressionPlus(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Plus, first, second));
        }

        public override void ExitExpressionMinus(ChoopParser.ExpressionMinusContext context)
        {
            base.ExitExpressionMinus(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Minus, first, second));
        }

        public override void ExitExpressionLShift(ChoopParser.ExpressionLShiftContext context)
        {
            base.ExitExpressionLShift(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.LShift, first, second));
        }

        public override void ExitExpressionRShift(ChoopParser.ExpressionRShiftContext context)
        {
            base.ExitExpressionRShift(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.RShift, first, second));
        }

        public override void ExitExpressionLT(ChoopParser.ExpressionLTContext context)
        {
            base.ExitExpressionLT(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.LessThan, first, second));
        }

        public override void ExitExpressionGT(ChoopParser.ExpressionGTContext context)
        {
            base.ExitExpressionGT(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.GreaterThan, first, second));
        }

        public override void ExitExpressionLTE(ChoopParser.ExpressionLTEContext context)
        {
            base.ExitExpressionLTE(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.LessThanEq, first, second));
        }

        public override void ExitExpressionGTE(ChoopParser.ExpressionGTEContext context)
        {
            base.ExitExpressionGTE(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.GreaterThanEq, first, second));
        }

        public override void ExitExpressionEquals(ChoopParser.ExpressionEqualsContext context)
        {
            base.ExitExpressionEquals(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Equal, first, second));
        }

        public override void ExitExpressionNEquals(ChoopParser.ExpressionNEqualsContext context)
        {
            base.ExitExpressionNEquals(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.NotEqual, first, second));
        }

        public override void ExitExpressionAnd(ChoopParser.ExpressionAndContext context)
        {
            base.ExitExpressionAnd(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.And, first, second));
        }

        public override void ExitExpressionOr(ChoopParser.ExpressionOrContext context)
        {
            base.ExitExpressionOr(context);

            IExpression second = _currentExpressions.Pop();
            IExpression first = _currentExpressions.Pop();
            _currentExpressions.Push(new CompoundExpression(CompundOperator.Or, first, second));
        }

        #endregion

        #endregion

        #endregion
    }
}