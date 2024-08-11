using DS.BLL.SMS;
using DS.PropertyEntities.Model.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Notification
{
    public partial class Template : System.Web.UI.Page
    {
        SMSBodyTitleEntry smsBodyTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    txtMsg.Attributes.Add("MaxLength", "2000");
                    LoadTemplate();
                }
        }
        private void LoadTemplate()
        {
            try
            {
                string divInfo = string.Empty;
                if (smsBodyTitle == null)
                {
                    smsBodyTitle = new SMSBodyTitleEntry();
                }
                List<SMSBodyTitleEntities> TemplateList = smsBodyTitle.GetEntitiesData();
                divInfo = " <table id='tblClassList' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Title</th>";
                divInfo += "<th>Message</th>";
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (TemplateList == null)
                {
                    divInfo += "<tr><td colspan='2'>No Template available</td></tr>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                    divTemplateList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = string.Empty;
                for (int x = 0; x < TemplateList.Count; x++)
                {
                    id = TemplateList[x].TitleID.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td><span id=title" + id + ">" + TemplateList[x].Title.ToString() + "</span></td>";
                    divInfo += "<td><span id=message" + id + ">" + TemplateList[x].Body.ToString() + "</span></td>";  
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editTemplate(" + id + ");'/>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divTemplateList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnAddMsg_Click(object sender, EventArgs e)
        {
            if (lblTitleID.Value == "0")
            {
                if (SaveName() == true)
                {
                    LoadTemplate();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateName() == true)
                {
                    LoadTemplate();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
            }
        }
        private Boolean SaveName()
        {
            try
            {
                using (SMSBodyTitleEntities entities = GetFormData())
                {
                    if (smsBodyTitle == null)
                    {
                        smsBodyTitle = new SMSBodyTitleEntry();
                    }
                    smsBodyTitle.AddEntities = entities;
                    bool result = smsBodyTitle.Insert();
                    lblTitleID.Value = "0";
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
        private SMSBodyTitleEntities GetFormData()
        {
            SMSBodyTitleEntities smsBodyTitle = new SMSBodyTitleEntities();
            smsBodyTitle.TitleID = int.Parse(lblTitleID.Value);
            smsBodyTitle.Title = txtTitle.Text.Trim();
            smsBodyTitle.Body = txtMsg.Text.Trim();
            return smsBodyTitle;
        }
        private Boolean UpdateName()
        {
            try
            {
                using (SMSBodyTitleEntities entities = GetFormData())
                {
                    if (smsBodyTitle == null)
                    {
                        smsBodyTitle = new SMSBodyTitleEntry();
                    }
                    smsBodyTitle.AddEntities = entities;
                    bool result = smsBodyTitle.Update();
                    lblTitleID.Value = "0";
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