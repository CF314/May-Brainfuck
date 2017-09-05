using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CF314.MayBrainfuck
{
    public class Interpreter
    {
        public static void RunFile(string path)
        {
            RunCode(
                File.ReadAllText(path));
        }

        public static void RunCode(string code)
        {
            byte[] memory = new byte[100];
            Stack<uint> loops = new Stack<uint>();
            int currentCell = 0;

            checked
            {
                for (uint readIndex = 0; readIndex < code.Length; ++readIndex)
                {
                    char instruction = code[(int)readIndex];

                    if (instruction == '+')         // increment memory cell value
                        ++memory[currentCell];

                    else if (instruction == '-')    // decrement memory cell value
                        --memory[currentCell];

                    else if (instruction == '>')    // use next memory cell
                        ++currentCell;

                    else if (instruction == '<')    // use previous memory cell
                        --currentCell;

                    else if (instruction == '.')    // print value of current cell as ASCII character
                        Console.Write((byte)memory[currentCell]);

                    else if (instruction == ',')    // read next character from the console and save it's ASCII value in the current cell
                        memory[currentCell] = (byte)Console.Read();

                    else if (instruction == '[')    // goto next ']' if the value of the current cell is 0
                    {                               // C++ equivalent: "while (memory[currentCell]) {"
                        if (memory[currentCell] == 0)
                        {
                            while (code[(int)readIndex] != ']')
                                ++readIndex;
                            ++readIndex;
                        }
                        else
                            loops.Push(readIndex);
                    }

                    else if (instruction == ']')    // goto previous '[' if the value of the current cell is not 0
                    {                               // C++ equivalent: "}" (end of "while (memory[currentCell] {")
                        if (memory[currentCell] == 0)
                            loops.Pop();
                        else
                        {
                            readIndex = loops.Peek();
                        }
                    }
                }
            }
        }
    }
}
