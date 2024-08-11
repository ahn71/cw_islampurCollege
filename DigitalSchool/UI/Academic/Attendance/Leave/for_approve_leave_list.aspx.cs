
using ComplexScriptingSystem;
using DS.BLL.ControlPanel;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL;
using DS.BLL.Attendace;

namespace DS.UI.Academics.Attendance.Leave
{
    public partial class for_approve_list : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Classes.commonTask.loadShift(ddlShiftName);
                searchLeaveApplicationForApproved();

                PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "for_approve_leave_list.aspx", gvForApprovedList);
            }
        }
       

        DataTable dtForApprovedList;
        private void searchLeaveApplicationForApproved()
        {
            try
            {
                
                dtForApprovedList = new DataTable();
                
                if (ddlFindingType.SelectedValue.ToString().Equals("Today"))
                {
                    // for all leave request of today
                    if (ddlShiftName.SelectedIndex == 0 && ddlFindingType.SelectedIndex == 0)
                        sqlDB.fillDataTable("select la.LACode,la.EName,la.LeaveName,Format(la.FromDate,'dd-MM-yyyy') as FromDate,Format(la.ToDate,'dd-MM-yyyy') as ToDate,la.TotalDays,Format(la.EntryDate,'dd-MM-yyyy') as EntryDate  from v_Leave_Application as la where la.IsApproved='false' AND la.IsProcessessed='false' AND EntryDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", dtForApprovedList = new DataTable());
                    // for sepecifc shift leave request of today
                    else if (ddlShiftName.SelectedIndex != 0 && ddlFindingType.SelectedIndex != 0)
                        sqlDB.fillDataTable("select la.LACode,la.EName,la.LeaveName,Format(la.FromDate,'dd-MM-yyyy') as FromDate,Format(la.ToDate,'dd-MM-yyyy') as ToDate,la.TotalDays,Format(la.EntryDate,'dd-MM-yyyy') as EntryDate  from v_Leave_Application as la where la.IsApproved='false' AND la.IsProcessessed='false' AND EntryDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                            " ShiftId=" + ddlShiftName.SelectedValue.ToString() + " ", dtForApprovedList = new DataTable());
                    
                }
                else
                {
                    // for all leave request of all shift
                    if (ddlShiftName.SelectedIndex == 0 && ddlFindingType.SelectedIndex != 0)
                        sqlDB.fillDataTable("select la.LACode,la.EName,la.LeaveName,Format(la.FromDate,'dd-MM-yyyy') as FromDate,Format(la.ToDate,'dd-MM-yyyy') as ToDate,la.TotalDays,Format(la.EntryDate,'dd-MM-yyyy') as EntryDate  from v_Leave_Application as la where la.IsApproved='false' AND la.IsProcessessed='false' ", dtForApprovedList = new DataTable());

                    
                    // for all leave request of specific shift
                    else if (ddlShiftName.SelectedIndex != 0 && ddlFindingType.SelectedIndex != 0)
                        sqlDB.fillDataTable("select la.LACode,la.EName,la.LeaveName,Format(la.FromDate,'dd-MM-yyyy') as FromDate,Format(la.ToDate,'dd-MM-yyyy') as ToDate,la.TotalDays,Format(la.EntryDate,'dd-MM-yyyy') as EntryDate  from v_Leave_Application as la where la.IsApproved='false' AND la.IsProcessessed='false' " +
                            " AND ShiftId=" + ddlShiftName.SelectedValue.ToString() + " ", dtForApprovedList = new DataTable());
                    
                }
                  
                if (dtForApprovedList.Rows ==null || dtForApprovedList.Rows.Count==0)
                {
                    gvForApprovedList.DataSource = null;
                    gvForApprovedList.DataBind();
                    divRecordMessage.InnerText = "Any Leave Approved Request Are Not Founded !";
                    divRecordMessage.Visible = true;
                    return;
                }
                gvForApprovedList.DataSource = dtForApprovedList;
                gvForApprovedList.DataBind();
                divRecordMessage.InnerText = "";
                divRecordMessage.Visible = false;
            }
            catch (Exception ex)
            {
               // lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        protected void gvForApprovedList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                int rIndex = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName.Equals("Yes"))
                {
                    ViewState["__Predecate__"] = "Yes";
                    YesApproved(rIndex);

                    
                }
                else if (e.CommandName.Equals("No"))
                {
                    ViewState["__Predecate__"] = "No";
                    saveLeaveApplicationRequest_AsLog(rIndex);  // this function calling for save leave application log
                    
                }
                else if (e.CommandName.Equals("Alter"))
                {
                    
                    TextBox txtFromDate = (TextBox)gvForApprovedList.Rows[rIndex].Cells[2].FindControl("txtFromDate");
                    TextBox txtToDate = (TextBox)gvForApprovedList.Rows[rIndex].Cells[3].FindControl("txtToDate");
                    txtFromDate.Enabled = true;
                    txtToDate.Enabled = true;
                    txtFromDate.Style.Add("border-style","solid");
                    txtFromDate.Style.Add("border-color", "#0000ff");

                    txtToDate.Style.Add("border-style", "solid");
                    txtToDate.Style.Add("border-color", "#0000ff");
                }

                else if (e.CommandName.Equals("Calculation"))
                {
                    TextBox txtFromDate = (TextBox)gvForApprovedList.Rows[rIndex].Cells[2].FindControl("txtFromDate");
                    TextBox txtToDate = (TextBox)gvForApprovedList.Rows[rIndex].Cells[3].FindControl("txtToDate");

                    TextBox txtTotalDays = (TextBox)gvForApprovedList.Rows[rIndex].Cells[3].FindControl("txtTotalDays");
                    
                    DataTable dt = new DataTable();

                    string [] getFDays = txtFromDate.Text.Trim().Split('-');
                    string[] getTDays = txtToDate.Text.Trim().Split('-');
                    getFDays[0] = getFDays[2] + "-" + getFDays[1] + "-" + getFDays[0];
                    getTDays[0] = getTDays[2] + "-" + getTDays[1] + "-" + getTDays[0];
                     
                    
                   
                    sqlDB.fillDataTable("select distinct Convert(varchar(11),Offdate,105) as OffDate,Reason from v_AllOffDays where OffDate >='" + getFDays[0] + "' AND OffDate<='" + getTDays[0] + "'", dt = new DataTable());
                    ViewState["__WHnumber__"] = dt.Rows.Count;

                    sqlDB.fillDataTable("select DateDiff(Day,'" + getFDays[0] + "','" + getTDays[0] + "') as TotalDays", dt=new DataTable ());

                    txtTotalDays.Text = (int.Parse(dt.Rows[0]["TotalDays"].ToString())+1).ToString();

                }
                else if (e.CommandName == "View") 
                {
                    ForLeaveReport.generateLeaveApplicationReport(gvForApprovedList.DataKeys[rIndex].Values[0].ToString());
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=LeaveApplicationReport');", true);

                }
                
            }
            catch { }
        }

        

        private void YesApproved(int rIndex)
        {
            try
            {
  	         string [] getColumns={"IsApproved","ApprovedDate","IsProcessessed"};
	         string [] getValues={"1",convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString(),"1"};
             if (SQLOperation.forUpdateValue("Leave_Application", getColumns, getValues, "LACode", gvForApprovedList.DataKeys[rIndex].Values[0].ToString(), DbConnection.Connection) == true)
	         {
                 saveLeaveApplicationRequest_AsLog(rIndex);   // this function calling for save leave application log
                 lblMessage.InnerText = "success->" + gvForApprovedList.Rows[rIndex].Cells[2].Text.ToString() + " approved for " + gvForApprovedList.Rows[rIndex].Cells[1].Text;
                 gvForApprovedList.Rows[rIndex].Visible = false;
	         }

            }
            catch{}
       
        }

        private void NoApproved(int rIndex)
        {
            try
            {


                if (SQLOperation.forDeleteRecordByIdentifier("Leave_Application", "LACode", gvForApprovedList.DataKeys[rIndex].Values[0].ToString(), DbConnection.Connection))
                {
                    SQLOperation.forDeleteRecordByIdentifier("Leave_Application_Details", "LACode", gvForApprovedList.DataKeys[rIndex].Values[0].ToString(), DbConnection.Connection); // for clear all leave details

                    SqlCommand cmd = new SqlCommand("update Leave_Application_Log set ApprovedDate ='" + convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString() + "' where LACode=" + gvForApprovedList.DataKeys[rIndex].Values[0].ToString() + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                    lblMessage.InnerText = "success->" + gvForApprovedList.Rows[rIndex].Cells[2].Text.ToString() + " not approved for " + gvForApprovedList.Rows[rIndex].Cells[1].Text.ToString();
                    gvForApprovedList.Rows[rIndex].Visible = false;
                }

            }
            catch { }
        }

        private void saveLeaveApplicationRequest_AsLog(int rIndex)    // this function for save leave application log
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select LACode,LeaveId,Format(FromDate,'dd-MM-yyyy') as FromDate,Format(ToDate,'dd-MM-yyyy') as ToDate,WeekHolydayNo,TotalDays,"+
                    "Remarks,EId,IsApproved,Format(ApprovedDate,'dd-MM-yyyy')as ApprovedDate,IsProcessessed,Format(PregnantDate,'dd-MM-yyyy') as PregnantDate,"+
                    "Format(PrasaberaDate,'dd-MM-yyyy') as PrasaberaDate,Format(EntryDate,'dd-MM-yyyy') as EntryDate,ShiftId,IsMeternity" +
                    " from Leave_Application where LACode=" + gvForApprovedList.DataKeys[rIndex].Values[0].ToString() + "", dt);

                ViewState["__OldFromDate__"]=dt.Rows[0]["FromDate"].ToString();
                ViewState["__OldToDate__"]=dt.Rows[0]["ToDate"].ToString();

                byte isApproved = (bool.Parse(dt.Rows[0]["IsApproved"].ToString()).Equals(true)) ? (byte)1 : (byte)0;
                byte isProcessed = (bool.Parse(dt.Rows[0]["IsProcessessed"].ToString()).Equals(true)) ? (byte)1 : (byte)0;


                if (bool.Parse(dt.Rows[0]["IsMeternity"].ToString()).Equals(true)) // IsMeternity = true means this is meternity leave request
                {
                    if (isApproved == 1)   // while leave nature is maternity and already this leave are approved.So,ApprovedDate must be added in log table
                    {
                         string[] getColumns = { "LACode","LeaveId", "FromDate", "ToDate", "WeekHolydayNo", "TotalDays", "Remarks", "EID", "IsApproved","ApprovedDate",
                                               "IsProcessessed", "EntryDate", "IsMeternity", "ShiftId","ApprovedRejected","PregnantDate", "PrasaberaDate" };

                         string[] getValues = {dt.Rows[0]["LACode"].ToString(),dt.Rows[0]["LeaveId"].ToString(),convertDateTime.getCertainCulture(dt.Rows[0]["FromDate"].ToString()).ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["ToDate"].ToString()).ToString(),dt.Rows[0]["WeekHolydayNo"].ToString(),dt.Rows[0]["TotalDays"].ToString(),dt.Rows[0]["Remarks"].ToString(),dt.Rows[0]["EId"].ToString(),
                                         isApproved.ToString(),convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString(),isProcessed.ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["EntryDate"].ToString()).ToString(),"1",dt.Rows[0]["ShiftId"].ToString(),"Approved",
                                         convertDateTime.getCertainCulture(dt.Rows[0]["PregnantDate"].ToString()).ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["PrasaberaDate"].ToString()).ToString()};

                         SQLOperation.forSaveValue("Leave_Application_Log", getColumns, getValues, DbConnection.Connection);
                       
                      
                      
                     //  ChangeEmpTypeForML(dt.Rows[0]["EmpId"].ToString());  // for employee current status changed when leave type are maternity leave 
                    }
                    else // while leave nature is maternity and  leave are not approved.so approved date not entered in log table
                    {
                        string[] getColumns = { "LACode","LeaveId", "FromDate", "ToDate", "WeekHolydayNo", "TotalDays", "Remarks", "EID", "IsApproved",
                                               "IsProcessessed", "EntryDate", "IsMeternity", "ShiftId","ApprovedRejected","PregnantDate", "PrasaberaDate" };

                        string[] getValues = {dt.Rows[0]["LACode"].ToString(),dt.Rows[0]["LeaveId"].ToString(),convertDateTime.getCertainCulture(dt.Rows[0]["FromDate"].ToString()).ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["ToDate"].ToString()).ToString(),dt.Rows[0]["WeekHolydayNo"].ToString(),dt.Rows[0]["TotalDays"].ToString(),dt.Rows[0]["Remarks"].ToString(),dt.Rows[0]["EId"].ToString(),
                                         isApproved.ToString(),isProcessed.ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["EntryDate"].ToString()).ToString(),"1",dt.Rows[0]["ShiftId"].ToString(),"Rejected",
                                         convertDateTime.getCertainCulture(dt.Rows[0]["PregnantDate"].ToString()).ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["PrasaberaDate"].ToString()).ToString()};

                        SQLOperation.forSaveValue("Leave_Application_Log", getColumns, getValues, DbConnection.Connection);
                    }
                }
                else  // here count every leave without maternity leave type,so here just considering approved status for approved date
                {
                    if (isApproved == 1)
                    {
                        string[] getColumns = { "LACode","LeaveId", "FromDate", "ToDate", "WeekHolydayNo", "TotalDays", "Remarks", "EID", "IsApproved","ApprovedDate",
                                               "IsProcessessed", "EntryDate", "IsMeternity", "ShiftId","ApprovedRejected" };

                        string[] getValues = {dt.Rows[0]["LACode"].ToString(),dt.Rows[0]["LeaveId"].ToString(),convertDateTime.getCertainCulture(dt.Rows[0]["FromDate"].ToString()).ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["ToDate"].ToString()).ToString(),dt.Rows[0]["WeekHolydayNo"].ToString(),dt.Rows[0]["TotalDays"].ToString(),dt.Rows[0]["Remarks"].ToString(),dt.Rows[0]["EId"].ToString(),
                                         isApproved.ToString(),convertDateTime.getCertainCulture(DateTime.Now.ToString("dd-MM-yyyy")).ToString(),isProcessed.ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["EntryDate"].ToString()).ToString(),"1",dt.Rows[0]["ShiftId"].ToString(),"Approved"};

                        SQLOperation.forSaveValue("Leave_Application_Log", getColumns, getValues, DbConnection.Connection);
                    }
                    else
                    {
                        string[] getColumns = { "LACode","LeaveId", "FromDate", "ToDate", "WeekHolydayNo", "TotalDays", "Remarks", "EID", "IsApproved",
                                               "IsProcessessed", "EntryDate", "IsMeternity", "ShiftId","ApprovedRejected" };

                        string[] getValues = {dt.Rows[0]["LACode"].ToString(),dt.Rows[0]["LeaveId"].ToString(),convertDateTime.getCertainCulture(dt.Rows[0]["FromDate"].ToString()).ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["ToDate"].ToString()).ToString(),dt.Rows[0]["WeekHolydayNo"].ToString(),dt.Rows[0]["TotalDays"].ToString(),dt.Rows[0]["Remarks"].ToString(),dt.Rows[0]["EId"].ToString(),
                                         isApproved.ToString(),isProcessed.ToString(),
                                         convertDateTime.getCertainCulture(dt.Rows[0]["EntryDate"].ToString()).ToString(),"1",dt.Rows[0]["ShiftId"].ToString(),"Rejected"};

                        SQLOperation.forSaveValue("Leave_Application_Log", getColumns, getValues, DbConnection.Connection);
                    }
                
                }


                if (isApproved==1) // if and date are changed then change leave application date and total days changed
                {
                    TextBox txtFromDate = (TextBox)gvForApprovedList.Rows[rIndex].Cells[2].FindControl("txtFromDate");
                    TextBox txtToDate = (TextBox)gvForApprovedList.Rows[rIndex].Cells[3].FindControl("txtToDate");

                    if (!ViewState["__OldFromDate__"].ToString().Equals(txtFromDate.Text.Trim()) || !ViewState["__OldToDate__"].ToString().Equals(txtToDate.Text.Trim()))
                    {
                        TextBox txtTotalDays = (TextBox)gvForApprovedList.Rows[rIndex].Cells[3].FindControl("txtTotalDays");

                        string[] getColumns = { "FromDate", "ToDate", "TotalDays", "WeekHolydayNo" };
                        string[] getValues = { convertDateTime.getCertainCulture(txtFromDate.Text.Trim()).ToString(), convertDateTime.getCertainCulture(txtToDate.Text.Trim()).ToString(), txtTotalDays.Text.Trim(), ViewState["__WHnumber__"].ToString()};
                        SQLOperation.forUpdateValue("Leave_Application", getColumns, getValues, "LACode", gvForApprovedList.DataKeys[rIndex].Values[0].ToString(), DbConnection.Connection);

                        saveLeaveDetails(gvForApprovedList.DataKeys[rIndex].Values[0].ToString(), txtFromDate.Text, txtToDate.Text, dt.Rows[0]["EId"].ToString());  // for save leave details
                    }
                }

                if (ViewState["__Predecate__"].ToString().Equals("No")) NoApproved(rIndex);
                
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }


        private void ChangeEmpTypeForML(string getEmpId)   // change EmpType when any female get any Maternity Leave
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Personnel_EmpCurrentStatus set EmpStatus='8' where SN=(select Max(SN) from  Personnel_EmpCurrentStatus Where EmpId='" + getEmpId+ "' AND IsActive=1)", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }


        private void saveLeaveDetails(string LACode, string FDates,string TDates,string EmpId)
        {
            try
            {
                SQLOperation.forDeleteRecordByIdentifier("Leave_Application_Details", "LACode", LACode, DbConnection.Connection);

                string[] getFDate = FDates.ToString().Split('-');  // dd-MM-yyyy

                string [] getToDate=TDates.ToString().Split('-');

                DateTime dtFromDate = new DateTime(int.Parse(getFDate[2]), int.Parse(getFDate[1]), int.Parse(getFDate[0]));

                DateTime dtToDate = new DateTime(int.Parse(getToDate[2]), int.Parse(getToDate[1]), int.Parse(getToDate[0]));

                while(dtFromDate<=dtToDate)
                {
                    getFDate = dtFromDate.ToString().Split('/'); // now Format m-d-yyyy

                    

                    string Date = getFDate[1] + "-" + getFDate[0] + "-" + getFDate[2]; // convert format in dd-MM-yyyy
                    string[] getColumns = { "LACode", "EId", "LeaveDate", "Used" };
                    string[] getValues = { LACode,EmpId, convertDateTime.getCertainCulture(Date).ToString(), "0" };
                    SQLOperation.forSaveValue("Leave_Application_Details", getColumns, getValues, DbConnection.Connection);
                    dtFromDate=dtFromDate.AddDays(1);

 
                }
               

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        

        protected void gvForApprovedList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                }
            }
            catch { }
        }

        protected void ddlDepartmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                searchLeaveApplicationForApproved();   // searching operation
            }
            catch { }
        }

        protected void ddlFindingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                searchLeaveApplicationForApproved();   // searching operation
            }
            catch { }
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                searchLeaveApplicationForApproved();   // searching operation
            }
            catch { }
        }

        protected void btnCLeave_Click(object sender, EventArgs e)
        { 
        
        }

        //Md.Nayem 
        //Email=xxx@gmail.com
        //purpose : To set value for leave applicaiton report

        private void viewLeaveApplication(string LaCode)
        {
            try
            {
                string getSQLCMD;
                DataTable dt = new DataTable();
                DataTable dtApprovedRejectedDate = new DataTable();
                getSQLCMD = " SELECT LACode,CompanyId,ShortName,EmpId, Format(v_Leave_LeaveApplication.FromDate,'dd-MM-yyyy') as FromDate , Format(v_Leave_LeaveApplication.ToDate,'dd-MM-yyyy') as ToDate ,v_Leave_LeaveApplication.TotalDays,v_Leave_LeaveApplication.Remarks,"
                    + " v_Leave_LeaveApplication.EmpName, v_Leave_LeaveApplication.DptName, v_Leave_LeaveApplication.DsgName, v_Leave_LeaveApplication.CompanyName, "
                    + " v_Leave_LeaveApplication.SftName, Format(v_Leave_LeaveApplication.EntryDate,'dd-MM-yyyy') as EntryDate"
                    + " FROM"
                    + " Glory.dbo.v_Leave_LeaveApplication"
                    + " where LACode=" + LaCode + "";
                sqlDB.fillDataTable(getSQLCMD, dt);
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning->Sorry any record are not founded"; return;
                }
                Session["__Language__"] = "English";
                Session["__LeaveApplication__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTabandWindow('/All Report/Report.aspx?for=LeaveApplication');", true);  //Open New Tab for Sever side code

            }
            catch { }
        }
        protected void ddlShiftName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                
                searchLeaveApplicationForApproved();   // searching operation
            }
            catch { }
        }
       
    }
}