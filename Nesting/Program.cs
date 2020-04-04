using System;
using System.Collections.Generic;
using System.Linq;

namespace Nesting
{
    class Solution
    {
        public string Nesting(string S)
        {
            var list = Number.FromString(S);

            for (int i = 1; i < list.Count; i++)
            {
                int maxToRemove = new int[] {
                    list[i - 1].RightP,
                    list[i].LeftP,
                }.Min();

                list[i - 1].RightP -= maxToRemove;
                list[i].LeftP -= maxToRemove;
            }

            return string.Join("", list.Select(n => n.ToString()));
        }
    }

    public class Number
    {

        public static List<Number> FromString(string S)
        {
            List<Number> list = new List<Number>(S.Length);

            char last = S[0];
            int repeated = 0;
            foreach (char c in S)
            {
                if (c != last)
                {
                    list.Add(new Number((int)(last - '0'), repeated));
                    repeated = 1;
                    last = c;
                }
                else
                {
                    repeated++;
                }
            }
            list.Add(new Number((int)(last - '0'), repeated));

            return list;
        }

        public Number(int n, int repeated)
        {
            LeftP = RightP = N = n;
            Repeated = repeated;
        }

        public int LeftP { get; set; }
        public int RightP { get; set; }
        public int Repeated { get; set; }
        public int N { get; set; }

        public override string ToString()
        {
            return new string('(', LeftP) + new string((char)(N + (int)'0'), Repeated) + new string(')', RightP);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            long T = ReadSingleNumber;

            for (long t = 0; t < T; t++)
            {
                string S = ReadString;

                Console.WriteLine($"Case #{t + 1}: {new Solution().Nesting(S)}");
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
