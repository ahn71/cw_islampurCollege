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
using DS.DAL;

namespace DS.UI.Academic.Attendance.Student.Manually
{
    public partial class ForUpdate1 : System.Web.UI.Page
    {
        DataTable dt;
        string getValue;
        string getTblData;
        SqlCommand cmd;
        SqlDataAdapter da;
        string[] getTableName;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // string value format = TableName,ShiftId,BatchId,ClsGroupId,SectionId,Date,ColumnName,StudentId
                getTblData = Request.QueryString["tbldata"];
                getValue = Request.QueryString["val"];

                /* value splited for set parameter in every update function*/

                getTableName = getTblData.Split(',');

                /* call function for by using parameters*/

                if (getTableName[0].Equals("CurrentStudentInfo")) updateRollForClassNine(getTableName[2]);
                else if (getTableName[0].Equals("OptionalSubjectInfo")) updateOptionalSubjectForClassNine(getTableName[2]);
                else if (getTableName[0].Equals("ParticularsLoad")) loadParticulars(getValue);
                else if (getTableName[1].Equals("Marks")) marksEntry(getTableName, getValue);  // marks enterd in marksheet              
                else StudentsAttendanceCount();     // Daily  student attendance count 
            }
            catch { }
        }

        private void StudentsAttendanceCount()   // update daily attendance for every student
        {
            try
            {
                // string value format = TableName,ShiftId,BatchId,ClsGroupId,SectionId,Date,ColumnName,StudentId
                
                string[] getTableData = getTblData.Split(',');
                string [] attDates=getTableData[5].Split('_');
                string StateStatus = (getValue == "a") ? "Absent" : "Present";
                attDates[0] = (attDates[0].Length == 1) ? "0" + attDates[0] : attDates[0];

                cmd = new SqlCommand("update " + getTableData[0] + " set AttStatus ='" + getValue.ToLower() + "',StateStatus='"+StateStatus+"',AttManual='Manual' where " +
                                     "StudentId=@StudentId AND Format(AttDate,'dd-MM-yyyy')=@AttDate", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@StudentId", getTableData[7]);
                cmd.Parameters.AddWithValue("@AttDate",attDates[0]+"-"+attDates[1]+"-"+attDates[2]);
                byte affectedRow=(byte)cmd.ExecuteNonQuery();

                if (affectedRow == 0)
                {
                    AddNewAttrecordAsAttendance(getTableName[7], getTableName[10], getTableName[2], getTableName[1], getTableName[4], getTableName[3], attDates[2] + "-" + attDates[1] + "-" + attDates[0], getValue, StateStatus, getTableName[11], getTableName[12], getTableName[13].Substring(0, getTableName[13].LastIndexOf(' ')), getTableName[14], getTableName[15], getTableName[16].Substring(0, getTableName[16].LastIndexOf(' ')), getTableName[17]);
                }

                if (getTableData[8].ToString() == "False") return;

                if (getValue.Equals("P") || getValue.Equals("p"))
                {

                    
                    da = new SqlDataAdapter("select * from StudentAbsentDetails where absentDate= '" + attDates[2] + "-" + attDates[1] + "-" + attDates[0] + "' " +
                                            "AND StudentId=" + getTableData[2] + "", DbConnection.Connection);
                    da.Fill(dt = new DataTable());
                   
                    if (dt.Rows.Count > 0)
                    {
                        
                        AbsentRecordDelete(attDates[2] + "-" + attDates[1] + "-" + attDates[0],getTableData[2]);
                       // saveAsStudentFine(getTableData[7], getTableData[2], false);
                    }
                }
                else if (getValue.Equals("A") || getValue.Equals("a"))
                {
                    AbsentRecordDelete(attDates[2] + "-" + attDates[1] + "-" + attDates[0], getTableData[2]);

                    cmd = new SqlCommand("insert into StudentAbsentDetails (BatchId,StudentId,AbsentDate,AbsentFine,IsPaid) " +
                                         "values(@BatchId,@StudentId,@AbsentDate,@AbsentFine,@IsPaid) ", DbConnection.Connection);
                    cmd.Parameters.AddWithValue("@BatchId", getTableData[2]);
                    cmd.Parameters.AddWithValue("@StudentId", getTableData[7]);
                    cmd.Parameters.AddWithValue("@AbsentDate", attDates[2] + " - " + attDates[1] + " - " + attDates[0]);                  
                    cmd.Parameters.AddWithValue("@AbsentFine", getTableData[9]);
                    cmd.Parameters.AddWithValue("@IsPaid","0");                  
                    cmd.ExecuteNonQuery();
                   // saveAsStudentFine(getTableData[7], getTableData[2],true);
                }
            }
            catch { }
        }

        public void AddNewAttrecordAsAttendance(string StudentId,string RollNo,string BatchId,string shiftId,string clsSecId,string ClsGrpId, string AttDate, string AttStatus, string StateStatus, string InH, string InM, string Ins, string OutH, string OutM, string OutS, string ALT)
        {
            try
            {
               // DataTable dt = CRUD.ReturnTableNull("select DesId From EmployeeInfo where EId=" + EId + "");
                cmd = new SqlCommand("insert into DailyAttendanceRecord (StudentId,RollNo,BatchId,ShiftId,ClsSecId,ClsGrpId,AttDate,AttStatus,StateStatus,AttManual,InHur,InMin,InSec,OutHur,OutMin,OutSec,DailyStartTimeALT_CloseTime) " +
                      "Values (" + StudentId + "," + RollNo + "," + BatchId + "," + shiftId + ",'" + clsSecId + "','" + ClsGrpId + "','"+AttDate+"',"+
                      "'"+AttStatus+"', '" + StateStatus + "','Manual'," + InH + "," + InM + "," + Ins + "," + OutH + "," + OutM + "," + OutS + ",'" + InH + ":" + InM + ":" + Ins + ":" + OutH + ":" + OutM + ":" + OutS + ":" + ALT + "')", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }

        }

        private void AbsentRecordDelete(string absentDate,string studentId)
        {
            try
            {
                cmd = new SqlCommand("delete from StudentAbsentDetails where absentDate= '" +absentDate+ "' " +
                                             "AND StudentId=" + studentId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void saveAsStudentFine(string studentId, string BatchId,bool IsIncrease)   // save student fine for absent
        {
            try
            {
                dt = new DataTable(); ;
                da = new SqlDataAdapter("select * from studentFine where StudentId=" + studentId + " AND PayDate is null AND FinePurpose='absent'", DbConnection.Connection);
                da.Fill(dt);

                DataTable dtFam = new DataTable();
                sqlDB.fillDataTable("select AbsentFineAmount From AbsentFine Order By AFId DESC", dtFam);
                if (dtFam.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        float fine = 0;
                        if (dtFam.Rows.Count > 0)
                        {
                          if(IsIncrease)   fine = float.Parse(dtFam.Rows[0]["AbsentFineAmount"].ToString()) + float.Parse(dt.Rows[0]["Fineamount"].ToString());
                          else fine = float.Parse(dtFam.Rows[0]["AbsentFineAmount"].ToString()) - float.Parse(dt.Rows[0]["Fineamount"].ToString());
                        }
                        cmd = new SqlCommand("update StudentFine set Fineamount=@Fineamount " +
                                             "where StudentId=" + studentId + " AND PayDate is null AND FinePurpose='absent'", DbConnection.Connection);
                        cmd.Parameters.AddWithValue("@Fineamount", fine);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into studentFine (BatchId,StudentId,FinePurpose,Fineamount,Discount) values " +
                                             "(@BatchId,@StudentId,@FinePurpose,@Fineamount,@Discount)", DbConnection.Connection);
                        cmd.Parameters.AddWithValue("@BatchId", BatchId);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@FinePurpose", "absent");
                        if (dtFam.Rows.Count > 0)
                        {
                            cmd.Parameters.AddWithValue("@Fineamount", 
                                (dtFam.Rows[0]["AbsentFineAmount"].ToString() == string.Empty) ? "0" : dtFam.Rows[0]["AbsentFineAmount"].ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Fineamount", "0");
                        }
                        cmd.Parameters.AddWithValue("@Discount", 0);
                        cmd.ExecuteNonQuery();
                    }
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
                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + 
                        "<img src='/Images/gridImages/edit.png'  onclick='editParticularCat(" + id + ");'  />";
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
                cmd = new SqlCommand("Update " + getNeededInfo[0] + " set " + getNeededInfo[1] + "=" + getValue + " where " +
                                     "ExInId='" + getNeededInfo[2] + "' AND StudentId=" + getNeededInfo[3] + " AND " +
                                     "SubQPId=" + getNeededInfo[4] + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
  
    
    }
}