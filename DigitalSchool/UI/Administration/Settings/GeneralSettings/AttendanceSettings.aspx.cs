using DS.DAL.ComplexScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.Attendace;
using DS.PropertyEntities.Model.Attendance;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Settings.GeneralSettings
{
    public partial class AttendanceSettings : System.Web.UI.Page
    {
        AttendanceFineEntry AttFineEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AttendanceSettings.aspx", btnSaveFineAmount)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        loadAbsentFineAmount();                       
                    }
                }
            catch { }
        }
        private void loadAbsentFineAmount()
        {
            try
            {
                if (AttFineEntry == null)
                {
                    AttFineEntry = new AttendanceFineEntry();
                }
                List<AttendanceFineEntities> ListAttFineList = new List<AttendanceFineEntities>();
                ListAttFineList = AttFineEntry.GetEntitiesData();
                string divInfo = "";                                 
                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric'>Fine Amount</th>";
                divInfo += "<th >Date</th>";
                divInfo += "<th style='text-align:center;' >IsActive</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (ListAttFineList==null)
                {
                    divInfo += "</tbody></table>";
                    divAbsentFineList.Controls.Add(new LiteralControl(divInfo));
                    return;
                } 
                string id = "";
                for (int x = 0; x < ListAttFineList.Count; x++)
                {
                    id = ListAttFineList[x].AFId.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td class='numeric'>" + ListAttFineList[x].AbsentFineAmount + "</td>";
                    string Date = ListAttFineList[x].Date.ToString("dd-MM-yyyy");
                    divInfo += "<td >" + Date + "</td>";
                    if (ListAttFineList[x].IsActive == true)
                    {
                        divInfo += "<td style='text-align:center;'>Yes</td>";
                    }
                    else
                    {
                        divInfo += "<td style='text-align:center;'>No</td>";
                    }
                    if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editAbsentAmount(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divAbsentFineList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }       
       
        protected void btnSaveFineAmount_Click(object sender, EventArgs e)
        {
            try
            {
                using (AttendanceFineEntities entities = GetData())
               {
                   if (AttFineEntry == null)
                   {
                       AttFineEntry = new AttendanceFineEntry();
                   }
                   AttFineEntry.AddEntities = entities;
                   if (lblAbsentId.Value == "0")
                   {
                       if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadAbsentFineAmount();return; }
                       Boolean result = AttFineEntry.Insert();
                       if (result == true)
                       {
                           Clear();
                           ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);
                       }
                   }
                   else
                   {
                       Boolean result = AttFineEntry.Update();
                       if(result==true)
                       {
                           Clear();
                           ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                       }
                   }
               }                
                loadAbsentFineAmount();
            }
            catch { }
        }
        private AttendanceFineEntities GetData()
        {
            AttendanceFineEntities entitiesdata = new AttendanceFineEntities();
            entitiesdata.AFId = int.Parse(lblAbsentId.Value);
            entitiesdata.AbsentFineAmount = double.Parse(txtAbsentFineAmount.Text);
            entitiesdata.Date = DateTime.Now;
            if (chkAbsentFineCount.Checked)
            {
                entitiesdata.IsActive = true;
            }
            else
            {
                entitiesdata.IsActive = false;
            }
            return entitiesdata;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            txtAbsentFineAmount.Text = "";
            lblAbsentId.Value = "0";
            chkAbsentFineCount.Checked = true;
        }        
    }
}