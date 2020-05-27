using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPCommunication;

namespace TestUDPCommunicationIA
{
    class Program
    {
        static void Main(string[] args)
        {
            UDPCommunicationIA udpia = new UDPCommunicationIA();

            udpia.MessageReceived += Udpia_MessageReceived;

            udpia.StartListening(8002);
            Console.WriteLine("Listening on port 8002");

            Console.WriteLine("Type 'p' to listen on port 8003, 's' to send an ASCII message on port 8005, 'h' to send an Hex message to port 8005");

            var c = Console.ReadKey().KeyChar;
            while (c != 'q')
            {
                //test change port
                if (c == 'p')
                {
                    udpia.StartListening(8003);
                    Console.WriteLine("UPDATE: Listening on port 8003");
                }
                else if (c == 's')
                {
                    udpia.SendMessage("127.0.0.1", 8005, "Hello World!");
                }
                else if (c == 'h')
                {
                    udpia.SendHexMessage("127.0.0.1", 8005, "48 65 6c 6c 6f 20 57 6f 72 6c 64 20 69 6e 20 48 65 78 21");                    
                }
                c = Console.ReadKey().KeyChar;
            }
        }

        private static void Udpia_MessageReceived(object sender, UDPCommunication.Events.MessageEventArgs e)
        {
            Console.WriteLine("Message received: " + e.Message);
        }
    }
}
