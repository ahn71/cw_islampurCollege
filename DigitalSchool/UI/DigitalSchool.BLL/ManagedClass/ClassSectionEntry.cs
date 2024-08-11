using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedClass;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedClass
{
    public class ClassSectionEntry : IDisposable
    {
        static List<ClassSectionEntities> ClassNameList;
        private ClassSectionEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public ClassSectionEntry() { }
        public ClassSectionEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            if (_Entities.ClsGrpID == null)
            {
                sql = string.Format("INSERT INTO [dbo].[Tbl_Class_Section] " +
                    "([ClassID],[ClsGrpID],[SectionID]) VALUES ( '" + _Entities.ClassID + "','0','" + _Entities.SectionID + "')");
            }
            else
            {
                sql = string.Format("INSERT INTO [dbo].[Tbl_Class_Section] " +
                    "([ClassID],[ClsGrpID],[SectionID]) VALUES ( '" + _Entities.ClassID + "','" + _Entities.ClsGrpID + "','" + _Entities.SectionID + "')");
            }

            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Class_Section] SET " +
                "[ClassID] = " + _Entities.ClassID + ", " +
                "[ClsGrpID] = " + _Entities.ClsGrpID + ", " +
                "[SectionID]=" + _Entities.SectionID + " " +
                "WHERE [ClsSecID] = '" + _Entities.ClsSecID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<ClassSectionEntities> GetEntitiesData()
        {
            List<ClassSectionEntities> ListEntities = new List<ClassSectionEntities>();
            sql = string.Format("SELECT [dbo].Tbl_Class_Section.ClsSecID,[dbo].Tbl_Class_Section."
            + "ClassID,[dbo].Tbl_Class_Section.SectionID,[dbo].Classes.ClassName,[dbo].Sections.SectionName,Tbl_Class_Group.GroupID,"
            +"[dbo].Tbl_Class_Group.ClsGrpID,[dbo].Tbl_Group.GroupName FROM [dbo].Tbl_Class_Section inner "
            +"join [dbo].Classes on [dbo].Tbl_Class_Section.ClassID=[dbo].Classes.ClassID inner join [dbo]."
            +"Sections on [dbo].Tbl_Class_Section.SectionID=[dbo].Sections.SectionID left join Tbl_Class_Group  "
            +"on [dbo].Tbl_Class_Section.ClsGrpID=Tbl_Class_Group.ClsGrpID left JOIN Tbl_Group ON Tbl_Class_Group."
            +"GroupID=Tbl_Group.GroupID order by  [dbo].Classes.ClassOrder");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassSectionEntities
                                    {
                                        ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        GroupID =row["GroupID"].ToString()==""? 0: int.Parse(row["GroupID"].ToString()),
                                        GroupName = row["GroupName"].ToString(),
                                        ClsGrpID = row["ClsGrpID"].ToString() == "" ? 0 : int.Parse(row["ClsGrpID"].ToString()),
                                        SectionID = int.Parse(row["SectionID"].ToString()),
                                        SectionName = row["SectionName"].ToString()

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<ClassSectionEntities> GetEntitiesData(int ClassId)
        {
            List<ClassSectionEntities> ListEntities = new List<ClassSectionEntities>();
            sql = string.Format("SELECT [dbo].Tbl_Class_Section.ClsSecID,Tbl_Group.GroupID,[dbo].Tbl_Class_"
            +"Section.ClassID,[dbo].Tbl_Class_Section.SectionID,[dbo].Classes.ClassName,[dbo].Sections.SectionName,"
            +"[dbo].Tbl_Class_Group.ClsGrpID,[dbo].Tbl_Group.GroupName FROM [dbo].Tbl_Class_Section inner "
            +"join [dbo].Classes on [dbo].Tbl_Class_Section.ClassID=[dbo].Classes.ClassID inner join [dbo]."
            +"Sections on [dbo].Tbl_Class_Section.SectionID=[dbo].Sections.SectionID left join Tbl_Class_Group  "
            +"on [dbo].Tbl_Class_Section.ClsGrpID=Tbl_Class_Group.ClsGrpID left JOIN Tbl_Group ON Tbl_Class_Group."
            +"GroupID=Tbl_Group.GroupID  where [dbo].Tbl_Class_Section.ClassID=" + ClassId + " "
            + "order by  [dbo].Classes.ClassOrder");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassSectionEntities
                                    {
                                        ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        GroupID = row["GroupID"].ToString() == "" ? 0 : int.Parse(row["GroupID"].ToString()),
                                        GroupName = row["GroupName"].ToString(),
                                        ClsGrpID = row["ClsGrpID"].ToString() == "" ? 0 : int.Parse(row["ClsGrpID"].ToString()),
                                        SectionID = int.Parse(row["SectionID"].ToString()),
                                        SectionName = row["SectionName"].ToString()

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void GetEntitiesData(DropDownList dl,int classId,string clsgroupId)
        {
            try
            {
                ClassSectionEntry SectionName = new ClassSectionEntry();
                var sectionList = ClassNameList;                
                ClassNameList = SectionName.GetEntitiesData(classId);               
                if (ClassNameList != null)
                {
                    if (clsgroupId != "0")
                    {
                        sectionList = ClassNameList.FindAll(c => c.ClassID == classId && c.ClsGrpID == int.Parse(clsgroupId));
                    }
                    else
                    {
                        sectionList = ClassNameList.FindAll(c => c.ClassID == classId);
                    }
                    dl.DataTextField = "SectionName";
                    dl.DataValueField = "ClsSecID";
                    dl.DataSource = sectionList;
                    dl.DataBind();
                    dl.Items.Insert(0, new ListItem("...Select...", "0"));
                }
                else
                {
                    dl.Items.Clear();
                }
                
               
            }
            catch { }
        }
        public static void GetSectionListByBatchId(DropDownList ddl, string BatchId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ClsSecID,SectionName from v_Tbl_Class_Section where BatchId=" + BatchId + "");
                ddl.DataTextField = "SectionName";
                ddl.DataValueField = "ClsSecID";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public static void GetSectionListByBatchId_ClsGrpId(DropDownList ddl, string BatchId, string ClsGrpID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ClsSecID,SectionName from v_Tbl_Class_Section where BatchId=" + BatchId + " AND ClsGrpId=" + ClsGrpID + "");
                ddl.DataTextField = "SectionName";
                ddl.DataValueField = "ClsSecID";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
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
