﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedClass;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedClass
{
    public class ClassGroupEntry : IDisposable
    {
        private ClassGroupEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public ClassGroupEntry() { }
        public List<ClassGroupEntities> grpList;       

        public ClassGroupEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Class_Group] " +
                "([ClassID],[GroupID]) VALUES ( " + _Entities.ClassID + "," + _Entities.GroupID + ")");

            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Class_Group] SET " +
                "[ClassID] = " + _Entities.ClassID + ", " +
                "[GroupID]=" + _Entities.GroupID + " " +
                "WHERE [ClsGrpID] = '" + _Entities.ClsGrpID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<ClassGroupEntities> GetEntitiesData()
        {
            List<ClassGroupEntities> ListEntities = new List<ClassGroupEntities>();
            sql = string.Format("SELECT [dbo].[Tbl_Class_Group].[ClsGrpID],[dbo].Tbl_Class_Group.ClassID,[dbo].Tbl_Class_Group.GroupID,"
            + "[dbo].Classes.ClassName,[dbo].Tbl_Group.GroupName "
            + " FROM [dbo].Tbl_Class_Group inner join [dbo].Classes on [dbo].Tbl_Class_Group.ClassID"
            + "=[dbo].Classes.ClassID inner join [dbo].Tbl_Group on [dbo].Tbl_Class_Group.GroupID=[dbo].Tbl_Group.GroupID");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassGroupEntities
                                    {
                                        ClsGrpID = int.Parse(row["ClsGrpID"].ToString()),
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        GroupID = int.Parse(row["GroupID"].ToString()),
                                        GroupName = row["GroupName"].ToString()

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public void GetDropDownListClsGrpId(int classId, DropDownList dl) // For GroupId Replease by ClsGrpId
        {
            ClassGroupEntry clsGrp = new ClassGroupEntry();

            if (grpList == null)
            {
                grpList = clsGrp.GetEntitiesData();
            }
            if (grpList == null)
            {
                dl.Items.Clear();
                dl.Items.Insert(0, new ListItem("...No Group...", "0"));
                dl.Enabled = false;
                return;
            }
            var grp = grpList.FindAll(c => c.ClassID == classId);
            if (grp.Count > 0)
            {
                dl.DataTextField = "GroupName";
                dl.DataValueField = "ClsGrpID";
                dl.DataSource = grp.ToList();
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
                dl.Enabled = true;
            }
            else
            {
                dl.DataTextField = "GroupName";
                dl.DataValueField = "ClsGrpID";
                dl.DataSource = grpList;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...No Group...", "0"));
                dl.Enabled = false;
            }
        }
        public void GetDropDownList(int classId, DropDownList dl)
        {
            ClassGroupEntry clsGrp = new ClassGroupEntry();
            
            if (grpList == null)
            {
                grpList = clsGrp.GetEntitiesData();               
            }
            if (grpList == null)
            {
                dl.Items.Insert(0, new ListItem("...No Group...", "0"));
                dl.Enabled = false;
                return;
            }
            var grp = grpList.FindAll(c => c.ClassID == classId);
            if (grp.Count > 0)
            {
                dl.DataTextField = "GroupName";
                dl.DataValueField = "GroupID";
                dl.DataSource = grp.ToList();
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
                dl.Enabled = true;                
            }
            else
            {
                dl.DataTextField = "GroupName";
                dl.DataValueField = "GroupID";
                dl.DataSource = grpList;
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...No Group...", "0"));
                dl.Enabled = false;                
            }           
        }

        public  void GetDropDownAllList(DropDownList dl) // For GroupId Replease by ClsGrpId
        {
            ClassGroupEntry clsGrp = new ClassGroupEntry();           
            grpList = clsGrp.GetEntitiesData();
            
            dl.DataTextField = "GroupName";
            dl.DataValueField = "ClsGrpID";
            dl.DataSource = grpList;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...No Group...", "0"));
            dl.Enabled = false;            
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
