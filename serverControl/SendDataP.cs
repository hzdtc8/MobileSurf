using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;

namespace serverControl
{
    class SendDataP
    {
        public Socket Interaction_SendingPoint_Socket;
        byte[] outbytesP;
        public SendDataP(Socket Interaction_SendingPoint_Socket)
        {
            this.Interaction_SendingPoint_Socket = Interaction_SendingPoint_Socket;
            Debug.WriteLine("in the sendDataP");
        }

        public void sendDataP(double x, double y)
        {
            string X = Convert.ToString(x);
            string Y = Convert.ToString(y);

            outbytesP = Encoding.ASCII.GetBytes("X=" + X + " " + "Y=" + Y + " ");
            string inbyte = Encoding.ASCII.GetString(outbytesP);
            Debug.WriteLine("Point.length:" + outbytesP.Length);
            Interaction_SendingPoint_Socket.Send(outbytesP);

        }

    }
}
