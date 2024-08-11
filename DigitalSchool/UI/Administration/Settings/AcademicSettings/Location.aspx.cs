using DS.BLL.ControlPanel;
using DS.BLL.ManagedClass;
using DS.BLL.Timetable;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class Location : System.Web.UI.Page
    {
        LocationEntry locationNameEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!Page.IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "Location.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                loadLocationName();
            }
        }

        private void loadLocationName()
        {
            string divInfo = string.Empty;
            if (locationNameEntry == null)
            {
                locationNameEntry = new LocationEntry();
            }
            List<LocationEntities> LocationList = locationNameEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Location Name</th>";
            if (Session["__Update__"].ToString().Equals("true")) divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (LocationList == null)
            {
                divInfo += "</tbody>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < LocationList.Count; x++)
            {
                id = LocationList[x].LocationID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=locationName" + id + ">" + LocationList[x].LocationName.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editLocation(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }

        private LocationEntities GetFormData()
        {
            LocationEntities locationNameEntities = new LocationEntities();
            locationNameEntities.LocationID = int.Parse(lblLocationId.Value.ToString());
            locationNameEntities.LocationName = txtLocationName.Text.Trim();
            return locationNameEntities;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblLocationId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadLocationName(); return; }
                lblLocationId.Value = "0";
                if (SaveLocationName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateLocationName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }

        private bool UpdateLocationName()
        {
            try
            {
                using (LocationEntities entities = GetFormData())
                {
                    if (locationNameEntry == null)
                    {
                        locationNameEntry = new LocationEntry();
                    }
                    locationNameEntry.AddEntities = entities;
                    bool result = locationNameEntry.Update();
                    loadLocationName();
                    lblLocationId.Value = string.Empty;
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

        private Boolean SaveLocationName()
        {
            try
            {
                using (LocationEntities entities = GetFormData())
                {
                    if (locationNameEntry == null)
                    {
                        locationNameEntry = new LocationEntry();
                    }
                    locationNameEntry.AddEntities = entities;
                    bool result = locationNameEntry.Insert();
                    btnSubmit.Text = "Save";
                    lblLocationId.Value = string.Empty;
                    loadLocationName();
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

        private Boolean UpdateBuildingName()
        {
            try
            {
                using (LocationEntities entities = GetFormData())
                {
                    if (locationNameEntry == null)
                    {
                        locationNameEntry = new LocationEntry();
                    }
                    locationNameEntry.AddEntities = entities;
                    bool result = locationNameEntry.Update();
                    loadLocationName();
                    lblLocationId.Value = string.Empty;
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
            txtLocationName.Text = string.Empty;
        }
    }
}