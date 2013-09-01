using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Item
    {
        public int weight;
        public int value;
        Random rand;

        public Item()
        {
            rand = new Random();
            weight = rand.Next(5, 51); // weight is random between 5-50
            value = rand.Next(5, 51);  // value is random between 5-50
        }
    }

    public class ItemList
    {
        public List<Item> list;

        public ItemList(int numItems)
        {
            list = new List<Item>();

            for (int i = 0; i < numItems; i++)
            {
                Item item = new Item();

                list.Add(item);
                Thread.Sleep(10);

               //  Console.WriteLine("item " + i + "(w: " + item.weight + ") " + "(v: " +  item.value +")");
            }
        }   
    }
}
