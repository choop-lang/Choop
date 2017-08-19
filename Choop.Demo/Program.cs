using System;
using Choop.Compiler;
using Choop.Compiler.Interfaces;

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
            const string inputProject = @"Samples\Fibonacci\";
            const string outputFile = @"Output\project.json";

            // Create compiler instance
            using (ChoopCompiler compiler = new ChoopCompiler(new DiskFileProvider()))
            {
                // Compile code
                compiler.LoadProject(inputProject);
                compiler.Compile();

                // Check if compilation was successful
                if (compiler.HasErrors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not compile file:");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (CompilerError error in compiler.CompilerErrors)
                        Console.WriteLine(string.IsNullOrEmpty(error.TokenText)
                            ? $"Line {error.Line}:{error.Col}  {error.Message} ({error.FileName})"
                            : $"Line {error.Line}:{error.Col}..{error.StopIndex - error.StartIndex + error.Col}\t{error.Message} ({error.FileName})");
                }
                else
                {
                    // Compilation finished
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully compiled!\r\n");

                    compiler.Save(outputFile);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Saved to {0}", outputFile);
                }
            }

            // Keep console open until dismissed by user
            Console.ReadLine();
        }
    }
}