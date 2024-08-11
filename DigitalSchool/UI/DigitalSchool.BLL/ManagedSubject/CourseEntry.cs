using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedSubject;
using System.Data;
using DS.DAL;
using System.Web.UI.WebControls;
namespace DS.BLL.ManagedSubject
{
    public class CourseEntry
    {
        private CourseEntity course_entity;
        bool result;
        DataTable dt;


        public CourseEntity SetValues
        {
            set
            {
                course_entity = value;
            
            }
        }

        public Boolean Insert()
        {
            try
            {
                result = CRUD.ExecuteQuery("insert into AddCourseWithSubject (CourseName,Ordering,SubId) values ('" + course_entity.CourseName + "','" + course_entity.Ordering + "','" + course_entity.SubId + "')");
                return result;
            }
            catch { return false; }
        }

        public Boolean Update()
        {
            try
            {
                result = CRUD.ExecuteQuery("update AddCourseWithSubject set CourseName='" + course_entity.CourseName + "',Ordering='" + course_entity.Ordering + "',SubId='" + course_entity.SubId + "' where CourseId =" + course_entity.CourseId + "");
                return result;
            }
            catch { return false; }
        }

        public static List<CourseEntity> GetCourseList
        {
            get
            {
                try
                {
                   
                    DataTable dt=new DataTable ();
                    dt = CRUD.ReturnTableNull("select c.CourseId,c.CourseName,c.Ordering,c.SubId,s.SubName from AddCourseWithSubject as c  inner join NewSubject as s on c.SubId=s.SubId  ");
                    if (dt.Rows.Count >= 0)
                    {
                        List<CourseEntity> GetList = new List<CourseEntity>();
                        GetList = (from DataRow dr in dt.Rows
                                   select new CourseEntity
                                   {
                                       CourseId = int.Parse(dr["CourseId"].ToString()),
                                       CourseName = dr["CourseName"].ToString(),
                                       Ordering = int.Parse(dr["Ordering"].ToString()),
                                       SubId = int.Parse(dr["SubId"].ToString()),
                                       subName=dr["SubName"].ToString()
                                   }).ToList();
                        return GetList;
                    }
                    return null;
                }
                catch { return null; }
            
            }
        }

        public static void GetCourseListBySubject(DropDownList ddl,string getSubId)
        {
            try
            {
                ddl.Items.Clear();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select CourseName,CourseId from AddCourseWithSubject where SubId=" + getSubId + "");

                ddl.DataTextField = "CourseName";
                ddl.DataValueField = "CourseId";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("select", "0"));
                
            }
            catch { }
        }
    }
}
