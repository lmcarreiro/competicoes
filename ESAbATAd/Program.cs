using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ESAbATAd
{
    class Program
    {
        static void Print(int i)
        {
            //File.AppendAllText("a.txt", i.ToString() + Environment.NewLine);
            Console.Out.WriteLine(i);
            Console.Out.Flush();
        }

        static void Print(string s)
        {
            //File.AppendAllText("a.txt", s + Environment.NewLine);
            Console.Out.WriteLine(s);
            Console.Out.Flush();
        }

        static string ESAbATAd(int B)
        {
            var array = new bool?[B];

            if (B == 10)
            {
                ReadFirst10(array);
            }
            else if (B == 20 || B == 100)
            {
                int? posEq = null;
                int? posDiff = null;

                int? endFilledSize = null;

                int bitsRead = 0;
                int questionsAsked = 0;

                for (int i = 1; i <= B && questionsAsked < 150; i++)
                {
                    if ((questionsAsked % 10) == 0)
                    {
                        if (posEq == null && posDiff == null)
                        {
                            ReadSymmetric(array, B, i, ref posEq, ref posDiff);
                            questionsAsked += 2;
                            bitsRead += 2;
                        }
                        else if (posEq != null ^ posDiff != null)
                        {
                            DetectAndReflect(array, ref i, ref questionsAsked, ref posEq, ref posDiff, ref endFilledSize);
                        }
                        else if (posEq != null && posDiff != null)
                        {
                            DetectAndReflect(array, ref i, ref questionsAsked, ref posEq, ref posDiff, ref endFilledSize);
                        }
                        else throw new InvalidOperationException();
                    }
                    else
                    {
                        if (posEq == null || posDiff == null)
                        {
                            if (((questionsAsked + 1) % 10) == 0)
                            {
                                var trash = ReadRepeated(array, 0);
                                questionsAsked += 1;
                                i--;
                                // TODO: if fails, put DetectAndReflect between ReadSymmetric here
                            }
                            else
                            {
                                ReadSymmetric(array, B, i, ref posEq, ref posDiff);
                                questionsAsked += 2;
                                bitsRead += 2;

                                if (posEq.HasValue == posDiff.HasValue)
                                {
                                    endFilledSize = i;
                                }
                            }
                        }
                        else
                        {
                            ReadHere(array, B, i);
                            questionsAsked += 1;
                            bitsRead += 1;
                        }
                    }

                    if (bitsRead == B)
                    {
                        break;
                    }
                }
            }

            return string.Join("", array.Select(b => b.Value ? '1' : '0'));
        }

        static void DetectAndReflect(bool?[] array, ref int i, ref int questionsAsked, ref int? posEq, ref int? posDiff, ref int? endFilledSize)
        {
            if (posEq != null ^ posDiff != null)
            {
                int posRepeated = posEq.HasValue ? posEq.Value : posDiff.Value;
                var repeated = ReadRepeated(array, posRepeated);
                questionsAsked += 1;
                if (repeated != array[posRepeated])
                {
                    Complement(array);
                }

                i--;
            }
            else if (posEq != null && posDiff != null)
            {
                var repeatedEq = ReadRepeated(array, posEq.Value);
                var repeatedDiff = ReadRepeated(array, posDiff.Value);
                questionsAsked += 2;

                if (repeatedEq == array[posEq.Value] && repeatedDiff == array[posDiff.Value])
                {
                    // nada
                }
                else if (repeatedEq == array[posEq.Value] && repeatedDiff != array[posDiff.Value])
                {
                    Reverse(array);

                    var temp = i;
                    i = endFilledSize.Value + 1;
                    endFilledSize = temp - 1;
                }
                else if (repeatedEq != array[posEq.Value] && repeatedDiff == array[posDiff.Value])
                {
                    Complement(array);
                    Reverse(array);

                    var temp = i;
                    i = endFilledSize.Value + 1;
                    endFilledSize = temp - 1;
                }
                else if (repeatedEq != array[posEq.Value] && repeatedDiff != array[posDiff.Value])
                {
                    Complement(array);
                }
                else throw new InvalidOperationException();

                i--;
            }
        }

        private static void Complement(bool?[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].HasValue ? !array[i].Value : (bool?)null;
            }
        }

        private static void Reverse(bool?[] array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                bool? temp = array[i];
                array[i] = array[array.Length - 1 - i];
                array[array.Length - 1 - i] = temp;
            }
        }

        static bool ReadRepeated(bool?[] array, int where)
        {
            Print(where + 1);
            return ReadSingleNumber == 1;
        }

        static void ReadHere(bool?[] array, int B, int q)
        {
            int here = q - 1;

            if (array[here] != null)
            {
                throw new InvalidOperationException();
            }

            Print(here + 1);
            array[here] = ReadSingleNumber == 1;
        }

        static void ReadSymmetric(bool?[] array, int B, int q, ref int? posEq, ref int? posDiff)
        {
            int here = q - 1;
            int there = B - q;

            if (array[here] != null || array[there] != null)
            {
                throw new InvalidOperationException();
            }

            Print(here + 1);
            array[here] = ReadSingleNumber == 1;

            Print(there + 1);
            array[there] = ReadSingleNumber == 1;

            if (array[here] == array[there] & posEq == null)
            {
                posEq = here;
            }
            else if (array[here] != array[there] & posDiff == null)
            {
                posDiff = here;
            }
        }

        static void ReadFirst10(bool?[] array)
        {
            for (int q = 1; q <= 10; q++)
            {
                Print(q);
                array[q - 1] = ReadSingleNumber == 1;
            }
        }

        static void Main(string[] args)
        {
            int T = ReadNextNumber;
            int B = ReadNextNumber;

            for (int t = 0; t < T; t++)
            {
                Print(ESAbATAd((int)B));

                if (ReadString != "Y")
                {
                    break;
                }
            }
        }


        static string ReadString => Console.ReadLine();
        static long ReadSingleNumber => long.Parse(Console.ReadLine());

        static Stack<int> lineBuffer = new Stack<int>();
        static int ReadNextNumber
        {
            get
            {
                if (!lineBuffer.Any())
                {
                    Console.ReadLine().Split(' ')
                        .Select(n => int.Parse(n))
                        .Reverse()
                        .ToList()
                        .ForEach(lineBuffer.Push);
                }

                return lineBuffer.Pop();
            }
        }

        static int[] ReadArrayOfNumbers => Console.ReadLine().Split(' ')
            .Select(n => int.Parse(n))
            .ToArray();
    }
}
