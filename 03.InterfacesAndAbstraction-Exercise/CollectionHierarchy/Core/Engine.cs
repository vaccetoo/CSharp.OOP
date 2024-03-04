
using CollectionHierarchy.Core.Interfaces;
using CollectionHierarchy.Models;

namespace CollectionHierarchy.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            AddCollection addCollection = new AddCollection();
            AddRemoveCollection removeCollection = new AddRemoveCollection();
            MyList myList = new MyList();

            string[] items = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int removeOperations = int.Parse(Console.ReadLine());

            foreach (var item in items)
            {
                Console.Write($"{addCollection.Add(item)} ");
            }

            Console.WriteLine();

            foreach (var item in items)
            {
                Console.Write($"{removeCollection.Add(item)} ");
            }

            Console.WriteLine();

            foreach (var item in items)
            {
                Console.Write($"{myList.Add(item)} ");
            }

            Console.WriteLine();

            for (int i = 0; i < removeOperations; i++)
            {
                Console.Write($"{removeCollection.Remove()} ");
            }

            Console.WriteLine();

            for (int i = 0; i < removeOperations; i++)
            {
                Console.Write($"{myList.Remove()} ");
            }
        }
    }
}
