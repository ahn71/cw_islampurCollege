using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;
using System.Drawing;
namespace DS.UI.Administration.Users
{
    public partial class UserAccountList : System.Web.UI.Page
    {
        UserAccountEntry uae;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to perform this action.";
                }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "UserAccountList.aspx","")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                UserAccountEntry.getUserList(gvAccountList);
            }
        }

        protected void gvAccountList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ShowUserIdPassord"))
                {
                   
                    int rIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    Label lbl = (Label)gvAccountList.Rows[rIndex].Cells[7].FindControl("lblUIP");
                    if (lbl.Text == "Show")
                    {
                        gvAccountList.Rows[rIndex].Cells[3].Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(gvAccountList.Rows[rIndex].Cells[3].Text);
                        gvAccountList.Rows[rIndex].Cells[4].Text = DS.DAL.ComplexScripting.ComplexLetters.getEntangledLetters(gvAccountList.Rows[rIndex].Cells[4].Text);
                        lbl.Text = "Hide";
                        lbl.ForeColor = Color.Red;
                    }
                    else
                    {
                        gvAccountList.Rows[rIndex].Cells[3].Text = DS.DAL.ComplexScripting.ComplexLetters.getTangledLetters(gvAccountList.Rows[rIndex].Cells[3].Text);
                        gvAccountList.Rows[rIndex].Cells[4].Text = DS.DAL.ComplexScripting.ComplexLetters.getTangledLetters(gvAccountList.Rows[rIndex].Cells[4].Text);
                        lbl.Text = "Show";
                        lbl.ForeColor = Color.Green;
                    }
                }
                else if (e.CommandName.Equals("Change"))
                {
                     int rIndex = Convert.ToInt32(e.CommandArgument.ToString());

                    Response.Redirect("~/UI/Administration/Users/ChangeUserAccount.aspx?id="+gvAccountList.DataKeys[rIndex].Value.ToString());
                }
                else if (e.CommandName.Equals("Evaluator"))
                {
                    int rIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    Button btnEvaluator = (Button)gvAccountList.Rows[rIndex].FindControl("btnEvaluator");
                    string IsEvaluator = (btnEvaluator.Text.Trim().Equals("Yes")) ? "0" : "1";
                    if (uae == null) uae = new UserAccountEntry();
                    if (uae.UpdateIsEvaluator(gvAccountList.DataKeys[rIndex].Value.ToString(), IsEvaluator))
                    {
                        btnEvaluator.Text = (btnEvaluator.Text.Trim().Equals("Yes")) ? "No" : "Yes";
                    }

                }
            }
            catch { }
        }

        protected void gvAccountList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserAccountEntry.getUserList(gvAccountList);

            gvAccountList.PageIndex = e.NewPageIndex;
            gvAccountList.DataBind();
        }
    }
}