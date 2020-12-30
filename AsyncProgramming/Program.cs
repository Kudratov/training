using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    class Program
    {
        internal static long counter = 0;
        internal static object syncObject = new object();
        static void Zb() 
        {
            if (counter <= 25) 
            {
                counter++;

                Console.WriteLine($"Value: {counter}. Thread #{Thread.CurrentThread.ManagedThreadId}.");

                Thread.Sleep(100);

                var thread = new Thread(Zb);

                thread.Start();

                thread.Join();

                Console.WriteLine($"Thread #{Thread.CurrentThread.ManagedThreadId} finished.");
            }
        }

        static void Zb1() 
        {
            for (int i = 1, length = 1000; i <= length; i++)
            {
                // 
                // T2, T3, T3, T5
                // T1
                 // Monitor.Enter(syncObject);
                 lock (syncObject) 
                 {
                    counter++;
                 }
                 // Monitor.Exit(syncObject);
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var threads = new Thread[7];

            for (int i = 0, length = threads.Length; i < length; i++)
            {
                threads[i] = new Thread(Zb1);
                threads[i].Start();
            }

            for (int i = 0, length = threads.Length; i < length; i++)
            {
                threads[i].Join();
            }

            Console.ResetColor();

            Console.WriteLine($"Expected value: 700.");
            // 700
            Console.WriteLine($"Actual value: {counter}.");
            Console.WriteLine("Primary thread is done!.");
            Console.ReadKey();
        }
    }
}
