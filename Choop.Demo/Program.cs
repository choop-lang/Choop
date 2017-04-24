using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            string filepath = "testerror.ch";

            using (StreamReader reader = new StreamReader(filepath))
            {
                ChoopCompiler compiler = new ChoopCompiler();

                compiler.Compile(reader.BaseStream);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Compilation finished");
            }

            Console.ReadLine();
        }
    }
}
