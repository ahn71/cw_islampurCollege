using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;
using DS.DAL;
using DS.DAL.AdviitDAL;
using Microsoft.VisualBasic;

namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class AddClass : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddClass.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                loadClassList("");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblClassId.Value.ToString().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
                saveClasses();
            }
            else
            {
                updateClasses();
            }
        }
        private Boolean saveClasses()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveClasses", DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassName", txtClassName.Text.Trim());
                cmd.Parameters.AddWithValue("@ClassOrder", txtOrder.Text.Trim());
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    createMarkSheetAccordingClass();   // for create marksheet 
                    loadClassList("");
                    ClearControl();
                    lblMessage.InnerText = "success->Save Successfully";
                }
                else lblMessage.InnerText = "error->Unable to save";
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void ClearControl()
        {
            txtClassName.Text = string.Empty;
            txtOrder.Text = string.Empty;
        }
        private Boolean updateClasses()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Classes  SET ClassName=@ClassName,ClassOrder=@ClassOrder where ClassID=@ClassId", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@ClassId", lblClassId.Value);
                cmd.Parameters.AddWithValue("@ClassName", txtClassName.Text.Trim());
                cmd.Parameters.AddWithValue("@ClassOrder", txtOrder.Text.Trim());
                cmd.ExecuteNonQuery();
                renameTable();       // marks table renaem, if any marsk type table are exists under of the class
                renameBatchName();  // bathcname renaem, if any batch are exists under of the class
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                loadClassList("");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void createMarkSheetAccordingClass()
        {
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("CREATE TABLE Class_" + txtClassName.Text.Trim().Replace(" ", "") 
                    + "MarksSheet (MarksSL bigint identity,ExId smallint,ExInId varchar(100),StudentId bigint,RollNo bigint,"
                + "BatchId int,ClsSecId smallint,ShiftId smallint,SubId int,CourseId tinyint,SubQPId int,Marks float,ConvertToPercentage float,ConvertMarks float,"
                + "ClsGrpID smallint,IsPassed bit,TotalConvertMarksOfSub float, Primary Key (MarksSl), Foreign Key (StudentId) References CurrentStudentInfo(StudentId)"
                +" ON UPDATE CASCADE ON DELETE CASCADE,Foreign key (ExInId) References ExamInfo (ExInId) on update cascade on "
                + "delete cascade,Foreign key (ExId) References ExamType (ExId) on update cascade on delete cascade,"
                +" Foreign key (BatchId) references BatchInfo (BatchId) on update cascade on delete cascade,"
                + " Foreign key (ClsSecId) references Tbl_Class_Section (ClsSecId) on update cascade on delete cascade,"
                + " Foreign Key (ShiftId) references ShiftConfiguration (ConfigId) on update cascade on delete cascade, "
                + " Foreign Key (SubQPId) references SubjectQuestionPattern (SubQPId) on update cascade on delete cascade)", DbConnection.Connection);                
                cmd.ExecuteNonQuery();

                createTotalResultProcess_Table("Class_" + txtClassName.Text.Trim().Replace(" ", "") + "MarksSheet");
            }
            catch { }
        }
        private void renameTable()  // for table rename
        {
            try
            {
                if (hfClassName.Value.ToString().Trim() != (txtClassName.Text.Trim()))
                {
                    cmd = new System.Data.SqlClient.SqlCommand("sp_rename  Class_" + hfClassName.Value.ToString().Trim() 
                    + "MarksSheet,Class_" + txtClassName.Text.Trim().Replace(" ", "") + "MarksSheet", DbConnection.Connection);
                    cmd.ExecuteNonQuery();

                    cmd = new System.Data.SqlClient.SqlCommand("sp_rename  Class_" + hfClassName.Value.ToString().Trim()
                     + "MarksSheet_TotalResultProcess,Class_" + txtClassName.Text.Trim().Replace(" ", "") + "MarksSheet_TotalResultProcess", DbConnection.Connection);
                    cmd.ExecuteNonQuery();


                }
            }
            catch { }
        }

        private void renameBatchName()
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select BatchId,BatchName from BatchInfo where ClassID=" + lblClassId.Value + "");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    string getYear = Strings.Right(dt.Rows[i]["BatchName"].ToString(), 4);
                    getYear = txtClassName.Text.Trim().Replace(" ", "") + getYear;
                    cmd = new SqlCommand("update BatchInfo set BatchName='" + getYear + "' where BatchId =" + dt.Rows[i]["BatchId"].ToString() + "",DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        private void createTotalResultProcess_Table(string getClassName)
        {
            try
            {
                cmd = new SqlCommand("Create Table "+getClassName+"_TotalResultProcess "
                    + "(Sl bigint Primary Key Identity(1,1),ExInId varchar(100),StudentId bigint,RollNo bigint,BatchId int,ClsSecId smallint,ShiftId smallint,"
                    + "SubId bigint,IsOptional bit,CourseId int,QPId bigint,Marks float,ClsGrpID smallint,MarksOfAllPatternBySCId float,GradeOfAllPatternBySCId varchar(2),PointOfAllPatternBySCId float,"
                    + "MarksOfSubject_WithAllDependencySub float,GradeOfSubject_WithAllDependencySub varchar(2),PointOfSubject_WithAllDependencySub float,"
                    + " Foreign key (ExInId) references ExamInfo (ExInId) on update cascade on delete cascade,"
                    + "Foreign key (StudentId) references CurrentStudentInfo (StudentId) on update cascade on delete cascade,"
                    + "Foreign key (BatchId) references BatchInfo (BatchId) on update cascade on delete cascade,"
                    + "Foreign key (ClsSecId) references Tbl_Class_Section (ClsSecId) on update cascade on delete cascade,"
                    + "Foreign key (ShiftId) references ShiftConfiguration (ConfigId) on update cascade on delete cascade,"
                    + "Foreign key (SubId) references NewSubject (SubId) on update cascade on delete cascade,"
                    + "Foreign key (QPId) references QuestionPattern (QPId) on update cascade on delete cascade)",DbConnection.Connection);

                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void loadClassList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select ClassID, ClassName,ClassOrder from Classes  Order by ClassOrder ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);
            int totalRows = dt.Rows.Count;
            string divInfo = "";
            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Order</th>";
            divInfo += "<th>ClassName</th>";            
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody></table>";               
                divClassList.Controls.Add(new LiteralControl(divInfo));
                return;
            }            
            string id = "";
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["ClassID"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td class='numeric_control'>" + dt.Rows[x]["ClassOrder"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";                
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editClass(" + id + ");'  />";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
            divClassList.Controls.Add(new LiteralControl(divInfo));
        }
    }
}