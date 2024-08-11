using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace DS.Forms.sms
{
    public partial class SendSMS : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["__UserId__"] = "oitl";
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadBatch(dlBatch);
                    Classes.commonTask.loadSection(dlSection);
                    LoadTemplate(dlTitel);
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("Select StudentId,RollNo From CurrentStudentInfo where Shift='" + dlShift.SelectedItem.Text + "' and BatchName='" + dlBatch.SelectedItem.Text + "'and SectionName='" + dlSection.SelectedItem.Text + "'", dt = new DataTable());
                chklbRollNo.DataSource = dt;
                chklbRollNo.DataTextField = "RollNo";
                chklbRollNo.DataValueField = "StudentId";
                chklbRollNo.DataBind();
                foreach (ListItem item in chklbRollNo.Items)
                {                 
                        item.Selected = true;
                }
                if (dt.Rows.Count > 0)
                {
                    chkselect.Visible = true;
                    chkselect.Checked = true;
                    chkselect.Text = "UnSelect All";
                }
                else
                {
                    chkselect.Visible = false;
                    chkselect.Checked = false;
                }

            }
            catch { }
        }
        private void LoadTemplate(DropDownList dl)
        {
            try
            {
                sqlDB.fillDataTable("Select TId,Title From SMS_Template", dt = new DataTable());
                dl.DataSource = dt;
                dl.DataTextField = "Title";
                dl.DataValueField = "TId";
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch { }

        }

        protected void dlTitel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlDB.fillDataTable("Select Body From SMS_Template where TId=" + dlTitel.SelectedValue + "", dt = new DataTable());
                txtBody.Text = dt.Rows[0]["Body"].ToString();
            }
            catch { }
            
        }

        protected void chkselect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkselect.Checked)
            {
                chkselect.Text = "UnSelect All";
                foreach (ListItem item in chklbRollNo.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                chkselect.Text = "Select All";
                foreach (ListItem item in chklbRollNo.Items)
                {
                    item.Selected = false;
                }
            }
        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            sqlDB.fillDataTable("Select Distinct Email,EmailPassword From School_Setup", dt = new DataTable());
            for (int i = 0; i < chklbRollNo.Items.Count; i++)
            {
                if (chklbRollNo.Items[i].Selected == true)
                {
                    SendSMST(chklbRollNo.Items[i].Value, dt.Rows[0]["Email"].ToString(), adviitSecurity.crypto(dt.Rows[0]["EmailPassword"].ToString(), false));
                }
            }
        }
        private void SendSMST(string StudentId,string Mail,string pass)
        {
            try
            {
                DataTable dtSId = new DataTable();
                sqlDB.fillDataTable("Select FatherEmail From StudentProfile where StudentId=" + StudentId + "", dtSId);
                string StudentFatherEmail = dtSId.Rows[0]["FatherEmail"].ToString();
                if (Mail != "" && StudentFatherEmail != "" && pass != "")
                {
                    MailMessage m = new MailMessage();
                    SmtpClient sc = new SmtpClient();


                    try
                    {

                        m.From = new MailAddress(Mail, "Display name");

                        m.To.Add(new MailAddress(StudentFatherEmail, "Display name To"));
                        // m.CC.Add(new MailAddress("CC@yahoo.com", "Display name CC"));
                        //similarly BCC


                        m.Subject = dlTitel.SelectedItem.Text;
                        m.IsBodyHtml = true;
                        m.Body = txtBody.Text.Trim();




                        /* // Send With Attachements.
                        FileStream fs = new FileStream("E:\\TestFolder\\test.pdf",
                                           FileMode.Open, FileAccess.Read);
                        Attachment a = new Attachment(fs, "test.pdf",
                                           MediaTypeNames.Application.Octet);
                        m.Attachments.Add(a);
                         */




                        /* // Send html email wiht images in it.
                        string str = "<html><body><h1>Picture</h1><br/><img
                                         src=\"cid:image1\"></body></html>";
                        AlternateView av =
                                     AlternateView.CreateAlternateViewFromString(str,
                                     null,MediaTypeNames.Text.Html);
                        LinkedResource lr = new LinkedResource("E:\\Photos\\hello.jpg",
                                     MediaTypeNames.Image.Jpeg);
                        lr.ContentId = "image1";
                        av.LinkedResources.Add(lr);
                        m.AlternateViews.Add(av);
                         */


                        sc.Host = "smtp.gmail.com";
                        sc.Port = 587;
                        sc.Credentials = new
                        System.Net.NetworkCredential(Mail, pass);
                        sc.EnableSsl = true;
                        double getTime = Math.Round((double.Parse(dtSId.Rows.Count.ToString())) / 100, 0);
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(getTime));
                        sc.Send(m);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.InnerText = ex.Message;
                    }
                }
            }
            catch { }
        }
    }
}