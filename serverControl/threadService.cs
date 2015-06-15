using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace serverControl
{
   public class threadService
    {

        //public Socket clientCommunication;
        public string Currentmode;
        public Socket Command_Listening_Socket; //used to listen for connections for exchanging commands between server and mobile
        public Socket Interaction_Listening_ReceivingMsgFromMobile;// designed for receiving information from mobile
        public Socket Interaction_Listening_SendingImage;// designed for sending information to mobile
        public Socket Interaction_Listening_SendingPoint;// designed for the sending point formate
        public Socket Interaction_Listening_SendingMsg;// designed for the sending the Message to the mobile

        public Socket Interaction_ReceivingMsgFromMobile_Socket;// receiving
        public Socket Interaction_SendingImage_Socket;// sending
        public Socket Interaction_SendingPoint_Socket;// sending point formate "x="+x+" "+"y="+y
        public Socket Interaction_SendingMsg_Socket;// sending Msg to the mobile
        public static int connections = 0;
        int i = 0;
        User user;
        public CommunicationThread communication; 
        //default constructor
        public threadService()
        {

        }
        public threadService(User my_User)
        {
            Command_Listening_Socket = my_User.command_Communication_Socket;
            Interaction_Listening_ReceivingMsgFromMobile = my_User.interation_Receiving_Socket;
            Interaction_Listening_SendingImage = my_User.interation_Sending_Socket;
            Interaction_Listening_SendingPoint = my_User.interaction_SendingPoint_Socekt;
            Interaction_Listening_SendingMsg = my_User.interaction_SendMsg_Socekt;

            user = my_User;
        }

        public void userCommand(User user)
        {
            string data = null;
            byte[] bytes = new byte[1024];

            if (user.command_Communication_Socket != null)
            {
                connections++;
            }
            Debug.WriteLine("new client connects, {0} connections", connections);

            //using client which is command socket to receiveneww the command from the mobile. this thread is infinite.

            while (true)
            {
                i = user.command_Communication_Socket.Receive(bytes);
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Currentmode = data;
                Debug.WriteLine("receive status from client: " + Currentmode);

                // if the current mode send from mobile is equal to crossDevice,
                // we open three socket related crossDevice mode
                // Change the user's status to the current mode
                // start two threads 
                if (Currentmode == "crossDevice")
                {

                    user.interation_Receiving_Socket = user.interation_Receiving_Socket.Accept();//receiving the message about surfaceObjectSelected from the mobile
                    user.interation_Sending_Socket = user.interation_Sending_Socket.Accept();// send image size and image to the mobile
                    user.interaction_SendingPoint_Socekt = user.interaction_SendingPoint_Socekt.Accept();// send the coordinate of tabletop to mobile
                    user.interaction_SendMsg_Socekt = user.interaction_SendMsg_Socekt.Accept();// send the speech message to the mobile

                    user.MyMode = Mode.crossDevice;
                   /* user.interation_Receiving_Socket = Interaction_ReceivingMsgFromMobile_Socket;
                    user.interation_Sending_Socket = Interaction_SendingImage_Socket;
                    user.interaction_SendingPoint_Socekt = Interaction_SendingPoint_Socket;
                    user.interaction_SendMsg_Socekt = Interaction_SendingMsg_Socket;*/
                    //CommunicationThread communication = new CommunicationThread(user);
                    Thread newReceiveThread = new Thread(
                         () =>
                         {
                             communication.receiveThread(user);

                         }

                         );
                    newReceiveThread.Start();

                }
                else if (Currentmode == "mobileMobile")
                {
                    user.MyMode = Mode.MobileMobile;
                }
                else if (Currentmode == "MobileTabletop")
                {
                    user.MyMode = Mode.MobileTabletop;
                }
                else if (Currentmode == "terminate")
                {
                    user.MyMode = Mode.terminate;
                    //terminate all the socket
                }//In what condition, we need terminate?

            }
        }

    }
}
