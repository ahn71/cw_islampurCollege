<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AccountStatement.aspx.cs" Inherits="DS.UI.Administration.Finance.Accounts.AccountStatement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .rbl label {
            padding:5px;
            padding-top:3px;
            font-size:14px;
        }
        .tb {
        border:1px solid #808080;
        }
         @media (min-width: 320px) and (max-width: 480px) {
            .input{
                width:250px;
            }

        }
         @media (min-width: 481px) and (max-width: 700px) {
            .input{
                width:300px;
            }

        }
        .rbl1 label {
            margin:3px;
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
                
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/Accounts/AccountsHome.aspx">Accounts Management</a></li>                          
                <li class="active">Account Statement</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <div class="row">
         
         <div class="col-sm-8 col-sm-offset-2">
                <div class="tb">
                    <div class="tgPanelHead">Account Statement</div>
                      <asp:UpdatePanel runat="server" ID="upPrint">
        <ContentTemplate> 
             <br />
            <div class="row tbl-controlPanel">
                <center>
                    <asp:RadioButtonList ID="rblReportType" runat="server" CssClass="radiobuttonlist rbl1" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged" >
                    <asp:ListItem Value="Statement"  Selected="True">Statement Report</asp:ListItem>
                    <asp:ListItem Value="Summary" >Summary Report</asp:ListItem>
                        <asp:ListItem Value="ProfitLoss">Profit and Loss Report</asp:ListItem>
                    </asp:RadioButtonList>
               </center>
            </div>
                       
            <div runat="server" id="divTitleType" class="row tbl-controlPanel"> 
                <div class="col-sm-4 col-sm-offset-4">       
                    <asp:RadioButtonList ID="rblAccountStatement" RepeatLayout="Flow" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" class="radio-inline" Selected="True">Income</asp:ListItem>
                    <asp:ListItem Value="2" class="radio-inline">Expense</asp:ListItem>
                    </asp:RadioButtonList>
               </div>  
             </div>
            <div class="row tbl-controlPanel"> 
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="form-inline">
                         <div class="form-group">
                             <label for="exampleInputName2">From Date</label>
                                <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"></asp:CalendarExtender>
                         </div>
                        <div class="form-group">
                             <label for="exampleInputName2">To Date</label>
                                <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"></asp:CalendarExtender>
                         </div>
                        <div class="form-group">
                             <asp:Button runat="server" ID="btnPrint" Text="Print Preview" Width="120px"  CssClass="btn btn-success"  OnClick="btnPrint_Click" />
                
                         </div>
                        
                    </div>
                </div>
            </div> 
                 
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>                 
                </div>
            </div>
         </div>
      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
