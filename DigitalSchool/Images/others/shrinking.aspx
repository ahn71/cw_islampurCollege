<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="shrinking.aspx.cs" Inherits="DS.others.shrinking" %>
<%@ Register Assembly="ComplexScriptingWebControl" Namespace="ComplexScriptingWebControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc1:ProgressBar runat="server" ID="ProgressBar1" />

    <script type="text/javascript" >
        function CloseWindow() {
            window.close();
        }
    </script>
</asp:Content>
