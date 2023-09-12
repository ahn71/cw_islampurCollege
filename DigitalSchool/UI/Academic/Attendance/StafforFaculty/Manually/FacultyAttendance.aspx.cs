using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using DS.BLL.Attendace;
using DS.BLL.GeneralSettings;
using DS.DAL;
using DS.BLL.ControlPanel;

namespace DS.UI.Academics.Attendance.StafforFaculty.Manually
{
    public partial class FacultyAttendance : System.Web.UI.Page
    {
        DataTable dt;
        SqlDataAdapter da;
        string sqlCmd = "";
        string ReportTitel = "";
        string ReportType = "";
        string DepartmentList = "";
        string DesignationList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    Classes.commonTask.LoadDeprtmentAtttedence(dlDepartments);
                    ShiftEntry.GetDropDownList(ddlShift);
                    SheetInfoEntry.loadMonths(dlMonths);
                    Classes.commonTask.LoadDesignation(dlDesignation);

                    if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "FacultyAttendance.aspx", btnProcess)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                }
        }

        private void loadShiftInfo()
        {
            try
            {
                DataTable dtShiftTime = new DataTable();
               dtShiftTime= CRUD.ReturnTableNull("select StartTime,CloseTime,LateTime from ShiftConfiguration where ConfigId =" + ddlShift.SelectedItem.Value + "");
                ViewState["__StartTime__"] = dtShiftTime.Rows[0]["StartTime"].ToString();

                ViewState["__CloseTime__"] = dtShiftTime.Rows[0]["CloseTime"].ToString();

                ViewState["__LateTime__"] = dtShiftTime.Rows[0]["LateTime"].ToString();
            }
            catch { }
        }

        private void getDepartmentsList()
        {
            try
            {
                dlDepartments.Items.Clear();

                SQLOperation.selectBySetCommandInDatatable("Select DName,DId from Departments_HR   Order by DName ", dt = new DataTable(), DbConnection.Connection);
                dlDepartments.DataValueField = "DId";
                dlDepartments.DataTextField = "DName";
                dlDepartments.DataSource = dt;
                dlDepartments.DataBind();
                dlDepartments.Items.Insert(0, new ListItem("...Select...", "00"));
             //   dlDepartments.Items.Add("All");
               // dlDepartments.SelectedIndex = dlDepartments.Items.Count;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            //............validation..............................
            if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning->Please select a Shift!"; ddlShift.Focus(); return; }
            if (dlDepartments.SelectedValue == "00") { lblMessage.InnerText = "warning->Please select a Department!"; dlDepartments.Focus(); return; }           
            if (dlMonths.SelectedValue == "0") { lblMessage.InnerText = "warning->Please select a Month!"; dlMonths.Focus(); return; }
            //..........................................................
            lblMessage.InnerText = "";
            loadShiftInfo();
            AttendanceSheetGenerate();
        }

        private void AttendanceSheetGenerate()
        {
            try
            {
                DataTable dt=new DataTable ();

                ViewState["__DptIdList__"]=EmpSheetInfoEntry.GetDepartmentIdList(dlDepartments);
                ViewState["__DsgIdList__"]=EmpSheetInfoEntry.GetDesignationIdList(dlDesignation);
                dt = EmpSheetInfoEntry.HasAttendanceMonthDateInDATable(ddlShift.SelectedItem.Value, ViewState["__DptIdList__"].ToString(), ViewState["__DsgIdList__"].ToString(), dlMonths.SelectedItem.Value);
                if (dt.Rows.Count > 0)
                {


                    if (EmpSheetInfoEntry.InsertEmployeeInDailyAttendanceRecordTable_ForCertainDate(dt,dlMonths.SelectedItem.Value,ddlShift.SelectedItem.Value))
                    {
                        dt = new DataTable();
                        dt = EmpSheetInfoEntry.getEmpAttendanceSheet(ddlShift.SelectedItem.Value, dlMonths.SelectedItem.Value, ViewState["__DptIdList__"].ToString());
                        loadAttendanceSheet(dt);
                    }
                    
                }
                else
                {

                    dt = new DataTable();
                    dt = EmpSheetInfoEntry.HasAnyStaffRecord(dlDepartments.SelectedItem.Value, dlMonths.SelectedItem.Value, ViewState["__DptIdList__"].ToString(), ViewState["__DsgIdList__"].ToString(),ddlShift.SelectedItem.Value);
                    if (dt.Rows.Count > 0)
                    {
                        dt = EmpSheetInfoEntry.getEmpAttendanceSheet(ddlShift.SelectedItem.Value, dlMonths.SelectedItem.Value, ViewState["__DptIdList__"].ToString());
                        loadAttendanceSheet(dt);
                    }
                    else lblMessage.InnerText = "error->Any staff or faculty  are not founded in this organization";
                }

            }
            catch { }
        }


        private void loadAttendanceSheet(DataTable dtStudentInf)
        {
            try
            {
                lblMessage.InnerText = "";
                AttendanceSheetTitle.InnerText = "";

                DataView dv = new DataView(dtStudentInf);
                dt = dv.ToTable(false, "EId","ECardNo", "EName", "DId","EJoiningDate", "1_Code", "2_Code", "3_Code", "4_Code", "5_Code", "6_Code", "7_Code", "8_Code", "9_Code", "10_Code",
                    "11_Code", "12_Code", "13_Code", "14_Code", "15_Code", "16_Code", "17_Code", "18_Code", "19_Code", "20_Code",
                    "21_Code", "22_Code", "23_Code", "24_Code", "25_Code", "26_Code", "27_Code", "28_Code", "29_Code", "30_Code", "31_Code");

                AttendanceSheetTitle.Style["Color"] = "#1fb5ad";
                AttendanceSheetTitle.InnerText = "Attendance sheet of Faculty and staff " + dlMonths.Text + "";


                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";
                int DaysInMonth = DateTime.DaysInMonth(int.Parse(dlMonths.SelectedItem.Value.Substring(3, 4)), int.Parse(dlMonths.SelectedItem.Value.Substring(0, 2)));

                DataTable dtOffdays = new DataTable();
                sqlDB.fillDataTable("select Format(OffDate,'dd') as OffDate,Purpose,Format(OffDate,'yyyy-MM-dd') as OffDates from OffdaySettings where Format(OffDate,'MM-yyyy')='" + dlMonths.SelectedItem.Value + "' order by OffDate ", dtOffdays);

                for (byte b = 5; b < (DaysInMonth + 5); b++)
                {
                    string[] col = dt.Columns[b].ToString().Split('_');
                    string col1 = col[0];
                    col1 = new String(col1.Where(Char.IsNumber).ToArray());
                    tbl += "<th style='width: 76px;text-align:center'>" + col1 + "</th>";
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px; text-align:center'>CardNo</th> <th style='width: 280px'>Name</th>" + tbl + "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i + 1;
                    DataTable dtTemp = dtOffdays.Copy();
                    DateTime JDate = new DateTime(int.Parse(dt.Rows[i]["EJoiningDate"].ToString().Substring(0, 4)), int.Parse(dt.Rows[i]["EJoiningDate"].ToString().Substring(5, 2)), int.Parse(dt.Rows[i]["EJoiningDate"].ToString().Substring(8, 2)));

                    for (byte b = 5; b < (DaysInMonth + 5); b++)   // this loop generate every student inputbox 
                    {
                        
                        DateTime AttDate = new DateTime(int.Parse(dlMonths.SelectedValue.ToString().Substring(3, 4)),int.Parse(dlMonths.SelectedValue.ToString().Substring(0,2)),b-4);
                      
                        bool getJoinStatus =(AttDate<JDate)?false :true;

                        string attStatus = string.Empty;
                        if (dt.Rows[i].ItemArray[b].ToString().Equals("120") || dt.Rows[i].ItemArray[b].ToString().Equals("0"))
                            attStatus = string.Empty;
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("112")) attStatus = "p";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("97")) attStatus = "a";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("108")) attStatus = "l";
                        else if (dt.Rows[i].ItemArray[b].ToString().Equals("226")) attStatus = "v";
                        //string attStatus = string.Empty;
                        if (dtTemp.Rows.Count > 0)
                        {


                            bool isStatus = false;
                            for (byte x = 0; x < dtTemp.Rows.Count; x++)
                            {
                                if (int.Parse((b - 4).ToString()) == int.Parse(dtTemp.Rows[x]["OffDate"].ToString()))
                                {
                                    if (attStatus == "v")
                                    {
                                        if (!getJoinStatus)
                                            tblInputElement += "<td style='width: 50px'> <input disabled='disabled'  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value='' > </td>";
                                        else tblInputElement += "<td style='width: 50px'> <input  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + " > </td>";
                                        isStatus = true;
                                        dtTemp.Rows.RemoveAt(x);
                                        break;
                                    }
                                    else
                                    {
                                        DataTable dtLeaveisExists = new DataTable();
                                        sqlDB.fillDataTable("select EId From Leave_Application_Details where EId=" + dt.Rows[i]["EId"].ToString() + " AND LeaveDate='" + dtTemp.Rows[x]["OffDates"].ToString() + "'", dtLeaveisExists);
                                        if (dtLeaveisExists.Rows.Count > 0)
                                        {
                                            if (!getJoinStatus)
                                                tblInputElement += "<td style='width: 50px'> <input disabled='disabled' style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value='' > </td>";
                                            else
                                                tblInputElement += "<td style='width: 50px'> <input  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + " > </td>";

                                            isStatus = true;
                                            dtTemp.Rows.RemoveAt(x);
                                            break;
                                        }

                                        attStatus = (dtTemp.Rows[x]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";

                                        //if (!getJoinStatus)
                                        //    tblInputElement += "<td  style='width: 50px'> <input disabled='disabled' AutosizeMode ='false'  autocomplete='off' readonly='false' color:White; text-align:center'  tabindex=" + row + " onchange='saveData(this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=''> </td>";  // this line for hilight weekly liholyday 
                                        //else
                                            tblInputElement += "<td disabled='disabled' style='width: 50px'> <input AutosizeMode ='false'  autocomplete='off' readonly='false' style='background-color:#980000 ;color:White; text-align:center'  tabindex=" + row + " onchange='saveData(this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + "> </td>";  // this line for hilight weekly liholyday 
                                        isStatus = true;
                                        dtTemp.Rows.RemoveAt(x);
                                        break;
                                    }
                                }
                            }

                            if (!isStatus)
                            {
                                if (!getJoinStatus)
                                {
                                    if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td  style='width: 50px'> <input disabled='disabled'  style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value='' > </td>";
                                    else tblInputElement += "<td style='width: 50px'> <input disabled='disabled' style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + "' value=''> </td>";
                                }
                                else
                                {
                                    if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td  style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text'  id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + dlDepartments.SelectedItem.Value + ":" + ddlShift.SelectedItem.Value + ":" + ViewState["__StartTime__"].ToString() + ":" + ViewState["__CloseTime__"].ToString() + ":" + ViewState["__LateTime__"].ToString() + "' value=" + attStatus + " > </td>";
                                    else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + "' value=" + attStatus + "> </td>";
                                }
                            }
                        }
                        else
                        {
                            if (!getJoinStatus)
                            {
                                if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td disabled='disabled' style='width: 50px'> <input disabled='disabled' style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                                else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                            }
                            else
                            {
                                if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                                else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' onkeydown='return acceptValidCharacter(event, this)' MaxLength='1' type='text' id='DailyAttendanceRecord:" + (b - 4).ToString() + "_" + dlMonths.SelectedValue.ToString().Replace('-', '_') + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + "' value=" + attStatus + "> </td>";
                            }
                        }

                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px;text-align:center'> " + dt.Rows[i]["ECardNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["EName"].ToString() + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divTable.Controls.Add(new LiteralControl(tableInfo));
                divTable.Visible = true;
                SetWeekendHolyDay();
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
                string[] getYear = dlMonths.SelectedItem.Text.Split('-');
                DataTable dtWHList = CRUD.ReturnTableNull("select Format(OffDate,'yyyy-MM-dd') as OffDate,Purpose from OffdaySettings where Month='" + getYear[0] + "' AND OffDateYear='" + getYear[1] + "'");
                for (byte b = 0; b < dtWHList.Rows.Count; b++)
                {
                    string AttStatus = (dtWHList.Rows[b]["Purpose"].ToString().Equals("Weekly Holiday")) ? "w" : "h";
                    string StateStatus = (dtWHList.Rows[b]["Purpose"].ToString().Equals("Weekly Holiday")) ? "Weekend" : "Holiday";
                    
                    SqlCommand cmd = new SqlCommand("Update DailyAttendanceRecord set AttStatus='" + AttStatus + "',StateStatus='" + StateStatus + "',AttManual='Manual' where ShiftId=" + ddlShift.SelectedItem.Value + " AND AttDate='" + dtWHList.Rows[b]["OffDate"].ToString() + "'", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }

        }

        private void loadAttendanceSheet()
        {
            try
            {
                AttendanceSheetTitle.InnerText = "";
                string batch = new String(dlMonths.Text.Where(Char.IsNumber).ToArray());

                dt = new DataTable();
                da = new SqlDataAdapter();
                da = new SqlDataAdapter("SELECT Departments_HR.DName,EmployeeInfo.EName, EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim()
                    + ".* FROM EmployeeInfo INNER JOIN Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + " ON EmployeeInfo.EId = Faculty_Staff_AttendanceSheet_"
                    + dlMonths.Text.Trim() + ".EId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId", DbConnection.Connection);
                da.Fill(dt = new DataTable());
                AttendanceSheetTitle.Style["Color"] = "green";
                AttendanceSheetTitle.InnerText = "Attendance sheet of Faculty and staff " + dlMonths.Text + "";

                if (dlDepartments.SelectedItem.Text != "All") dt = dt.Select("DName='" + dlDepartments.SelectedItem.Text.Trim() + "'").CopyToDataTable();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(dt.Rows.Count));
                string tbl = "";
                string tblInputElement = "";

                for (byte b = 4; b < dt.Columns.Count; b++)
                {
                    tbl += "<th style='width: 76px'>" + dt.Columns[b].ToString() + "</th>";
                }

                string tableInfo = "";
                tableInfo = "<table id='tblStudentAttendance'   >";
                tableInfo += " <th style='width: 70px'>Card No</th> <th style='width: 280px'>Name</th>" + tbl + "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tblInputElement = "";
                    int row = i + 1;

                    for (byte b = 4; b < dt.Columns.Count; b++)   // this loop generate every employee inputbox 
                    {
                        if (dt.Rows[i].ItemArray[b].ToString().Equals("w") || dt.Rows[i].ItemArray[b].ToString().Equals("h")) tblInputElement
                            += "<td  style='width: 50px'> <input autocomplete='off' style='background-color:red;text-align:center' readonly='true' tabindex=" + row
                            + " onchange='saveData(this)' type=text id='Faculty_Staff_AttendanceSheet_" + "_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString()
                            + ":" + dt.Rows[i]["EId"] + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";  // this line for hilight weekly liholyday 
                        else
                        {
                            if (dt.Rows[i].ItemArray[b].ToString().Trim().Length >= 1) tblInputElement += "<td style='width: 50px'> <input readonly='true' "
                            + "style='text-align:center' autocomplete='off' tabindex=" + row + " onchange='saveData(this)' type=text id='Faculty_Staff_AttendanceSheet_"
                            + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString() + ":" + dt.Rows[i]["EId"] + ":" + batch + "' value="
                            + dt.Rows[i].ItemArray[b].ToString() + "> </td>";
                            else tblInputElement += "<td style='width: 50px'> <input style='text-align:center' autocomplete='off' tabindex=" + row
                                + " onchange='saveData(this)' type=text id='Faculty_Staff_AttendanceSheet_" + dlMonths.Text.Trim() + ":" + dt.Columns[b].ToString()
                                + ":" + dt.Rows[i]["EId"] + ":" + batch + "' value=" + dt.Rows[i].ItemArray[b].ToString() + "> </td>";
                        }
                        row += dt.Rows.Count;
                    }
                    tableInfo += "<tr> <td style='width: 80px'> " + dt.Rows[i]["ECardNo"].ToString() + "</td>  <td style='width: 60px'>" + dt.Rows[i]["EName"].ToString()
                        + "</td>" + tblInputElement + "</tr>";
                }
                tableInfo += "</table>";
                divTable.Controls.Add(new LiteralControl(tableInfo));
                divTable.Visible = true;
            }
            catch
            {
                AttendanceSheetTitle.Style["Color"] = "Red";
                AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                divTable.Visible = false;
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            AttendanceSheetTitle.InnerText = "";
            lblMessage.InnerText = "";
            if (reportGenerateForFiltering() == true)
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/FacultyStaffAttendanceReport.aspx');", true);  //Open New Tab for Sever side code
            else
            {
                // AttendanceSheetTitle.Style["Color"] = "Red";
                lblMessage.InnerText = "warning->Sorry this attendance sheet is not created";
                //  divTable.Visible = false;
            }
        }
        private Boolean reportGenerateForFiltering()
        {
            try
            {
                DataTable dt = new DataTable();
                //dt = (DataTable)Session["__dt__"];
                if (dlDepartments.SelectedItem.Text == "All") sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_"
                    + dlMonths.SelectedItem.Text + ".*, Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text
                    + " INNER JOIN EmployeeInfo ON Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text + ".EID = EmployeeInfo.EID INNER JOIN Designations "
                + "ON EmployeeInfo.DesId = dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId ", dt);
                else
                    sqlDB.fillDataTable("SELECT EmployeeInfo.EName,EmployeeInfo.ECardNo,Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text
                        + ".*, Departments_HR.DName, Designations.DesName FROM Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text + " INNER JOIN EmployeeInfo ON "
                    + "Faculty_Staff_AttendanceSheet_" + dlMonths.SelectedItem.Text + ".EID = EmployeeInfo.EID INNER JOIN Designations ON EmployeeInfo.DesId = "
                    + "dbo.Designations.DesId INNER JOIN Departments_HR ON EmployeeInfo.DId = Departments_HR.DId where Departments_HR.DName='" + dlDepartments.SelectedItem.Text + "'", dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                string divInfoReport = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Student available</div>";
                    divInfo += "<div><div class='head'></div></div>";
                    AttendanceSheetTitle.InnerText = "Sorry this attendance sheet is not created";
                    return false;
                }


                divInfo = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfoReport = " <table id='tblStudentList' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfoReport += "<thead>";
                divInfo += "<tr>";
                divInfoReport += "<tr>";
                divInfo += "<th>Teacher Name</th>";
                divInfoReport += "<th>Teacher Name</th>";
                divInfo += "<th>Card No</th>";
                divInfoReport += "<th>Card No</th>";

                for (byte i = 3; i < dt.Columns.Count - 2; i++)
                {

                    string[] columnname = dt.Columns[i].ToString().Split('_');
                    string val = columnname[0];
                    string col = new String(val.Where(Char.IsNumber).ToArray());

                    divInfo += "<th style='text-align:center'>" + col + "</th>";
                    divInfoReport += "<th style='text-align:center'>" + col + "</th>";
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
                    id = dt.Rows[x]["EID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfoReport += "<tr id='r_" + id + "'>";
                    for (byte k = 0; k < dt.Columns.Count - 2; k++)
                    {
                        if (k == 2)
                        {
                            divInfo += "<td style='width:3%;display:none'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            divInfoReport += "<td style='width:3%;display:none'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                        }
                        else if (k > 0)
                        {
                            if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "a") TA += 1;
                            else if (dt.Rows[x][dt.Columns[k].ToString()].ToString() == "p") TP += 1;

                            divInfo += "<td style='width: 3%; text-align: center;'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                            divInfoReport += "<td style='width: 3%; text-align: center;'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString().ToLower() + "</td>";
                        }
                        else
                        {
                            divInfo += "<td style='width: 3%'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                            divInfoReport += "<td style='width: 3%'  >" + dt.Rows[x][dt.Columns[k].ToString()].ToString() + "</td>";
                        }

                        if (dt.Columns.Count - 3 == k)
                        {
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green' >" + TP + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:green' >" + TP + "</td>";
                            divInfo += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red' >" + TA + "</td>";
                            divInfoReport += "<td style='width:15% ; text-align:center ; font-weight:bold ; color:red' >" + TA + "</td>";
                            divInfo += "<td style='width:3%;' class='numeric control' >" + "<img src='/Images/gridImages/view.png' onclick='viewEmployee(" + id + ");'  />";
                        }
                    }
                    TP = 0;
                    TA = 0;
                }

                divInfo += "</tbody>";
                divInfoReport += "</tbody>";
                divInfo += "<tfoot>";
                divInfoReport += "<tfoot>";

                divInfo += "</table>";
                divInfoReport += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divInfoReport += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                // divMonthWiseAttendaceSheet.Controls.Add(new LiteralControl(divInfo));
                Session["__FacultyReport__"] = divInfoReport;
                Session["__ReportType__"] = "Attendance Sheet at " + dlMonths.SelectedItem.Text;

                Session["__Department__"] = "Department : " + dlDepartments.SelectedItem.Text;
                Session["Designation"] = "Designation : " + "All";
                Session["__MonthName__"] = dlMonths.SelectedItem.Text;
                return true;
            }
            catch { return false; }
        }

        protected void btnPreview_Click1(object sender, EventArgs e)
        {           
           GeneratMonthlyEmpAttendanceSheet();
        }
        private void GeneratMonthlyEmpAttendanceSheet()
        {
            //----------validation-------------------------------
            if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShift.Focus(); return; }
            if (dlMonths.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Month!"; dlMonths.Focus(); return; }
            //-----------------------------------------------------

            if (dlDepartments.SelectedItem.Text == "All") DepartmentList = GetAlllist(dlDepartments);
            else DepartmentList = dlDepartments.SelectedValue;
            if (dlDesignation.SelectedItem.Text == "All") DesignationList = GetAlllist(dlDesignation);
            else DepartmentList = dlDesignation.SelectedValue;
            string shift = (ddlShift.SelectedValue == "0") ? "" : " and ShiftId='" + ddlShift.SelectedValue + "'";
            sqlCmd = " SELECT  EName as FullName, ECardNo as RollNo,ShiftName,ShiftId,  SUM(CASE DATEPART(day, AttDates) WHEN 1 THEN code ELSE 0 END) AS [1], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 2 THEN code ELSE 0 END) AS [2], SUM(CASE DATEPART(day, AttDates) WHEN 3 THEN code ELSE 0 END) AS [3], SUM(CASE DATEPART(day, AttDates) WHEN 4 THEN code ELSE 0 END) AS [4], " +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 5 THEN code ELSE 0 END) AS [5], SUM(CASE DATEPART(day, AttDates) WHEN 6 THEN code ELSE 0 END) AS [6], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 7 THEN code ELSE 0 END) AS [7], SUM(CASE DATEPART(day, AttDates) WHEN 8 THEN code ELSE 0 END) AS [8], SUM(CASE DATEPART(day, AttDates) WHEN 9 THEN code ELSE 0 END) AS [9]," +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 10 THEN code ELSE 0 END) AS [10], SUM(CASE DATEPART(day, AttDates) WHEN 11 THEN code ELSE 0 END) AS [11], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 12 THEN code ELSE 0 END) AS [12], SUM(CASE DATEPART(day, AttDates) WHEN 13 THEN code ELSE 0 END) AS [13], SUM(CASE DATEPART(day, AttDates) WHEN 14 THEN code ELSE 0 END) AS [14], " +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 15 THEN code ELSE 0 END) AS [15], SUM(CASE DATEPART(day, AttDates) WHEN 16 THEN code ELSE 0 END) AS [16], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 17 THEN code ELSE 0 END) AS [17], SUM(CASE DATEPART(day, AttDates) WHEN 18 THEN code ELSE 0 END) AS [18], SUM(CASE DATEPART(day, AttDates) WHEN 19 THEN code ELSE 0 END) AS [19], " +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 20 THEN code ELSE 0 END) AS [20], SUM(CASE DATEPART(day, AttDates) WHEN 21 THEN code ELSE 0 END) AS [21], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 22 THEN code ELSE 0 END) AS [22], SUM(CASE DATEPART(day, AttDates) WHEN 23 THEN code ELSE 0 END) AS [23], SUM(CASE DATEPART(day, AttDates) WHEN 24 THEN code ELSE 0 END) AS [24]," +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 25 THEN code ELSE 0 END) AS [25], SUM(CASE DATEPART(day, AttDates) WHEN 26 THEN code ELSE 0 END) AS [26], SUM(CASE DATEPART(day, AttDates) " +
                        " WHEN 27 THEN code ELSE 0 END) AS [27], SUM(CASE DATEPART(day, AttDates) WHEN 28 THEN code ELSE 0 END) AS [28], SUM(CASE DATEPART(day, AttDates) WHEN 29 THEN code ELSE 0 END) AS [29]," +
                        " SUM(CASE DATEPART(day, AttDates) WHEN 30 THEN code ELSE 0 END) AS [30], SUM(CASE DATEPART(day, AttDates) WHEN 31 THEN code ELSE 0 END) AS [31], SUM(CASE Code WHEN 112 THEN 1 ELSE 0 END) " +
                        " AS P, SUM(CASE Code WHEN 97 THEN 1 ELSE 0 END) AS A, SUM(CASE Code WHEN 104 THEN 1 ELSE 0 END) AS H, SUM(CASE Code WHEN 119 THEN 1 ELSE 0 END) AS W, " +
                        " SUM(CASE Code WHEN 226 THEN 1 ELSE 0 END) AS LV, DName as BatchId " +
                        " FROM dbo.v_DailyEmployeeAttendanceRecord " +
                        " where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") "+shift+" and FORMAT(AttDates,'MM-yyyy')='" + dlMonths.SelectedValue + "'" +
                        " GROUP BY EName,ECardNo,DName,ShiftName,ShiftId";
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt == null || dt.Rows.Count < 1)
            {
                lblMessage.InnerText = "warning-> Any attendance Record are not founded!";
                return;
            }
            Session["__EmpAttendanceSheet__"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EmpAttendanceSheet-" + dlMonths.SelectedItem.Text + "-" + ddlShift.SelectedItem.Text + "-" + "Staff and Faculty" + "');", true);
            //Open New Tab for Sever side code  
        }
        private string GetAlllist(DropDownList ddlSftList)
        {
            try
            {
                string setPredicate = "";
                for (byte b = 0; b < ddlSftList.Items.Count; b++)
                {
                    setPredicate += ddlSftList.Items[b].Value.ToString() + ",";
                }

                setPredicate = setPredicate.Remove(setPredicate.LastIndexOf(','));
                return setPredicate;
            }
            catch { return " "; }

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
        private void LoadDailyAttendanceReportData(string reportType)
        {
            //-----------Validation--------------           
            if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select a Shift!"; ddlShift.Focus(); return; }
        
            
            DataTable dt = new DataTable();


            DepartmentList = (dlDepartments.SelectedItem.Text == "All") ? GetAlllist(dlDepartments) : dlDepartments.SelectedValue;
            DesignationList = (dlDesignation.SelectedItem.Text == "All") ? GetAlllist(dlDesignation) : dlDesignation.SelectedValue;
            if (reportType == "attendance") // Daily Attendance Status
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShift.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Attendance Status";
                ReportType = "Status";
            }
            else if (reportType == "present") // Daily Present status
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='p' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShift.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Present Status";
                ReportType = "PresentAbsent";
            }
            else if (reportType == "absent") // Daily Absent Staust
            {
                sqlCmd = "select ECardNo,EName,DName,DesName,ShiftName,ShiftId,AttStatus,Format(AttDates,'dd-MM-yyyy') as AttDates,Format(AttDates,'MM-yyyy') as AttMonth from v_DailyEmployeeAttendanceRecord where  AttStatus='a' and DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") and ShiftId='" + ddlShift.SelectedValue + "' and Format (AttDates,'dd-MM-yyyy')='" + DateTime.Now.ToString("dd-MM-yyyy") + "'";
                ReportTitel = "Daily Absent Status";
                ReportType = "PresentAbsent";
            }

            //sqlDB.fillDataTable(sqlCmd, dt = new DataTable());
            dt = CRUD.ReturnTableNull(sqlCmd);
            if (dt.Rows.Count < 1)
            {
               lblMessage.InnerText = "warning->Any " + reportType + " record are not founded";
               
                return;
            }          
            Session["__DailyEmpAttendance__"] = dt;
            ViewState["__ReportTitle__"] = ReportTitel;
            ViewState["__ReportType__"] = ReportType;
            ViewState["__Report__"] = "";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ViewState["__ReportTitle__"].ToString() + "-" + ViewState["__ReportType__"].ToString() + "-" + ddlShift.SelectedItem.Text + "-" + "Staff and Fatulty" + "');", true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DailyEmpAttendance-" + ReportTitel + "-" + ReportType + "-" + ddlShift.SelectedItem.Text + "-" + "Staff and Fatulty" + "-" +DateTime.Now.ToString("dd/MM/yyyy")+ "-No');", true);
        }
    }
}