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

        Point lockStatusImageTopLeftToScreen, lockStatusImageBottomRightToScreen;
        public double lockStatusImageTofet, lockStatusImageToTop;

        Point surfaceTextBoxTopLeftToScreen, surfaceTextBoxBottomRightToScreen;
        public double surfaceTextBoxTofet, surfaceTextBoxToTop;

        Point passwordTopLeftToScreen, passwordBottomRightToScreen;
        public double passwordTofet, passwordToTop;



        double loginWidth, loginHeight, forgetWidth, forgetHeight;
        double TextBoxWidth, TextBoxHeight;
        double lockStatusImageWidth, lockStatusImageHeight;
        double passwordWidth, passwordHeight;
        double surfaceTextBoxWidth, surfaceTextBoxHeight;
        TagVisualizationDefinition newDefinition;

        SurfaceTextBox textbox;


        //User user;
        public SurfaceWindow1()
        {
            InitializeComponent();
            UserControl1 uc = new UserControl1();
            convertor = new ConvertIntoPhysicalDistance();
            newDefinition = new TagVisualizationDefinition();
            newDefinition.Value = 5;
            newDefinition.Source = new Uri("ToolTips.xaml", UriKind.Relative);
            newDefinition.UsesTagOrientation = true;
            newDefinition.TagRemovedBehavior = TagRemovedBehavior.Fade;
            newDefinition.PhysicalCenterOffsetFromTag = new Vector(1.0, 5.0);

            myScatter.Visibility = Visibility.Hidden;


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
                            double currentFontSize= tbSurfaceTextBox.FontSize;
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
                //how to handler the lock or unlock
            }
        }


        void ct_ItemSelected(object sender, itemSelectedEventArgs e)
        {


            myScatter.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            myTextBox.Text = e.centent;
                            myScatter.Visibility = Visibility.Visible;
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
            double mobilePointX = MobilePoint[0];
            double mobilePointY = MobilePoint[1];
            double tabletopPointX = e.CoordinateOfTabletopPointX;
            double tabletopPointY = e.CoordinateOfTabletopPointY;
            double tabletopPointXPhysical = convertors.ConvertDistanceFromPixel(tabletopPointX);
            double tabletopPointYPhysical = convertors.ConvertDistanceFromPixel(tabletopPointY);
            double mobilePointXPhysical = convertors.ConvertDistanceFromPixelInMobile(mobilePointX);
            double mobilePointYPhysical = convertors.ConvertDistanceFromPixelInMobile(mobilePointY);
            User user = e.user;
            //bool ObjectSelectedOnAutoFill
            bool surfacetextBoxSelected = compare.compareWith(surfaceTextBoxTofet, surfaceTextBoxToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, surfaceTextBoxWidth, surfaceTextBoxHeight);
            bool LockStatusSelected= compare.compareWith(lockStatusImageTofet, lockStatusImageToTop, tabletopPointXPhysical, tabletopPointYPhysical, mobilePointXPhysical, mobilePointYPhysical, lockStatusImageWidth, lockStatusImageHeight);
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
            else if (surfacetextBoxSelected)
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
                MessageBox.Show("lockStatusSelected");
            }


            else if (ObjectSelected && user.OnSurface)
            {
                serverControl.MsgFormate[] myMessage = {
                        serverControl.MsgFormate.newSpeech("login button clicked","Successful",user.UserID.ToString()),
                        serverControl.MsgFormate.newVibrate(),
                        serverControl.MsgFormate.newDropdownList(),
                        serverControl.MsgFormate.newEnd()
                                                       };

                //string message = "success" + ";" + user.UserID.ToString()+";"+"login button clicked" ;
                SendMsg sendMsgToMobile = new SendMsg();
                string message = sendMsgToMobile.translateMessage(myMessage);
                sendMsgToMobile.SendMsgToMobile(user.interaction_SendMsg_Socekt, message);
                helloTag.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            helloTag.Visibility = Visibility.Visible;
                        }
                ));





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








    }
}