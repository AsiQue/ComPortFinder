using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComFinder
{
    public static class AppConfiguration
    {
        public static void loadDefualtConfiguration(out string puttyPath, out string puttyConf)
        {
            Properties.Settings.Default.Reload();
            puttyPath = Properties.Settings.Default.puttyPath;
            puttyConf = Properties.Settings.Default.puttyConf;
        }

        public static void saveConfiguration(string puttyPath, string puttyConf)
        {
            Properties.Settings.Default.puttyPath = puttyPath;
            Properties.Settings.Default.puttyConf = puttyConf;
            Properties.Settings.Default.Save();

        }
    }
}
