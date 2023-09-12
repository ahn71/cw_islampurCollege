<%@ Page EnableEventValidation="false" Title="Guide Student list" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="GuideStudentList.aspx.cs" Inherits="DS.UI.Adviser.GuideStudentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <style>
        .tgPanel {
            width: 100%;
        }

        input[type="checkbox"] {
            margin: 5px;
        }

        .table tr th {
            background-color: #23282C;
            color: white;
        }
        .table tr{
           height:60px;
        }

        .tbl-controlPanel th:nth-child(1),
        .tbl-controlPanel th:nth-child(3),
        .tbl-controlPanel th:nth-child(4),
         .tbl-controlPanel th:nth-child(5), 
        .tbl-controlPanel th:nth-child(6) ,
         .tbl-controlPanel th:nth-child(7), 
        .tbl-controlPanel th:nth-child(8),
         .tbl-controlPanel th:nth-child(9){
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
   <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                 <li><a id="A2" runat="server" href="~/UI/Adviser/AdviserHome.aspx">Adviser Home</a></li>               
                <li class="active">Guide Student</li>
            </ul>
            <!--breadcrumbs end -->
        </div>   
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <Triggers>

       </Triggers>
        <ContentTemplate>
    <div class="col-lg-12">    
         <div id="showParticular" runat="server" style="display: block; width: 100%; height: 100% auto; background-color: white; top: 60px">
                        <div style="background-color: #23282C; color: white">
                            <h3>Student list</h3>
                        </div>

                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Width="100%" Height="100%">
                            <asp:GridView ID="gvStudentList" runat="server" Width="100%" DataKeyNames="StudentId" OnRowCommand="gvStudentList_RowCommand" CssClass="table tbl-controlPanel" AutoGenerateColumns="False" >

                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="StudentId" runat="server"
                                                Value='<%# DataBinder.Eval(Container.DataItem, "StudentId")%>' />
                                            <%# Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FullName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStdName" Style="float: left" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batch">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBatch" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "BatchName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShift" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ShiftName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroup" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "GroupName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsection" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "SectionName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Roll">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoll" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField >
                                        <HeaderTemplate >
                                            Result
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="Result" CssClass="btn btn-primary" ClientIDMode="Static" ID="btnResult" Text="Result"  CommandArgument='<%#Eval("StudentId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField >
                                        <HeaderTemplate >
                                            Attendance
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="Attendance" CssClass="btn btn-primary" ClientIDMode="Static" ID="btnAttendance" Text="Attendance"  CommandArgument='<%#Eval("StudentId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>     

                                </Columns>
                            </asp:GridView>
                            <%--  </div>--%>
                        </asp:Panel>

                    </div>
        </div>
            </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
