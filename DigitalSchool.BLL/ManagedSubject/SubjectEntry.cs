using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedSubject;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedSubject
{
    public class SubjectEntry
    {
        private SubjectEntities _Entities;
        static string sql = string.Empty;
        bool result = true;
        public SubjectEntry() { }
        public SubjectEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[NewSubject] " +
                "([SubName],[Ordering],[IsActive]) VALUES ( " +
                "'" + _Entities.SubjectName + "', " +  
                "'" + _Entities.OrderBy + "',"+
                "'"+_Entities.IsActive+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[NewSubject] SET " +
                "[SubName] = '" + _Entities.SubjectName + "', " +
                "[Ordering] = '" + _Entities.OrderBy + "', [IsActive]='"+_Entities.IsActive+"' " +
                "WHERE [SubId] = '" + _Entities.SubjectId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public static List<SubjectEntities> GetEntitiesData
        {
            get
            {
                List<SubjectEntities> ListEntities = new List<SubjectEntities>();
                SubjectEntry se = new SubjectEntry();
                sql = string.Format("SELECT [SubId],[SubName],[IsActive],[Ordering] FROM [dbo].[NewSubject] Order by Ordering ");

                 DataTable dt = new DataTable();
                

                dt = CRUD.ReturnTableNull(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ListEntities = (from DataRow row in dt.Rows
                                        select new SubjectEntities
                                        {
                                            SubjectId = int.Parse(row["SubId"].ToString()),
                                            SubjectName = row["SubName"].ToString(),
                                            IsActive=bool.Parse(row["IsActive"].ToString()),
                                            OrderBy = int.Parse(row["Ordering"].ToString())

                                        }).ToList();
                        return ListEntities;
                    }

                }
                return ListEntities = null;
            }
        }

        public static List<SubjectEntities> GetMandatorySubjectList
        {
            get
            {
                try
                {
                    List<SubjectEntities> GetList = new List<SubjectEntities>();
                    SubjectEntry se=new SubjectEntry ();
                  //  GetList = se.GetEntitiesData().FindAll(s=>s.IsMandatory==true).ToList();
                    return GetList;
                }
                catch { return null; }
            }
            
        }
      
        public static List<SubjectEntities> GetOptionalSubjectList
        {
            get 
            {
                try
                {
                    List<SubjectEntities> GetList = new List<SubjectEntities>();
                    SubjectEntry se = new SubjectEntry();
                //    GetList = se.GetEntitiesData().FindAll(s => s.IsOptional == true).ToList();
                    return GetList;
                }
                catch { return null; }
            }
        
        }

        public static void GetSujectList(DropDownList ddl)
        {
                try
                {
                sql = string.Format("SELECT [SubId],[SubName],[IsActive],[Ordering] FROM [dbo].[NewSubject] where  IsActive=1 Order by Ordering ");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                ddl.DataTextField = "SubName";
                    ddl.DataValueField = "SubId";
                    ddl.DataSource= dt;
                    ddl.DataBind();
                    ddl.Items.Insert(0,new ListItem ("...Select...","0"));
                    
                }
                catch { }
        
        }

        //Related Subject
        //public static void GetRelSujectList(DropDownList ddl, string classId)
        //{
        //    try
        //    {
        //        sql = string.Format("SELECT  cs.SubId,ns.SubName FROM ClassSubject cs INNER JOIN NewSubject ns ON cs.SubId=           ns.SubId LEFT OUTER JOIN Class_DependencyPassMarks cdp ON cs.ClassId=cdp.ClassId WHERE cs.ClassID='221' GROUP BY cs.ClassID,cs.SubId,ns.SubName,cdp.PassMarks");
        //        DataTable dt = new DataTable();
        //        dt = CRUD.ReturnTableNull(sql);
        //        ddl.DataTextField = "SubName";
        //        ddl.DataValueField = "SubId";
        //        ddl.DataSource = dt;
        //        ddl.DataBind();
        //        ddl.Items.Insert(0, new ListItem("...Select...", "0"));

        //    }
        //    catch { }

        //}



        //Get Related Subject
        public static void GetSujectList(DropDownList ddl,string ClassID)
        {
            try
            {
                sql = string.Format("SELECT  convert(varchar,cs.CSID)+'_'+ convert(varchar,cs.SubId)  as SubId,ns.SubName as SubName FROM ClassSubject cs INNER JOIN NewSubject ns ON cs.SubId=ns.SubId LEFT OUTER JOIN Class_DependencyPassMarks cdp ON cs.ClassId=cdp.ClassId WHERE cs.ClassID=" + ClassID + " ");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                ddl.DataTextField = "SubName";
                ddl.DataValueField = "SubId";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));

            }
            catch { }

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
