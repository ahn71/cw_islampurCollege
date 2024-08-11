using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Timetable
{
    public partial class ForUpdate : System.Web.UI.Page
    {
        DataTable dt;
        string getValue;
        string getTblData;
        SqlCommand cmd;
        SqlDataAdapter da;
        string[] getTableName;

        protected void Page_Load(object sender, EventArgs e)
        {
            // string value format = TableName,ShiftId,BatchId,ClsGroupId,SectionId,Date,ColumnName,StudentId
            getTblData = Request.QueryString["tbldata"];
            getValue = Request.QueryString["val"];

            /* value splited for set parameter in every update function*/

            getTableName = getTblData.Split(',');
            addTeacherAccordintToSubjectCourse();
        }

        private void addTeacherAccordintToSubjectCourse()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete from TeacherSubjectAllocation where BatchId=" + getTableName[1] + " AND ClsGrpId=" + getTableName[2] + " AND ClasSecId=" + getTableName[3] + " AND SubjectId=" + getTableName[4] + " AND CourseId=" + getTableName[5] + " AND ShiftId="+getTableName[6]+" ", DbConnection.Connection);
                cmd.ExecuteNonQuery();
                CRUD.ExecuteQuery("insert into TeacherSubjectAllocation (BatchId,ClsGrpId,ClasSecId,SubjectId,CourseId,EId,ShiftId) values ("+getTableName[1]+","+getTableName[2]+","+getTableName[3]+","+getTableName[4]+","+getTableName[5]+","+getValue+","+getTableName[6]+")");


            }
            catch { }
        }
    }
}