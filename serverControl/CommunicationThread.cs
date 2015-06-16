using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace serverControl
{
    public class CommunicationThread
    {
        public Socket Interaction_ReceivingMsgFromMobile_Socket, Interaction_SendingImage_Socket, Interaction_SendingPoint_Socket, Interaction_SendMsg_Socket;
        User user;
        double x, y;
        public delegate void myObjectClickedEventHandler(object sender, objectClickedEventArgs e);
        //public delegate void myReturnValueEventHandler(object sender, returnValueEventArgs e);
        //public delegate void myReturnGestureEventHandler(object sender, returnGestureEventArgs e);
        public event myObjectClickedEventHandler objectClicked;
        //public event myReturnValueEventHandler returnValue;
        //public event myReturnGestureEventHandler returnGesture;


        //Defaut constructor

        public CommunicationThread()
        {

        }

        public CommunicationThread(User my_User)
        {
            Interaction_ReceivingMsgFromMobile_Socket = my_User.interation_Receiving_Socket;
            Interaction_SendingImage_Socket = my_User.interation_Sending_Socket;
            Interaction_SendingPoint_Socket = my_User.interaction_SendingPoint_Socekt;
            Interaction_SendMsg_Socket = my_User.interaction_SendMsg_Socekt;

            user = my_User;
        }
 

        public CommunicationThread(Socket Interaction_ReceivingMsgFromMobile_Socket, Socket Interaction_SendingImage_Socket,Socket Interaction_SendingPoint_Socket,Socket Interaction_SendMsg_Socket)
        {
            this.Interaction_ReceivingMsgFromMobile_Socket = Interaction_ReceivingMsgFromMobile_Socket;
            this.Interaction_SendingImage_Socket = Interaction_SendingImage_Socket;
            this.Interaction_SendingPoint_Socket = Interaction_SendingPoint_Socket;
            this.Interaction_SendMsg_Socket = Interaction_SendMsg_Socket;
   
        }


        //// message back

        public static void sendMsg(Socket Interaction_SendMsg_Socket, String Msg)
        {

            byte[] buffer = new byte[1024];

            buffer = Encoding.ASCII.GetBytes(Msg);

            Interaction_SendMsg_Socket.Send(buffer);

        }
        public string[] trim(string msg)
        {

            string[] Msg = msg.Split(';');

            return Msg;

        }
        public string[] trimSpace(string msg)
        {

            string[] Msg = msg.Split(' ');

            return Msg;

        }

        //used for showing the tooltips by receiving the selection of mobileUse

        //{objectClicked, Confirmation, PersonalizedGesture}
        //Formate: header+" "+TagID+" "+content
        public void receiveThread(User user)
        {
            int i = 0;
            string data = null;
            Debug.WriteLine("In the receive method");
            byte[] bytes = new byte[1024];

            while ((i = user.interation_Receiving_Socket.Receive(bytes)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Debug.WriteLine("object select from the userID {0}: {1}", user.UserID, data);
                string[] msgArray = new string[3];
                msgArray=trim(data);
                if (msgArray[0] == "objectClick")
                {
                    objectClickedEventArgs e = new objectClickedEventArgs();
                    //pass the tagID,Coordinate,last point, user into event args
                    e.TagID = Convert.ToInt32( msgArray[1]);
                    e.Coordinate = msgArray[3];
                    e.CoordinateOfTabletopPointX = user.pointX;
                    e.CoordinateOfTabletopPointY = user.pointY;
                    e.user = user;
                    if(objectClicked!=null)

                    objectClicked(this, e);

                   // user.surfaceObjectSelected = true;
                   
                }
                if (msgArray[0] == "returnValue")
                {
                    returnValueEventArgs e = new returnValueEventArgs();
                    //returnValue(this, e);
                }
                if (msgArray[0] == "returnGesture")
                {
                    returnGestureEventArgs e = new returnGestureEventArgs();
                   // returnGesture(this, e);
                }


            }

        }



    }
}
