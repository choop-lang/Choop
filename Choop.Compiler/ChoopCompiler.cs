using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.ObjectModel;
using System.IO;
using Choop.Compiler.ChoopModel;

namespace Choop.Compiler
{
    /// <summary>
    /// Compiles Choop code.
    /// </summary>
    public class ChoopCompiler
    {
        #region Properties

        /// <summary>
        /// Gets the collection of compiler errors that occured whilst compiling the file. 
        /// </summary>
        public Collection<CompilerError> CompileErrors { get; private set; }

        /// <summary>
        /// Gets whether any compiler errors occured.
        /// </summary>
        public bool HasErrors => CompileErrors.Count > 0;

        #endregion

        #region Methods

        /// <summary>
        /// Compiles the code from the specified input stream.
        /// </summary>
        /// <param name="input">The input stream containing the code to compile.</param>
        public void Compile(Stream input)
        {
            Compile(new AntlrInputStream(input));
        }

        /// <summary>
        /// Compiles the specified code.
        /// </summary>
        /// <param name="input">The code to compile.</param>
        public void Compile(string input)
        {
            Compile(new AntlrInputStream(input));
        }

        /// <summary>
        /// Compiles the code from the specified input stream.
        /// </summary>
        /// <param name="input">The input stream to compile the code from.</param>
        private void Compile(ICharStream input)
        {
            // Clear compile errors
            CompileErrors = new Collection<CompilerError>();

            // Create the lexer
            ChoopLexer lexer = new ChoopLexer(input, CompileErrors);

            // Get the tokens from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            // Create the parser
            ChoopParser parser = new ChoopParser(tokens, CompileErrors);

            // Gets the parse tree
            ChoopParser.RootContext root = parser.root();
            // Create the listener which will translate the code
            // ChoopListener translator = new ChoopListener(parser);

            // Walk along the tree, causing it to be translated
            // ParseTreeWalker.Default.Walk(translator, root);

            // Sprites

            foreach (ChoopParser.SpriteContext spriteAst in root.sprite())
            {
                // Get basic info
                string name = spriteAst.Identifier().GetText();
                string metaFile = name + ".sm";

                ChoopParser.MetaAttributeContext metaAttribute = spriteAst.metaAttribute();
                if (metaAttribute != null)
                {
                    metaFile = metaAttribute.constant().GetText();
                }

                // Create declaration object
                SpriteBaseDeclaration sprite;

                if (name.Equals("stage", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Stage
                    sprite = new StageDeclaration(metaFile);
                }
                else
                {
                    // Sprite
                    sprite = new SpriteDeclaration(name, metaFile);
                }

                // Get imported modules
                foreach (ChoopParser.UsingStmtContext usingStmtAst in spriteAst.usingStmt())
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
                        parser.NotifyErrorListeners(module.Symbol, "Duplicate using statement", null);
                    }
                }

                // Translate body
                TranslateSpriteBody(spriteAst.spriteBody(), sprite, parser);
            }

            // Modules

            foreach (ChoopParser.ModuleContext moduleAst in root.module())
            {
                // Get basic info
                string name = moduleAst.Identifier().GetText();

                // Create declaration
                ModuleDeclaration module = new ModuleDeclaration(name);

                // Translate body
                TranslateSpriteBody(moduleAst.spriteBody(), module, parser);
            }

            // Superglobal variables and constants

            foreach (ChoopParser.GlobalStmtContext globalStmtAst in root.globalStmt())
            {
                // Translate statement
                // TODO
            }
        }

        /// <summary>
        /// Imports a sprite body AST into a sprite declaration object.
        /// </summary>
        /// <param name="spriteBodyAst">The AST of the sprite body.</param>
        /// <param name="sprite">The sprite declaration.</param>
        /// <param name="parser">The parser to report errors to.</param>
        private static void TranslateSpriteBody(ChoopParser.SpriteBodyContext spriteBodyAst, ISpriteDeclaration sprite,
            Parser parser)
        {
            // Translate methods
            foreach (ChoopParser.MethodDeclarationContext methodAst in spriteBodyAst.methodDeclaration())
            {
                // Get basic info
                string name = methodAst.Identifier().GetText();
                ITerminalNode[] inlineTags = methodAst.InlineTag();
                ITerminalNode[] atomicTags = methodAst.AtomicTag();
                ITerminalNode[] unsafeTags = methodAst.UnsafeTag();
                bool hasReturn = false;
                DataType type = DataType.Object;
                ChoopParser.TypeSpecifierContext typeSpecifierAst = methodAst.typeSpecifier();
                if (typeSpecifierAst == null)
                {
                    if (methodAst.FunctionTag() != null)
                        hasReturn = true;
                }
                else
                {
                    type = GetDataType(typeSpecifierAst);
                }

                // Validate modifiers
                ValidateModifier(inlineTags, "inline", parser);
                ValidateModifier(atomicTags, "atomic", parser);
                ValidateModifier(unsafeTags, "unsafe", parser);

                // Create declaration
                MethodDeclaration method = new MethodDeclaration(name, type, hasReturn, unsafeTags.Length > 0,
                    inlineTags.Length > 0, atomicTags.Length > 0);

                // Translate body

                // Add to sprite
                sprite.Methods.Add(method);
            }

            // Translate event handlers
            foreach (ChoopParser.EventHandlerContext eventHandlerAst in spriteBodyAst.eventHandler())
            {
                // Get name
                ITerminalNode identifier = eventHandlerAst.Identifier();
                string name;
                if (ChoopParser.EventNames.TryGetValue(identifier.GetText(), out name))
                {
                    // Get modifiers
                    ITerminalNode[] atomicTags = eventHandlerAst.AtomicTag();
                    ITerminalNode[] unsafeTags = eventHandlerAst.UnsafeTag();

                    // Get parameter
                    TerminalExpression parameter = null;
                    ChoopParser.ConstantContext constant = eventHandlerAst.constant();
                    if (constant != null)
                        parameter = new TerminalExpression(constant.GetText(), DataType.String); // Todo: fix

                    // Validate modifiers
                    ValidateModifier(atomicTags, "atomic", parser);
                    ValidateModifier(unsafeTags, "unsafe", parser);

                    // Create declaration
                    ChoopModel.EventHandler eventHandler =
                        new ChoopModel.EventHandler(name, parameter, unsafeTags.Length > 0, atomicTags.Length > 0);

                    // Translate event body

                    // Add to sprite
                    sprite.EventHandlers.Add(eventHandler);
                }
                else
                {
                    // Couldn't find match for event name
                    parser.NotifyErrorListeners(identifier.Symbol, "Could not recognise event name", null);
                }
            }

            // Translate global statements
            foreach (ChoopParser.GlobalStmtContext globalStmt in spriteBodyAst.globalStmt())
            {
                TranslateGlobalStmt(globalStmt, sprite, parser);
            }
        }

        /// <summary>
        /// Translates a global statement.
        /// </summary>
        /// <param name="stmt">The statement to translate.</param>
        /// <param name="sprite">The sprite declaration.</param>
        /// <param name="parser">The parser to report errors to.</param>
        private static void TranslateGlobalStmt(ChoopParser.GlobalStmtContext stmt, ISpriteDeclaration sprite,
            Parser parser)
        {
            IParseTree declaration = stmt.GetChild(0).GetChild(0);

            string identifier;
            DataType type;

            ChoopParser.ConstDeclarationContext constStmt = declaration as ChoopParser.ConstDeclarationContext;
            if (constStmt != null)
            {
                identifier = constStmt.Identifier().GetText();
                type = GetDataType(constStmt.typeSpecifier());
                TerminalExpression value = new TerminalExpression(constStmt.constant().GetText(), type);

                ConstDeclaration constDecl = new ConstDeclaration(identifier, type, value);
                sprite.Constants.Add(constDecl);
            }
            else
            {
                ChoopParser.VarGlobalDeclarationContext globalVarStmt =
                    declaration as ChoopParser.VarGlobalDeclarationContext;
                if (globalVarStmt != null)
                {
                    identifier = globalVarStmt.Identifier().GetText();
                    type = GetDataType(globalVarStmt.typeSpecifier());
                    //TerminalExpression value = new TerminalExpression();
                }
                else
                {
                    ChoopParser.ArrayGlobalDeclarationContext globalArrayStmt =
                        declaration as ChoopParser.ArrayGlobalDeclarationContext;
                    if (globalArrayStmt != null)
                    {
                    }
                    else
                    {
                        ChoopParser.ListGlobalDeclarationContext globalListStmt =
                            declaration as ChoopParser.ListGlobalDeclarationContext;
                        if (globalListStmt != null)
                        {
                        }
                        else
                        {
                            // Statement isn't a well-formed global statement
                            throw new ArgumentException("Invalid global statement", nameof(stmt));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validates that a modifier was not specified multiple times.
        /// </summary>
        /// <param name="modifier">The modifier to validate.</param>
        /// <param name="name">The display name of the modifier.</param>
        /// <param name="parser">The parser to report errors to.</param>
        private static void ValidateModifier(ITerminalNode[] modifier, string name, Parser parser)
        {
            // Only allow max 1
            if (modifier.Length <= 1) return;

            for (int i = 1; i < modifier.Length; i++)
                parser.NotifyErrorListeners(modifier[i].Symbol, $"Duplicate '{name}' modifier", null);
        }

        /// <summary>
        /// Converts a <see cref="ChoopParser.TypeSpecifierContext"/> instance into a <see cref="DataType"/> value.
        /// </summary>
        /// <param name="typeSpecifierAst">The <see cref="ChoopParser.TypeSpecifierContext"/> instance to convert. Can be null.</param>
        /// <returns>The equivalent <see cref="DataType"/> for the <see cref="ChoopParser.TypeSpecifierContext"/> instance.</returns>
        private static DataType GetDataType(ChoopParser.TypeSpecifierContext typeSpecifierAst)
        {
            if (typeSpecifierAst == null)
                return DataType.Object;

            switch (typeSpecifierAst.start.Type)
            {
                case ChoopParser.TypeNum:
                    return DataType.Number;
                case ChoopParser.TypeString:
                    return DataType.String;
                case ChoopParser.TypeBool:
                    return DataType.Boolean;
                default:
                    throw new ArgumentException("Unknown type", nameof(typeSpecifierAst));
            }
        }

        #endregion
    }
}