using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serverControl
{
    public class userListEventArgs:EventArgs
    {
        public User user;
        public List<User> UserList;
    }
}
