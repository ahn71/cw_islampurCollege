<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Notice.aspx.cs" Inherits="DS.UI.Administration.DSWS.Notice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .input {
            color: #000;
        }

        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }

        .tbl-controlPanel {
            width: auto;
        }

        .table-bordered tr th {
            background-color: #23282C;
            color: white;
        }

        .padingtable {
            margin-top: 0px;
        }

            .padingtable th:nth-child(7), th:nth-child(8), td:nth-child(5), td:nth-child(6) {
                text-align: center;
                width: 30px;
            }

        .chkbox label {
            margin-left: 7px;
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
                <%-- <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>   --%>
                <li>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li><a runat="server" id="aWebsite">Website Settings Module</a></li>
                <li class="active">Notice</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>



    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <div class="dataTables_filter_New">
                    <label>
                        Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                    </label>
                </div>
            </div>
            <div class="col-md-4" style="float: right">
                <asp:Button CssClass="btn btn-primary" ID="btnAdd" runat="server" Text="Add New" ClientIDMode="Static" OnClick="btnAdd_Click" />
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">



            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="tgPanel">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvNoticeList" CssClass="table table-striped table-bordered dt-responsive nowrap" Width="100%" CellSpacing="0" DataKeyNames="NSL,IsActive,NDetails,PublishdDate,onlyFileName,IsImportantNews" AutoGenerateColumns="False" runat="server" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvNoticeList_PageIndexChanging" OnRowCommand="gvNoticeList_RowCommand" OnRowDeleting="gvNoticeList_RowDeleting">

                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Serial No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Subject" DataField="NSubject" />
                                    <asp:BoundField HeaderText="Notice" DataField="NDetails" />
                                    <asp:BoundField HeaderText="Entry Date" DataField="NEntryDate" />
                                    <asp:BoundField HeaderText="Published Date" DataField="PublishdDate" />
                                    <asp:TemplateField HeaderText="Important News">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsImportantNews" Enabled="false" runat="server" Checked='<%#bool.Parse(Eval("IsImportantNews").ToString())%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsActive" Enabled="false" runat="server" Checked='<%#bool.Parse(Eval("IsActive").ToString())%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pin to Top">
                                        <ItemTemplate>
                                            <asp:Button ID="btnPin" runat="server" Text='<%#Eval("pinText").ToString()%>' CssClass="btn btn-danger" CommandName="Pin" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure want to pin to top.');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit" ControlStyle-CssClass="btn btn-success">
                                        <ControlStyle CssClass="btn btn-success" />
                                    </asp:ButtonField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-2"></div>


        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvNoticeList', '');
                $('#MainContent_gvNoticeList').dataTable({
                    "iDisplayLength": 10,
                    "lengthMenu": [10, 20, 30, 40, 50, 100]
                });
            });
        });
        function loadStudentInfo() {
            $('#MainContent_gvNoticeList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
    </script>
</asp:Content>
