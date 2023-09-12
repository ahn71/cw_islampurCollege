<%@ Page Title="Teacher Partial Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherPartialInfo.aspx.cs" Inherits="DigitalSchool.Forms.TeacherPartialInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ScriptManager runat="server" ID="scriptmanager"></asp:ScriptManager>

<asp:UpdatePanel ID="uplMessage" runat="server">
<ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>


    <center><h2>Employee Info</h2></center>

<div class="widget">
    
<div class="head" >
<img src="/Images/master/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
  
<div class="dataTables_filter" style="float:right;">
    <label>Search: <input type="text" class="search" placeholder="type here..."/>
        <div class=""></div>
    </label>
</div>
</div>

</div>


 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divTeacherInfo" class="datatables_wrapper" runat="server" style=" width:100%; height:auto"></div>
    </ContentTemplate>
 </asp:UpdatePanel>






<script src="/Scripts/adviitJS.js"></script>
<script type="text/javascript" >

    $(document).ready(function () {


        $(document).on("keyup", '.search', function () {
            searchTable($(this).val(), 'tblTeacherInfo', '');
        });

    });


    function editTeacher(teacherId) {
        goURL('/Forms/EmployeeInformation.aspx?TeacherId=' + teacherId + "&Edit=True");
    }

    function viewTeacher(teacherId) {
        goURL('/Forms/EmployeeView.aspx?TeacherId=' + teacherId);
    }

    function setSalary(teacherId) {
        goURL('/Forms/SetSalary.aspx?TeacherId=' + teacherId + "&Save=True");
    }

</script>



</asp:Content>
