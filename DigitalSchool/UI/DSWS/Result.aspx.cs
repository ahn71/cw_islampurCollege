using DS.BLL.Admission;
using DS.BLL.Examinition;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class Result : System.Web.UI.Page
    {
        DataTable dt;
        Class_ClasswiseMarksheet_TotalResultProcess_Entry resut;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {                
               // BatchEntry.GetDropdownlist(ddlBatch, "True");
                commonTask.loadYearFromBatch(ddlYear); 
                commonTask.loadClassFromBatch(ddlBatch); 
            }
            
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {     
                ExamInfoEntry.getExamByBatchNameWithExInSl(ddlExamId,ddlBatch.SelectedValue+ddlYear.SelectedValue);
            }
            catch { }
        }
        
        
        private void getResult()
        {
            try
            {
                if (resut == null)
                    resut = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();

                dt = new DataTable();
                dt = resut.LoadSemesterProgressReportIndividual(ddlYear.SelectedValue,ddlBatch.SelectedValue, ddlExamId.SelectedValue, txtRollNo.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    string session = ddlYear.SelectedValue + "-" +( int.Parse(ddlYear.SelectedValue) + 1).ToString();
                    string divinfo = "";
                    string divinfoPrint = "";
                    string tr = "";
                    string tr_p = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string[] Grade=new string[2];
                        if (!dt.Rows[i]["AB"].ToString().Equals(""))
                        {
                            Grade[0] = "0";
                            Grade[1] = "AB";
                        }
                        else if (!dt.Rows[i]["IsFailed"].ToString().Equals("0"))
                        {
                            Grade[0] = "0";
                            Grade[1] = "F";
                        }                            
                        else
                            Grade = commonTask.markToGPA(int.Parse(dt.Rows[i]["TotalMark"].ToString()), int.Parse(dt.Rows[i]["Marks"].ToString())).Split('_');
                        tr += "<tr>" +
                            "<td class='cent-align'>" + dt.Rows[i]["SubCode"].ToString() + "</td>" +
                            "<td><span class='code_101'>" + dt.Rows[i]["SubName"].ToString() + "</span></td>";
                        tr_p += "<tr>" + 
                            "<td>" + dt.Rows[i]["SubCode"].ToString() +
                            "</td><td><span class='code_101'>" + dt.Rows[i]["SubName"].ToString() + "</span></td>";
                        if (ckbWithMarks.Checked)
                        {
                            tr+= "<td><span class='code_101'>" + dt.Rows[i]["TotalCQ"].ToString() + "</span></td>"; 
                            tr_p+= "<td><span class='code_101'>" + dt.Rows[i]["TotalCQ"].ToString() + "</span></td>"; 
                            tr+= "<td><span class='code_101'>" + dt.Rows[i]["Practical"].ToString() + "</span></td>"; 
                            tr_p+= "<td><span class='code_101'>" + dt.Rows[i]["Practical"].ToString() + "</span></td>"; 
                            tr+= "<td><span class='code_101'>" + dt.Rows[i]["mcq"].ToString() + "</span></td>"; 
                            tr_p+= "<td><span class='code_101'>" + dt.Rows[i]["mcq"].ToString() + "</span></td>"; 
                            tr+= "<td><span class='code_101'>" + dt.Rows[i]["TotalMark"].ToString() + "</span></td>"; 
                            tr_p+= "<td><span class='code_101'>" + dt.Rows[i]["TotalMark"].ToString() + "</span></td>"; 
                        }
                        tr+="<td class='cent-align'>" + Grade[1] + "</td>" +
                            "</tr>";
                        tr_p+= "<td style ='text-align: center;'>" + Grade[1] + "</td> " +
                            "</tr>";
                    }

                    divinfo += "<div><div id='page-wrapper'>" +
                        "<div class='row'>" +
                        "<div class='col-md-12'>" +
                        "<div class='page-header text-center' id='page-header'>" +
                        "<h3>" + ddlExamId.SelectedItem.Text + " -" + ddlYear.SelectedValue + "</h3>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "<div class='row'>" +
                        "<div class='col-md-12'>" +
                        "<div id = 'result_display' style=''>" +
                        "<div class='table-responsive'>" +
                        "<table class='table table-striped table-bordered' width='100%'>" +
                        "<tbody>" +
                        "<tr>" +
                        "<td>Roll No</td>" +
                        "<td colspan='3'>" + dt.Rows[0]["RollNo"].ToString() + "</ td >" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td> Name of Student</td>" +
                                                "<td colspan='3'>" + dt.Rows[0]["FullName"].ToString() + "</td></tr>" +
                                                "<tr>" +
                                                "<td>Father's Name</td>" +
                                                "<td colspan='3'>" + dt.Rows[0]["FathersName"].ToString() + "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td>Mother's Name</td>" +
                                                "<td colspan='3'>" + dt.Rows[0]["MothersName"].ToString() + "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                //"<td>Board</td>" +
                                                //"<td>DHAKA</td>" +
                                                "<td>Session</td>" +
                                                "<td colspan='3'>" + session + "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td>Group</td>" +
                                                "<td> " + dt.Rows[0]["GroupName"].ToString() + " </td>" +
                                                "<td>Type</td>" +
                                                "<td>REGULAR</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td>Result</td>" +
                                                "<td>GPA="+ dt.Rows[0]["GPA"].ToString() + "</td>" +

                                                //"<td>Date of Birth</td>" +
                                                //"<td>" + ((dt.Rows[0]["DateOfBirth"].ToString().Equals("01-01-1900")) ? "" : dt.Rows[0]["DateOfBirth"].ToString()) + "</td>"
                                                "<td>Grade</td><td>"+dt.Rows[0]["Grade"].ToString()+"</td>"
                                                +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td>Name of Institute</td>" +
                                                "<td colspan='3'><span id='i_name'>ISLAMPUR COLLEGE</span></td>" +
                                                "</tr>" +
                                                "</tbody>" +
                                                "</table>" +
                                                "<div class='alert alert-info text-center' id='err_msg' style='display:none'></div>" +
                                                "<div class='text-center'>" +
                                                "<h4>Subject-Wise Grade/Marks</h4>" +
                                                "</div>" +
                                                "<table class='table table-striped table-bordered' width='100%'>" +
                                                "<thead class='btn-success'>" +
                                                "<tr>" +
                                                "<th>Subject Code</th>" +
                                                "<th>Subject Name</th>";
                                                if (ckbWithMarks.Checked)
                                                 {
                                                 divinfo += "<th>Theory</th><th>Practical</th><th>MCQ</th><th>Total</th>";
                                                 }
                                                divinfo += "<th>Grade</th>" +
                                                "</tr>" +
                                                "</thead>" +
                                                "<tbody>" + tr + "</tbody>" +
                                                "</table>" +
                                                "<div class='divpadding'></div>" +
                                                "</div>" +
                                                "</div>" +
                                                "</div>" +
                                                "</div>" +
                                                "</div>" +
                                                
                                                "<div class='row buttons' id='buttons_down' style=''>" +
                                                "<div class='text-center'>" +
                                                //"<div class='btn-group'><button type = 'button' class='btn btn-info search-button' id='search'>Search Again</button><button type = 'button' class='btn btn-success print-button' id='printbtn' onclick='window.print();'>Print</button></div>" +  
                                                "<div class='btn-group'>" +
                                                "<button type = 'button' class='btn btn-info search-button' id='search' onclick='searchAgain();'>Search Again</button>" +
                                                "<button type = 'button' class='btn btn-success print-button' id='printbtn' onclick='printDiv();'>Print</button></div>" +
                                                "</div>" +
                                                "</div>" +
                                                "</div>";
                    divinfoPrint += @"<div id='page-wrapper1' style='display :none; width: 50%;margin: 0 auto;background: #fff;padding: 15px;'>
                <div style = 'width: 100%;overflow: hidden;background: #ededed;padding: 5px 0;'> 
                     <span style='width: 20%;float: left;'><img style='width:120px'class='img-fluid' src='http://islampurcollege.edu.bd/websitedesign/assets/images/logo.png' alt=''></span>
                    <span style='width:80%;float:left;'><p style='font-size:40px;margin:0;line-height:10px;margin-top: 28px;'>Islampur College</p><br><p style='font-size: 18px;line-height: 1px;margin-left: 3px;'>Islampur, Jamalpur</p></span>
                </div>
                <div class='row'>
                    <h3 style = 'text-align: center;background: #ededed;padding: 7px;font-size: 20px;'>" + ddlExamId.SelectedItem.Text + " -" + ddlYear.SelectedValue + @"</h3>
                </div>
                <div class='row'>
                    <div class='table-responsive'>
                        <div class='row'>
                    <div class='table-responsive'>
                        <table style='width: 100%;' border='1' bordercolor='#efefef' cellspacing='0' cellpadding='5'>
                            <tbody>
                                <tr>
                                    <td><strong> Roll No</strong></td>
                                    <td colspan='3'>" + dt.Rows[0]["RollNo"].ToString() + @"</td>
                                </tr>
                                <tr>
                                    <td><strong>Name of Student</strong> </td>
                                    <td colspan='3'>" + dt.Rows[0]["FullName"].ToString() + @"</td>
                                </tr>
                                <tr>
                                    <td><strong>Father's Name </strong></td>
                                    <td colspan='3'>" + dt.Rows[0]["FathersName"].ToString() + @"</td>
                                </tr>
                                <tr>
                                    <td><strong>Mother's Name</strong></td>
                                    <td colspan='3'>" + dt.Rows[0]["MothersName"].ToString() + @"</td>
                                </tr>
                                <tr>
                                    
                                    <td><strong>Session</strong></td>
                                    <td colspan='3'>" + session + @"</td>
                                </tr>
                                <tr>
                                    <td><strong>Group</strong></td>
                                    <td>" + dt.Rows[0]["GroupName"].ToString() + @"</td>
                                    <td><strong>Type</strong></td>
                                    <td>REGULAR</td>
                                </tr>
                                <tr>
                                    <td><strong>Result</strong></td>
                                    <td>GPA= " + dt.Rows[0]["GPA"].ToString() + @"</td>
                                    <td><strong>Grade</strong></td>
                                    <td>" + dt.Rows[0]["Grade"].ToString() + @"</td>
                                </tr>                               
                            </tbody>
                        </table><div class='alert alert-info text-center' id='err_msg' style='display:none'></div>
                        <div class='text-center'>
                            <h4>Subject-Wise Grade/Marks</h4>
                        </div><table style='width: 100%;' border='1' cellspacing='0' cellpadding='5' >
                            <thead class='btn-success'>
                                <tr>
                                    <th style='text-align: left;background: #ededed;'>Subject Code</th>
                                    <th style='text-align: left;background: #ededed;'>Subject Name</th>";
                    if (ckbWithMarks.Checked)
                    {
                       
                        divinfoPrint += "<th style='text-align: left;background: #ededed;'>Theory</th><th style='text-align: left;background: #ededed;'>Practical</th><th style='text-align: left;background: #ededed;'>MCQ</th><th style='text-align: left;background: #ededed;'>Total</th>";
                    }
                    divinfoPrint +=@"<th style='text-align: center;background: #ededed;'>Grade</th>
                                </tr>
                            </thead>
                            <tbody>"+ tr_p + @"</tbody>
                        </table>
                        <div class='divpadding'></div>                    
                    </div>
                </div>
            </div>";
                    divinfo += divinfoPrint;
                    divResultView.Controls.Add(new LiteralControl(divinfo));
                }
                else
                {
                    lblMessage.InnerText = "warning-> Result not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }

        protected void btnResult_Click(object sender, EventArgs e)
        {
            getResult();
           
        }       
    }
}