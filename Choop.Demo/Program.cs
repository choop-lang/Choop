using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Choop.Compiler;

namespace Choop.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            AntlrInputStream input = new AntlrInputStream("sprite hello {\r\nconst hello = true;\r\n}");

            ChoopLexer lexer = new ChoopLexer(input);

            CommonTokenStream tokens = new CommonTokenStream(lexer);

            ChoopParser parser = new ChoopParser(tokens);

            ChoopParser.RootContext root = parser.root();
            
            Console.WriteLine(root.ToStringTree(parser));

            Console.ReadLine();
        }
    }
}
