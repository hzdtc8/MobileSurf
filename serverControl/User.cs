using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace serverControl
{


    public class User
    {

        Mode my_Mode;
        int userID;//equal to the Surface Tag ID
        bool SurfaceObjectSelected; // a user select an object
        bool onSurface; //the mobile is on the surface or not
        Socket Command_Communication_Socket;
        Socket Interaction_ReceivingMsgFromMobile_Socket;
        Socket Interaction_SendingImage_Socket;
        Socket Interaction_SendingPoint_Socket;
        Socket Interaction_SendMsg_Socket;
        bool isCapture;
        double PointX;
        double PointY;


        public User( int userID, bool SurfaceObjectSelected, bool onSurface,Socket Command_Communication_Socket,Socket Interaction_ReceivingMsgFromMobile_Socket,Socket Interaction_SendingImage_Socket,Socket Interaction_SendingPoint_Socket,Socket Interaction_SendMsg_Socket,bool isCapture,Mode my_Mode)

        {
            this.userID = userID;
            this.SurfaceObjectSelected = SurfaceObjectSelected;
            this.onSurface = onSurface;
            this.Command_Communication_Socket = Command_Communication_Socket;
            this.Interaction_ReceivingMsgFromMobile_Socket = Interaction_ReceivingMsgFromMobile_Socket;
            this.Interaction_SendingImage_Socket = Interaction_SendingImage_Socket;
            this.Interaction_SendingPoint_Socket = Interaction_SendingPoint_Socket;
            this.Interaction_SendMsg_Socket = Interaction_SendMsg_Socket;
            this.isCapture = isCapture;
            this.my_Mode= my_Mode;
        }

        //accessor and mumator 
        public double pointX
        {
            get { return PointX; }
            set { PointX = value; }
        }
        public double pointY
        {
            get { return PointY; }
            set { PointY = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public Mode MyMode
        {
            get { return my_Mode; }
            set { my_Mode= value; }
        }

        public bool surfaceObjectSelected
        {
            get { return SurfaceObjectSelected; }
            set { SurfaceObjectSelected = value; }
        }

        public bool OnSurface
        {
            get { return onSurface; }
            set { onSurface = value; }
        }

        public Socket command_Communication_Socket
        {
            get { return Command_Communication_Socket; }
            set { Command_Communication_Socket = value; }
        }

        public Socket interation_Receiving_Socket
        {
            get { return Interaction_ReceivingMsgFromMobile_Socket; }
            set { Interaction_ReceivingMsgFromMobile_Socket = value; }
        }

        public Socket interation_Sending_Socket
        {
            get { return Interaction_SendingImage_Socket; }
            set { Interaction_SendingImage_Socket = value; }
        }

        public Socket interaction_SendingPoint_Socekt
        {
            get { return Interaction_SendingPoint_Socket; }
            set { Interaction_SendingPoint_Socket = value; }
        }
        public Socket interaction_SendMsg_Socekt
        {
            get { return Interaction_SendMsg_Socket; }
            set { Interaction_SendMsg_Socket = value; }
        }

        public bool IsCapture
        {
            get { return isCapture; }
            set { isCapture = value; }
        }

    }

}
