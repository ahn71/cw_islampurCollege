<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="QuickLink.aspx.cs" Inherits="DS.UI.Administration.DSWS.QuickLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }

        .table-bordered tr th {
            background-color: #23282C;
            color: white;
        }

        .padingtable {
            margin-top: 0px;
        }

            .padingtable th:nth-child(3), th:nth-child(4), th:nth-child(5), th:nth-child(6), td:nth-child(3), td:nth-child(4), td:nth-child(5), td:nth-child(6) {
                text-align: center;
                width: 50px;
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
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>--%>
                <li>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li><a runat="server" id="aWebsite">Website Settings Module</a></li>
                <li class="active">Quick Link</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-6">
                <div class="dataTables_filter_New">
                    <label>
                        Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                    </label>
                </div>
            </div>
            <div class="col-md-2">
                <asp:Button CssClass="btn btn-primary" ID="btnAdd" runat="server" Text="Add New" ClientIDMode="Static" OnClick="btnAdd_Click" />
            </div>
    </div>
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-8">
            <div class="tgPanel">
                <asp:GridView ID="gvSlider" CssClass="table table-bordered padingtable" DataKeyNames="SL" AutoGenerateColumns="False" runat="server" OnRowCommand="gvSlider_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                SL
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Title" DataField="Title" />
                        <asp:BoundField HeaderText="Link" DataField="Url" />
                        <asp:BoundField HeaderText="Ordering" DataField="Ordering" />
                        <asp:TemplateField HeaderText="Is Active?">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsActive" Enabled="false" runat="server" Checked='<%#bool.Parse(Eval("IsActive").ToString())%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit" ControlStyle-CssClass="btn btn-success" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="remove" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvSlider', '');
            });
        });
        function validateInputs() {
            if (validateText('txtTitle', 1, 150, 'Enter Valid Title') == false) return false;
            if (validateText('txtUrl', 1, 150, 'Enter Valid Link') == false) return false;
            if (validateText('txtOrdering', 1, 3, 'Enter Valid Order No') == false) return false;
            return true;
        }
    </script>
</asp:Content>
