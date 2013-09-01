using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class AMain
    {
        public const int knapsackMaxWeight = 10000; //something about this being lower screws up the result
        public const int numberOfItems = 1000;

        static void Main(string[] args)
        {
            int iterations = 20;
            TimeSpan totalSequentialTime = new TimeSpan();
            TimeSpan totalParallelTime = new TimeSpan();
            int seqResult = 0;
            int pResult = 0;

            // construct knapsack
            Console.WriteLine("Constructing list of items and table.");
            ItemList items = new ItemList(numberOfItems);

            // costruct table
            int[,] dTable = new int[numberOfItems+1, knapsackMaxWeight+1];

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("\nIteration " + (i + 1) + ":");

                // parallel
                TimeSpan parallelTime = RunParallel(items, dTable, out pResult);
                totalParallelTime += parallelTime;
                Console.WriteLine("Parallel knapsack lasted = " + parallelTime);

                // sequential
                TimeSpan sequentialTime = RunSequential(items, dTable, out seqResult);
                Console.WriteLine("Sequential knapsack lasted = " + sequentialTime);
                totalSequentialTime += sequentialTime;

                // compare output to ensure same results
                if (seqResult != pResult)
                {
                    Console.WriteLine("ERROR: P and S values don't match");
                    break;
                }
            }

            Console.WriteLine("\nSequential total time = " + totalSequentialTime);
            Console.WriteLine("Parallel total time = " + totalParallelTime);
            Console.WriteLine("Speed up = " + totalSequentialTime.TotalMilliseconds / totalParallelTime.TotalMilliseconds);

            Console.ReadLine();
        }

        private static TimeSpan RunSequential(ItemList items, int[,] dTable, out int result)
        {
            DateTime start = DateTime.Now;

            SequentialKnapSack.Execute(items, dTable, out result);

            DateTime end = DateTime.Now;

            return end - start;
        }

        private static TimeSpan RunParallel(ItemList items, int[,] dTable, out int result)
        {
            DateTime start = DateTime.Now;

            ParallelKnapSack.Execute(items, dTable, out result);

            DateTime end = DateTime.Now;

            return end - start;
        }
    }
}
