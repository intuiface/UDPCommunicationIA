// ****************************************************************************
// <copyright file="MessageEventArgs.cs" company="Intuilab SAS">
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommunication.Events
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageEventArgs(string msg_)
        {
            Message = msg_;
        }
    }
}
