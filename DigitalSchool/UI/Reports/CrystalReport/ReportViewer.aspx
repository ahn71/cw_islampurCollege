<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="DS.UI.Reports.CrystalReport.ReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html>
    <head>
    <title>Reports</title>
        <script lang="javaScript" type="text/javascript" src="crystalreportviewers13/js/crviewer/crv.js"></script>
</head>
<body>
    <div style="width:100%; margin:0 auto">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
            <br />
        </form>
    </div>
</body>
</html>
