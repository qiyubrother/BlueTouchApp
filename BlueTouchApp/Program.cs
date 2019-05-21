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
            //CBlueToothManage btm = new CBlueToothManage();
            //do
            //{
            //    Console.WriteLine("Searching...");
            //    var lst = btm.Search();
            //    foreach (var item in lst)
            //    {
            //        Console.WriteLine($"{item.blueName}, {item.blueAddress}");
            //    }
            //    Console.WriteLine("Done. Press enter key to research, other key to exit.");
            //    var key = Console.ReadKey();
            //    if (key.KeyChar != 0x0d)
            //    {
            //        break;
            //    }
            //} while (true);
            //Console.WriteLine("Press any send message to device.");
            //Console.ReadKey();

            var sample = new BluetoothUserSample();
            // 连接到蓝牙设备
            var bluetoothAddress = "14:41:13:07:1C:40"; //"BT04-A"
            try
            {
                var btm = sample.ConnectToBluetooth(bluetoothAddress, string.Empty);
                // 打开蓝牙设备
                sample.SendMessage(btm, new byte[] { 0xa0, 0x01, 0x01, 0xa2 }, 5);
                // 停顿1秒钟
                System.Threading.Thread.Sleep(3000);
                // 关闭蓝牙设备
                sample.SendMessage(btm, new byte[] { 0xa0, 0x01, 0x00, 0xa1 }, 5);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //sample.SendMessageToBluetooth("BT04-A", string.Empty, new byte[] { 0xa0,0x01,0x01,0xa2});
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
