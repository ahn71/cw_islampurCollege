<%@ Page Title="Class Routine For Teacher" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ClassRoutine For_Teacher.aspx.cs" Inherits="DS.UI.Reports.TimeTable.ClassRoutine_For_Teacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         .radiobuttonlist label {
            margin-left:5px;
        }
      
        .display {
            width:850px;           
            
        }
        /*.display th:first-child,td:first-child {
            text-align:center;
            width:50px;
        }*/     
          .display th:nth-child(7),td:nth-child(7) {
            width:100px;     
            text-align:center;      
            
        }
           /*.tbl-controlPanel tr th {
            background-color: #23282C;
            color: white;
            height:50px;
        }*/
.tg {
  border-collapse: collapse;
  border-spacing: 0;
  margin: 0 auto;
  width: 100%;
}
.tg td {
  border-style: solid;
  border-width: 1px;
  font-family: Arial,sans-serif;
  font-size: 11px;
  font-weight: bold;
  line-height: 13px;
  overflow: hidden;
  padding: 1px 3px;
  word-break: normal;
}
.tg th {
  border-style: solid;
  border-width: 1px;
  font-family: Arial,sans-serif;
  font-size: 14px;
  font-weight: bold;
  overflow: hidden;
  padding:1px 5px;
  word-break: normal;
}
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

    <div  class="routine">
<div style="text-align:center; border-bottom:1px solid #D2D2D2; padding:10px;">
  
    <asp:UpdatePanel runat="server" ID="upPrint">        
        <ContentTemplate>
             <div style="text-align:center; width:200px; margin:0px auto; margin-top:10px">
            <asp:RadioButtonList ID="rblReportType" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="0">Routine</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="1">Load Report</asp:ListItem>            
        </asp:RadioButtonList>                   
                     </div> 
             Shift
            <asp:DropDownList runat="server" ID="ddlShift" Width="240px" AutoPostBack="true" CssClass="input controlLength" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"></asp:DropDownList>
           <%--Department--%>
            <asp:DropDownList Visible="false" runat="server" ID="ddlDepartment" Width="240px" AutoPostBack="true" CssClass="input controlLength" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
            Teacher List
            <asp:DropDownList runat="server" ID="ddlTeacher" Width="240px" AutoPostBack="true" CssClass="input controlLength" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged"></asp:DropDownList> 
            &nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success " OnClick="btnPrint_D_Click" />            
            <%-- <asp:Button runat="server" OnClick="btnPrint_Click" ID="btnPrint" Text="Print Preview" Width="120px" style=" height: 30px; margin-top: -8px; width: 120px; padding: 0 !important;" CssClass="greenBtn" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</div><br />
<asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlTeacher" />
         <asp:AsyncPostBackTrigger ControlID="ddlDepartment" />
        <asp:AsyncPostBackTrigger ControlID="rblReportType" />
    </Triggers>
    <ContentTemplate>
        <div id="divRoutineInfo" style="width:100%" runat="server" >
            <table>
                <thead>

                </thead>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
    <div >
      </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_ddlDepartment").select2();
            $("#MainContent_ddlTeacher").select2();
        });
        function goToNewTab(url) {
            window.open(url);
            load();
        }
        function load() {
            $("#MainContent_ddlDepartment").select2();
            $("#MainContent_ddlTeacher").select2();
        }
    </script>
</asp:Content>
