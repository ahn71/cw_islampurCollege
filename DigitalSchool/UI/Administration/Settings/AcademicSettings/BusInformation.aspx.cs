using DS.BLL.ControlPanel;
using DS.BLL.ManagedClass;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class BusInformation : System.Web.UI.Page
    {
        BusInformationEntry busNameEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!Page.IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "BusInformation.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                loadBusName();
            }
        }

        private void loadBusName()
        {
            string divInfo = string.Empty;
            if (busNameEntry == null)
            {
                busNameEntry = new BusInformationEntry();
            }
            List<BusInformationEntities> BusList = busNameEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Bus Name</th>";
            if (Session["__Update__"].ToString().Equals("true")) divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (BusList == null)
            {
                divInfo += "</tbody>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < BusList.Count; x++)
            {
                id = BusList[x].BusID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=busname" + id + ">" + BusList[x].BusName.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editBuilding(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }

        private BusInformationEntities GetFormData()
        {
            BusInformationEntities BusInformationEntities = new BusInformationEntities();
            BusInformationEntities.BusID = int.Parse(lblBusId.Value.ToString());
            BusInformationEntities.BusName = txtBusName.Text.Trim();
            return BusInformationEntities;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblBusId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadBusName(); return; }
                lblBusId.Value = "0";
                if (SaveBuildingName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateBusName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }

        private Boolean SaveBuildingName()
        {
            try
            {
                using (BusInformationEntities entities = GetFormData())
                {
                    if (busNameEntry == null)
                    {
                        busNameEntry = new BusInformationEntry();
                    }
                    busNameEntry.AddEntities = entities;
                    bool result = busNameEntry.Insert();
                    btnSubmit.Text = "Save";
                    lblBusId.Value = string.Empty;
                    loadBusName();
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

        private Boolean UpdateBusName()
        {
            try
            {
                using (BusInformationEntities entities = GetFormData())
                {
                    if (busNameEntry == null)
                    {
                        busNameEntry = new BusInformationEntry();
                    }
                    busNameEntry.AddEntities = entities;
                    bool result = busNameEntry.Update();
                    loadBusName();
                    lblBusId.Value = string.Empty;
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void ClearField()
        {
            txtBusName.Text = string.Empty;
        }
    }
}