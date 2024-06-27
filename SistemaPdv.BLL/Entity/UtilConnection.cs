using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Entity
{
    public class UtilConnection
    {
        public static string getStrConnection()
        {
            string assemblyPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(assemblyPath);
            string result = cfg.AppSettings.Settings["sqlConnection"].Value;

            return result;
        }

    }
}
