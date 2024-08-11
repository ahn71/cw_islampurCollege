<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="TeacherwiseRoutine.aspx.cs" Inherits="DS.UI.Reports.TimeTable.TeacherwiseRoutine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
.tg  {border-collapse:collapse;border-spacing:0;}
.tg td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;}
.tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;}
.tg .tg-baqh{text-align:center;vertical-align:top}
.tg .tg-yw4l{vertical-align:top}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
<div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                <li><a runat="server" href="~/UI/Reports/TimeTable/ScheduleHome.aspx">Schedule</a></li>
                <li><a runat="server" href="~/UI/Reports/TimeTable/ClassScheduleHome.aspx">Class Schedule</a></li>
               
                <li class="active">Teacher Wise</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

<table class="tg">
  <tr>
    <th class="tg-yw4l" rowspan="2">Teacher's Name<br></th>
    <th class="tg-yw4l"></th>
    <th class="tg-yw4l" colspan="2">1st</th>
    <th class="tg-yw4l" colspan="2">2nd</th>
    <th class="tg-yw4l" colspan="2">3rd</th>
    <th class="tg-yw4l" colspan="2">4th<br></th>
    <th class="tg-yw4l" rowspan="2"></th>
    <th class="tg-yw4l" colspan="2">5th</th>
    <th class="tg-yw4l" colspan="2">6th</th>
    <th class="tg-yw4l" colspan="2">7th</th>
    <th class="tg-yw4l" colspan="2">8th</th>
  </tr>
  <tr>
    <td class="tg-yw4l">Day</td>
    <td class="tg-yw4l" colspan="2">11:15-11:55</td>
    <td class="tg-yw4l" colspan="2">11:55-12:25<br></td>
    <td class="tg-yw4l" colspan="2">12:25-12:55</td>
    <td class="tg-yw4l" colspan="2">12:55-01:20</td>
    <td class="tg-yw4l" colspan="2">02:00-2:30</td>
    <td class="tg-yw4l" colspan="2">02:30-3:00</td>
    <td class="tg-yw4l" colspan="2">03:00-3:30</td>
    <td class="tg-yw4l" colspan="2">3:30-4:00</td>
  </tr>
  <tr>
    <td class="tg-yw4l" rowspan="6">(V.P.)</td>
    <td class="tg-yw4l">Sat<br></td>
    <td class="tg-yw4l" rowspan="6"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l" rowspan="6"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-baqh" rowspan="6">VII</td>
    <td class="tg-yw4l">Arabic 2nd<br></td>
    <td class="tg-yw4l" rowspan="6"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l" rowspan="6"></td>
    <td class="tg-yw4l" rowspan="6"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l" rowspan="6">IX</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l" rowspan="6">VI</td>
    <td class="tg-yw4l">Quran</td>
    <td class="tg-yw4l" rowspan="6"></td>
    <td class="tg-yw4l"></td>
  </tr>
  <tr>
    <td class="tg-yw4l">Sun</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Arabic 2nd</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Quran</td>
    <td class="tg-yw4l"></td>
  </tr>
  <tr>
    <td class="tg-yw4l">Mon</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Arabic 2nd</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Quran</td>
    <td class="tg-yw4l"></td>
  </tr>
  <tr>
    <td class="tg-yw4l">Tue</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Arabic 2nd</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Hadith</td>
    <td class="tg-yw4l">Quran</td>
    <td class="tg-yw4l"></td>
  </tr>
  <tr>
    <td class="tg-yw4l">Wed</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Arabic 2nd</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Hadith</td>
    <td class="tg-yw4l">Quran</td>
    <td class="tg-yw4l"></td>
  </tr>
  <tr>
    <td class="tg-yw4l">Thu</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Arabic 2nd</td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l"></td>
    <td class="tg-yw4l">Hadith</td>
    <td class="tg-yw4l">Quran</td>
    <td class="tg-yw4l"></td>
  </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
