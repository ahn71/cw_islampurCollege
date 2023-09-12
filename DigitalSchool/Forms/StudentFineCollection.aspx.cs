using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DS.DAL.AdviitDAL;
using DS.BLL;

namespace DS.Forms
{
    public partial class StudentFineCollection : System.Web.UI.Page
    {
        static string studentId;
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
                        Classes.commonTask.loadBatch(dlBatch);
                        Classes.commonTask.loadSection(dlSection);
                    }
                }

            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadFeeFine("");
            Session["__fineCollection__"] = " ";
        }


        DataTable dt = new DataTable();
        private void loadFeeFine(string sqlCmd)   // generate studentfine information if his already fined
        {
            try
            {
                DataTable dtid = new DataTable();
                sqlDB.fillDataTable("Select StudentId,FullName from v_CurrentStudentInfo where BatchName='" + dlBatch.SelectedItem.Text + "' and RollNo='" 
                + txtRoll.Text.Trim() + "' and SectionName='" + dlSection.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text + "' ", dtid);
                studentId = dtid.Rows[0]["StudentId"].ToString();

                string divInfo = "";

                if (dtid.Rows.Count == 0)
                {
                    divInfo = "<div class='noData'>Student not found</div>";
   
                    divFineInfo.Controls.Add(new LiteralControl(divInfo));
                    lblStudentName.Text = "";
                    btnPayNow.Visible = false;
                    return;
                }

                btnPayNow.Visible = true; ;
                if(dtid.Rows.Count>0) lblStudentName.Text = "Name : " + dtid.Rows[0]["FullName"].ToString();

                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select FineId,FinePurpose, Fineamount from StudentFine where BatchName='" + dlBatch.SelectedItem.Text 
                + "' and Studentid='" + dtid.Rows[0]["StudentId"].ToString() + "' and (FineamountPaid is null or FineamountPaid=0) ";               
                sqlDB.fillDataTable(sqlCmd, dt);
                int totalRows = dt.Rows.Count;               
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Fee Fine</div>";
                    divFineInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblFine' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Fine Purpose</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Select</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Fine Amount</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FinePurpose"].ToString() + "</td>";
                    divInfo += "<td class='numeric'> <input onchange='checkFine(this)' type='checkbox' id='" + dt.Rows[x]["FinePurpose"].ToString() + "_" + dt.Rows[x]["FineId"].ToString() + "'  value='" + dt.Rows[x]["Fineamount"].ToString() + "' > </td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Fineamount"].ToString() + "</td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";          
                divFineInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoll.Text == "")
                {
                    lblMessage.InnerText = "warning->Input Roll Number";
                    return;
                }
                DataTable dtCheck = new DataTable();
                sqlDB.fillDataTable("Select PayStatus,FineAmount,AmountPaid From StudentPayment Where StudentId='" + studentId + "'", dtCheck);
                if (dtCheck.Rows[0]["AmountPaid"].ToString() == "0")
                {
                    lblMessage.InnerText = "warning->Go to Fees Collection";
                    Session["__SaveStatus__"] = "gotoFeesCollection";
                    return;
                }

                //-----------------------------------------------------------------------------------------------------------------

                DataTable dtstp = new DataTable();
                sqlDB.fillDataTable("Select StudentId,AmountPaid,StudentPaymentId,FineAmount From [dbo].[StudentPayment] Where StudentId='" + studentId + "' ", dtstp);
                SqlCommand cmd = new SqlCommand("Update StudentPayment  Set DateOfPayment=@DateOfPayment, PayStatus=@PayStatus, AmountPaid=@AmountPaid, "
                +"FineAmount=@FineAmount where StudentPaymentId=@StudentPaymentId", sqlDB.connection);
                cmd.Parameters.AddWithValue("@StudentPaymentId", dtstp.Rows[0]["StudentPaymentId"].ToString());
                cmd.Parameters.AddWithValue("@DateOfPayment", TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@PayStatus", "True");
                float totalAmountPaid = (float.Parse(Session["__TotalFineCollection__"].ToString()) + float.Parse(dtstp.Rows[0]["AmountPaid"].ToString()));
                cmd.Parameters.AddWithValue("@AmountPaid", totalAmountPaid);
                float fineAmount = (float.Parse(dtstp.Rows[0]["FineAmount"].ToString()) + float.Parse(Session["__TotalFineCollection__"].ToString()));
                cmd.Parameters.AddWithValue("@FineAmount", fineAmount);
                cmd.ExecuteNonQuery();
                txtRoll.Text = "";
                lblMessage.InnerText = "success->Payment Successfull";
            }
            catch (Exception ex) 
            {
                lblMessage.InnerText ="error->"+ ex.Message;
            }
        }


    }
}