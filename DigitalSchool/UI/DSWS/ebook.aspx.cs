using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.DSWS;
using System.Data;
namespace DS.UI.DSWS
{
    public partial class ebook : System.Web.UI.Page
    {
        string divInfo = "";
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
            loadEbooks(ddlClass.SelectedValue);
            }
        }
        private void loadEbooks(string ClassId) 
        {
            
            dt = new DataTable();
            dt = EBooksBLL.getEbooksData(ClassId);
            for (int r = 0; r < dt.Rows.Count;r++ )
                divInfo += "<div class='col-sm-6 col-md-3 no-margin'>" +
                     "<div class='thumbnail'>" +
                      " <img alt='...' src='/Images/dsimages/ebook/school_e-book/" + dt.Rows[r]["BookImage"].ToString() + "' >" +
                       "<div class='caption'>" +
                         "<h5>" + dt.Rows[r]["BookTiltle"].ToString() + "</h5>" +
                         "<p><a role='button' class='btn btn-default' href='http://" + dt.Rows[r]["BookReadUrl"].ToString() + "' target='_blank'>পড়ুন</a> <a role='button' class='btn btn-default' href='http://" + dt.Rows[r]["BookDownUrl"].ToString() + "'>ডাউনলোড</a></p>" +
                       "</div>" +
                     "</div>" +
                  "</div>";
            divEbooks.Controls.Add(new LiteralControl(divInfo));
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadEbooks(ddlClass.SelectedValue);
        }

    }
}