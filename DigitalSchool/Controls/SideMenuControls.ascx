<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuControls.ascx.cs" Inherits="DS.Controls.SideMenuControls" %>
<div id="sidebar" class="nav-collapse">
    <!-- sidebar menu start-->
    <div id="divMain" runat="server" class="leftside-navigation">
        <ul class="sidebar-menu" id="nav-accordion">
            <li>
                <a class="active" runat="server" href="~/Dashboard.aspx">
                    <i class="fa fa-dashboard"></i>
                    <span>Dashboard</span>
                </a>
            </li>
            <li class="sub-menu" runat="server" id="liAcademicModuleDB">
                <a href="javascript:;">
                    <i class="fa fa-book"></i>
                    <span>Academic</span>
                </a>
                <ul class="sub-menu">
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Students</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a runat="server" href="~/UI/Academic/Students/OldStudentEntry.aspx">Old Student Entry Form</a></li>
                            <li><a runat="server" href="~/UI/Academic/Students/StdAdmission.aspx">Admission Form</a></li>
                            <li><a runat="server" href="~/UI/Academic/Students/AdmissionDetails.aspx">Admission Details</a></li>
                            <li><a runat="server" href="~/UI/Academic/Students/AdmStdAssign.aspx">New Student Batch Assign</a></li>
                            <li><a runat="server" href="~/UI/Academic/Students/CurrentStudentInfo.aspx">All Current Student Info</a></li>
                            <li><a runat="server" href="~/UI/Academic/Students/StdPromotion.aspx">Student Promotion</a></li>
                            <%--<li><a runat="server" href="~/UI/Academic/Students/StudentAssign.aspx">Promotion(failed)</a></li> This is needed --%>                                                           
                            <%--<li><a runat="server" href="~/UI/Academic/Students/IndividualBatchAssign.aspx">Batch Assign(Specific)</a></li>--%>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Attendance</span>
                        </a>
                        <ul class="sub-menu">
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Student</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a id="A14" runat="server" href="~/UI/Academic/Attendance/Student/Machine/import_data.aspx">Machine Attendance</a></li> 
                                     <li><a id="A15" runat="server" href="~/UI/Academic/Attendance/Student/Manually/StudentAttendance.aspx">Manually Attendance</a></li> 
                                    <li><a id="A16" runat="server" href="~/UI/Academic/Attendance/Student/Manually/AbsentDetails.aspx">Attendance Details</a></li>                                    
                                </ul>
                            </li>
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Staff or Faculty</span>
                                </a>
                                <ul class="sub-menu">
                                   <li><a id="A17" runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/emp_import_data.aspx">Machine Attendance</a></li> 
                                     <li><a id="A18" runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyAttendance.aspx">Manually Attendance</a></li> 
                                    <li><a id="A19" runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/FacultyStaffAbsentDetails.aspx">Attendance Details</a></li> 
                                </ul>
                            </li>
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Leave</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Academic/Attendance/Leave/application.aspx">Application</a></li> 
                                    <li><a runat="server" href="~/UI/Academic/Attendance/Leave/for_approve_leave_list.aspx">Leave Approved</a></li>                                   
                                    <li><a runat="server" href="~/UI/Academic/Attendance/Leave/leave_configuration.aspx">Configuration</a></li>
                                </ul>
                            </li>
                            <li class="sub-menu" runat="server" id="li1">
                                <a id="A9" runat="server" href="~/UI/Academic/Attendance/Student/Manually/OffDaysSet.aspx">                                   
                                    <span>Off Days Settings</span>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Examination</span>
                        </a>
                        <ul class="sub-menu">
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Subject Management</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Academic/Examination/ManagedSubject/NewSubject.aspx">New Subject</a></li>
                                    <li><a id="A2" runat="server" href="~/UI/Academic/Examination/ManagedSubject/AddCourseWithSubject.aspx">Subject Wise Course </a></li>
                                    <li><a runat="server" href="~/UI/Academic/Examination/ManagedSubject/ClassSubjectSetup.aspx">Class Wise Subject Setup</a></li>
                                     <li><a id="A1" runat="server" href="~/UI/Academic/Examination/ManagedSubject/SetOptionalSubject.aspx">Set Optional Subject</a></li>                                       
                                </ul>
                            </li>
                            <li><a runat="server" href="~/UI/Academic/Examination/AddExam.aspx">Exam Type</a></li>
                            <li><a runat="server" href="~/UI/Academic/Examination/QuestionPattern.aspx">Question Pattern</a></li>                                    
                            <li><a runat="server" href="~/UI/Academic/Examination/SubjectQuestionPattern.aspx">Subject Wise Question Pattern</a></li>                            
                            <li><a runat="server" href="~/UI/Academic/Examination/ExamInfo.aspx">Exam Info</a></li>
                            <li><a runat="server" href="~/UI/Academic/Examination/Grading.aspx">Grading</a></li>
                            <li><a runat="server" href="~/UI/Academic/Examination/MarksEntry.aspx">Marks Entry</a></li>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Timetable</span>
                        </a>
                        <ul class="sub-menu">
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Class Room Allocation</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Academic/Timetable/RoomAllocation/ManagedBuildings.aspx">Buildings Management</a></li>
                                    <li><a runat="server" href="~/UI/Academic/Timetable/RoomAllocation/Allocated.aspx">Classroom Allocated To Building</a></li>
                                </ul>
                            </li>
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Set Timings</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/ShiftConfig.aspx">Shift Configuration</a></li>
                                    <li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/CreateWeekdays.aspx">Create Weekly Days</a></li>
                                    <li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/OffDaysSet.aspx">Off Days Settings</a></li>
                                    <li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/ClassTimeSetName.aspx">Class Time Set Name</a></li>
                                    <li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/ClassTimeSpecification.aspx">Class Time Specification</a></li>
                                     <li><a id="A12" runat="server" href="~/UI/Academic/Timetable/SetTimings/ExamTimeSetName.aspx">Exam Time Set Name</a></li>
                                    <li><a id="A13" runat="server" href="~/UI/Academic/Timetable/SetTimings/ExamTimeSpecification.aspx">Exam Time Specification</a></li>
                                    <%--<li><a runat="server" href="~/UI/Academic/Timetable/SetTimings/SessionDateTime.aspx">Session Date Time</a></li>--%>
                                </ul>
                            </li> 
                            <li><a id="A10" runat="server" href="~/UI/Academic/Timetable/WorkAllotment.aspx">Work Allotment</a></li>                           
                            <li><a runat="server" href="~/UI/Academic/Timetable/SetClassTimings.aspx">Set Class Timings</a></li>
                            <li><a id="A11" runat="server" href="~/UI/Academic/Timetable/SetExamTimings.aspx">Set Exam Timings</a></li>                                 
                           
                        </ul>
                    </li>
                </ul>
            </li>
            <li class="sub-menu" runat="server" id="liAdministrationModuleDB">
                <a href="javascript:;">
                    <i class="fa fa-wheelchair"></i>
                    <span>Administration</span>
                </a>
                <ul class="sub-menu">
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Finance</span>
                        </a>
                        <ul class="sub-menu">
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Fee Management</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/AddParticular.aspx">Particulars</a></li>                                    
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeesCategoriesInfo.aspx">Fee Category</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmissionFeesCategories.aspx">Admission Fee Category</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/ParticularCategories.aspx">Fee Particulars</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmissionAssignParticular.aspx">Admission Fee Particulars</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/DateOfPayment.aspx">Set Fee Date</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/DiscountSet.aspx">Discount</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeesCollection.aspx">Fee Collection</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmFeesCollection.aspx">Admission Collection</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeCollectionDetails.aspx">Fee Collection Details</a></li>
                                    <li><a id="A4" runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmCollectionDetails.aspx">Admission Collection Details</a></li>
                                </ul>
                            </li>
                            <li class="sub-menu">
                                <a id="A3" runat="server" href="~/UI/Administration/Finance/FineManaged/StudentFineCollection.aspx">
                                    <i class=""></i>
                                    <span>Student Fine Collection</span>
                                </a>
                            </li>                            
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Human Resource</span>
                        </a>
                        <ul class="sub-menu">
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Employee</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpRegForm.aspx">Employee Registration Form</a></li>                                    
                                    <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpDetails.aspx">Employee Details</a></li>
                                    <li><a runat="server" href="~/UI/Administration/HR/Employee/AddDepartment.aspx">Department</a></li>
                                    <li><a runat="server" href="~/UI/Administration/HR/Employee/AddDesignation.aspx">Designation</a></li>
                                </ul>
                            </li>
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Payroll</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a runat="server" href="~/UI/Administration/HR/Payroll/SalarySetDetails.aspx">Salary Set Details</a></li>                                    
                                    <li><a runat="server" href="~/UI/Administration/HR/Payroll/SalaryAllowanceType.aspx">Salary Allowance Type</a></li>
                                    <li><a runat="server" href="~/UI/Administration/HR/Payroll/SalaryDetailsInfo.aspx">View Salary Details Info</a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <%--<li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Inventory</span>
                        </a>
                    </li>--%>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Control Panel</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a id="A20" runat="server" href="~/UI/Administration/Users/UserType.aspx">Add User Type</a></li>
                            <li><a id="A21" runat="server" href="~/UI/Administration/Users/UserRegister.aspx">Add User Account</a></li> 
                            <li><a id="A22" runat="server" href="~/UI/Administration/Users/UserAccountList.aspx">List of Account</a></li>
                            <li><a id="A23" runat="server" href="~/UI/Administration/Users/StudentAccount.aspx">Add Student Account</a></li> 
                            <li><a id="A24" runat="server" href="~/UI/Administration/Users/StudentAccountList.aspx">List of Student Account</a></li>
                            <li><a id="A25" runat="server" href="~/UI/Administration/Users/ChangePageInfo.aspx">Page Setup & Set Privilege</a></li> 
                            <li><a id="A26" runat="server" href="~/UI/Administration/Users/OffMainModule.aspx">Main Module Privilege</a></li>                                                     
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Settings</span>
                        </a>
                        <ul class="sub-menu">
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Academic Settings</span>
                                </a>
                                <ul class="sub-menu">                                                                       
                                    <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AddClass.aspx">Class</a></li>                                    
                                    <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AddSection.aspx">Section</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManageGroup.aspx">Group</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManageClassGroup.aspx">Manage Class Group</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManageClassSection.aspx">Manage Class Section</a></li>
                                    <li><a id="A27" runat="server" href="~/UI/Administration/Settings/AcademicSettings/ManagedBatch/CreateBatch.aspx">Create Batch</a></li>     
                                </ul>
                            </li>
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>General Settings</span>
                                </a>
                                <ul class="sub-menu">                                                                        
                                    <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddDistrict.aspx">District</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddThana.aspx">Thana Upazila</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AddBoard.aspx">Board</a></li>
                                    <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/AttendanceSettings.aspx">Attendance Settings</a></li>                                    
                                    <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/SchoolSetup.aspx">School Setup</a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>                    
                </ul>
            </li>
            <li class="sub-menu" runat="server" id="liReportsModuleDB">
                <a href="javascript:;">
                    <i class="fa fa-paste"></i>
                    <span>Reports</span>
                </a>
                <ul class="sub-menu">
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Students</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a runat="server" href="~/UI/Reports/Students/IndivisualStudentList.aspx">Individual Student Profile</a></li>
                            <li><a runat="server" href="~/UI/Reports/Students/StudentList.aspx">Student List</a></li>
                            <li><a runat="server" href="~/UI/Reports/Students/GenderwiseStdList.aspx">Gender Wise Student List</a></li>
                            <li><a runat="server" href="~/UI/Reports/Students/StudentContactList.aspx">Student Contact List</a></li>
                            <%--<li><a runat="server" href="~/UI/Reports/Students/CourseWiseStudent.aspx">Course Wise Student</a></li>--%>
                            <li><a runat="server" href="~/UI/Reports/Students/GuardianInformation.aspx">Guardian Information</a></li>
                            <li><a runat="server" href="~/UI/Reports/Students/GuardianContactList.aspx">Guardian Contact List</a></li>
                            <li><a runat="server" href="~/UI/Reports/Students/ParentsInformationList.aspx">Parents Information List</a></li>
                            <li><a runat="server" href="~/UI/Reports/Students/AdmitCardGenerator.aspx">Admit ID Card</a></li>
                            <%--<li><a runat="server" href="~/UI/Reports/Students/DueList.aspx">Due List</a></li>--%>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Staff or Faculty</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a runat="server" href="~/UI/Reports/StafforFaculty/ProfileStafforFaculty.aspx">Profile Staff or Faculty</a></li>
                            <li><a runat="server" href="~/UI/Reports/StafforFaculty/EmployeeList.aspx">Staff or Faculty List</a></li>
                            <li><a runat="server" href="~/UI/Reports/StafforFaculty/DepartmentwiseReport.aspx">Department Wise Report</a></li>
                             <li><a id="A5" runat="server" href="~/UI/Reports/StafforFaculty/DesignationwiseReport.aspx">Designation Wise Report</a></li>
                            <li><a id="A6" runat="server" href="~/UI/Reports/StafforFaculty/BloodGroup.aspx">Blood Group</a></li>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Schedule</span>
                        </a>
                         <ul class="sub-menu">
                            <li class="sub-menu">
                                <a runat="server" href="~/UI/Reports/TimeTable/ExamRoutine.aspx">
                                   <i class=""></i>
                                    <span>Exam Schedule</span>
                                </a>
                              
                            </li>
                            <li class="sub-menu">
                                <a href="javascript:;">
                                    <i class=""></i>
                                    <span>Class Schedule</span>
                                </a>
                                <ul class="sub-menu">
                                    <li><a id="A8" runat="server" href="~/UI/Reports/TimeTable/ClassRoutineReport.aspx">Class Wise</a></li>
                                    <li><a id="A7" runat="server" href="~/UI/Reports/TimeTable/ClassRoutine For_Teacher.aspx">Teacher Wise</a></li>                                   
                                    
                                </ul>
                            </li>                           
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Attendace</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a runat="server" href="~/UI/Reports/Attendance/MonthWiseAttendanceSheet.aspx">Student Attendance</a></li>
                            <li><a runat="server" href="~/UI/Reports/Attendance/MonthWiseAttendanceSheetSummary.aspx">Staff/Faculty Attendance</a></li>
                            <%--<li><a runat="server" href="~/UI/Reports/Attendance/IndividualAbsentDetails.aspx">Individual Student Absent</a></li>
                            <li><a runat="server" href="~/UI/Reports/Attendance/AttendanceInfoDetails.aspx">Attendance Info Details</a></li>--%>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Student Fine</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a runat="server" href="~/UI/Reports/StudentFine/StudentFineList.aspx">Fine Collection List</a></li>
                        </ul>
                    </li>
                    <li class="sub-menu">
                        <a href="javascript:;">
                            <i class=""></i>
                            <span>Examination</span>
                        </a>
                        <ul class="sub-menu">
                            <li><a runat="server" href="~/UI/Reports/Examination/ExamReports.aspx">Examination</a></li>
                            <li><a runat="server" href="~/UI/Reports/Examination/ExamOverView.aspx">Exam OverView</a></li>
                            <%--<li><a runat="server" href="~/UI/Reports/Examination/TeacherWiseResult.aspx">Teacher Ways Result</a></li>--%>
                            <li><a runat="server" href="~/UI/Reports/Examination/AcademicTranscript.aspx">Academic Transcript</a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <%--<li class="sub-menu">
                <a href="javascript:;">
                    <i class="fa fa-bullhorn"></i>
                    <span>Notification</span>
                </a>
                <ul class="sub-menu">
                    <li class="sub-menu">
                        <a href="javacript:;">
                            <i class=""></i>
                            <span>SMS</span>
                        </a>
                        <ul class="sub-menu">                            
                            <li><a href="#">Send SMS To Students</a></li>
                            <li><a href="#">Send SMS To Parents</a></li>
                            <li><a href="#">Send SMS To Staff or Faculty</a></li>
                            <li><a href="#">Result Published</a></li>
                            <li><a href="#">Send SMS To Absent Student's Parent</a></li>
                        </ul>
                    </li>
                </ul>
            </li>--%>
            <li class="sub-menu" runat="server" id="liNotificationModuleDB">
                <a runat="server" href="~/UI/Notification/SendSMS.aspx">
                    <i class="fa fa-bullhorn"></i>
                    <span>Notification</span>
                </a>                
            </li>
        </ul>
    </div>
    <!-- sidebar menu end-->
</div>
