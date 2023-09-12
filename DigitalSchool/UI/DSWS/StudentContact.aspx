<%@ Page Title="ছাত্র/ছাত্রী পরিচিতি" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="StudentContact.aspx.cs" Inherits="DS.UI.DSWS.StudentContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main-content-inner {
            float: left;
            min-height: 468px;
            min-width: 70%;
        }

        .img-responsive {
            border: 1px solid #ddd;
            height: 130px;
            margin: 5px auto 0;
            padding: 4px;
        }

        .tblStudentInfo {
            font-family: 'Times New Roman';
            /*margin-left:10px;*/
            margin: 0px auto;
            /*min-width:50%;*/
            width: 60%;
        }

            .tblStudentInfo tr {
                border: 1px solid #ddd
            }

                .tblStudentInfo tr:nth-child(even) {
                    background: #fafafa;
                }

                .tblStudentInfo tr td {
                    padding: 5px;
                }

                    .tblStudentInfo tr td:first-child {
                        font-weight: bold;
                        color: #1CA59E;
                    }

        .divStdInfo {
            width: 100%;
            margin: 0px auto;
            /*border:1px solid silver;*/
        }

        .tblStudentInfo tr td:nth-child(4n) {
            font-weight: bold;
            color: #1CA59E;
        }

        .divStdInfo {
            width: 100%;
            margin: 0px auto;
            /*border:1px solid silver;*/
        }

        .divHeader {
            min-height: 100px;
            text-align: center;
        }

        .divTable {
            border: 1px solid red;
            width: auto;
            /*margin:0px auto;*/
        }

        .title {
            color: #1fb5ad;
            /*margin-left:10px;*/
            text-align: center;
        }

        .hMsg {
            color: red;
            text-align: center;
        }
        @media only screen and (max-width: 600px) {
            .tblStudentInfo {
                width: 100%;
                margin: auto;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="btnShow" />--%>
            </Triggers>
            <ContentTemplate>
                <div runat="server" class="main-content">

                    <!-- ================Start New Design Code ================ -->
                    <section>
                        <div class="container">
                            <div style="padding: 50px 0px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="main-content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="student-info-page-title text-center">
                                                        <h4 style="font-size: 24px; color: green;">শিক্ষার্থীদের তথ্য</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4>তথ্য সার্চ করুন </h4>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-2"></div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Session Year</label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlYear" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Class</label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlClass" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">SSC Roll/Admission No</label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox runat="server" ID="txtAdmNo" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">&nbsp;</label>
                                                                        <div class="col-md-12">
                                                                            <asp:Button runat="server" Text="দেখুন" ClientIDMode="Static" ID="Button1" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <h4 runat="server" id="hMessage" visible="false" class='hMsg'>Student Not Found</h4>
                                                <div runat="server" id="divStudentContacts" class="divStdInfo">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div  class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4>শিক্ষার্থীর তথ্য</h4>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div runat="server" id="ForLeftSideMenuList_divStudentContacts" class="divStdInfo">
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                    <!-- ================End New Design Code ================ -->


                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
