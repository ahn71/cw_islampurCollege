using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model;
using DS.PropertyEntities.Model.ManagedClass;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class ManageGroup : System.Web.UI.Page
    {
        GroupEntry GroupNameEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
             lblMessage.InnerText = string.Empty;
             if (!Page.IsPostBack)
             {
                 if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ManageGroup.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                 loadGroupName();
             }
        }
        private void loadGroupName()
        {
            string divInfo = string.Empty;
            if (GroupNameEntry == null)
            {
                GroupNameEntry = new GroupEntry();
            }
            List<GroupEntities> GroupList = GroupNameEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Group Name</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (GroupList == null)
            {
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < GroupList.Count; x++)
            {
                id = GroupList[x].GroupID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=GroupName" + id + ">" + GroupList[x].GroupName.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editGroup(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblGroupId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadGroupName(); return; } 
                lblGroupId.Value = "0";
                if (SaveGroupName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateGroupName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }
        private Boolean SaveGroupName()
        {
            try
            {
                using (GroupEntities entities = GetFormData())
                {
                    if (GroupNameEntry == null)
                    {
                        GroupNameEntry = new GroupEntry();
                    }
                    GroupNameEntry.AddEntities = entities;
                    bool result = GroupNameEntry.Insert();
                    btnSubmit.Text = "Save";
                    lblGroupId.Value = string.Empty;
                    loadGroupName();
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
        private GroupEntities GetFormData()
        {
            GroupEntities GroupNameEntities = new GroupEntities();
            GroupNameEntities.GroupID = int.Parse(lblGroupId.Value.ToString());
            GroupNameEntities.GroupName = txtGroupName.Text.Trim();
            return GroupNameEntities;
        }
        private Boolean UpdateGroupName()
        {
            try
            {
                using (GroupEntities entities = GetFormData())
                {
                    if (GroupNameEntry == null)
                    {
                        GroupNameEntry = new GroupEntry();
                    }
                    GroupNameEntry.AddEntities = entities;
                    bool result = GroupNameEntry.Update();
                    loadGroupName();
                    lblGroupId.Value = string.Empty;
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to update";
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
    }
}