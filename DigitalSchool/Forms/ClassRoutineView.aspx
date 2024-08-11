<%@ Page Title="Class Routine" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassRoutineView.aspx.cs" Inherits="DS.Forms.ClassRoutineView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="routine">
        <div style="text-align: center; border-bottom: 1px solid #D2D2D2; padding: 10px;">
            <asp:UpdatePanel runat="server" ID="upPrint">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="dlClass" ClientIDMode="Static" Width="220px" CssClass="input"
                        OnSelectedIndexChanged="dlClass_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Button runat="server" OnClick="btnPrint_Click" ID="btnPrint" Text="Print Preview" Width="120px"
                        Style="height: 30px; margin-top: -8px; width: 120px; padding: 0 !important;" CssClass="greenBtn" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlClass" />
            </Triggers>
            <ContentTemplate>
                <div id="divRoutineInfo" style="width: 100%" runat="server">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

