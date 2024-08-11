<%@ Page Title="New Subject" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSubject.aspx.cs" Inherits="DS.Forms.NewSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 100%;            
        } 
        .NoneBorder{
            border: none;
        }       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblSubId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">New Subject</div>
                            <table class="tbl-controlPanel">                                
                                <tr>
                                    <td>Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubName" runat="server" Width="290px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Marks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubTotalMarks" runat="server" Width="123px" ClientIDMode="Static" Style="margin-right: 5px" CssClass="input"></asp:TextBox>Order<asp:TextBox ID="txtOrder" runat="server" Width="123px" ClientIDMode="Static" CssClass="input" Style="margin-left: 5px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="chkIsOptional" runat="server" ClientIDMode="Static" Checked="false" />
                                        &nbsp; Is Optional 
                                    <asp:CheckBox ID="chkdependency" runat="server" ClientIDMode="Static" Style="margin-left: 75px" Checked="false" />
                                        &nbsp; Dependency 
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Reset" onclick="clearIt();" />
                            </div>
                        </div>                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-3"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <p class="text-left">Compulsory Subject List</p>
            </div>
            <div class="col-md-6">
                <p class="text-right">Optional Subject List</p>
            </div>
        </div>
        <div class="row">
            <div class="tgPanel NoneBorder">
                <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divSubjectList" class="datatables_wrapper" runat="server"
                                style="width: 100%; height: auto; max-height: 245px; overflow: auto; overflow-x: hidden;">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divOptionlist" runat="server" class="datatables_wrapper"
                                style="width: 100%; height: auto; max-height: 245px; overflow: auto; overflow-x: hidden;">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtSubName', 1, 60, 'Enter Subject Name') == false) return false;
            if (validateText('txtSubTotalMarks', 1, 10, 'Enter Marks') == false) return false;
            return true;
        }
        function editSubject(Id) {
            $('#lblSubId').val(Id);
            var strAT = $('#r_' + Id + ' td:first-child').html();
            $('#txtSubName').val(strAT);
            var strP = $('#r_' + Id + ' td:nth-child(2)').html();
            $('#txtSubTotalMarks').val(strP);
            var strO = $('#r_' + Id + ' td:nth-child(3)').html();
            $('#txtOrder').val(strO);
            if ($('#chkid' + Id).is(':checked')) {
                $("#chkdependency").removeProp('checked');
                $("#chkdependency").click();
            }
            else {
                $("#chkdependency").removeProp('checked');
            }
            $("#btnSave").val('Update');
            $("#chkIsOptional").removeProp('checked');
            $("#btnSave").val('Update');
        }
        function editOptionlSubject(Id) {
            $('#lblSubId').val(Id);
            var strAT = $('#r_' + Id + ' td:first-child').html();
            $('#txtSubName').val(strAT);
            var strP = $('#r_' + Id + ' td:nth-child(2)').html();
            $('#txtSubTotalMarks').val(strP);
            var strO = $('#r_' + Id + ' td:nth-child(3)').html();
            $('#txtOrder').val(strO);
            if ($('#chkid' + Id).is(':checked')) {
                $("#chkdependency").removeProp('checked');
                $("#chkdependency").click();
            }
            else {
                $("#chkdependency").removeProp('checked');
            }
            $("#btnSave").val('Update');
            $("#chkIsOptional").removeProp('checked');
            $("#chkIsOptional").click();
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblSubId').val('');
            $('#txtSubName').val('');
            $('#txtSubCode').val('');
            $('#txtSubTotalMarks').val('');
            $('#txtOrder').val('');
            $("#chkIsOptional").removeProp('checked');
            $("#chkdependency").removeProp('checked');
            setFocus('txtSubName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SaveSuccess() {
            showMessage('Save Successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
