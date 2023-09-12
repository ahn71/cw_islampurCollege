using DS.BLL.Attendace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Attendance.Attendance_Slider
{
    public  partial class AttendanceStatusBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // AttendanceStatusLoad();
        }

       
        [System.Web.Services.WebMethod]
        public static  string AttendanceStatusLoad()
        {
             string divInnerText = "";
             string divAbsentText = "";
             string divLateText = "";
             string imgurl = "";
             string defaultimg="";
            DataTable dt = new DataTable();
            DataTable dtSchoolInfo = new DataTable();
            dtSchoolInfo = ForAttendanceReport.returnSchoolInfo();
            string SchoolName = dtSchoolInfo.Rows[0]["SchoolName"].ToString();
            string LogoUrl = "/Images/Logo/" + dtSchoolInfo.Rows[0]["LogoName"].ToString();
            dt = ForAttendanceReport.returnTodaysAttendanceStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    defaultimg = (dt.Rows[i]["EGender"].ToString().Trim().Equals("Female")) ? "pp1.png" : "pp2.png";
                    imgurl = (dt.Rows[i]["EPictureName"].ToString().Equals(dt.Rows[i]["EID"].ToString())) ? "images/" + defaultimg : "/Images/teacherProfileImage/" + dt.Rows[i]["EPictureName"].ToString();
                    if (dt.Rows[i]["AttStatus"].ToString().Equals("a"))
                        divAbsentText += "<div class='col-md-2'>" +
                                                   "<div class='ads-status-box'>" +
                                                       "<p><img src='" + imgurl + "' alt=''></p>" +
                                                       "<div class='ads-txt'>" +
                                                           "<h4><strong>Name : </strong> " + dt.Rows[i]["EName"].ToString() + "</h4>" +
                                                           "<h4><strong>Designation : </strong> " + dt.Rows[i]["DesName"].ToString() + "</h4>" +
                                                       "</div>" +
                                                   "</div>" +
                                               "</div>";
                    else
                        divLateText +="<div class='col-md-2'>"+
	                                    		"<div class='ads-status-box'>"+
                                                    "<p><img src='" + imgurl + "' alt=''></p>" +
		                                    		"<div class='ads-txt'>"+
                                                        "<h4><strong>Name : </strong> " + dt.Rows[i]["EName"].ToString() + "</h4>" +
                                                        "<h4><strong>Designation : </strong> " + dt.Rows[i]["DesName"].ToString() + "</h4>" +
                                                        "<span><strong>Late Time :</strong> " + dt.Rows[i]["LateTime"].ToString() + " Minute</span>" +
		                                    		"</div>"+
	                                    		"</div>"+
	                                    	"</div>";
                }
                   
                //divAbsent.Controls.Add(new LiteralControl(divInnerText));
               
            }
            divInnerText = "<div class='item active'>"+
	                                "<div class='slider-content'>"+
	                                "<div class='ads-header'>"+
	                                		"<div class='container'>"+
	                                			"<div class='col-md-2 ads-logo'>"+
                                                    "<p><img src='" + LogoUrl + "' alt=''></p>" +
	                                			"</div>"+
	                                			"<div class='col-md-10 ads-name'>"+
                                                    "<h1>" + SchoolName + "</h1>" +
	                                			"</div>"+
	                                		"</div>"+
	                                	"</div>"+
	                                	"<div class='ads-status-title'>"+
	                                		"<h2>Today Absent</h2>"+
	                                	"</div>"+	
	                                    "<div  class='ads-main'>"+divAbsentText+
                                            
                                              "</div>"+

	                                "</div>"+
	                            "</div>"+
	                            "<div class='item'>"+
	                                "<div class='slider-content'>"+
	                                	"<div class='ads-header'>"+
	                                		"<div class='container'>"+
	                                			"<div class='col-md-2 ads-logo'>"+
                                                    "<p><img src='" + LogoUrl + "' alt=''></p>" +
	                                			"</div>"+
	                                			"<div class='col-md-10 ads-name'>"+
	                                				"<h1>"+SchoolName+"</h1>"+
	                                			"</div>"+
	                                		"</div>"+
	                                	"</div>"+
	                                	"<div class='ads-status-title'>"+
	                                		"<h2>Today Late</h2>"+
	                                	"</div>"+	
	                                    "<div class='ads-main'>"+
	                                    	divLateText +                        	
	                                    "</div>"+
                                    "</div>" +
	                            "</div>";
             
            return divInnerText;
        }

    }
}