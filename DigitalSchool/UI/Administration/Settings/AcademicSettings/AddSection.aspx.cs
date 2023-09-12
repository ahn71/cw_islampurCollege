﻿using DS.BLL.ControlPanel;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL;

namespace DS.UI.Administration.Settings.AcademicSettings
{
    public partial class AddSection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddSection.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
               
                    loadSection("");
                }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblSectionID.Value.ToString().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadSection(""); return; }
                saveSections();
            }
            else
            {
                if(updateSections())
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }

        private void loadSection(string sqlcmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select * from Sections  Order by SectionID ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlcmd, dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";               
                divInfo = " <table id='tblSectionList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Section Name</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";
                    divSectionList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["SectionID"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editSection(" + id + ");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                //divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divSectionList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private Boolean saveSections()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("saveSections",DbConnection.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SectionName", txtSectionName.Text.Trim());

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    loadSection("");
                    lblMessage.InnerText = "success->Save Successfully ";
                }
                else lblMessage.InnerText = "error->Unable to save";

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private Boolean updateSections()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update Sections  Set SectionName=@SectionName where SectionID=@SectionID ", DbConnection.Connection);

                cmd.Parameters.AddWithValue("@SectionID", lblSectionID.Value.ToString());
                cmd.Parameters.AddWithValue("@SectionName", txtSectionName.Text.Trim());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    loadSection("");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
    }
}