<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="ClassRoutine.aspx.cs" Inherits="DS.UI.Adviser.ClassRoutine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a id="A1" runat="server" href="~/UI/Adviser/AdviserHome.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>           
               
                <li class="active">Class Routine</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

    <div  class="routine">
<asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
    <Triggers>
       
    </Triggers>
    <ContentTemplate>
        <div id="divRoutineInfo" style="width:100%" runat="server" >
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
    <div >
      </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
