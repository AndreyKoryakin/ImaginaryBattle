using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy
{
    class Logger
    {
        private static string Path = @"log.txt";
        public static bool File = true;

        public static void Clear()
        {
            using (StreamWriter sw = new StreamWriter(Path, false, Encoding.Default))
            {
            }
        }

        public static void Log(string text)
        {
            if (File)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(Path, true, Encoding.Default))
                    {
                        sw.WriteLine(text);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}
