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
        string sql = string.Empty;
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

                DataTable dt = new DataTable();

                dt = CRUD.ReturnTableNull("SELECT [SubId],[SubName],[IsActive],[Ordering] FROM [dbo].[NewSubject] Order by Ordering ");
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
                    List<SubjectEntities> GetList = new List<SubjectEntities>();
                   // SubjectEntry se = new SubjectEntry();
                    GetList = SubjectEntry.GetEntitiesData.FindAll(x=>x.IsActive==true).ToList();

                    ddl.DataTextField = "SubjectName";
                    ddl.DataValueField = "SubjectId";
                    ddl.DataSource=GetList;
                    ddl.DataBind();
                    ddl.Items.Insert(0,new ListItem ("Select","0"));
                    
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
