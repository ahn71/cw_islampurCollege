<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="AdviserWiseStdAttDetails.aspx.cs" Inherits="DS.UI.Adviser.AdviserWiseStdAttDetails" %>
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
            width: 350px;
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
                    <a id="A1" runat="server" href="~/UI/Adviser/AdviserHome.aspx">
                        <i class="fa fa-dashboard"></i>
                        DashBoard
                    </a>
                </li>
                <li class="active">Student Attendance</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <div class="tgPanel">  
        <div class="tgPanelHead">Daily & Monthly Attendance Report</div>
       <div class="row">
            
                    <div class="col-lg-6">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">        
        <ContentTemplate>
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border"><b>Daily Attendance</b></legend>
            <div class="tgPanel" style="height: 145px">
                <div style="text-align: center; width: 600px; margin: 0px auto; margin-top: 10px">
                    <asp:RadioButtonList ID="rblReportType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Attendance Status</asp:ListItem>                       
                        <asp:ListItem style="margin-left: 10px">Log in-out Time</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <table id="Table1" class="tbl-controlPanel" style="width: auto" runat="server">
                    <tr>

                        <td>&nbsp;From Date&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtDate" Width="174" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender Format="dd-MM-yyyy" ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                        </td>
                         <td>&nbsp;To Date&nbsp;</td>
                         <td>
                            <asp:TextBox ID="txtToDate" Width="174" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender4" runat="server" TargetControlID="txtToDate">
                            </asp:CalendarExtender>
                        </td>
                        <td>&nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_D_Click" />
                        </td>
                    </tr>
                </table>

            </div>
                            </fieldset>
              </ContentTemplate>
   </asp:UpdatePanel>
                        </div>

            <div class="col-lg-6">               
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
        <ContentTemplate>   
            <fieldset class="scheduler-border">
                            <legend class="scheduler-border"><b>Month Wise Attendance</b></legend>                     
    <div class="tgPanel" style="height: 145px" > 
        <div style="margin:0px auto; width:400px; margin-top:10px" ">
            <asp:RadioButtonList ID="rblRepotMontly" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblRepotMontly_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0" style="margin-left: -20px">Attendance Sheet</asp:ListItem>
                <asp:ListItem Value="1" style="margin-left: 10px">Attendance Summary</asp:ListItem>
                <asp:ListItem Value="2" style="margin-left: 10px">Absent List</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <table id="Table2" style="margin: 10px auto; font-family: Calibri; font-size: 15px; padding: 5px" runat="server">
            <tr>

                <td runat="server" id="tdMonth">&nbsp;Month &nbsp;</td>
                <td>
                    <asp:DropDownList Visible="false" ID="dlSheetName" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    <asp:TextBox ID="txtMonthName" Width="160px" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
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
                    <asp:TextBox ID="txtFdate" Width="174" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                    <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtFdate">
                    </asp:CalendarExtender>
                </td>
                <td>To Date</td>
                <td>
                    <asp:TextBox ID="txtTdate" Width="174" runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"></asp:TextBox>
                    <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender3" runat="server" TargetControlID="txtTdate">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
    </div>
               </ContentTemplate>
   </asp:UpdatePanel>  
      
                </div> 
           </div>
       </div>
     <br />
    <div class="tgPanel height">
        <asp:Label ID="lblFineAmount"   Font-Bold="true" Font-Size="20px" ForeColor="#ff0066" runat="server"></asp:Label>

    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
