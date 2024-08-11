<%@ Page Title="Set Roll And Optional Subject For New Student Of Class Nine" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetRollSubjectClassNine.aspx.cs" Inherits="DS.Forms.SetRollSubjectClassNine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength{
            width:200px;
            margin: 5px;
        }
        .tgPanel
        {
            width: 100%;
        }
        #tblSetRollOptionalSubject
        {
            width:100%;            
        }
        #tblSetRollOptionalSubject th,  
        #tblSetRollOptionalSubject td, 
        #tblSetRollOptionalSubject td input,
        #tblSetRollOptionalSubject td select
        {
            padding: 5px 5px;
            margin-left: 10px;
            text-align: center;
        }
        .litleMargin{
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Set Roll And Optional Subject For New Student Of Class Nine</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Shift</td>
                                    <td>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="input controlLength">
                                            <asp:ListItem Value="0">Morning</asp:ListItem>
                                            <asp:ListItem Value="1">Day</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>Batch</td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Section</td>
                                    <td>
                                        <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength"></asp:DropDownList></td>

                                    <td>
                                        <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server"
                                            CssClass="btn btn-primary" OnClick="btnProcess_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <br />
                                    <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                        <p>Wait attendance sheet is processing</p>
                                    </span>
                                    <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div runat="server" id="divStduentInfo"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function setOS(celldata) // os= Optional Subject
        {
            var getId = celldata.id;
            var splitedData = getId.split("_");
            var getVal = celldata.value;
            $('#opt_' + getId).val(getVal);
            var getTxt = $("#" + getId + " option:selected").text();
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + getVal + '&do=attUpdate', function (data) {
            });
        }
        function saveData(celldata) {
            var getId = celldata.id;
            var splitedData = getId.split("_");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=setRoll_OS', function (data) {
                //jx.load('ForUpdate.aspx?tbldata=' + splitedData + ', function (data) {
                //  alert(data);
            });
        }
    </script>
</asp:Content>
