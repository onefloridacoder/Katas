using System.Threading;

namespace ActionsConsole
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {                       
            Action<List<string>> handleStuff = ProcessList;

            var myLetterList = new List<string> {"A", "B", "C", "D"};

            var myNumberList = new List<string> { "1", "2", "3", "4" };

            var myLowerCaseLetterList = new List<string> { "d", "e", "f", "g" };

            handleStuff(myLetterList);


            // Delegate Option
            var consoleConsumer = new Consumer<string>(Console.WriteLine, myLetterList.ToArray());
            var lengthConsumer = new Consumer<string>(item => Console.WriteLine(item.Length), myNumberList.ToArray());
            var upperCaseConsumer = new Consumer<string>(item => Console.WriteLine(item.ToUpper()), myLowerCaseLetterList.ToArray());

            BuildUpBlockCollectionWithTask();

            Console.ReadLine();
        }

        static void ProcessList(IEnumerable<string> collection)
        {
            Console.WriteLine(collection.Count());
        }
        
        private static void BuildUpBlockCollectionWithTask()
        {
            var bc = new BlockingCollection<Person>();

            Task task1 = Task.Factory.StartNew(() =>
                                                   {
                                                       bc.Add(new Person{ FirstName = "Bill", LastName = "Gates"});
                                                       bc.Add(new Person { FirstName = "Jaco", LastName = "Pastorious" });
                                                       bc.Add(new Person { FirstName = "Hello", LastName = "World" });
                                                       bc.CompleteAdding();
                                                   });

            Task task2 = Task.Factory.StartNew(() =>
                                                   {
                                                       try
                                                       {
                                                           while (true)
                                                           {
                                                               var p = bc.Take();
                                                               Console.WriteLine("{0}:{1}", p.FirstName, p.LastName);
                                                           }
                                                       }
                                                       catch (InvalidOperationException)
                                                       {
                                                           Console.WriteLine("Done!");
                                                       }
                                                   });

            Task.WaitAll(task1, task2);
        }
    }

    /// <summary>
    /// Using a Generic sealed class instead of using an abstract class
    /// that uses methods that will be overridden.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Consumer<T>
    {
        private BlockingCollection<T> collection = new BlockingCollection<T>();
        private Action<T> consumeItem;
        private T[] items;

        public Consumer(Action<T> consumeItem, T[] array)
        {
            if (consumeItem == null)
            {
                throw new ArgumentNullException("consumeItem");
            }
            this.consumeItem = consumeItem;
            this.items = array;
            
            this.ConsuptionLoop();
        }   

        private void ConsuptionLoop()
        {
            items.ToList().ForEach(i =>
            {
                if (i != null)
                {
                    collection.Add(i);
                }
            });

            // If nothing signals the collection we stopped adding items, the loop is infinite.
            collection.CompleteAdding();
            
            while (! collection.IsCompleted)
            {
                T item;
                if (collection.TryTake(out item, TimeSpan.FromSeconds(1.0)))
                {
                    consumeItem(item);
                }
            }            
        }
    }

    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
