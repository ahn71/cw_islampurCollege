<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FineReports.aspx.cs" Inherits="DS.UI.Reports.Finance.FineReports" %>
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
            min-width:110px;
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
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a id="A2" runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>               
                <li class="active">Fine Reports</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>        
    </asp:UpdatePanel>
    <div id="ManiDivDailyAtt_L" style="border:1px black solid">
        <div class="tgPanelHead">Fine Reports</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>           
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >    
        <div class="row tbl-controlPanel"> 
            <div class="col-sm-10 col-sm-offset-1">
                <div class="form-inline">
                     <div class="form-group">
                         <label for="exampleInputName2">Shift</label>
                        <asp:DropDownList ID="ddlShift" runat="server" class="input controlLength form-control"></asp:DropDownList>
                     </div>
                    <div class="form-group">
                         <label for="exampleInputName2">Batch</label>
                            <asp:DropDownList ID="ddlBatch" runat="server" class="input controlLength form-control"
                                         OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
                     </div>
                    <div class="form-group" runat="server" id="divGroup" visible="false">
                         <label for="exampleInputName2">Group</label>
                            <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength form-control" 
                            OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged"  ClientIDMode="Static" Enabled="true"  AutoPostBack="True"></asp:DropDownList>
                     </div>
                    <div class="form-group">
                         <label for="exampleInputName2">Section</label>
                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true"
                             OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" class="input controlLength form-control"></asp:DropDownList>
                     </div>
                    <div class="form-group">
                         <label for="exampleInputName2">Roll</label>
                            <asp:DropDownList ID="ddlRoll" runat="server" class="input controlLength form-control"></asp:DropDownList>
                     </div>
                    <div class="form-group">
                         <asp:Button ID="btnPreview" Text="Fine List " ClientIDMode="Static" runat="server"
                           OnClientClick="return validateDropDown();"   CssClass="btn btn-success litleMargin" OnClick="btnPreview_Click"/>
                         <asp:Button ID="btnFineCollectionSummary" Text="Fine Collection Summary " ClientIDMode="Static" runat="server"
                           OnClientClick="return validateDropDown();"   CssClass="btn btn-success litleMargin" OnClick="btnFineCollectionSummary_Click" />
                
                     </div>
                    
                </div>
            </div>
        </div> 
          
        
        <br />       
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
