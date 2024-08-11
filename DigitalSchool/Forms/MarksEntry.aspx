<%@ Page Title="Marks Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarksEntry.aspx.cs" Inherits="DS.Forms.MarksEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 70%; margin: 0 auto">
        <div class="tgInput" style="width: 400px; margin: 20px auto; height: 200px; float: left;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 56px">Shift </td>
                            <td>
                                <asp:DropDownList ID="ddlShift" Width="282px" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Value="0">Morning</asp:ListItem>
                                    <asp:ListItem Value="1">Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 56px">Batch</td>
                            <td>
                                <asp:DropDownList ID="ddlBatch" Width="140px" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSectionName" Width="140px" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 56px">Exam Id</td>
                            <td>
                                <asp:DropDownList ID="ddlExamId" Style="width: 282px;" runat="server" ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddlExamId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <img style="width: 60px; height: 26px; cursor: pointer;" src="/images/action/search.gif" onclick="$('#btnSearch').click();" />
                                <asp:Button ID="btnSearch" Style="display: none;" Text="Search" ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnRefresh" Text="Refresh" ClientIDMode="Static" runat="server" CssClass="greenBtn" Style="width: 60px; height: 26px;" OnClick="btnRefresh_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center; font-size: -25px">
                        <p runat="server" id="MarkSheetTitle" visible="false"></p>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                <p>Wait Total result Processing </p>
                            </span>
                            <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="tgInput" style="width: 400px; margin: 20px auto; height: 200px; float: left;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlExamId" />
                    <%--       <asp:AsyncPostBackTrigger ControlID="rdoNoImage" />--%>
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 56px">Subject </td>
                            <td>
                                <asp:DropDownList ID="ddlsubjectName" Style="width: 307px;" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 56px"></td>
                            <td>
                                <asp:Button Text="Preview Marks list" CssClass="greenBtn" ID="btnPreviewMarksheet" runat="server" Width="154px" OnClick="btnPreviewMarksheet_Click" />
                                <asp:Button Text="Details Select Exam" CssClass="greenBtn" ID="btnDetailsMarks" runat="server" Width="154px" OnClick="btnDetailsMarks_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 56px"></td>
                            <td>
                                <asp:Button Text="Total Result Process" CssClass="greenBtn" ID="btnTotalResultProcess" runat="server" Width="308px" OnClick="btnTotalResultProcess_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center; font-size: 20px">
                        <p runat="server" id="P1" visible="false"></p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="width: 100%; height: 0 auto; overflow-x: auto; overflow-y: auto; float: left;">
            <div style="width: auto">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview" CssClass="greenBtn" Width="120px" OnClick="btnPrintPreview_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnTotalResultProcess" />
                </Triggers>
                <ContentTemplate>
                    <div runat="server" id="divDisplayFinalResult">
                        <asp:Label ID="lblClassTitle" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                        <asp:Label ID="lblShiftTitle" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                        <asp:GridView ID="gvDisplayTotalFinalResult" ClientIDMode="Static" CssClass="display" runat="server"></asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
            <ContentTemplate>
                <br />
                <div id="divMarksheet" runat="server" style="width: 100%; height: 0 auto; overflow-x: auto; overflow-y: auto; float: left;" visible="false">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">    
    <script type="text/javascript">
        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
               
            });
        }
    </script>
</asp:Content>
