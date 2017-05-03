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
            string filepath = "test.ch";

            // Open file
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Create compiler instance
                ChoopCompiler compiler = new ChoopCompiler();

                // Compile code
                compiler.Compile(reader.BaseStream);

                // Check if compilation was successful
                if (compiler.HasErrors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not compile file:");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (CompilerError error in compiler.CompileErrors)
                    {
                        if (string.IsNullOrEmpty(error.TokenText))
                        {
                            Console.WriteLine($"Line {error.Line}:{error.Col}  {error.Message}");
                        }
                        else
                        {
                            Console.WriteLine($"Line {error.Line}:{error.Col}..{error.StopIndex - error.StartIndex + error.Col}\t{error.Message}");
                        }
                    }
                }
                else
                {
                    // Compilation finished
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully compiled!");
                }
            }

            // Keep console open until dismissed by user
            Console.ReadLine();
        }
    }
}
