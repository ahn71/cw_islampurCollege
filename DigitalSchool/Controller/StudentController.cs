using DS.BLL.SMS;
using DS.Classes;
using DS.DAL;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Description;

namespace DS.Controller
{
    [EnableCors(origins: "https://websupportbd.com,https://islampurcollege.edu.bd,https://localhost:44343,http://tripxpart.test", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        DataTable dt = new DataTable();
        string ImagebaseUrl = "https://islampurcollege.edu.bd/Images/profileImages/";

        public IEnumerable<String> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public IHttpActionResult Get(int id)
        {
            dt = commonTask.getStudentInfo(id);
            if (dt.Rows.Count > 0)
            {
                DataRow studentRow = dt.Rows[0];
                var currentStudentInfo=getCurrentStudentIfo(studentRow);
                return Ok(new { StatusCode = "200", Message = "Success", Data = currentStudentInfo });
            }
            else
            {
                dt = commonTask.getStudentInfo(id);
                DataRow studentRow = dt.Rows[0];
                var admissionStudentInfos = getAdmissionStudentInfo(studentRow);
                return Ok(new { StatusCode = "200", Message = "Success", Data = admissionStudentInfos });
            }
           
            
        }
        public StudentInfoDto getAdmissionStudentInfo(DataRow dataRows)
        {
            DataRow studentRow = dataRows;

            StudentInfoDto studentDto = new StudentInfoDto
            {
                AdmissionFormNo = Convert.ToInt32(studentRow["AdmissionFormNo"]),
                FullName = studentRow["FullName"].ToString(),
                FathersName = studentRow["FathersName"].ToString(),
                ClassId = Convert.ToInt32(studentRow["classId"]),
                ClassName = studentRow["ClassName"].ToString(),
                AdmissionYear = studentRow["AdmissionYear"] != DBNull.Value ? Convert.ToInt32(studentRow["AdmissionYear"]) : (int?)null,
                GroupName = studentRow["GroupName"].ToString(),
                ImageName = ImagebaseUrl + studentRow["ImageName"].ToString(),
                Mobile = studentRow["Mobile"].ToString()
            };
            return studentDto;
        }

        public StudentInfoDto getCurrentStudentIfo(DataRow dataRows)
        {
            DataRow studentRow = dataRows;
            StudentInfoDto studentDto = new StudentInfoDto
            {
                AdmissionFormNo = Convert.ToInt32(studentRow["AdmissionNo"]),
                FullName = studentRow["FullName"].ToString(),
                FathersName = studentRow["FathersName"].ToString(),
                ClassId = Convert.ToInt32(studentRow["ClassID"]),
                ClassName = studentRow["BatchName"].ToString(),
                AdmissionYear = studentRow["Year"] != DBNull.Value ? Convert.ToInt32(studentRow["Year"]) : (int?)null,
                GroupName = studentRow["GroupName"].ToString(),
                ImageName = ImagebaseUrl + studentRow["ImageName"].ToString(),
                Mobile = studentRow["Mobile"].ToString()
            };
            return studentDto;
        }
 

    }
}
