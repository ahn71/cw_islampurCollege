 <%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmCollectionDetails.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.AdmCollectionDetails" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;
        }
        input[type="checkbox"]
        {
            margin: 5px;
        }
        /*.controlLength{
            width:200px;
        }*/
        /*.tbl-controlPanel
        {
            width: 778px;
        }*/
        #btnCSearch {
            margin-bottom:150px;
           

        }
        .tbl-controlPanel td:first-child,
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5)
        {
            text-align: right;
            padding-right: 5px;
        }
        .ajax__tab_tab
        {
            -webkit-box-sizing: content-box!important;
            -moz-box-sizing: content-box!important;
            box-sizing: content-box!important;
        }  
        .ajax__tab_default .ajax__tab_tab
        {
            display: inline!important;            
        } 
        .ajax__tab_xp .ajax__tab_header .ajax__tab_outer{
            height: 20px!important;
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
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a id="A4" runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Admission Collection Details</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" Width="100%" TabIndex="1">
            <ajax:TabPanel ID="TabPanel5" TabIndex="1" runat="server" HeaderText="Collection Details">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Admission Fees Collection Details</div>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCClass" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-8 col-sm-offset-2">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4">Shift</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCShift" runat="server" ClientIDMode="Static"
                                                            CssClass="input controlLength form-control"> 
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>                                                   
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4">Class</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCClass" AutoPostBack="true" ClientIDMode="Static"
                                                             OnSelectedIndexChanged="ddlCClass_SelectedIndexChanged"
                                                            runat="server" CssClass="input controlLength form-control">
                                                            <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4">Fee Category</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCFeeCat" runat="server" ClientIDMode="Static"
                                                             CssClass="input controlLength form-control">
                                                            <asp:ListItem Value="All">...All...</asp:ListItem>      
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4">From</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" ID="txtCFrom" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender5" Format="dd-MM-yyyy" runat="server" 
                                                            TargetControlID="txtCFrom"></ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4">To</label>
                                                    <div class="col-sm-8">
                                                         <asp:TextBox runat="server" ID="txtCTo" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender6" Format="dd-MM-yyyy" runat="server" 
                                                                TargetControlID="txtCTo"></ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    
                                                    <div class="col-sm-12">
                                                        <asp:CheckBox ID="chkCTodayCollect" ClientIDMode="Static" Text="Is Today Collection" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4"></label>
                                                    <div class="col-sm-8"></div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4"></label>
                                                    <div class="col-sm-8"></div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="col-sm-4"></label>
                                                    <div class="col-sm-8">
                                                         <asp:Button ID="btnCSearch" OnClick="btnCSearch_Click" Text="Preview & Print" 
                                                    OnClientClick="return btnCSearch_Validation();" 
                                                    ClientIDMode="Static" runat="server" CssClass="btn btn-success"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <table class="tbl-controlPanel">
                                        <tr>
                                            <td>Shift                        
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCShift" runat="server" ClientIDMode="Static"
                                                    CssClass="input controlLength"> 
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </td>
                                            <td>Class
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCClass" AutoPostBack="true" ClientIDMode="Static"
                                                     OnSelectedIndexChanged="ddlCClass_SelectedIndexChanged"
                                                    runat="server" CssClass="input controlLength">
                                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                </asp:DropDownList></td>                                                                                     
                                            <td>Fee Category
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCFeeCat" runat="server" ClientIDMode="Static"
                                                     CssClass="input controlLength">
                                                    <asp:ListItem Value="All">...All...</asp:ListItem>      
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>From
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtCFrom" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender5" Format="dd-MM-yyyy" runat="server" 
                                                    TargetControlID="txtCFrom"></ajax:CalendarExtender>
                                            </td>
                                            <td>To
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtCTo" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender6" Format="dd-MM-yyyy" runat="server" 
                                                    TargetControlID="txtCTo"></ajax:CalendarExtender>
                                            </td> 
                                            <td></td>
                                            <td>
                                                <asp:CheckBox ID="chkCTodayCollect" ClientIDMode="Static" Text="Is Today Collection" runat="server" />
                                            </td>                                           
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>                                                                         
                                            <td></td>
                                            <td></td>                                                 
                                            <td>                                                
                                                <asp:Button ID="btnCSearch" OnClick="btnCSearch_Click" Text="Preview & Print" 
                                                    OnClientClick="return btnCSearch_Validation();" 
                                                    ClientIDMode="Static" runat="server" CssClass="btn btn-success"/>
                                            </td>
                                        </tr>
                                    </table>  --%>                                 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>          
           
            <ajax:TabPanel ID="TabPanel4" TabIndex="5" runat="server" HeaderText="Unpaid Student List">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Unpaid Student List</div>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnPrintPreviewDueList" />                                   
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row">
                                            <div class="col-sm-6 col-sm-offset-3">
                                                <div class="row tbl-controlPanel">
                                                    <label class="col-sm-2">Shift</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="dlShiftDueList" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                        CssClass="input controlLength form-control">
                                                        
                                                    </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-2">Class</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="dlClassDueList" AutoPostBack="false" runat="server" ClientIDMode="Static"
                                                         CssClass="input controlLength form-control">
                                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-8"></div>
                                                    <div class="col-sm-4">
                                                        <asp:Button runat="server" ID="btnPrintPreviewDueList" Text="Preview & Print" CssClass="btn btn-success" ClientIDMode="Static"
                                                        OnClick="btnPrintPreviewDueList_Click" />
                                                    </div>
                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <label class="col-sm-2"></label>
                                                    <div class="col-sm-4">
                                                        <asp:Label ID="Label2" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<table class="tbl-controlPanel" style="width: 478px;">
                                            <tr>
                                                <td>Shift
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlShiftDueList" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                        CssClass="input controlLength">
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Class
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlClassDueList" AutoPostBack="false" runat="server" ClientIDMode="Static"
                                                         CssClass="input controlLength">
                                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList></td>                                                
                                                
                                            </tr>
                                            <tr >
                                                <td> </td>                                               
                                                <td></td>
                                                <td></td>
                                                <td>                                                    
                                                    <asp:Button runat="server" ID="btnPrintPreviewDueList" Text="Preview & Print" CssClass="btn btn-success" ClientIDMode="Static"
                                                        OnClick="btnPrintPreviewDueList_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3">
                                                    <asp:Label ID="Label2" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </div>
                                    <div id="divDueList" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <h4>
                                            <asp:Label runat="server" Text="" ID="lblBatch" class="lblFontStyle"></asp:Label></h4>
                                        <h4>
                                            <asp:Label runat="server" Text="" ID="lblShift" class="lblFontStyle"></asp:Label></h4>
                                        <h4>
                                            <asp:Label runat="server" Text="" ID="lblSection" class="lblFontStyle"></asp:Label></h4>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>            
        </ajax:TabContainer>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        function btnCSearch_Validation() {            
            if (validateCombo('ddlCFeeCat', "0", 'Select a Fee Category') == false) return false;
            var fromDate = $('#txtCFrom').val();
            var toDate = $('#txtCTo').val();
            var chk = $('#chkCTodayCollect').is(":checked");
            if (fromDate == "" && toDate == "" && chk == false) {
                showMessage("Plese Select Date Rang", 'error');
                return false;
            }
            if (fromDate != "" && toDate != "" && chk != false) {
                showMessage("Plese Select From And To Date Or Is Today Collection Collection At a Time", 'error');
                return false;
            }
            if ((fromDate == "" && toDate != "") || (fromDate != "" && toDate == "") && chk == false) {
                showMessage("Plese Select Another Date Rang", 'error');
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
