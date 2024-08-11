using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using DS.PropertyEntities.Model.Admission;
using System.Reflection;
using DS.BLL.Admission;
using DS.BLL.Attendace;

namespace DS.UI.Reports.CrystalReport
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        DataTable dt;
        ReportDocument rpd;

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    rpd.Dispose(); //context means your crystal report document object.
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        List<AdmStdInfoEntities> AdmStdList = new List<AdmStdInfoEntities>();
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                string[] query = Request.QueryString["for"].ToString().Split('-');
                if (query[0].Equals("StdAdmDetails")) LoadStdAdmDetails();
                if (query[0].Equals("StudentListAll")) loadStudentListAll("full");
                else if (query[0].Equals("AdmMoneyReceipt")) LoadAdmMoneyReceipt();
                else if (query[0].Equals("StudentBusInfo")) LoadStudentBusInfo();
                else if (query[0].Equals("StudentBusInformation")) LoadStudentBusInformation();
                else if (query[0].Equals("MoneyReceipt")) LoadMoneyReceipt();
                else if (query[0].Equals("GenderwiseStdList")) loadGenderwiseStudentList(query[1]);
                else if (query[0].Equals("StudentContactList")) loadStudentContactList();
                else if (query[0].Equals("GuardianInformation")) loadGuardianInformation();
                else if (query[0].Equals("GuardianContactList")) loadGuardianContactList();
                else if (query[0].Equals("ParentsInformationList")) loadParentsInformationList();
                else if (query[0].Equals("AllContactList")) loadAllContactList();
                else if (query[0].Equals("DueList")) DueList();
                else if (query[0].Equals("EmployeeList")) EmployeeList(query[1]);
                else if (query[0].Equals("DptwiseEmpList")) DptwiseEmpList();
                else if (query[0].Equals("FineCollectionList")) FineCollectionList();
                else if (query[0].Equals("DiscountList")) LoadDiscountList();
                else if (query[0].Equals("DiscountSummary")) LoadDiscountSummary(query[1]);
                else if (query[0].Equals("FineListReport")) LoadFineListReport();
                else if (query[0].Equals("FineCollectionSummary")) LoadFineCollectionSummary();
                else if (query[0].Equals("StaffAttSheet")) LoadStaffAttSheet(query[1], query[2], query[3]);
                else if (query[0].Equals("StudentProfile")) LoadStudentProfile();
                else if (query[0].Equals("CollectionDetails")) loadCollectionDetails();
                else if (query[0].Equals("CollectionSummary")) loadCollectionSummary();
                else if (query[0].Equals("StudentPaymentDetails")) loadStudentPaymentDetails();
                else if (query[0].Equals("FinalResult")) LoadFinalResult(query[1]);
                else if (query[0].Equals("SubjectWiseMarks")) LoadSubjectWiseMarks();
                else if (query[0].Equals("ExamResultDetails")) LoadExamResultDetails();
                else if (query[0].Equals("ResultSummary")) LoadResultSummary(query[1]);
                else if (query[0].Equals("FailedStudentReport")) FailedStudentReport();
                else if (query[0].Equals("SubjectWiseFailedStudentReport")) SubjectWiseFailedStudentReport();
                else if (query[0].Equals("ResultList")) LoadPassList(query[1]);
                else if (query[0].Equals("FailSubject")) LoadFailSubject();
                else if (query[0].Equals("ExamOverView")) LoadExamOverView();
                else if (query[0].Equals("GenderWiseOverView")) LoadGenderwiseResultOverView();
                else if (query[0].Equals("SubjectQuestionPattern")) LoadSubjectQuestionPattern();//For Subject Question Pattern PrintPreview
                else if (query[0].Equals("DependencyCnvtMarks")) LoadDependencyCnvtMarks();//For Dependency Convert Marks PrintPreview
                else if (query[0].Equals("DependencyExamResult")) LoadDependencyExamResult();//For Dependency Exam Result PrintPreview
                else if (query[0].Equals("SubjectPattern")) LoadSubjectPattern();//For Subject  Pattern PrintPreview
                else if (query[0].Equals("AttendanceSheet")) LoadMonthlyAttendanceReport(query[1], query[2],
                        query[3], query[4], query[5] + "-" + query[6]); //For Attendance Sheet 
                else if (query[0].Equals("StdAttendanceSheet")) LoadStdMonthlyAttendanceReport(query[1], query[2],
                         query[3], query[4], query[5] + "-" + query[6]); //For Attendance Sheet 
                else if (query[0].Equals("EmpAttendanceSheet")) LoadMonthlyEmpAttendanceReport(query[1] + "-"
                        + query[2], query[3], query[4]); //Form Employee Attendance Sheet         
                else if (query[0].Equals("AdmCollectionDetails")) LoadAdmCollectionDetails(query[1]);
                else if (query[0].Equals("IndivisualAbsentDetails")) LoadIndivisualAbsentDetails(query[1],
                        query[2], query[3], query[4] + "-" + query[5] + "-" + query[6], query[7] + "-"
                        + query[8] + "-" + query[9], query[10]); // Individual Monthly Absent Report
                else if (query[0].Equals("StdIndivisualAbsentDetails")) LoadStdIndivisualAbsentDetails(query[1],
                   query[2], query[3], query[4] + "-" + query[5] + "-" + query[6], query[7] + "-"
                   + query[8] + "-" + query[9], query[10]); // Individual Monthly Absent Report
                else if (query[0].Equals("IndivisualEmpAbsentDetails")) LoadEmpIndivisualAbsentDetails(query[1] + "-"
                        + query[2] + "-" + query[3], query[4] + "-" + query[5] + "-" + query[6],
                        query[7], query[8]); //  Individual Monthly Absent Report (Staff And Faculty)
                else if (query[0].Equals("AttendanceSummaryReport")) LoadAttendanceSummaryReport(query[1], query[2], query[3],
                        query[4], query[5] + "-" + query[6] + "-" + query[7], query[8] + "-" + query[9] + "-" + query[10]); // For Attendance Summary
                else if (query[0].Equals("StdAttendanceSummaryReport")) LoadStdAttendanceSummaryReport(query[1], query[2], query[3],
                    query[4], query[5] + "-" + query[6] + "-" + query[7], query[8] + "-" + query[9] + "-" + query[10]); // For Attendance Summary
                else if (query[0].Equals("EmpAttendanceSummaryReport")) LoadEmpAttendanceSummaryReport(query[1], query[2]
                        + "-" + query[3] + "-" + query[4], query[5] + "-" + query[6] + "-" + query[7], query[8]); // For Attendance Summary (Staff And Faculty)
                else if (query[0].Equals("DailyAttendance")) LoadDailyAttendanceReport(query[1], query[2], query[3], query[4],
                         query[5], query[6], query[7]); //For Daily Attendance Report
                else if (query[0].Equals("StudentDailyAttendance")) LoadStudentDailyAttendanceReport(query[1], query[2], query[3], query[4],
                    query[5] + "-" + query[6] + "-" + query[7], query[8], query[9], query[10] + "-" + query[11] + "-" + query[12]); //For Daily Attendance Report
                else if (query[0].Equals("StudentDailyAttendanceSummary")) LoadDailyAttendanceSummary(query[1]);//For Daily Attendance Summary Report
                else if (query[0].Equals("DailyEmpAttendance")) LoadDailyEmpAttendanceReport(query[1], query[2],
                        query[3], query[4], query[5], query[6]);// Daily  Attendance Report (Staff & Faculty)
                else if (query[0].Equals("AdmUnpaidList")) LoadAdmUnpaidStdList();
                else if (query[0].Equals("IndFineCollectionReport")) LoadIndFineCollectionReport();
                else if (query[0].Equals("HRProfile")) LoadHRProfile();
                else if (query[0].Equals("HRwithoutImageProfile")) LoadHRwithoutImageProfile();
                else if (query[0].Equals("DesignationwiseEmpList")) LoadDesignationwiseEmpList();
                else if (query[0].Equals("EmpBloodGroup")) LoadEmpBloodGroup();
                else if (query[0].Equals("CategorywiseParticulars")) LoadCategorywiseParticulars(query[1], query[2], query[3], query[4]);
                else if (query[0].Equals("BatchWiseCategory")) LoadBatchWiseCategory();
                else if (query[0].Equals("AdmissionWiseCategory")) LoadAdmissionWiseCategory();
                else if (query[0].Equals("FineList")) LoadFineList();
                else if (query[0].Equals("StdCollectionDetails")) LoadStdCollectionDetails(query[1]);

                //------------------------------------Leave Report--------------------------------------------
                else if (query[0].Equals("LeaveApplicationReport")) loadLeaveApplicationReport();
                else if (query[0].Equals("LeaveListReport")) loadLeaveListReport(query[1], query[2]);// For Leave Balance Report 
                else if (query[0].Equals("LeaveBalanceReport")) loadLeaveBalanceReport(query[1], query[2]);// For Leave Balance Report 
                else if (query[0].Equals("YearlyLeaveStatus")) loadYearlyLeaveStatus(query[1]);// For YearlyLeaveStatus
                else if (query[0].Equals("LeaveApprovedRejectedReport")) loadApprovedRejectedReport(query[1], query[2]);
                //--------------------------------------------------------------------------------

                //------------------------------------TimeTable----------------------------------
                else if (query[0].Equals("TeacherLoad")) TeacherLoadReport(); // For Teacher Load Report
                //-----------------------------------------------------------------------------

                else if (query[0].Equals("MarksEntrySheet")) LoadMarkSheetEntryList(query[1], query[2], query[3], query[4], query[5]);
                else if (query[0].Equals("StudentSubjectList")) LoadStudentSubjectList();
                else if (query[0].Equals("StudentGrpSubjectList")) LoadStudentGrpSubjectList();
                else if (query[0].Equals("TC")) LoadTC(query[1], query[2], query[3], query[4]);
                else if (query[0].Equals("monthlytestreport")) Loadmonthlytestreport();
                else if (query[0].Equals("progressreportsemester")) Loadprogressreportsemester();

                //-------------------------New Progress Reports by Nayem date: 30-01-2018----------------
                else if (query[0].Equals("SemesterProgressReport")) SemesterProgressReport(query[1], query[2]);
                else if (query[0].Equals("AttendanceInExam")) AttendanceSheetInExamReport();
                else if (query[0].Equals("NumberSheetInExam")) NumberSheetInExamReport();
                else if (query[0].Equals("AdmitCard")) AdmitCardReport();
                else if (query[0].Equals("ExamRoutine")) ExamRoutineReport();
                else if (query[0].Equals("ExamineeNumber")) ExamineeNumberReport();
                else if (query[0].Equals("AcademicTranscript")) AcademicTranscript(query[1], query[2]);
                else if (query[0].Equals("AcademicTranscriptWithMarks")) AcademicTranscriptWithMarks(query[1], query[2]);
                else if (query[0].Equals("meritlist")) MeritListReport();
                //-------------------------------------------------------------------------------------
                //-------------------------Teacher Eavaluation Reports by Nayem date: 12-03-2018----------------
                else if (query[0].Equals("EvaFinalGradeSheet")) EvaFinalGradeSheet();
                else if (query[0].Equals("EvaTeachersPerformanceRanking")) EvaTeachersPerformanceRanking();
                else if (query[0].Equals("EvaDepartmentRank")) EvaDepartmentRank();
                else if (query[0].Equals("IndividualPerformanceReport")) IndividualPerformanceReport();
                else if (query[0].Equals("DepartmentPerformanceReport")) DepartmentPerformanceReport();
                else if (query[0].Equals("SubIndicatorBasedPerformanceReport")) SubIndicatorBasedPerformanceReport();
                
                //-------------------------------------------------------------------------------------

                //--------------------------------For Hide Group Tree--------------------------------
                CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                //-----------------------------------------------------------------------------------


            }
            catch { }
        }
        private void LoadAdmMoneyReceipt()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AdmMoneyReceipt__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//AdmissionMoneyReceipt.rpt"));
                
                rpd.SetDataSource(dt);
                
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(1, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(0, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, dt.Rows[0]["LogoName"].ToString());
                rpd.SetParameterValue(3, Server.MapPath("//Images//Logo//"));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadMoneyReceipt()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__MoneyReceipt__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//AdmissionMoneyReceipt.rpt"));

                rpd.SetDataSource(dt);

                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(1, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(0, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, dt.Rows[0]["LogoName"].ToString());
                rpd.SetParameterValue(3, Server.MapPath("//Images//Logo//"));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void TeacherLoadReport()// For Students
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__TeacherLoad__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//TeacherLoadReport.rpt"));
                rpd.SetDataSource(dt);
                DataTable dtSclInfo = new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dtSclInfo.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dtSclInfo.Rows[0]["Address"].ToString());              
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadIndivisualAbsentDetails(string Batch, string sec, string group, string FDate, string TDate, string shift)// For Students
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AbsentDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//IndivisualAbsentDetails.rpt"));
                rpd.SetDataSource(dt);
                DataTable dtSclInfo = new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dtSclInfo.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Batch);
                rpd.SetParameterValue(2, sec);
                rpd.SetParameterValue(3, FDate);
                rpd.SetParameterValue(4, group);
                rpd.SetParameterValue(5, dt.Rows[0]["ClassName"].ToString());
                rpd.SetParameterValue(6, dtSclInfo.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(7, TDate);
                rpd.SetParameterValue(8, shift);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStdIndivisualAbsentDetails(string Batch, string sec, string group, string FDate, string TDate, string shift)// For Students
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AbsentDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StudentPanel//StdIndivisualAbsentDetails.rpt"));
                rpd.SetDataSource(dt);
                DataTable dtSclInfo = new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dtSclInfo.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Batch);
                rpd.SetParameterValue(2, sec);
                rpd.SetParameterValue(3, FDate);
                rpd.SetParameterValue(4, group);
                rpd.SetParameterValue(5, dt.Rows[0]["ClassName"].ToString());
                rpd.SetParameterValue(6, dtSclInfo.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(7, TDate);
                rpd.SetParameterValue(8, shift);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void LoadEmpIndivisualAbsentDetails(string FDate, string TDate, string shift, string Emptype)// For Staff And Faculty Absent Details
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EmpAbsentDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//IndivisualAbsentDetails_ForEmp.rpt"));
                rpd.SetDataSource(dt);
                DataTable dtSclInfo = new DataTable();
                dtSclInfo = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dtSclInfo.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, FDate);
                rpd.SetParameterValue(2, dtSclInfo.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(3, TDate);
                rpd.SetParameterValue(4, shift);
                rpd.SetParameterValue(5, Emptype);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void LoadAttendanceSummaryReport(string shift, string BatchName, string GroupName, string Section, string FDate, string TDate) // Attendance Summary For Student
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceSummaryReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//MonthwaysAttSummary.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, BatchName);
                rpd.SetParameterValue(2, Section);
                rpd.SetParameterValue(3, GroupName);
                rpd.SetParameterValue(4, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(5, shift);
                rpd.SetParameterValue(6, FDate);
                rpd.SetParameterValue(7, TDate);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStdAttendanceSummaryReport(string shift, string BatchName, string GroupName, string Section, string FDate, string TDate) // Attendance Summary For Student
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceSummaryReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StudentPanel//StdMonthwaysAttSummary.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, BatchName);
                rpd.SetParameterValue(2, Section);
                rpd.SetParameterValue(3, GroupName);
                rpd.SetParameterValue(4, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(5, shift);
                rpd.SetParameterValue(6, FDate);
                rpd.SetParameterValue(7, TDate);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }


        private void LoadDailyAttendanceReport(string Shift, string Batch, string Group, string Section, string Date, string ReportTitle, string ReportType) // For Daily Attendance Report
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DailyAttendance__"];
                rpd = new ReportDocument();
                if (ReportType == "Status")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyAttendanceStatus.rpt"));
                else if (ReportType == "PresentAbsent")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyPresentAbsentStatus.rpt"));
                else
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyLogInOutStatus.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Shift);
                rpd.SetParameterValue(3, Batch);
                rpd.SetParameterValue(4, Group);
                rpd.SetParameterValue(5, Section);
                rpd.SetParameterValue(6, ReportTitle);
                rpd.SetParameterValue(7, Date.Replace('/', '-'));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStudentDailyAttendanceReport(string Shift, string Batch, string Group, string Section, string Date, string ReportTitle, string ReportType,string ToDate) // For Daily Attendance Report
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DailyAttendance__"];
                rpd = new ReportDocument();
                if (ReportType == "Status")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StudentPanel//StudentDailyAttendanceStatus.rpt"));              
                else
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StudentPanel//StudentDailyLogInOutStatus.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Shift);
                rpd.SetParameterValue(3, Batch);
                rpd.SetParameterValue(4, Group);
                rpd.SetParameterValue(5, Section);
                rpd.SetParameterValue(6, ReportTitle);
                rpd.SetParameterValue(7, Date);
                rpd.SetParameterValue(8,ToDate);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadDailyAttendanceSummary(string date) // For Daily Attendance Report
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DailyAttendanceSummary__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyAttSummary.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, date.Replace('/','-'));                
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }


        private void LoadEmpAttendanceSummaryReport(string shift, string FDate, string TDate, string EmpType) // Attendance Summary For Staff and Faculty
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EmpAttendanceSummaryReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//MonthwaysAttSummary_For_Info.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, shift);
                rpd.SetParameterValue(3, FDate);
                rpd.SetParameterValue(4, TDate);
                rpd.SetParameterValue(5, EmpType);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadDailyEmpAttendanceReport(string ReportTitle, string ReportType, string shift, string empType, string DateRange, string individual) // Daily  Attendance Report (Staff & Faculty)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DailyEmpAttendance__"];
                rpd = new ReportDocument();
                if (ReportType == "LogInOut")
                {
                    if (individual == "Yes")
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyEmpLogInOutStatus_Individual.rpt"));
                    else
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyEmpLogInOutStatus.rpt"));
                }
                else
                {
                    if (individual == "Yes")
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyEmpAtendanceStatus_Individual.rpt"));
                    else
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyEmpAtendanceStatus.rpt"));
                }
                //else if (ReportType == "PresentAbsent")
                //    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyPresentAbsentStatus.rpt"));
                //else
                //    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//DailyLogInOutStatus.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, ReportTitle);
                rpd.SetParameterValue(3, shift);
                rpd.SetParameterValue(4, empType);
                rpd.SetParameterValue(5, DateRange.Replace('/', '-'));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadMonthlyEmpAttendanceReport(string month, string shift, string Emptype) // Monthly Employee Attendance Report
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EmpAttendanceSheet__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//MonthlyEmpAttendanceSheet.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, month);
                rpd.SetParameterValue(3, shift);
                rpd.SetParameterValue(4, Emptype);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }    
        private void LoadMonthlyAttendanceReport(string Shift, string Batch, string Group, string Section, string month) // Monthly Attendance Report
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceSheet__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//MonthlyAttendanceStatusSummary3.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Batch);
                rpd.SetParameterValue(3, Group);
                rpd.SetParameterValue(4, Section);
                rpd.SetParameterValue(5, month);
                rpd.SetParameterValue(6, Shift);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStdMonthlyAttendanceReport(string Shift, string Batch, string Group, string Section, string month) // Monthly Attendance Report
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceSheet__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StudentPanel//StdMonthlyAttendanceStatusSummary3.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Batch);
                rpd.SetParameterValue(3, Group);
                rpd.SetParameterValue(4, Section);
                rpd.SetParameterValue(5, month);
                rpd.SetParameterValue(6, Shift);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStdAdmDetails()
        {
            try
            {
                AdmStdList = (List<AdmStdInfoEntities>)Session["__StdAdmDetails__"];
                dt = ConvertDataTable();
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//AdmDetails.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private DataTable ConvertDataTable()
        {
            dt = new DataTable();
            dt.Columns.Add("AdmissionNo",typeof(int));
            dt.Columns.Add("AdmissionDate", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("ClassName", typeof(string));
            dt.Columns.Add("Shift", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("GuardianMobileNo", typeof(string));
            dt.Columns.Add("FathersName", typeof(string));
            dt.Columns.Add("StdStatus", typeof(Boolean));
            for (int i = 0; i < AdmStdList.Count; i++)
            {
                dt.Rows.Add(AdmStdList[i].AdmissionNo, AdmStdList[i].AdmissionDate.ToString("dd-MM-yyyy"),
                    AdmStdList[i].Student.FullName, AdmStdList[i].Student.ClassName,
                    AdmStdList[i].Student.Shift, AdmStdList[i].Student.Gender,
                    AdmStdList[i].Student.GuardianMobileNo,AdmStdList[i].Student.FathersName, AdmStdList[i].StdStatus);
            }
                return dt;
        }
        
        private void loadCollectionDetails()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__CollectionDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//FeeCollectionDetails.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = (DataTable)Session["__CollectionDetailsParticular__"];
                rpd.Subreports[0].SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        private void loadCollectionSummary()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__CollectionSummary__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//CollectionDetails.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        private void loadStudentPaymentDetails()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__StudentPaymentDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//StudentPaymentDetails.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        private void loadStudentListAll(string Type)
        {
            try
            {                
                dt = new DataTable();
                dt = (DataTable)Session["_StudentList_"];
                rpd = new ReportDocument();
                if(Type=="full")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//StudentAllList_Full.rpt"));
                else
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//StudentAllList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Server.MapPath("//Images//profileImages//"));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        private void LoadStudentBusInfo()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["_StudentBusInfo_"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//StudentBusInfo.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        private void LoadStudentBusInformation()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["_StudentBusInformation_"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//StudentBusInformation.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        private void loadGenderwiseStudentList(string gender)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__GenderwiseStdList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//GenderwiseStdList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, gender);
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }

        private void loadStudentContactList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ContactInfo__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//StudentContactList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }

        private void loadGuardianInformation()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__GuardianInformation__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//GuardianInformation.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void loadGuardianContactList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__GuardianInfo__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//GuardianContactList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void loadParentsInformationList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ParentsInformationList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//ParentsInformationList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void loadAllContactList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AllContactList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//AllContactList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void DueList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DueList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//DueList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void EmployeeList(string ReportTitle)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EmployeeList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//EmployeeList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, ReportTitle);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }        
        private void FineCollectionList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FineCollectionList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StudentFine//FineCollectionList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        //.....................................Attendance Report........................................................//
        private void LoadMonthwaysAttendance(string cls, string sec, string month)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceSheet__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//AttendanceSheet.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, cls);
                rpd.SetParameterValue(2, sec);
                rpd.SetParameterValue(3, month);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStaffAttSheet(string Month, string dept, string desg)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__StaffAttSheet__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//StaffAttSheet.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Month);
                rpd.SetParameterValue(2, dept);
                rpd.SetParameterValue(3, desg);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadAttendanceSummaryReport(string cls, string sec, string month)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceSummaryReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//MonthwaysAttSummary.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, cls);
                rpd.SetParameterValue(2, sec);
                rpd.SetParameterValue(3,month);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }       
        private void LoadStudentProfile()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["_StudentProfile_"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Students//StudentProfile.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, Server.MapPath("//Images//profileImages//"));
                rpd.SetParameterValue(1, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());  
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadFinalResult(string FinalResultReportName)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FinalResult__"];
                rpd = new ReportDocument();
                if (FinalResultReportName.Equals("FinalResultwithOptional"))
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//FinalResultwithOpSub.rpt"));
                }
                else
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//FinalReslutwithoutOpSub.rpt"));
                }
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadSubjectWiseMarks()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__SubjectWiseMarks__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//SubjectwiseMarklist.rpt"));               
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadExamResultDetails()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ExamResultDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ExamResultDetails.rpt"));
                rpd.SetDataSource(dt);               
                //DataTable distinctTable = new DataTable();
                //DataView view = new DataView(dt);
                //distinctTable = view.ToTable(true, "MainSubName", "RollNo", "MarksOfSubject_WithAllDependencySub",
                //    "GradeOfSubject_WithAllDependencySub", "PointOfSubject_WithAllDependencySub");
                //rpd.Subreports[0].SetDataSource(distinctTable);
                CrystalReportViewer1.ReportSource = rpd;
                DataTable dtSchool = new DataTable();
                dtSchool = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dtSchool.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dtSchool.Rows[0]["Address"].ToString());
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadResultSummary(string Section)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ResultSummary__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ResultSummaryReport.rpt"));               
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = (DataTable)Session["__SubjectWiseResultAnalysis__"];
                rpd.Subreports[0].SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(3, Section);

                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void FailedStudentReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FailedStudentReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//FailedStudentReport.rpt"));               
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = (DataTable)Session["__FailedAccordingToNumberOfSubjects__"];
                rpd.Subreports[0].SetDataSource(dt);
                dt = new DataTable();
                dt = (DataTable)Session["__SubjectWiseFailedStudentSummary__"];
                rpd.Subreports[1].SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());

                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void SubjectWiseFailedStudentReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__SubjectWiseFailedStudentReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//SubjectWiseFailedStudentReport.rpt"));               
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());

                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadPassList(string ResultList)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["_ResultList_"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ResultList.rpt"));
                rpd.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rpd;
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, ResultList);
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadFailSubject()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FailSubject__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//FailSubject.rpt"));
                rpd.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rpd;
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());               
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadExamOverView()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ExamOverView__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ExamOverView.rpt"));
                rpd.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rpd;
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadGenderwiseResultOverView()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__GenderWiseOverView__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//GenderwiseOverView.rpt"));
                rpd.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rpd;
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadSubjectQuestionPattern() //For Subject Question Pattern PrintPreview
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["_SubjectQuestionPattern_"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//SubjectWiseQuestionPattern.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadDependencyCnvtMarks() //For Dependency Convert Marks
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DependencyCnvtMarks__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//DependencyCnvtMarks.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadDependencyExamResult() //For Dependency Exam Result
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DependencyExamResult__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//FinalResultwithDependencyExam.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        //...............For Subject Pattern ....................
        private void LoadSubjectPattern()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["_SubjectPattern_"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ManagedSubject//SubjectPattern.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        //............For Admission Collection Details...........................................
        private void LoadAdmCollectionDetails(string date)
        {
            try
            {
                date = date.Replace("/","-");
                dt = new DataTable();
                dt = (DataTable)Session["__AdmCollectionDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//AdmCollectionDetails.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, date);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadAdmUnpaidStdList()
        {
            try
            {                
                dt = new DataTable();
                dt = (DataTable)Session["__AdmUnpaidList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//AdmUnpaidStdList.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());                
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadIndFineCollectionReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__IndFineCollectionReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//IndFineCollection.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        //............HR Part..................
        private void LoadHRProfile()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__HRProfile__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//HRProfile.rpt"));
                rpd.SetDataSource(dt);

                dt = (DataTable)Session["__EmpEducationalInfo__"];
                rpd.Subreports[0].SetDataSource(dt);
                dt = (DataTable)Session["__EmpExperienceInfo__"];
                rpd.Subreports[1].SetDataSource(dt);
                dt = (DataTable)Session["__EmpOthersInfo__"];
                rpd.Subreports[2].SetDataSource(dt);

                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();                
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Server.MapPath("//Images//teacherProfileImage//"));
                rpd.SetParameterValue(3, Server.MapPath("//Images//EmpSign//"));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadHRwithoutImageProfile()
        {
           
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__HRProfile__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//HRProfilewithoutImage.rpt"));
                rpd.SetDataSource(dt);

                dt = (DataTable)Session["__EmpEducationalInfo__"];
                rpd.Subreports[0].SetDataSource(dt);
                dt = (DataTable)Session["__EmpExperienceInfo__"];
                rpd.Subreports[1].SetDataSource(dt);
                dt = (DataTable)Session["__EmpOthersInfo__"];
                rpd.Subreports[2].SetDataSource(dt);

                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());                
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void DptwiseEmpList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DptwiseEmpList__"];
                rpd = new ReportDocument();
                if (Session["__Image__"].ToString() == "withoutimage")
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//DptwiseEmpList.rpt"));
                }
                else
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//DptwiseEmpListwithImage.rpt"));
                }
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                if (Session["__Image__"].ToString() == "withimage")
                {
                    rpd.SetParameterValue(2, Server.MapPath("//Images//teacherProfileImage//"));                   
                }
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadDesignationwiseEmpList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DesignationwiseEmpList__"];
                rpd = new ReportDocument();
                if (Session["__Image__"].ToString() == "withoutimage")
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//DesignationwiseReportwithoutImage.rpt"));
                }
                else
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//DesignationwiseReportwithImage.rpt"));
                }
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                if (Session["__Image__"].ToString() == "withimage")
                {
                     rpd.SetParameterValue(2, Server.MapPath("//Images//teacherProfileImage//"));
                     rpd.SetParameterValue(3, Server.MapPath("//Images//teacherProfileImage//noimage.gif"));
                }                
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadEmpBloodGroup()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__BloodGroup__"];
                rpd = new ReportDocument();
                if (Session["__Image__"].ToString() == "withoutimage")
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//BloodGroupwithoutImage.rpt"));
                }
                else
                {
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//StafforFaculty//BloodGroupwithImage.rpt"));
                }
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                if (Session["__Image__"].ToString() == "withimage")
                {
                    rpd.SetParameterValue(2, Server.MapPath("//Images//teacherProfileImage//"));                   
                }
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadCategorywiseParticulars(string shift, string batch, string group, string section)
        {
            dt = new DataTable();
            dt = (DataTable)Session["__CategorywiseParticulars__"];
            rpd = new ReportDocument();
            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//CategorywiseParticulars.rpt"));
            rpd.SetDataSource(dt);
            dt = new DataTable();
            dt = Classes.commonTask.LoadShoolInfo();
            rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
            rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
            rpd.SetParameterValue(2, shift);
            rpd.SetParameterValue(3, batch);
            rpd.SetParameterValue(4, group);
            rpd.SetParameterValue(5, section);
            CrystalReportViewer1.ReportSource = rpd;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
        }
        private void LoadBatchWiseCategory()
        {
            dt = new DataTable();
            dt = (DataTable)Session["__BatchWiseCategory__"];
            rpd = new ReportDocument();
            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//BatchWiseParticulars.rpt"));
            rpd.SetDataSource(dt);
            dt = new DataTable();
            dt = Classes.commonTask.LoadShoolInfo();
            rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
            rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());           
            CrystalReportViewer1.ReportSource = rpd;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
        }
        private void LoadAdmissionWiseCategory()
        {
            dt = new DataTable();
            dt = (DataTable)Session["__AdmissionWiseCategory__"];
            rpd = new ReportDocument();
            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//AdmissionWiseCategory.rpt"));
            rpd.SetDataSource(dt);
            dt = new DataTable();
            dt = Classes.commonTask.LoadShoolInfo();
            rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
            rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
            CrystalReportViewer1.ReportSource = rpd;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
        }
        private void LoadFineList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FineList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//StudentFine.rpt"));
                rpd.SetDataSource(dt);
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStdCollectionDetails(string Name)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__StdCollectionDetails__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//StudentCollectionDetails.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Name);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;

            }
            catch { }
        }
        //--------------------------------------Leave Report----------------------------------------
        private void loadLeaveApplicationReport()
        {

            rpd = new ReportDocument();

            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveApplicationReport.rpt"));
            dt = new DataTable();
            dt = (DataTable)Session["__LeaveApplicationReport__"];

            DataTable dtRequestedDate = new DataTable();
            dtRequestedDate = ForLeaveReport.GetRequestedDate(dt.Rows[0]["LACode"].ToString());
            // sqlDB.fillDataTable(getSQLCMD, dtRequestedDate);
            DataTable dtLeaveStatus = new DataTable();
            dtLeaveStatus = ForLeaveReport.GetLeaveStatus(dt.Rows[0]["EID"].ToString());
            //    sqlDB.fillDataTable(getSQLCMD1, dtLeaveStatus);

            rpd.SetDataSource(dt);

            if (dtRequestedDate.Rows.Count == 0)
            {
                rpd.SetParameterValue(11, dt.Rows[0]["FromDate"].ToString());
                rpd.SetParameterValue(12, dt.Rows[0]["ToDate"].ToString());
                rpd.SetParameterValue(13, dt.Rows[0]["TotalDays"].ToString());

                rpd.SetParameterValue(18, " ");
                rpd.SetParameterValue(19, " ");
                rpd.SetParameterValue(20, " ");
                rpd.SetParameterValue(21, "Leave Application : Pending");


            }
            else
            {
                if (dtRequestedDate.Rows[0]["ApprovedRejected"].ToString().Equals("Rejected"))
                {
                    rpd.SetParameterValue(11, dtRequestedDate.Rows[0]["FromDate"].ToString());
                    rpd.SetParameterValue(12, dtRequestedDate.Rows[0]["ToDate"].ToString());
                    rpd.SetParameterValue(13, dtRequestedDate.Rows[0]["TotalDays"].ToString());

                    rpd.SetParameterValue(18, " ");
                    rpd.SetParameterValue(19, " ");
                    rpd.SetParameterValue(20, " ");
                    rpd.SetParameterValue(21, "Leave Application : Rejected");
                }
                else
                {
                    rpd.SetParameterValue(11, dtRequestedDate.Rows[0]["FromDate"].ToString());
                    rpd.SetParameterValue(12, dtRequestedDate.Rows[0]["ToDate"].ToString());
                    rpd.SetParameterValue(13, dtRequestedDate.Rows[0]["TotalDays"].ToString());

                    rpd.SetParameterValue(18, dt.Rows[0]["FromDate"].ToString());
                    rpd.SetParameterValue(19, dt.Rows[0]["ToDate"].ToString());
                    rpd.SetParameterValue(20, dt.Rows[0]["TotalDays"].ToString());
                    rpd.SetParameterValue(21, "Leave Application : Approved");
                }

            }

            DataTable dtsclinfo = new DataTable();
            dtsclinfo = Classes.commonTask.LoadShoolInfo();
            rpd.SetParameterValue(0, "HR Department");
            rpd.SetParameterValue(1, dtsclinfo.Rows[0]["Address"].ToString());
            rpd.SetParameterValue(27, dtsclinfo.Rows[0]["SchoolName"].ToString());
            DataTable dtLeaveConfig = new DataTable();
            // dtLeaveConfig = ForLeaveReport.LeaveConfig();
            //sqlDB.fillDataTable("select * from tblLeaveConfig where CompanyId='" + dt.Rows[0]["CompanyId"].ToString() + "'", dtLeaveConfig);

            try
            {
                DataRow[] dr = dtLeaveStatus.Select("ShortName='c/l'", null);

                rpd.SetParameterValue(2, dr[0]["LeaveDays"]);    // for initial all c/l  of this year
                rpd.SetParameterValue(3, dr[0]["Amount"]);      // for used all c/l of this year
                rpd.SetParameterValue(4, dr[0]["Remaining"]);   // for remaining  c/l   of this year
            }
            catch
            {
                dtLeaveConfig = ForLeaveReport.LeaveConfig("c/l");
                rpd.SetParameterValue(2, dtLeaveConfig.Rows[0]["LeaveDays"]);    // for initial all c/l  of this year
                rpd.SetParameterValue(3, "0");      // for used all c/l of this year
                rpd.SetParameterValue(4, dtLeaveConfig.Rows[0]["LeaveDays"]);   // for remaining  c/l   of this year          
            }

            try
            {
                DataRow[] dr = dtLeaveStatus.Select("ShortName='s/l'", null);
                rpd.SetParameterValue(5, dr[0]["LeaveDays"]);    // for initial all s/l  of this year
                rpd.SetParameterValue(6, dr[0]["Amount"]);      // for used all s/l of this year
                rpd.SetParameterValue(7, dr[0]["Remaining"]);   // for remaining  s/l   of this year
            }
            catch
            {
                dtLeaveConfig = ForLeaveReport.LeaveConfig("s/l");
                if (dtLeaveConfig.Rows.Count > 0)
                {
                    rpd.SetParameterValue(5, dtLeaveConfig.Rows[0]["LeaveDays"]);    // for initial all s/l  of this year
                    rpd.SetParameterValue(6, "0");      // for used all s/l of this year
                    rpd.SetParameterValue(7, dtLeaveConfig.Rows[0]["LeaveDays"]);   // for remaining  s/l   of this year  
                }
                else
                {
                    rpd.SetParameterValue(5, "0");    // for initial all s/l  of this year
                    rpd.SetParameterValue(6, "0");      // for used all s/l of this year
                    rpd.SetParameterValue(7, "0");   // for remaining  s/l   of this year  
                }
            }

            try
            {
                DataRow[] dr = dtLeaveStatus.Select("ShortName='a/l'", null);
                rpd.SetParameterValue(8, dr[0]["LeaveDays"]);    // for initial all a/l  of this year
                rpd.SetParameterValue(9, dr[0]["Amount"]);      // for used all a/l of this year
                rpd.SetParameterValue(10, dr[0]["Remaining"]);   // for remaining  a/l   of this year
            }
            catch
            {
                dtLeaveConfig = ForLeaveReport.LeaveConfig("a/l");
                if (dtLeaveConfig.Rows.Count > 0)
                {
                    rpd.SetParameterValue(8, dtLeaveConfig.Rows[0]["LeaveDays"]);    // for initial all a/l  of this year
                    rpd.SetParameterValue(9, "0");      // for used all a/l of this year
                    rpd.SetParameterValue(10, dtLeaveConfig.Rows[0]["LeaveDays"]);   // for remaining  a/l   of this year   
                }
                else
                {
                    rpd.SetParameterValue(8, "0");    // for initial all a/l  of this year
                    rpd.SetParameterValue(9, "0");      // for used all a/l of this year
                    rpd.SetParameterValue(10, "0");   // for remaining  a/l   of this year   
                }
            }
            try
            {
                DataRow[] dr = dtLeaveStatus.Select("ShortName='o/l'", null);
                rpd.SetParameterValue(26, dr[0]["LeaveDays"]);    // for initial all o/l  of this year
                rpd.SetParameterValue(24, dr[0]["Amount"]);      // for used all o/l of this year
                rpd.SetParameterValue(25, dr[0]["Remaining"]);   // for remaining  o/l   of this year
            }
            catch
            {
                dtLeaveConfig = ForLeaveReport.LeaveConfig("o/l");
                if (dtLeaveConfig.Rows.Count > 0)
                {
                    rpd.SetParameterValue(26, dtLeaveConfig.Rows[0]["LeaveDays"]);    // for initial all o/l  of this year
                    rpd.SetParameterValue(24, "0");      // for used all o/l of this year
                    rpd.SetParameterValue(25, dtLeaveConfig.Rows[0]["LeaveDays"]);   // for remaining o/l  of this year 
                }
                else
                {
                    rpd.SetParameterValue(26, "0");    // for initial all o/l  of this year
                    rpd.SetParameterValue(24, "0");      // for used all o/l of this year
                    rpd.SetParameterValue(25, "0");   // for remaining o/l  of this year 
                }
            }
            if (dt.Rows[0]["ShortName"].ToString().Equals("c/l")) rpd.SetParameterValue(14, "✔");
            else rpd.SetParameterValue(14, " ");

            if (dt.Rows[0]["ShortName"].ToString().Equals("s/l")) rpd.SetParameterValue(15, "✔");
            else rpd.SetParameterValue(15, " ");

            if (dt.Rows[0]["ShortName"].ToString().Equals("a/l")) rpd.SetParameterValue(16, "✔");
            else rpd.SetParameterValue(16, " ");

            if (dt.Rows[0]["ShortName"].ToString().Equals("m/l")) rpd.SetParameterValue(17, "✔");
            else rpd.SetParameterValue(17, " ");

            if (dt.Rows[0]["ShortName"].ToString().Equals("op/l")) rpd.SetParameterValue(22, "✔");
            else rpd.SetParameterValue(22, " ");

            if (dt.Rows[0]["ShortName"].ToString().Equals("o/l")) rpd.SetParameterValue(23, "✔");
            else rpd.SetParameterValue(23, " ");

            CrystalReportViewer1.ReportSource = rpd;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
        }
        private void loadLeaveListReport(string DateRange, string ReportType) // For Leave Balance Report 
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__LeaveListReport__"];
                rpd = new ReportDocument();
                //if (ReportType == "0")
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveList.rpt"));
                //else
                //    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveListForIndividual.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, DateRange.Replace('/', '-'));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void loadLeaveBalanceReport(string DateRange, string ReportType) // For Leave Balance Report 
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__LeaveBalanceReport__"];
                rpd = new ReportDocument();
                if (ReportType == "0")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveBalanceReport.rpt"));
                else
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveBalanceReportForIndividual.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, DateRange.Replace('/', '-'));
                rpd.SetParameterValue(1, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void loadYearlyLeaveStatus(string ReportType) // ForYearlyLeaveStatus
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__YearlyLeaveStatus__"];
                rpd = new ReportDocument();
                if (ReportType == "0")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveYearlyStatus.rpt"));
                else rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveYearlyStatusForIndividual.rpt")); // For Individual
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void loadApprovedRejectedReport(string DateRange, string ApprovedRejected) // ForYearlyLeaveStatus
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__LeaveApprovedRejected__"];
                rpd = new ReportDocument();
                if (ApprovedRejected == "All")
                {
                    ApprovedRejected = "Approved and Rejected";
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveApprovedRejectedReportALL.rpt"));
                }
                else rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Attendance//Leave//LeaveApprovedRejectedReport.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, DateRange.Replace('/', '-'));
                rpd.SetParameterValue(3, ApprovedRejected + " Leave List");
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        //----------------------------------------------------------------------------------------
        private void LoadDiscountList() // Discount List
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DiscountList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//DiscountList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());               
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadDiscountSummary(string DateRange)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DiscountSummary__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//DiscountSummary.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                string RealDateRange = DateRange.Replace('/','-');
                rpd.SetParameterValue(2, RealDateRange);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadFineListReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FineListReport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//FineList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadFineCollectionSummary()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__FineCollectionSummary__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Finance//FineCollectionSummary.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void LoadMarkSheetEntryList(string Shift, string Batch, string Group, string Section, string Exam)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__MarkSheet__"];
                byte index = byte.Parse(dt.Columns.Count.ToString());
                rpd = new ReportDocument();
                switch (index)
                {
                    case 2:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_2.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        break;
                    case 3:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_3.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        break;
                    case 4:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_4.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        break;
                    case 5:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_5.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());

                        break;
                    case 6:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_6.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        break;
                    case 7:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_7.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        break;
                    case 8:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_8.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        break;
                    case 9:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_9.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        break;
                    case 10:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_10.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        break;
                    case 11:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_11.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        break;
                    case 12:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_12.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        break;
                    case 13:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_13.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        break;
                    case 14:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_14.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        break;
                    case 15:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_15.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        break;
                    case 16:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_16.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        break;
                    case 17:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_17.rpt"));
                        rpd.SetDataSource(dt);
                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        break;
                    case 18:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_18.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        break;
                    case 19:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_19.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        break;
                    case 20:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_20.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        break;
                    case 21:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_21.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        break;
                    case 22:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_22.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        break;
                    case 23:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_23.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        break;
                    case 24:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_24.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        rpd.SetParameterValue(29, dt.Columns[23].ColumnName.ToString());
                        break;
                    case 25:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_25.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        rpd.SetParameterValue(29, dt.Columns[23].ColumnName.ToString());
                        rpd.SetParameterValue(30, dt.Columns[24].ColumnName.ToString());
                        break;
                    case 26:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_26.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        rpd.SetParameterValue(29, dt.Columns[23].ColumnName.ToString());
                        rpd.SetParameterValue(30, dt.Columns[24].ColumnName.ToString());
                        rpd.SetParameterValue(31, dt.Columns[25].ColumnName.ToString());

                        break;
                    case 27:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_27.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        rpd.SetParameterValue(29, dt.Columns[23].ColumnName.ToString());
                        rpd.SetParameterValue(30, dt.Columns[24].ColumnName.ToString());
                        rpd.SetParameterValue(31, dt.Columns[25].ColumnName.ToString());
                        rpd.SetParameterValue(32, dt.Columns[26].ColumnName.ToString());

                        break;
                    case 28:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_28.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        rpd.SetParameterValue(29, dt.Columns[23].ColumnName.ToString());
                        rpd.SetParameterValue(30, dt.Columns[24].ColumnName.ToString());
                        rpd.SetParameterValue(31, dt.Columns[25].ColumnName.ToString());
                        rpd.SetParameterValue(32, dt.Columns[26].ColumnName.ToString());
                        rpd.SetParameterValue(33, dt.Columns[27].ColumnName.ToString());
                        break;
                    case 29:
                        rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//MarksEntrySheet//MarksEntrySteet_29.rpt"));
                        rpd.SetDataSource(dt);

                        dt = new DataTable();
                        dt = (DataTable)Session["__JustColumnsName__"];
                        rpd.SetParameterValue(7, dt.Columns[1].ColumnName.ToString());
                        rpd.SetParameterValue(8, dt.Columns[2].ColumnName.ToString());
                        rpd.SetParameterValue(9, dt.Columns[3].ColumnName.ToString());
                        rpd.SetParameterValue(10, dt.Columns[4].ColumnName.ToString());
                        rpd.SetParameterValue(11, dt.Columns[5].ColumnName.ToString());
                        rpd.SetParameterValue(12, dt.Columns[6].ColumnName.ToString());
                        rpd.SetParameterValue(13, dt.Columns[7].ColumnName.ToString());
                        rpd.SetParameterValue(14, dt.Columns[8].ColumnName.ToString());
                        rpd.SetParameterValue(15, dt.Columns[9].ColumnName.ToString());
                        rpd.SetParameterValue(16, dt.Columns[10].ColumnName.ToString());
                        rpd.SetParameterValue(17, dt.Columns[11].ColumnName.ToString());
                        rpd.SetParameterValue(18, dt.Columns[12].ColumnName.ToString());
                        rpd.SetParameterValue(19, dt.Columns[13].ColumnName.ToString());
                        rpd.SetParameterValue(20, dt.Columns[14].ColumnName.ToString());
                        rpd.SetParameterValue(21, dt.Columns[15].ColumnName.ToString());
                        rpd.SetParameterValue(22, dt.Columns[16].ColumnName.ToString());
                        rpd.SetParameterValue(23, dt.Columns[17].ColumnName.ToString());
                        rpd.SetParameterValue(24, dt.Columns[18].ColumnName.ToString());
                        rpd.SetParameterValue(25, dt.Columns[19].ColumnName.ToString());
                        rpd.SetParameterValue(26, dt.Columns[20].ColumnName.ToString());
                        rpd.SetParameterValue(27, dt.Columns[21].ColumnName.ToString());
                        rpd.SetParameterValue(28, dt.Columns[22].ColumnName.ToString());
                        rpd.SetParameterValue(29, dt.Columns[23].ColumnName.ToString());
                        rpd.SetParameterValue(30, dt.Columns[24].ColumnName.ToString());
                        rpd.SetParameterValue(31, dt.Columns[25].ColumnName.ToString());
                        rpd.SetParameterValue(32, dt.Columns[26].ColumnName.ToString());
                        rpd.SetParameterValue(33, dt.Columns[27].ColumnName.ToString());
                        rpd.SetParameterValue(34, dt.Columns[28].ColumnName.ToString());
                        break;

                }
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Shift);
                rpd.SetParameterValue(3, Batch);
                rpd.SetParameterValue(4, Group);
                rpd.SetParameterValue(5, Section);
                Exam = Exam + "-" + Batch.Substring(Batch.Length - 4);
                rpd.SetParameterValue(6, Exam);

                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStudentSubjectList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__StudentSubjectList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ManagedSubject//StudentSubList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadStudentGrpSubjectList()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__StudentGrpSubjectList__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ManagedSubject//StudentGrpSubList.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void LoadTC(string clv,string mc,string b,string p) //casue of leaving(clv),moral charater(mc),Bahavoir(b),Progress(p)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__TC__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//TransferCertificate.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, clv);
                rpd.SetParameterValue(2,mc);
                rpd.SetParameterValue(3, b);
                rpd.SetParameterValue(4, p);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void Loadmonthlytestreport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__monthlytestreport__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//monthlytestreport.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void SemesterProgressReport(string ExamName,string IsFinal) 
        {

            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__progressreportsemester__"];
                rpd = new ReportDocument();
                if(IsFinal== "Independent")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//IndependentExamProgressReport.rpt"));
                else if (IsFinal=="True")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//YearlyExamProgressReport.rpt"));
                else
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//SemestersExamProgressReport.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
               rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                //rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
               rpd.SetParameterValue(1, ExamName);
               rpd.SetParameterValue(2,Server.MapPath("//Images//Logo//"+ dt.Rows[0]["LogoName"].ToString()));
               rpd.SetParameterValue(3, Server.MapPath("//Images//EmpSign//"+Classes.commonTask.getPrincipalSignature()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void AttendanceSheetInExamReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AttendanceInExam__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AttendanceSheetInExam.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void NumberSheetInExamReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__NumberSheetInExam__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ExamNumberSheet.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2,Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void AdmitCardReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__AdmitCard__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AdmitCard.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void ExamRoutineReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ExamRoutine__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ExamRoutine.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void ExamineeNumberReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__ExamineeNumber__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//ExamineeNumber.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.SetParameterValue(2, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

        private void AcademicTranscript(string ExamName, string IsFinal)
        {

            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__progressreportsemester__"];
                rpd = new ReportDocument();
                if (IsFinal == "Independent")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscript.rpt"));
                else if (IsFinal == "True")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscript.rpt"));
                else
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscript.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, ExamName);
                rpd.SetParameterValue(2, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                rpd.SetParameterValue(3, Server.MapPath("//Images//EmpSign//" + Classes.commonTask.getPrincipalSignature()));
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        //private void AcademicTranscriptWithMarks(string ExamName, string IsFinal)
        //{

        //    try
        //    {
        //        dt = new DataTable();
        //        dt = (DataTable)Session["__progressreportsemester__"];
        //        rpd = new ReportDocument();
        //        if (IsFinal == "Independent")
        //            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscriptWithMarks.rpt"));
        //        else if (IsFinal == "True")
        //            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscriptWithMarks.rpt"));
        //        else
        //            rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscriptWithMarks.rpt"));
        //        rpd.SetDataSource(dt);
        //        dt = new DataTable();
        //        dt = Classes.commonTask.LoadShoolInfo();
        //        rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
        //        rpd.SetParameterValue(1, ExamName);
        //        rpd.SetParameterValue(2, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
        //        rpd.SetParameterValue(3, Server.MapPath("//Images//EmpSign//" + Classes.commonTask.getPrincipalSignature()));
        //        CrystalReportViewer1.ReportSource = rpd;
        //        CrystalReportViewer1.HasToggleGroupTreeButton = false;
        //    }
        //    catch { }
        //}
        private void AcademicTranscriptWithMarks(string ExamName, string IsFinal)
        {

            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__progressreportsemester__"];
                rpd = new ReportDocument();
                if (IsFinal == "Independent")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscriptWithMarks_TMC.rpt"));
                else if (IsFinal == "True")
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscriptWithMarks_TMC.rpt"));
                else
                    rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//AcademicTranscriptWithMarks_TMC.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, ExamName);
                rpd.SetParameterValue(2, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                rpd.SetParameterValue(3, Server.MapPath("//Images//EmpSign//" + Classes.commonTask.getPrincipalSignature()));
                rpd.SetParameterValue(4, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void MeritListReport()
        {

            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__meritlist__"];
                rpd = new ReportDocument();              
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//MeritListReport.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, Server.MapPath("//Images//Logo//" + dt.Rows[0]["LogoName"].ToString()));
                rpd.SetParameterValue(2, dt.Rows[0]["Address"].ToString());
                            
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void EvaFinalGradeSheet() 
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EvaFinalGradeSheet__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//TeacherEvaluation//TE_FinalGradeSheet.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());                
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        
        private void EvaTeachersPerformanceRanking()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EvaTeachersPerformanceRanking__"];
                string SessionId = dt.Rows[0]["SessionId1"].ToString();
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//TeacherEvaluation//TE_TeachersPerformanceRanking.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();


                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                dt = new DataTable();
                dt = Classes.commonTask.LoadEavluator(SessionId);
                rpd.SetParameterValue(2, dt.Rows[0]["Evaluator"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void EvaDepartmentRank()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__EvaDepartmentRank__"];
                string SessionId = dt.Rows[0]["SessionId1"].ToString();
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//TeacherEvaluation//TE_DepartmentRanking.rpt"));
                rpd.SetDataSource(dt);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                dt = new DataTable();
                dt = Classes.commonTask.LoadEavluator(SessionId);
                rpd.SetParameterValue(2, dt.Rows[0]["Evaluator"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void IndividualPerformanceReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__IndividualPerformanceReport__"];
                
                string SessionId = dt.Rows[0]["SessionId1"].ToString();
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//TeacherEvaluation//TE_IndividualPerformance.rpt"));
                rpd.SetDataSource(dt);
              DataTable  dtd = new DataTable();
                dtd = (DataTable)Session["__IndividualPerformanceDetails__"];
                rpd.Subreports[0].SetDataSource(dtd);
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());                
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void DepartmentPerformanceReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__DepartmentPerformanceReport__"];
                
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//TeacherEvaluation//TE_DepartmentPerformanceReport.rpt"));
                rpd.SetDataSource(dt);                
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        private void SubIndicatorBasedPerformanceReport()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__SubIndicatorBasedPerformanceReport__"];
                
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//TeacherEvaluation//TE_SubIndicatorPerformanceReport.rpt"));
                rpd.SetDataSource(dt);                
                dt = new DataTable();
                dt = Classes.commonTask.LoadShoolInfo();
                rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }
        
        private void Loadprogressreportsemester()
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)Session["__progressreportsemester__"];
                rpd = new ReportDocument();
                rpd.Load(Server.MapPath("//UI//Reports//CrystalReport//Examination//progressreportsemester.rpt"));
                rpd.SetDataSource(dt);
                //dt = new DataTable();
                //dt = Classes.commonTask.LoadShoolInfo();
                //rpd.SetParameterValue(0, dt.Rows[0]["SchoolName"].ToString());
                //rpd.SetParameterValue(1, dt.Rows[0]["Address"].ToString());
                rpd.Subreports[2].SetDataSource(dt);
                rpd.Subreports[3].SetDataSource(dt);
                dt = new DataTable();
                dt = (DataTable)Session["__grading__"];
                dt.TableName = "Grading";
                rpd.Subreports[0].SetDataSource(dt);
                dt = new DataTable();
                dt = (DataTable)Session["__monthlytest__"];
                rpd.Subreports[1].SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rpd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
            }
            catch { }
        }

    }
}