using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;

namespace DS.Forms
{
    public partial class AssignBatch : System.Web.UI.Page
    {
       
        SqlDataAdapter da;
        DataTable dt;
        DataTable dt2;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadAllBatch();
                }
            }
        }

        
        private void loadAllBatch()
        {
            try
            {
                sqlDB.fillDataTable("select BatchName from BatchInfo where IsUsed="+0+"",dt=new DataTable());
                sqlDB.fillDataTable("select BatchName from BatchInfo where IsUsed=" +1+ "", dt2 = new DataTable());
                lstNotAssignedList.Items.Clear();
                lstAssignedList.Items.Clear();
              //  divNotAssign.InnerText = "List of all current batch";
                for (int b = 0; b < dt.Rows.Count; b++)
                {
                    lstNotAssignedList.Items.Add(dt.Rows[b]["BatchName"].ToString());
                }
            //    divAssigned.InnerText = "List of all used batch";
                for (int b = 0; b < dt2.Rows.Count; b++)
                {
                    lstAssignedList.Items.Add(dt2.Rows[b]["BatchName"].ToString());
                }
            }
            catch { }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
           
            batchAssign();
        }

        private void batchAssign()
        {
            try
            {            
                string getClass = new String(getBatchName.Where(Char.IsLetter).ToArray());
                sqlDB.fillDataTable("select Distinct ClassName from CurrentStudentInfo where ClassName='" + getClass + "'", dt = new DataTable());
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "Worning->Any Student is not exists at class "+getClass;
                    return;
                }
                sqlDB.fillDataTable("select BatchName from BatchInfo where BatchName='" +getBatchName+ "' AND IsUsed="+0+"",dt=new DataTable ());

                sqlDB.fillDataTable("select ClassName, ClassOrder from Classes", dt = new DataTable());

                var getOrder = dt.Select("className='" + getClass + "'");
                int getNewOrder = int.Parse(getOrder[0]["ClassOrder"].ToString()) + 1;
                var getNewClass = dt.Select("ClassOrder=" + getNewOrder + "");

                da = new SqlDataAdapter("select StudentId from CurrentStudentInfo where ClassName='" + getClass + "'",sqlDB .connection);
                da.Fill(dt=new DataTable ());

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("update CurrentStudentInfo set batchName='" + lstNotAssignedList.SelectedValue.ToString() + "' where ClassName='" + getClass + "'", sqlDB.connection);
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new SqlCommand("Update BatchInfo set IsUsed='1' where BatchName='" + lstNotAssignedList.SelectedValue.ToString() + "'",sqlDB.connection);
                    cmd.ExecuteNonQuery();
                    lblMessage.InnerText = "success->Successfully batch assigned !";
                    loadAllBatch();
                }
            }
            catch { }
        }
        string getBatchName;
        protected void lstNotAssignedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBatchName = lstNotAssignedList.SelectedValue.ToString();
        }

       
    }
}