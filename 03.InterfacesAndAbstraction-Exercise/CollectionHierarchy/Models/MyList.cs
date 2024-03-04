using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class MyList : IMyList
    {
        private List<string> items;

        public MyList()
        {
            items = new List<string>();
        }

        public int Used => items.Count;

        public int Add(string item)
        {
            items.Insert(0, item);

            return 0;
        }

        public string Remove()
        {
            string item = items.First();

            items.Remove(items.First());

            return item;
        }
    }
}
