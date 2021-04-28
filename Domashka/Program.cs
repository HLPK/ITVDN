using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domashka
{
    class Program
    {
        static int count=0;
        static void Procedure()
        {
            Console.WriteLine($"Current thread id: {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 10; i++)
            {
                count++;
                Console.Write(count.ToString()+" ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"Main thread id: {Thread.CurrentThread.GetHashCode()}");
            Procedure();
            
            Thread thread2 = new Thread(Procedure);
            thread2.Start();
            thread2.Join();

            Thread thread3 = new Thread(Procedure);
            thread3.Start();
            thread3.Join();

            Console.ReadKey(); 
            //Задание сделано правильно!
        }
    }
}
