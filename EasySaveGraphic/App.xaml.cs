using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace EasySaveGraphic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            // Check if EasySave process is already executed
            Process[] appByName = Process.GetProcessesByName("EasySaveGraphic");
            if(appByName.Length > 1) // if there are more than 1 process (including this one)
            {
                System.Windows.Application.Current.Shutdown(); // shutdown before anything
            }
        }
    }
}
