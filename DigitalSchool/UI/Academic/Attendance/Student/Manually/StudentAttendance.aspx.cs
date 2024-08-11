using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.SchoolInfo;
using DS.DAL.AdviitDAL;
using DS.BLL.Attendace;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using System.Text;
using DS.DAL;
using DS.BLL.ControlPanel;
using ComplexScriptingSystem;

namespace DS.UI.Academics.Attendance.Student.Manually
{
    public partial class StudentAttendance : System.Web.UI.Page
    {
        DataTable dt;
        SqlDataAdapter da;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    ShiftEntry.GetDropDownList(ddlShiftList);

                    BatchEntry.GetDropdownlist(ddlBatch, true); 
                 
                  //  Classes.commonTask.loadMonths(dlMonths);
                    //SheetInfoEntry.loadMonths(ddlMonths);                   
                    CheckFineSettings();

                    if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "StudentAttendance.aspx", btnProcess)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                }
            lblMessage.InnerText = "";
        }

        private void loadShiftInfo()
        {
            try
            {
                DataTable dtShiftTime = new DataTable();
                dtShiftTime = CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime from ShiftConfiguration where ConfigId =" + ddlShiftList.SelectedItem.Value + "");
                ViewState["__StartTime__"] = dtShiftTime.Rows[0]["StartTime"].ToString();

                ViewState["__CloseTime__"] = dtShiftTime.Rows[0]["CloseTime"].ToString();

                ViewState["__LateTime__"] = dtShiftTime.Rows[0]["LateTime"].ToString();
            }
            catch { }
        }

        private void generateAttendanceSheet()
        {
            try
            {

                DataTable dt = new DataTable();
                string[] dd_MM_yyyy = txtdate.Text.Split('-');
                string ClsGrpId = (divGroup.Visible) ? ddlgroup.SelectedItem.Value : "0";
                lblexecutionMsg.InnerText += "67,";
                dt =SheetInfoEntry.HasAttendanceMonthDateInDATable(ddlShiftList.SelectedItem.Value, ddlBatch.SelectedItem.Value, ClsGrpId, ddlSection.SelectedItem.Value, dd_MM_yyyy[1]+"-"+dd_MM_yyyy[2], "01");
                lblexecutionMsg.InnerText += "67["+ dt.Rows.Count+ "],";
                if (dt.Rows.Count>0)
                {

                   // dt = SheetInfoEntry.CheckAnyStudentIsExists(ddlShiftList.SelectedItem.Value, ddlBatch.SelectedItem.Value, ClsGrpId, ddlSection.SelectedItem.Value, ddlMonths.SelectedItem.Value);

                    if (SheetInfoEntry.InsertSudentInDailyAttendanceRecordTable_ForCertainDate(dt, ddlShiftList.SelectedItem.Value, ddlBatch.SelectedItem.Value, ClsGrpId, ddlSection.SelectedItem.Value, dd_MM_yyyy[1] + "-" + dd_MM_yyyy[2], "01"))
                    {
                        lblexecutionMsg.InnerText += "77,";
                        TodaysAttendanceCount();  // for count todays attendance as present by default
                        dt = new DataTable();
                        dt = SheetInfoEntry.loadAttendanceSheetByMonthYear(dd_MM_yyyy[1] + "-" + dd_MM_yyyy[2], ddlBatch.SelectedItem.Value, ddlShiftList.SelectedItem.Value, ddlSection.SelectedItem.Value, ddlgroup.SelectedItem.Value, "");
                        lblexecutionMsg.InnerText += "81,";
                        loadAttendanceSheet(dt);
                        lblexecutionMsg.InnerText += "83,";
                    }
                    
                    
                }
                else
                {
                    dt = new DataTable();
                    dt = SheetInfoEntry.CheckAnyStudentIsExists(ddlShiftList.SelectedItem.Value, ddlBatch.SelectedItem.Value, ClsGrpId, ddlSection.SelectedItem.Value, dd_MM_yyyy[1] + "-" + dd_MM_yyyy[2]);
                    lblexecutionMsg.InnerText += "92["+dt.Rows.Count+"],";
                    if (dt.Rows.Count > 0)
                    {
                        lblexecutionMsg.InnerText += "95,";
                        TodaysAttendanceCount();  // for count todays attendance as present by default
                        dt = new DataTable();
                        dt = SheetInfoEntry.loadAttendanceSheetByMonthYear(dd_MM_yyyy[1] + "-" + dd_MM_yyyy[2], ddlBatch.SelectedItem.Value, ddlShiftList.SelectedItem.Value, ddlSection.SelectedItem.Value, ddlgroup.SelectedItem.Value, "");
                        lblexecutionMsg.InnerText += "99,";
                        loadAttendanceSheet(dt);
                        lblexecutionMsg.InnerText += "101,";
                    }
                    else 
                    lblMessage.InnerText = "error->Any student are not founded in this batch";                   
                }
            }
            catch (Exception ex) { lblMessage.InnerText = "error->"+ex.Message; }
        }

        private void CheckFineSettings()
        {

            sqlDB.fillDataTable("select AbsentFineAmount,IsActive from AbsentFine where IsActive='True'", dt = new DataTable());
            ViewState["__IsActive__"] = (dt.Rows.Count > 0) ? dt.Rows[0]["IsActive"].ToString() : "False";
            ViewState["__AbsentFineAmount__"] = (dt.Rows.Count > 0) ? dt.Rows[0]["AbsentFineAmount"].ToString() : "0";
        }

        private void loadAttendanceSheet(DataTable dtStudentInf)
        {
            try
            {
                lblMessage.InnerText = "";
                AttendanceSheetTitle.InnerText = "";
                string[] dd_MM_yyyy = txtdate.Text.Split('-');
          
                DataView dv=new DataView (dtStudentInf);
                dt = dv.ToTable(false, "StudentId", "FullName", "RollNo", "AdmissionDate", "1_Code", "2_Code", "3_Code", "4_Code", "5_Code", "6_Code", "7_Code", "8_Code", "9_Code", "10_Code",
                    "11_Code", "12_Code", "13_Code", "14_Code", "15_Code", "16_Code", "17_Code", "18_Code", "19_Code", "20_Code",
                    "21_Code", "22_Code", "23_Code", "24_Code", "25_Code", "26_Code", "27_Code", "28_Code", "29_Code", "30_Code","31_Code");
                
              

                AttendanceSheetTitle.Style["Color"] = "#1fb5ad";
                AttendanceSheetTitle.InnerText = "Attendance sheet of " + ddlBatch.SelectedItem.Text + "(" + ddlSection.SelectedItem.Text + ") " + txtdate.Text + "";
                 
                
                //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";                

                DataTable dtOffdays = new DataTable();
                sqlDB.fillDataTable("select Format(OffDate,'dd') as OffDate,Purpose from OffdaySettings where Format(OffDate,'MM-yyyy')='" + dd_MM_yyyy[1] + "-" + dd_MM_yyyy[2] + "' order by OffDate ", dtOffdays);

                //for (byte b = 4; b < (DaysInMonth + 4); b++)
                //{
                //    string[] col = dt.Columns[b].ToString().Split('_');
                //    string col1 = col[0];
                //    col1 = new String(col1.Where(Char.IsNumber).ToArray());
                    tbl += "<th style='text-align:center'>Status</th>";
                //}

                string tableInfo = "";
                tableInfo = "<div class='sticky'>";
                tableInfo = "<table  id='tblStudentAttendance' class='table-hover'>";
                tableInfo += "<thead id='att-head-row' class=' table-fixed '>";
                tableInfo += "<tr>";
                tableInfo += " <th style='text-align:center'>Roll</th> <th>Name</th>" + tbl + "";
                tableInfo += "</tr>";
                tableInfo += "</thead>";
                tableInfo += "<tbody class='attbody'>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    DateTime AdDate = new DateTime(int.Parse(dt.Rows[i]["AdmissionDate"].ToString().Substring(0, 4)), int.Parse(dt.Rows[i]["AdmissionDate"].ToString().Substring(5, 2)), int.Parse(dt.Rows[i]["AdmissionDate"].ToString().Substring(8, 2)));

                    

                    int row = i + 1;
                    DataTable dtTemp = dtOffdays.Copy();
                    //for (byte b = 4; b < (DaysInMonth + 4); b++)   // this loop generate every student inputbox 
                    //{
                    //string a = ddlMonths.SelectedValue.ToString().Substring(3, 4);
                    //string aa = ddlMonths.SelectedValue.ToString().Substring(0, 2);
                    string[] d = txtdate.Text.Split('-');
                    string sDate = d[2] + "-" + d[1] + "-" + d[0];
                    //DateTime AttDate = convertDateTime.getCertainCulture(sDate);
                    DateTime AttDate = DateTime.Parse(sDate);
                       
                        bool getAdmissiStatus = (AttDate < AdDate) ? false : true;

                        string attStatus=string.Empty;
                        if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Equals("120") || dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Equals("0"))
                            attStatus = string.Empty;
                        else if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Equals("112")) attStatus = "p";
                        else if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Equals("97")) attStatus = "a";
                        else if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Equals("108")) attStatus = "l";
                        else if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Equals("226")) attStatus = "lv";

                        //string attStatus = string.Empty;
                        string sa = "", sp = "", sl = "", slv = "";
                        switch (attStatus)
                        {
                            case "a":
                                sa = "selected='selected'";
                                break;
                            case "p":
                                sp = "selected='selected'";
                                break;
                            case "l":
                                sl = "selected='selected'";
                                break;
                            case "lv":
                                slv = "selected='selected'";
                                break;
                        }
                        if (dtTemp.Rows.Count > 0)
                        {
                            bool isStatus = false;
                            for (byte x = 0; x < dtTemp.Rows.Count; x++)
                            {
                                if (int.Parse(dd_MM_yyyy[0]) == int.Parse(dtTemp.Rows[x]["OffDate"].ToString()))
                                {
                                    attStatus = (dtTemp.Rows[x]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                                    //if (!getAdmissiStatus)
                                    //    tblInputElement += "<td  style='width: 50px;'> <input disabled='disabled' AutosizeMode ='false' autocomplete='off' readonly='false' style='Height:40px; text-align:center'  tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)'  MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=''> </td>";  // this line for hilight weekly liholyday 

                                    tblInputElement += "<td  style='width: 50px;text-align:center'> <input disabled='disabled' AutosizeMode ='false' autocomplete='off' readonly='false' style='Height:40px;background-color:#980000 ;color:White; text-align:center'  tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)'  MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (txtdate.Text).ToString() + "_" + txtdate.Text.ToString().Replace('-', '_') + ":" + dt.Columns[0].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";  // this line for hilight weekly liholyday 

                                    isStatus = true;
                                    dtTemp.Rows.RemoveAt(x);
                                    break;
                                }
                            }
                           
                           

                            if (!getAdmissiStatus)
                            {
                                if (!isStatus)
                                {
                                    if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <select disabled='disabled' class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                                    //if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <input disabled='disabled' style='Height:40px;text-align:center;background-color:#F0F0F0 ;color:White;' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value='' > </td>";
                                    //else tblInputElement += "<td style='width: 50px;'> <input disabled='disabled' style='Height:40px;text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=''> </td>";
                                    else tblInputElement += "<td style=''> <select disabled='disabled' class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                                }
                            }
                            else
                            {
                                if (!isStatus)
                                {
                                   
                                    //if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <input  style='Height:40px;text-align:center;background-color:#FFFFFF ;' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + " > </td>";
                                    //else tblInputElement += "<td style='width: 50px;'> <input style='Height:40px;text-align:center;background-color:#FFFFFF;' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";
                                    if (dt.Rows[i].ItemArray[int.Parse(dd_MM_yyyy[0]) + 3].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <select class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                                    else tblInputElement += "<td style=''> <select class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                                }
                            }
                        }
                        else
                        {
                            if (!getAdmissiStatus)
                            {
                                //if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <input disabled='disabled' style='Height:40px;text-align:center;background-color:#F0F0F0'' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=''> </td>";
                                //else tblInputElement += "<td style='width: 50px;'> <input disabled='disabled' style='Height:40px;text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=''> </td>";
                                if (dt.Rows[i].ItemArray[0].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <select class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                                else tblInputElement += "<td style=''> <select class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                            }
                            else
                            {
                                //if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <input  style='Height:40px;text-align:center;background-color:#FFFFFF' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";
                                //else tblInputElement += "<td style='width: 50px;'> <input style='Height:40px;text-align:center;background-color:#FFFFFF ;' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + (b - 3).ToString() + "_" + ddlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";
                                if (dt.Rows[i].ItemArray[0].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px;'> <select class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                                else tblInputElement += "<td style=''> <select class='input controlLength' onchange='saveData(this)' id='DailyAttendanceRecord:" + ddlShiftList.SelectedItem.Value + ":" + ddlBatch.SelectedItem.Value + ":" + ddlgroup.SelectedItem.Value + ":" + ddlSection.SelectedItem.Value + ":" + dd_MM_yyyy[0] + "_" + dd_MM_yyyy[1] + "_" + dd_MM_yyyy[2].Replace('-', '_') + ":" + dd_MM_yyyy[0] + ":" + dt.Rows[i]["StudentId"] + ":" + ViewState["__IsActive__"].ToString() + ":" + ViewState["__AbsentFineAmount__"] + ":" + dt.Rows[i]["RollNo"].ToString() + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> <option " + sp + "  value='p'>Present</option><option " + sa + " value='a'>Absent</option><option " + sl + " value='l'>Late</option><option " + slv + " value='lv'>Leave</option></select> </td>";
                            
                            }
                        }
                       

                        
                        row += dt.Rows.Count;
                    //}                  
                    tableInfo += "<tr> <td style='width: 80px;text-align:center'> " + dt.Rows[i]["RollNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["FullName"].ToString() + "</td>" + tblInputElement + "</tr>";
                   
                }
                tableInfo += "</tbody>";
                tableInfo += "</table>";
                tableInfo += "</div>";
                divTable.Controls.Add(new LiteralControl(tableInfo));
                divTable.Visible = true;
                SetWeekendHolyDay();   // for set weekend and holy day 

            }
            catch
            {
                AttendanceSheetTitle.Style["Color"] = "Red";
                AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                divTable.Visible = false;
            }
        }

        private void SetWeekendHolyDay()  // for set weekend and holy day 
        {
            try
            {
                string[] getYear = txtdate.Text.Split('-');
                DataTable dtWHList = CRUD.ReturnTableNull("select Format(OffDate,'yyyy-MM-dd') as OffDate,Purpose from OffdaySettings where Month='" + getYear[0] + "' AND OffDateYear='" + getYear[1] + "'");
                bool IsActiveDays = false;
                for (byte b = 0; b < dtWHList.Rows.Count; b++)
                {                   
                    string AttStatus = (dtWHList.Rows[b]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                    string StateStatus = (dtWHList.Rows[b]["Purpose"].ToString().Equals("Weekly Holiday")) ? "Weekend" : "Holiday";
                    string GrpId = (ddlgroup.Visible) ? ddlgroup.SelectedItem.Value : "0";
                    SqlCommand cmd = new SqlCommand("Update DailyAttendanceRecord set AttStatus='" + AttStatus + "',StateStatus='" + StateStatus + "',AttManual='Manual' where ShiftId=" + ddlShiftList.SelectedItem.Value + " AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId=" + ddlSection.SelectedItem.Value + " AND  AttDate='" + dtWHList.Rows[b]["OffDate"].ToString() + "' AND ClsGrpId=" + GrpId + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();                                       
                }

                

            }
            catch { }

        }

       private void TodaysAttendanceCount()
        {
            try
            {
                DataTable dtPStatus = new DataTable();
                //------------------------Check This date is holiday or weedend-----------
                dtPStatus= CRUD.ReturnTableNull("select Format(OffDate,'yyyy-MM-dd') as OffDate,Purpose from OffdaySettings where convert(varchar(10),OffDate,105)='" + txtdate.Text+"'");
                if (dtPStatus.Rows.Count > 0) return;
                //------------------------------------------------------------------------
                
                //------------------------For Todays Attendance Count---------------------
                dtPStatus = new DataTable();
                string GrpsId = (ddlgroup.Visible) ? ddlgroup.SelectedItem.Value : "0";
                dtPStatus = CRUD.ReturnTableNull("select AttStatus from DailyAttendanceRecord where ShiftId=" + ddlShiftList.SelectedItem.Value + " AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId=" + ddlSection.SelectedItem.Value + " AND  convert(varchar(11),AttDate,105)='"+txtdate.Text+"' AND ClsGrpId=" + GrpsId + " AND AttStatus='p'");

                if (dtPStatus.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("Update DailyAttendanceRecord set AttStatus='p',StateStatus='Present',AttManual='Manual' where ShiftId=" + ddlShiftList.SelectedItem.Value + " AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId=" + ddlSection.SelectedItem.Value + " AND  convert(varchar(11),AttDate,105)='"+txtdate.Text+"' AND ClsGrpId=" + GrpsId + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                }
                //-------------------------------------------------------------------------
            }
            catch { }
        }
        
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            if (Validation() == false) return;
            loadShiftInfo();
            generateAttendanceSheet();
           // loadAttendanceSheet();
            AttendancPanel.Visible = true;
        }
        private bool Validation()
        {
            if (ddlShiftList.SelectedValue == "0")
            {
                lblMessage.InnerText = "warning->Please Select a Shift";
                ddlShiftList.Focus();
                return false;
            }
            else if (ddlBatch.SelectedValue == "0")
            {
                lblMessage.InnerText = "warning->Please Select a Batch";
                ddlBatch.Focus();
                return false;
            }
            else if (divGroup.Visible == true)
            {
                if (ddlgroup.SelectedValue == "0")
                {
                    lblMessage.InnerText = "warning->Please Select a Group";
                    ddlgroup.Focus();
                    return false;
                }
            }
            if (ddlSection.SelectedValue == "0")
            {
                lblMessage.InnerText = "warning->Please Select a Section";
                ddlSection.Focus();
                return false;
            }
            if (txtdate.Text == "")
            {
                lblMessage.InnerText = "warning->Please Select a date";
                txtdate.Focus();
                return false;
            }
            return true;
        }
        
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (Validation() == false) return;
            GenerateAttendanceSheet();
        }
        private Boolean loadAttendanceReport()
        {
            try
            {
                string batch = new String(txtdate.Text.Where(Char.IsNumber).ToArray());
             //   batch = dlClass.Text + batch;
               // string findTbl = "AttendanceSheet_" + dlClass.SelectedItem.Text + "_" + dlSection.SelectedItem.Text + "_" + dlMonths.SelectedItem.Text + "";
              //  string attendanceQuery = "SELECT CurrentStudentInfo.FullName,CurrentStudentInfo.AdmissionNo,CurrentStudentInfo.RollNo, " + findTbl + ".* " +
                      //                   "FROM CurrentStudentInfo INNER JOIN " + findTbl + " ON CurrentStudentInfo.StudentId = "
              //  + findTbl + ".StudentId where CurrentStudentInfo.BatchName='" + batch + "' and CurrentStudentInfo.Shift='" + dlShift.SelectedItem.Text + "' and "
              //  + "CurrentStudentInfo.SectionName='" + dlSection.SelectedItem.Text + "' Order by CurrentStudentInfo.RollNo ";
               // sqlDB.fillDataTable(attendanceQuery, dt = new DataTable());

                //..........................For Sheet ..............................

                int totalRows = dt.Rows.Count;
                string divInfo = "";
                string divInfoReport = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    return false;
                }
                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfoReport = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfoReport += "<thead>";
                divInfo += "<tr>";
                divInfoReport += "<tr>";
                divInfo += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfoReport += "<th style='text-align:center;  width:8%'>Roll No</th>";
                divInfo += "<th style=' width:25%'>Name</th>";
                divInfoReport += "<th style=' width:25%'>Name</th>";
                for (byte i = 4; i < dt.Columns.Count; i++)
                {
                    string[] columnname = dt.Columns[i].ToString().Split('_');
                    string val = columnname[0];
                    string col = new String(val.Where(Char.IsNumber).ToArray());
                    //string Month = new String(MonthYear.Where(Char.IsLetter).ToArray());
                    divInfo += "<th style='text-align:center; padding:05px'>" + col + "</th>";
                    divInfoReport += "<th style='text-align:center; padding:05px'>" + col + "</th>";
                }
                divInfo += "<th style='text-align:center'>TP</th>";
                divInfoReport += "<th style='text-align:center'>TP</th>";
                divInfo += "<th style='text-align:center'>TA</th>";
                divInfoReport += "<th style='text-align:center'>TA</th>";
                divInfo += "<th style='text-align:center'>View</th>";
                divInfo += "</tr>";
                divInfoReport += "</tr>";
                divInfo += "</thead>";
                divInfoReport += "</thead>";
                divInfo += "<tbody>";
                divInfoReport += "<tbody>";
                string id = "";
                int TP = 0;
                int TA = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfoReport += "<tr id='r_" + id + "'>";
                    for (byte k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k == 0)
                        {
                            divInfo += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfoReport += "<td style='width:13%; text-align:center' >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                            divInfoReport += "<td style='width:13%' >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                        }
                        else
                        {
                            if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "a")
                            {
                                TA += 1;
                                divInfo += "<td style='width:3%; color:red ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3%; color:red ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            }
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "p")
                            {
                                TP += 1;
                                divInfo += "<td style='width:3% ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            }
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "w" || dt.Rows[x][dt.Columns[k].ToString()].ToString() == "h")
                            {
                                divInfo += "<td style='width:3% ; text-align:center ; padding-left:0px; border-bottom:0px solid white' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center ; padding-left:0px; border-bottom:0px solid white' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                            }
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "")
                            {
                                divInfo += "<td style='width:3% ; text-align:center;' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                                divInfoReport += "<td style='width:3% ; text-align:center;' >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            }
                        }
                        if (dt.Columns.Count - 1 == k)
                        {
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green; padding-left:0px' >" + TP + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green; padding-left:0px' >" + TP + "</td>";
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red; padding-left:0px ' >" + TA + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red; padding-left:0px ' >" + TA + "</td>";
                            divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewStudent(" + id + ");'  />";
                        }
                    }
                    TA = 0;
                    TP = 0;
                }
                divInfo += "</tbody>";
                divInfoReport += "</tbody>";
                divInfo += "<tfoot>";
                divInfoReport += "<tfoot>";
                divInfo += "</table>";
                divInfoReport += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divInfoReport += "<div class='dataTables_wrapper'><div class='head'></div></div>";
              //  Session["__AttendanceDetails__"] = divInfoReport;
                //Session["__ReportType__"] = "Attendance Sheet at " + dlMonths.SelectedItem.Text;
               // Session["__Month__"] = "Attendance Info at " + dlMonths.SelectedItem.Text;
               // Session["__BatchAttendance__"] = batch;
             //   Session["__ShiftAttendance__"] = dlShift.SelectedItem.Text;
              //  Session["__SectionAttendance__"] = dlSection.SelectedItem.Text;
             //   Session["__MonthNameAttendance__"] = dlMonths.SelectedItem.Text;
                Session["__dataTableDateRange__"] = null;
                return true;
            }
            catch { return false; }
        }
        private void LoadDailyAttendanceReportData(string reportType)
        {
            //-----------Validation--------------
            if (ddlShiftList.SelectedValue == "0")
            { lblMessage.InnerText = "warning-> Please select a Shift!"; return; }
            if (ddlBatch.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Batch!"; return; }
            if (divGroup.Visible == true && ddlgroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Group!"; return; }
            if (ddlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Section!"; return; }
            //--------------------------------------------------
            string sqlCmd = "";
            string ReportTitel = "", ReportType = "";
            DataTable dt = new DataTable();
            if (reportType == "attendance") // Daily Attendance Status
            {
                sqlCmd = "select FullName,RollNo,AttStatus,convert(varchar(11),Attdate,105) as StateStatus,inHur,InMin,OutHur,OutMin from v_DailyAttendanceRecordForReport where ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and convert(varchar(11),Attdate,105)='" +txtdate.Text + "'";
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (reportType == "present") // Daily Present status
            {
                sqlCmd = "select FullName,RollNo,AttStatus,convert(varchar(11),Attdate,105) as StateStatus,inHur,InMin,OutHur,OutMin from v_DailyAttendanceRecordForReport where AttStatus='p' and  ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and convert(varchar(11),Attdate,105)='" + txtdate.Text + "'";
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
            }
            else if (reportType == "absent") // Daily Absent Staust
            {
                sqlCmd = "select FullName,RollNo,AttStatus,convert(varchar(11),Attdate,105) as StateStatus,inHur,InMin,OutHur,OutMin from v_DailyAttendanceRecordForReport where AttStatus='a' and  ShiftId='" + ddlShiftList.SelectedValue + "'and BatchId='" + ddlBatch.SelectedValue + "' and ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and convert(varchar(11),Attdate,105)='" + txtdate.Text + "'";
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
            }

            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning->Any "+reportType+" record are not founded";
                return;
            }          
            Session["__DailyAttendance__"] = dt;
            ViewState["__ReportTitle__"] = ReportTitel;
            ViewState["__ReportType__"] = ReportType;
            ViewState["__Report__"] = "";
            string GroupName = (ddlgroup.SelectedValue == "0") ? "" : " Group : " + ddlgroup.SelectedItem.Text + ", ";           
               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyAttendance-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + GroupName + "-" + ddlSection.SelectedItem.Text + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + ViewState["__ReportTitle__"].ToString() + "-" + ViewState["__ReportType__"].ToString() + "');", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyAttendance-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + GroupName + "-" + ddlSection.SelectedItem.Text + "-" + txtdate.Text.Replace('-', '/') + "-" + ReportTitel + "-" + ReportType + "');", true);
        }
        private void GenerateAttendanceSheet()
        {
            string[] dd_MM_yyyy = txtdate.Text.Split('-');
           
            
            //--------------------------------------------------
            DataTable dt = new DataTable();
            string SqlCmd = "SELECT   FullName, RollNo, SUM(CASE DATEPART(day, AttDate) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDate) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDate) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDate) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDate) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDate) WHEN 9 THEN code ELSE 0 END) AS [9], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDate) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDate) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDate) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDate) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDate) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDate) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDate) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDate) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDate) WHEN 24 THEN code ELSE 0 END) AS [24], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDate) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDate) " +
                        " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDate) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDate) WHEN 29 THEN code ELSE 0 END) AS [29], " +
                        " SUM(CASE DATEPART(day, AttDate) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDate) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                        " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                        " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV " +
                        " FROM            dbo.v_DailyAttendanceRecordForReport" +
                        " where ShiftId='" + ddlShiftList.SelectedValue + "' and BatchId='" + ddlBatch.SelectedValue + "' and " +
                        " ClsGrpId='" + ddlgroup.SelectedValue + "' and ClsSecId='" + ddlSection.SelectedValue + "' and right(CONVERT(varchar(11),AttDate,105),7)='" + dd_MM_yyyy[1]+"-"+dd_MM_yyyy[2] + "'" +
                        " GROUP BY StudentId, FullName, RollNo";
            sqlDB.fillDataTable(SqlCmd, dt);
            if (dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                return;
            }
            string Group = (ddlgroup.SelectedValue == "0") ? "" : "Group : " + ddlgroup.SelectedItem.Text + ",";
            Session["__AttendanceSheet__"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AttendanceSheet-" + ddlShiftList.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + Group + "-" + ddlSection.SelectedItem.Text + "-" + txtdate.Text + "');", true);
            //Open New Tab for Sever side code  
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {


            ddlSection.Items.Clear();
            BatchEntry.loadGroupByBatchId(ddlgroup, ddlBatch.SelectedValue.ToString());

            if (ddlgroup.Items.Count == 1)
            {
                divGroup.Visible = false;                
               // ClassSectionEntry.GetSectionListByBatchId(ddlSection, ddlBatch.SelectedValue.ToString());
                ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(), ddlgroup.SelectedItem.Value);
            }
            else
            {
                ddlgroup.Enabled = true;
                divGroup.Visible = true;              
                
            }
            
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSection, ddlBatch.SelectedValue.ToString(),ddlgroup.SelectedItem.Value);
        }
        protected void btnTodayAttendanceList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("attendance");
        }
        protected void btnTodayPresentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("present");
        }

        protected void btnTodayAbsentList_Click(object sender, EventArgs e)
        {
            LoadDailyAttendanceReportData("absent");
        }
    }
}