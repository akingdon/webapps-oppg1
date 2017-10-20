using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppsOppgave1.DAL
{
    public class Logger
    {
        private const string EVENT_SEPARATOR = "------------------------------";

        public static void Write(String msg, TextWriter w)
        {
            w.Write(Environment.NewLine + EVENT_SEPARATOR + Environment.NewLine +
                "{0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString() + ":" + Environment.NewLine);
            w.Write(msg);
            w.Write(Environment.NewLine);
        }
    }
}
