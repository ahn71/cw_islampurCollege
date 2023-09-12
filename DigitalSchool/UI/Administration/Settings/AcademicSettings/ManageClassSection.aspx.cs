using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedClass;
using DS.BLL.ControlPanel;
namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class ManageClassSection : System.Web.UI.Page
    {
        ClassSectionEntry ClsSecEntry;
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ManageClassSection.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
               
                ClassEntry.GetEntitiesData(ddlClass);
                SectionEntry.GetEntitiesData(ddlSection);
                ClassGroupEntry clsgrp = new ClassGroupEntry();
                clsgrp.GetDropDownAllList(ddlGroup);
                ddlGroup.Enabled = false;
                LoadClassSection();
            }
        }
        private void LoadClassSection()
        {
            string divInfo = string.Empty;
            if (ClsSecEntry == null)
            {
                ClsSecEntry = new ClassSectionEntry();
            }
            List<ClassSectionEntities> ClsSecList = ClsSecEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Class  Name</th>";
            divInfo += "<th>Group  Name</th>";
            divInfo += "<th>Section Name</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (ClsSecList == null)
            {                
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            string ClassID = string.Empty;
            string SectionID = string.Empty;
            string GroupID = string.Empty;
            string clsGrpId = string.Empty;
            for (int x = 0; x < ClsSecList.Count; x++)
            {
                id = ClsSecList[x].ClsSecID.ToString();
                ClassID = ClsSecList[x].ClassID.ToString();
                GroupID = ClsSecList[x].GroupID.ToString();
                clsGrpId = ClsSecList[x].ClsGrpID.ToString();
                SectionID = ClsSecList[x].SectionID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                //divInfo += "<td><span id=ClsSecID" + id + ">" + ClsSecList[x].ClassName.ToString() + "</span></td>";
                divInfo += "<td><span id=roomName" + id + ">" + ClsSecList[x].ClassName.ToString() + "</span></td>";
                if (ClsSecList[x].GroupName.ToString() == "")
                {
                    divInfo += "<td><span id=GroupName" + id + ">No Group</span></td>";
                }
                else
                {
                    divInfo += "<td><span id=GroupName" + id + ">" + ClsSecList[x].GroupName.ToString() + "</span></td>";
                }
               
                divInfo += "<td><span id=capacity" + id + ">" + ClsSecList[x].SectionName.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editCS(" + id + ", " + ClassID + "," + clsGrpId + "," + SectionID + ");'/>";
                divInfo += "</tr>";
            }
            divInfo += "</tbody>"; 
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(ddlClass.SelectedValue), ddlGroup);
            }
            catch { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblClsSecID.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; LoadClassSection(); return; }
                lblClsSecID.Value = "0";
                if (SaveClsSecName() == true)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateClsSecName() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }  
        }
        private Boolean SaveClsSecName()
        {
            try
            {
                using (ClassSectionEntities entities = GetFormData())
                {
                    if (ClsSecEntry == null)
                    {
                        ClsSecEntry = new ClassSectionEntry();
                    }
                    ClsSecEntry.AddEntities = entities;
                    bool result = ClsSecEntry.Insert();
                    LoadClassSection();
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
        private ClassSectionEntities GetFormData()
        {
            ClassSectionEntities ClsSecEntities = new ClassSectionEntities();
            ClsSecEntities.ClsSecID = int.Parse(lblClsSecID.Value);
            ClsSecEntities.ClassID = int.Parse(ddlClass.SelectedValue);
            ClsSecEntities.ClassName = ddlClass.SelectedItem.Text;            
            ClsSecEntities.ClsGrpID = int.Parse(ddlGroup.SelectedValue);
            ClsSecEntities.SectionID = int.Parse(ddlSection.SelectedValue);
            ClsSecEntities.SectionName = ddlSection.SelectedItem.Text;
            return ClsSecEntities;
        }
        private Boolean UpdateClsSecName()
        {
            try
            {
                using (ClassSectionEntities entities = GetFormData())
                {
                    if (ClsSecEntry == null)
                    {
                        ClsSecEntry = new ClassSectionEntry();
                    }
                    ClsSecEntry.AddEntities = entities;
                    bool result = ClsSecEntry.Update();
                    btnSubmit.Text = "Save";
                    LoadClassSection();
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