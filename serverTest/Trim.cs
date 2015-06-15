using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serverTest
{
    public class Trim
    {
        public string[] trim(string msg)
        {

            string[] Msg = msg.Split(' ');

            return Msg;

        }

        public double[] trimToDouble(string[] msg)
        {
            double[] finalDouble = new double[2];
            int i=0;
            foreach (string Msg in msg)
            {

                finalDouble[i]=Convert.ToDouble(Msg);
                i++;
            }
            return finalDouble;
            
        }


    }
}
