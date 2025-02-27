using System.Drawing.Text;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace EprinAppClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new ClientForm());
        }
    }
}