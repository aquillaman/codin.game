using System;
using System.Collections.Generic;

namespace Aneo
{
    class Solution
    {
        private static readonly decimal Mps = new decimal(3.6);

        public static void Main(string[] args)
        {
            int speedLimit = int.Parse(Console.ReadLine());
            int lightsCount = int.Parse(Console.ReadLine());

            Dictionary<decimal, decimal> input = new Dictionary<decimal, decimal>();
            for (int i = 0; i < lightsCount; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int distance = int.Parse(inputs[0]);
                int duration = int.Parse(inputs[1]);

                input.Add(distance, duration);
            }

            int bestSpeed = 0;
            while (ProcessInput(input, speedLimit, out bestSpeed))
            {
                speedLimit = bestSpeed;
            }

            Console.WriteLine(bestSpeed);
        }

        private static bool ProcessInput(Dictionary<decimal, decimal> input, int speedLimit, out int bestSpeed)
        {
            bool changed = false;
            bestSpeed = speedLimit;
            foreach (var kv in input)
            {
                var speed = GetBestSpeed(kv.Key, kv.Value, speedLimit);
                if (speed < speedLimit)
                {
                    changed = true;
                    speedLimit = bestSpeed = speed;
                }
            }

            return changed;
        }

        private static int GetBestSpeed(decimal distance, decimal duration, int speedLimit)
        {
            decimal timeLimit = distance / (speedLimit / Mps);

            for (decimal start = 0;; start += duration * 2)
            {
                decimal end = start + duration;

                if (timeLimit >= end) continue;

                if (timeLimit >= start && timeLimit < end) return speedLimit;

                for (decimal t = start; t <= start + duration; t++)
                {
                    if (t == 0) continue;

                    var speed = decimal.ToInt32(distance / t * Mps);

                    if (speed <= speedLimit)
                    {
                        return speed;
                    }
                }
            }
        }
    }
}