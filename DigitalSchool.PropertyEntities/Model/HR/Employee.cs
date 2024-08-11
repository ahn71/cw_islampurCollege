using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.HR
{
    public class Employee : IDisposable
    {
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime Birthday { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Blood { get; set; }
        public string Nationality { get; set; }
        public string NationIdNo { get; set; }
        public int DepartmentId { get; set; }
        public string DeptName{ get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public long EmpCardNo { get; set; }
        public DateTime JoiningDate { get; set; }
        public string TCode { get; set; }
        public string LastDegree { get; set; } 
        public bool IsExaminer { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string PicName { get; set; }
        public string Shift { get; set; }
        public bool IsTeacher { get; set; }

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
