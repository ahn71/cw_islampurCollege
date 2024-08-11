<%@ Page Title="Collection Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CollectionDetails.aspx.cs" Inherits="DS.Report.CollectionDetails" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel{
            width:90%;
        }
        input[type="checkbox"]
        {
            margin: 5px;
        }
        .controlLength{
            width:200px;
        }
        .tbl-controlPanel
        {
            width: 900px;
        }
        .tbl-controlPanel tr td{
            margin: 5px;
        }
        .ajax__tab_tab
        {
            -webkit-box-sizing: content-box!important;
            -moz-box-sizing: content-box!important;
            box-sizing: content-box!important;
        }               
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" ForeColor="Black" Width="100%">
            <ajax:TabPanel ID="TabPanel1" runat="server" HeaderText="View">
                <ContentTemplate>
                    <div class="row">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Collection View</div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="tbl-controlPanel" style="width: 580px;">
                                        <tr>
                                            <td>Batch
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlBatch" AutoPostBack="true" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged"
                                                    runat="server" class="input controlLength">
                                                </asp:DropDownList></td>
                                            <td>Categery
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="dlCategory"
                                                    CssClass="input controlLength">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="3">
                                                <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="border: 1px solid gray; padding: 05px;" runat="server" id="divFeeCategory" visible="false">
                                                <asp:Label runat="server" ID="lblCategory"></asp:Label>
                                            </div>
                                            <div id="divCollectionView" class="datatables_wrapper" runat="server"
                                                style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
            <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="Summary">
                <ContentTemplate>
                    <div class="row">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Collection Summary</div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatchSummary" />
                                </Triggers>
                                <ContentTemplate>
                                    <table class="tbl-controlPanel">
                                        <tr>
                                            <td>Batch
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlBatchSummary" AutoPostBack="true" OnSelectedIndexChanged="dlBatchSummary_SelectedIndexChanged"
                                                    runat="server" CssClass="input controlLength">
                                                    <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>Section
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlSectionSummary" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                            </td>
                                            <td>Shift                        
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                    CssClass="input controlLength">
                                                    <asp:ListItem>Morning</asp:ListItem>
                                                    <asp:ListItem>Day</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>From
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFromDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"></ajax:CalendarExtender>
                                            </td>
                                            <td>To
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtToDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"></ajax:CalendarExtender>
                                            </td>
                                            <td>Fee Category
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlFeeCategorySummary" runat="server" CssClass="input controlLength">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:CheckBox ID="chkToday" ClientIDMode="Static" Text="Is Today Collection" runat="server" />
                                                <asp:Button ID="btnSearchSummary" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                    OnClick="btnSearchSummary_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divCollectionSummary" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <asp:Label ID="lblClassName" runat="server" ForeColor="Black"></asp:Label>
                                        <asp:Button ID="btnPreview" OnClick="btnPreview_Click" Visible="false" Text="Preview" CssClass="btn btn-success"
                                            ClientIDMode="Static" runat="server" />
                                        <br />
                                        <br />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
            <ajax:TabPanel ID="TabPanel3" runat="server" HeaderText="Details">
                <ContentTemplate>
                    <div class="row">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Collection Details</div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="">
                                        <table class="tbl-controlPanel">
                                            <tr>
                                                <td>Batch
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlBatchDetails" AutoPostBack="true" CssClass="input controlLength"
                                                        OnSelectedIndexChanged="dlBatchDetails_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>Section
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlSectionDetails" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                                </td>
                                                <td>Shift                    
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlShiftForDetails" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                        AutoPostBack="false">
                                                        <asp:ListItem>Morning</asp:ListItem>
                                                        <asp:ListItem>Day</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>From
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFromDateDetails" ClientIDMode="Static"
                                                        CssClass="input controlLength"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        TargetControlID="txtFromDateDetails">
                                                    </ajax:CalendarExtender>
                                                </td>
                                                <td>To
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtToDateDetails" ClientIDMode="Static"
                                                        CssClass="input controlLength"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender4" runat="server"
                                                        TargetControlID="txtToDateDetails">
                                                    </ajax:CalendarExtender>
                                                </td>
                                                <td>Fee Category
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlFeeCategoryDetails" runat="server" CssClass="input controlLength">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnSearchDetails" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                        OnClick="btnSearchDetails_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divCollectionDetails" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <asp:Label ID="lblClassDetails" runat="server" ForeColor="Black"></asp:Label>
                                        <asp:Button ID="btnPreviewDetails" OnClick="btnPreviewDetails_Click" Visible="false" Text="Preview" CssClass="greenBtn" ClientIDMode="Static" runat="server" />
                                        <br />
                                        <br />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
            <ajax:TabPanel ID="TabPanel4" runat="server" HeaderText="Due List">
                <ContentTemplate>
                    <div class="row">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Due List</div>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="">
                                        <table class="tbl-controlPanel" style="width: 800px;">
                                            <tr>
                                                <td>Batch
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlBatchDueList" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="dlBatchDueList_SelectedIndexChanged" CssClass="input controlLength">
                                                        <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>Section
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlSectionDueList" runat="server" CssClass="input controlLength">
                                                    </asp:DropDownList></td>
                                                <td>Shift
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlShiftDueList" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                        CssClass="input controlLength">
                                                        <asp:ListItem>Morning</asp:ListItem>
                                                        <asp:ListItem>Day</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Fee Category
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlFeesCategoryDueList" runat="server" CssClass="input controlLength">
                                                    </asp:DropDownList>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnSearchDueList" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                        OnClick="btnSearchDueList_Click" />
                                                    <asp:Button runat="server" ID="btnPrintPreviewDueList" Text="Print Preview" CssClass="btn btn-primary"
                                                        OnClick="btnPrintPreviewDueList_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3">
                                                    <asp:Label ID="Label2" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
</asp:Content>
