using System;
using System.IO;

namespace CF314.MayBrainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            Interpreter.RunFile(args[0]);
            Environment.Exit(0);
        }
    }
}