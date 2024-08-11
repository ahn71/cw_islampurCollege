<%@ Page Title="Class Subject Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassSubjectSetup.aspx.cs" Inherits="DS.Forms.ClassSubjectSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 450px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="CSId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-7">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="ddlClassList" />
                        <asp:AsyncPostBackTrigger ControlID="ddlClassName" />
                    </Triggers>
                    <ContentTemplate>
                        <table class="col-md-12">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlClassList" runat="server" CssClass="input"
                                        Style="height: 26px; width: 150px; margin-right: 10px; padding: 2px" ClientIDMode="Static"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlClassList_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">---Select Class---</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p class="text-right">Classwise Subject List</p>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-5"></div>
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-7">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="ddlClassList" />
                                <asp:AsyncPostBackTrigger ControlID="ddlClassName" />
                            </Triggers>
                            <ContentTemplate>
                                <div id="divClassSubject" class="datatables_wrapper tgPanel" runat="server"
                                    style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-5">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="ddlClassName" />
                                <asp:AsyncPostBackTrigger ControlID="ddlSubject" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="">
                                    <div class="tgPanel">
                                        <div class="tgPanelHead">Class Subject Setup</div>
                                        <table class="tbl-controlPanel">
                                            <tr>
                                                <td>Select Class</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClassName" runat="server" CssClass="input" Style="padding: 2px"
                                                        ClientIDMode="Static" Height="26px" Width="292px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subject</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="input"
                                                        ClientIDMode="Static" Height="26px" Width="150px" Style="margin-right: 10px; padding: 2px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    Sub.Code
                                                    <asp:TextBox ID="txtSubCode" Width="60px"
                                                        CssClass="input" Style="margin-left: 14px" runat="server" MaxLength="7"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trDependencysub" visible="false">
                                                <td>Dependency
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddldependencysub" runat="server" CssClass="input" Style="padding: 2px"
                                                        ClientIDMode="Static" Height="26px" Width="292px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="buttonBox">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" ClientIDMode="Static"
                                                Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                            &nbsp;<input type="button" value="Reset" class="btn btn-default" onclick="clearText();" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('ddlClassName', 1, 30, 'Select Class') == false) return false;
            if (validateText('ddlSubject', 1, 50, 'Select Subject') == false) return false;
            return true;
        }
        function editClassSubject(Id) {
            $('#CSId').val(Id);
            var strClass = $('#r_' + Id + ' td:first-child').html();
            $('#ddlClassName option:selected').text(strClass);
            var strP = $('#r_' + Id + ' td:nth-child(2)').html();
            $('#ddlSubject option:selected').text(strP);
            $("#btnSave").val('Update');
        }
        function clearText() {
            $('#CSId').val('');
            $('#ddlClassName').select('');
            $('#ddlSubject').select('');
            $('#btnSave').val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearText();
        }
        function SaveSuccess() {
            showMessage('Save successfully', 'success');
            clearText();
        }
    </script>
</asp:Content>

