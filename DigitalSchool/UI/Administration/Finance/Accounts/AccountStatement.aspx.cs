using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.Accounts
{
    public partial class AccountStatement : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            
        }


        private void GenerateIncomeReport() 
        {
            try { 

            string[] FDate = txtFromDate.Text.Split('-');
            string[] TDate = txtToDate.Text.Split('-');
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("DECLARE @cols  AS NVARCHAR(MAX)='';"+
                "DECLARE @query AS NVARCHAR(MAX)='';SELECT @cols = @cols + QUOTENAME(Title) + ','"
            +"FROM (select distinct Title from v_Accounts_Income where Date>='"+FDate[2]+"-"+FDate[1]+"-"+FDate[0]+"' and "
            + "Date<='" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "') as tmp select @cols = substring(@cols, 0, len(@cols)) set "
            + "@query = 'SELECT * from (select format(Date,''dd-MM-yyyy'') as Date, Amount, Title from v_Accounts_Income "
            + "Where Date>=''" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "'' and Date<=''" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "'') src pivot (SUM(Amount) "
            +"for Title in (' + @cols + ')) piv' execute(@query)", DbConnection.Connection);
           
            da.Fill(dt);
            if (dt.Rows.Count == 0) 
            {
                lblMessage.InnerText = "warning-> Icome records are not founed."; return;
            }
            DataRow dr = dt.NewRow();
            dr[dt.Columns[0].ColumnName] = "Total";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                dr[dt.Columns[i].ColumnName] = dt.AsEnumerable().Sum(x => x.IsNull(dt.Columns[i].ColumnName.ToString()) ? 0 : Convert.ToInt32(x[dt.Columns[i].ColumnName.ToString()]));
            }
            dt.Rows.Add(dr);
            Session["__ReportData__"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Administration/Finance/Accounts/ReportViewer.aspx?For=Income|" + txtFromDate.Text + " to " + txtToDate.Text + "');", true);
            }
            catch {
                lblMessage.InnerText = "warning-> Icome records are not founed.";

            }
        }

        private void GenerateExpenseReport()
        {
            try
            {

                string[] FDate = txtFromDate.Text.Split('-');
                string[] TDate = txtToDate.Text.Split('-');
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("DECLARE @cols  AS NVARCHAR(MAX)='';" +
                    "DECLARE @query AS NVARCHAR(MAX)='';SELECT @cols = @cols + QUOTENAME(Title) + ','"
                + "FROM (select distinct Title from v_Accounts_Expenses where Date>='" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "' and "
                + "Date<='" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "') as tmp select @cols = substring(@cols, 0, len(@cols)) set "
                + "@query = 'SELECT * from (select format(Date,''dd-MM-yyyy'') as Date, Amount, Title from v_Accounts_Expenses "
                + "Where Date>=''" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "'' and Date<=''" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "'') src pivot (SUM(Amount) "
                + "for Title in (' + @cols + ')) piv' execute(@query)", DbConnection.Connection);

                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Expense records are not founed."; return;
                }
             
                DataRow dr = dt.NewRow();
                dr[dt.Columns[0].ColumnName] = "Total";        
                for (int i = 1; i < dt.Columns.Count; i++) 
                {
                   dr[dt.Columns[i].ColumnName] = dt.AsEnumerable().Sum(x => x.IsNull(dt.Columns[i].ColumnName.ToString()) ? 0 : Convert.ToInt32(x[dt.Columns[i].ColumnName.ToString()]));
                }     
                dt.Rows.Add(dr);
                Session["__ReportData__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Administration/Finance/Accounts/ReportViewer.aspx?For=Expense|" + txtFromDate.Text + " to " + txtToDate.Text + "');", true);
            }
            catch
            {
                lblMessage.InnerText = "warning-> Expense records are not founed.";

            }
        }
        private void GenerateIncomeSummaryReport()
        {
            try
            {

                string[] FDate = txtFromDate.Text.Split('-');
                string[] TDate = txtToDate.Text.Split('-');
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("DECLARE @cols  AS NVARCHAR(MAX)='';" +
                    "DECLARE @query AS NVARCHAR(MAX)='';SELECT @cols = @cols + QUOTENAME(Title) + ','"
                + "FROM (select distinct Title from v_Accounts_Income where Date>='" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "' and "
                + "Date<='" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "') as tmp select @cols = substring(@cols, 0, len(@cols)) set "
                + "@query = 'SELECT * from (select format(Date,''MM-yyyy'') as Month, Amount, Title from v_Accounts_Income "
                + "Where Date>=''" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "'' and Date<=''" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "'') src pivot (SUM(Amount) "
                + "for Title in (' + @cols + ')) piv' execute(@query)", DbConnection.Connection);

                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Icome records are not founed."; return;
                }
                DataRow dr = dt.NewRow();
                dr[dt.Columns[0].ColumnName] = "Total";
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dr[dt.Columns[i].ColumnName] = dt.AsEnumerable().Sum(x => x.IsNull(dt.Columns[i].ColumnName.ToString()) ? 0 : Convert.ToInt32(x[dt.Columns[i].ColumnName.ToString()]));
                }
                dt.Rows.Add(dr);
                Session["__ReportData__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Administration/Finance/Accounts/ReportViewer.aspx?For=Income Summary|" + txtFromDate.Text + " to " + txtToDate.Text + "');", true);
            }
            catch
            {
                lblMessage.InnerText = "warning-> Icome records are not founed.";

            }
        }
        private void GenerateExpenseSummaryReport()
        {
            try
            {

                string[] FDate = txtFromDate.Text.Split('-');
                string[] TDate = txtToDate.Text.Split('-');
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("DECLARE @cols  AS NVARCHAR(MAX)='';" +
                    "DECLARE @query AS NVARCHAR(MAX)='';SELECT @cols = @cols + QUOTENAME(Title) + ','"
                + "FROM (select distinct Title from v_Accounts_Expenses where Date>='" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "' and "
                + "Date<='" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "') as tmp select @cols = substring(@cols, 0, len(@cols)) set "
                + "@query = 'SELECT * from (select format(Date,''MM-yyyy'') as Month, Amount, Title from v_Accounts_Expenses "
                + "Where Date>=''" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "'' and Date<=''" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "'') src pivot (SUM(Amount) "
                + "for Title in (' + @cols + ')) piv' execute(@query)", DbConnection.Connection);

                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Expense records are not founed."; return;
                }

                DataRow dr = dt.NewRow();
                dr[dt.Columns[0].ColumnName] = "Total";
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dr[dt.Columns[i].ColumnName] = dt.AsEnumerable().Sum(x => x.IsNull(dt.Columns[i].ColumnName.ToString()) ? 0 : Convert.ToInt32(x[dt.Columns[i].ColumnName.ToString()]));
                }
                dt.Rows.Add(dr);
                Session["__ReportData__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Administration/Finance/Accounts/ReportViewer.aspx?For=Expense Summary|" + txtFromDate.Text + " to " + txtToDate.Text + "');", true);
            }
            catch
            {
                lblMessage.InnerText = "warning-> Expense records are not founed.";

            }
        }
        private void GenerateProfitAndLossReport()
        {
            try
            {

                string[] FDate = txtFromDate.Text.Split('-');
                string[] TDate = txtToDate.Text.Split('-');
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("with "+
                    "Expenses as(select format(Date,'MM-yyyy') as Month, sum(Amount) Expenses from  v_Accounts_Expenses where Date>='" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "' and " +
                    "Date<='" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "' group by format(Date,'MM-yyyy'))," +
                    "Income as(select format(Date,'MM-yyyy') as Month, sum(Amount) Income from  v_Accounts_Income where Date>='" + FDate[2] + "-" + FDate[1] + "-" + FDate[0] + "' and " +
                    "Date<='" + TDate[2] + "-" + TDate[1] + "-" + TDate[0] + "' group by format(Date,'MM-yyyy')), " +
                    "Both as "+
                    "(select Month,Expenses,0 as Income from Expenses union  "+
                    "select Month,0 as Expenses,Income  from "+
                    "Income) "+
                    "select Month, sum(Income) Income,sum(Expenses) Expenses,sum(Income)-sum(Expenses) as [Profit/Loss] from both group by Month", DbConnection.Connection);

                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Income or Expense records are not founed."; return;
                }

                DataRow dr = dt.NewRow();
                dr[dt.Columns[0].ColumnName] = "Total";
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dr[dt.Columns[i].ColumnName] = dt.AsEnumerable().Sum(x => x.IsNull(dt.Columns[i].ColumnName.ToString()) ? 0 : Convert.ToInt32(x[dt.Columns[i].ColumnName.ToString()]));
                }
                dt.Rows.Add(dr);
                Session["__ReportData__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Administration/Finance/Accounts/ReportViewer.aspx?For=Profit/Loss Summary|" + txtFromDate.Text + " to " + txtToDate.Text + "');", true);
            }
            catch
            {
                lblMessage.InnerText = "warning-> Income or Expense records are not founed.";

            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtFromDate.Text.Trim().Length < 8) 
            {
                lblMessage.InnerText = "warning-> Please select from date !";
                txtFromDate.Focus();
                return;
            }
            if (txtToDate.Text.Trim().Length < 8)
            {
                lblMessage.InnerText = "warning-> Please select to date !";
                txtToDate.Focus();
                return;
            }
            if (rblReportType.SelectedValue == "Statement")
            {
                if (rblAccountStatement.SelectedValue == "1")
                    GenerateIncomeReport();
                else
                    GenerateExpenseReport();
            }
            else if (rblReportType.SelectedValue == "Summary")
            {
                if (rblAccountStatement.SelectedValue == "1")
                    GenerateIncomeSummaryReport();
                else
                    GenerateExpenseSummaryReport();
            }
            else 
            {
                GenerateProfitAndLossReport();
            }
           

        }

        protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReportType.SelectedValue == "ProfitLoss")
                divTitleType.Visible = false;
            else
                divTitleType.Visible = true;
        }
    }
}