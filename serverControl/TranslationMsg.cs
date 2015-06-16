using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace serverControl
{
    public class TranslationMsg
    {
        public void TranslateMdg(string msg)
        {
            string[] inMsg = msg.Split(';');
            int i=1;
            foreach (string msga in inMsg)
            {
                Debug.WriteLine("#{0} message is {1}", i, msga);
                i++;
            }
        }
    }
}
