<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllReport.aspx.cs" Inherits="DS.Forms.AllReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnStudentList" />
            <asp:AsyncPostBackTrigger ControlID="btnStudentContactList" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardianInformation" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardianContractList" />
            <asp:AsyncPostBackTrigger ControlID="btnParentsInformation" />
            <%-- <asp:AsyncPostBackTrigger ControlID="btnAdmitCard" />--%>
        </Triggers>
        <ContentTemplate>
            <asp:DropDownList ID="dlYear" runat="server" CssClass="dropDownListRoutine"></asp:DropDownList>
            <div style="border: 1px solid gray; padding: 15px; margin-top: 10px;">

                <asp:Button ID="btnStudentList" runat="server" Text="Student List" CssClass="greenBtn" OnClick="btnStudentList_Click" />
                <asp:Button ID="btnStudentContactList" runat="server" Text="Student Contact List" CssClass="greenBtn" OnClick="btnStudentContactList_Click" />
                <asp:Button ID="btnGuardianInformation" runat="server" Text="Guardian Information" CssClass="greenBtn" OnClick="btnGuardianInformation_Click" />
                <asp:Button ID="btnGuardianContractList" runat="server" Text="Guardian Contract List" CssClass="greenBtn" OnClick="btnGuardianContractList_Click" />
                <asp:Button ID="btnParentsInformation" runat="server" Text="Parents Information" CssClass="greenBtn" OnClick="btnParentsInformation_Click" />
                <%--  <asp:Button ID="btnAdmitCard" runat="server" Text="Admit Card" CssClass="greenBtn"/>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

