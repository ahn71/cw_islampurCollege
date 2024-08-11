using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.PropertyEntities.Model.ManagedClass;
using DS.BLL.ManagedClass;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class ManageClassGroup : System.Web.UI.Page
    {
        ClassGroupEntry ClsGrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ManageClassGroup.aspx", btnSubmit)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");

                ddlClass.ClearSelection();
                ddlGroup.ClearSelection();
                ClassEntry.GetEntitiesData(ddlClass);
                GroupEntry.GetEntitiesData(ddlGroup);
                LoadClassGroup();

                //UpdatePanel2.Update();
            }
        }
        private void LoadClassGroup()
        {
            string divInfo = string.Empty;
            if (ClsGrpEntry == null)
            {
                ClsGrpEntry = new ClassGroupEntry();
            }
            List<ClassGroupEntities> ClsGrpList = ClsGrpEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Class  Name</th>";
            divInfo += "<th>Group Name</th>";
            divInfo += "<th>N.Mandatory Subject</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (ClsGrpList == null)
            {                
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            string ClassID = string.Empty;
            string GroupID = string.Empty;
            string nofsub = string.Empty;
            for (int x = 0; x < ClsGrpList.Count; x++)
            {
                id = ClsGrpList[x].ClsGrpID.ToString();
                ClassID = ClsGrpList[x].ClassID.ToString();
                GroupID = ClsGrpList[x].GroupID.ToString();
                nofsub = ClsGrpList[x].NumofMandatorySub.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                //divInfo += "<td><span id=ClsSecID" + id + ">" + ClsSecList[x].ClassName.ToString() + "</span></td>";
                divInfo += "<td><span id=ClassName" + id + ">" + ClsGrpList[x].ClassName.ToString() + "</span></td>";
                divInfo += "<td><span id=GroupName" + id + ">" + ClsGrpList[x].GroupName.ToString() + "</span></td>";
                divInfo += "<td><span id=GroupName" + id + ">" + ClsGrpList[x].NumofMandatorySub.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editCG(" + id + ", " + ClassID + "," + GroupID + ","+nofsub+");'/>";
                divInfo += "</tr>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblClsGrpID.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; LoadClassGroup(); return; }
                lblClsGrpID.Value = "0";
                if (SaveClsGrpName() == true)
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
        private Boolean SaveClsGrpName()
        {
            try
            {
                using (ClassGroupEntities entities = GetFormData())
                {
                    if (ClsGrpEntry == null)
                    {
                        ClsGrpEntry = new ClassGroupEntry();
                    }
                    ClsGrpEntry.AddEntities = entities;
                    bool result = ClsGrpEntry.Insert();
                    LoadClassGroup();
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
        private ClassGroupEntities GetFormData()
        {
            ClassGroupEntities ClsGrpEntities = new ClassGroupEntities();
            ClsGrpEntities.ClsGrpID = int.Parse(lblClsGrpID.Value);
            ClsGrpEntities.ClassID = int.Parse(ddlClass.SelectedValue);
            ClsGrpEntities.ClassName = ddlClass.SelectedItem.Text;
            ClsGrpEntities.GroupID = int.Parse(ddlGroup.SelectedValue);
            ClsGrpEntities.GroupName = ddlGroup.SelectedItem.Text;
            ClsGrpEntities.NumofMandatorySub = int.Parse(txtnumofmandatorySub.Text==""?"0":txtnumofmandatorySub.Text);
            return ClsGrpEntities;
        }
        private Boolean UpdateClsSecName()
        {
            try
            {
                using (ClassGroupEntities entities = GetFormData())
                {
                    if (ClsGrpEntry == null)
                    {
                        ClsGrpEntry = new ClassGroupEntry();
                    }
                    ClsGrpEntry.AddEntities = entities;
                    bool result = ClsGrpEntry.Update();
                    btnSubmit.Text = "Save";
                    LoadClassGroup();
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