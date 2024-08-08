using DS.BLL.ControlPanel;
using DS.BLL.ManageUser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Users
{
    public partial class StudentAccountList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if(!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to perform this action.";
                }
                catch { }

                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "StudentAccountList.aspx","")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                StudentUserAccountEntry.getStudentAccountList(gvAccountList);
            }
        }
        protected void gvAccountList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ShowUserIdPassord"))
                {

                    int rIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    Label lbl = (Label)gvAccountList.Rows[rIndex].FindControl("lblUIP");
                    if (lbl.Text == "Show")
                    {
                     
                        gvAccountList.Rows[rIndex].Cells[3].Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(gvAccountList.Rows[rIndex].Cells[3].Text);
                        lbl.Text = "Hide";
                        lbl.ForeColor = Color.Red;
                    }
                    else
                    {                        
                        gvAccountList.Rows[rIndex].Cells[3].Text = DS.DAL.ComplexScripting.ComplexLetters.getTangledLetters(gvAccountList.Rows[rIndex].Cells[3].Text);
                        lbl.Text = "Show";
                        lbl.ForeColor = Color.Green;
                    }
                }
                else if (e.CommandName.Equals("Change"))
                {
                    int rIndex = Convert.ToInt32(e.CommandArgument.ToString());

                    Response.Redirect("~/UI/Administration/Users/ChangeStudentAccount.aspx?id=" + gvAccountList.DataKeys[rIndex].Value.ToString());
                }
            }
            catch { }
        }

        protected void gvAccountList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StudentUserAccountEntry.getStudentAccountList(gvAccountList);
            gvAccountList.PageIndex = e.NewPageIndex;
            gvAccountList.DataBind();
        }
    }
}