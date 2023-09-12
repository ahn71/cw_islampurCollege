<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddQuickLink.aspx.cs" Inherits="DS.UI.Administration.DSWS.AddQuickLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                <li><a runat="server" id="aQuickLink">Quick Link</a></li>
                <li runat="server" id="liAddEdit" class="active"></li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" />
            </Triggers>
        <ContentTemplate>
            <div class="row">
            <div class="col-md-5">
                <div class="tgPanelHead">Add Quick Link</div>
                <div class="tgPanel">

                    <div class="row" style="margin-left: 10px">
                    </div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Title
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" Width="246px" ClientIDMode="Static" class="input controlLength"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Link
                            </td>
                            <td>
                                <asp:TextBox ID="txtUrl" runat="server" Width="246px" ClientIDMode="Static" class="input controlLength"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Ordering
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrdering" runat="server" Width="246px" ClientIDMode="Static" class="input controlLength"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtOrdering" FilterType="Numbers" FilterMode="ValidChars">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="ckbIsActive" CssClass="chkbox" runat="server" ClientIDMode="Static" Text="Is Active" Checked="true" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" ClientIDMode="Static" OnClientClick="return validateInputs()" OnClick="btnSave_Click" />
                                &nbsp;<asp:Button runat="server" Text="Clear" ID="btnClear" ClientIDMode="Static" CssClass="btn btn-default" OnClick="btnClear_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">            
        function validateInputs() {
            if (validateText('txtTitle', 1, 150, 'Enter Valid Title') == false) return false;
            if (validateText('txtUrl', 1, 150, 'Enter Valid Link') == false) return false;
            if (validateText('txtOrdering', 1, 3, 'Enter Valid Order No') == false) return false;
            return true;
        }
    </script>
</asp:Content>
