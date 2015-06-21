using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serverControl
{
    public class paragraphSelectedEventArgs
    {
        public string Coordinate;
        public int TagID;
        public double CoordinateOfTabletopPointX;
        public double CoordinateOfTabletopPointY;
        public User user;
    }
}
