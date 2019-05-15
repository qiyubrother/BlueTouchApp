using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueTouchApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            CBlueToothManage btm = new CBlueToothManage();
            do
            {
                Console.WriteLine("Searching...");
                var lst = btm.Search();
                foreach (var item in lst)
                {
                    Console.WriteLine($"{item.blueName}, {item.blueAddress}");
                }
                Console.WriteLine("Done. Press enter key to research, other key to exit.");
                var key = Console.ReadKey();
                if (key.KeyChar != 0x0d)
                {
                    break;
                }
            } while (true);
        }
    }
}
