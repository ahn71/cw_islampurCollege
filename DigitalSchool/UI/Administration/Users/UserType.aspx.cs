using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.ControlPanel;
using DS.BLL.ControlPanel;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using System.Data;
using DS.DAL;

namespace DS.UI.Administration.Users
{
    public partial class UserType : System.Web.UI.Page
    {
        UserTypeInfoEntry utie;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["hasperm"].ToString() != null) lblMessage.InnerText = "warning->You need permission to perform this action.";
                }
                catch { }
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "UserType.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                DataBindToView();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (lblUserTypeId.Value.ToString() == string.Empty)
            {
                lblUserTypeId.Value = "0";
                if (saveUserType())
                {
                   // DataTable dt=new DataTable ();
                   // sqlDB.fillDataTable("select Max(UserTypeId) as UserTypeId from UserTypeInfo ",dt);
                   // ViewState["__UserTypeId__"] = dt.Rows[0]["UserTypeId"].ToString();
                   
                    // List<PageInfo> PageList = new List<PageInfo>();
                    
                   // utie = new UserTypeInfoEntry();
                   // PageList = utie.GetPageInfoList();
                   // gvPageInfoList.DataSource = PageList;
                    //gvPageInfoList.DataBind();
                    
                  //  showDependencyModal.Show();
                }
            }
            else
            {
                updateUserType();
            }

        }

        private bool  saveUserType()
        {
            try
            {
                if (utie == null)
                {
                    utie = new UserTypeInfoEntry();
                    if (utie.Insert(TxtName.Text.Trim()))
                    {
                        if (utie.InsertUserTypeId())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                            DataBindToView(); return true;
                        }
                    }
                }
                return false;
            }
            catch { return false; }
        }

        private void updateUserType()
        {
            try
            {
                if (utie == null)
                {
                    utie = new UserTypeInfoEntry();
                    if (utie.Update(TxtName.Text.Trim(), lblUserTypeId.Value.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);                      
                        DataBindToView();
                    }
                }
            }
            catch { }
        }

        private void DataBindToView()
        {
            string divInfo = string.Empty;
            if (utie == null)
            {
                utie = new UserTypeInfoEntry(); 
            }
            List<UserTypeInfo> uti = utie.GetUserTypeList();
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>All User Type Name</th>";
            divInfo += "<th>Privilege</th>";
            divInfo += "<th>Edit</th>";
           
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (uti == null)
            {
                divInfo += "<tr><td colspan='2'>No Class Time Set Name available</td></tr>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = string.Empty;
            for (int x = 0; x < uti.Count; x++)
            {
                id = uti[x].UserTypeId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=ClsTimeSetName" + id + ">" + uti[x].UserType.ToString() + "</span></td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/datatables/privilege.png' class='editImg' onclick='NP(" + id + ");' />";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editRow(" + id + ");' />";
               
               
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }

        protected void chkChosen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[1].FindControl("chkChosen");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                SqlCommand cmd = new SqlCommand("delete from UserTypePageInfo where UserTypeId=" + ViewState["__UserTypeId__"].ToString() + " AND PageNameId=" + PageNameId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,Chosen) values(" + ViewState["__UserTypeId__"].ToString() + "," + PageNameId + ","+Action+")", DbConnection.Connection);
                cmd.ExecuteNonQuery();
                
                cmd = new SqlCommand("Update PageInfo set Chosen=" + Action + ",UserTypeId=" + ViewState["__UserTypeId__"].ToString() + " where PageNameId=" + PageNameId + "",DbConnection.Connection);
                cmd.ExecuteNonQuery();
                
                showDependencyModal.Show();

            }
            catch { }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            showDependencyModal.Hide();
        }
    }
}