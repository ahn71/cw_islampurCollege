<%@ Page Title="যোগাযোগ" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="DS.UI.DSWS.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: auto;
        }
    </style>
    <script type="text/javascript">        
        function goToNewTab(url) {
            window.open(url);
        }
        function validateDropDown() {
            if (validateText('txtName', 1, 30, 'Enter Name') == false) return false;
            if (validateText('txtEmail', 1, 70, 'Enter Email') == false) return false;
            if (validateText('txtComments', 1, 300, 'Enter Comments') == false) return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">

    <div class="container">
        <div class="row">
            <div runat="server" id="divBoardDirContacts" class="main-content">

                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <div class="row">
                                <div style="" class="col-md-8">
                                    <div class="panel panel-default cform-panel">
                                        <div class="panel-heading">
                                            <h4>
                                            Submit your Query</h4></div>
                                        <div class="panel-body bg-info">
                                            <div class="form-group">
                                                <label class="exampleInputEmail1">নাম *</label>
                                                <asp:TextBox ID="txtName" placeholder="Name" runat="server" CssClass="form-control input controlLength"
                                                    ClientIDMode="Static">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="exampleInputEmail1">ই- মেইল *</label>
                                                <asp:TextBox ID="txtEmail" placeholder="E-mail" runat="server" CssClass="form-control input controlLength"
                                                    ClientIDMode="Static">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="exampleInputEmail1">ফোন নাম্বার</label>
                                                <asp:TextBox ID="txtPhoneNumber" placeholder="Phone Number" runat="server" CssClass="form-control input controlLength"
                                                    ClientIDMode="Static">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="exampleInputEmail1">আপনার মতামত *</label>
                                                <asp:TextBox ID="txtComments" placeholder="Comments" TextMode="MultiLine" runat="server" CssClass="form-control input controlLength"
                                                    ClientIDMode="Static">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSend" CssClass="btn btn-primary littleMargin" Text="পাঠান"
                                                    OnClientClick="return validateDropDown();" OnClick="btnSend_Click" ClientIDMode="Static" runat="server" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="c-address-box bg-info">
                                        <iframe runat="server" id="iframeGoogleMap"  width="100%" height="350" frameborder="0" style="border:0" allowfullscreen></iframe>
                                        <ul style="padding: 10px 5px 15px 20px;">
                                            <li><strong>বর্তমান ঠিকানাঃ</strong><p runat="server" id="pAddress">   
                                            </p>
                                            </li>
                                            <li><strong>Web </strong><a runat="server" id="aWeb"></a></li>
                                            <li><strong>Phone: </strong><label runat="server" id="lblPhone"></label>
                                            </li>
                                            <li><strong>Email:</strong> <label runat="server" id="lblEmail"></label></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
