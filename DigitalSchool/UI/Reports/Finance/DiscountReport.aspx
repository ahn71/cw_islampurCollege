<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DiscountReport.aspx.cs" Inherits="DS.UI.Reports.Finance.DiscountReport" %>
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
            min-width:120px;
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
                <li class="active">Discount Reports</li>               
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
        <div class="tgPanelHead">Discount List</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>           
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >   
        <div class="row tbl-controlPanel"> 
            <div class="col-sm-8 col-sm-offset-2">
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
                         <label for="exampleInputName2"></label>
                        <asp:Button ID="btnPreview" Text="Preview & Print " ClientIDMode="Static" runat="server"
                         OnClientClick="return validateDropDown();"   CssClass="btn btn-success litleMargin" OnClick="btnPreview_Click"/>
               
                     </div>
                </div>
            </div>
        </div> 
           
       
        <br />       
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>   
     <div id="Div1" style="border:1px black solid">
        <div class="tgPanelHead">Discount Summary</div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>           
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >  
        <div class="row tbl-controlPanel"> 
            <div class="col-sm-12">
                <div class="form-inline">
                    <div class="form-group">
                       <label for="exampleInputName2">Shift</label>
                        <asp:DropDownList ID="ddlShift_S" runat="server" class="input controlLength form-control">
                                    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">Batch</label>
                           <asp:DropDownList ID="ddlBatch_S" runat="server" class="input controlLength form-control"
                           OnSelectedIndexChanged="ddlBatch_S_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
                    </div>
                    <div class="form-group" runat="server" id="divgroup_S" visible="false">
                        <label for="exampleInputName2">Group</label>
                        <asp:DropDownList ID="ddlgroup_S" runat="server" class="input controlLength form-control" 
                        OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged"  ClientIDMode="Static" Enabled="true"  AutoPostBack="True"></asp:DropDownList>
                     </div>
                    <div class="form-group">
                       <label for="exampleInputName2">Section</label>
                        <asp:DropDownList ID="ddlSection_S" runat="server" AutoPostBack="true"
                         OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" class="input controlLength form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" class="input controlLength form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">From Date</label>
                        <asp:TextBox ID="txtFromDate" Width="120" runat="server" CssClass="input controlLength form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtFromDate">
                                </asp:CalendarExtender>
                     </div>
                    <div class="form-group">
                       <label for="exampleInputName2">To Date</label>
                        <asp:TextBox ID="txtToDate" Width="120" runat="server" CssClass="input controlLength1 form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                            TargetControlID="txtToDate">
                        </asp:CalendarExtender>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2"></label>
                        <asp:Button ID="btnDiscountSummary" Text="Preview & Print " ClientIDMode="Static" runat="server"
                                 OnClientClick="return validateDropDown();"   
                                    CssClass="btn btn-success litleMargin" OnClick="btnDiscountSummary_Click"/>
                    </div>
                    
                </div>
            </div>
        </div> 
            
        <%--<div class="row">
                        <div class="col-md-12">
                            <div class="form-inline">
                                <div class="form-group">
                                    <label for="exampleInputName2">Shift</label>
                                    <asp:DropDownList ID="ddlShift_S" Width="120px" runat="server" class="input controlLength">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail2">Batch</label>
                                    <asp:DropDownList ID="ddlBatch_S" Width="120px" runat="server" class="input controlLength"
                                         OnSelectedIndexChanged="ddlBatch_S_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divgroup_S" visible="false">
                                    <label for="exampleInputName2">Group</label>
                                    <asp:DropDownList ID="ddlgroup_S" Width="120px" runat="server" class="input controlLength" 
                                       OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged"  ClientIDMode="Static" Enabled="true"  AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputName2">Section</label>
                                    <asp:DropDownList ID="ddlSection_S" Width="120px" runat="server" AutoPostBack="true"
                                         OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" class="input controlLength"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputName2">Category</label>
                                    <asp:DropDownList ID="ddlCategory"  Width="120px" runat="server" class="input controlLength">
                                    </asp:DropDownList>
                                </div>                               
                               <div class="form-group">
                                    <label for="exampleInputName2">From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="120px" CssClass="input controlLength"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtFromDate">
                                </asp:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail2">To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" Width="120px" CssClass="input controlLength"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtToDate">
                                </asp:CalendarExtender>
                                </div>                                                              
                                <asp:Button ID="btnDiscountSummary" Text="Preview & Print " ClientIDMode="Static" runat="server"
                                 OnClientClick="return validateDropDown();"   
                                    CssClass="btn btn-success litleMargin" OnClick="btnDiscountSummary_Click"/>

                            </div>
                           
                        </div>
                    </div>--%>
        <br />       
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
