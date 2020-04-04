using System;
using System.Collections.Generic;
using System.Linq;

namespace Vestigium
{
    class Solution
    {
        public string Vestigium(int N, int[][] M)
        {
            int trace = M.Select((row, i) => row[i]).Sum();

            var set = new HashSet<int>(Enumerable.Range(1, N));

            var repeatedRows = 0;
            var repeatedCols = 0;
            for (int i = 0; i < N; i++)
            {
                set.Clear();
                for (int j = 0; j < N; j++)
                {
                    if (set.Contains(M[i][j]))
                    {
                        repeatedRows++;
                        break;
                    }
                    set.Add(M[i][j]);
                }

                set.Clear();
                for (int j = 0; j < N; j++)
                {
                    if (set.Contains(M[j][i]))
                    {
                        repeatedCols++;
                        break;
                    }
                    set.Add(M[j][i]);
                }
            }

            return $"{trace} {repeatedRows} {repeatedCols}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            long T = ReadSingleNumber;

            for (long t = 0; t < T; t++)
            {
                int N = (int)ReadSingleNumber;
                int[][] M = new int[N][];

                for (int i = 0; i < N; i++)
                {
                    M[i] = ReadArrayOfNumbers;
                }

                Console.WriteLine($"Case #{t + 1}: {new Solution().Vestigium(N, M)}");
            }
        }


        static string ReadString => Console.ReadLine();
        static long ReadSingleNumber => long.Parse(Console.ReadLine());

        static Stack<long> lineBuffer = new Stack<long>();
        static long ReadNextNumber
        {
            get
            {
                if (!lineBuffer.Any())
                {
                    Console.ReadLine().Split(' ')
                        .Select(n => long.Parse(n))
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
