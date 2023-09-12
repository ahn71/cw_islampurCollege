using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.HR;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.HR
{
    public class EmployeeEntry : IDisposable
    {
        private Employee _Entities;
        string sql = string.Empty;
        bool result = true;
        public EmployeeEntry() { }
        public Employee AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[EmployeeInfo] " +
                "([ECardNo],[EJoiningDate],[EName],[TCodeNo],[EGender],[EFathersName],"+
                "[EMothersName],[DId],[DesId],[EReligion],[EMaritalStatus],[EPhone]," +
                "[EMobile],[EEmail],[EBirthday],[EPresentAddress],[EParmanentAddress]," +
                "[EBloodGroup],[ELastDegree],[EExaminer],[ENationality],[EPictureName]," +
                "[IsActive],[EStatus]) VALUES ( " +
                "'" + _Entities.EmpCardNo + "'," +
                "'" + _Entities.JoiningDate + "'," +
                "'" + _Entities.EmpName + "'," +
                "'" + _Entities.TCode + "'," +
                "'" + _Entities.Gender + "'," +
                "'" + _Entities.FatherName + "'," +
                "'" + _Entities.MotherName + "'," +
                "'" + _Entities.DepartmentId + "'," +
                "'" + _Entities.DesignationId + "'," +
                "'" + _Entities.Religion + "'," +
                "'" + _Entities.MaritalStatus + "'," +
                "'" + _Entities.Phone + "'," +
                "'" + _Entities.Mobile + "'," +
                "'" + _Entities.Email + "'," +
                "'" + _Entities.Birthday + "'," +
                "'" + _Entities.PresentAddress + "'," +
                "'" + _Entities.PermanentAddress + "'," +
                "'" + _Entities.Blood + "'," +
                "'" + _Entities.LastDegree + "'," +
                "'" + _Entities.IsExaminer + "'," +
                "'" + _Entities.Nationality + "'," +
                "'" + _Entities.PicName + "'," +
                "'" + _Entities.IsActive + "'," +
                "'" + _Entities.Status + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[EmployeeInfo] SET " +
                "[ECardNo] = '" + _Entities.EmpCardNo + "', " +
                "[EJoiningDate] = '" + _Entities.JoiningDate + "', " +
                "[EName] = '" + _Entities.EmpName + "', " +
                "[TCodeNo] = '" + _Entities.TCode + "', " +
                "[EGender] = '" + _Entities.Gender + "', " +
                "[EFathersName] = '" + _Entities.FatherName + "', " +
                "[EMothersName] = '" + _Entities.MotherName + "', " +
                "[DId] = '" + _Entities.DepartmentId + "', " +
                "[DesId] = '" + _Entities.DesignationId + "', " +
                "[EReligion] = '" + _Entities.Religion + "', " +
                "[EMaritalStatus] = '" + _Entities.MaritalStatus + "', " +
                "[EPhone] = '" + _Entities.Phone + "', " +
                "[EMobile] = '" + _Entities.Mobile + "', " +
                "[EEmail] = '" + _Entities.Email + "', " +
                "[EBirthday] = '" + _Entities.Birthday + "', " +
                "[EPresentAddress] = '" + _Entities.PresentAddress + "', " +
                "[EParmanentAddress] = '" + _Entities.PermanentAddress + "', " +
                "[EBloodGroup] = '" + _Entities.Blood + "', " +
                "[ELastDegree] = '" + _Entities.LastDegree + "', " +
                "[EExaminer] = '" + _Entities.IsExaminer + "', " +
                "[ENationality] = '" + _Entities.Nationality + "', " +
                "[EPictureName] = '" + _Entities.PicName + "', " +
                "[IsActive] = '" + _Entities.IsActive + "', " +
                "[EStatus] = '" + _Entities.Status + "'" +
                "WHERE [EID] = '" + _Entities.EmployeeId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<Employee> GetEntitiesData()
        {
            List<Employee> ListEntities = new List<Employee>();
            sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE ORDER BY e.[EID] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new Employee
                                    {
                                        EmployeeId = int.Parse(row["EID"].ToString()),
                                        EmpCardNo = long.Parse(row["ECardNo"].ToString()),
                                        JoiningDate = DateTime.Parse(row["EJoiningDate"].ToString()),
                                        EmpName = row["EName"].ToString(),
                                        TCode = row["TCodeNo"].ToString(),
                                        Gender = row["EGender"].ToString(),
                                        FatherName = row["EFathersName"].ToString(),
                                        MotherName = row["EMothersName"].ToString(),
                                        DeptName = row["DName"].ToString(),
                                        Designation = row["DesName"].ToString(),
                                        Religion = row["EReligion"].ToString(),
                                        MaritalStatus = row["EMaritalStatus"].ToString(),
                                        Mobile = row["EMobile"].ToString(),
                                        Email = row["EEmail"].ToString(),
                                        Birthday = DateTime.Parse(row["EBirthday"].ToString()),
                                        PresentAddress = row["EPresentAddress"].ToString(),
                                        PermanentAddress = row["EParmanentAddress"].ToString(),
                                        Blood = row["EBloodGroup"].ToString(),
                                        LastDegree = row["ELastDegree"].ToString(),
                                        IsExaminer = bool.Parse(row["EExaminer"].ToString()),
                                        Nationality = row["ENationality"].ToString(),
                                        PicName = row["EPictureName"].ToString(),
                                        IsActive = bool.Parse(row["IsActive"].ToString()),
                                        Status = row["EStatus"].ToString(),
                                        Shift = row["Shift"].ToString(),
                                        IsTeacher = bool.Parse(row["IsTeacher"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<Employee> GetEntitiesData(string departmentID, string designation, string shift)
        {
            List<Employee> ListEntities = new List<Employee>();
            if (departmentID != null && designation != null && shift!=null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE e.[DId] = '" + departmentID + "' " +
                                "AND e.[DesId] ='" + designation + "' AND e.[ShiftId] = '" + shift + "' AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");
            }
            else if(departmentID == null && designation != null && shift!=null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE " +
                                "e.[DesId] ='" + designation + "' AND e.[ShiftId] = '" + shift + "' AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");                
            }
            else if (departmentID != null && designation == null && shift != null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE e.[DId] = '" + departmentID + "' " +
                                "AND e.[ShiftId] = '" + shift + "' AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");               
            }
            else if (departmentID != null && designation != null && shift == null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE e.[DId] = '" + departmentID + "' " +
                                "AND e.[DesId] ='" + designation + "' AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");
            }
            else if (departmentID == null && designation != null && shift == null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE " +
                                "e.[DesId] ='" + designation + "' AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");
            }
            else if (departmentID != null && designation == null && shift == null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE e.[DId] = '" + departmentID + "' " +
                                " AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");
            }
            else if (departmentID == null && designation == null && shift != null)
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE " +
                                "e.[ShiftId] = '" + shift + "' AND e.[IsActive] = 'true' ORDER BY e.[EID] ASC");
            }
            else
            {
                sql = string.Format("SELECT e.[EID],e.[ECardNo],e.[EJoiningDate],e.[EName],e.[TCodeNo]," +
                                "e.[EGender],e.[EFathersName],e.[EMothersName],d.[DName],d.[IsTeacher],ds.[DesName]," +
                                "e.[EReligion],e.[EMaritalStatus],e.[EMobile],e.[EEmail],e.[EBirthday]," +
                                "e.[EPresentAddress],e.[EParmanentAddress],e.[EBloodGroup],e.[ELastDegree],e.[EExaminer]," +
                                "e.[ENationality],e.[EPictureName],e.[IsActive],e.[EStatus],e.[Shift] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId]) WHERE " +
                                "  e.[IsActive] = 'true' ORDER BY e.[EID] ASC");  
            }
            
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new Employee
                                    {
                                        EmployeeId = int.Parse(row["EID"].ToString()),
                                        EmpCardNo = long.Parse(row["ECardNo"].ToString()),
                                        JoiningDate = DateTime.Parse(row["EJoiningDate"].ToString()),
                                        EmpName = row["EName"].ToString(),
                                        TCode = row["TCodeNo"].ToString(),
                                        Gender = row["EGender"].ToString(),
                                        FatherName = row["EFathersName"].ToString(),
                                        MotherName = row["EMothersName"].ToString(),
                                        DeptName = row["DName"].ToString(),
                                        Designation = row["DesName"].ToString(),
                                        Religion = row["EReligion"].ToString(),
                                        MaritalStatus = row["EMaritalStatus"].ToString(),
                                        Mobile = row["EMobile"].ToString(),
                                        Email = row["EEmail"].ToString(),
                                        Birthday = DateTime.Parse(row["EBirthday"].ToString()),
                                        PresentAddress = row["EPresentAddress"].ToString(),
                                        PermanentAddress = row["EParmanentAddress"].ToString(),
                                        Blood = row["EBloodGroup"].ToString(),
                                        LastDegree = row["ELastDegree"].ToString(),
                                        IsExaminer = bool.Parse(row["EExaminer"].ToString()),
                                        Nationality = row["ENationality"].ToString(),
                                        PicName = row["EPictureName"].ToString(),
                                        IsActive = bool.Parse(row["IsActive"].ToString()),
                                        Status = row["EStatus"].ToString(),
                                        Shift = row["Shift"].ToString(),
                                        IsTeacher = bool.Parse(row["IsTeacher"].ToString())
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }
        public static void LoadCardNo(DropDownList dl,string EmployeeTypeID)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("SELECT EID,CONVERT(varchar(30),ECardNo)+'_'+EName as CardNo FROM EmployeeInfo WHERE EmployeeTypeID='" + EmployeeTypeID + "'");
                dl.DataSource = dt;
                dl.DataTextField = "CardNo";
                dl.DataValueField = "EID";
                dl.DataBind();
                dl.Items.Insert(0,new ListItem("...Select...","0"));
            }
            catch { }
        }
        public static void LoadTeacher(DropDownList dl)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("SELECT EID,EName FROM EmployeeInfo WHERE IsFaculty='True'");
                dl.DataSource = dt;
                dl.DataTextField = "EName";
                dl.DataValueField = "EID";
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public  List<Employee> getTeacherCotactList_WS() // Teacher Contacts List for  web site 
        {
            List<Employee> ListEntities = new List<Employee>();
            sql = string.Format("SELECT e.[EPictureName], e.[EName],ds.[DesName],e.[EMobile],e.[EEmail],e.[EBirthday],d.[DName] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId])  where  Isfaculty=1 ORDER BY e.[EID] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new Employee
                                    {
                                        PicName = row["EPictureName"].ToString(),                                    
                                        EmpName = row["EName"].ToString(),
                                        Designation = row["DesName"].ToString(), 
                                        DeptName = row["DName"].ToString(),                                       
                                        Mobile = row["EMobile"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<Employee> getTeacher() // Teacher Contacts List for  web site 
        {
            List<Employee> ListEntities = new List<Employee>();
            sql = string.Format("SELECT e.[EID],e.[EName],e.[TCodeNo],d.[DId],d.[DName] " +
                                "FROM [dbo].[EmployeeInfo] e INNER JOIN [dbo].[Departments_HR] d ON (e.[DId] = d.[DId]) " +
                                " where  e.Isfaculty=1 ORDER BY e.[EID] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new Employee
                                    {
                                        EmployeeId = int.Parse(row["EID"].ToString()),
                                        EmpName = row["EName"].ToString(),
                                        TCode=row["TCodeNo"].ToString(),
                                        DepartmentId = int.Parse(row["DId"].ToString()),
                                        DeptName = row["DName"].ToString()                                       
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<Employee> getStaffCotactList_WS() // Staff Contacts List for  web site 
        {
            List<Employee> ListEntities = new List<Employee>();
            sql = string.Format("SELECT e.[EPictureName],e.[ECardNo], e.[EName],ds.[DesName],e.[EMobile],e.[EEmail],e.[EBirthday],d.[DName] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId])  where  Isfaculty=0 ORDER BY e.[EID] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new Employee
                                    {
                                        PicName = row["EPictureName"].ToString(),
                                        EmpName = row["EName"].ToString(),
                                        EmpCardNo = int.Parse(row["ECardNo"].ToString()),
                                        Designation = row["DesName"].ToString(),
                                        DeptName = row["DName"].ToString(),
                                        Mobile = row["EMobile"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<Employee> getBoardDirectorsCotactList_WS() // BoardDirector Contacts List for  web site 
        {
            List<Employee> ListEntities = new List<Employee>();
            sql = string.Format("SELECT e.[EPictureName], e.[EName],e.[ECardNo],ds.[DesName],e.[EMobile],e.[EEmail],e.[EBirthday],d.[DName] " +
                                "FROM [dbo].[EmployeeInfo] e JOIN [dbo].[Departments] d ON (e.[DId] = d.[DId]) " +
                                "JOIN [dbo].[Designations] ds ON (e.[DesId] = ds.[DesId])  where  Isfaculty Is Null ORDER BY e.[EID] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new Employee
                                    {
                                        PicName = row["EPictureName"].ToString(),
                                        EmpName = row["EName"].ToString(),
                                        EmpCardNo = int.Parse(row["ECardNo"].ToString()),
                                        Designation = row["DesName"].ToString(),
                                        DeptName = row["DName"].ToString(),
                                        Mobile = row["EMobile"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public bool UpdateCurrentEmpStatus(string EID, string stdStatus)
        {
            sql = string.Format("Update [dbo]."
                + "[EmployeeInfo] Set "
                 + "[IsActive]='" + stdStatus + "'"
                 + " where EID='" + EID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }

        public bool InsertToActivationLog(string EID,  string Note, string ActivationType)
        {
            sql = string.Format("INSERT INTO [dbo].[EmployeeActivation_Log] (EID,Note,ActivationType,EntryDate) values(" + EID + ",'" + Note + "'," + ActivationType + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

            return result = CRUD.ExecuteQuery(sql);
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
