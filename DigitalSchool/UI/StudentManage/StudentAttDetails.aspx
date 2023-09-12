<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentAttDetails.aspx.cs" Inherits="DS.UI.Reports.Attendance.StudentAttDetails1" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
        }
        .controlLength{
            width: 92px;
        }
        
        .tbl-controlPanel td:first-child td:{
            text-align: right;
            padding-right: 5px;           
        }        
        .btn {
            margin: 3px;
        }
         input[type="radio"]{
            margin: 5px;
        }
          .height {
             height:40px;
             text-align:center;
         }
         fieldset.scheduler-border {
             border: 1px groove #ddd !important;
             padding: 0 1.4em 1.4em 1.4em !important;
             margin: 0 0 1.5em 0 !important;
             -webkit-box-shadow: 0px 0px 0px 0px #000;
             box-shadow: 0px 0px 0px 0px #000;
             height:192px;
         }

         legend.scheduler-border {
             width: inherit; /* Or auto */
             padding: 0 10px; /* To give a bit of padding on the left and right */
             border-bottom: none;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/UI/StudentManage/StudentManage.aspx">
                        <i class="fa fa-dashboard"></i>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </a>
                </li>
                <li class="active">Attendance</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">  
        <div class="tgPanelHead">Daily & Monthly Attendance Report</div>
       <div class="row">
            
                    <div class="col-lg-6">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">        
        <ContentTemplate>
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border"><b>Daily Attendance</b></legend>
            <%--<div class="tgPanel" style="height: 145px">--%>
                <div style="text-align: center; width: 600px; margin: 0px auto; margin-top: 10px">
                    <asp:RadioButtonList ID="rblReportType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Attendance Status</asp:ListItem>                       
                        <asp:ListItem style="margin-left: 10px">Log in-out Time</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <table class="tbl-controlPanel" style="width: auto" runat="server">
                    <tr>

                        <td>&nbsp;From Date&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender Format="dd-MM-yyyy" ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                        </td>
                         <td>&nbsp;To Date&nbsp;</td>
                         <td>
                            <asp:TextBox ID="txtToDate"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender4" runat="server" TargetControlID="txtToDate">
                            </asp:CalendarExtender>
                        </td>
                        <td>&nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_D_Click" />
                        </td>
                    </tr>
                </table>

            <%--</div>--%>
                            </fieldset>
              </ContentTemplate>
   </asp:UpdatePanel>
                        </div>

            <div class="col-lg-6">               
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
        <ContentTemplate>   
            <fieldset class="scheduler-border">
                            <legend class="scheduler-border"><b>Month Wise Attendance</b></legend>                     
    <%--<div class="tgPanel" style="height: 145px" >--%> 
        <div style="margin:0px auto; width:400px; margin-top:10px" ">
            <asp:RadioButtonList ID="rblRepotMontly" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblRepotMontly_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0" style="margin-left: -20px">Attendance Sheet</asp:ListItem>
                <asp:ListItem Value="1" style="margin-left: 10px">Attendance Summary</asp:ListItem>
                <asp:ListItem Value="2" style="margin-left: 10px">Absent List</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <table id="Table1" style="margin: 10px auto; font-family: Calibri; font-size: 15px; padding: 5px" runat="server">
            <tr>

                <td runat="server" id="tdMonth">&nbsp;Month &nbsp;</td>
                <td>
                    <asp:DropDownList Visible="false" ID="dlSheetName" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    <asp:TextBox ID="txtMonthName" Width="126px" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                    <asp:CalendarExtender Format="MMMM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtMonthName">
                    </asp:CalendarExtender>
                </td>
                <td>&nbsp;<asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px"
                    CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                    <asp:Button ID="btnSearch" Visible="false" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin"
                        runat="server" />
                </td>
            </tr>
        </table>
        <table style="margin: 10px auto; font-family: Calibri; font-size: 15px; padding: 5px" visible="false" runat="server" id="tblDateRange">
            <tr>
                <td>From Date</td>
                <td>
                    <asp:DropDownList Visible="false" ID="DropDownList1" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    <asp:DropDownList Visible="false" ID="DropDownList2" runat="server" ClientIDMode="Static" CssClass="input controlLength" Width="150px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFdate" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                    <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtFdate">
                    </asp:CalendarExtender>
                </td>
                <td>To Date</td>
                <td>
                    <asp:TextBox ID="txtTdate" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                    <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender3" runat="server" TargetControlID="txtTdate">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
    <%--</div>--%>
               </ContentTemplate>
   </asp:UpdatePanel>  
      
                </div> 
           </div>
       </div>
     <br />
    <div class="tgPanel height">        
        <div style="margin-top:8px;">
        <asp:Label ID="lblFineAmount"  Font-Bold="true" Font-Size="20px" ForeColor="#ff0066" runat="server"></asp:Label>
            </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });       
    </script>
</asp:Content>
