using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serverControl
{
    public class MsgFormate
    {


        public enum MsgTpye
        {
            speech,
            vibrate,
            success,
            failure,
            end
        };

        //-----------Surface to mobile message formate-------------------------

        private static String Speech = "speech;{0};{1};{2}/>"; // Speech; TagID, Caption; Content
        private static String ViabrateMsg = "vibrate/>";//vibrate
        private static String Success = "success/>";
        private static String failure = "failure/>";
        private static String end = "end/>";

        public String messageText { get; set; }
        public MsgTpye messageType {get;set; }

        public static MsgFormate newSpeech(String SpeechText, String caption,String TagID)
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = String.Format(Speech,TagID,caption,SpeechText);
            msg.messageType = MsgTpye.speech;
            return msg;
        }

        public static MsgFormate newVibrate()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = ViabrateMsg;
            msg.messageType = MsgTpye.vibrate;
            return msg;
        }
        public static MsgFormate newSuccess()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageType = MsgTpye.success;
            msg.messageText = Success;
            return msg;

        }
        public static MsgFormate newFailure()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = failure;
            msg.messageType = MsgTpye.failure;
            return msg;
        }
        public static MsgFormate newEnd()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = end;
            msg.messageType = MsgTpye.end;
            return msg;
        }




    }
}