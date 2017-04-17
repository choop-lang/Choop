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
            string code = "sprite hello {\r\n\tconst hello = -3.2e-2;\r\n\tvar T20 = \"Home time\";\r\n}";

            Console.WriteLine(code);

            AntlrInputStream input = new AntlrInputStream(code);

            ChoopLexer lexer = new ChoopLexer(input);

            CommonTokenStream tokens = new CommonTokenStream(lexer);

            ChoopParser parser = new ChoopParser(tokens);

            ChoopParser.RootContext root = parser.root();
            
            Console.WriteLine(root.ToStringTree(parser));

            Console.ReadLine();
        }
    }
}
