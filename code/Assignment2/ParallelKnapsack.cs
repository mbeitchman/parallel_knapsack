using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class ParallelKnapSack
    {
        public static void Execute(ItemList item, int[,] table, out int result) 
        {
            // initial row - no items
            Parallel.For(0, (AMain.knapsackMaxWeight/2)+1, (i)=>
            {
                table[0, i] = 0;
            });

            // process each item in each row in our dp table in parallel
            for (int element = 1; element < AMain.numberOfItems + 1; element++)
            {
                Item curItem = item.list.ElementAt<Item>(element - 1);

                Parallel.For(0, AMain.knapsackMaxWeight+1, (work) =>
                {
                    if (curItem.weight <= work)
                    {
                        table[element, work] = Math.Max(table[element - 1, work], curItem.value + table[element - 1, work - curItem.weight]);
                    }
                    else
                    {
                        table[element, work] = table[element - 1, work];
                    }
                });
            }

            /* Process the dp table as 4 blocks
             * the upper left block and lower right block are processed sequentially
             * the upper gith block and lower left block are processed in parallel
             */

            //for (int element = 1; element < AMain.numberOfItems / 2; element++)
            //{
            //    Item curItem = item.list.ElementAt<Item>(element - 1);

            //    Parallel.For(0, AMain.knapsackMaxWeight / 2, (work) =>
            //    {
            //        if (curItem.weight <= work)
            //        {
            //            table[element, work] = Math.Max(table[element - 1, work], curItem.value + table[element - 1, work - curItem.weight]);
            //        }
            //        else
            //        {
            //            table[element, work] = table[element - 1, work];
            //        }
            //    });
            //}

            //Parallel.Invoke(
            //    delegate()
            //    {
            //        for (int element = 1; element < AMain.numberOfItems / 2; element++)
            //        {
            //            Item curItem = item.list.ElementAt<Item>(element - 1);

            //            Parallel.For(AMain.knapsackMaxWeight / 2, AMain.knapsackMaxWeight, (work) =>
            //            {
            //                if (curItem.weight <= work)
            //                {
            //                    table[element, work] = Math.Max(table[element - 1, work], curItem.value + table[element - 1, work - curItem.weight]);
            //                }
            //                else
            //                {
            //                    table[element, work] = table[element - 1, work];
            //                }
            //            });
            //        }
            //    },
            //    delegate()
            //    {
            //        for (int element = AMain.numberOfItems / 2; element < AMain.numberOfItems + 1; element++)
            //        {
            //            Item curItem = item.list.ElementAt<Item>(element - 1);

            //            Parallel.For(0, AMain.knapsackMaxWeight / 2, (work) =>
            //            {
            //                if (curItem.weight <= work)
            //                {
            //                    table[element, work] = Math.Max(table[element - 1, work], curItem.value + table[element - 1, work - curItem.weight]);
            //                }
            //                else
            //                {
            //                    table[element, work] = table[element - 1, work];
            //                }
            //            });
            //        }
            //    }
            //);

            //for (int element = AMain.numberOfItems / 2; element < AMain.numberOfItems + 1; element++)
            //{
            //    Item curItem = item.list.ElementAt<Item>(element - 1);

            //    Parallel.For(AMain.knapsackMaxWeight / 2, AMain.knapsackMaxWeight, (work) =>
            //    {
            //        if (curItem.weight <= work)
            //        {
            //            table[element, work] = Math.Max(table[element - 1, work], curItem.value + table[element - 1, work - curItem.weight]);
            //        }
            //        else
            //        {
            //            table[element, work] = table[element - 1, work];
            //        }
            //    });
            //}

            result =  table[AMain.numberOfItems, AMain.knapsackMaxWeight];
            //PrintTable(table);
        }

        public static void PrintTable(int[,] t)
        {
            for (int i = 0; i < AMain.numberOfItems + 1; i++)
            {
                for (int j = 0; j < AMain.knapsackMaxWeight + 1; j++)
                {
                    Console.Write(t[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
	}
}
