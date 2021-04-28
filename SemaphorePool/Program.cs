using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphorePool
{
    class Program
    {
        static SemaphoreSlim pool = new SemaphoreSlim(4);
        static StringBuilder stringBuilder = new StringBuilder();
        static readonly object _locker = new object();

        static void Enter(object id)
        {
            pool.Wait();
            Show((int)id, "in");
            Thread.Sleep(2000);
            Show((int)id, "out");
            pool.Release();
            WriteToLog();
        }

        private static void Show(int id, string txt)
        {
            string text = $"{DateTime.Now}: {id} is {txt}!";
            stringBuilder.AppendLine(text);
            Console.WriteLine(text);
        }

        private static void WriteToLog()
        {
            lock (_locker)
            {
                using (StreamWriter streamWriter = new StreamWriter("Log.txt", true))
                {
                    streamWriter.Write(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }
        }

        static void ClearLogFile()
        {
            FileStream fileStream = new FileStream("Log.txt", FileMode.Create);
            fileStream.Close();
        }

        static void Main(string[] args)
        {
            Thread thread = new Thread(ClearLogFile);
            thread.Start();
            thread.Join();

            for (int i = 1; i <= 8; i++)
            {
                new Thread(Enter).Start(i);
            }
            Console.ReadKey();
        }
    }
}
