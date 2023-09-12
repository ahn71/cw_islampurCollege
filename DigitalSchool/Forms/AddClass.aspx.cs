using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;


namespace DS.Admin
{
    public partial class AddClass : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        loadDesignationList("");
                    }
                }
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblClassId.Value.ToString().Length == 0 )
            {
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
                SqlCommand cmd = new SqlCommand("saveClasses", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassName", txtClassName.Text.Trim());
                cmd.Parameters.AddWithValue("@ClassOrder",txtOrder.Text.Trim());

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    createMarkSheetAccordingClass();   // for create marksheet 
                    loadDesignationList("");
                    lblMessage.InnerText = "success->Successfully saved";
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


        private Boolean updateClasses()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update Classes  Set ClassName=@ClassName,ClassOrder=@ClassOrder where ClassID=@ClassId", sqlDB.connection);
                cmd.Parameters.AddWithValue("@ClassId", lblClassId.Value);
                cmd.Parameters.AddWithValue("@ClassName", txtClassName.Text.Trim());
                cmd.Parameters.AddWithValue("@ClassOrder",txtOrder.Text.Trim());
                cmd.ExecuteNonQuery();
                renameTable();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);


                loadDesignationList("");
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
                cmd = new System.Data.SqlClient.SqlCommand("create table Class_" + txtClassName.Text.Trim().Replace(" ", "") + "MarksSheet (MarksSL bigint identity,ExId smallint,ExInId varchar(50),StudentId bigint,RollNo bigint,BatchName varchar(20),SectionName varchar(50),Shift varchar(7),SubQPId int,Marks float,ConvertTo float,GroupName varchar(20), Primary Key (MarksSl), Foreign Key (StudentId) References StudentProfile(StudentId) ON UPDATE CASCADE ON DELETE CASCADE,Foreign key (ExInId) References ExamInfo (ExInId) on update cascade on delete cascade,Foreign key (ExId) References ExamType (ExId) on update cascade on delete cascade )", sqlDB.connection);
                //else cmd = new System.Data.SqlClient.SqlCommand("create table Class_" + txtClassName.Text.Trim().Replace(" ", "") + "MarksSheet (MarksSL bigint identity,ExId smallint,ExInId varchar(50),StudentId bigint,RollNo bigint,BatchName varchar(20),SectionName varchar(50),Shift varchar(7),SubQPId int,Marks float,ConvertTo float, Primary Key (MarksSl), Foreign Key (StudentId) References StudentProfile(StudentId) ON UPDATE CASCADE ON DELETE CASCADE,Foreign key (ExInId) References ExamInfo (ExInId) on update cascade on delete cascade,Foreign key (ExId) References ExamType (ExId) on update cascade on delete cascade )", sqlDB.connection);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        private void renameTable()  // for table rename
        {
            try
            {
                if (hfClassName.Value.ToString().Trim()!=(txtClassName.Text.Trim()))
                {

                    cmd = new System.Data.SqlClient.SqlCommand("sp_rename  Class_" + hfClassName.Value.ToString().Trim()+ "MarksSheet,Class_" + txtClassName.Text.Trim().Replace(" ", "") + "MarksSheet", sqlDB.connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        private void loadDesignationList(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select ClassID, ClassName,ClassOrder from Classes  Order by ClassID ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Class available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divClassList.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>ClassName</th>";
            divInfo += "<th>Order</th>";
            divInfo += "<th>Edit</th>";

            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                id = dt.Rows[x]["ClassID"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                divInfo += "<td class='numeric_control'>" + dt.Rows[x]["ClassOrder"].ToString() + "</td>";
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