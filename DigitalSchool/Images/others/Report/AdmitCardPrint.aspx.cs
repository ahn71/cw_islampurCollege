using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.IO;
using DS.BLL.Admission;

namespace DS.Report
{
    public partial class AdmitCardPrint : System.Web.UI.Page
    {
        DataTable dt;
        CurrentStdEntry currentstdEntry;
        string[] getECS = { "" };
        bool status = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                getECS = Request.QueryString["getcs"].ToString().Split('_');
                if (getECS.Length == 8)
                {
                    if (getECS[7] == "AC")
                    {
                        if (getECS[6] != " ") status = true;
                        createAdmitCard();
                    }
                    else
                    {
                        if (getECS[6] != " ") status = true;
                        createIdCard();
                    }
                }
                else
                {
                    if (getECS[6] == "AC")
                    {
                        if (getECS[5] != " ") status = true;
                        createAdmitCard();
                    }
                    else
                    {
                        if (getECS[5] != " ") status = true;
                        createIdCard();
                    }
                }
            }
            catch { }
        }
        private void createAdmitCard()
        {
            try
            {                            
                dt = new DataTable();
                DataTable schoolInfo=new DataTable();
                string condition = "";
               
                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                if (getECS.Length == 8)
                {
                    if (status == true)
                    {
                        if (getECS[6] != "All")
                        {
                            condition = " WHERE StudentId='" + getECS[6] + "'";
                        }
                        else
                        {
                            condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2] + "_" + getECS[3], getECS[4], getECS[5]);
                        }
                    }
                    else
                    {
                        condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2] + "_" + getECS[3], getECS[4], getECS[5]);
                    }
                }
                else
                {
                    if (status == true)
                    {
                        if (getECS[5] != "All")
                        {
                            condition = " WHERE StudentId='" + getECS[5] + "'";
                        }
                        else
                        {
                            condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2], getECS[3], getECS[4]);
                        }
                    }
                    else
                    {
                        condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2], getECS[3], getECS[4]);
                    }

                }

                dt = currentstdEntry.GetAdmitCard(condition);  
                sqlDB.fillDataTable("Select SchoolName,Address,LogoName From School_Setup", schoolInfo);
               
                string url = @"/Images/Logo/" + Path.GetFileName(schoolInfo.Rows[0]["LogoName"].ToString());
                string divInfo = "";

                divInfo = "<table border='0' cellspacing='10'>";
                int i = 0;

                while(i <dt.Rows.Count)
                {
                    divInfo += "<tr>";

                    divInfo += "<td><div class='content admin-card-box'><div class='content-box'><div class='Image-cont'></div><div class='left-box'><img src='/images/admitCard/ADMIT-CARD.png' /></div><div class='right-box'><div class='box'><img height='80px' src='"+ url + "' /><h1>" + schoolInfo.Rows[0]["SchoolName"].ToString().ToUpper() + "</h1><p>" + schoolInfo.Rows[0]["Address"].ToString().ToUpper() + "</p></div><div class='box'><h4><label class='labelh4'>" + getECS[0] + "</label></h4><h3>Student's Name: <label class='labelh3'>" + dt.Rows[i]["FullName"].ToString() + "</label></h3><div class='box'><div class='left-level'><h2>Class:<label class='labelh3'>" + dt.Rows[i]["ClassName"].ToString() + "</label></h3></div><div class='right-level'><h2>Roll No.<label class='labelh3'>" + dt.Rows[i]["RollNo"].ToString() + "</label></h2></div></div><h5>Section: <label class='labelh5'>" + dt.Rows[i]["SectionName"].ToString() + "</label></h5><div class='box-footer'><div class='sign-teacherAdmit'>.............................<br/>Sign of Class Teacher</div><div class='sign-headmasterAdmit'><img src='/Images/Sign.png' class='signatureAdmit' /><br/>Sign of Headmaster</div></div></div></div></div></div></td>";
                    i++;
                    if (i<dt.Rows.Count)
                        divInfo += "<td><div class='content admin-card-box'><div class='content-box'><div class='Image-cont'></div><div class='left-box'><img src='/images/admitCard/ADMIT-CARD.png' /></div><div class='right-box'><div class='box'><img height='80px' src='" + url + "' /><h1>" + schoolInfo.Rows[0]["SchoolName"].ToString().ToUpper() + "</h1><p>" + schoolInfo.Rows[0]["Address"].ToString().ToUpper() + "</p></div><div class='box'><h4><label class='labelh4'>" + getECS[0] + "</label></h4><h3>Student's Name: <label class='labelh3'>" + dt.Rows[i]["FullName"].ToString() + "</label></h3><div class='box'><div class='left-level'><h2>Class:<label class='labelh3'>" + dt.Rows[i]["ClassName"].ToString() + "</label></h3></div><div class='right-level'><h2>Roll No.<label class='labelh3'>" + dt.Rows[i]["RollNo"].ToString() + "</label></h2></div></div><h5>Section: <label class='labelh5'>" + dt.Rows[i]["SectionName"].ToString() + "</label></h5><div class='box-footer'><div class='sign-teacherAdmit'>.............................<br/>Sign of Class Teacher</div><div class='sign-headmasterAdmit'> <img src='/Images/Sign.png'  class='signatureAdmit' /> <br/>Sign of Headmaster</div></div></div></div></div></div></td>";

                    divInfo += "</tr>";
                    i++;
                }

                divInfo += "</table>";
                divAdmitPrint.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        private void createIdCard()
        {
            try
            {
              
               
                DataTable schoolInfo = new DataTable();
                sqlDB.fillDataTable("Select SchoolName,Address,LogoName From School_Setup", schoolInfo);
                dt = new DataTable();              
                string condition = "";

                if (currentstdEntry == null)
                {
                    currentstdEntry = new CurrentStdEntry();
                }
                if (getECS.Length == 8)
                {
                    if (status == true)
                    {
                        if (getECS[6] != "All")
                        {
                            condition = " WHERE StudentId='" + getECS[6] + "'";
                        }
                        else
                        {
                            condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2] + "_" + getECS[3], getECS[4], getECS[5]);
                        }
                    }
                    else
                    {
                        condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2] + "_" + getECS[3], getECS[4], getECS[5]);
                    }
                }
                else
                {
                    if (status == true)
                    {
                        if (getECS[5] != "All")
                        {
                            condition = " WHERE StudentId='" + getECS[5] + "'";
                        }
                        else
                        {
                            condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2], getECS[3], getECS[4]);
                        }
                    }
                    else
                    {
                        condition = currentstdEntry.GetSearchCondition(getECS[1], getECS[2], getECS[3], getECS[4]);
                    }

                }

                dt = currentstdEntry.GetAdmitCard(condition); 

                string divInfo = "";
                divInfo = "<table border='0' cellspacing='10'>";
                int i=0;
                while(i < dt.Rows.Count)
                {
                    divInfo += "<tr>";

                    divInfo += "<td><div class='content'><div class='content-box'><div class='Image-cont'><img src='/images/admitCard/bg.png' /></div><div class='left-box'><img src='/images/admitCard/idcard.png' /></div><div class='right-box'><div class='box'><img src='/images/admitCard/logo.png' /><h1>" + schoolInfo.Rows[0]["SchoolName"].ToString().ToUpper() + "</h1><p>" + schoolInfo.Rows[0]["Address"].ToString().ToUpper() + "</p></div><div class='box'><h3>Student's Name: <label class='labelh3'>" + dt.Rows[i]["FullName"].ToString() + "</label></h3><div class='box'><div class='left-level'><h2>Class:<label class='labelh3'>" + dt.Rows[i]["ClassName"].ToString() + "</label></h3></div><div class='right-level'><h2>Roll No.<label class='labelh3'>" + dt.Rows[i]["RollNo"].ToString() + "</label></h2></div></div><h5>Section: <label class='labelh5'>" + dt.Rows[i]["SectionName"].ToString() + "</label></h5><div class='box-footer'><div class='sign-teacher'>.............................<br/>Sign of Class Teacher</div><div class='sign-headmaster'><img src='/Images/Sign.png' class='signature' /><br/>Sign of Headmaster</div></div></div></div></div></div></td>";
                    i++;
                    if (i < dt.Rows.Count)
                        divInfo += "<td><div class='content'><div class='content-box'><div class='Image-cont'><img src='/images/admitCard/bg.png' /></div><div class='left-box'><img src='/images/admitCard/idcard.png' /></div><div class='right-box'><div class='box'><img src='/images/admitCard/logo.png' /><h1>" + schoolInfo.Rows[0]["SchoolName"].ToString().ToUpper() + "</h1><p>" + schoolInfo.Rows[0]["Address"].ToString().ToUpper() + "</p></div><div class='box'><h3>Student's Name: <label class='labelh3'>" + dt.Rows[i]["FullName"].ToString() + "</label></h3><div class='box'><div class='left-level'><h2>Class:<label class='labelh3'>" + dt.Rows[i]["ClassName"].ToString() + "</label></h3></div><div class='right-level'><h2>Roll No.<label class='labelh3'>" + dt.Rows[i]["RollNo"].ToString() + "</label></h2></div></div><h5>Section: <label class='labelh5'>" + dt.Rows[i]["SectionName"].ToString() + "</label></h5><div class='box-footer'><div class='sign-teacher'>.............................<br/>Sign of Class Teacher</div><div class='sign-headmaster'><img src='/Images/Sign.png' class='signature' /><br/>Sign of Headmaster</div></div></div></div></div></div></td>";

                    divInfo += "</tr>";
                    i++;
                }

                divInfo += "</table>";
                divAdmitPrint.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}