using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace serverTest
{
    class ConvertIntoPhysicalDistance
    {

        //convert (pixel)PointX into (cm)PointX
        public double ConvertDistanceFromPointX(Point PixelDistance)
        {
            return ConvertDistanceFromPixel(PixelDistance.X);
        }


        //convert (pixel)PointY into (cm)PointY

        public double ConvertDistanceFromPointY(Point PixelDistance, double Height)
        {
            return ConvertDistanceFromPixel(PixelDistance.Y - Height / 2);
        }



        // convert any Pixel in to CM
        public double ConvertDistanceFromPixel(double PixelPoint)
        {
            // 35.955 means 1 cm = 35.955 pixel on the tabletop
            // 21.69 means 1 cm = 21.69 pixel on the tabletop
            return PixelPoint / 21.69;
        }

        public double ConvertDistanceFromPixelInMobile(double PixelPoint)
        {
            // 242.8842504743833 means 1 cm = 124.2718446601942 pixel on the mobile
            return PixelPoint / 124.2718446601942;
        }



        public double ConvertDistanceFromPointXButtomRight(Point PixelDistance, double Width)
        {
            return ConvertDistanceFromPixel(PixelDistance.X - Width);
        }




        public double ConvertDistanceFromPointYButtomRight(Point PixelDistance, double Height)
        {
            return ConvertDistanceFromPixel(PixelDistance.Y - Height);
        }
    }
}
