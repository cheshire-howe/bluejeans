using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Services.Local
{
    public static class Log
    {
        public static void ToFile(string msg)
        {
            using (StreamWriter sw = File.AppendText("C:\\3PT\\BluejeansLMSConnector\\Bluejeans LMS Connector\\Main\\Code\\BJN\\log.txt"))
            {
                sw.WriteLine("*************************************************");
                string logLine = String.Format(
                    "{0:G}: {1}.", DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
        }
    }
}
