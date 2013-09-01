using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
	public class SequentialKnapSack
	{
        public static void Execute(ItemList item, int[,] table, out int result) 
        {
            // initial row - no items
            for (int i = 0; i < AMain.knapsackMaxWeight+1; i++)
            {
                table[0, i] = 0;
            }

            // process each item in each row in our dp table
            for (int element = 1; element < AMain.numberOfItems + 1; element++)
            {
                Item curItem = item.list.ElementAt<Item>(element - 1);

                for (int work = 0; work < AMain.knapsackMaxWeight + 1; work++)
                {
                    if (curItem.weight <= work)
                    {
                        table[element, work] = Math.Max(table[element - 1, work], curItem.value + table[element - 1, work - curItem.weight]);
                    }
                    else
                    {
                        table[element, work] = table[element - 1, work];
                    }
                }
            }

            result = table[AMain.numberOfItems, AMain.knapsackMaxWeight];

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
