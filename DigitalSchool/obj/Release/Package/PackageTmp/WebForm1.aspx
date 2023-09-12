<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DigitalSchool.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script type="text/javascript">

        function ifSubmited() {
            return false;
        }

        $(document).ready(function(){

        }

    </script>
<body>
    <form id="form1" runat="server" onsubmit="return ifSubmited()">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
