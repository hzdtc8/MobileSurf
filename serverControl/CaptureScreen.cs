using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using ScreenShotDemo;

namespace serverControl
{
    class CaptureScreen
    {
        public Socket Interaction_Sending_Socket;
        public bool isCapture;

        public CaptureScreen(User user)
        {
            this.Interaction_Sending_Socket = user.interation_Sending_Socket;
            Debug.WriteLine("in the caputre screen, accepted");
            this.isCapture = user.IsCapture;
        }


        public void send(byte[] imageByte)
        {



            byte[] imageSize;
            string imgSize = Convert.ToString(imageByte.Length);
            //Debug.WriteLine("string=" + imgSize);
            imageSize = Encoding.ASCII.GetBytes(imgSize);
            //Debug.WriteLine(" header size:" + imageSize.Length);

            Interaction_Sending_Socket.Send(imageSize);
            //Debug.WriteLine("sent size");
            Interaction_Sending_Socket.Send(imageByte);


        }



        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public void captureScreen()
        {

            ScreenCapture sc = new ScreenCapture();
            Image img = sc.CaptureScreen();
            if (!isCapture)
            {
                send(imageToByteArray(img));

            }
            else
            {
                Debug.WriteLine("have already capture screen.");
            }

        }



    }
}
