using DS.BLL.ControlPanel;
using DS.BLL.ManagedClass;
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
    public partial class Place : System.Web.UI.Page
    {
        PlaceInformationEntry placeInformationEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!Page.IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "Place.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                loadPlaceName();
            }
        }

        private void loadPlaceName()
        {
            string divInfo = string.Empty;
            if (placeInformationEntry == null)
            {
                placeInformationEntry = new PlaceInformationEntry();
            }
            List<PlaceInformationEntities> BuildingList = placeInformationEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Bus Stand Name</th>";
            if (Session["__Update__"].ToString().Equals("true")) divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (BuildingList == null)
            {
                divInfo += "</tbody>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < BuildingList.Count; x++)
            {
                id = BuildingList[x].PlaceID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=placeName" + id + ">" + BuildingList[x].PlaceName.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editBuilding(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }

        private PlaceInformationEntities GetFormData()
        {
            PlaceInformationEntities placeInformationEntities = new PlaceInformationEntities();
            placeInformationEntities.PlaceID = int.Parse(lblPlaceId.Value.ToString());
            placeInformationEntities.PlaceName = txtPlaceName.Text.Trim();
            return placeInformationEntities;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblPlaceId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadPlaceName(); return; }
                lblPlaceId.Value = "0";
                if (SavePlaceName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdatePlaceName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }

        private Boolean SavePlaceName()
        {
            try
            {
                using (PlaceInformationEntities entities = GetFormData())
                {
                    if (placeInformationEntry == null)
                    {
                        placeInformationEntry = new PlaceInformationEntry();
                    }
                    placeInformationEntry.AddEntities = entities;
                    bool result = placeInformationEntry.Insert();
                    btnSubmit.Text = "Save";
                    lblPlaceId.Value = string.Empty;
                    loadPlaceName();
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

        private Boolean UpdatePlaceName()
        {
            try
            {
                using (PlaceInformationEntities entities = GetFormData())
                {
                    if (placeInformationEntry == null)
                    {
                        placeInformationEntry = new PlaceInformationEntry();
                    }
                    placeInformationEntry.AddEntities = entities;
                    bool result = placeInformationEntry.Update();
                    loadPlaceName();
                    lblPlaceId.Value = string.Empty;
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
            txtPlaceName.Text = string.Empty;
        }
    }
}