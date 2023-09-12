<%@ Page Title="Student Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmissionDetails.aspx.cs" Inherits="DigitalSchool.Forms.AdmissionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/gridview.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ScriptManager runat="server" ID="scriptmanager"></asp:ScriptManager>
<asp:UpdatePanel ID="uplMessage" runat="server">
<ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>
    



<div class="tgInput" style="width:140px;margin:20px auto;">
      
<div>
<div>Class</div>
<div>
    <asp:DropDownList ID="dlClass" style="width:120px;" runat="server" ClientIDMode="Static" AutoPostBack="false"  ></asp:DropDownList>
</div>
</div>

<div style="margin-top:10px;">
<div>Section</div>
<div>
 <asp:DropDownList ID="dlSection" style="width:120px;"  runat="server" ClientIDMode="Static" AutoPostBack="false"  ></asp:DropDownList>

</div>
</div>

<div style="margin-top:30px;">
    <img style="width:60px;height:26px;cursor:pointer;" src="/images/action/search.gif" onclick="$('#btnSearch').click();" />
    <asp:Button ID="btnSearch" style="display:none;" Text="Search"  ClientIDMode="Static" runat="server" OnClick="btnSearch_Click"/>
</div>
</div>
             

    


<div class="widget">

<div class="head" >
<img src="/Images/master/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
<div class="dataTables_filter" style="float:right;">
    <label>Search: <input type="text" class="search" placeholder="type here..."/>
        <div class=""></div>
    </label>
</div>
</div>

 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <div id="divStudentDetails" class="datatables_wrapper" runat="server" style=" width:100%; height:auto"></div>
    </ContentTemplate>
 </asp:UpdatePanel>

</div>
    


<script src="/Scripts/adviitJS.js"></script>
<script type="text/javascript" >

$(document).ready(function () {


    $(document).on("keyup",'.search', function () {
        searchTable($(this).val(), 'tblStudentInfo', '');
    });

});


function editStudent(studentId)
{
    goURL('/Forms/Admission.aspx?StudentId=' + studentId +"&Edit=True");
}

function viewStudent(studentId)
{
    goURL('/Forms/StudentView.aspx?StudentId=' + studentId );
}

function onMouseOver(rowIndex) {
    var gv = document.getElementById("gvAdmissionDetails");
    var rowElement = gv.rows[rowIndex];
    rowElement.style.backgroundColor = "#c8e4b6";
    //rowElement.cells[1].style.backgroundColor = "green";
}

function onMouseOut(rowIndex) {
    var gv = document.getElementById("gvAdmissionDetails");
    var rowElement = gv.rows[rowIndex];
    rowElement.style.backgroundColor = "#fff";
    //rowElement.cells[1].style.backgroundColor = "#fff";
}

</script>


</asp:Content>
