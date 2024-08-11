using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using DS.BLL.ControlPanel;
using DS.DAL;
using System.IO;

namespace DS.UI.Administration.HR.Payroll
{
    public partial class SetSalary : System.Web.UI.Page
    {
        static string EmployeeId = "";
        static string saveAction = "";
        static string editAction = "";
        static double totalAllowance = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        string EditMode = "";
                        try { EditMode = (Request.QueryString["Edit"].ToString() != null) ? "Yes" : "No"; }
                        catch { }
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SetSalary.aspx", btnSave, EditMode)) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no"); 
                        loadEmployeeInfo();

                        if (Request.QueryString["Edit"] != "True")
                        {
                            txtSchoolSalary.Enabled = false;
                        }

                    }
            }
            catch { }
        }

        private void loadEmployeeInfo()
        {
            try
            {
                EmployeeId = Request.QueryString["TeacherId"];
                saveAction = Request.QueryString["Save"];
                editAction = Request.QueryString["Edit"];
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select ECardNo,convert(varchar(11),EJoiningDate,106) as EJoiningDate,EName,DName,DesName,EPictureName from v_EmployeeInfo where EId ="
                + Request.QueryString["TeacherId"].ToString() + "", dt);
                if (dt.Rows.Count.Equals(0))
                {
                    //etImage.ImageUrl = "http://www.placehold.it/150x150/EFEFEF/AAAAAA&text=no+image";
                    return;
                }
                if (dt.Rows[0]["ECardNo"].ToString() == "0")
                {
                    lblIndexNo.Text = "No Index";
                }
                else
                {
                    lblIndexNo.Text = dt.Rows[0]["ECardNo"].ToString();
                }

                lblJoiningDate.Text = dt.Rows[0]["EJoiningDate"].ToString();
                lblName.Text = dt.Rows[0]["EName"].ToString();
                lblDepartment.Text = dt.Rows[0]["DName"].ToString();
                lblDesignation.Text = dt.Rows[0]["DesName"].ToString();
                if (dt.Rows[0]["DesName"].ToString()!=""){
                     string url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[0]["EPictureName"].ToString());
                     imgProfile.ImageUrl = url;
                       }
                //etImage.ImageUrl = "~/Images/teacherProfileImage/" + dt.Rows[0]["EPictureName"].ToString();
                //etImage.ImageUrl = "<img src='http://www.placehold.it/150x150/EFEFEF/AAAAAA&text=no+image'/>";
                if (editAction == "True")
                {
                    loadSalarysetInfo();
                }

            }
            catch { }

        }

        private void loadSalarysetInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EId", EmployeeId) };
                sqlDB.fillDataTable("Select SaID, SaGovtOrBasic, SaSchool, SaTotal from Salaryset where EId=@EId ", prms, dt);
                if (dt.Rows[0]["SaGovtOrBasic"].ToString() == "0.00")
                {
                    rdoScale.Checked = false;
                    rdoGross.Checked = true;
                    txtGovSalary.Enabled = false;
                    txtSchoolSalary.Enabled = true;

                    txtGovSalary.Text = "";
                    txtSchoolSalary.Text = dt.Rows[0]["SaSchool"].ToString();
                }
                else
                {
                    rdoScale.Checked = true;
                    rdoGross.Checked = false;
                    txtGovSalary.Enabled = true;
                    txtSchoolSalary.Enabled = false;

                    txtSchoolSalary.Text = "";
                    txtGovSalary.Text = dt.Rows[0]["SaGovtOrBasic"].ToString();
                    totalAllowance = Convert.ToDouble(dt.Rows[0]["SaSchool"].ToString());
                }
                btnSave.Text = "Update";
                lblTotalSalary.Text = dt.Rows[0]["SaTotal"].ToString();
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (EmployeeId != "" && editAction != "True")
            {
                saveSalaryset();
            }
            else if (EmployeeId != "" && editAction == "True")
            {
                updateSalaryset();
            }
        }


        private Boolean saveSalaryset()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into  Salaryset ( EId, SaGovtOrBasic, SaSchool, SaTotal, SaStaus)  values ( @EId, @SaGovtOrBasic, @SaSchool, @SaTotal, @SaStaus) ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@EId", EmployeeId);

                if (rdoGross.Checked)
                {
                    cmd.Parameters.AddWithValue("@SaGovtOrBasic", "0.0");
                    cmd.Parameters.AddWithValue("@SaSchool", txtSchoolSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaStaus", "Flat");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SaGovtOrBasic", txtGovSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaSchool", totalAllowance);
                    cmd.Parameters.AddWithValue("@SaStaus", "Both");
                }
                cmd.Parameters.AddWithValue("@SaTotal", lblTotalSalary.Text);

                int result = (int)cmd.ExecuteNonQuery();

                EmployeeId = "";
                totalAllowance = 0.0;
                editAction = "";

                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "error->Unable to save";
                Response.Redirect("~/UI/Administration/HR/Employee/EmpDetails.aspx");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean updateSalaryset()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Salaryset  Set  SaGovtOrBasic=@SaGovtOrBasic, SaSchool=@SaSchool, SaTotal=@SaTotal, SaStaus=@SaStaus where EId=@EId ", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@EId", EmployeeId);
                if (rdoGross.Checked)
                {
                    cmd.Parameters.AddWithValue("@SaGovtOrBasic", "0.00");
                    cmd.Parameters.AddWithValue("@SaSchool", txtSchoolSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaTotal", lblTotalSalary.Text);
                    cmd.Parameters.AddWithValue("@SaStaus", "Flat");
                }
                else
                {

                    cmd.Parameters.AddWithValue("@SaGovtOrBasic", txtGovSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaSchool", totalAllowance);
                    cmd.Parameters.AddWithValue("@SaTotal", lblTotalSalary.Text);
                    cmd.Parameters.AddWithValue("@SaStaus", "Both");
                }
                cmd.ExecuteNonQuery();
                EmployeeId = "";
                totalAllowance = 0.0;
                editAction = "";
                saveSalarySetLog();//for old data save
                Response.Redirect("~/UI/Administration/HR/Payroll/SalarySetDetails.aspx");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean saveSalarySetLog()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EId", EmployeeId) };
                sqlDB.fillDataTable("Select SaID, SaGovtOrBasic, SaSchool, SaTotal from Salaryset where EId=@EId ", prms, dt);
                SqlCommand cmd = new SqlCommand("Insert into  SalarySetLog ( SaID, EId, SaLNewSaGovt, SaLOldSaGovt, SaLNewSaSchool, SaLSaOldSchool, SaLDate, SaLChangedBy) "
                + "values ( @SaID, @EId, @SaLNewSaGovt, @SaLOldSaGovt, @SaLNewSaSchool, @SaLSaOldSchool, @SaLDate, @SaLChangedBy) ", DbConnection.Connection);
                cmd.Parameters.AddWithValue("@SaID", dt.Rows[0]["SaID"].ToString());
                cmd.Parameters.AddWithValue("@EId", EmployeeId);
                if (rdoGross.Checked)
                {
                    cmd.Parameters.AddWithValue("@SaLNewSaGovt", "0.00");
                    cmd.Parameters.AddWithValue("@SaLNewSaSchool", txtSchoolSalary.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SaLNewSaGovt", txtGovSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaLNewSaSchool", "0.00");
                }

                cmd.Parameters.AddWithValue("@SaLOldSaGovt", dt.Rows[0]["SaGovtOrBasic"].ToString());
                cmd.Parameters.AddWithValue("@SaLSaOldSchool", dt.Rows[0]["SaSchool"].ToString());
                cmd.Parameters.AddWithValue("@SaLDate", DateTime.Parse(System.DateTime.Now.Date.ToShortDateString()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@SaLChangedBy", "");
                int result = (int)cmd.ExecuteNonQuery();
                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "error->Unable to save";
                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }


        protected void rdoScale_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtGovSalary.Enabled = true;
                txtSchoolSalary.Enabled = false;
                rdoGross.Checked = false;
                txtGovSalary.Text = "";
                txtSchoolSalary.Text = "";
                lblTotalSalary.Text = "";
            }
            catch { }
        }

        protected void rdoGross_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtGovSalary.Enabled = false;
                txtSchoolSalary.Enabled = true;
                rdoScale.Checked = false;
                txtSchoolSalary.Text = "";
                txtGovSalary.Text = "";
                lblTotalSalary.Text = "";
            }
            catch { }
        }

        protected void txtGovSalary_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select AId, AType, APercentage, AStatus from AllowanceType where AStatus='True' ", dt);
                double basicSalary = double.Parse(txtGovSalary.Text.Trim());
                totalAllowance = 0.0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totalAllowance += basicSalary * double.Parse(dt.Rows[i]["APercentage"].ToString()) / 100;
                }
                lblTotalSalary.Text = (basicSalary + totalAllowance).ToString();
            }
            catch { }
        }

        protected void txtSchoolSalary_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblTotalSalary.Text = txtSchoolSalary.Text.Trim();
            }
            catch { }
        }
    }
}