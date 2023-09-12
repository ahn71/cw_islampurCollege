using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Administration.Settings.GeneralSettings
{
    public partial class AddBoard : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddBoard.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadBorads("");
                }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (lblBoardId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadBorads(""); return; }
                saveBoard();
            }
            else updateBoard();
        }


        private void updateBoard()
        {
            try
            {
                string[] getColumns = { "BoardName" };
                string[] getValues = { txtBoardName.Text };
                string getIdentifierValue = lblBoardId.Value.ToString();
                if (ComplexScriptingSystem.SQLOperation.forUpdateValue("Boards", getColumns, getValues, "BoardId", getIdentifierValue, DbConnection.Connection) == true)
                {
                    btnSave.Text = "Save";
                    txtBoardName.Text = "";
                    lblBoardId.Value = "";
                    loadBorads("");
                    lblMessage.InnerText = "success->>Update Successfully";
                }
            }
            catch { }
        }

        private void saveBoard()
        {
            try
            {
                string[] getColumns = { "BoardName" };
                string[] getValues = { txtBoardName.Text.Trim() };
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("Boards", getColumns, getValues, DbConnection.Connection) == true)
                {
                    loadBorads("");
                    clear();
                    lblMessage.InnerText = "success->Add Successfully";
                }
            }
            catch { }
        }

        private void loadBorads(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select *  from Boards  Order by BoardId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";
            divBoard.Controls.Clear();
           

            divInfo = " <table id='tblDesignationList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Board Name Type</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody>";
                divInfo += "</table>";
                divBoard.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["BoardId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td>" + dt.Rows[x]["BoardName"].ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editBoards(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divBoard.Controls.Add(new LiteralControl(divInfo));

        }

        private void clear()
        {
            txtBoardName.Text = "";
            txtBoardName.Focus();
        }
    }
}