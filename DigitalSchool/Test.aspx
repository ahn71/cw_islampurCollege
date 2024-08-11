<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DS.Test" %>

<!DOCTYPE html>

<html>
<head>
    <title>Test Page</title>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <asp:Button ID="btnConfirm" runat="server" Text = "Raise Confirm" OnClientClick = "Confirm()"/>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:Button ID="btnPDF" runat="server" Text="Pdf" OnClick="btnPDF_Click" />

        
        <asp:LinkButton ID="lnkView" runat="server" Text="View File" OnClick="View" CommandArgument="MyFile"></asp:LinkButton>

        <div id="divReport" runat="server">
            test create pdf
        </div>
    </form>
</body>
</html>