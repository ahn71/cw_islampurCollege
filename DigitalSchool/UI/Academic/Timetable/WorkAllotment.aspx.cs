using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.HR;
using DS.BLL.HR;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using DS.BLL.ManagedBatch;
using DS.PropertyEntities.Model.ManagedBatch;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using System.Data;
using DS.BLL.GeneralSettings;
using DS.BLL.ControlPanel;
using DS.BLL.Timetable;


namespace DS.UI.Academic.Timetable
{
    public partial class WorkAllotment : System.Web.UI.Page
    {
        ClassSubjectEntry classSubjectEntry;
        EmpDepartmentEntry empDepartment;
        List<EmpDepartment> EmpDeptList;
        List<SubjectTeacher> SubjectTeacherList;
        SubjectTeacherEntry subjectTeacherEntry;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "WorkAllotment.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                ShiftEntry.GetDropDownList(ddlShift);
            }

            
          //  TeacherWorkloadCount();                          
        }
        private void getClassSubject()
        {
            try
            {
                int? id = null;
                var divInfo = string.Empty;
                if (classSubjectEntry == null)
                {
                    classSubjectEntry = new ClassSubjectEntry();
                }
                List<ClassSubject> CSList = classSubjectEntry.GetEntitiesData;     // to get all subject and teacher list  

                List<ClassEntities> OnlyClassList = ClassSubjectEntry.OnlyClassList; // to get just class list
                BatchEntry be = new BatchEntry();
                List<BatchEntities> BList = be.GetEntitiesData();  // to get batch current active batch list

                divInfo += "<div class='tgPanel'>";  // div 1

                foreach (ClassEntities cl in OnlyClassList)
                {
                    //if (cl.ClassID == 480)
                    //{
                       

                        

                        DataTable dt = new DataTable();
                        sqlDB.fillDataTable("select Distinct BatchId,BatchName,ClsGrpID,ClsSecID,SectionName from v_Tbl_Class_Section where ClassId=" + cl.ClassID + " AND IsUsed='True' Order by ClsGrpID", dt);
                        DataTable dtSCList=new DataTable ();
                                      
                        dtSCList = ClassSubjectEntry.GetSubjec_Course_tList(cl.ClassID,"0");  // to get subject list of specifict class
                     
                        byte b = 0;

                        if (dt.Rows.Count > 0 && dtSCList != null)
                        {
                           // divInfo += "<div class='tgPanelHead'>" + cl.ClassName + ":"+ dt.Rows[0]["BatchName"].ToString() + "</div>";  // for every class title+batch title 
                            divInfo += "<div class='tgPanelHead' id='" + dt.Rows[0]["BatchId"].ToString() + "'>" + dt.Rows[0]["BatchName"].ToString() + "</div>";  // for every class title+batch title 
                            divInfo += "<div class='row'>";
                        }

                        while (b < dt.Rows.Count && dtSCList != null)  // any batch is exists
                        {

                            if (dt.Rows[b]["ClsGrpID"].ToString() != "0")  // if any group is exists
                            {

                                DataRow[] dr = dt.Select("ClsGrpID=" + dt.Rows[b]["ClsGrpID"].ToString() + "");
                                for (byte c = 0; c < dr.Length; c++)
                                {
                                   
                                    DataTable dtGrpId = new DataTable();
                                    sqlDB.fillDataTable("select GroupId from Tbl_Class_Group where ClsGrpId=" + dr[c]["ClsGrpID"].ToString() + "", dtGrpId);

                                    dtSCList = new DataTable();
                                    dtSCList = ClassSubjectEntry.GetSubjec_Course_tList(cl.ClassID, dtGrpId.Rows[0]["GroupId"].ToString()); 

                                    for (byte s = 0; s < dtSCList.Rows.Count; s++)
                                    {
                                        if (s == 0)
                                        {
                                            DataTable dtGroupName = new DataTable();
                                            sqlDB.fillDataTable("select GroupName from Tbl_Group where GroupID =(select GroupId from Tbl_Class_Group where ClsGrpId=" + dt.Rows[b]["ClsGrpID"].ToString() + ")", dtGroupName);

                                            divInfo += "<div class='col-md-6' style='font-weight: bold; font-size: 20px;background-color: white;text-align: center;'>Group : " + dtGroupName.Rows[0]["GroupName"].ToString() + " | Section : " + dt.Rows[b]["SectionName"].ToString() + ""; //   div 2

                                            divInfo += "<table id=t_" + cl.ClassID + " class='tbl-controlPanel'>";  // table start.tbl 1
                                        }
                                        divInfo += "<tr>";
                                        divInfo += "<td id=" + dtSCList.Rows[s]["SubjectId"].ToString() + ">" + dtSCList.Rows[s]["SubjectName"].ToString() + " " + dtSCList.Rows[s]["CourseName"].ToString() + "</td>";  // for show title of subject and course name 
                                        divInfo += "<td>" + DropDownListBind(int.Parse(dt.Rows[0]["BatchId"].ToString()), int.Parse(dt.Rows[b]["ClsGrpID"].ToString()), int.Parse(dt.Rows[b]["ClsSecId"].ToString()), int.Parse(dtSCList.Rows[s]["SubjectId"].ToString()), int.Parse(dtSCList.Rows[s]["CourseId"].ToString())) + "</td>"; //to get all teacher list for every subject               
                                        divInfo += "</tr>";




                                        if (s == dtSCList.Rows.Count - 1)
                                        {


                                            divInfo += "</table>";  // table close.tbl 1
                                            //divInfo += "<table class='tbl-controlPanel'><tr><td></td><td>"; // start tbl 2
                                            //divInfo += "<input id='btnSave' class='btn btn-primary' type='Submit' onclick='return SaveInput(" + id + ");' value='Save' name='btnSave'/>";
                                            //divInfo += "</td></tr></table>"; // close tbl 2
                                            divInfo += "</div>"; // div 2 close

                                            b++;
                                        }
                                    }

                                }

                                if (b == dt.Rows.Count) divInfo += "</div>";
                            }
                            else
                            {
                                for (byte s = 0; s < dtSCList.Rows.Count; s++)
                                {
                                    if (s == 0)
                                    {
                                        divInfo += "<div class='col-md-6' style='font-weight: bold; font-size: 20px;background-color: white;text-align: center;'>Section : " + dt.Rows[b]["SectionName"].ToString() + ""; //   div 2

                                        divInfo += "<table id=t_" + cl.ClassID + " class='tbl-controlPanel'>";  // table start.tbl 1
                                    }
                                    divInfo += "<tr>";
                                    divInfo += "<td id=" + dtSCList.Rows[s]["SubjectId"].ToString() + ">" + dtSCList.Rows[s]["SubjectName"].ToString() + " " + dtSCList.Rows[s]["CourseName"].ToString() + "</td>";  // for show title of subject and course name 
                                 //   divInfo += "<td>" + dropDownListBind(int.Parse(dtSCList.Rows[s]["SubjectId"].ToString()), cl.ClassID) + "</td>"; //to get all teacher list for every subject               
                                    divInfo += "<td>" + DropDownListBind(int.Parse(dt.Rows[0]["BatchId"].ToString()), int.Parse(dt.Rows[b]["ClsGrpID"].ToString()), int.Parse(dt.Rows[b]["ClsSecId"].ToString()), int.Parse(dtSCList.Rows[s]["SubjectId"].ToString()), int.Parse(dtSCList.Rows[s]["CourseId"].ToString())) + "</td>"; //to get all teacher list for every subject               
                                    divInfo += "</tr>";

                                    if (s == dtSCList.Rows.Count - 1)
                                    {


                                        divInfo += "</table>";  // table close.tbl 1
                                        //divInfo += "<table class='tbl-controlPanel'><tr><td></td><td>"; // start tbl 2
                                        //divInfo += "<input id='btnSave' class='btn btn-primary' type='Submit' onclick='return SaveInput(" + id + ");' value='Save' name='btnSave'/>";
                                        //divInfo += "</td></tr></table>"; // close tbl 2
                                        divInfo += "</div>"; // div 2 close
                                        b++;
                                    }
                                }

                                if (b == dt.Rows.Count) divInfo += "</div>";
                            }

                        }

                       
                   // }

                    
                }

                divInfo += "</div>";
                SubList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private List<EmpDepartment> TeacherWorkloadDataBind()
        {
            if (empDepartment == null)
            {
                empDepartment = new EmpDepartmentEntry();
            }
            if(EmpDeptList == null)
            {
                EmpDeptList = empDepartment.GetEntitiesData();
            }            
            return EmpDeptList;
        }

        private string DropDownListBind(int BatchId, int ClsGrpId, int ClsSecId, int subjectId, int CourseId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("<select  id='s:{0}' onchange='saveData(this)' class='input controlLength'><Option value ='0'>...Select...</Option>",BatchId+":"+ClsGrpId+":"+ClsSecId+":"+subjectId+":"+CourseId+":"+ddlShift.SelectedItem.Value));
                int? id = null;
                int teacherid = FindTeacherId(BatchId, ClsGrpId, ClsSecId, subjectId, CourseId);

                DataTable dtDepartment = new DataTable();
                sqlDB.fillDataTable("select Distinct DId,DName from v_EmployeeInfo where IsTeacher='true' ", dtDepartment);

                for (byte b = 0; b < dtDepartment.Rows.Count; b++)
                {
                    DataTable dtEmployeeList = new DataTable();
                    sqlDB.fillDataTable("select EId,Ename,TCodeNo From v_EmployeeInfo where IsActive='True' AND DId=" + dtDepartment.Rows[b]["DId"].ToString() + " AND (ShiftId="+ddlShift.SelectedItem.Value+" OR ForAllShift='True') ", dtEmployeeList);
                    for (byte c = 0; c < dtEmployeeList.Rows.Count; c++)
                    {
                        if (c == 0) sb.Append("<optgroup label='" + dtDepartment.Rows[b]["DName"].ToString() + "'>");
                        sb.Append(string.Format("<Option value ='{0}' {2}>{1}</Option>", int.Parse(dtEmployeeList.Rows[c]["EId"].ToString()), dtEmployeeList.Rows[c]["EName"].ToString() + " | " + dtEmployeeList.Rows[c]["TCodeNo"].ToString(), (int.Parse(dtEmployeeList.Rows[c]["EId"].ToString()) == teacherid ? "selected" : "")));
 
                        if (c == dtEmployeeList.Rows.Count - 1) sb.Append("</optgroup>");
                    }    
                }
                sb.Append("</select>");
                return sb.ToString();

            }
            catch { return null; }
        }

        private string dropDownListBind( int BatchId,int ClsGrpId,int ClsSecId, int subjectId,int CourseId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("<select  id='s_{0}' onchange='saveData(this)' class='input controlLength'><Option value ='0'>...Select...</Option>", subjectId));
                int? id = null;
                int teacherid = FindTeacherId(BatchId, ClsGrpId, ClsSecId, subjectId, CourseId);
                foreach (EmpDepartment empdept in TeacherWorkloadDataBind())
                {
                    if (id != null)
                    {
                        if (id != empdept.Department.DepartmentId)
                        {
                            sb.Append("</optgroup>");
                        }
                    }
                    if (empdept.Department.DepartmentId != id)
                    {
                        sb.Append("<optgroup label='" + string.Format(empdept.Department.DepartmentName) + "'>");
                        id = empdept.Department.DepartmentId;
                    }
                    sb.Append(string.Format("<Option value ='{0}' {2}>{1}</Option>", empdept.Employee.EmployeeId.ToString(), empdept.Employee.EmpName, (empdept.Employee.EmployeeId == teacherid ? "selected" : "")));
                }
                sb.Append("</select>");
                return sb.ToString();
            }
            catch { return null; }
        }
        private int FindTeacherId(int BatchId, int ClsGrpId, int ClsSecId, int subjectId, int CourseId)
        {
            try
            {
                DataTable dtTeachar=new DataTable ();
                sqlDB.fillDataTable("select EId from TeacherSubjectAllocation where BatchId=" + BatchId + " AND ClsGrpId=" + ClsGrpId + " AND ClasSecId=" + ClsSecId + " AND SubjectId=" + subjectId + " AND CourseId=" + CourseId + " AND ShiftId="+ddlShift.SelectedItem.Value+"", dtTeachar);
                if (dtTeachar.Rows.Count > 0) return int.Parse(dtTeachar.Rows[0]["EId"].ToString());
                else return 0;
            }
            catch { return 0; }
        }     
        private void TeacherWorkloadCount()
        {
            int? id = null;
            var divInfo = string.Empty;  
            divInfo += "<div class='tgPanel'>";
            foreach (EmpDepartment empdept in TeacherWorkloadDataBind())
            {
                if (id != null)
                {
                    if (id != empdept.Department.DepartmentId)
                    {
                        divInfo += "</table>";
                    }
                }
                if (empdept.Department.DepartmentId != id)
                {
                    divInfo += "<div class='tgPanelHead'>" + empdept.Department.DepartmentName + "</div>";
                    divInfo += "<table class='tbl-controlPanel table'>";
                    id = empdept.Department.DepartmentId;
                }
                divInfo += "<tr>";
                divInfo += "<td style='text-align:left;'>" + string.Format("{0}({1})", empdept.Employee.EmpName, (empdept.WorkLoad == null) ? 0 : empdept.WorkLoad) + "</td>";               
                divInfo += "</tr>";
            }
            divInfo += "</table>";
            divInfo += "</div>";
            //TeacherList.Controls.Add(new LiteralControl(divInfo));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string  insertSubTeacher(string subId, string teacherId, string classId)
        {
            string[] subjects;
            string[] teachers;   
            int classid = int.Parse(classId);
            string msg = "failed";
            bool result = true;
            if (subId != string.Empty && teacherId != string.Empty)
            {
                subjects = subId.Split(',');
                teachers = teacherId.Split(',');               
                for(int i =0; i < subjects.Length; i++)
                {
                    int subject = int.Parse(subjects[i]);
                    int teacher = int.Parse(teachers[i]);                   
                    if(subject != 0 && teacher != 0)
                    {
                        DS.PropertyEntities.Model.HR.SubjectTeacher subTeacher = new SubjectTeacher();
                        subTeacher.SubTecherId = 0;
                        subTeacher.TeacherId = teacher;
                        subTeacher.SubjectId = subject;
                        subTeacher.ClassId = classid;
                        DS.BLL.HR.SubjectTeacherEntry subTeachEntry = new SubjectTeacherEntry();
                        subTeachEntry.AddEntities = subTeacher;
                        result = subTeachEntry.SelectInsertUpdate();
                        if(result)
                        {
                            msg = "Save Successfully";
                        }
                    }
                }
            }
            return msg;
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getClassSubject();
            }
            catch { }
        }

        protected void btnLoadRepot_Click(object sender, EventArgs e)
        {
            DataTable dtL = new DataTable();
            if (ddlShift.SelectedValue == "0") { lblMessage.InnerText = "warning-> Please select any Shift"; return; }
            dtL = ForClassRoutineReport.return_dt_for_TeacherLoadReport("00", ddlShift.SelectedValue);
            if(dtL==null || dtL.Rows.Count==0)
            { lblMessage.InnerText = "warning-> Any record are not available"; return; }
            Session["__TeacherLoad__"] = dtL;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=TeacherLoad');", true);

        }
    }   
}