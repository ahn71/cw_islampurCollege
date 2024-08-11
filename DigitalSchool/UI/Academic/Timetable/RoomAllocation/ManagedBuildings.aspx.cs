using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.BLL.Timetable;
using DS.PropertyEntities.Model;
using DS.PropertyEntities.Model.Timetable;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Timetable.RoomAllocation
{
    public partial class ManagedBuildings : System.Web.UI.Page
    {
        BuildingNameEntry buildingNameEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!Page.IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ManagedBuildings.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                loadBuildingName();
            } 
        }

        private void loadBuildingName()
        {
            string divInfo = string.Empty;
            if (buildingNameEntry == null)
            {
                buildingNameEntry = new BuildingNameEntry();
            }
            List<BuildingNameEntities> BuildingList = buildingNameEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Building Name</th>";
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
                id = BuildingList[x].BuildingId.ToString();                
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=buildingName" + id + ">" + BuildingList[x].BuildingName.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editBuilding(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }
        
        private BuildingNameEntities GetFormData()
        {
            BuildingNameEntities buildingNameEntities = new BuildingNameEntities();        
            buildingNameEntities.BuildingId = int.Parse(lblBuidlingId.Value.ToString());
            buildingNameEntities.BuildingName = txtBuildingName.Text.Trim();            
            return buildingNameEntities;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblBuidlingId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadBuildingName(); return; }
                lblBuidlingId.Value = "0";
                if (SaveBuildingName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateBuildingName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);                
            }
        }

        private Boolean SaveBuildingName()
        {
            try
            {
                using (BuildingNameEntities entities = GetFormData())
                {
                    if (buildingNameEntry == null)
                    {
                        buildingNameEntry = new BuildingNameEntry();
                    }
                    buildingNameEntry.AddEntities = entities;
                    bool result = buildingNameEntry.Insert();                    
                    btnSubmit.Text = "Save";
                    lblBuidlingId.Value = string.Empty;
                    loadBuildingName();
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
                using (BuildingNameEntities entities = GetFormData())
                {
                    if (buildingNameEntry == null)
                    {
                        buildingNameEntry = new BuildingNameEntry();
                    }
                    buildingNameEntry.AddEntities = entities;
                    bool result = buildingNameEntry.Update();                    
                    loadBuildingName();
                    lblBuidlingId.Value = string.Empty;
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
            txtBuildingName.Text = string.Empty;
        }
    }
}