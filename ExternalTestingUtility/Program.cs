using ML2.UI.Core.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML2
{
    static class Program
    {
#if DEBUG
        private const bool NoErrorHandling = false;
#endif
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Debugger.IsAttached
#if DEBUG
                && !NoErrorHandling
#endif
                )

            {
                Application.ThreadException += new ThreadExceptionEventHandler(HandleUIThreadExceptions);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleCurrentDomainExceptions);
            }
            Application.Run(new MainForm());
        }

        private static void HandleUIThreadExceptions(object sender, ThreadExceptionEventArgs args)
        {
            try
            {
#if DEBUG
                new CErrorDialog("Mod Launcher Error", args.Exception.ToString()).ShowDialog();
#else
                new CErrorDialog("Mod Launcher Error", args.Exception.Message).ShowDialog();
#endif
                //if (args.Exception.Message.Contains("[AUTH]"))
                //{
                //    Application.Exit();
                //}
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal internal exception...");
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        private static void HandleCurrentDomainExceptions(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                new CErrorDialog("Mod Launcher Error", ((Exception)args.ExceptionObject).Message).ShowDialog();
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal internal exception...");
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
    }
}
