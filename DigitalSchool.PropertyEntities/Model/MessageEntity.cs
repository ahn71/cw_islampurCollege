using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DS.PropertyEntities.Model
{
    public class MessageEntity
    {
        public Panel _PnlMsg;
        public Panel _PnlSuccess;
        public Panel _PnlError;
        public Label _SuccessMessage;
        public Label _ErrMessage;

        private static MessageEntity _msgEntity;

        private MessageEntity()
        {
            _PnlMsg = new Panel();
            _PnlSuccess = new Panel();
            _PnlError = new Panel();
            _SuccessMessage = new Label();
            _ErrMessage = new Label();
        }

        public static MessageEntity _MsgEntity
        {
            get
            {
                if (_msgEntity == null)
                {
                    _msgEntity = new MessageEntity();
                }
                return _msgEntity;
            }
        }
    }
}
