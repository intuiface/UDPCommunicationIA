// ****************************************************************************
// <copyright file="UDPCommunicationIA.cs" company="Intuilab SAS">
// INTUILAB SAS
//_____________________
// [2002] - [2020] Intuilab SAS
// All Rights Reserved.
// NOTICE: All information contained herein is, and remains
// the property of Intuilab SAS.
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UDPCommunication.Events;

namespace UDPCommunication
{
    public class UDPCommunicationIA : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged


        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region Attributes


        private string m_strLastMessage = "";
        private string m_strLogs = "";
        private bool m_bLoop = true;
        private UdpClient m_udpReceiverClient;

        #endregion

        #region Properties


        public string LastMessage
        {
            get { return m_strLastMessage; }
            set
            {
                if (m_strLastMessage != value)
                {
                    m_strLastMessage = value;
                    NotifyPropertyChanged("LastMessage");
                }
            }
        }

        public string Logs
        {
            get { return m_strLogs; }
            set
            {
                if (m_strLogs != value)
                {
                    m_strLogs = value;
                    NotifyPropertyChanged("Logs");
                }
            }
        }

        #endregion

        #region Events

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler MessageReceived;


        private void RaiseMessageReceived(string msg_)
        {
            if (MessageReceived != null)
                MessageReceived(this, new MessageEventArgs(msg_));
        }


        #endregion

        #region Constructor

        public UDPCommunicationIA()
        {
            //do nothing. Use the "StartListening" method to specify port. 
        }

        public void Dispose()
        {
            m_bLoop = false;
            if (m_udpReceiverClient != null)
                m_udpReceiverClient.Close();

        }

        #endregion

        #region public methods

        public void StartListening(int port_)
        {
            _updatePort(port_);
        }

        public void SendMessage(string address_, int port_, string message_)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(message_);
            _send(address_, port_, sendBuffer);        
        }

        public void SendHexMessage(string address_, int port_, string message_)
        {
            byte[] sendBuffer = HexString2Bytes(message_);
            _send(address_, port_, sendBuffer);          
        }
     
        #endregion

        #region private methods

        private void _send(string address_, int port_, byte[] buffer_)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serverAddr = IPAddress.Parse(address_);
                IPEndPoint endPoint = new IPEndPoint(serverAddr, port_);                
                sock.SendTo(buffer_, endPoint);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: " + exception.Message);
            }
        }

        private void _updatePort(int port_)
        {            
            Task.Run(async () =>
            {
                m_udpReceiverClient = new UdpClient(port_);

                try
                {
                    m_bLoop = true;
                    while (m_bLoop)
                    {
                        //IPEndPoint object will allow us to read datagrams sent from any source.
                        var receivedResults = await m_udpReceiverClient.ReceiveAsync();
                        var message = Encoding.ASCII.GetString(receivedResults.Buffer);

                        LastMessage = message;
                        Logs += message + "\n";
                        RaiseMessageReceived(message);                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("UDP Client exception: " + ex.Message);
                }
            });
        }

        private byte[] HexString2Bytes(string hexString)
        {
            //check for null
            if (hexString == null) return null;

            //check for blank spaces
            hexString = hexString.Replace(" ", String.Empty);            

            //get length
            int len = hexString.Length;
            if (len % 2 == 1) return null;
            int len_half = len / 2;
            //create a byte array
            byte[] bs = new byte[len_half];
            try
            {
                //convert the hexstring to bytes
                for (int i = 0; i != len_half; i++)
                {
                    bs[i] = (byte)Int32.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
            //return the byte array
            return bs;
        }

        #endregion
    }
}
