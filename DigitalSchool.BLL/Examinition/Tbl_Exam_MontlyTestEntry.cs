using DS.DAL;
using DS.PropertyEntities.Model.Examinition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Examinition
{
    public class Tbl_Exam_MontlyTestEntry : IDisposable
    {
        private Tbl_Exam_MontlyTestEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        int result2 = 0;

        public Tbl_Exam_MontlyTestEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public int Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Exam_MontlyTest] " +
                "([ShiftId], [BatchId],[ClsGrpID],[ClsSecId],[ExInId],[Patternmarks],[Passmarks]" +
                ") VALUES (" +
                "'" + _Entities.ShiftId + "'," +
                "'" + _Entities.BatchId + "'," +
                "'" + _Entities.ClsGrpID + "'," +
                "'" + _Entities.ClsSecId + "'," +
                "'" + _Entities.ExInId+ "'," +
                "'" + _Entities.Patternmarks + "'," +
                "'" + _Entities.Passmarks + "');SELECT SCOPE_IDENTITY()");
            return result2 = CRUD.GetMaxID(sql);
        }
       
        public bool Delete()
        {
            sql = string.Format("Delete From [dbo].[Tbl_Exam_MontlyTest]" +
                "WHERE [ExInId] ='" + _Entities.ExInId + "'"
                + " and [ShiftId] ='" + _Entities.ShiftId + "'"
                + " and [ClsSecId] ='" + _Entities.ClsSecId + "'"
                + " and [BatchId] ='" + _Entities.BatchId + "'"
                + " and [ClsGrpID] ='" + _Entities.ClsGrpID + "'");
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

