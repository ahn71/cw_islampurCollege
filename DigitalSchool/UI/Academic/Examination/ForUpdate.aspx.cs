using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;
using System.Text;
using DS.PropertyEntities.Model.Examinition;
using DS.BLL.Examinition;
using DS.DAL;

namespace DS.UI.Academic.Examination
{
    public partial class ForUpdate1 : System.Web.UI.Page
    {
        DataTable dt;
        string getValue;
        string getTblData;
        SqlCommand cmd;
        SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                getTblData = Request.QueryString["tbldata"];
                getValue = Request.QueryString["val"];

                /* value splited for set parameter in every update function*/

                string[] getTableName = getTblData.Split(',');

                /* call function for by using parameters*/

                if (getTableName[0].Equals("CurrentStudentInfo")) updateRollForClassNine(getTableName[2]);
                else if (getTableName[0].Equals("OptionalSubjectInfo")) updateOptionalSubjectForClassNine(getTableName[2]);
                else if (getTableName[0].Equals("ParticularsLoad")) loadParticulars(getValue);
                else if (getTableName[1].Equals("Marks")) marksEntry(getTableName, getValue);  // marks enterd in marksheet
                else if (getTableName[0].Contains("Faculty_Staff_AttendanceSheet_")) FacultyNStaffAttendanceCount();  // faculty and staff attendence count
                else if (getTableName[0].Equals("NewSubInfo")) getSubjectType(getTableName[1]);  // for find subject type
                else if (getTableName[0].Equals("CourseSubInfo")) getCouseWaysSubId(getTableName[1]); // for get subject id by course id
                else if (getTableName[0].Equals("getCourseList")) getCourseListbySubJectId(getTableName[1]);
                else if (getTableName[0].Equals("ExamDependencyInfo"))deleteExamDependencyInfo(getTableName[1]);
                else StudentsAttendanceCount();     // Daily  student attendance count 
            }
            catch { }
        }

        private void StudentsAttendanceCount()   // update daily attendance for every student
        {
            try
            {
                string[] getTableData = getTblData.Split(',');
                cmd = new SqlCommand("update " + getTableData[0] + " set " + getTableData[1] + "='" + getValue.ToLower() + "' where StudentId=@StudentId", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@StudentId", getTableData[2]);
                cmd.ExecuteNonQuery();



                if (getValue.Equals("P") || getValue.Equals("p"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    da = new SqlDataAdapter("select * from StudentAbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND StudentId=" + getTableData[2] + "", DbConnection.Connection);
                    da.Fill(dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        cmd = new SqlCommand("delete from StudentAbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND StudentId=" + getTableData[2] + "", DbConnection.Connection);
                        cmd.ExecuteNonQuery();
                    }
                }

                else if (getValue.Equals("A") || getValue.Equals("a"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    DataTable dtFam = new DataTable();
                    sqlDB.fillDataTable("select AbsentFineAmount From AbsentFine Order By AFId DESC", dtFam);

                    cmd = new SqlCommand("insert into StudentAbsentDetails (BatchName,StudentId,AbsentDate,AbsentFine) values(@BatchName,@StudentId,@AbsentDate,@AbsentFine) ", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@BatchName", getTableData[3]);
                    cmd.Parameters.AddWithValue("@StudentId", getTableData[2]);
                    cmd.Parameters.AddWithValue("@AbsentDate", getTotalDate[1] + " - " + justDate + " - " + getTotalDate[2]);
                    cmd.Parameters.AddWithValue("@AbsentFine", dtFam.Rows[0]["AbsentFineAmount"].ToString());
                    cmd.ExecuteNonQuery();
                    saveAsStudentFine(getTableData[2], getTableData[3]);
                }
            }
            catch { }
        }

        private void saveAsStudentFine(string studentId, string batch)   // save student fine for absent
        {
            try
            {
                dt = new DataTable(); ;
                da = new SqlDataAdapter("select * from studentFine where StudentId=" + studentId + " AND PayDate is null AND FinePurpose='absent'", DbConnection.Connection);
                da.Fill(dt);

                DataTable dtFam = new DataTable();
                sqlDB.fillDataTable("select AbsentFineAmount From AbsentFine Order By AFId DESC", dtFam);

                if (dt.Rows.Count > 0)
                {
                    float fine = float.Parse(dtFam.Rows[0]["AbsentFineAmount"].ToString()) + float.Parse(dt.Rows[0]["Fineamount"].ToString());
                    cmd = new SqlCommand("update StudentFine set Fineamount=@Fineamount where StudentId=" + studentId + " AND PayDate is null AND FinePurpose='absent'", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@Fineamount", fine);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("insert into studentFine (BatchName,StudentId,FinePurpose,Fineamount,Discount) values (@BatchName,@StudentId,@FinePurpose,@Fineamount,@Discount)", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@BatchName", batch);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@FinePurpose", "absent");
                    cmd.Parameters.AddWithValue("@Fineamount", dtFam.Rows[0]["AbsentFineAmount"].ToString());
                    cmd.Parameters.AddWithValue("@Discount", 0);
                    cmd.ExecuteNonQuery();
                }

            }
            catch { }

        }

        private void updateRollForClassNine(string getStudentId)    // set roll for every new student of class nine
        {
            try
            {
                cmd = new SqlCommand("update CurrentStudentInfo set rollNo =" + getValue + " where StudentId=" + getStudentId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void updateOptionalSubjectForClassNine(string getStudentId)   // set optional subject name for every student of class nine
        {
            try
            {
                cmd = new SqlCommand("update OptionalSubjectInfo set SubId =" + getValue + "where StudentId=" + getStudentId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void loadParticulars(string dataId)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatId", dataId) };
                sqlDB.fillDataTable("Select CatPId, FeeCatId, PId, Amount from ParticularsCategory where FeeCatId=@FeeCatId ", prms, dt);
                string divInfo = "";
                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Fee Catetory Name</th>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric'>Amount</th>";

                divInfo += "<th class='numeric control'>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["FeeCatId"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["PId"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Amount"].ToString() + "</td>";

                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/gridImages/edit.png'  onclick='editParticularCat(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                Response.Write(divInfo);
            }
            catch { }
        }

        private void marksEntry(string[] getNeededInfo, string getMarks)   // marks enterd in marksheet
        {
            try
            {
                if (getMarks == "")
                {
                    cmd = new SqlCommand("Update " + getNeededInfo[0] + " set " + getNeededInfo[1] + "=Null,ConvertMarks=Null ,IsPassed=Null,TotalConvertMarksOfSub=Null where ExInId='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND SubQPId=" + getNeededInfo[4] + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                }
                else {
                    // frist time compare marks are acurade
                    if (float.Parse(getMarks) > float.Parse(getNeededInfo[5])) { Response.Write("Not Save"); return; }
                    // for entered subject mark of examinations

                    //convert marks as 100% for get accurate result of dependency exam.so all time every marks must be convert in 100%
                    string getConvertMarks = "0";
                    string TotalConvertMarksOfSub = "0";
                    if (float.Parse(getNeededInfo[6]) > 0)
                    {

                        string GetHundradePurcentMarrks = Math.Round((double.Parse(getMarks.ToString()) * 100) / double.Parse(getNeededInfo[7]), 0).ToString();
                        getConvertMarks = Math.Round((double.Parse(GetHundradePurcentMarrks) * double.Parse(getNeededInfo[6]) / 100), 0).ToString();

                        DataTable dtSubCourseTotal = new DataTable();
                        SQLOperation.selectBySetCommandInDatatable("select sum(Marks) as Marks from " + getNeededInfo[0] + " where ExInId='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND SubId=" + getNeededInfo[8] + " AND CourseId=" + getNeededInfo[9] + " AND SubQPId <> " + getNeededInfo[4] + "", dtSubCourseTotal, DbConnection.Connection);
                        float Marks = (dtSubCourseTotal.Rows[0]["Marks"].ToString().Trim() == "") ? 0 : float.Parse(dtSubCourseTotal.Rows[0]["Marks"].ToString());
                        Marks += float.Parse(getMarks);

                        GetHundradePurcentMarrks = Math.Round((double.Parse(Marks.ToString()) * 100) / double.Parse(getNeededInfo[7]), 0).ToString();        //[7]  subject question pattern marks                 
                        TotalConvertMarksOfSub = Math.Round((double.Parse(GetHundradePurcentMarrks) * double.Parse(getNeededInfo[6]) / 100), 0).ToString();

                    }

                    bool IsPaased = (float.Parse(getMarks) < float.Parse(getNeededInfo[10])) ? false : true; // [10] index means pass marks

                    cmd = new SqlCommand("Update " + getNeededInfo[0] + " set " + getNeededInfo[1] + "=" + getValue + ",ConvertMarks=" + getConvertMarks + ",IsPassed='" + IsPaased + "',TotalConvertMarksOfSub=" + TotalConvertMarksOfSub + " where ExamID='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND SubQPId=" + getNeededInfo[4] + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update " + getNeededInfo[0] + " set TotalConvertMarksOfSub='0' where ExamID='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND SubId='" + getNeededInfo[8] + "' AND CourseId=" + getNeededInfo[9] + " AND SubQPId <> " + getNeededInfo[4] + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();



                    // for delete exists record as fail by exam and studentId and others 
                    cmd = new SqlCommand("delete from StudentFailList where ExInId='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND SubQPId=" + getNeededInfo[4] + "", DbConnection.Connection);
                    cmd.ExecuteNonQuery();

                    // for compare between marks.If predicate are true then enterd record as fail
                    if (!IsPaased)
                    {
                        cmd = new SqlCommand("insert into StudentFailList (StudentId,ExInId,SubQpId,getMarks) values (" + getNeededInfo[3] + ",'" + getNeededInfo[2] + "'," + getNeededInfo[4] + "," + getMarks + ")", DbConnection.Connection);
                        cmd.ExecuteNonQuery();
                    }
                }
                
                Response.Write("Yes Save");
            }
            catch (Exception ex) { Response.Write("Not Save"); }
        }
        private void FacultyNStaffAttendanceCount()   // update daily attendance for every student
        {
            try
            {
                string[] getTableData = getTblData.Split(',');
                cmd = new SqlCommand("update " + getTableData[0] + " set " + getTableData[1] + "='" + getValue.ToLower() + "' where EId=@EId", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@EId", getTableData[2]);
                cmd.ExecuteNonQuery();



                if (getValue.Equals("P") || getValue.Equals("p"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    da = new SqlDataAdapter("select * from Faculty_Staff_AbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND EId=" + getTableData[2] + "", DbConnection.Connection);
                    da.Fill(dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        cmd = new SqlCommand("delete from Faculty_Staff_AbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND EId=" + getTableData[2] + "", DbConnection.Connection);
                        cmd.ExecuteNonQuery();
                    }
                }

                else if (getValue.Equals("A") || getValue.Equals("a"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    cmd = new SqlCommand("insert into Faculty_Staff_AbsentDetails (EId,AbsentDate) values(@EId,@AbsentDate) ", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@EId", getTableData[2]);
                    cmd.Parameters.AddWithValue("@AbsentDate", getTotalDate[1] + " - " + justDate + " - " + getTotalDate[2]);
                    cmd.ExecuteNonQuery();
                    //  saveAsStudentFine(getTableData[2], getTableData[3]);
                }
            }
            catch { }
        }

        private void getSubjectType(string getSubId)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("select IsOptional,IsMandatory from NewSubject where SubId=" + getSubId + "", dt);
                string status = dt.Rows[0]["IsMandatory"].ToString() + "_" + dt.Rows[0]["IsOptional"].ToString() + "_ ";
                Response.Write(status);
            }
            catch { }
        }

        private void getCouseWaysSubId(string getCID)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("select SubId from AddCourseWithSubject Where CourseId=" + getCID + "", dt);
                Response.Write(dt.Rows[0]["SubId"].ToString() + "_");

            }
            catch { }
        }

        private void getCourseListbySubJectId(string SubId)
        {
            dt = new DataTable();
            sqlDB.fillDataTable("select CourseId,CourseName from AddCourseWithSubject Where SubId=" + SubId + "", dt);

            /*
            string sb = "";
            sb += "Select-0:";
            for (byte b = 0; b < dt.Rows.Count; b++)
            {
                sb += dt.Rows[b]["CourseName"].ToString() + "-" + dt.Rows[b]["CourseId"].ToString() + ":";
            }

            sb = sb.Remove(sb.LastIndexOf(':'));

            Response.Write(sb);
            */
            
            Response.Write("<option value=" + 0+ "> Select </option>");
  
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                
                Response.Write("<option value=" + dt.Rows[x]["CourseId"].ToString() + ">" + dt.Rows[x]["CourseName"].ToString() + "</option>");          
            }
           
        }

        private void deleteExamDependencyInfo(string getExamId)
        {
            try
            {
                bool result =CRUD.ExecuteQuery("delete from ExamDependencyInfo where ParentExInId='"+getExamId+"'");
                cmd.ExecuteNonQuery();

                DataBindForView();
            }
            catch { }
        }

        private void DataBindForView()
        {



            List<ExamDependencyInfoEntity> GetDependencyExamList = ExamDependencyInfoEntry.GetDependencyExamList; // for get all exam list
            string divInfo = "";

            if (GetDependencyExamList.Count == 0)
            {
                divInfo = "<div class='noData'>No Class available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
               
                Response.Write(divInfo);
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th> Final Exam Identity </th>";

            divInfo += "<th>Delete</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < GetDependencyExamList.Count; x++)
            {

                id = GetDependencyExamList[x].DeId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + GetDependencyExamList[x].ParentExInId + "</span></td>";


                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/delete.gif' class='editImg'   onclick='editExam(" + id + ");'  style='margin-left: 16px;'   />";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            Response.Write(divInfo);
        }
    }
}