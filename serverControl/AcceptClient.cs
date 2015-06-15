using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Windows.Threading;

namespace serverControl
{

    public class AcceptClient
    {
        public Socket server;
        public Socket Command_Listening_Socket; //used to listen for connections for exchanging commands between server and mobile
        public Socket Interaction_Listening_ReceivingMsgFromMobile;// designed for receiving information from mobile
        public Socket Interaction_Listening_SendingImage;// designed for sending information to mobile
        public Socket Interaction_Listening_SendingPoint;// designed for the sending point formate
        public Socket Interaction_Listening_SendingMsg;// designed for the sending the Message to the mobile

        public IPEndPoint iepCommand, iepReceivingMsgFromMobile, iepSendingImage, iepSendingPoint, iepSendMsg; // Four ip end point
        public User my_user;        
        Mode currentMode;
        public threadService command;
        int i = 0;

        private List<User> userList = new List<User>();
        public List<User> UserList
        {
            get { return userList; }
            set { userList = value; }
        }

        public List<User> returnUserList()
        {
            return UserList;
        }


        public AcceptClient(IPEndPoint iepCommand, IPEndPoint iepReceivingMsgFromMobile, IPEndPoint iepSendingImage, IPEndPoint iepSendingPoint, IPEndPoint iepSendMsg)
        {
            // sign each ip end point number
            this.iepCommand = iepCommand;
            this.iepReceivingMsgFromMobile = iepReceivingMsgFromMobile;
            this.iepSendingImage = iepSendingImage;
            this.iepSendingPoint = iepSendingPoint;
            this.iepSendMsg = iepSendMsg;
            UserList = new List<User>();
        }
        //default constructor
        public AcceptClient()
        {

        }



        // this method used to accept the new client
        public List<User> accept(IPEndPoint iepCommand, IPEndPoint iepReceivingMsgFromMobile, IPEndPoint iepSendingImage, IPEndPoint iepSendingPoint, IPEndPoint iepSendMsg)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Interaction_Listening_ReceivingMsgFromMobile = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Interaction_Listening_SendingImage = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Interaction_Listening_SendingPoint = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Interaction_Listening_SendingMsg = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(iepCommand);
            server.Listen(20);

            Interaction_Listening_SendingImage.Bind(iepSendingImage);
            Interaction_Listening_SendingImage.Listen(20);

            Interaction_Listening_ReceivingMsgFromMobile.Bind(iepReceivingMsgFromMobile);
            Interaction_Listening_ReceivingMsgFromMobile.Listen(20);

            Interaction_Listening_SendingPoint.Bind(iepSendingPoint);
            Interaction_Listening_SendingPoint.Listen(20);

            Interaction_Listening_SendingMsg.Bind(iepSendMsg);
            Interaction_Listening_SendingMsg.Listen(20);

            while (true)
            {


                // Here is three blocks for accepting the new client, 
                // once client connect to the command listening socket, 
                // pass the command listening socket to the threadService class
                // and start a thread for receiving the userID

                byte[] msgFromMobile = new byte[1024];// using to store  the msg from mobile,Formate"TagID"+" "+"Mode"
                string[] msgArray = new string[2];// string array to store the TagID and mode after analyse
                Debug.Write("stop before the accept");
                Command_Listening_Socket = server.Accept();
                Debug.Write("Stop after the accept");
                int msgLenght = Command_Listening_Socket.Receive(msgFromMobile);// receive the byte array from mobile, and store into msgFormMobile
                string msg = System.Text.Encoding.ASCII.GetString(msgFromMobile, 0, msgLenght);// convert into string type
                msgArray = trim(msg);//msg formate should be "tagID"+" "+"Mode",
                int Tag = Convert.ToInt32(msgArray[0]);// return the tagID in integer 
                string mode = msgArray[1];// return the mode in string
                Debug.WriteLine("mode:" + mode);
                Debug.WriteLine("tagID:" + Tag);


                // If mode is equal to "setup" under condition TagID is unique,
                // we intialize new user, add this into list
                // If TagID is not unique, we believe that same user only change the mode;
                // we only change the mode for that user
                if (mode == "setup")
                {
                    currentMode = Mode.setup;
                    // when UserList is more than one user
                    if (UserList.Count > 0)
                    {
                        // travel all the member of list
                        // in order to compare with tagID 
                        bool found = false;
                        for ( i = 0; i < UserList.Count; i++)
                        {
                            //If cannot find the TagID which is send from mobile in the list
                            //that one is new user, therefore, we add it into UserList
                            //start a command Thread to the further action
                            if (UserList[i].UserID == Tag)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            my_user = new User(Tag, false, false, Command_Listening_Socket, Interaction_Listening_ReceivingMsgFromMobile, Interaction_Listening_SendingImage, Interaction_Listening_SendingPoint, Interaction_Listening_SendingMsg, false, currentMode);
                                UserList.Add(my_user);
                                //command = new threadService(my_user);
                                Thread commandThread = new Thread(
                         () =>
                         {
                             command.userCommand(my_user);

                         }

                         );
                                commandThread.Start();
                        }
                            //If we can find the TagID in the list
                            //we change this User's status to the current mode
                        else 
                        {
                                UserList[i].MyMode = currentMode;
                        }

                        
                    }
                    // when UserList is empty,this user can be considered as first User
                    // we add it into list, and start a command thread to the further action
                    else
                    {
                        Debug.WriteLine("Before add, UserList count:{0}", UserList.Count);
                        my_user = new User(Tag, false, false, Command_Listening_Socket, Interaction_Listening_ReceivingMsgFromMobile, Interaction_Listening_SendingImage, Interaction_Listening_SendingPoint, Interaction_Listening_SendingMsg, false, currentMode);
                        UserList.Add(my_user);
                        Debug.WriteLine("after add, UserList tagID:{0}", UserList[0].UserID);
                       // command = new threadService(my_user);
                        Thread commandThread = new Thread(
                         () =>
                         {
                             command.userCommand(my_user);

                         }

                         );

                        Debug.Write("add into UserList");
                        commandThread.Start();
                    }
                }
                // if the mode's name is anthing else, 
                // we need tell the user need setup firstly
                else
                {
                    Debug.WriteLine("please setup firstly");
                }
                return UserList;

            }
            

        }

        public User My_user
        {
            get { return my_user; }
            set { my_user = value; }
        }

        public string[] trim(string msg)
        {

            string[] Msg = msg.Split(' ');

            return Msg;

        }
    }
}
