using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjBTLDemo.NET.Forms;
using prjBTLDemo.NET.ViewModels;
using prjBTLDemo.NET.Views;

namespace prjBTLDemo.NET
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormHoaDon());
        }
    }
}
