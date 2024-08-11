using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.BLL.Timetable;
using DS.PropertyEntities.Model;
using DS.PropertyEntities.Model.Timetable;
using DS.SysErrMsgHandler;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Timetable.RoomAllocation
{
    public partial class Allocated : System.Web.UI.Page
    {
        RoomEntry rmEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!Page.IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "Allocated.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                GetBuildName();
                DataBindToTableView(null);
            }
        }

        private void GetBuildName()
        {            
            BuildingNameEntry buildingName = new BuildingNameEntry();
            List<BuildingNameEntities> buildingNameList = buildingName.GetEntitiesData();
            drpBuildingName.DataTextField = "BuildingName";
            drpBuildingName.DataValueField = "BuildingId";
            drpBuildingName.DataSource = buildingNameList;
            drpBuildingName.DataBind();
            drpBuildingName.Items.Insert(0, new ListItem("...Select Building Name...", "0"));
        }

        private RoomEntities GetFormData()
        {
            RoomEntities rmEntities = new RoomEntities();
            rmEntities.RoomId = int.Parse(lblRmId.Value);
            rmEntities.RoomName = TxtRName.Text.Trim();
            rmEntities.RoomCapacity = int.Parse(TxtRCapacity.Text.Trim());
            rmEntities.BuildingId = int.Parse(drpBuildingName.SelectedValue);
            return rmEntities;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblRmId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindToTableView(int.Parse(drpBuildingName.SelectedValue)); return; }
                lblRmId.Value = "0";
                if (SaveName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);                
            }            
        }

        private Boolean SaveName()
        {
            try
            {
                using (RoomEntities entities = GetFormData())
                {                
                    if (rmEntry == null)
                    {
                        rmEntry = new RoomEntry();
                    }
                    rmEntry.AddEntities = entities;
                    bool result = rmEntry.Insert();
                    lblRmId.Value = string.Empty;
                    DataBindToTableView(entities.BuildingId);
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

        private Boolean UpdateName()
        {
            try
            {
                using (RoomEntities entities = GetFormData())
                {
                    if (rmEntry == null)
                    {
                        rmEntry = new RoomEntry();
                    }
                    rmEntry.AddEntities = entities;
                    bool result = rmEntry.Update();
                    lblRmId.Value = string.Empty;
                    btnSubmit.Text = "Save";
                    DataBindToTableView(entities.BuildingId);
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
            ClearFields();
        }

        private void ClearFields()
        {
            drpBuildingName.SelectedValue = "0";
            TxtRName.Text = string.Empty;
            TxtRCapacity.Text = string.Empty;
        }

        protected void drpBuildingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblBuidlingId.Value == string.Empty)
            {
                int? id = (int.Parse(drpBuildingName.SelectedValue));
                DataBindToTableView(id = (id == 0) ? null : id);
                TxtRName.Text = string.Empty;
                TxtRCapacity.Text = string.Empty;                
            }                      
        }

        private void DataBindToTableView(int? buildingId)
        {
            string divInfo = string.Empty;
            if (rmEntry == null)
            {
                rmEntry = new RoomEntry();
            }
            List<RoomEntities> RMList = rmEntry.GetEntitiesData(buildingId);
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Building Name</th>";
            divInfo += "<th>Room Name</th>";
            divInfo += "<th>Capacity</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if(RMList == null)
            {                           
                divInfo += "</tbody>";                
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }            
            string id = string.Empty;
            string buildingsId = string.Empty;
            for (int x = 0; x < RMList.Count; x++)
            {
                id = RMList[x].RoomId.ToString();
                buildingsId = RMList[x].BuildingId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=buildingId" + buildingId + ">" + RMList[x].BuildingName.ToString() + "</span></td>";
                divInfo += "<td><span id=roomName" + id + ">" + RMList[x].RoomName.ToString() + "</span></td>";
                divInfo += "<td><span id=capacity" + id + ">" + RMList[x].RoomCapacity.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editRM(" + id + ", " + buildingsId + ");'/>";
                divInfo += "</tr>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }
    }
}