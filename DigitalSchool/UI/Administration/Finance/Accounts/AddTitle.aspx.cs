using DS.BLL.Finance;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.Accounts
{
    public partial class AddTitle : System.Web.UI.Page
    {
        TitleEntry TleEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadTitle();
            }
        }
        private void LoadTitle()
        {
            try
            {
                string divInfo = string.Empty;
                if (TleEntry == null)
                {
                    TleEntry = new TitleEntry();
                }
                List<TitleEntities> TitleList = TleEntry.GetEntitiesData();
                divInfo = " <table id='tblClassList' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Title</th>";
                divInfo += "<th>Type</th>"; 
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (TitleList == null)
                {
                    divInfo += "<tr><td colspan='2'>No Title available</td></tr>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                    divTemplateList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = string.Empty;
                for (int x = 0; x < TitleList.Count; x++)
                {
                    id = TitleList[x].ID.ToString();
                    string Type = (TitleList[x].Type.ToString() == "True") ? "Expense" : "Income";
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td><span id=title" + id + ">" + TitleList[x].Title.ToString() + "</span></td>";
                    divInfo += "<td><span id=type" + id + ">" + Type + "</span></td>"; 
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editTemplate(" + id + ");'/>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divTemplateList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblTitleID.Value == "0")
            {
                if (SaveName() == true)
                {
                    LoadTitle();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateName() == true)
                {
                    LoadTitle();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
            }
        }
        private Boolean SaveName()
        {
            try
            {
                using (TitleEntities entities = GetFormData())
                {
                    if (TleEntry == null)
                    {
                        TleEntry = new TitleEntry();
                    }
                    TleEntry.AddEntities = entities;
                    bool result = TleEntry.Insert();
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
        private TitleEntities GetFormData()
        {
            TitleEntities tleEntities = new TitleEntities();
            tleEntities.ID = int.Parse(lblTitleID.Value);
            tleEntities.Title = txtTitle.Text.Trim();
            tleEntities.Type = bool.Parse(rblTitleType.SelectedValue);
            return tleEntities;
        }
        private Boolean UpdateName()
        {
            try
            {
                using (TitleEntities entities = GetFormData())
                {
                    if (TleEntry == null)
                    {
                        TleEntry = new TitleEntry();
                    }
                    TleEntry.AddEntities = entities;
                    bool result = TleEntry.Update();
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