using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Master != null) ((Label)Master.FindControl("lblMsg")).Text = "Control Pnel";
            }
            catch { }
        }

        protected void imgUserControl_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("/Admin/Register.aspx");
            }
            catch { }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("/Admin/AddDistrict.aspx");
            }
            catch { }
        }
    }
}