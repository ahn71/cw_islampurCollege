using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Settings.GeneralSettings
{
    public partial class AddThana : System.Web.UI.Page
    {
        DataTable dt;
        string sqlcmd = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddThana.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadDistricts();
                    loadThanaList();
                   // savethana();
                }
            }
            catch { }
        }

        private void loadDistricts()
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("select DistrictID, districtName from Distritcts", dt);
            dlDistrict.DataValueField = "DistrictID";
            dlDistrict.DataTextField = "DistrictName";
            dlDistrict.DataSource = dt;
            dlDistrict.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblThanaId.Value.Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadThanaList(); return; }
                saveThana();
            }
            else updateThanas();
        }
        private void saveThana()
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
                string[] getColumns = { "DistrictId", "ThanaName", "ThanaNameBn" };
                string[] getValues = { dlDistrict.SelectedValue, txtThana.Text.Trim(),txtThanaBn.Text.Trim() };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("Thanas", getColumns, getValues,DbConnection.Connection) == true)
                {
                    lblMessage.InnerText = "success-> Add Successfully";
                    loadThanaList();
                    clear();
                }

            }
            catch (Exception ex)
            {
             lblMessage.InnerText="error->"+ex.Message+"";
            }
        }

        private Boolean updateThanas()
        {
            try
            {
                getDistrictId();
                SqlCommand cmd = new SqlCommand(" update Thanas  Set DistrictId=@DistrictId, ThanaName=@ThanaName, ThanaNameBn=@ThanaNameBn where ThanaId=@ThanaId ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@ThanaId", lblThanaId.Value.ToString());
                cmd.Parameters.AddWithValue("@DistrictId",dlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@ThanaName", txtThana.Text.Trim());
                cmd.Parameters.AddWithValue("@ThanaNameBn", txtThanaBn.Text.Trim());

                cmd.ExecuteNonQuery();
                loadThanaList();
                lblMessage.InnerText = "success->Update Successfully";
                lblThanaId.Value = "";
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
                txtThana.Text = "";
                txtThanaBn.Text = "";
                btnSave.Text = "Save";
                txtThana.Focus();
            }
            catch { }
        }



        private void loadThanaList()
        {
            if(dlDistrict.SelectedValue=="0")
                sqlcmd = "Select ThanaId,DistrictId,DistrictName,ThanaName,ThanaNameBn from v_ThanaDetails  Order by ThanaId ";
            else
                sqlcmd = "Select ThanaId,DistrictId,DistrictName,ThanaName,ThanaNameBn from v_ThanaDetails where DistrictID=" + dlDistrict.SelectedValue+"  Order by ThanaId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);
            int totalRows = dt.Rows.Count;

            string divInfo = "";

            divThanaList.Controls.Add(new LiteralControl(divInfo));
           

            divInfo = " <table id='tblThana' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>ThanaName</th>";
            divInfo += "<th>থানা/উপজেলা</th>";
            divInfo += "<th>DistrictName</th>";            
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
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

                id = dt.Rows[x]["ThanaId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td style='display:none'>" + dt.Rows[x]["DistrictId"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["ThanaName"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["ThanaNameBn"].ToString() + "</td>";
                divInfo += "<td>" + dt.Rows[x]["DistrictName"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editThana(" + id + ");'  />";


            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";

            divInfo += "<div class='dataTables_wrapper'><div class='head'> </div></div>";
            divThanaList.Controls.Add(new LiteralControl(divInfo));

        }

        protected void dlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadThanaList();
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