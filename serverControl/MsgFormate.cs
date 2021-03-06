﻿using System;
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
            end,
            button,
            editText,
            drawing,
            linearLayout,
            dropdownList,
            textbox,
            surfaceTextbox,
            image,
            dialog
        };



        //-----------Surface to mobile message formate-------------------------
        private static String Speech = "speech;{0};{1};{2}/>"; // Speech; TagID, Caption; Content
        private static String ViabrateMsg = "vibrate/>";//vibrate
        private static String Success = "success/>";
        private static String failure = "failure/>";
        private static String end = "end/>";
        //------------surface to mobile interface message formate
        private static String buttonMsg = "button;{0};{1};{2}/>"; //button;tagID;caption;content
        private static String editTextMsg = "editText;{0};{1};{2}/>";//editText;tagID;caption;content
        private static String drawingMsg = "drawing;{0};{1};{2}/>"; //drawomg;tagID;caption;content
        private static String linearLayoutMsg = "linearLayout;{0};{1};{2}/>";//linearLayout;tagID;caption;content 
        private static String dropdownList = "dropdownList;{0};{1};{2}/>";//dropdownList;tagID;caption;content 
        private static String Textbox = "textbox;{0};{1};{2}/>";//textbox;tagID;caption;content 
        private static String SurfaceTextbox = "surfaceTextbox;{0};{1};{2}/>";//textbox;tagID;caption;content 
        private static String Image = "image;{0};{1};{2}/>";//image;tagID;caption;content 
        private static String dialog = "dialog;{0};{1};{2}/>";//dialog;tagID;title;content 


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
        public static MsgFormate newDropdownList()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = dropdownList;
            msg.messageType = MsgTpye.dropdownList;
            return msg;

        }
        public static MsgFormate newEditText()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = editTextMsg;
            msg.messageType = MsgTpye.editText;
            return msg;
        }

        public static MsgFormate newTextbox(String content, String caption, String TagID)
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = String.Format(Textbox, TagID, caption, content);
            msg.messageType = MsgTpye.textbox;
            return msg;

        }
        public static MsgFormate newSurfaceTextbox()
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = SurfaceTextbox;
            msg.messageType = MsgTpye.surfaceTextbox;
            return msg;

        }
        public static MsgFormate newImage(string LockStatus)
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText =String.Format(Image,"",LockStatus,"");
            msg.messageType = MsgTpye.image;
            return msg;

        }

        public static MsgFormate newDialog(String message, String title, String TagID)
        {
            MsgFormate msg = new MsgFormate();
            msg.messageText = String.Format(dialog, TagID, title, message);
            msg.messageType = MsgTpye.dialog;
            return msg;

        }




    }
}