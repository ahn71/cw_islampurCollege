using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class EmployeeInformation : System.Web.UI.Page
    {
        static string imageName = "";
        static string teacherId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                lblMessage.InnerText = "";
                if (!IsPostBack)
                {
                    getDesignationsList();
                    getDepartmentsList();
                    loadEmployeeInfoInfo();
                }
            }
        }

        private void loadEmployeeInfoInfo()
        {
            try
            {
                teacherId = Request.QueryString["TeacherId"];
                if (teacherId == "") return;
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@EID", teacherId) };
                sqlDB.fillDataTable("Select EID, ECardNo,  convert(varchar(11),EJoiningDate,106) as EJoiningDate, EName, TCodeNo, EGender, EFathersName, EMothersName, DId,"
                +"DesId, EReligion, EMaritalStatus, EPhone, EMobile, EEmail,  convert(varchar(11),EBirthday,106) as EBirthday, EPresentAddress, EParmanentAddress, "
                +"EBloodGroup, ELastDegree, EExaminer, ENationality, EPictureName,IsActive,EStatus from EmployeeInfo where EID=@EID ", prms, dt);
                if (dt.Rows.Count > 0)
                {
                    txtE_CardNo.Text = dt.Rows[0]["ECardNo"].ToString();
                    txtE_JoiningDate.Text = dt.Rows[0]["EJoiningDate"].ToString();
                    txtE_Name.Text = dt.Rows[0]["EName"].ToString();
                    txtTCodeNo.Text = dt.Rows[0]["TCodeNo"].ToString();
                    dlGender.Text = dt.Rows[0]["EGender"].ToString();
                    txtE_FathersName.Text = dt.Rows[0]["EFathersName"].ToString();
                    txtE_MothersName.Text = dt.Rows[0]["EMothersName"].ToString();

                    DataTable dtdep = new DataTable();
                    SqlParameter[] prmsdep = { new SqlParameter("@DId", dt.Rows[0]["DId"].ToString()) };
                    sqlDB.fillDataTable("select DName from Departments_HR where DId=@DId", prmsdep, dtdep);
                    dlDepartments.Text = dtdep.Rows[0]["DName"].ToString();


                    DataTable dtdesg = new DataTable();
                    SqlParameter[] prmsdesg = { new SqlParameter("@DesId", dt.Rows[0]["DesId"].ToString()) };
                    sqlDB.fillDataTable("select DesName from Designations where DesId=@DesId", prmsdesg, dtdesg);
                    dlDesignation.Text = dtdesg.Rows[0]["DesName"].ToString();


                    dlReligion.Text = dt.Rows[0]["EReligion"].ToString();
                    dlMaritalStatus.Text = dt.Rows[0]["EMaritalStatus"].ToString();
                    txtE_Phone.Text = dt.Rows[0]["EPhone"].ToString();
                    txtE_Mobile.Text = dt.Rows[0]["EMobile"].ToString();
                    txtE_Email.Text = dt.Rows[0]["EEmail"].ToString();
                    txtE_Birthday.Text = dt.Rows[0]["EBirthday"].ToString();
                    txtE_PresentAddress.Text = dt.Rows[0]["EPresentAddress"].ToString();
                    txtE_ParmanentAddress.Text = dt.Rows[0]["EParmanentAddress"].ToString();
                    dlBloodGroup.Text = dt.Rows[0]["EBloodGroup"].ToString();
                    txtE_LastDegree.Text = dt.Rows[0]["ELastDegree"].ToString();

                    if (dt.Rows[0]["EExaminer"].ToString() == "True") chkExaminer.Checked = true;
                    else chkExaminer.Checked = false;
                    if (dt.Rows[0]["IsActive"].ToString() == "True") chkIsActive.Checked = true;
                    else chkIsActive.Checked = false;

                    dlEStatus.Text = dt.Rows[0]["EStatus"].ToString();
                    // txtE_Nationality.Text = dt.Rows[0]["E_Nationality"].ToString();
                    imageName = dt.Rows[0]["EPictureName"].ToString();


                    string url = @"/Images/teacherProfileImage/" + Path.GetFileName(dt.Rows[0]["EPictureName"].ToString());
                    imgProfile.ImageUrl = url;

                    btnSave.Text = "Update";
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }


        private void getDepartmentsList()
        {
            try
            {
                dlDepartments.Items.Clear();
                dlDepartments.Items.Add("");
                sqlDB.loadDropDownList("Select DName from Departments_HR   Order by DName ", dlDepartments);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private void getDesignationsList()
        {
            try
            {
                dlDesignation.Items.Clear();
                dlDesignation.Items.Add("");
                sqlDB.loadDropDownList("Select DesName from Designations   Order by DesName ", dlDesignation);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save") saveEmployeeInfo();
            else updateEmployeeInfo();
        }

        private Boolean updateEmployeeInfo()
        {
            try
            {
                DataTable dtTCodeNo = new DataTable();
                sqlDB.fillDataTable("Select TCodeNo,EID From EmployeeInfo Where TCodeNo='" + txtTCodeNo.Text.Trim() + "' ", dtTCodeNo);
                if (dtTCodeNo.Rows.Count > 0 && dtTCodeNo.Rows[0]["EID"].ToString() != teacherId)
                {
                    lblMessage.InnerText = "Already this Teacher Code No Exist ";
                    return false;
                }

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@DName", dlDepartments.Text) };
                sqlDB.fillDataTable("Select DId from Departments_HR where DName=@DName ", prms, dt);

                DataTable dtdes = new DataTable();
                SqlParameter[] prmsd = { new SqlParameter("@DesName", dlDesignation.Text) };
                sqlDB.fillDataTable("Select DesId from Designations where DesName=@DesName ", prmsd, dtdes);

                if (FileUpload1.HasFile == true)
                {
                    SqlCommand cmd = new SqlCommand(" update EmployeeInfo  Set ECardNo=@ECardNo, EJoiningDate=@EJoiningDate, EName=@EName, TCodeNo=@TCodeNo, "
                    +"EGender=@EGender, EFathersName=@EFathersName, EMothersName=@EMothersName, DId=@DId, DesId=@DesId, EReligion=@EReligion, EMaritalStatus=@EMaritalStatus,"
                    +"EPhone=@EPhone, EMobile=@EMobile, EEmail=@EEmail, EBirthday=@EBirthday, EPresentAddress=@EPresentAddress, EParmanentAddress=@EParmanentAddress, "
                    +"EBloodGroup=@EBloodGroup, ELastDegree=@ELastDegree, EExaminer=@EExaminer, ENationality=@ENationality, EPictureName=@EPictureName,IsActive=@IsActive,"
                    +"EStatus=@EStatus  where EID=@EID ", sqlDB.connection);

                    cmd.Parameters.AddWithValue("@EID", Request.QueryString["TeacherId"]);
                    cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EJoiningDate", DateTime.Parse(txtE_JoiningDate.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                    cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                    cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                    cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                    cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMobile", txtE_Mobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                    cmd.Parameters.AddWithValue("@EBirthday", DateTime.Parse(txtE_Birthday.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                    cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());

                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EExaminer", 1);
                    else cmd.Parameters.AddWithValue("@EExaminer", 0);

                    cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                    cmd.Parameters.AddWithValue("@EPictureName", teacherId + FileUpload1.FileName);
                    if (chkIsActive.Checked)
                    {
                        cmd.Parameters.AddWithValue("@IsActive", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsActive", 0); 
                    }

                    cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());
                    cmd.ExecuteNonQuery();

                    if (imageName != "")
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath + "/Images/teacherProfileImage/" + imageName);
                    }
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("/Images/teacherProfileImage/" + teacherId + filename));    //Save images into Images folder

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                    Response.Redirect("/Forms/TeacherPartialInfo.aspx");
                    return true;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(" update EmployeeInfo  Set ECardNo=@ECardNo, EJoiningDate=@EJoiningDate, EName=@EName, TCodeNo=@TCodeNo, "
                    +"EGender=@EGender, EFathersName=@EFathersName, EMothersName=@EMothersName, DId=@DId, DesId=@DesId, EReligion=@EReligion, EMaritalStatus=@EMaritalStatus,"
                    +"EPhone=@EPhone, EMobile=@EMobile, EEmail=@EEmail, EBirthday=@EBirthday, EPresentAddress=@EPresentAddress, EParmanentAddress=@EParmanentAddress, "
                    +"EBloodGroup=@EBloodGroup, ELastDegree=@ELastDegree, EExaminer=@EExaminer, ENationality=@ENationality,IsActive=@IsActive,EStatus=@EStatus "
                    +"where EID=@EID ", sqlDB.connection);

                    cmd.Parameters.AddWithValue("@EID", Request.QueryString["TeacherId"]);
                    cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EJoiningDate", DateTime.Parse(txtE_JoiningDate.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                    cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                    cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                    cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                    cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMobile", txtE_Mobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                    cmd.Parameters.AddWithValue("@EBirthday", DateTime.Parse(txtE_Birthday.Text.Trim()).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                    cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());

                    if (chkExaminer.Checked) cmd.Parameters.AddWithValue("@EExaminer", 1);
                    else cmd.Parameters.AddWithValue("@EExaminer", 0);

                    cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                    if (chkIsActive.Checked)
                    {
                        cmd.Parameters.AddWithValue("@IsActive",1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsActive", 0);
                    }

                    cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());

                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                    Response.Redirect("/Forms/TeacherPartialInfo.aspx");
                    return true;
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean saveEmployeeInfo()
        {
            try
            {
                if (checkMandatoryData() == false) return false;

                DataTable dtTCodeNo = new DataTable();
                sqlDB.fillDataTable("Select TCodeNo From EmployeeInfo Where TCodeNo='"+txtTCodeNo.Text.Trim()+"' ", dtTCodeNo);
                if (dtTCodeNo.Rows.Count > 0)
                {
                    lblMessage.InnerText = "Already this Teacher Code No Exist";
                    return false;
                }

                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@DName", dlDepartments.Text) };
                sqlDB.fillDataTable("Select DId from Departments_HR where DName=@DName ", prms, dt);

                DataTable dtdes = new DataTable();
                SqlParameter[] prmsd = { new SqlParameter("@DesName", dlDesignation.Text) };
                sqlDB.fillDataTable("Select DesId from Designations where DesName=@DesName ", prmsd, dtdes);



                SqlCommand cmd = new SqlCommand("saveEmployeeInfo", sqlDB.connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ECardNo", txtE_CardNo.Text.Trim());
                cmd.Parameters.AddWithValue("@EJoiningDate", DateTime.Parse(txtE_JoiningDate.Text.Trim()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@EName", txtE_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@TCodeNo", txtTCodeNo.Text.Trim());
                cmd.Parameters.AddWithValue("@EGender", dlGender.Text.Trim());
                cmd.Parameters.AddWithValue("@EFathersName", txtE_FathersName.Text.Trim());
                cmd.Parameters.AddWithValue("@EMothersName", txtE_MothersName.Text.Trim());
                cmd.Parameters.AddWithValue("@DId", dt.Rows[0]["DId"].ToString());
                cmd.Parameters.AddWithValue("@DesId", dtdes.Rows[0]["DesId"].ToString());
                cmd.Parameters.AddWithValue("@EReligion", dlReligion.Text.Trim());
                cmd.Parameters.AddWithValue("@EMaritalStatus", dlMaritalStatus.Text.Trim());
                cmd.Parameters.AddWithValue("@EPhone", txtE_Phone.Text.Trim());
                cmd.Parameters.AddWithValue("@EMobile", txtE_Mobile.Text.Trim());
                cmd.Parameters.AddWithValue("@EEmail", txtE_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@EBirthday", DateTime.Parse(txtE_Birthday.Text.Trim()).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@EPresentAddress", txtE_PresentAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@EParmanentAddress", txtE_ParmanentAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@EBloodGroup", dlBloodGroup.Text.Trim());
                cmd.Parameters.AddWithValue("@ELastDegree", txtE_LastDegree.Text.Trim());
                cmd.Parameters.AddWithValue("@EExaminer", 1);
                cmd.Parameters.AddWithValue("@ENationality", dlNationality.Text.Trim());
                cmd.Parameters.AddWithValue("@EPictureName", "");
                if (chkIsActive.Checked)
                {
                    cmd.Parameters.AddWithValue("@IsActive", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IsActive", 0);
                }

                cmd.Parameters.AddWithValue("@EStatus", dlEStatus.Text.Trim());


                int result = (int)cmd.ExecuteScalar();
                saveImg();
                clearTextBox();
                if (result > 0) lblMessage.InnerText = "success->Successfully saved";
                else lblMessage.InnerText = "error->Unable to save";

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "saveSuccess();", true);

                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        Boolean checkMandatoryData()
        {
            try
            {
                if (txtE_CardNo.Text == "")
                {
                    lblMessage.InnerText = "Input Card Number";
                    return false;
                }
                if (txtE_Name.Text == "")
                {
                    lblMessage.InnerText = "Input Name";
                    return false;
                }
                if (txtTCodeNo.Text == "")
                {
                    lblMessage.InnerText = "Input Teacher Code No.";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }

        private void saveImg()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select Max(EID) as EID from EmployeeInfo ", dt);

                //Get Filename from fileupload control
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //Save images into Images folder
                FileUpload1.SaveAs(Server.MapPath("/Images/teacherProfileImage/" + dt.Rows[0]["EID"].ToString()+ filename));

                SqlCommand cmd = new SqlCommand("update EmployeeInfo set EPictureName=@EPictureName where EID=@EID",sqlDB.connection);
                cmd.Parameters.AddWithValue("@EID", dt.Rows[0]["EID"].ToString());
                cmd.Parameters.AddWithValue("@EPictureName", dt.Rows[0]["EID"].ToString()+ filename);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }



        private void clearTextBox()
        {
            try
            {
                txtE_Birthday.Text = "";
                txtE_CardNo.Text = "";
                txtE_Email.Text = "";
                txtE_FathersName.Text = "";
                txtE_JoiningDate.Text = "";
                txtE_LastDegree.Text = "";
                txtE_Mobile.Text = "";
                txtE_MothersName.Text = "";
                txtE_Name.Text = "";
                txtTCodeNo.Text = "";
                txtE_ParmanentAddress.Text = "";
                txtE_Phone.Text = "";
                txtE_PresentAddress.Text = "";
                chkIsActive.Checked = true;
            }
            catch { }
        }

    }
}