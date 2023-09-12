using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class background : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadBackground();
            }

        }
        private void loadBackground()
        {
            try {
                lblBackgroundTitle.InnerText = Session["__BackgroundTitle__"].ToString();
                string url = Session["__BackgroundImgUrl__"].ToString();
                imgforBackground.ImageUrl = url;
                pSpeech.InnerText = Session["__BackgroundDetailsFull__"].ToString();
            } catch(Exception ex) { }
        }
    }
}