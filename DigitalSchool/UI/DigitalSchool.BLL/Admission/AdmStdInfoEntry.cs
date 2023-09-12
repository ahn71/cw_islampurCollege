using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Admission;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.Admission
{
    public class AdmStdInfoEntry : IDisposable
    {
        private AdmStdInfoEntities _Entities;
        static List<AdmStdInfoEntities> AdmStdInfoList;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public AdmStdInfoEntry()
        {}
        public AdmStdInfoEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[CurrentStudentInfo] "
                + "([FullName],[ClassID],"
                + "[ClassName],[ClsGrpID],[ClsSecID],[SectionName],"
                + "[RollNo],[Religion],[ConfigId],[Shift],[DateOfBirth],"
                + "[Gender],[Mobile],[Session],[BloodGroup],[FathersName]"
                + ",[FathersProfession],[FathersYearlyIncome],[FathersMobile],"
                + "[FatherEmail],[MothersName],[MothersProfession],"
                + "[MothersYearlyIncome],[MothersMoible],[MotherEmail],"
                + "[HomePhone],[GuardianName],[GuardianRelation],[GuardianMobileNo]"
                + ",[GuardianAddress],[PAVillage],[PAPostOffice],[PThanaId]"
                + ",[PAThana],[PDistrictId],[PADistrict],[TAViIlage],"
                + "[TAPostOffice],[TThanaId],[TAThana],[TDistrictId]"
                + ",[TADistrict],[MotherTongue],[Nationality],[ImageName],"
                + "[PreviousExamType],[PSCRollNo],[PSCPassingYear],"
                + "[PSCGPA],[PSCBoard],[CertifiedDate],[PreviousSchoolName],"
                + "[PSCJSCRegistration],[TransferCertifiedNo],[PreferredClass],"
                + "[IsActive],[Comments],[BatchName],[Status],[PaymentStatus])"
                + " VALUES ( "
                + "'" + _Entities.Student.FullName + "','" + _Entities.Student.ClassID + "',"
                + "'" + _Entities.Student.ClassName + "','" + _Entities.Student.ClsGrpID + "',"
                + "'" + _Entities.Student.ClsSecID + "','" + _Entities.Student.SectionName + "',"
                + "'" + _Entities.Student.RollNo + "','" + _Entities.Student.Religion + "',"
                + "'" + _Entities.Student.ConfigId + "','" + _Entities.Student.Shift + "',"
                + "'" + _Entities.Student.DateOfBirth + "','" + _Entities.Student.Gender + "',"
                + "'" + _Entities.Student.Mobile + "','" + _Entities.Student.Session + "',"
                + "'" + _Entities.Student.BloodGroup + "','" + _Entities.Student.FathersName + "',"
                + "'" + _Entities.Student.FathersProfession + "','" + _Entities.Student.FathersYearlyIncome + "',"
                + "'" + _Entities.Student.FathersMobile + "','" + _Entities.Student.FatherEmail + "',"
                + "'" + _Entities.Student.MothersName + "','" + _Entities.Student.MothersProfession + "',"
                + "'" + _Entities.Student.MothersYearlyIncome + "','" + _Entities.Student.MothersMoible + "',"
                + "'" + _Entities.Student.MotherEmail + "','" + _Entities.Student.HomePhone + "',"
                + "'" + _Entities.Student.GuardianName + "','" + _Entities.Student.GuardianRelation + "',"
                + "'" + _Entities.Student.GuardianMobileNo + "','" + _Entities.Student.GuardianAddress + "',"
                + "'" + _Entities.Student.PAVillage + "','" + _Entities.Student.PAPostOffice + "',"
                + "'" + _Entities.Student.PThanaId + "','" + _Entities.Student.PAThana + "',"
                + "'" + _Entities.Student.PDistrictId + "','" + _Entities.Student.PADistrict + "',"
                + "'" + _Entities.Student.TAViIlage + "','" + _Entities.Student.TAPostOffice + "',"
                + "'" + _Entities.Student.TThanaId + "','" + _Entities.Student.TAThana + "',"
                + "'" + _Entities.Student.TDistrictId + "','" + _Entities.Student.TADistrict + "',"
                + "'" + _Entities.Student.MotherTongue + "','" + _Entities.Student.Nationality + "',"
                + "'" + _Entities.Student.ImageName + "',"
                + "'" + _Entities.Student.PreviousExamType + "','" + _Entities.Student.PSCRollNo + "',"
                + "'" + _Entities.Student.PSCPassingYear + "','" + _Entities.Student.PSCGPA + "',"
                + "'" + _Entities.Student.PSCBoard + "','" + _Entities.Student.CertifiedDate + "',"
                + "'" + _Entities.Student.PreviousSchoolName + "','" + _Entities.Student.PSCJSCRegistration + "',"
                + "'" + _Entities.Student.TransferCertifiedNo + "','" + _Entities.Student.PreferredClass + "',"
                + "'" + _Entities.Student.IsActive + "','" + _Entities.Student.Comments + "',"
                + "'" + _Entities.Student.BatchName + "','" + _Entities.Student.Status + "',"
                + "'" + _Entities.Student.PaymentStatus + "'); " +
                " SELECT SCOPE_IDENTITY()");
               int MaxId = CRUD.GetMaxID(sql);
            if(MaxId > 0)
            {
                result = StdAdmissionInfoInsert(MaxId, _Entities.Student.ClassID, _Entities.Student.ClsSecID, _Entities.Student.RollNo, _Entities.Session, _Entities.AdmissionNo, _Entities.AdmissionDate);
            }
            return result;             
        }
        private bool StdAdmissionInfoInsert(int studentId, int classId,int sectionId,int? rollNo,string session,long AdmissionNo, DateTime AdmissionDate)
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_STD_Admission_INFO] " +
                 "([AdmissionNo],[AdmissionDate],[StudentID],[ClassID],[ClsSecID],[RollNo],[StartBatchID],[EndBatchID],[Session])"
                 + " VALUES ( '" + AdmissionNo + "','" + AdmissionDate + "','" + studentId + "','" + _Entities.ClassID + "','" + sectionId + "','" + rollNo + "',"
                 + "'" + 0 + "','" + 0 + "','" + session + "')");
            return result = CRUD.ExecuteQuery(sql);    
        }

        public bool Update()
        {
            sql = string.Format("Update [dbo].[CurrentStudentInfo] "
                 + "set "
                 + "[FullName]='" + _Entities.Student.FullName + "',"
                 + "[ClassID]='" + _Entities.Student.ClassID + "',"
                 + "[ClassName]='" + _Entities.Student.ClassName + "',"
                 + "[ClsGrpID]='" + _Entities.Student.ClsGrpID + "',"
                 + "[ClsSecID]='" + _Entities.Student.ClsSecID + "',"
                 + "[SectionName]='" + _Entities.Student.SectionName + "',"
                 + "[RollNo]='" + _Entities.Student.RollNo + "',"
                 + "[Religion]='" + _Entities.Student.Religion + "',"
                 + "[ConfigId]='" + _Entities.Student.ConfigId + "',"
                 + "[Shift]='" + _Entities.Student.Shift + "',"
                 + "[DateOfBirth]='" + _Entities.Student.DateOfBirth + "',"
                 + "[Gender]='" + _Entities.Student.Gender + "',"
                 + "[Mobile]='" + _Entities.Student.Mobile + "',"
                 + "[Session]='" + _Entities.Student.Session + "',"
                 + "[BloodGroup]='" + _Entities.Student.BloodGroup + "',"
                 + "[FathersName]='" + _Entities.Student.FathersName + "',"
                 + "[FathersProfession]='" + _Entities.Student.FathersProfession + "',"
                 + "[FathersYearlyIncome]='" + _Entities.Student.FathersYearlyIncome + "',"
                 + "[FathersMobile]='" + _Entities.Student.FathersMobile + "',"
                 + "[FatherEmail]='" + _Entities.Student.FatherEmail + "',"
                 + "[MothersName]='" + _Entities.Student.MothersName + "',"
                 + "[MothersProfession]='" + _Entities.Student.MothersProfession + "',"
                 + "[MothersYearlyIncome]='" + _Entities.Student.MothersYearlyIncome + "',"
                 + "[MothersMoible]='" + _Entities.Student.MothersMoible + "',"
                 + "[MotherEmail]='" + _Entities.Student.MotherEmail + "',"
                 + "[HomePhone]='" + _Entities.Student.HomePhone + "',"
                 + "[GuardianName]='" + _Entities.Student.GuardianName + "',"
                 + "[GuardianRelation]='" + _Entities.Student.GuardianRelation + "',"
                 + "[GuardianMobileNo]='" + _Entities.Student.GuardianMobileNo + "',"
                 + "[GuardianAddress]='" + _Entities.Student.GuardianAddress + "',"
                 + "[PAVillage]='" + _Entities.Student.PAVillage + "',"
                 + "[PAPostOffice]='" + _Entities.Student.PAPostOffice + "',"
                 + "[PThanaId]='" + _Entities.Student.PThanaId + "',"
                 + "[PAThana]='" + _Entities.Student.PAThana + "',"
                 + "[PDistrictId]='" + _Entities.Student.PDistrictId + "',"
                 + "[PADistrict]='" + _Entities.Student.PADistrict + "',"
                 + "[TAViIlage]='" + _Entities.Student.TAViIlage + "',"
                 + "[TAPostOffice]='" + _Entities.Student.TAPostOffice + "',"
                 + "[TThanaId]='" + _Entities.Student.TThanaId + "',"
                 + "[TAThana]='" + _Entities.Student.TAThana + "',"
                 + "[TDistrictId]='" + _Entities.Student.TDistrictId + "',"
                 + "[TADistrict]='" + _Entities.Student.TADistrict + "',"
                 + "[MotherTongue]='" + _Entities.Student.MotherTongue + "',"
                 + "[Nationality]='" + _Entities.Student.Nationality + "',"
                 + "[PreviousExamType]='" + _Entities.Student.PreviousExamType + "',"
                 + "[PSCRollNo]='" + _Entities.Student.PSCRollNo + "',"
                 + "[PSCPassingYear]='" + _Entities.Student.PSCPassingYear + "',"
                 + "[PSCGPA]='" + _Entities.Student.PSCGPA + "',"
                 + "[PSCBoard]='" + _Entities.Student.PSCBoard + "',"
                 + "[CertifiedDate]='" + _Entities.Student.CertifiedDate + "',"
                 + "[PreviousSchoolName]='" + _Entities.Student.PreviousSchoolName + "',"
                 + "[PSCJSCRegistration]='" + _Entities.Student.PSCJSCRegistration + "',"
                 + "[TransferCertifiedNo]='" + _Entities.Student.TransferCertifiedNo + "',"
                 + "[PreferredClass]='" + _Entities.Student.PreferredClass + "',"
                 + "[Comments]='" + _Entities.Student.Comments + "',"
                 + "[Status]='" + _Entities.Student.Status + "',"
                 + "[PaymentStatus]='" + _Entities.Student.PaymentStatus + "',"
                 + "[ImageName]='" + _Entities.Student.ImageName + "'"
                 + " where StudentID='" + _Entities.Student.StudentID + "' ");
                 result = CRUD.ExecuteQuery(sql);
                 if (result == true)
                 {
                     result = UpdateStdAdmission(_Entities.Student.StudentID, _Entities.Student.ClassID, _Entities.Student.ClsSecID, _Entities.Student.RollNo, 
                         _Entities.Session, _Entities.AdmissionNo, _Entities.AdmissionDate);
                 }
            return result;
        }
        public bool Update(List<AdmStdInfoEntities> admStdInfoList)
        {
            foreach (var AdmStdInfo in admStdInfoList)
            {
                sql = string.Format("UPDATE [dbo].[TBL_STD_Admission_INFO] SET " +
                                    "ClsSecID = '" + AdmStdInfo.ClsSecID + "'," +                                    
                                    "RollNo = '" + AdmStdInfo.RollNo + "'," +
                                    "StartBatchID = '" + AdmStdInfo.StartBatchID + "' " +
                                    "WHERE [StudentID] = '" + AdmStdInfo.Student.StudentID + "'");
                result = CRUD.ExecuteQuery(sql);
                if (result)
                {
                    sql = string.Format("UPDATE [dbo].[CurrentStudentInfo] SET " +
                                    "ClsSecID = '" + AdmStdInfo.ClsSecID + "'," +
                                    "SectionName = '" + AdmStdInfo.Student.SectionName + "'," +
                                    "RollNo = '" + AdmStdInfo.RollNo + "'," +
                                    "SpendYear='"+AdmStdInfo.SpendYear+"',"+
                                    "BatchID = '" + AdmStdInfo.StartBatchID + "', " +
                                    "BatchName = '" + AdmStdInfo.Student.BatchName + "' " +
                                    "WHERE [StudentID] = '" + AdmStdInfo.Student.StudentID + "'");
                    result = CRUD.ExecuteQuery(sql);
                }
            }
            return result;
        }
        private bool UpdateStdAdmission(int studentId, int classId, int sectionId, int? rollNo, string session, long AdmissionNo, DateTime AdmissionDate)
        {           
            sql = string.Format("Update [dbo]."
                 + "[Tbl_STD_Admission_INFO] Set "
                 + "[AdmissionNo]='" + AdmissionNo + "',"
                 + "[AdmissionDate]='" + AdmissionDate + "',"
                 + "[ClassID]='" + classId + "',"
                 + "[ClsSecID]='" + sectionId + "',"
                 + "[RollNo]='" + rollNo + "',"                
                 + "[Session]='" + session + "'"
                 + " where StudentID='" + studentId + "'");
            return result = CRUD.ExecuteQuery(sql);            
        }
        public List<AdmStdInfoEntities> GetEntitiesData()
        {
            List<AdmStdInfoEntities> ListEntities = new List<AdmStdInfoEntities>();            
            sql = string.Format("SELECT [cs].*,[asi].* " +
                                "FROM [dbo].[CurrentStudentInfo] cs INNER JOIN [dbo].[TBL_STD_Admission_INFO] asi ON " +
                                "([cs].[StudentId] = [asi].[StudentID]) INNER JOIN [dbo].[Classes] c ON " +
                                "([cs].[ClassID] = [c].[ClassID]) WHERE  [asi].[Session] = " +
                                "(SELECT MAX(Session) FROM [dbo].[TBL_STD_Admission_INFO]) ORDER BY [c].[ClassOrder] ASC, [asi].[AdmissionID] ASC");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new AdmStdInfoEntities
                                    {
                                        AdmissionID = int.Parse(row["AdmissionID"].ToString()),
                                        AdmissionNo = long.Parse(row["AdmissionNo"].ToString()),
                                        AdmissionDate = Convert.ToDateTime(row["AdmissionDate"].ToString()),
                                        Student = new CurrentStdEntities() {
                                            StudentID = int.Parse(row["StudentId"].ToString()),
                                            AdmissionNo = int.Parse(row["AdmissionNo"].ToString()),
                                            AdmissionDate = Convert.ToDateTime(row["AdmissionDate"].ToString()),
                                            FullName = row["FullName"].ToString(),
                                            ClassID = int.Parse(row["ClassID"].ToString()),
                                            ClassName = row["ClassName"].ToString(),
                                            ClsGrpID = int.Parse(row["ClsGrpID"].ToString()),
                                            ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                            SectionName = row["SectionName"].ToString(),
                                            RollNo = int.Parse(row["RollNo"].ToString()),
                                            Religion = row["Religion"].ToString(),
                                            ConfigId = int.Parse(row["ConfigId"].ToString()),
                                            Shift = row["Shift"].ToString(),
                                            DateOfBirth = Convert.ToDateTime(row["DateOfBirth"].ToString()),
                                            Gender = row["Gender"].ToString(),
                                            Mobile = row["Mobile"].ToString(),
                                            Session = row["Session"].ToString(),
                                            BloodGroup = row["BloodGroup"].ToString(),
                                            FathersName = row["FathersName"].ToString(),
                                            FathersProfession = row["FathersProfession"].ToString(),
                                            FathersYearlyIncome = int.Parse(row["FathersYearlyIncome"].ToString()),
                                            FathersMobile = row["FathersMobile"].ToString(),
                                            FatherEmail = row["FatherEmail"].ToString(),
                                            MothersName = row["MothersName"].ToString(),
                                            MothersProfession = row["MothersProfession"].ToString(),
                                            MothersYearlyIncome = int.Parse(row["MothersYearlyIncome"].ToString()),
                                            MothersMoible = row["MothersMoible"].ToString(),
                                            MotherEmail = row["MotherEmail"].ToString(),
                                            HomePhone = row["HomePhone"].ToString(),
                                            GuardianName = row["GuardianName"].ToString(),
                                            GuardianRelation = row["GuardianRelation"].ToString(),
                                            GuardianMobileNo = row["GuardianMobileNo"].ToString(),
                                            GuardianAddress = row["GuardianAddress"].ToString(),
                                            PAVillage = row["PAVillage"].ToString(),
                                            PAPostOffice = row["PAPostOffice"].ToString(),
                                            PThanaId = int.Parse(row["PThanaId"].ToString()),
                                            PDistrictId = int.Parse(row["PDistrictId"].ToString()),
                                            TAViIlage = row["TAViIlage"].ToString(),
                                            TAPostOffice = row["TAPostOffice"].ToString(),
                                            TThanaId = int.Parse(row["TThanaId"].ToString()),
                                            TDistrictId = int.Parse(row["TDistrictId"].ToString()),
                                            MotherTongue = row["MotherTongue"].ToString(),
                                            Nationality = row["Nationality"].ToString(),
                                            PreviousExamType = row["PreviousExamType"].ToString(),
                                            PSCRollNo = int.Parse(row["PSCRollNo"].ToString()),
                                            PSCPassingYear = int.Parse(row["PSCPassingYear"].ToString()),
                                            PSCGPA = double.Parse(row["PSCGPA"].ToString()),
                                            PSCBoard = row["PSCBoard"].ToString(),
                                            CertifiedDate = Convert.ToDateTime(row["CertifiedDate"].ToString()),
                                            PreviousSchoolName = row["PreviousSchoolName"].ToString(),
                                            PSCJSCRegistration = row["PSCJSCRegistration"].ToString(),
                                            TransferCertifiedNo = int.Parse(row["TransferCertifiedNo"].ToString()),
                                            PreferredClass = row["PreferredClass"].ToString(),
                                            IsActive = bool.Parse(row["IsActive"].ToString()),
                                            Comments = row["Comments"].ToString(),
                                            BatchName = row["BatchName"].ToString(),
                                            Status = row["Status"].ToString(),                                            
                                            PaymentStatus = bool.Parse(row["PaymentStatus"].ToString())                 
                                        },
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        StartBatchID = int.Parse(row["StartBatchID"].ToString()),
                                        RollNo = int.Parse(row["RollNo"].ToString()),
                                        ClsSecID = int.Parse(row["ClsSecID"].ToString()),                                        
                                        StdStatus =row["StdStatus"].ToString() == string.Empty ? null:(bool?)bool.Parse(row["StdStatus"].ToString()),
                                        Session = row["Session"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }  
      
        public static int GetLastRoll(int clsId)
        {
            int i = 0;
            string sql = string.Format("SELECT MAX(RollNo) FROM [dbo].[CurrentStudentInfo] WHERE ClassID ='" + clsId + "'");
            return i = CRUD.GetMaxID(sql) + 1;
        }
        public DataTable GetUnpaidStdList(string condition)
        {
            try
            {
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT AdmissionNo,FORMAT(AdmissionDate,'dd-MM-yyyy') as AdmissionDate,FullName,FathersName,ShiftName,"
                +"ClassName,Gender,GuardianMobileNo FROM v_CurrentStudentInfo WHERE  BatchID='0' AND StdStatus is null "+condition+"");
                return dt;
            }
            catch { return dt; }
        }
        public static DataTable GetAdmStudent(string stdID)
        {
            DataTable dt=new DataTable();
            string sql = string.Format("SELECT FullName FROM CurrentStudentInfo WHERE StudentId='" + stdID + "'");
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public static string CheckAdmissionNo(string stdID)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT AdmissionNo FROM TBL_STD_Admission_INFO WHERE StudentID='" + stdID + "'");
            dt = CRUD.ReturnTableNull(sql);
            return dt.Rows[0].ItemArray[0].ToString();
        }
        public static void GetAdmissionNo(DropDownList dl)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("SELECT Convert(varchar(10),AdmissionNo)+'_'+FullName as AdmissionNo,StudentID FROM v_CurrentStudentInfo WHERE StdStatus='True'");
            dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
            dl.DataTextField = "AdmissionNo";
            dl.DataValueField = "StudentID";
            dl.DataBind();
            dl.Items.Insert(0,new ListItem("...Select...","0"));
        }
        
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}
