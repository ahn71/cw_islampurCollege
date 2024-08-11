using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string filePath = Server.MapPath("~/Report/") + Request.QueryString["FN"];
                this.Response.ContentType = "application/pdf";
                this.Response.AppendHeader("Content-Disposition;", "attachment;filename=" + Request.QueryString["FN"]);
                this.Response.WriteFile(filePath);
                this.Response.End();
            }         

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(string.Format("<script>window.open('{0}','_blank');</script>", "/Default.aspx")); //Open New Window
        }

        protected void View(object sender, EventArgs e)
        {
            try
            {
                string url = string.Format("/Test.aspx?FN={0}.pdf", (sender as LinkButton).CommandArgument);
                string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);
            }
            catch { }
        }


        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=print.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                divReport.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
            catch { }
        }
    }
}