<!--
From: http://svn.martinp23.com/npw/

Copyright 2007 Martin Peeks
Copyright 2007 Reedy_boy
Copyright 2007 Martijn Hoekstra  

This file is part of NPWatcher.

NPWatcher is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

NPWatcher is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with NPWatcher.  If not, see <http://www.gnu.org/licenses/>.

-->
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