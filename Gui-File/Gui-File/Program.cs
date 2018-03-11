using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Gui_File
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Loopback, 1234);
            server.Bind(iPEndPoint);

            server.Listen(10);

            Socket client = server.Accept();

            byte[] nhan = new byte[1024];
            int rec = client.Receive(nhan);
            string snhan = Encoding.ASCII.GetString(nhan, 0, rec);

            if(snhan == "fileText.txt")
            {
                FileStream fs = new FileStream("fileText.txt", FileMode.Open);
                StreamReader rd = new StreamReader(fs, Encoding.UTF8);
                String giatri = rd.ReadToEnd();// ReadLine() chỉ đọc 1 dòng đầu thoy, ReadToEnd là đọc hết
                
                byte[] gui = Encoding.ASCII.GetBytes(giatri);
                client.Send(gui);

                rd.Close();
            }
        }
    }
}
