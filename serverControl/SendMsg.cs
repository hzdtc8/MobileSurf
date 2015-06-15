using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace serverControl
{
    public class SendMsg
    {
        public string translateMessage(serverControl.MsgFormate[] msg)
        {
            string allMsg="";
            for (int i = 0; i < msg.Length; i++)
            {
                allMsg += msg[i].messageText;
            }
            return allMsg;
        }
        

        public void SendMsgToMobile(Socket sendMsgSocket, string msg)
        {
            byte[] bytes = new byte[4028];
            bytes = Encoding.ASCII.GetBytes(msg);
            sendMsgSocket.Send(bytes);
        }
       
    }


    }
