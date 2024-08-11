using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.DAL;

namespace DS.Classes
{
    public class Exam
    {
       public static  DataTable dt;
        public static string sqlCmd = "";
        public static double getGPA(int optainMark)
        {
            try
            {
                if (optainMark >= 80) return 5;
                if (optainMark >= 70) return 4;
                if (optainMark >= 60) return 3.5;
                if (optainMark >= 50) return 3;
                if (optainMark >= 40) return 2;
                if (optainMark >= 33) return 1;
                else return 0;
            }
            catch (Exception ex) { return 0; }
        }
        public static double getGPA(float optainMark, float fullMark)
        {
            try
            {
                optainMark = (optainMark / fullMark) * 100;
                if (optainMark >= 80) return 5;
                if (optainMark >= 70) return 4;
                if (optainMark >= 60) return 3.5;
                if (optainMark >= 50) return 3;
                if (optainMark >= 40) return 2;
                if (optainMark >= 33) return 1;
                else return 0;
            }
            catch (Exception ex) { return 0; }
        }
        public static void getSubjects(DropDownList ddlSubject,string BatchId,string ClsGrpID, bool hasGroup, string ExInSl)
        {
            //----------for load subject question pattern by batchId and exam type----------------------
           
            if (hasGroup == false)
            {
                sqlCmd = "select distinct convert(varchar,SubId) +'_'+ convert(varchar,CourseId) as SubId,SubName+' '+ ISNULL(CourseName,'') as  SubName from v_SubjectQuestionPattern where BatchId='" + BatchId + "' AND ExId=(select ExId from ExamInfo where ExInSl ='" + ExInSl + "')";
            }
            else
            {
                sqlCmd = "select distinct convert(varchar,SubId) +'_'+ convert(varchar,CourseId) as SubId,SubName+' '+ ISNULL(CourseName,'') as  SubName from v_SubjectQuestionPattern where BatchId=" + BatchId + " AND ExId=(select ExId from ExamInfo where ExInSl ='" + ExInSl + "') AND ClsGrpID='" + ClsGrpID + "' ";    // specially for nine ten and upper from ten
            }
            dt = new DataTable();
            dt = CRUD.ReturnTableNull(sqlCmd); 
                ddlSubject.DataTextField = "SubName";
                ddlSubject.DataValueField = "SubId";
                ddlSubject.DataSource = dt;
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, new ListItem("...Select...", "0"));
            
        }
        public static string getGrade(double GPA)
        {
            try
            {
                if (GPA >= 5) return "A+";
                if (GPA >= 4) return "A";
                if (GPA >= 3.5) return "A-";
                if (GPA >= 3) return "B";
                if (GPA >= 2) return "C";
                if (GPA >= 1) return "D";

                else return "F";
            }
            catch (Exception ex) { return "F"; }
        }
        public static DataTable loadMarks(string ClassName, string ExamID, string BatchID, string ShiftID, string RollNo)
        {
            try
            {
                if (RollNo != "")
                    RollNo = " and RollNo='" + RollNo + "'";
                if (ShiftID != "0")
                    ShiftID = " and ShiftID='" + ShiftID + "'";
                dt = new DataTable();
                //sqlDB.fillDataTable("select StudentId,SubId,CourseId,sum(Marks) as Marks,sum(case when IsPassed=1 then 0 else 1 end) as IsFailed from Class_"+ ClassName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + BatchID + "' and ExamID ='" + ExamID + "' " + ShiftID + " " + RollNo + "  group by StudentId,SubId,CourseId order by StudentId", dt);
             
                string query = "select StudentId,m.SubId,m.CourseId,sum(m.Marks) as Marks,sum(case when IsPassed=1 then 0 else 1 end) as IsFailed,cs.Marks as FullMarks from Class_" + ClassName + "MarksSheet m inner join BatchInfo b on m.BatchId=b.BatchId inner join ClassSubject cs on b.ClassID=cs.ClassID and m.Subid=cs.SubId and m.CourseId=cs.Courseid where  m.BatchId='" + BatchID + "' and ExamID ='" + ExamID + "' " + ShiftID + " " + RollNo + "  group by StudentId,m.SubId,m.CourseId,cs.Marks order by StudentId";
                sqlDB.fillDataTable(query, dt);
                return dt;
            }
            catch (Exception ex) { return null; }

        }
        public static DataTable loadStudentsInfo(string ClassName, string ExamID, string BatchID, string ShiftID,string ClsGrpID,string ClsSecID, string RollNo)
        {
            try
            {
                if (RollNo != "")
                    RollNo = " and RollNo='" + RollNo + "'";
                if (ShiftID != "0")
                    ShiftID = " and ShiftID='" + ShiftID + "'";
                else
                    ShiftID = "";
                if (ClsSecID != "0")
                    ClsSecID = " and ClsSecID=" + ClsSecID;
                else
                    ClsSecID = "";

                dt = new DataTable();
                //sqlDB.fillDataTable("select distinct StudentId,RollNo,BatchID,ExamID,ClsGrpID,ClsSecID,ShiftId from Class_" + ClassName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + BatchID + "' and ClsGrpID="+ ClsGrpID + " and ExamID ='" + ExamID + "' " + ShiftID + ClsSecID + RollNo + "   order by StudentId", dt);
                sqlDB.fillDataTable("select distinct StudentId,RollNo,BatchID,ExamID,ClsGrpID,ClsSecID,ShiftId from Class_" + ClassName + "MarksSheet where  BatchId='" + BatchID + "' and ClsGrpID="+ ClsGrpID + " and ExamID ='" + ExamID + "' " + ShiftID + ClsSecID + RollNo + "   order by StudentId", dt);
                return dt;
            }
            catch (Exception ex) { return null; }

        }
        public static DataTable loadSubjectsByStudent(string BatchID, string StudentID)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("(select SubId,CourseId,1 as MsStatus,SubCode from ClassSubject where ClassID=(select ClassID from BatchInfo where BatchId=" + BatchID + ") and IsCommon=1 and IsOptional=0) union all (select SubId, CourseId, MSStatus,SubCode from v_StudentGroupSubSetupDetails where StudentId = " + StudentID + " and BatchId = " + BatchID + ")", dt);
                return dt;
            }
            catch (Exception ex) { return null; }

        }
        public static void LoadSubjectList(DropDownList dl,string ClassId)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select SubId,SubName From V_ClasswiseSubject where ClassID=" + ClassId + "", dt);
                dl.DataSource = dt;
                dl.DataTextField = "SubName";
                dl.DataValueField = "SubId";
                dl.DataBind();
                dl.Items.Add("...Select Subject...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static void LoadSubjectListWithAll(DropDownList dl, string ClassId)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select SubId,SubName From V_ClasswiseSubject where ClassID=" + ClassId + " order by Ordering", dt);
                dl.DataSource = dt;
                dl.DataTextField = "SubName";
                dl.DataValueField = "SubId";
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("All", "0"));
            }
            catch { }
        }
        public static void LoadSubjectListWithAll(DropDownList dl, string ClassId,string GroupID)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select convert(varchar,SubId)+'_'+convert(varchar,CourseId) as SubId,case when CourseId=0 then SubName else SubName+' '+ CourseName end as SubName From v_ClassSubjectList where ClassID=" + ClassId + " and GroupId in(0,(select  GroupID from Tbl_Class_Group where ClsGrpID="+ GroupID + ")) order by Ordering", dt);
                dl.DataSource = dt;
                dl.DataTextField = "SubName";
                dl.DataValueField = "SubId";
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("All", "0"));
            }
            catch { }
        }
        public static void LoadQuestionPattern(DropDownList dl,string ClassId,string SubId)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select QPId,QPName From v_SubjectQuestionPattern where ClassID=" + ClassId + " and SubId=" + SubId + "", dt);
                dl.DataSource = dt;
                dl.DataTextField = "QPName";
                dl.DataValueField = "QPId";
                dl.DataBind();
                dl.Items.Add("...Select Pattern...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static double LoadMarks(string ClassId,string SubjectId,string QPId)
        {
            try
            {
                double Marks = 0;
                dt = new DataTable();
                sqlDB.fillDataTable("Select QMarks From v_SubjectQuestionPattern where ClassID=" + ClassId + " and SubId=" + SubjectId + " and QPId=" + QPId + " ", dt);
                Marks = double.Parse(dt.Rows[0]["QMarks"].ToString());
                return Marks;
            }
            catch { return 0; }
        }
        public static void LoadClassExamSetup(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct ClassID,ClassName From V_ExamSetup", dt);
                dl.DataSource = dt;
                dl.DataTextField = "ClassName";
                dl.DataValueField = "ClassID";
                dl.DataBind();
                dl.Items.Add("...Select Class...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static void LoadExamTypeExamSetup(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct ExId,ExName From V_ExamSetup", dt);
                dl.DataSource = dt;
                dl.DataTextField = "ExName";
                dl.DataValueField = "ExId";
                dl.DataBind();
                dl.Items.Add("...Select Exam type...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static void LoadExamTypeList(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select Distinct ExId,ExName From v_SubjectQuestionPattern", dt);
                dl.DataSource = dt;
                dl.DataTextField = "ExName";
                dl.DataValueField = "ExId";
                dl.DataBind();
                dl.Items.Add("...Select Exam Type...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }

        public static void loadDependencyExam(DropDownList dl)
        {
            try
            {
                sqlDB.fillDataTable("select ExInId from ExamInfo where ExInDependency ='0' order by ExInSl desc", dt = new DataTable());
                dl.DataSource = dt;
                dl.DataTextField = "ExInId";
                
                dl.DataBind();
                //for (byte b = 0; b < dt.Rows.Count; b++)
                //{
                //    dl.Items.Add(dt.Rows[b]["ExName"].ToString());
                //}
            }
            catch { }
        }
        public static void LoadExamType(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select ExId,ExName from ExamType   Order by ExName ", dt);
                dl.DataSource = dt;
                dl.DataTextField = "ExName";
                dl.DataValueField = "ExId";
                dl.DataBind();
                dl.Items.Add("...Select...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }

        public static void loadExamId(DropDownList dl)
        {
            try
            {
                DataTable dt = new DataTable();
                SQLOperation.selectBySetCommandInDatatable("select ExInId from ExamInfo order by ExInSl desc", dt, sqlDB.connection);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dl.Items.Add(dt.Rows[i]["ExInId"].ToString());
                }
            }
            catch { }
        }


        public static void loadPeriod(DropDownList dl)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select PeriodName,PeriodId From PeriodSetting", dt);
                dl.DataSource = dt;
                dl.DataTextField = "PeriodName";
                dl.DataValueField = "PeriodId";
                dl.DataBind();
                dl.Items.Add("...Select...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static DataTable loadExamList()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select ExInSl, ExInId, ExName, GroupName, IsPublished,case when IsPublished = 1 then 'Published' else 'Unpublished' end Status   from v_ExamInfo order by ExInSl desc", dt);
               return  dt;
            }
            catch { return null; }
        }
    }
}