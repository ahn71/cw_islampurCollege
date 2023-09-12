using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model;

namespace DS.SysErrMsgHandler
{
    public class ErrorSuccessMsg
    {
        private static MessageEntity _msgEntity;
        public static bool ChkErrorMsg = false;

        public static MessageEntity MsgControl
        {
            set
            {
                _msgEntity = value;
            }
        }

        public static void SuccessMsg()
        {
            if (!ChkErrorMsg)
            {
                _msgEntity._PnlMsg.Visible = true;
                _msgEntity._PnlError.Visible = false;
                _msgEntity._PnlSuccess.Visible = true;
                _msgEntity._ErrMessage.Text = string.Empty;
                _msgEntity._SuccessMessage.Text = "Your Request has been Submitted, Thank you !!";
            }
        }

        public static void SuccessMsg(string msg)
        {
            if (!ChkErrorMsg)
            {
                _msgEntity._PnlMsg.Visible = true;
                _msgEntity._PnlError.Visible = false;
                _msgEntity._PnlSuccess.Visible = true;
                _msgEntity._ErrMessage.Text = string.Empty;
                _msgEntity._SuccessMessage.Text = msg;
            }
        }

        public static void ErrorMsg(string msg)
        {
            _msgEntity._PnlMsg.Visible = true;
            _msgEntity._PnlError.Visible = true;
            _msgEntity._PnlSuccess.Visible = false;
            _msgEntity._ErrMessage.Text = msg;
            _msgEntity._SuccessMessage.Text = string.Empty;
        }
    }
}
