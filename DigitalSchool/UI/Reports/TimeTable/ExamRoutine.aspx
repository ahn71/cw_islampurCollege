<%@ Page Title="Exam Routine" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ExamRoutine.aspx.cs" Inherits="DS.UI.Reports.TimeTable.ExamRoutine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         /**/
        .display td:nth-child(2), td:nth-child(3),td:nth-child(4)
        , td:nth-child(5), td:nth-child(6), td:nth-child(7){
            width:250px;
            
        }
        .display td:first-child {
            width:100px;
        }
        .display th
        {
            background-color:black;
            color:white;
        }           
         /**/
        .controlLength{
            width: 200px;
        }
        .tbl-controlPanel1{
            width: 670px;
            color:gray;
            font-size:large;
            font-family: 'Times New Roman';       
       margin: 10px auto;
       padding: 5px;
        }
         .table tr th:first-child, tr td:first-child, tr th:nth-child(1), tr td:nth-child(1),tr th:nth-child(2), tr td:nth-child(2), tr th:nth-child(3),tr td:nth-child(3), tr th:nth-child(4),tr td:nth-child(4),tr th:nth-child(5),tr td:nth-child(5), tr th:nth-child(6),tr td:nth-child(6), tr th:nth-child(7),tr td:nth-child(7),tr th:nth-child(8),tr td:nth-child(8) {
            text-align: center;        
        }
        .radioButtonLists label {
            margin-left:5px;
        }
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
                <li class="active">Exam Schedule</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div  class="routine">
<div style="text-align:center; border-bottom:1px solid #D2D2D2; padding:10px;">  
    <asp:UpdatePanel runat="server" ID="upPrint">
        <ContentTemplate> 
             <div style="margin-left:40%"> <asp:RadioButtonList ID="rblReportType" runat="server"  CssClass="radioButtonLists" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged">
                 <asp:ListItem style="margin-left:10px" Value="0" Selected="True">Full</asp:ListItem>
                  <asp:ListItem style="margin-left:10px" Value="1">Partial</asp:ListItem>
                                           </asp:RadioButtonList></div>
            
            Exam
            <asp:DropDownList runat="server" ID="ddlExam" ClientIDMode="Static" Width="220px" CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"></asp:DropDownList>
            Batch
            <asp:DropDownList runat="server" ID="ddlBatch" ClientIDMode="Static" Width="220px" CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>                
             <asp:Button runat="server" ID="btnPrint" Text="Print Preview" Width="120px"  CssClass="btn btn-success" OnClick="btnPrint_Click"  />
            <div class="row">
                <div  class="col-lg-1"></div>
                <div  class="col-lg-10">
             <div  runat="server" id="divGv" class="col-md-12" style="margin-top:10px">             
                            <asp:GridView ID="gvExamSchedule"  CssClass="tbl-controlPanel1 display"   runat="server"  GridLines="Both" AutoGenerateColumns="true"  EditRowStyle-HorizontalAlign="Center" >                                
                                <HeaderStyle Height="45px" HorizontalAlign="Center" />                               
                                    </asp:GridView>
                      </div> 
                </div>
                <div  class="col-lg-1"></div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div><br />
<asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlExam" />
        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />   
        <asp:AsyncPostBackTrigger ControlID="rblReportType" />     
    </Triggers>
    <ContentTemplate>
        <div id="divRoutineInfo" style="width:100%" runat="server" >
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
