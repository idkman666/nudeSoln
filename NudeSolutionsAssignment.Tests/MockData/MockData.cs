using NudeSolutionsAssignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NudeSolutionsAssignment.Tests.MockData
{
    internal class MockData
    {
        public static ItemCollection GetItemsList()
        {
            List<Item> items = new List<Item>()
           {
               new Item(){itemId="123",itemName="somename", price=123},
               new Item(){itemId="1233",itemName="name2", price=454}
           };
            Dictionary<string, IList<Item>> data = new Dictionary<string, IList<Item>>()
            {
                { "1", items },
                { "2", items }
            };

            ItemCollection itemCollection = new ItemCollection()
            {
                categoryItemsMap = data,
                collectionId = "123",
                userId = "11",
            };
            return itemCollection;
            
        }
    }
}
