using adviitRuntimeScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class TeacherWeeklyLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                teacherWeeklyLoad();
            }
            catch { }
        }

        static DataSet ds;
        static DataSet dpws;
        string totalTable;
        int clm = 0;
        private void teacherWeeklyLoad()
        {
            try
            {
                DataTable dtEId = new DataTable();
                sqlDB.fillDataTable("Select distinct EId, DName From v_ClassRoutine Order By DName ", dtEId);

                DataTable dt;
                ds = new DataSet();
                for (int i = 0; i < dtEId.Rows.Count; i++)
                {
                    sqlDB.fillDataTable("Select EId,EName,DName,BatchId,SubName,TCodeNo From v_ClassRoutine Where EId='" + dtEId.Rows[i]["EId"] + "' ", dt = new DataTable());
                    ds.Tables.Add(dt);
                }

                string divInfo = "";
                divInfo += "<div style='width:100%'>"; 
                divInfo = " <table id='tblClassRoutine' class='displayRoutine'  > ";
                divInfo += "<thead>";
                int b=0;
                for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                {
                    if (ds.Tables[x].Rows.Count > clm)
                    {
                        if (b == 4)
                        {
                            divInfo += "<tr>";
                            b = 0;
                        }
                        divInfo += "<th>" + ds.Tables[x].Rows[0]["EName"] + "(" + ds.Tables[x].Rows[0]["TCodeNo"] + ")" + "<br/>" + ds.Tables[x].Rows[0]["DName"] + " <br/>(Weekly Class : " + ds.Tables[x].Rows.Count + ")</th>";
                        clm++;
                        b++;
                    }
                    else divInfo += "<th> &nbsp; </th>";

                    clm = 0;
                    //divInfo += "</tr>";
                }

                divInfo += "</thead>";
                divInfo += "</table>";
                divTeacherWeeklyLoad.Controls.Add(new LiteralControl(divInfo));

                lblYear.Text = DateTime.Now.Year.ToString();

            }
            catch { }
        }
    }
}