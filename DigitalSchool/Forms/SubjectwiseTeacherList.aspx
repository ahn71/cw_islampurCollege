<%@ Page Title="Department wise Teacher List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubjectwiseTeacherList.aspx.cs" Inherits="DS.Forms.SubjectwiseTeacherList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }

        .tgPanel {
            width: 100%;
        }

        input[type="radio"] {
            margin: 10px;
        }

        table td {
            margin: 4px !Important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="tgInput" style="width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdoWithImage" />
                    <asp:AsyncPostBackTrigger ControlID="rdoNoImage" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Department</td>
                            <td>
                                <asp:DropDownList ID="ddlDepartmentList" Style="width: 120px;" runat="server" ClientIDMode="Static">
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>                                
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-primary litleMargin"
                                    OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:RadioButton runat="server" ID="rdoWithImage" Checked="true" ClientIDMode="Static" Text="With Image" 
                                    AutoPostBack="true" OnCheckedChanged="rdoWithImage_CheckedChanged" />                                
                                <asp:RadioButton runat="server" ID="rdoNoImage" ClientIDMode="Static" Text="Without Image"
                                    AutoPostBack="true" OnCheckedChanged="rdoNoImage_CheckedChanged" />                                
                            </td>                          
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px" 
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                        <div class="dataTables_filter" style="float: right;">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divTeacherList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
    </script>
</asp:Content>
