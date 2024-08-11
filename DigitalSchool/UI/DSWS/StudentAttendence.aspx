<%@ Page Title="ছাত্র/ছাত্রীর হাজিরা" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="StudentAttendence.aspx.cs" Inherits="DS.UI.DSWS.StudentAttendence" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>        
        .tgPanel {
            width: 809px;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel td:nth-child(1),
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5) {
            padding: 0px 5px;
        }
        .tbl-controlPanel td:nth-child(1),        
        .tbl-controlPanel td:nth-child(4) {
            text-align:center;
        }
        .tbl-controlPanel td:nth-child(1){
            width:55px;
        }
        
        .tblstdatt th:nth-child(3),
        .tblstdatt th:nth-child(4),
        .tblstdatt th:nth-child(5),
        .tblstdatt th:nth-child(6),        
        .tblstdatt td:nth-child(3),
        .tblstdatt td:nth-child(4),
        .tblstdatt td:nth-child(5),
        .tblstdatt td:nth-child(6)
         {
            text-align: center;
        }
        .tblstdatt th:nth-child(1),
        .tblstdatt th:nth-child(2),
        .tblstdatt td:nth-child(1),
        .tblstdatt td:nth-child(2)
        {
            text-align:left
        }
        .litleMargin {
            margin-left: 5px;
        }
        .btn {
            margin: 3px;
        }
        .tbl-controlPanel {
            width:345px;
        }
        .techwidth {
            width:745px;
        }
        .techImage{
             height:45px;
             width:40px;
             text-align:center;
        }
        .ajax__calendar_container{
             width:180px !important;
        }
        .tdate{
            width:180px;
        }
        .txtcenter{
            text-align:center;
        }
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
  vertical-align:middle !important;
}
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    <div class="row">
         <div runat="server" id="divBoardDirContacts" class="main-content">
            
    <asp:UpdatePanel runat="server" ID="upstd" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnStdSearch" />
        </Triggers>
        <ContentTemplate>   
    <table class="tbl-controlPanel" style="width: 300px">
        <tr>
            <td colspan="3">
                <h3 style="text-align: center;"><u>শিক্ষার্থী হাজিরা</u></h3>
            </td>
        </tr>
        <tr>
            <td>তারিখঃ</td>
            <td>
                <asp:TextBox ID="txtStdDate" ClientIDMode="Static" TabIndex="1" runat="server" CssClass="input controlLength tdate"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                    TargetControlID="txtStdDate">
                </asp:CalendarExtender>
            </td>
            <td>
                <asp:Button ID="btnStdSearch" Text="খুঁজুন" CssClass="btn btn-primary litleMargin" ClientIDMode="Static"
                    runat="server" OnClientClick="return validationInput();" OnClick="btnStdSearch_Click" />
            </td>
        </tr>
    </table>
             </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="widget">
            <div id="div1" class="datatables_wrapper" runat="server" style="width: 100%; height: auto">
                <asp:GridView ID="gvStudentAtt" runat="server" ClientIDMode="Static" DataKeyNames="BatchId,ClsGrpID,ClsSecID"
                    CssClass="table table-bordered tbl-controlPanel techwidth tblstdatt"
                    AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvStudentAtt_PageIndexChanging">
                    <PagerStyle CssClass="gridview" />
                    <Columns>

                        <asp:TemplateField HeaderText="ব্যাচ">
                            <ItemTemplate>
                                <asp:Label ID="lblBatch" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "BatchName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="গ্রুপ">
                            <ItemTemplate>
                                <asp:Label ID="lblGroupName" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "GroupName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="শাখা">
                            <ItemTemplate>
                                <asp:Label ID="lblSection" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "SectionName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="মোট শিক্ষার্থী">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalStudent" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "TotalStudent")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="উপস্থিত শিক্ষার্থী">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPresent" runat="server" CssClass="input controlLength txtcenter" AutoPostBack="false"
                                    onchange="return GetSelectedRow(this,1);" Text='<%# DataBinder.Eval(Container.DataItem, "TotalPresent")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="অনুপস্থিত শিক্ষার্থী">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAbsent" runat="server" CssClass="input controlLength txtcenter" AutoPostBack="true"
                                    onchange="return GetSelectedRow(this,2);" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAbsent")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
              </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
   
</asp:Content>
