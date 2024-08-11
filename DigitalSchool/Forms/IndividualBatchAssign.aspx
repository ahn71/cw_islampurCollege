<%@ Page Title="Specific Student Batch Assign" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndividualBatchAssign.aspx.cs" Inherits="DS.Forms.IndividualBatchAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength {
            width: 200px;
            margin: 5px;
        }

        .tgPanel {
            width: 100%;
        }

        #tblSetRollOptionalSubject {
            width: 100%;
        }

            #tblSetRollOptionalSubject th,
            #tblSetRollOptionalSubject td,
            #tblSetRollOptionalSubject td input,
            #tblSetRollOptionalSubject td select {
                padding: 5px 5px;
                margin-left: 10px;
                text-align: center;
            }

        .litleMargin {
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="lblStudentId" ClientIDMode="Static" Value="" runat="server" />
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Specific Student Batch Assign</div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStName" runat="server" CssClass="input controlLength"
                                            ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>Roll
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStRoll" runat="server" CssClass="input controlLength"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td>Batch       
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                    </td>
                                    <td>Section
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dlClass" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="dlSectionAssign" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSsign" runat="server" Text="Assign" CssClass="btn btn-primary controlLength"
                                            OnClientClick="return validateInputs();" OnClick="btnSsign_Click" />
                                        <input type="button" value="Reset" class="btn btn-default controlLength" onclick="clearIt();" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <br />
    <%---------------------------Current Student List---------------------%>
    <div class="tgPanel" style="width: 100%">
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="head_title">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Class
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlClass" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                            AutoPostBack="True" OnSelectedIndexChanged="dlClass_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Section
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dlClass" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>Shift
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength">
                                            <asp:ListItem>Morning</asp:ListItem>
                                            <asp:ListItem>Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin"
                                            runat="server" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="dataTables_filter" style="float: right;">
                    <label>
                        Search:
                    <input type="text" class="search" placeholder="type here..." />
                        <div class=""></div>
                    </label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div id="divStudentDetails" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtStName', 1, 20, 'Select student') == false) return false;
            else if (validateText('txtStRoll', 1, 20, 'Enter roll') == false) return false;
            return true;
        }
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function studentAssign(studentId) {
            $('#lblStudentId').val(studentId);
            $('#imgProfile').val();
            var strStname = $('#r_' + studentId + ' td:nth-child(3)').html();
            var strStRoll = $('#r_' + studentId + ' td:nth-child(4)').html();
            var strClass = $('#r_' + studentId + ' td:nth-child(1)').html();
            var strSection = $('#r_' + studentId + ' td:nth-child(2)').html();
            $('#txtStName').val(strStname);
            $('#txtStRoll').val(strStRoll);
            $('#dlClassName').val(strClass);
            $('#dlSectionName').val(strSection);
            // $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#txtStName').val('');
            $('#txtStRoll').val('');
            $('dlClassName').val('');
            $('dlSectionName').val('');
        }
    </script>
</asp:Content>
