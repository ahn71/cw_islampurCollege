using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ControlPanel;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ControlPanel
{
    public class UserTypeInfoEntry :IDisposable
    {
        private UserTypeInfo utf;
        public bool result = false;

        public bool Insert(string UserType)
        {
            try
            {
                result = CRUD.ExecuteQuery("Insert into UserTypeInfo (UserType) values ('" + UserType + "')");
                return result;
            }
            catch { return false; }
           
        }

        public bool Update(string UserType, string UserTypeId)
        {
            try
            {
                result = CRUD.ExecuteQuery("Update UserTypeInfo set UserType='"+UserType+"' where UserTypeId="+UserTypeId+"");
                return result;
            }
            catch { return false; }
        }

        public bool InsertUserTypeId()
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select Max(UserTypeId) as UserTypeId from UserTypeInfo where IsActive='True'");
                result = CRUD.ExecuteQuery("Insert Into UserTypeInfo_ModulePrivilege (UserTypeId) values (" + dt.Rows[0]["UserTypeId"].ToString() + ")");
                return result;
            }
            catch { return false; }
        }

        public List<UserTypeInfo> GetUserTypeList()
        {
            try
            {
                List<UserTypeInfo> UserList = new List<UserTypeInfo>();

                DataTable dt = CRUD.ReturnTableNull("select UserTypeId,UserType from UserTypeInfo");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        UserList = (from DataRow dr in dt.Rows
                                    select new UserTypeInfo
                                    {
                                        UserType = dr["UserType"].ToString(),
                                        UserTypeId =int.Parse(dr["UserTypeId"].ToString())
                                    }).ToList();

                        return UserList;
                    
                    }
                }
                return null;
               
            }
            catch { return null; }
        }

        public List<PageInfo> GetPageInfoList()
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select PageNameId,PageName,PageTitle,ModuleType from PageInfo where Chosen='false'");
                List<PageInfo> PageList = new List<PageInfo>();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        PageList=(from DataRow dr in dt.Rows 
                                  select new PageInfo 
                                  {
                                  PageNameId=int.Parse(dr["PageNameId"].ToString()),
                                  PageName=dr["PageName"].ToString(),
                                  PageTitle=dr["PageTitle"].ToString(),
                                  ModuleType = dr["ModuleType"].ToString()
                                  }).ToList();
                        return PageList;
                    
                    }
                }
                return null;

            }
            catch { return null; }
        
    
        }

        public static void GetUserTypeList_inDropdownList(DropDownList ddl)
        {
            DataTable dt = CRUD.ReturnTableNull("select UserTypeId,UserType from UserTypeInfo where IsActive='true'");
            List<UserTypeInfo> UserTypeList = new List<UserTypeInfo>();
            if (dt != null && dt.Rows.Count > 0)
            {
                UserTypeList = (from DataRow dr in dt.Rows
                                select new UserTypeInfo
                                {
                                    UserTypeId = int.Parse(dr["UserTypeId"].ToString()),
                                    UserType = dr["UserType"].ToString()
                                }).ToList();

                ddl.DataTextField = "UserType";
                ddl.DataValueField = "UserTypeId";
                ddl.DataSource = UserTypeList;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        public static void GetUserTypeList(DropDownList ddl)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select UserTypeId,UserType from UserTypeInfo where IsActive='true'");
                List<UserTypeInfo> UserTypeList = new List<UserTypeInfo>();
                if (dt !=null && dt.Rows.Count>0)
                {
                    UserTypeList = (from DataRow dr in dt.Rows
                                    select new UserTypeInfo
                                    {
                                        UserTypeId = int.Parse(dr["UserTypeId"].ToString()),
                                        UserType = dr["UserType"].ToString()
                                    }).ToList();

                    ddl.DataTextField = "UserType";
                    ddl.DataValueField = "UserTypeId";
                    ddl.DataSource =UserTypeList;
                    ddl.DataBind();

 
                 
                }

                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch {  }
        }

        public static void GetPageInfoListByUserType(string UserTypeId,GridView gv)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select UserTypeId,PageNameId,PageTitle,ModuleType,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction from v_UserTypePageInfo_AccurateData where UserTypeId=" + UserTypeId + "");
                List<v_UserTypePageInfo> GetPageList = new List<v_UserTypePageInfo>();
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    GetPageList = (from DataRow dr in dt.Rows
                                   select new v_UserTypePageInfo
                                   {
                                       PageNameId = int.Parse(dr["PageNameId"].ToString()),
                                       PageTitle = dr["PageTitle"].ToString(),
                                       ModuleType = dr["ModuleType"].ToString(),
                                       ViewAction = (dr["ViewAction"].ToString() == "" || dr["ViewAction"].ToString().ToLower() == "false") ? false : true,
                                       SaveAction = (dr["SaveAction"].ToString() == "" || dr["SaveAction"].ToString().ToLower() == "false") ? false : true,
                                       UpdateAction = (dr["UpdateAction"].ToString() == "" || dr["UpdateAction"].ToString().ToLower() == "false") ? false : true,
                                       DeleteAction = (dr["DeleteAction"].ToString() == "" || dr["DeleteAction"].ToString().ToLower() == "false") ? false : true,
                                       GenerateAction = (dr["GenerateAction"].ToString() == "" || dr["GenerateAction"].ToString().ToLower() == "false") ? false : true,
                                       AllAction = ((dr["ViewAction"].ToString().ToLower() == "true") && (dr["SaveAction"].ToString().ToLower() == "true") || (dr["UpdateAction"].ToString().ToLower() == "true")) ? true : false
                                   }).ToList();                               
                }
                dt = new DataTable();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("select PageNameId,PageTitle,ModuleType from PageInfo where PageNameId not in(select PageNameId from v_UserTypePageInfo_AccurateData where UserTypeId=" + UserTypeId + ")", DbConnection.Connection);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    GetPageList.Add(new v_UserTypePageInfo()
                    {
                        PageNameId = int.Parse(dr["PageNameId"].ToString()),
                        PageTitle = dr["PageTitle"].ToString(),
                        ModuleType = dr["ModuleType"].ToString(),
                        ViewAction = false,
                        SaveAction = false,
                        UpdateAction = false,
                        DeleteAction = false,
                        GenerateAction = false,
                        AllAction = false
                    });
                }

                gv.DataSource = GetPageList;
                gv.DataBind();
            }
            catch { }
        }

        public static void GetUserTypeModulePrivilege(GridView gv)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select UserTypeDId,UserTypeId,UserType,AcademicModule,AdministrationModule,NotificationModule,ReportsModule from v_UserTypeInfo_ModulePrivilege where IsActive ='true'");
                if (dt!=null && dt.Rows.Count>0)
                {
                    List <v_UserTypeInfo_ModulePrivilege> ModulePrivilegeSet = new List <v_UserTypeInfo_ModulePrivilege>();

                    ModulePrivilegeSet = (from DataRow dr in dt.Rows
                                          select new v_UserTypeInfo_ModulePrivilege {
                                              UserTypeDId = int.Parse(dr["UserTypeDId"].ToString()),
                                              UserTypeId = int.Parse(dr["UserTypeId"].ToString()),
                                              UserType = dr["UserType"].ToString(),
                                              AcademicModule = bool.Parse(dr["AcademicModule"].ToString()),
                                              AdministrationModule = bool.Parse(dr["AdministrationModule"].ToString()),
                                              NotificationModule = bool.Parse(dr["NotificationModule"].ToString()),
                                              ReportsModule = bool.Parse(dr["ReportsModule"].ToString())

                                          }).ToList();
                    gv.DataSource = dt;
                    gv.DataBind();
                }
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
