using CollectionHierarchy.Core.Interfaces;
using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class AddRemoveCollection : IAddRemoveCollection
    {
        private List<string> items;

        public AddRemoveCollection()
        {
            items = new List<string>();
        }

        public int Add(string item)
        {
            items.Insert(0, item);

            return 0;
        }

        public string Remove()
        {
            string item = items.Last();

            items.Remove(items.Last());

            return item;
        }
    }
}
