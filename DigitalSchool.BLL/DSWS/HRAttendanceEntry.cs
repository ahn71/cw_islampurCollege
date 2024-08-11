using DS.DAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.DSWS;
using DS.PropertyEntities.Model.HR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
    public class HRAttendanceEntry:IDisposable
    {
        private HRAttendanceEntities _Entities;
        static List<HRAttendanceEntities> AdmStdInfoList;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public HRAttendanceEntry()
        {}
        public HRAttendanceEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSHRAttendance] " +
                "([EID],[AttDate],[AttStatus]) VALUES (" +
                "'" + _Entities.EID + "', " +
                "'" + _Entities.AttDate + "' ," +               
                "'" + _Entities.AttStatus + "')");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public bool Delete(string date)
        {
            sql = string.Format("Delete FROM  [dbo].[WSHRAttendance] WHERE [AttDate]='"+date+"'");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public List<HRAttendanceEntities> getHRAttendance(string status,string date)
        {
            sql = " SELECT EID,ECardNo,EName,DName,DesName,TCodeNo,EMobile,IsFaculty,case when "
            +"EPictureName='' then '/Images/teacherProfileImage/noProfileImage.jpg' else '/Images/teacherProfileImage/'+EPictureName "
            +"END EPictureName,FORMAT(AttDate,'dd-MM-yyyy') as AttDate,AttStatus FROM v_WSHRAttendance where IsFaculty='" + status + "' "
            +"and IsFaculty is not null and AttDate='"+date+"'";
            dt = new DataTable();
            List<HRAttendanceEntities> ListEntities = new List<HRAttendanceEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new HRAttendanceEntities
                                    {
                                        EID = int.Parse(row["EID"].ToString()),
                                        EmpInfo = new Employee()
                                        {
                                        EmployeeId=int.Parse(row["EID"].ToString()),
                                        EmpCardNo = int.Parse(row["ECardNo"].ToString()),
                                        EmpName = row["EName"].ToString(),
                                        DeptName = row["DName"].ToString(),
                                        Designation = row["DesName"].ToString(),
                                        TCode = row["TCodeNo"].ToString(),
                                        Mobile = row["EMobile"].ToString(),
                                        IsTeacher=row["IsFaculty"].ToString()=="True"?true:false,
                                        PicName = row["EPictureName"].ToString()
                                        },
                                        AttDate = row["AttDate"].ToString() == "" ? (DateTime?)null : convertDateTime.getCertainCulture(row["AttDate"].ToString()),
                                        AttStatus = row["AttStatus"].ToString()

                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<HRAttendanceEntities> getHRAttendance(int EID, string year,string month)
        {
            sql = " SELECT EID,ECardNo,EName,DName,DesName,TCodeNo,EMobile,IsFaculty,case when "
            + "EPictureName='' then '/Images/teacherProfileImage/noProfileImage.jpg' else '/Images/teacherProfileImage/'+EPictureName "
            + "END EPictureName,FORMAT(AttDate,'dd-MM-yyyy') as AttDate, case when AttStatus='p' then '1' else '0' end AttStatus FROM v_WSHRAttendance where EID='" + EID + "' "
            + " and FORMAT(AttDate,'MMMM-yyyy')='"+month+"-"+year+"'";
            dt = new DataTable();
            List<HRAttendanceEntities> ListEntities = new List<HRAttendanceEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new HRAttendanceEntities
                                    {
                                        EID = int.Parse(row["EID"].ToString()),
                                        EmpInfo = new Employee()
                                        {
                                            EmployeeId = int.Parse(row["EID"].ToString()),
                                            EmpCardNo = int.Parse(row["ECardNo"].ToString()),
                                            EmpName = row["EName"].ToString(),
                                            DeptName = row["DName"].ToString(),
                                            Designation = row["DesName"].ToString(),
                                            TCode = row["TCodeNo"].ToString(),
                                            Mobile = row["EMobile"].ToString(),
                                            IsTeacher = row["AttStatus"].ToString() == "1" ? true : false,
                                            PicName = row["EPictureName"].ToString()
                                        },
                                        AttDate = row["AttDate"].ToString() == "" ? (DateTime?)null : convertDateTime.getCertainCulture(row["AttDate"].ToString()),
                                        AttStatus = row["AttStatus"].ToString()

                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<HRAttendanceEntities> getHR(string status)
        {
            sql = " SELECT DISTINCT EID,ECardNo,EName,DName,DesName,TCodeNo,EMobile,IsFaculty,case when EPictureName='' then '/Images/teacherProfileImage/noProfileImage.jpg' else '/Images/teacherProfileImage/'+EPictureName END EPictureName FROM v_WSHRAttendance where IsFaculty='" + status + "' and IsFaculty is not null";
            dt = new DataTable();
            List<HRAttendanceEntities> ListEntities = new List<HRAttendanceEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new HRAttendanceEntities
                                    {
                                        EID = int.Parse(row["EID"].ToString()),
                                        EmpInfo = new Employee()
                                        {
                                            EmployeeId = int.Parse(row["EID"].ToString()),
                                            EmpCardNo = int.Parse(row["ECardNo"].ToString()),
                                            EmpName = row["EName"].ToString(),
                                            DeptName = row["DName"].ToString(),
                                            Designation = row["DesName"].ToString(),
                                            TCode = row["TCodeNo"].ToString(),
                                            Mobile = row["EMobile"].ToString(),
                                            IsTeacher = row["IsFaculty"].ToString() == "True" ? true : false,
                                            PicName = row["EPictureName"].ToString()
                                        },
                                        AttDate =  (DateTime?)null,
                                        AttStatus = ""

                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<HRAttendanceEntities> getHR(int EID)
        {
            sql = " SELECT DISTINCT EID,ECardNo,EName,DName,DesName,TCodeNo,EMobile,IsFaculty,case when EPictureName='' then '/Images/teacherProfileImage/noProfileImage.jpg' else '/Images/teacherProfileImage/'+EPictureName END EPictureName FROM v_WSHRAttendance where EID='" + EID + "'";
            dt = new DataTable();
            List<HRAttendanceEntities> ListEntities = new List<HRAttendanceEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new HRAttendanceEntities
                                    {
                                        EID = int.Parse(row["EID"].ToString()),
                                        EmpInfo = new Employee()
                                        {
                                            EmployeeId = int.Parse(row["EID"].ToString()),
                                            EmpCardNo = int.Parse(row["ECardNo"].ToString()),
                                            EmpName = row["EName"].ToString(),
                                            DeptName = row["DName"].ToString(),
                                            Designation = row["DesName"].ToString(),
                                            TCode = row["TCodeNo"].ToString(),
                                            Mobile = row["EMobile"].ToString(),
                                            IsTeacher = row["IsFaculty"].ToString() == "True" ? true : false,
                                            PicName = row["EPictureName"].ToString()
                                        },
                                        AttDate = (DateTime?)null,
                                        AttStatus = ""

                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

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
