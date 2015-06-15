using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serverTest
{
    class Comparison
    {
        public bool compareWith(double controllerToleft,double controllerToTop,double tabletopPointXPhysical,double tabletopPointYPhysical,double mobilePointXPhysical,double mobilePointYPhysical,double wide,double height)
        {
            double TouchPointX = tabletopPointXPhysical + mobilePointXPhysical;
            double TouchPointY = tabletopPointYPhysical + mobilePointYPhysical-1.6;

            if (controllerToleft < TouchPointX && TouchPointX < (controllerToleft + wide) && controllerToTop < TouchPointY && TouchPointY < (controllerToTop + height))
            {
                return true;
            }
            else
                return false;


        }
    }
}
