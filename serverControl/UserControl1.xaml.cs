using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Threading;

namespace serverControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        // user control constructor is not allowed to use the infinit loop
        // therefore, I add a thread in the constructor
       
        CaptureScreen cs;
        public AcceptClient ac;
        List<User> userList = new List<User>();


        public UserControl1()
        {

            InitializeComponent();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            IPAddress local = IPAddress.Parse("134.129.125.177");
            IPEndPoint iepCommand = new IPEndPoint(local, 62442);// IP end point for command listening socket
            IPEndPoint iepReceivingMsgFromMobile = new IPEndPoint(local, 52342);// IP end point for receiving socket
            IPEndPoint iepSendingImage = new IPEndPoint(local, 42342);// Ip end point for sending socket
            IPEndPoint iepSendPoint = new IPEndPoint(local, 33342);//ip end point for sending the point
            IPEndPoint iepSendMsg = new IPEndPoint(local, 22222);// IP end Point for sending pensonal message
                
            //create object of AcceptClient, pass the five ip end point to the constructor
            //AcceptClient ac = new AcceptClient(iepCommand, iepReceivingMsgFromMobile, iepSendingImage, iepSendPoint, iepSendMsg);
           
            


            Thread accept = new Thread(
                           () =>
                           {
                               userList=ac.accept(iepCommand, iepReceivingMsgFromMobile, iepSendingImage, iepSendPoint, iepSendMsg);
                                
                               
                           }

                           );

            accept.Start();
            
            this.Loaded += UserControl1_Loaded;
            

        }

        void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            win.TouchUp += (send, ee) =>
            {
                MyTouchUpHandler(send, ee);
            };
            win.TouchMove += (send, ee) =>
            {
                MyTouchMoveHandler(send, ee);
            };
            win.PreviewTouchDown += (send, ee) =>
            {
                MyPreviewTouchDownHandler(send, ee);
            };
        }
        private void MyPreviewTouchDownHandler(object sender, TouchEventArgs e)
        {
            System.Windows.Point tp = e.GetTouchPoint(this).Position;
            bool isTag = e.TouchDevice.GetIsTagRecognized();
            int TagID = (int)e.TouchDevice.GetTagData().Value;
            

            Debug.WriteLine("user.id={0}",TagID );
            foreach (User user in userList)
            {
                Debug.WriteLine("user.id={0}", user.UserID);
                if (isTag)
                {
                    if (user.UserID == TagID)
                    {

                        cs = new CaptureScreen(user);
                        cs.captureScreen();
                        user.OnSurface = true;// after user touchDown, onSurface should be true.
                        user.surfaceObjectSelected = false;// after user touchDown, surfaceObjectSelected should true.
                        user.IsCapture = true;

                    }


                }
                else
                {
                    user.IsCapture = false;
                }
            }
        }

        private void MyTouchMoveHandler(object sender, TouchEventArgs e)
        {
            
            bool isTag = e.TouchDevice.GetIsTagRecognized();

            int TagID = (int)e.TouchDevice.GetTagData().Value;
            
            if (isTag)
            {


                foreach (User user in userList)
                    {
                        Debug.WriteLine("user.id={0}", user.UserID);


                        if (user.UserID == TagID)
                        {


                            System.Windows.Point tp = e.GetTouchPoint(this).Position;
                            SendDataP sendD = new SendDataP(user.interaction_SendingPoint_Socekt);
                            user.pointX = tp.X;
                            user.pointY = tp.Y;

                            Debug.WriteLine("user last point X: "+user.pointX);
                            sendD.sendDataP(tp.X, tp.Y);



                        }
                    }
            }
        }

        private void MyTouchUpHandler(object sender, TouchEventArgs e)
        {
            bool isTag = e.TouchDevice.GetIsTagRecognized();
            int TagID = (int)e.TouchDevice.GetTagData().Value;


            foreach (User user in userList)
            {
                Debug.WriteLine("user.id={0}", user.UserID);
                if (isTag)
                {
                    if (user.UserID == TagID)
                    {
                        long data = e.TouchDevice.GetTagData().Value;
                        Debug.WriteLine("tagID " + data);

                        System.Windows.Point tp1 = PointFromScreen(e.GetTouchPoint(this).Position);

                        if (user.surfaceObjectSelected)
                        {
                            //send msg
                            Debug.WriteLine("Send Message");
                        }
                        
                        user.OnSurface = false;// after user touchDown, onSurface should be true.
                        user.surfaceObjectSelected = false;// after user touchDown, surfaceObjectSelected should true.

                        break;



                    }
                }
            }
        }


        protected override void OnTouchMove(TouchEventArgs e)
        {
            base.OnTouchMove(e);
            MyTouchMoveHandler(this, e);
            e.Handled = true;

        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);

            MyTouchUpHandler(this, e);
            e.Handled = true;


        }
        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            base.OnPreviewTouchDown(e);
            MyPreviewTouchDownHandler(this, e);
            e.Handled = true;
        }

    }
}

