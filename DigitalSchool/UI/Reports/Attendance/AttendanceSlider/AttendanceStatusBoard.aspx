<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceStatusBoard.aspx.cs" Inherits="DS.UI.Reports.Attendance.Attendance_Slider.AttendanceStatusBoard" %>

<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">

	<title>Today's Attendance Status</title>
	<link rel="stylesheet" href="css/bootstrap.css">
	<link rel="stylesheet" href="css/font-awesome.min.css">
	<link rel="stylesheet" href="css/style.css">
</head> 
<body>
	<!-- Slider start -->
	<div class="slider">
	    <div class="slider-wrap">
	        
	            <div class="row">
	                <div class="col-md-12">
	                    <div id="mogo-slider" class="carousel slide" data-ride="carousel">
	                        <!-- Wrapper for slides -->
	                        <div id="divMainContainer" class="carousel-inner text-center" role="listbox">
	                           
	                        </div>
	                        <!-- Indicators -->
	                        <ol class="carousel-indicators">
	                            <li data-target="#mogo-slider" data-slide-to="0" class="active">
	                                <div class="indicator-inner">
	                                    <span class="number"></span>
	                                    <span class="text text-uppercase">Today Absent</span>
	                                </div>
	                            </li>
	                            <li data-target="#mogo-slider" data-slide-to="1">
	                                <div class="indicator-inner">
	                                    <span class="number"></span>
	                                    <span class="text text-uppercase">Today Late</span>
	                                </div>
	                            </li>
	                            
	                        </ol>
	                    </div>
	                </div>
	            </div>
	    </div>
	</div>
	<!-- End of slider -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
   <script type="text/javascript">
      $(document).ready(function ()
      {
          AttendanceStatusLoad();
      });
      jQuery('#mogo-slider').carousel({
          interval: 10000
      })
      function AttendanceStatusLoad() {

          // var senderId = $('#dlSenderId option:selected').val();
          var serverURL = window.location.protocol + "//" + window.location.host + "/";

          $.ajax({
              type: "POST",

              contentType: "application/json; charset=utf-8",
              data: "{ ReceiverId:'" + 10 + "'}",
              url: serverURL + "/UI/Reports/Attendance/AttendanceSlider/AttendanceStatusBoard.aspx/AttendanceStatusLoad",
              dataType: "json",
              success: function (data) {
                 
                  $('#divMainContainer').empty();
                 $("#divMainContainer").append(data.d);
                 
                  
              }
          });

         
          setTimeout("AttendanceStatusLoad()", 100000);
      }
  </script>
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/bootstrap.js"></script>
	<script src="js/custom.js"></script>
</body>
</html>
