using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
namespace DS.Forms
{
    public partial class SetRollSubjectClassNine : System.Web.UI.Page
    {
        DataTable dt;
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
                    loadSection();
                    loadbatch();
                }
            }
        }

        private void loadSection()
        {
            dlSection.Items.Add("Select Group");
            dlSection.Items.Add("Science");
            dlSection.Items.Add("Commerce");
            dlSection.Items.Add("Arts");
            dlSection.SelectedIndex = 0;
        }

        private void loadbatch()
        {
            try
            {
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select BatchName from batchInfo where batchName like '%Nine%'", sqlDB.connection);
                da.Fill(dt=new DataTable ());
                dlBatch.Items.Add(dt.Rows[0].ItemArray[0].ToString());
            }
            catch { }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("select StudentId,FullName,RollNo,SubName from v_optionalSubjectInfo where BatchName='" + dlBatch.Text.Trim() + "' AND SectionName='" + dlSection.Text.Trim() + "' AND Shift='"+ddlShift.SelectedItem.Text.Trim()+"'", dt = new DataTable());
                DataTable dtOp = new DataTable();
                sqlDB.fillDataTable("Select SubId,SubName from NewSubject where IsOptional=" + 1 + "", dtOp);

                string tblInfo = "";
                tblInfo += "<Table id='tblSetRollOptionalSubject'>";
                tblInfo += "<th style='text-align: center;'>SL</th><th style='text-align: center;'>Name</th><th style='text-align: center;'>" +
                            "Roll</th><th style='text-align: center;'>Choose Optional Subject</th>";

                bool status = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    status = false;
                    tblInfo += "<tr><td>"+(i+1)+"</td><td>"+dt.Rows[i]["FullName"].ToString()+"</td>";

                    tblInfo += "<td><input onChange='saveData(this)'; style='width: 150px;' width=110px type=text id=CurrentStudentInfo_RollNo_" + dt.Rows[i]["StudentId"].ToString() + " Value=" + dt.Rows[i]["RollNo"].ToString() + "></td>";
                    tblInfo += "<td><select onchange='setOS(this)' style='width: 180px;' id=OptionalSubjectInfo_SubId_" + dt.Rows[i]["StudentId"].ToString() +">";
                    for (byte b = 0; b < dtOp.Rows.Count; b++)
                    {
                        if (dt.Rows[i]["SubName"].ToString().Equals(dtOp.Rows[b]["SubName"].ToString()))
                        {
                            tblInfo += "<option selected='selected' value=" + dtOp.Rows[b]["SubId"].ToString() + ">" + dtOp.Rows[b]["SubName"].ToString() + "</option>";
                            status = true;
                        }
                        else tblInfo += "<option value=" + dtOp.Rows[b]["SubId"].ToString() + ">" + dtOp.Rows[b]["SubName"].ToString() + "</option>";
                        if (b == dtOp.Rows.Count - 1 && !status) tblInfo += "<option selected='selected'>Select</option>";  
                    }
                    tblInfo+="</select></td></tr>";
                }
                tblInfo += "</table>";
                divStduentInfo.Controls.Add(new LiteralControl(tblInfo));
            }
            catch { }
        }
    }
}