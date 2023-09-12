using DS.BLL.ManagedSubject;
using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS
{
    public partial class ForUpdate : System.Web.UI.Page
    {
        DataTable dt;
        string getValue;
        string getTblData;
        SqlCommand cmd;
        SqlDataAdapter da;
        OptionalSubjectEntry opSubEntry;
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
                else if (getTableName[0].Equals("OptionalSubjectInfo")) updateOptionalSubjectForClassNine(getTableName[2],getTableName[3],getValue);
                else if (getTableName[0].Equals("ParticularsLoad")) loadParticulars(getValue);
                else if (getTableName[1].Equals("Marks")) marksEntry(getTableName, getValue);  // marks enterd in marksheet
                else if (getTableName[0].Contains("Faculty_Staff_AttendanceSheet_")) FacultyNStaffAttendanceCount();  // faculty and staff attendence count
                else StudentsAttendanceCount();     // Daily  student attendance count 
            }
            catch { }
        }

        private void StudentsAttendanceCount()   // update daily attendance for every student
        {
            try
            {
                string[] getTableData = getTblData.Split(',');
                cmd = new SqlCommand("update " + getTableData[0] + " set " + getTableData[1] + "='" + getValue.ToLower() + "' where StudentId=@StudentId", sqlDB.connection);
                cmd.Parameters.AddWithValue("@StudentId", getTableData[2]);
                cmd.ExecuteNonQuery();
                if (getValue.Equals("P") || getValue.Equals("p"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    da = new SqlDataAdapter("select * from StudentAbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND StudentId=" + getTableData[2] + "", sqlDB.connection);
                    da.Fill(dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        cmd = new SqlCommand("delete from StudentAbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND StudentId=" + getTableData[2] + "", sqlDB.connection);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (getValue.Equals("A") || getValue.Equals("a"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    DataTable dtFam = new DataTable();
                    sqlDB.fillDataTable("select AbsentFineAmount From AbsentFine Order By AFId DESC", dtFam);

                    cmd = new SqlCommand("insert into StudentAbsentDetails (BatchName,StudentId,AbsentDate,AbsentFine) values(@BatchName,@StudentId,@AbsentDate,@AbsentFine) ", sqlDB.connection);
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
                da = new SqlDataAdapter("select * from studentFine where StudentId=" + studentId + " AND PayDate is null AND FinePurpose='absent'", sqlDB.connection);
                da.Fill(dt);
                DataTable dtFam = new DataTable();
                sqlDB.fillDataTable("select AbsentFineAmount From AbsentFine Order By AFId DESC", dtFam);
                if (dt.Rows.Count > 0)
                {
                    float fine = float.Parse(dtFam.Rows[0]["AbsentFineAmount"].ToString()) + float.Parse(dt.Rows[0]["Fineamount"].ToString());
                    cmd = new SqlCommand("update StudentFine set Fineamount=@Fineamount where StudentId=" + studentId + " AND PayDate is null AND FinePurpose='absent'", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@Fineamount", fine);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("insert into studentFine (BatchName,StudentId,FinePurpose,Fineamount,Discount) values (@BatchName,@StudentId,@FinePurpose,@Fineamount,@Discount)", sqlDB.connection);
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
                cmd = new SqlCommand("update CurrentStudentInfo set rollNo =" + getValue + " where StudentId=" + getStudentId + "", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void updateOptionalSubjectForClassNine(string getStudentId,string batchId,string subId)   // set optional subject name for every student of class nine
        {
            try
            {
                if (opSubEntry == null)
                {
                    opSubEntry = new OptionalSubjectEntry();
                }
                OptionalSubjectEntities opsubEntities = new OptionalSubjectEntities();
                opsubEntities.Student = new CurrentStdEntities()
                {
                    StudentID = int.Parse(getStudentId)
                };
                opsubEntities.Batch = new BatchEntities()
                {
                    BatchId = int.Parse(batchId)
                };
                opsubEntities.Subject = new SubjectEntities()
                {
                    SubjectId = int.Parse(subId)
                };
                opSubEntry.AddEntities = opsubEntities;
               bool result=opSubEntry.Delete();
               if (result == true)
               {
                   opSubEntry.Insert(); 
               }
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

                cmd = new SqlCommand("Update " + getNeededInfo[0] + " set " + getNeededInfo[1] + "=" + getValue + " where ExInId='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND SubQPId=" + getNeededInfo[4] + "", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
        private void FacultyNStaffAttendanceCount()   // update daily attendance for every student
        {
            try
            {
                string[] getTableData = getTblData.Split(',');
                cmd = new SqlCommand("update " + getTableData[0] + " set " + getTableData[1] + "='" + getValue.ToLower() + "' where EId=@EId", sqlDB.connection);
                cmd.Parameters.AddWithValue("@EId", getTableData[2]);
                cmd.ExecuteNonQuery();



                if (getValue.Equals("P") || getValue.Equals("p"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    da = new SqlDataAdapter("select * from Faculty_Staff_AbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND EId=" + getTableData[2] + "", sqlDB.connection);
                    da.Fill(dt = new DataTable());
                    if (dt.Rows.Count > 0)
                    {
                        cmd = new SqlCommand("delete from Faculty_Staff_AbsentDetails where absentDate= '" + getTotalDate[2] + "-" + getTotalDate[1] + "-" + justDate + "' AND EId=" + getTableData[2] + "", sqlDB.connection);
                        cmd.ExecuteNonQuery();
                    }
                }

                else if (getValue.Equals("A") || getValue.Equals("a"))
                {
                    string[] getTotalDate = getTableData[1].Split('_');
                    string justDate = new String(getTotalDate[0].Where(Char.IsNumber).ToArray());

                    cmd = new SqlCommand("insert into Faculty_Staff_AbsentDetails (EId,AbsentDate) values(@EId,@AbsentDate) ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@EId", getTableData[2]);
                    cmd.Parameters.AddWithValue("@AbsentDate", getTotalDate[1] + " - " + justDate + " - " + getTotalDate[2]);
                    cmd.ExecuteNonQuery();
                    //  saveAsStudentFine(getTableData[2], getTableData[3]);
                }
            }
            catch { }
        }
    }
}