using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NPWatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            
        }

        //internal static void quit()
        //{
        //    Application.ExitThread();
        //    Application.Exit();
        //}
        
    }
}