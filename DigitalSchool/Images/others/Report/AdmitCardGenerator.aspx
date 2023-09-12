<%@ Page Title="Admit Card" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmitCardGenerator.aspx.cs" Inherits="DS.Forms.AdmitCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/reports/CommonBorder.css" rel="stylesheet" />
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
            height: 300px;
        }
        .controlLength {
            width: 200px;
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
        <div class="row">
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Admit Card Generate For All Students</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Exam</td>
                            <td>
                                <asp:DropDownList ID="dlExamType" runat="server" class="input controlLength"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Class</td>
                            <td>
                                <asp:DropDownList ID="dlClass" runat="server" class="input controlLength"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSection" runat="server" class="input controlLength"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftAdmit" runat="server"
                                    ClientIDMode="Static" AutoPostBack="false" class="input controlLength">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnACGenerate" class="btn btn-primary" runat="server" Text="Process"
                                    OnClick="btnACGenerate_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Admit Card Generate For Individual Student</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Exam</td>
                            <td>
                                <asp:DropDownList ID="dlExamForAdmintcardByRoll"
                                    runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Class</td>
                            <td>
                                <asp:DropDownList ID="dlClassForAdmintcardByRoll"
                                    runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSectionForAdmintcardByRoll"
                                    runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftForAdmitRoll" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Roll</td>
                            <td>
                                <asp:TextBox ID="txtAdmitCardRoll" runat="server" CssClass="input controlLength"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAdmitCardProcessByRoll" OnClientClick="checking();" CssClass="btn btn-primary"
                                    ClientIDMode="Static" runat="server" Text="Porcess" OnClick="btnAdmitCardProcessByRoll_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Id Card Generate For All Students</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Class</td>
                            <td>
                                <asp:DropDownList ID="dlClassForIdCard" runat="server"
                                    CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSectionForIdCard" runat="server"
                                    CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftForIdCard" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnIdCardGenerate" CssClass="btn btn-primary" runat="server"
                                    Text="Porcess" OnClick="btnIdCardGenerate_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Id Card Generate For Individual Student</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Class</td>
                            <td>
                                <asp:DropDownList ID="dlClassForIdCardByROll" runat="server"
                                    CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSectionForIdCardByRoll" runat="server"
                                    CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftForIdCardRoll" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Roll</td>
                            <td>
                                <asp:TextBox ID="txtIdCardRoll" runat="server" ClientIDMode="Static"
                                    CssClass="input controlLength"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Porcess"
                                    OnClientClick="return validateInputs();" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtIdCardRoll', 1, 50, 'Enter a roll number') == false) return false;
            return true;
        }
        function editBoards(BoardId) {
            $('#lblBoardId').val(BoardId);

            var strBoardName = $('#r_' + BoardId + ' td:first-child').html();
            $('#txtBoardName').val(strBoardName);
            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>
