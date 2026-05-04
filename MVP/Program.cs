using MVC.Model;
using MVP.Presenter;
using MVP.View;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MVP
{
    internal static class Program
    {
        /// <summary>
        /// Импорт функции AllocConsole для выделения консоли в GUI-приложении.
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var model = new InternetShopCollection();
            if (args.Length > 0 && args[0].ToLower() == "console")
            {
                AllocConsole();
                var view = new ConsoleView();
                var presenter = new InternetShopPresenter(model, view);
                view.Run();
            }
            else
            {
                var view = new Form1();
                var presenter = new InternetShopPresenter(model, view);
                Application.Run(view);
            }
        }
    }
}