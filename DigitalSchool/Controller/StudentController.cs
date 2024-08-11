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

        public IEnumerable<String> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public IHttpActionResult Get(int id)
        {
            var studentInfos = getStudentInfeos(id);
            return Ok(new {StatusCode="200",Message="Success", Data=studentInfos });
        }

        public StudentInfoDto getStudentInfeos(int admissionNo)
        {
            string ImagebaseUrl = "https://islampurcollege.edu.bd/Images/profileImages/";
     
            dt = commonTask.getStudentInfo(admissionNo);
            DataRow studentRow = dt.Rows[0];
            StudentInfoDto studentDto = new StudentInfoDto
            {
                AdmissionFormNo = Convert.ToInt32(studentRow["AdmissionFormNo"]),
                FullName = studentRow["FullName"].ToString(),
                FathersName = studentRow["FathersName"].ToString(),
                ClassId= Convert.ToInt32(studentRow["classId"]),
                ClassName = studentRow["ClassName"].ToString(),
                AdmissionYear = studentRow["AdmissionYear"] != DBNull.Value ? Convert.ToInt32(studentRow["AdmissionYear"]) : (int?)null,
                GroupName = studentRow["GroupName"].ToString(),
                ImageName = ImagebaseUrl+studentRow["ImageName"].ToString(),
                Mobile = studentRow["Mobile"].ToString()
            };

            return studentDto;
        }

    }
}
