using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.Admission;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;

namespace DS.UI.Academics.Students
{
    public partial class AdmissionDetails : System.Web.UI.Page
    {
        DataTable dt;
        AdmStdInfoEntry AdmStdEntry;
        List<AdmStdInfoEntities> AdmStdList = new List<AdmStdInfoEntities>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "AdmissionDetails.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");
                        if (Session["__View__"].ToString().Equals("false")) btnPrint.Enabled = false;
                        ClassEntry.GetEntitiesData(dlClass);
                        dlClass.Items.Insert(1, new ListItem("All", "00"));
                        ShiftEntry.GetDropDownList(dlShift);
                        dlShift.Items.Insert(1, new ListItem("All", "00"));
                        loadStudentInfo("");
                    }                   
            }
            catch { }
        }
        private void loadStudentInfo(string sqlCmd) // for load student partial information
        {
            try
            {
                if (AdmStdEntry == null)
                {
                    AdmStdEntry = new AdmStdInfoEntry();
                }
                if ((dlClass.SelectedValue == "0" || dlClass.SelectedValue == "00") &&
                    (dlShift.SelectedValue == "0" || dlShift.SelectedValue == "00") &&
                    (ddlStatus.SelectedValue == "0" || ddlStatus.SelectedValue == "1"))
                {
                    AdmStdList = AdmStdEntry.GetEntitiesData();
                }
                else if (dlClass.SelectedValue == "00" && dlShift.SelectedValue == "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c=>c.StdStatus==true);
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c=>c.StdStatus==null);
                    }
                }
                else if (dlClass.SelectedValue == "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue == "1")
                {                   
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.Student.ConfigId==int.Parse(dlShift.SelectedValue));                   
                }
                else if (dlClass.SelectedValue == "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue == "00" && ddlStatus.SelectedValue == "1")
                {                   
                    AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.Student.ClassID==int.Parse(dlClass.SelectedValue));                  
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue == "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true && c.Student.ClassID == int.Parse(dlClass.SelectedValue));
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null && c.Student.ClassID == int.Parse(dlClass.SelectedValue));
                    }
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue == "1")
                {
                    AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.Student.ClassID == int.Parse(dlClass.SelectedValue)
                        && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));  
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true && c.Student.ClassID == int.Parse(dlClass.SelectedValue)
                          && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null && c.Student.ClassID == int.Parse(dlClass.SelectedValue)
                          && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }                    
                }
                
                string divInfo = "";
                divInfo = " <table id='tblStudentInfo' class='table table-striped table-bordered dt-responsive nowrap' style='cellspacing='0' width='100%'' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>SL</th>";
                divInfo += "<th>Admission No</th>";
                divInfo += "<th>Admission Date</th>"; 
                divInfo += "<th>Full Name</th>";               
                divInfo += "<th>Fathers Name</th>";
                divInfo += "<th>Class</th>";               
                divInfo += "<th>Shift</th>";
                divInfo += "<th>Gender</th>";
                divInfo += "<th>Guardian Mobile</th>";
                if (Session["__View__"].ToString().Equals("true"))
                divInfo += "<th>View</th>";
                divInfo += "<th>Status</th>";  
                divInfo += "</tr>";
                divInfo += "</thead>";
                if (AdmStdList == null)
                {
                    divInfo += "</table>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                if (AdmStdList.Count == 0)
                {
                    divInfo += "</table>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }             
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < AdmStdList.Count; x++)
                {
                    int sl = x + 1;
                    id = AdmStdList[x].Student.StudentID.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + sl + "</td>";
                    divInfo += "<td>" + AdmStdList[x].AdmissionNo + "</td>";
                    divInfo += "<td>" + AdmStdList[x].AdmissionDate.ToString("dd-MM-yyyy") + "</td>";
                    divInfo += "<td >" + AdmStdList[x].Student.FullName + "</td>";
                    divInfo += "<td >" + AdmStdList[x].Student.FathersName + "</td>";
                    divInfo += "<td >" + AdmStdList[x].Student.ClassName + "</td>";
                    divInfo += "<td >" + AdmStdList[x].Student.Shift + "</td>";
                    divInfo += "<td>" + AdmStdList[x].Student.Gender + "</td>";
                    divInfo += "<td>" + AdmStdList[x].Student.GuardianMobileNo + "</td>";
                    if (Session["__View__"].ToString().Equals("true"))
                    divInfo += "<td>" + "<img src='/Images/gridImages/view.png' onclick='viewStudent(" + id + ");'  />";
                    if (AdmStdList[x].StdStatus == null)
                    {
                        divInfo += "<td>" + "<img src='/Images/gridImages/pay-now.jpg' id='" + AdmStdList[x].Student.ClassID + "_" + AdmStdList[x].AdmissionNo + "' onclick='PayNow(this);'  />";
                    }
                    else
                    {
                        divInfo += "<td>" + "<img src='/Images/gridImages/paid.jpg' id='btnGPaid'/>";
                    }
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                //divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divStudentDetails.Controls.Add(new LiteralControl(divInfo));               
            }
            catch { }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                if (string.IsNullOrEmpty(dlClass.Text.ToString())) return;
                loadStudentInfo("");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
            }
            catch { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
          //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                if (AdmStdEntry == null)
                {
                    AdmStdEntry = new AdmStdInfoEntry();
                }
                if ((dlClass.SelectedValue == "0" || dlClass.SelectedValue == "00") &&
                    (dlShift.SelectedValue == "0" || dlShift.SelectedValue == "00") &&
                    (ddlStatus.SelectedValue == "0" || ddlStatus.SelectedValue == "1"))
                {
                    AdmStdList = AdmStdEntry.GetEntitiesData();
                }
                else if (dlClass.SelectedValue == "00" && dlShift.SelectedValue == "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true);
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null);
                    }
                }
                else if (dlClass.SelectedValue == "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue == "1")
                {
                    AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                }
                else if (dlClass.SelectedValue == "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue == "00" && ddlStatus.SelectedValue == "1")
                {
                    AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.Student.ClassID == int.Parse(dlClass.SelectedValue));
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue == "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true && c.Student.ClassID == int.Parse(dlClass.SelectedValue));
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null && c.Student.ClassID == int.Parse(dlClass.SelectedValue));
                    }
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue == "1")
                {
                    AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.Student.ClassID == int.Parse(dlClass.SelectedValue)
                        && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                }
                else if (dlClass.SelectedValue != "00" && dlShift.SelectedValue != "00" && ddlStatus.SelectedValue != "1")
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == true && c.Student.ClassID == int.Parse(dlClass.SelectedValue)
                          && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                    else
                    {
                        AdmStdList = AdmStdEntry.GetEntitiesData().FindAll(c => c.StdStatus == null && c.Student.ClassID == int.Parse(dlClass.SelectedValue)
                          && c.Student.ConfigId == int.Parse(dlShift.SelectedValue));
                    }
                }
                if (AdmStdList == null)
                {
                    lblMessage.InnerText = "warning->No Data Available";
                    return;
                }
                if (AdmStdList.Count != 0)
                {
                    Session["__StdAdmDetails__"] = AdmStdList.ToList();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StdAdmDetails');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->No Data Available";
                }
                
            }
            catch { }
        }       
    }
}