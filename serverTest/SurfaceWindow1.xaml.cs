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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using serverControl;
using System.Diagnostics;
using System.Net.Sockets;


namespace serverTest
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        ConvertIntoPhysicalDistance convertor;

        Point LoginButtonTopLeftToScreen, LoginButtonBottomRightToScreen, ForgetButtonTopLeftToScreen, ForgetButtonBottomRightToScreen;
        public double LoginButtonTofet, LoginButtonToTop;


        Point TextBoxTopLeftToScreen, TextBoxBottomRightToScreen;
        public double TextBoxTofet, TextBoxToTop;

        Point TextBoxAddressTopLeftToScreen, TextBoxAddressBottomRightToScreen;
        public double TextBoxAddressTofet, TextBoxAddressToTop;

        Point lockStatusImageTopLeftToScreen, lockStatusImageBottomRightToScreen;
        public double lockStatusImageTofet, lockStatusImageToTop;

        Point surfaceTextBoxTopLeftToScreen, surfaceTextBoxBottomRightToScreen;
        public double surfaceTextBoxTofet, surfaceTextBoxToTop;

        Point passwordTopLeftToScreen, passwordBottomRightToScreen;
        public double passwordTofet, passwordToTop;

        //
        Point UsernameTopLeftToScreen;
        Point UsernameBottomRightToScreen;
        double UsernameTofet;
        double UsernameWidth ;
        double UsernameHeight ;
        double UsernameToTop;
        //


        double loginWidth, loginHeight, forgetWidth, forgetHeight;
        double TextBoxWidth, TextBoxHeight;
        double lockStatusImageWidth, lockStatusImageHeight;
        double passwordWidth, passwordHeight;
        double surfaceTextBoxWidth, surfaceTextBoxHeight;
        double TextBoxAddressWidth, TextBoxAddressHeight;

        List<User> userlist = new List<User>();
        int currentUserID;
        bool found = false;
        string lockStatus = "unlock";
        SurfaceTextBox textbox;


        //User user;
        public SurfaceWindow1()
        {
            InitializeComponent();
            UserControl1 uc = new UserControl1();
            convertor = new ConvertIntoPhysicalDistance();
            //newDefinition = new TagVisualizationDefinition();
            //newDefinition.Value = 5;
            //newDefinition.Source = new Uri("ToolTips.xaml", UriKind.Relative);
            //newDefinition.UsesTagOrientation = true;
            //newDefinition.TagRemovedBehavior = TagRemovedBehavior.Fade;
            //newDefinition.PhysicalCenterOffsetFromTag = new Vector(1.0, 5.0);

            //myScatter.Visibility = Visibility.Hidden;


            uc.ac = new AcceptClient();


            uc.ac.command = new threadService();
            uc.ac.command.communication = new CommunicationThread();
            uc.ac.command.communication.objectClicked += new CommunicationThread.myObjectClickedEventHandler(ct_objectClicked);
            uc.ac.command.communication.ItemSelected += new CommunicationThread.myItemSelectedEventHandler(ct_ItemSelected);
            uc.ac.command.communication.returnValue += new CommunicationThread.myReturnValueEventHandler(communication_returnValue);


            //bool paragraphSelected = compare.compareWith(TextBoxTofet, TextBoxToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, TextBoxWidth, TextBoxHeight);
            myControl.Children.Add(uc);
            //helloTag.Definitions.Add(newDefinition);
            
            helloTag.Visibility = Visibility.Hidden;//helloTag is the name of TagVisualizer
            helloTagSix.Visibility = Visibility.Hidden;
            AddWindowAvailabilityHandlers();


        }
        void communication_returnValue(object sender, returnValueEventArgs e)
        {
            if (e.nameOfInterface == "changeFontColor")
            {
                if (e.color == "red")
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("you change the font color to red","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);

                    tbSurfaceTextBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            tbSurfaceTextBox.Foreground = Brushes.Red;
                        }
                ));

                }
                else if (e.color == "blue")
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("you change the font color to blue","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);

                    tbSurfaceTextBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            tbSurfaceTextBox.Foreground = Brushes.Blue;
                        }
                ));
                }
                else if (e.color == "yellow")
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("you change the font color to yellow","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);

                    tbSurfaceTextBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            tbSurfaceTextBox.Foreground = Brushes.Yellow;
                        }
                ));
                }

            }
            else if (e.nameOfInterface == "changeFontSize")
            {
                if (e.changeFondSize == "grow")
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("you grow the font size","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);

                    tbSurfaceTextBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            double currentFontSize = tbSurfaceTextBox.FontSize;
                            tbSurfaceTextBox.FontSize = ++currentFontSize;

                        }
                ));
                }
                else if (e.changeFondSize == "shrink")
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("you shrink the fond size","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);

                    tbSurfaceTextBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            double currentFontSize = tbSurfaceTextBox.FontSize;
                            tbSurfaceTextBox.FontSize = --currentFontSize;
                        }
                ));
                }
            }
            else if (e.nameOfInterface == "login form")
            {
                if (e.password == "hcilab" && e.username == "hcilab" || e.password == "hcilabgood" && e.username == "hcilabgood")
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("login successful","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newSuccess(),
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);
                    MessageBox.Show("login successful");



                }
                else
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("username or password is incorrect, please try again","Successful",""),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEnd()

                                                       };
                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);
                    MessageBox.Show("login failure,please try again");



                }
            }
            else if (e.nameOfInterface == "accessControl")
            {

                if (e.lockStatus != lockStatus)
                {
                    if (userlist.Count == 0)
                    {
                        userlist.Add(e.user);
                        lockStatus = e.lockStatus;
                        currentUserID = e.user.UserID;
                        serverControl.MsgFormate[] myMessage = {
                                serverControl.MsgFormate.newSpeech("You Just lock the textbox only you can edit","Successful",e.user.UserID.ToString()),
                                serverControl.MsgFormate.newVibrate(), 
                               // serverControl.MsgFormate.newSurfaceTextbox(),
                               serverControl.MsgFormate.newDialog("successfully the textbox","Warning",e.user.UserID.ToString()),
                                serverControl.MsgFormate.newEnd()

                                                       };

                        SendMsg sendMsgToMobile = new SendMsg();
                        string message = sendMsgToMobile.translateMessage(myMessage);
                        sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);
                        if (lockStatus == "lock")
                        {
                            tbLockStatusImage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(
                                delegate()
                                {
                                    tbLockStatusImage.Source = new BitmapImage(new Uri(@"C:\Users\Kong\Pictures\Lock-Unlock-icon.png", UriKind.Absolute));
                                   
                                }
                        ));
                        }
                        else
                        {
                            tbLockStatusImage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            tbLockStatusImage.Source = new BitmapImage(new Uri(@"C:\Users\Kong\Pictures\Lock-lock-icon.png", UriKind.Absolute));

                        }
                    ));
                        }


                    }
                    else if (userlist.Count > 0)
                    {
                        if (e.user.UserID == currentUserID)
                        {
                            serverControl.MsgFormate[] myMessage = {
                                    serverControl.MsgFormate.newSpeech("You Just unlock the textbox everyone can edit now ","Successful",e.user.UserID.ToString()),
                                    serverControl.MsgFormate.newVibrate(), 
                                    serverControl.MsgFormate.newDialog("Successfully unlock the textbox by "+userlist[0].UserID+" user","Warning",e.user.UserID.ToString()),
                                    //serverControl.MsgFormate.newSurfaceTextbox(),
                                    serverControl.MsgFormate.newEnd()

                                                       };

                            SendMsg sendMsgToMobile = new SendMsg();
                            string message = sendMsgToMobile.translateMessage(myMessage);
                            sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);
                            lockStatus = e.lockStatus;
                            currentUserID = 0;
                            userlist.Remove(e.user);
                            if (lockStatus == "lock")
                            {
                                tbLockStatusImage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                new Action(
                                    delegate()
                                    {
                                        tbLockStatusImage.Source = new BitmapImage(new Uri(@"C:\Users\Kong\Pictures\Lock-Unlock-icon.png", UriKind.Absolute));

                                    }
                            ));
                            }
                            else
                            {
                                tbLockStatusImage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                            delegate()
                            {
                                tbLockStatusImage.Source = new BitmapImage(new Uri(@"C:\Users\Kong\Pictures\Lock-lock-icon.png", UriKind.Absolute));

                            }
                        ));
                            }
                        }
                        else
                        {
                            serverControl.MsgFormate[] myMessage = {
                                    serverControl.MsgFormate.newSpeech("You cannot edit the text until tag "+userlist[0].UserID+" user unlock","Successful",e.user.UserID.ToString()),
                                    serverControl.MsgFormate.newVibrate(),

                                    serverControl.MsgFormate.newDialog("You cannot edit the text until tag"+userlist[0].UserID+" user unlock","Warning",e.user.UserID.ToString()),
                                    serverControl.MsgFormate.newEnd()

                                                       };
                            

                            SendMsg sendMsgToMobile = new SendMsg();
                            string message = sendMsgToMobile.translateMessage(myMessage);
                            sendMsgToMobile.SendMsgToMobile(e.user.interaction_SendMsg_Socekt, message);
                        }
                    }
                }





            }
        }


        void ct_ItemSelected(object sender, itemSelectedEventArgs e)
        {



            txtinput.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            txtinput.Text = e.centent;
                            
                        }
                ));
            webOutput.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            Uri siteAddres = new Uri(e.centent.Trim(), UriKind.RelativeOrAbsolute);
                            if (siteAddres.IsAbsoluteUri)
                            {
                                webOutput.Navigate(siteAddres);
                            }
                        }
                ));
        }


        void ct_objectClicked(object sender, objectClickedEventArgs e)
        {

            Trim trim = new Trim();
            Comparison compare = new Comparison();
            double[] MobilePoint = new double[2];
            ConvertIntoPhysicalDistance convertors = new ConvertIntoPhysicalDistance();
            MobilePoint = trim.trimToDouble(trim.trim(e.Coordinate));
            double mobilePointX = MobilePoint[0]; // sent from mobile coodinate X
            double mobilePointY = MobilePoint[1];// coodinate Y
            double tabletopPointX = e.CoordinateOfTabletopPointX;// X coordinate of tag 
            double tabletopPointY = e.CoordinateOfTabletopPointY;// Y coordinate of tag

            // convert mobile point and tabletop point into phsical distance
            double tabletopPointXPhysical = convertors.ConvertDistanceFromPixel(tabletopPointX);
            double tabletopPointYPhysical = convertors.ConvertDistanceFromPixel(tabletopPointY);
            double mobilePointXPhysical = convertors.ConvertDistanceFromPixelInMobile(mobilePointX);
            double mobilePointYPhysical = convertors.ConvertDistanceFromPixelInMobile(mobilePointY);
            User user = e.user;
            //bool ObjectSelectedOnAutoFill
            bool usernameSelected = compare.compareWith(UsernameTofet, UsernameToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, UsernameWidth, UsernameHeight);
            bool textBoxAddressSelected = compare.compareWith(TextBoxAddressTofet, TextBoxAddressToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, TextBoxAddressWidth, TextBoxAddressHeight);
            bool surfacetextBoxSelected = compare.compareWith(surfaceTextBoxTofet, surfaceTextBoxToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, surfaceTextBoxWidth, surfaceTextBoxHeight);
            bool LockStatusSelected = compare.compareWith(lockStatusImageTofet, lockStatusImageToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, lockStatusImageWidth, lockStatusImageHeight);
            bool passwordSelected = compare.compareWith(passwordTofet, passwordToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, passwordWidth, passwordHeight);
            bool paragraphSelected = compare.compareWith(TextBoxTofet, TextBoxToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, TextBoxWidth, TextBoxHeight);
            bool ObjectSelected = compare.compareWith(LoginButtonTofet, LoginButtonToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, loginWidth, loginHeight);
            if (passwordSelected)
            {
                serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("password field select","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEditText(),
                        serverControl.MsgFormate.newEnd()

                                                       };

                SendMsg sendMsgToMobile = new SendMsg();
                string message = sendMsgToMobile.translateMessage(myMessage);
                sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
            }
            else if (usernameSelected)
            {
                serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("username field selected","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newEditText(),
                        serverControl.MsgFormate.newEnd()

                };

                SendMsg sendMsgToMobile = new SendMsg();
                string message = sendMsgToMobile.translateMessage(myMessage);
                sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
            }
            else if (surfacetextBoxSelected)
            {
                if (currentUserID == 0 || currentUserID == e.TagID)
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("surface text box is selected","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newSurfaceTextbox(),
                        serverControl.MsgFormate.newEnd()

                                                       };

                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
                }
                else
                {
                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("you cannot edit this textbox until tag "+currentUserID+" unlock","failure",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(), 
                        serverControl.MsgFormate.newDialog("You cannot edit the text until tag"+userlist[0].UserID+" user unlock","Warning",e.user.UserID.ToString()),
                        serverControl.MsgFormate.newEnd()

                                                       };

                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
                }
            }
            else if (paragraphSelected)
            {
                string content = "";

                tbTestBox.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                   new Action(
                       delegate()
                       {
                           content = tbTestBox.Text;
                       }
               ));


                serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("paragraph select","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(),
                        serverControl.MsgFormate.newTextbox(content,"textbox",user.UserID.ToString()),  
                        serverControl.MsgFormate.newEnd()

                                                       };

                SendMsg sendMsgToMobile = new SendMsg();
                string message = sendMsgToMobile.translateMessage(myMessage);
                sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
            }
            else if (LockStatusSelected)
            {
                if (lockStatus == "unlock")
                {


                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("lock image is selected","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(),
                        serverControl.MsgFormate.newImage("lock"),
                        serverControl.MsgFormate.newEnd()

                                                       };

                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
                }
                else if (lockStatus == "lock")
                {

                    serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("lock image is selected","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(),
                        serverControl.MsgFormate.newImage("unlock"),
                        serverControl.MsgFormate.newEnd()

                                                       };

                    SendMsg sendMsgToMobile = new SendMsg();
                    string message = sendMsgToMobile.translateMessage(myMessage);
                    sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
                }
            }


            else if (textBoxAddressSelected && user.OnSurface)
            {
                serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("select your URI link","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(),
                        serverControl.MsgFormate.newDropdownList(),
                        serverControl.MsgFormate.newEnd()
                                                       };

                //string message = "success" + ";" + user.UserID.ToString()+";"+"login button clicked" ;
                SendMsg sendMsgToMobile = new SendMsg();
                string message = sendMsgToMobile.translateMessage(myMessage);
                sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);

            }
            else
            {
                //string message = "failure" + ";" + user.UserID.ToString() + ";" + "Nothing Clicked";
                serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("Nothing Clicked","Failure",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(),
                        serverControl.MsgFormate.newFailure(),
                        serverControl.MsgFormate.newEnd()
                                                       };

                SendMsg sendMsgToMobile = new SendMsg();
                string message = sendMsgToMobile.translateMessage(myMessage);
                sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
                if (e.TagID == 5)
                {
                    helloTag.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                            delegate()
                            {
                                if (helloTag.Visibility == Visibility.Visible)
                                    helloTag.Visibility = Visibility.Hidden;
                                else
                                    helloTag.Visibility = Visibility.Visible;
                            }
                    ));
                }
                else if (e.TagID == 6)
                {
                    helloTagSix.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                            delegate()
                            {
                                if (helloTagSix.Visibility == Visibility.Visible)
                                    helloTagSix.Visibility = Visibility.Hidden;
                                else
                                    helloTagSix.Visibility = Visibility.Visible;
                            }
                    ));
                }

            }

        }


        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }


        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }


        private void SurfaceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            Debug.WriteLine("{0},{1}", desktopWorkingArea.BottomRight.X, desktopWorkingArea.BottomRight.Y);

            UsernameTopLeftToScreen= tbUsername.PointToScreen(desktopWorkingArea.TopLeft);
            UsernameBottomRightToScreen = login.PointFromScreen(desktopWorkingArea.BottomRight);
            UsernameTofet = convertor.ConvertDistanceFromPointX(UsernameTopLeftToScreen);
            UsernameWidth = convertor.ConvertDistanceFromPixel(tbUsername.ActualWidth);
            UsernameHeight = convertor.ConvertDistanceFromPixel(tbUsername.ActualHeight);
            UsernameToTop = convertor.ConvertDistanceFromPointY(UsernameTopLeftToScreen, UsernameHeight);
            Debug.WriteLine("Username: To Left={0}, To top={1}", UsernameTofet, convertor.ConvertDistanceFromPointY(UsernameBottomRightToScreen, tbUsername.ActualHeight));
            Debug.WriteLine("Username: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(UsernameBottomRightToScreen, tbUsername.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(UsernameBottomRightToScreen, tbUsername.ActualHeight));

            LoginButtonTopLeftToScreen = login.PointToScreen(desktopWorkingArea.TopLeft);
            LoginButtonBottomRightToScreen = login.PointFromScreen(desktopWorkingArea.BottomRight);
            LoginButtonTofet = convertor.ConvertDistanceFromPointX(LoginButtonTopLeftToScreen);
            loginWidth = convertor.ConvertDistanceFromPixel(login.ActualWidth);
            loginHeight = convertor.ConvertDistanceFromPixel(login.ActualHeight);
            LoginButtonToTop = convertor.ConvertDistanceFromPointY(LoginButtonTopLeftToScreen, login.ActualHeight);
            Debug.WriteLine("Login Button: To Left={0}, To top={1}", convertor.ConvertDistanceFromPointX(LoginButtonTopLeftToScreen), convertor.ConvertDistanceFromPointY(LoginButtonTopLeftToScreen, login.ActualHeight));
            Debug.WriteLine("Login Button: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(LoginButtonBottomRightToScreen, login.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(LoginButtonBottomRightToScreen, login.ActualHeight));

            TextBoxTopLeftToScreen = tbTestBox.PointToScreen(desktopWorkingArea.TopLeft);
            TextBoxBottomRightToScreen = tbTestBox.PointFromScreen(desktopWorkingArea.BottomRight);
            TextBoxTofet = convertor.ConvertDistanceFromPointX(TextBoxTopLeftToScreen);
            TextBoxToTop = convertor.ConvertDistanceFromPointYBox(TextBoxTopLeftToScreen);//convertor.ConvertDistanceFromPointY(TextBoxTopLeftToScreen, tbTestBox.ActualHeight);
            TextBoxWidth = convertor.ConvertDistanceFromPixel(tbTestBox.ActualWidth);
            TextBoxHeight = convertor.ConvertDistanceFromPixel(tbTestBox.ActualHeight);
            Debug.WriteLine("TextBox: To Left={0}, To top={1}", convertor.ConvertDistanceFromPointX(TextBoxTopLeftToScreen), TextBoxToTop);
            Debug.WriteLine("TextBox: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(TextBoxBottomRightToScreen, tbTestBox.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(TextBoxBottomRightToScreen, tbTestBox.ActualHeight));

            TextBoxAddressTopLeftToScreen = txtinput.PointToScreen(desktopWorkingArea.TopLeft);
            TextBoxAddressBottomRightToScreen = txtinput.PointFromScreen(desktopWorkingArea.BottomRight);
            TextBoxAddressTofet = convertor.ConvertDistanceFromPointX(TextBoxAddressTopLeftToScreen);
            TextBoxAddressToTop = convertor.ConvertDistanceFromPointY(TextBoxAddressTopLeftToScreen, txtinput.ActualHeight);
            TextBoxAddressWidth = convertor.ConvertDistanceFromPixel(txtinput.ActualWidth);
            TextBoxAddressHeight = convertor.ConvertDistanceFromPixel(txtinput.ActualHeight);
            Debug.WriteLine("TextBoxAddress: To Left={0}, To top={1}", convertor.ConvertDistanceFromPointX(TextBoxAddressTopLeftToScreen), TextBoxAddressToTop);
            Debug.WriteLine("TextBoxAddress: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(TextBoxAddressBottomRightToScreen, txtinput.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(TextBoxAddressBottomRightToScreen, txtinput.ActualHeight));


            surfaceTextBoxTopLeftToScreen = tbSurfaceTextBox.PointToScreen(desktopWorkingArea.TopLeft);
            surfaceTextBoxBottomRightToScreen = tbSurfaceTextBox.PointFromScreen(desktopWorkingArea.BottomRight);
            surfaceTextBoxTofet = convertor.ConvertDistanceFromPointX(surfaceTextBoxTopLeftToScreen);
            surfaceTextBoxToTop = convertor.ConvertDistanceFromPointYBox(surfaceTextBoxTopLeftToScreen);//convertor.ConvertDistanceFromPointY(surfaceTextBoxTopLeftToScreen, tbSurfaceTextBox.ActualHeight);
            surfaceTextBoxWidth = convertor.ConvertDistanceFromPixel(tbSurfaceTextBox.ActualWidth);
            surfaceTextBoxHeight = convertor.ConvertDistanceFromPixel(tbSurfaceTextBox.ActualHeight);
            Debug.WriteLine("surfaceTextBox: To Left={0}, To top={1}", convertor.ConvertDistanceFromPointX(surfaceTextBoxTopLeftToScreen), surfaceTextBoxToTop);
            Debug.WriteLine("surfaceTextBox: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(surfaceTextBoxBottomRightToScreen, tbSurfaceTextBox.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(surfaceTextBoxBottomRightToScreen, tbSurfaceTextBox.ActualHeight));

            passwordTopLeftToScreen = tbPassword.PointToScreen(desktopWorkingArea.TopLeft);
            passwordBottomRightToScreen = tbPassword.PointFromScreen(desktopWorkingArea.BottomRight);
            passwordTofet = convertor.ConvertDistanceFromPointX(passwordTopLeftToScreen);
            passwordToTop = convertor.ConvertDistanceFromPointY(passwordTopLeftToScreen, tbPassword.ActualHeight);
            passwordWidth = convertor.ConvertDistanceFromPixel(tbPassword.ActualWidth);
            passwordHeight = convertor.ConvertDistanceFromPixel(tbPassword.ActualHeight);
            Debug.WriteLine("password: To Left={0}, To top={1}", convertor.ConvertDistanceFromPointX(passwordTopLeftToScreen), passwordToTop);
            Debug.WriteLine("password: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(passwordBottomRightToScreen, tbPassword.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(passwordBottomRightToScreen, tbPassword.ActualHeight));

            lockStatusImageTopLeftToScreen = tbLockStatusImage.PointToScreen(desktopWorkingArea.TopLeft);
            lockStatusImageBottomRightToScreen = tbLockStatusImage.PointFromScreen(desktopWorkingArea.BottomRight);
            lockStatusImageTofet = convertor.ConvertDistanceFromPointX(lockStatusImageTopLeftToScreen);
            lockStatusImageToTop = convertor.ConvertDistanceFromPointY(lockStatusImageTopLeftToScreen, tbLockStatusImage.ActualHeight);
            lockStatusImageWidth = convertor.ConvertDistanceFromPixel(tbLockStatusImage.ActualWidth);
            lockStatusImageHeight = convertor.ConvertDistanceFromPixel(tbLockStatusImage.ActualHeight);
            Debug.WriteLine("lockStatusImage: To Left={0}, To top={1}", convertor.ConvertDistanceFromPointX(lockStatusImageTopLeftToScreen), lockStatusImageToTop);
            Debug.WriteLine("lockStatusImage: To right={0}, to bottom={1}", convertor.ConvertDistanceFromPointXButtomRight(lockStatusImageBottomRightToScreen, tbLockStatusImage.ActualWidth), convertor.ConvertDistanceFromPointYButtomRight(lockStatusImageBottomRightToScreen, tbLockStatusImage.ActualHeight));
            //ForgetButtonBottomRightToScreen = forgot.PointToScreen(desktopWorkingArea.TopLeft);
            //ForgetButtonTopLeftToScreen = forgot.PointFromScreen(desktopWorkingArea.BottomRight); 
            //forgetWidth = convertor.ConvertDistanceFromPixel(forgot.ActualWidth);
            //forgetHeight = convertor.ConvertDistanceFromPixel(forgot.ActualHeight);




        }

        private void goTo_Click(object sender, RoutedEventArgs e)
        {
            Uri siteAddres = new Uri(txtinput.Text.Trim(),UriKind.RelativeOrAbsolute);
            if (siteAddres.IsAbsoluteUri)
            {
                webOutput.Navigate(siteAddres);
            }
        }

        private void webOutput_Loaded(object sender, RoutedEventArgs e)
        {
            webOutput.Navigate(new Uri("http://www.theverge.com"));
        }

        private void Image_TouchDown(object sender, TouchEventArgs e)
        {
            if (this.webOutput.CanGoBack)
            {
                this.webOutput.GoBack();
            }
        }

        private void Image_TouchDown_1(object sender, TouchEventArgs e)
        {
            if (this.webOutput.CanGoForward)
            {
                this.webOutput.GoForward();
            }
        }


        private void SurfaceButton_Click_browser(object sender, RoutedEventArgs e)
        {
            content.Visibility = Visibility.Hidden;
            browser.Visibility = Visibility.Visible;
        }

        private void SurfaceButton_Click_content(object sender, RoutedEventArgs e)
        {
            content.Visibility = Visibility.Visible;
            browser.Visibility = Visibility.Hidden;
        }








    }
}