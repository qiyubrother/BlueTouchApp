using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTouchApp
{
    public class CBluetooth
    {
        public string blueName { get; set; }                 //蓝牙名字
        public BluetoothAddress blueAddress { get; set; }    //蓝牙的唯一标识符
        public ClassOfDevice blueClassOfDevice { get; set; } //蓝牙是何种类型
        public bool IsBlueAuth { get; set; }                 //指定设备通过验证
        public bool IsBlueRemembered { get; set; }           //记住设备
        public DateTime blueLastSeen { get; set; }
        public DateTime blueLastUsed { get; set; }
    }

    public class CBlueToothManage
    {
        BluetoothClient client = new BluetoothClient();

        public List<CBluetooth> Search()
        {
            List<CBluetooth> bluetoothList = new List<CBluetooth>();  //搜索到的蓝牙的集合
            BluetoothClient client = new BluetoothClient();
            BluetoothRadio radio = BluetoothRadio.PrimaryRadio;       //获取蓝牙适配器
            radio.Mode = RadioMode.Connectable;
            BluetoothDeviceInfo[] devices = client.DiscoverDevices(); //搜索蓝牙  10秒钟
            foreach (var item in devices)
            {
                bluetoothList.Add(new CBluetooth { blueName = item.DeviceName, blueAddress = item.DeviceAddress, blueClassOfDevice = item.ClassOfDevice, IsBlueAuth = item.Authenticated, IsBlueRemembered = item.Remembered, blueLastSeen = item.LastSeen, blueLastUsed = item.LastUsed });//把搜索到的蓝牙添加到集合中
            }

            return bluetoothList;
        }

        public bool ConnectTo(List<CBluetooth> blueTouchList, string blueName, string pwd)
        {
            var deviceAddress = blueTouchList.FirstOrDefault(x => x.blueName == blueName);
            if (deviceAddress != null)
            {
                try
                {
                    client.SetPin(deviceAddress.blueAddress, pwd);
                    client.Connect(deviceAddress.blueAddress, BluetoothService.Handsfree);
                }
                catch
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        public bool SendMessage(byte[] msg)
        {
            try
            {
                var s = client.GetStream();
                s.Write(msg, 0, msg.Length);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    public class BluetoothUserSample
    {
        public void SendMessageToBluetooth(string blueName, string pwd, byte[] msg)
        {
            var btm = new CBlueToothManage();
            var lst = btm.Search();
            if (lst == null || lst.Count() == 0)
            {
                Console.WriteLine($"指定设备不存在。");
                return;
            }
            if (!btm.ConnectTo(lst, blueName, pwd))
            {
                Console.WriteLine($"连接失败。指定设备不存在，或密码错误。");
            }
            if (!btm.SendMessage(msg))
            {
                Console.WriteLine($"向蓝牙设备发送指令失败。");
            }
        }
    }
}
