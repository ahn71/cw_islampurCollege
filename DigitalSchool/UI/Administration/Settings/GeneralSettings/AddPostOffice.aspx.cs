using adviitRuntimeScripting;
using DS.BLL.ControlPanel;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Settings.GeneralSettings
{
    public partial class AddPostOffice : System.Web.UI.Page
    {
        DataTable dt;
        string sqlcmd = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddPostOffice.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadDistricts();
                    loadPostOfficeList();
                    // savethana();
                }
            }
            catch { }
        }

        private void loadDistricts()
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select districtName,DistrictId from Distritcts");           
            dlDistrict.DataTextField = "DistrictName";
            dlDistrict.DataValueField = "DistrictId";
            dlDistrict.DataSource = dt;
            dlDistrict.DataBind();
            dlDistrict.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPostOfficeId.Value.Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadPostOfficeList(); return; }
                savePostOffice();
            }
            else updatePostOffice();
        }
        private void savePostOffice()
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
                string[] getColumns = { "ThanaId", "DistrictId", "PostOfficeName", "PostOfficeNameBn" };
                string[] getValues = { dlThana.SelectedValue, dlDistrict.SelectedValue,txtPostOffice.Text,txtPostOfficeBn.Text.Trim() };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("Post_Office", getColumns, getValues, DbConnection.Connection) == true)
                {
                    lblMessage.InnerText = "success-> Add Successfully";
                    loadPostOfficeList();
                    clear();
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message + "";
            }
        }

        private Boolean updatePostOffice()
        {
            try
            {
                getDistrictId();
                //SqlCommand cmd = new SqlCommand(" update Post_Office  Set DistrictId=@DistrictId, ThanaId=@ThanaId,PostOfficeName=@PostOfficeName,PostOfficeNameBn=@PostOfficeNameBn where PostOfficeID=@PostOfficeID", DbConnection.Connection);

                SqlCommand cmd = new SqlCommand(" update Post_Office  Set PostOfficeName=@PostOfficeName,PostOfficeNameBn=@PostOfficeNameBn where PostOfficeID=@PostOfficeID", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@PostOfficeID", lblPostOfficeId.Value.ToString());
                //cmd.Parameters.AddWithValue("@ThanaId", dlThana.SelectedValue);
                //cmd.Parameters.AddWithValue("@DistrictId", dlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@PostOfficeName", txtPostOffice.Text.Trim());
                cmd.Parameters.AddWithValue("@PostOfficeNameBn", txtPostOfficeBn.Text.Trim());

                cmd.ExecuteNonQuery();
                loadPostOfficeList();
                lblMessage.InnerText = "success->Update Successfully";
                lblPostOfficeId.Value = "";
                clear();
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private string getDistrictId()
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("select DistrictId from Distritcts where DistrictName='" + dlDistrict.Text.Trim() + "'", dt);
                return dt.Rows[0].ItemArray[0].ToString();
            }
            catch { return ""; }
        }


        private void clear()
        {
            try
            {
                txtPostOffice.Text = "";
                txtPostOfficeBn.Text = "";
                btnSave.Text = "Save";
                txtPostOffice.Focus();
            }
            catch { }
        }



        private void loadPostOfficeList()
        {
            string condition = "";
           
            if (dlDistrict.SelectedValue != "0")
            {
                if (dlThana.SelectedValue != "0")
                    condition = " where Post_Office.DistrictId=" + dlDistrict.SelectedValue+ " and Post_Office.ThanaId=" + dlThana.SelectedValue;
                else
                    condition = " where Post_Office.DistrictId=" + dlDistrict.SelectedValue;
            }
            sqlcmd = "Select Post_Office.ThanaId, Post_Office.DistrictId, PostOfficeID, Post_Office.PostOfficeName,Post_Office.PostOfficeNameBn,Distritcts.DistrictName,Thanas.ThanaName FROM Post_Office INNER JOIN Thanas ON Post_Office.ThanaId=Thanas.ThanaId INNER JOIN Distritcts ON Post_Office.DistrictId=Distritcts.DistrictId "+ condition;
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sqlcmd);
            //sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;

            string divInfo = "";

            divThanaList.Controls.Add(new LiteralControl(divInfo));


            divInfo = " <table id='tblThana' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>District Name</th>";
            divInfo += "<th>Thana Name</th>";
            divInfo += "<th>Post Office Name</th>";
            divInfo += "<th>ডাকঘর</th>";
            if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Action</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody>";
                divInfo += "</table>";
                divThanaList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["PostOfficeID"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td style='display:none'>" + dt.Rows[x]["DistrictId"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["DistrictName"].ToString() + "</td>";
                divInfo += "<td style='display:none'>" + dt.Rows[x]["ThanaId"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["ThanaName"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["PostOfficeName"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["PostOfficeNameBn"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editPostOffice(" + id + ");'  />";


            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";

            divInfo += "<div class='dataTables_wrapper'><div class='head'> </div></div>";
            divThanaList.Controls.Add(new LiteralControl(divInfo));

        }

        protected void dlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadThana();
            loadPostOfficeList();
        }
        private void loadThana()
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select ThanaName,ThanaId from Thanas where DistrictId='"+dlDistrict.SelectedValue+"'");
            dlThana.DataTextField = "ThanaName";
            dlThana.DataValueField = "ThanaId";
            dlThana.DataSource = dt;
            dlThana.DataBind();
        }

        protected void dlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPostOfficeList();
        }
        /*
private void savethana()
{
   for (int i = 0; i < menu_one_list.Items.Count; i++)
   {
       if (i != 0)
       {
           try
           {

               string[] getColumns = { "DistrictId", "ThanaName" };
               string[] getValues = { "290", menu_one_list.Items[i].Text };
               if (ComplexScriptingSystem.SQLOperation.forSaveValue("Thanas", getColumns, getValues, DbConnection.Connection) == true)
               {
                   lblMessage.InnerText = "success-> Save Successfull";
                   loadThanaList("");
                   clear();
               }

           }
           catch (Exception ex)
           {
               lblMessage.InnerText = "error->" + ex.Message + "";
           }
       }
   }
}
*/
    }
}