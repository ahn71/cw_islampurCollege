<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="AdviserLeave.aspx.cs" Inherits="DS.UI.Adviser.AdviserLeave" %>
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
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .btn {
            margin: 3px;
        }
         .radiobuttonlist label {
            margin-left:5px;
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
                <li><a id="A2" runat="server" href="~/UI/Adviser/AttLeaveHome.aspx">Attendance And Leave </a></li>              
                <li class="active">Leave</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div id="ManiDivDailyAtt_L" style="border:1px black solid">
        <div class="tgPanelHead">Leave List Report</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>           
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >       
         <div ">
                  
        <table id="Table2" style="margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
             
            <tr>               
                <td>&nbsp;From Date&nbsp;</td>
             <td><asp:TextBox ID="txtFromDate_L" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate_L" ID="CalendarExtender2">
              </asp:CalendarExtender>
             </td>
                 <td>&nbsp;To Date&nbsp;</td>
             <td><asp:TextBox ID="txtToDate_L" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate_L" ID="CalendarExtender3">
              </asp:CalendarExtender>
             </td> 
                    
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint_L" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right"  OnClick="btnPrint_L_Click"/> </td>                
            </tr>
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>
    <div id="ManiDivDailyAtt_Ap" style="border:1px black solid">
        <div class="tgPanelHead">Approved and Rejected Leave List </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>            
             <asp:AsyncPostBackTrigger ControlID="btnPrint_Ap" />   
                                 
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" > 
        <div class="row"> 
            <div class="col-md-4"></div>
            <div class="col-md-6">
        <asp:RadioButtonList runat="server" ID="rblApprovedRejected" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem style="margin-left:10px" Selected="True" Value="2">All</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="1">Approved</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="0">Rejected</asp:ListItem>
        </asp:RadioButtonList>
            </div>
            <div class="col-md-2"></div>
            </div>    
        
      
        <br />
        <table id="Table4" style="margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
             
            <tr>               
                <td>&nbsp;From Date&nbsp;</td>
             <td><asp:TextBox ID="txtFromDate_Ap" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate_Ap" ID="CalendarExtender4">
              </asp:CalendarExtender>
             </td>
                 <td>&nbsp;To Date&nbsp;</td>
             <td><asp:TextBox ID="txtToDate_Ap" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate_Ap" ID="CalendarExtender5">
              </asp:CalendarExtender>
             </td>  
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint_Ap" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right"  OnClick="btnPrint_Ap_Click"/> </td>              
            </tr>
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>
    <div id="ManiDivDailyAtt" style="border:1px black solid">
        <div class="tgPanelHead">Leave Balance Report</div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <Triggers>            
             <asp:AsyncPostBackTrigger ControlID="btnPrint" />   
          
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >     
       
        <br />
        <table id="Table6" style="margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
             
            <tr>               
                <td>&nbsp;From Date&nbsp;</td>
             <td><asp:TextBox ID="txtFromDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate" ID="txtFromDate_Calendar">
              </asp:CalendarExtender>
             </td>
                 <td>&nbsp;To Date&nbsp;</td>
             <td><asp:TextBox ID="txtToDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate" ID="CalendarExtender1">
              </asp:CalendarExtender>
             </td>
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_Click"/> </td> 
              
             
            </tr>
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="tgPanelHead">Yearly Leave Status Report</div>
           <div class="tgPanel">        
           
                <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px">
                        <tr>                        
                             <td runat="server" id="tdMonth">&nbsp;Year&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" Width="130px" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>                          
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" ClientIDMode="Static" CssClass="btn btn-success pull-right" Width="120px" OnClick="btnPrintPreview_Click"/>                            
                            </td>
                        </tr>
                    </table>                      
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
         function InputValidation() {
             if (validateText('txtFromDate', 1, 20, 'Enter a admission date') == false) return false;
             if (validateText('txtToDate', 1, 20, 'Enter a admission number') == false) return false;
             // if (validateCombo('ddlClass', "0", 'Select a class') == false) return false;
             return true;
         }
    </script>
</asp:Content>
