using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serverControl
{
    public class itemSelectedEventArgs:EventArgs
    {

        public int TagID;
        public string centent;
        public string caption;
        public User user;

    }
}
