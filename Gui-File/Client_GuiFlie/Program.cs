using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client_GuiFlie
{
    class Program
    {
        public static void CreateFIle(string pathfile, string data)
        {
            //E:\hoc_tap\Nam_3_HK2\Lap_trinh_mang\Server\text1.txt
            string path = pathfile;
            //string path = @"''";
            if (!File.Exists(path))
            {

                try
                // Create a file to write to.
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(data);
                    }
                    Console.WriteLine("Thanh cong");
                }
                catch
                {
                    Console.WriteLine("Loi duong dan: ");
                }
            }
            else
            {
                Console.WriteLine("file da ton tai!");
            }       
        }
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Loopback, 1234);

            client.Connect(iPEndPoint);

            string nhan = "";
            byte[] snhan = new byte[1024];
            int rec = client.Receive(snhan);
            nhan = Encoding.ASCII.GetString(snhan, 0, rec);
            Console.WriteLine(nhan);

            string s;

            Console.Write("Client: ");
            s = Console.ReadLine();
            byte[] sgui = new byte[1024];
            sgui = Encoding.ASCII.GetBytes(s);
            client.Send(sgui);

            if (s == "fileText.txt")
            {
                //nhập đường đẫn
                Console.Write("Nhap duong dan: ");
                string sf = Console.ReadLine();
                sf = sf + "\\" + s;
                CreateFIle(sf, nhan);
            }

        }
    }
}
