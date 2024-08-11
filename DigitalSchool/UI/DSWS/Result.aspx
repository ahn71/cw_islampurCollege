<%@ Page Title="ফলাফল" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="DS.UI.DSWS.Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .controlLength {
            width: 120px;
            margin: 5px;
        }
        .tgInput td:first-child, 
        .tgInput td:nth-child(3), 
        .tgInput td:nth-child(5), 
        .tgInput td:nth-child(4) {
            padding: 0px;
            width: 20px;    
        }     
        .tgPanel {
            width: 100%;
        }        
        .littleMargin {
            margin-right: 5px;
        }
        #btnPrintPreview{
            margin: 3px;
        }
         
        .std_result_show {
          margin: 0 auto;
          width: 40%;
        }
        .std_result_show tr{
             border:1px solid #ddd
        }
        .std_result_show tr:nth-child(even) {
            background:#fafafa;
        }
        .std_result_show tr td{
           padding:5px !important;
        }
        .std_result_show tr td:first-child{
           font-weight:bold;
           color:#1CA59E;
        }
         
    </style>
    <script type="text/javascript">
        function searchAgain() {
            $('#txtRollNo').val('');
            $('#txtRollNo').focus();
            $("#ForLeftSideMenuList_divResultView").empty();

        }
        function printDiv() {

            var divToPrint = document.getElementById('page-wrapper1');

            var newWin = window.open('', 'Print-Window');
            newWin.document.open();
            newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 10);

        }
        function goToNewTab(url) {
            window.open(url);            
        }        
        function validateDropDown() {           
            if (validateCombo('ddlYear', 0, 'Select Year') == false) return false;
            if (validateCombo('ddlBatch', 0, 'Select Class Name') == false) return false;
            if (validateCombo('ddlExamId', 0, 'Select Exam Id') == false) return false;
            if (validateText('txtRollNo', 1, 30, 'Type Roll No') == false) return false;
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
    <section class="template-section">
    <div class="container">
    <div class="row">
         <div runat="server" id="divBoardDirContacts" class="main-content">
        <div class="tgPanel">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="btnResult" />

                </Triggers>
                <ContentTemplate>
                    <div class="result-top-box">
                             <div class="row">
                                 <div class="col-md-2 col-xs-2">
                                      <img class="img-responsive" src="/websitedesign/assets/images/logo.png"  />
                                 </div>
                                 <div class="col-md-10 col-xs-10">
                                     <h4 class="title-4">Islampur college WEB BASED RESULT PUBLICATION SYSTEM</h4>
                                 </div>
                             </div>
                         </div>
                    <div class="panel panel-default cform-panel">
                         
                         <div class="panel-heading"><h4>Please provide your Exam and corresponding Info </h4></div>
                            <div class="panel-body">
                                <div class="" style="">
                                  <div class="form-group row">
                                    <label class="col-md-4">Year</label>
                                      <div class="col-md-8">
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true">
                                        </asp:DropDownList>
                                        </div>
                                  </div>
                                    <div class="form-group row">
                                    <label class="col-md-4">Class</label>
                                      <div class="col-md-8">
                                        <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true">
                                        </asp:DropDownList>
                                        </div>
                                  </div>
                                  <div class="form-group row">
                                    <label class="col-md-4">Exam Name</label>
                                      <div class="col-md-8">
                                            <asp:DropDownList ID="ddlExamId" runat="server"  ClientIDMode="Static"
                                                CssClass="form-control">
                                            </asp:DropDownList>
                                       </div>
                                  </div>
                                  <div class="form-group row">
                                    <label class="col-md-4">Roll No</label>
                                      <div class="col-md-8">
                                        <asp:TextBox ID="txtRollNo" placeholder="Enter your roll no" runat="server" CssClass="form-control"
                                            ClientIDMode="Static">
                                        </asp:TextBox>
                                      </div>
                                  </div>
                                    <div class="form-group row">
                                    <label class="col-md-4"></label>
                                      <div class="col-md-8">
                                        <asp:CheckBox runat="server" ID="ckbWithMarks" Text="With Marks" Checked="false" />
                                      </div>
                                  </div>
                                  <div class="form-group row">
                                      <div class="col-md-12">
                                          <div class="pull-right">                                            
                                              <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-primary littleMargin" ID="btnResult" Text="Get Result" OnClientClick="return validateDropDown();" OnClick="btnResult_Click" />                        
                                          </div>
                                      </div>
                                    </div>
                                </div>
                            </div>
                        </div>           
                   <%--  Result View  --%>
                    <div runat="server" id="divResultView" class="panel panel-default result-view-panel">                    
                    </div>
                   <%--  Result View  --%>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
             </div>
             </div>
    </div>
        </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
