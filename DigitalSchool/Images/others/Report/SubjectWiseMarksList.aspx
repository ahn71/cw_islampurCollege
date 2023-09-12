<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectWiseMarksList.aspx.cs" Inherits="DS.Report.SubjectWiseMarksList" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Subject Wise Marks List Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <div id="divTitelCertainMarkList" style="margin-top: 50px" runat="server">
                    <asp:Label ID="lblSchoolTitle" runat="server" Text="DIGITAL SCHOOL" Font-Bold="True"></asp:Label><br />
                    <asp:Label ID="examTitle" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                    <asp:Label ID="subjectTitle" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                </div>
                <div style="width:960px; overflow:auto">         
                    <div id="divTitleDetailsMarkList" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="DIGITAL SCHOOL" Font-Bold="True"></asp:Label><br />
                        <asp:Label ID="lblTitelExam" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                    </div>
            
                    <br />
                    <asp:GridView ID="gvMarkList" runat="server" Width="277px"></asp:GridView>
                    <asp:GridView ID="gvDisplayTotalFinalResult" ClientIDMode="Static" CssClass="display" runat="server"></asp:GridView>
                    <div id="divMarkListReport" runat="server" style="height: auto; border: 1px solid black;"></div>
                    <div id="divButton" style="position: fixed; padding: 5px; top: 0; left: 0; margin-top: 5px;">
                        <img alt="" src="/images/action/print.png" onclick="printCall()" />
                    </div>
                </div>                
            </center>
        </div>
    </form>
    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';
        }
        function goBack() {
            window.location = '/Forms/MarksEntry.aspx';
        }
    </script>
</body>
</html>
