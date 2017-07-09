using System;
using System.IO;
using Choop.Compiler;

namespace Choop.Demo
{
    internal static class Program
    {
        /// <summary>
        /// The entry point for the program.
        /// </summary>
        /// <param name="args">The arguments supplied to the program.</param>
        private static void Main(string[] args)
        {
            // The path of the file to compile
            const string filepath = "test.ch";

            // Open file
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Create compiler instance
                ChoopCompiler compiler = new ChoopCompiler("Test");

                // Compile code
                compiler.AddCode(reader.BaseStream, filepath);
                compiler.Compile();

                // Check if compilation was successful
                if (compiler.HasErrors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not compile file:");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (CompilerError error in compiler.CompilerErrors)
                    {
                        Console.WriteLine(string.IsNullOrEmpty(error.TokenText)
                            ? $"Line {error.Line}:{error.Col}  {error.Message} ({error.FileName})"
                            : $"Line {error.Line}:{error.Col}..{error.StopIndex - error.StartIndex + error.Col}\t{error.Message} ({error.FileName})");
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
