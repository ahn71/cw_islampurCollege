using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.PropertyEntities.Model;
using DS.PropertyEntities.Model.Timetable;
using DS.BLL.Timetable;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class SessionDateTime : System.Web.UI.Page
    {
        SessionEntry SEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SessionDateTime.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                DataBindToView();
            }
        }

        private void DataBindToView()
        {
            string divInfo = string.Empty;
            if (SEntry == null)
            {
                SEntry = new SessionEntry();
            }
            IList<SessionEntities> SessionList = SEntry.GetSessionEntities();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Sl No.</th>";
            divInfo += "<th>Session Name</th>";
            divInfo += "<th>Start of Month</th>";
            divInfo += "<th>End of Month</th>";
            divInfo += "<th>Description</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (SessionList == null)
            {
                divInfo += "<tr><td colspan='6'>No Session Name is Avaliable</td></tr>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < SessionList.Count; x++)
            {
                id = SessionList[x].SessionEntitiesId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=Sl" + id + ">" + x.ToString() + "</span></td>";
                divInfo += "<td><span id=Name" + id + ">" + SessionList[x].SessionName.ToString() + "</span></td>";
                divInfo += "<td><span id=StrMonth" + id + ">" + SessionList[x].StartMonth.ToString("dd, MMMM") + "</span></td>";
                divInfo += "<td><span id=EndMonth" + id + ">" + SessionList[x].EndMonth.ToString("dd, MMMM") + "</span></td>";
                divInfo += "<td><span id=Description" + id + ">" + SessionList[x].Details.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editRow(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblSessionId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindToView(); return; }
                lblSessionId.Value = "0";
                if (SaveUpdateName("Save") == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (SaveUpdateName("Update") == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }

        }

        private Boolean SaveUpdateName(string Save)
        {
            try
            {
                using (SessionEntities SE = GetFormData())
                {
                    bool result = true;
                    if (SEntry == null)
                    {
                        SEntry = new SessionEntry();
                    }
                    SEntry.AddSessionEntities = SE;
                    if(Save.Equals("Save"))
                    {
                        result = SEntry.Insert();
                    }
                    else
                    {
                        result = SEntry.Update();
                    }                   
                    lblSessionId.Value = string.Empty;
                    DataBindToView();
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }        
        
        private SessionEntities GetFormData()
        {
            SessionEntities SEntry = new SessionEntities();
            SEntry.SessionEntitiesId = int.Parse(lblSessionId.Value);        
            SEntry.SessionName = txtSessionName.Text.Trim();
            SEntry.StartMonth = Convert.ToDateTime(StartMonth.Text.Trim());
            SEntry.EndMonth = Convert.ToDateTime(EndMonth.Text.Trim());
            SEntry.Details = txtDetails.Text.Trim();
            return SEntry;
        }

        protected void clearField()
        {
            txtSessionName.Text = string.Empty;
            StartMonth.Text = string.Empty;
            EndMonth.Text = string.Empty;
            txtDetails.Text = string.Empty;
        }
    }
}