<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportExcel.aspx.cs" Inherits="DS.UI.ExportExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
               <asp:FileUpload ID="exlFileUpload" runat="server"/>
               <asp:Button runat="server" ID="btnImportExcel" Text="Import" CssClass="btn btn-success" OnClick="btnImportExcel_Click" />
            </div>
   
              
        
            
        </div>
    </form>
</body>
</html>
