using DS.BLL.Attendace;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Attendance
{
    public partial class IndividualAbsentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    btnPrintPreview.Enabled = false;
                    btnPrintPreview.CssClass = "";
                    ShiftEntry.GetDropDownList(ddlShiftList);
                    BatchEntry.GetDropdownlist(ddlBatch, true);
                    SheetInfoEntry.loadMonths(ddlMonths);
                    //Classes.commonTask.loadAttendanceSheet(dlSheetName);
                    //dlSheetName.Items.Add("...Select Month...");
                    //dlSheetName.SelectedIndex = dlSheetName.Items.Count - 1;
                }
            lblMessage.InnerText = "";
        }

        //protected void dlSheetName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dlRollNo.Items.Clear();
        //    string[] value = dlSheetName.SelectedItem.Text.Split('_');
        //    string Class = value[1];
        //    string Section = value[2];
        //    string MonthYear = value[3];
        //    string Year = new String(MonthYear.Where(Char.IsNumber).ToArray());
        //    string Batch = Class + Year;
        //    Classes.commonTask.LoadRollNo(dlRollNo, Class, Section, Batch);
        //    dlRollNo.Items.Add("...Select Roll...");
        //    dlRollNo.SelectedIndex = dlRollNo.Items.Count - 1;

        //}
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //....................Validation......................
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (ddlgroup.Enabled == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            if (ddlMonths.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; return; }
            
            //....................................................
            string divInfo = "";
            //string[] value = dlSheetName.SelectedItem.Text.Split('_');
            //string Class = value[1];
            //string Section = value[2];
            //string MonthYear = value[3];
            //string Year = new String(MonthYear.Where(Char.IsNumber).ToArray());
            //string Batch = Class + Year;
            DataTable dtall = new DataTable();
            DataTable dtDis = new DataTable();
            string sqlCmdAll = "SELECT StudentId, RollNo,AttDate as AbsentDate " +
                              " FROM  dbo.DailyAttendanceRecord " +
                              " WHERE AttStatus = 'a' and Format(AttDate,'MM-yyyy')='" + ddlMonths.SelectedValue + "' and StudentId='" + dlRollNo.SelectedValue + "'";
                              //" WHERE AttStatus = 'a' and  ShiftId='" + ddlShiftList.SelectedValue + "' and BatchId='" + ddlBatch.SelectedValue + "' "+
                              //"and  ClsSecId='" + ddlSection.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' "+
                              //"and Format(AttDate,'MM-yyyy')='" + ddlMonths.SelectedValue + "' and RollNo='" + dlRollNo.SelectedItem.Text + "'";
            sqlDB.fillDataTable(sqlCmdAll, dtall); //For All Data
            if (dtall.Rows.Count == 0)
            {
                divIndividualAbsent.Controls.Clear();
                btnPrintPreview.Enabled = false;
                btnPrintPreview.CssClass = "";
                divInfo = "<div class='noData'>No Absent available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divIndividualAbsent.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            btnPrintPreview.Enabled = true;
            btnPrintPreview.CssClass = "btn btn-success pull-right";
            string sqlCmdDis = "SELECT   distinct     dbo.DailyAttendanceRecord.StudentId, dbo.DailyAttendanceRecord.RollNo, dbo.CurrentStudentInfo.FullName," +
                              "dbo.CurrentStudentInfo.ClassName,dbo.CurrentStudentInfo.SectionName" +
                              " FROM  dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId" +
                              " Where dbo.DailyAttendanceRecord.StudentId='" + dlRollNo.SelectedValue+ "' ";
            //string sqlCmdAll = "SELECT StudentId, RollNo,AttDate as AbsentDate " +
            //                  " FROM  dbo.DailyAttendanceRecord " +
            //                  " WHERE AttStatus = 'a' and  BatchId='1'and  ClsSecId='117' and Format(AttDate,'MM-yyyy')='01-2015'and RollNo='" + dlRollNo.SelectedItem.Text + "'";

            //string sqlCmdDis = "SELECT   distinct     dbo.DailyAttendanceRecord.StudentId, dbo.DailyAttendanceRecord.RollNo, dbo.CurrentStudentInfo.FullName," +
            //                   "dbo.CurrentStudentInfo.ClassName,dbo.CurrentStudentInfo.SectionName,dbo.CurrentStudentInfo.BatchName" +
            //                   " FROM  dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId" +
            //                   " WHERE (dbo.DailyAttendanceRecord.AttStatus = 'a'" +
            //                   " and dbo.DailyAttendanceRecord. BatchId='1'and dbo.DailyAttendanceRecord. ClsSecId='117' and Format(AttDate,'MM-yyyy')='01-2015' and dbo.DailyAttendanceRecord.RollNo='" + dlRollNo.SelectedItem.Text + "')"; ;
            
            //sqlDB.fillDataTable("Select * From v_AbsentDetails where BatchName='" + Batch + "' and SectionName='" + Section + "' and RollNo=" + dlRollNo.SelectedItem.Text + " ", dtall); //For All Data
           
            //sqlDB.fillDataTable("Select Distinct RollNo,FullName,ImageName From v_AbsentDetails where BatchName='" + Batch + "' and SectionName='" + Section + "' and RollNo=" + dlRollNo.SelectedItem.Text + " ", dtDis); // For Distinct Data
            sqlDB.fillDataTable(sqlCmdDis, dtDis);
            Session["__AllData__"] = dtall;
            Session["__DistinctData__"] = dtDis;
            string studentId = "";
            string AbsentDays = "";
            int numberOfRecords = 0;
            DataRow[] rows;
            dtDis.Columns.Add("Days");
            dtDis.Columns.Add("Total Days");
            int j = 0;
            for (int i = 0; i < dtall.Rows.Count; i++)
            {
                studentId = dtall.Rows[i]["StudentId"].ToString();
                rows = dtall.Select("StudentId=" + studentId + "");
                numberOfRecords = rows.Length;
                for (int x = i; x < i + numberOfRecords; x++)
                {
                    AbsentDays += DateTime.Parse(dtall.Rows[x]["AbsentDate"].ToString()).ToString("dd");
                    if (x < i + numberOfRecords - 1)
                    {
                        AbsentDays += ",";
                    }
                }
                dtDis.Rows[j]["Days"] = AbsentDays;
                string[] total = dtDis.Rows[0]["Days"].ToString().Split(',');
                int count = 0;
                foreach (string dt in total)
                {
                    count++;
                }
                dtDis.Rows[j]["Total Days"] = count;
                j++;
                AbsentDays = "";
                i += numberOfRecords - 1;
            }
            Session["__AbsentDetails__"] = dtDis;
            //Session["__AttendanceSheet__"] = dlSheetName.Text;
            //Session["__AbsentDetails__"] = dtDis;
            for (int x = 0; x < dtDis.Rows.Count; x++)
            {
                divInfo = " <table id='tblStudentInfo' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tbody>";
                string id = "";
                divInfo += "<tr>";
                divInfo += "<td>Roll No</td>";
                divInfo += "<td >" + dtDis.Rows[x]["RollNo"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td>Name</td>";
                divInfo += "<td >" + dtDis.Rows[x]["FullName"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td>Days</td>";
                divInfo += "<td >" + dtDis.Rows[x]["Days"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr>";
                divInfo += "<td>Total Days</td>";
                divInfo += "<td >" + dtDis.Rows[x]["Total Days"].ToString() + "</td>";
                divInfo += "</tr>";
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divIndividualAbsent.Controls.Add(new LiteralControl(divInfo));
            }
        }
      
        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
           
            DataTable dtall = new DataTable();
            DataTable dtDis = new DataTable();
           // string sqlCmdAll = "SELECT StudentId, RollNo,AttDate as AbsentDate " +                               
           //                    " FROM  dbo.DailyAttendanceRecord "+
           //                    " WHERE AttStatus = 'a' and  ShiftId='"+ddlShiftList.SelectedValue+"' and BatchId='"+ddlBatch.SelectedValue+"'and  ClsSecId='"+ddlSection.SelectedValue+"' and ClsGrpId='"+ddlgroup.SelectedValue+"' and Format(AttDate,'MM-yyyy')='"+ddlMonths.SelectedValue+"' and RollNo='" + dlRollNo.SelectedItem.Text + "'";
           // sqlDB.fillDataTable(sqlCmdAll, dtall);
           // if (dtall.Rows.Count < 1)
           // {
           //     lblMessage.InnerText = "warning-> No Absent !"; return;
           // }
           // string sqlCmdDis = "SELECT   distinct     dbo.DailyAttendanceRecord.StudentId, dbo.DailyAttendanceRecord.RollNo, dbo.CurrentStudentInfo.FullName," +
           //                    "dbo.CurrentStudentInfo.ClassName,dbo.CurrentStudentInfo.SectionName" +
           //                    " FROM  dbo.DailyAttendanceRecord INNER JOIN dbo.CurrentStudentInfo ON dbo.DailyAttendanceRecord.StudentId = dbo.CurrentStudentInfo.StudentId" +
           //                    " Where dbo.DailyAttendanceRecord.StudentId='"+dtall.Rows[0]["StudentId"].ToString()+"' ";
           //                    //" WHERE (dbo.DailyAttendanceRecord.AttStatus = 'a'" +
           //                    //"and dbo.DailyAttendanceRecord.ShiftId='"+ddlShiftList.SelectedValue+"' and dbo.DailyAttendanceRecord. BatchId='" + ddlBatch.SelectedValue + "'and dbo.DailyAttendanceRecord. ClsSecId='" + ddlSection.SelectedValue + "' and dbo.DailyAttendanceRecord. ClsGrpId='" + ddlgroup.SelectedValue + "' and Format(AttDate,'MM-yyyy')='" + ddlMonths.SelectedValue + "' and dbo.DailyAttendanceRecord.RollNo='" + dlRollNo.SelectedItem.Text + "')"; ;
            
           // //...........................
            
           //// sqlDB.fillDataTable(sqlCmdDis , dtDis);
            /* dtall = (DataTable)Session["__AllData__"];
             dtDis = (DataTable)Session["__DistinctData__"];

                //........................
                string studentId = "";
                string AbsentDays = "";
                int numberOfRecords = 0;
                DataRow[] rows;
                dtDis.Columns.Add("Days");
                dtDis.Columns.Add("Total Days");
                int j = 0;
                for (int i = 0; i < dtall.Rows.Count; i++)
                {
                    studentId = dtall.Rows[i]["StudentId"].ToString();
                    rows = dtall.Select("StudentId=" + studentId + "");
                    numberOfRecords = rows.Length;
                    for (int x = i; x < i + numberOfRecords; x++)
                    {
                        AbsentDays += DateTime.Parse(dtall.Rows[x]["AbsentDate"].ToString()).ToString("dd");
                        if (x < i + numberOfRecords - 1)
                        {
                            AbsentDays += ",";
                        }
                    }
                    dtDis.Rows[j]["Days"] = AbsentDays;
                    string[] total = dtDis.Rows[0]["Days"].ToString().Split(',');
                    int count = 0;
                    foreach (string dt in total)
                    {
                        count++;
                    }
                    dtDis.Rows[j]["Total Days"] = count;
                    j++;
                    AbsentDays = "";
                    i += numberOfRecords - 1;
                }   
                */
           
            string[] Month = dlSheetName.Text.Split('_');
            string GrpName = (ddlgroup.SelectedValue == "0") ? "No Group" : ddlgroup.SelectedItem.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=IndivisualAbsentDetails-" +ddlBatch.SelectedItem.Text + "-" + ddlSection.SelectedItem.Text + "-" + GrpName + "-"+ddlMonths.SelectedItem.Text+"');", true);  //Open New Tab for Sever side code
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
                ddlgroup.Enabled = false;
                ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
            }
            else
            {
                ddlgroup.Enabled = true;

            }
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRollNo();      
        }
        private void LoadRollNo() 
        {
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (ddlgroup.Enabled == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }            
            dlRollNo.Items.Clear();
            AbsentStudents abS = new AbsentStudents();
            abS.LoadBatchWaysRollNo(dlRollNo, ddlBatch.SelectedValue, ddlgroup.SelectedValue, ddlSection.SelectedValue, ddlShiftList.SelectedValue);    
        }

        protected void ddlShiftList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedIndex == -1 || ddlSection.SelectedValue == "0") return;
            LoadRollNo();
        }

       
    }
}