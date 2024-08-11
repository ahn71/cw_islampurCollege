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

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class ClassTimeSetName : System.Web.UI.Page
    {
        ClsTimeSetNameEntry clsTimeNameEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ClassTimeSetName.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                DataBindToView();
            }
        }

        private void DataBindToView()
        { 
            string divInfo = string.Empty;
            if (clsTimeNameEntry == null)
            {
                clsTimeNameEntry = new ClsTimeSetNameEntry();
            }
            List<ClsTimeSetNameEntities> ClsTimeSetNameList = clsTimeNameEntry.GetEntitiesData();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Class Time Set Name</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (ClsTimeSetNameList == null)
            {               
                divInfo += "</tbody>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < ClsTimeSetNameList.Count; x++)
            {
                id = ClsTimeSetNameList[x].ClsTimeSetNameId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=ClsTimeSetName" + id + ">" + ClsTimeSetNameList[x].Name.ToString() + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editRow(" + id + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }        

        private ClsTimeSetNameEntities GetFormData()
        {
            ClsTimeSetNameEntities clsTimeSetNameEntities = new ClsTimeSetNameEntities();
            clsTimeSetNameEntities.ClsTimeSetNameId = int.Parse(lblClsTimeSetId.Value);
            clsTimeSetNameEntities.Name = TxtName.Text.Trim();
            return clsTimeSetNameEntities;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblClsTimeSetId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindToView(); return; }
                lblClsTimeSetId.Value = "0";
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
                using (ClsTimeSetNameEntities entities = GetFormData())
                {
                    if (clsTimeNameEntry == null)
                    {
                        clsTimeNameEntry = new ClsTimeSetNameEntry();
                    }
                    clsTimeNameEntry.AddEntities = entities;
                    bool result = clsTimeNameEntry.Insert();
                    lblClsTimeSetId.Value = string.Empty;
                    DataBindToView();
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
                using (ClsTimeSetNameEntities entities = GetFormData())
                {
                    if (clsTimeNameEntry == null)
                    {
                        clsTimeNameEntry = new ClsTimeSetNameEntry();
                    }
                    clsTimeNameEntry.AddEntities = entities;
                    bool result = clsTimeNameEntry.Update();
                    lblClsTimeSetId.Value = string.Empty;
                    DataBindToView();
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
            TxtName.Text = string.Empty;
        }
    }
}