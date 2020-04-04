using System;
using System.Collections.Generic;
using System.Linq;

namespace ParentingPartnering
{
    class Solution
    {
        public string ParentingPartnering(Activity[] activities)
        {
            var lastC = new Activity(0, 0);
            var lastJ = new Activity(0, 0);

            foreach (var a in activities.OrderBy(a => a.Start).ThenByDescending(a => a.End - a.Start))
            {
                if (a.Start >= lastC.End)
                {
                    a.Who = 'C';
                    lastC = a;
                }
                else if (a.Start >= lastJ.End)
                {
                    a.Who = 'J';
                    lastJ = a;
                }
                else
                {
                    return "IMPOSSIBLE";
                }
            }

            return string.Join("", activities.Select(a => a.Who));
        }
    }

    class Activity
    {
        public Activity(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; }
        public int End { get; }
        public char Who { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            long T = ReadSingleNumber;

            for (long t = 0; t < T; t++)
            {
                int N = (int)ReadSingleNumber;
                var activities = new Activity[N];

                for (int i = 0; i < N; i++)
                {
                    activities[i] = new Activity((int) ReadNextNumber, (int) ReadNextNumber);
                }

                Console.WriteLine($"Case #{t + 1}: {new Solution().ParentingPartnering(activities)}");
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
