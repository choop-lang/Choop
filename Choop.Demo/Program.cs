using System;
using System.IO;
using Choop.Compiler;

namespace Choop.Demo
{
    class Program
    {
        /// <summary>
        /// The entry point for the program.
        /// </summary>
        /// <param name="args">The arguments supplied to the program.</param>
        static void Main(string[] args)
        {
            // The path of the file to compile
            string filepath = "testerror.ch";

            // Open file
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Create compiler instance
                ChoopCompiler compiler = new ChoopCompiler();

                // Compile code
                compiler.Compile(reader.BaseStream);

                // Compilation finished
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Compilation finished");
            }

            // Keep console open until dismissed by user
            Console.ReadLine();
        }
    }
}
