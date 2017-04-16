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
            AntlrInputStream input = new AntlrInputStream("sprite 0hello{}");

            ChoopLexer lexer = new ChoopLexer(input);

            CommonTokenStream tokens = new CommonTokenStream(lexer);

            ChoopParser parser = new ChoopParser(tokens);

            Console.WriteLine(parser.compilation_unit().ToStringTree(parser));

            Console.ReadLine();
        }
    }
}
