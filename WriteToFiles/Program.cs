using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WriteToFiles
{
    class Program
    {
        static readonly object _locker = new object();
        static void Main(string[] args)
        {
            Console.WriteLine($"Main thread id: {Thread.CurrentThread.GetHashCode()}");
            Thread thread1 = new Thread(ManageFile1);
            thread1.Start();

            Thread thread2 = new Thread(ManageFile2);
            thread2.Start();

            Console.ReadKey();
        }

        private static void ManageFile1()
        {
            Console.WriteLine($"Current thread id: {Thread.CurrentThread.ManagedThreadId}");

            StreamReader streamReader = File.OpenText("File1.txt");
            string text = streamReader.ReadToEnd();
            Console.WriteLine("File1 reading: " + text);

            lock (_locker)
            {
                StreamWriter streamWriter = new StreamWriter("File3.txt", true);
                Console.WriteLine("File3 writing: " + text);
                streamWriter.WriteLine(text);
                streamWriter.Close();
            }
        }

        private static void ManageFile2()
        {
            Console.WriteLine($"Current thread id: {Thread.CurrentThread.ManagedThreadId}");

            StreamReader streamReader = File.OpenText("File2.txt");
            string text = streamReader.ReadToEnd();
            Console.WriteLine("File2 reading: " + text);

            lock (_locker)
            {
                StreamWriter streamWriter = new StreamWriter("File3.txt", true);
                Console.WriteLine("File3 writing: " + text);
                streamWriter.WriteLine(text);
                streamWriter.Close();
            }
        }
    }
}