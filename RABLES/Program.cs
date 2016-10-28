using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RABLES
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
            game BJGame = new game();

            Console.WriteLine("Drawing card:");
            //BJGame.drawCard().to;

            Application.Run(new Form1());


        }
    }
}
