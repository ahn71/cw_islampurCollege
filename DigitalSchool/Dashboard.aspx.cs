using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL;
using System.Data;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using DS.Classes;
namespace DS
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
            HttpCookie getCookies = Request.Cookies["userInfoSchool"];
            if (getCookies == null || getCookies.Value == "")
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    
                    Session["__UserTypeId__"] = getCookies["__UserTypeId__"].ToString();
                }
            }
        }

        private void BindTimeDayMonthYear()
        {
            
        }

        public StudentInfoDto getStudentInfeos(int admissionNo)
        {
            DataTable dt= new DataTable();
            dt = commonTask.getStudentInfo(admissionNo);
            DataRow studentRow = dt.Rows[0];
            StudentInfoDto studentDto = new StudentInfoDto
            {
                AdmissionFormNo = Convert.ToInt32(studentRow["AdmissionFormNo"]),
                FullName = studentRow["FullName"].ToString(),
                FathersName = studentRow["FathersName"].ToString(),
                ClassName = studentRow["ClassName"].ToString(),
                AdmissionYear = studentRow["AdmissionYear"] != DBNull.Value ? Convert.ToInt32(studentRow["AdmissionYear"]) : (int?)null,
                GroupName = studentRow["GroupName"].ToString(),
                ImageName = studentRow["ImageName"].ToString(),
                Mobile = studentRow["Mobile"].ToString()
            };

            return studentDto;
        }
    }
}