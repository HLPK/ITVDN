using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexApp
{
    class Program
    {
        static Mutex mutex = new Mutex(false,"MyMutex");
        static void Main(string[] args)
        {
            if (! mutex.WaitOne(TimeSpan.FromSeconds(3),false))
            {
                Console.WriteLine("There is another application's item is running!");
                Thread.Sleep(4000);
                return;
            }
            Run();
        }

        private static void Run()
        {
            Console.WriteLine("Application is running!");
            Console.ReadKey();
        }
    }
}
