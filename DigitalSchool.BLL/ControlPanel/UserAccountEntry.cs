using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.BLL.ControlPanel;
using DS.PropertyEntities.Model.ControlPanel;
using DS.DAL;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace DS.BLL.ControlPanel
{
    public class UserAccountEntry :IDisposable
    {
        private UserAccount _Entities;
        public bool result;

        public UserAccount SetValues
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            try
            {
                result = CRUD.ExecuteQuery("insert into UserAccount (FirstName,LastName,Email,Status,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,UserName,UserPassword,UserTypeId,EID,IsAdviser)" +
                            "values('" + _Entities.FirstName + "','" + _Entities.LastName + "','" + _Entities.Email + "','" 
                            + _Entities.Status + "','" + _Entities.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss") + "'," + _Entities.CreatedBy + ",'" 
                            + _Entities.ModifiedOn.ToString("yyyy-MM-dd HH:mm:ss") + "'," + _Entities.ModifiedBy + ",'" + _Entities.UserName + "','" 
                            + _Entities.UserPassword + "',"+_Entities.UserTypeId+",'"+_Entities.EID+"','"+_Entities.IsAdviser+"')");

                
                return result;
                    
            }
            catch { return false; }
        
        }

        public bool InsertDetails(DropDownList ddl)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("Select Max(UserId) as UserId from UserAccount");

                for (byte b = 0; b < ddl.Items.Count; b++)
                {
                    if (ddl.Items[b].Selected)
                    result = CRUD.ExecuteQuery("insert into UserAccountDetails (UserId,UserTypeId,IsActive)" +
                                "values('" + dt.Rows[0]["UserId"].ToString() + "','" + ddl.Items[b].Value + "','" + 1 + "')");

                }
                return result;

            }
            catch { return false; }

        }

        public bool Update(string UserId)
        {
            try
            {
                 result=CRUD.ExecuteQuery("Update UserAccount set FirstName='"+_Entities.FirstName+"',LastName='"+_Entities.LastName+"',UserPassword='"+_Entities.UserPassword+"',Email='"+_Entities.Email+"',ModifiedBy='"+_Entities.ModifiedBy+"',ModifiedOn='"+_Entities.ModifiedOn+"',Status='"+_Entities.Status+"', UserTypeId="+_Entities.UserTypeId+" where UserId="+UserId+"");
                return result;
            }
            catch { return false; }
       
        }
        public bool UpdatePassword(string EID)
        {
            try
            {
                result = CRUD.ExecuteQuery("Update UserAccount set UserPassword='"+_Entities.UserPassword+"' WHERE EID='"+EID+"'");
                return result;
            }
            catch { return false; }
        }
        public bool UpdateStatus(string UserId,string status)
        {
            try
            {
                result = CRUD.ExecuteQuery("Update UserAccount set Status='" +status + "' where UserId=" + UserId + "");
                return result;
            }
            catch { return false; }
        }
        public bool UpdateIsEvaluator(string UserId, string status)
        {
            try
            {
                result = CRUD.ExecuteQuery("Update UserAccount set IsEvaluator='" + status + "' where UserId=" + UserId + "");
                return result;
            }
            catch { return false; }
        }

        public bool UpdateAccountDetails(CheckBoxList chkBoxList,string UserId)
        {
            try 
            {
                for (byte b = 0; b < chkBoxList.Items.Count; b++)
                {
                    if (chkBoxList.Items[b].Selected)
                    {
                        SqlCommand cmd = new SqlCommand("Update UserAccountDetails set UserTypeId=" + chkBoxList.Items[b].Value + ",IsActive=" + 1 + " where UserId=" + UserId + " AND UserTypeId=" + chkBoxList.Items[b].Value + " ", DbConnection.Connection);
                        result = cmd.ExecuteNonQuery() == 1;
                        if (!result)
                        {
                            cmd = new SqlCommand("Insert into UserAccountDetails (UserId,UserTypeId,IsActive) values (" + UserId + "," + chkBoxList.Items[b].Value + "," + 1 + ")", DbConnection.Connection);
                            result = cmd.ExecuteNonQuery() == 1;
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Delete from UserAccountDetails where UserId=" + UserId + " AND UserTypeId=" + chkBoxList.Items[b].Value + " ", DbConnection.Connection);
                        result = cmd.ExecuteNonQuery() == 1;

                        if (result)
                        {
                            cmd = new SqlCommand("Delete from UserPrivilege Where UserId="+UserId+" AND PageNameId in (select PageNameId from PageInfo where UserTypeId="+chkBoxList.Items[b].Value+" AND Chosen='true')",DbConnection.Connection);
                            result=cmd.ExecuteNonQuery() == 1;
                        }

                        result =true;
                    
                    }
                
                }
                return result;
            }
            catch { return false; }
        }



        public static void GetCurrentUserList(DropDownList ddl)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select Distinct FirstName+' '+LastName as FullName,UserId from UserAccount Where Status='True'");


                ddl.DataValueField = "UserId";
                ddl.DataTextField = "FullName";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public static void GetCurrentEvaluatorList(CheckBoxList cbl)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select Distinct FirstName+' '+LastName as FullName,UserId from UserAccount Where Status='True' and IsEvaluator=1 ");


                cbl.DataValueField = "UserId";
                cbl.DataTextField = "FullName";
                cbl.DataSource = dt;
                cbl.DataBind();
                
            }
            catch { }
        }

        public static string GetTitel(string UserId, string FullName)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("Select UserType,UserTypeId,UserId From v_UserAccountDetails Where Status='True' AND  UserId='" + UserId + "'");
                string Title="";
                string UserTypeId = "";
                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Title += "|" + dr["UserType"].ToString();
                        UserTypeId += dr["UserTypeId"].ToString()+",";
                    }

                }
                else
                {
                    Title += "|" + dt.Rows[0]["UserType"].ToString();
                    UserTypeId += dt.Rows[0]["UserTypeId"].ToString()+",";

                }
                UserTypeId = UserTypeId.Substring(0, UserTypeId.LastIndexOf(','));
                return Title = FullName + Title + "_" + UserTypeId;
            }
            catch { return null; }
        }

        public static void GetPageListByUser(string UserTypeId,bool IsNewUser,GridView gv,string UserId)
        {
            try
            {
                DataTable dt = new DataTable();
                List<v_UserTypePageInfo> getPageList = new List<v_UserTypePageInfo>();

                if (!IsNewUser)  // for old user 
                {
                    dt = CRUD.ReturnTableNull("select PageNameId,PageTitle,ModuleType,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction from v_UserPrivilege where UserId =" + UserId + "");
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        getPageList = (from DataRow dr in dt.Rows
                                       select new v_UserTypePageInfo
                                       {
                                           PageNameId = int.Parse(dr["PageNameId"].ToString()),
                                           PageTitle = dr["PageTitle"].ToString(),
                                           ModuleType = dr["ModuleType"].ToString(),
                                           ViewAction = bool.Parse(dr["ViewAction"].ToString()),
                                           SaveAction = bool.Parse(dr["SaveAction"].ToString()),
                                           UpdateAction = bool.Parse(dr["UpdateAction"].ToString()),
                                           DeleteAction = bool.Parse(dr["DeleteAction"].ToString()),
                                           GenerateAction = bool.Parse(dr["GenerateAction"].ToString()),

                                       }).ToList();
                    }
                    dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select PageNameId,PageTitle,ModuleType from v_UserTypePageInfo where UserTypeId in (" + UserTypeId + ") AND PageNameId not in (select PageNameId from v_UserPrivilege where UserId ="+UserId+")", DbConnection.Connection);
                    da.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            getPageList.Add(new v_UserTypePageInfo() { PageNameId = int.Parse(dr["PageNameId"].ToString()), PageTitle = dr["PageTitle"].ToString(), ModuleType = dr["ModuleType"].ToString(), ViewAction = false, SaveAction = false, UpdateAction = false, DeleteAction = false, GenerateAction = false });
                        }
                    }

                }
                else // for new user
                {
                    SqlDataAdapter da = new SqlDataAdapter("select PageNameId,PageTitle,ModuleType from v_UserTypePageInfo where UserTypeId in (" + UserTypeId + ")", DbConnection.Connection);
                    da.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        getPageList = (from DataRow dr in dt.Rows
                                       select new v_UserTypePageInfo
                                       {
                                           PageNameId = int.Parse(dr["PageNameId"].ToString()),
                                           PageTitle = dr["PageTitle"].ToString(),
                                           ModuleType = dr["ModuleType"].ToString(),
                                           ViewAction = false,
                                           SaveAction = false,
                                           DeleteAction = false,
                                           UpdateAction = false,
                                           GenerateAction = false

                                       }).ToList();
                    }
                }
                gv.DataSource = getPageList;
                gv.DataBind();
            }
            catch (Exception ex)
            { 
            }
        }

        public static void getUserList(GridView gv)
        {
            
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select UserId,FirstName,LastName,UserName,UserPassword,Email,CreatedOn,Status,UserType,IsEvaluator,case when IsEvaluator=1 then 'Yes' else 'No' End as Evaluator,case when IsEvaluator=1 then 'Green' else 'Red' End as EvaluatorColor from v_UserAccount");
                gv.DataSource = dt;
                gv.DataBind();
            }
            catch { }
           
        }
        public static DataTable GetAdviserUserAccount(string EID)
        {
            DataTable dt=new DataTable();
            try
            {
                dt = CRUD.ReturnTableNull("SELECT UserName,UserPassword FROM v_UserAccount WHERE EID='"+EID+"'");
                return dt;
            }
            catch {return dt; }
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
