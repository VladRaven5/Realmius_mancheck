using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realmius;
using Xamarin.Forms.PlatformConfiguration;

namespace Realmius_mancheck
{
    public class Logger : ILogger
    {
        public void Exception(Exception ex, string text = null)
        {
            System.Diagnostics.Debug.WriteLine("Exception occured: {0}. {1}",ex, text);
        }

        public void Info(string text)
        {
            System.Diagnostics.Debug.WriteLine("Info: {0}.",text);
        }

        public void Debug(string text)
        {
            System.Diagnostics.Debug.WriteLine("Debug: {0}.", text);
        }
    }
}
