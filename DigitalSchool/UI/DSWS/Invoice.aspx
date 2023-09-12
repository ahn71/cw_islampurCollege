<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="DS.UI.DSWS.Invoice" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" />
</head>
<body style="background: #ededed">
   <main style="width: 800px; margin: 0 auto; padding: 10px;background-color: #fff;" id="printtable">
        <div class="payment-info-section">
            <header class="payment-header" style="margin: 5px 0;padding: 10px;">
                <!-- <div class="text-center">
                <h3 style="font-size: 24px">Payment Information</h3>
            </div> -->
                <table style="width: 100%">
                    <tr>
                        <td style="width: 120px">
                            <img style="height: 100px;" src="../../websitedesign/assets/images/logo.png" />
                            <%--<img style="height: 100px;" src="" alt="">--%>
                        </td>
                        <td style="padding-left: 10px;">
                            <h2 style="margin: 0;font-size: 24px;text-transform: uppercase;font-weight: 600;color: #000;">Govt. Islampur
                                College</h2>
                            <p style="margin-bottom: 0;font-size: 18px;">Islampur, Jamalpur</p>
                        </td>
                        <td style="text-align: right">
                            <h3 style="color: #2040fa;font-size: 18px;">Invoice No: <asp:Label runat="server" ID="lblInvoiceNo"
                                                    ClientIDMode="Static"></asp:Label> (<span
                                    style="color: #153df1;">PAID</span>)
                            </h3>
                            <p><strong>Date of Payment:</strong> <asp:Label runat="server" ID="lblDateOfPayment"
                                                    ClientIDMode="Static"></asp:Label></p>
                             <p runat="server" id="pOpenPayment" visible="false" style="color: #34900f;font-size: 24px; font-weight: 600">Open Payment</p> 

                        </td>
                    </tr>

                </table>
            </header>

            <section>
                <table class="table table-bordered">
                        <tr>
                            <th style="width: 120px;"><asp:Label runat="server" ID="lblAdmissionNoHead"
                                                    ClientIDMode="Static"></asp:Label></th>
                            <td style="width: 5px;">:</td>
                            <td><asp:Label runat="server" ID="lblAdmissionNo"
                                                    ClientIDMode="Static"></asp:Label></td>
                            <th style="width: 60px;">Name</th>
                            <td style="width: 5px;">:</td>
                            <td><asp:Label runat="server" ID="lblStudentName"
                                                    ClientIDMode="Static"></asp:Label></td>
                            <th style="width: 120px;">Class</th>
                            <td style="width: 5px;">:</td>
                            <td><asp:Label runat="server" ID="lblClass"
                                                    ClientIDMode="Static"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="width: 60px;">Group</th>
                            <td style="width: 5px;">:</td>
                            <td><asp:Label runat="server" ID="lblGroup"
                                                    ClientIDMode="Static"></asp:Label></td>
                            <th style="width: 60px;">Session</th>
                            <td style="width: 5px;">:</td>
                            <td><asp:Label runat="server" ID="lblYear"
                                                    ClientIDMode="Static"></asp:Label></td>
                            <th style="width: 120px;">Class Roll</th>
                            <td style="width: 5px;">:</td>
                            <td><asp:Label runat="server" ID="lblClassRoll"
                                                    ClientIDMode="Static"></asp:Label></td>
                        </tr>
                </table>
            </section>
            <section>
                <div runat="server" id="divParticularCategoryList">

                   <%-- <div style="margin-bottom: 10px;">
                        <strong>Fee Category :  </strong>DDFSDf
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th class="per70 text-center">Description</th>
                                <th class="per25 text-right" style="width: 30%;">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Examination Fee</td>
                                <td class="text-right">25.00 </td>
                            </tr>
                            <tr>
                                <td>Monthly Fee</td>
                                <td class="text-right">25.00 </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th class="text-right">Sub Total</th>
                                <th class="text-right">237.00 Tk</th>
                            </tr>
                            <tr>
                                <th class="text-right">Discount</th>
                                <th class="text-right">47.40 TK</th>
                            </tr>
                            <tr>
                                <th class="text-right">Online Charge (1.20 %)</th>
                                <th class="text-right">15.50 TK</th>
                            </tr>
                            <tr>
                                <th class="text-right">Total</th>
                                <th class="text-right">284.90 TK</th>
                            </tr>
                        </tfoot>
                    </table>--%>
                </div>
               
            </section>
        </div>
    </main>
    <div style="text-align: center; margin-top: 20px">
        <a style="text-decoration:none" href="javascript:void(0)" onclick="PortraitPrintHTML()" class="btn-lg btn-primary"> Print Invoice </a>
    </div>
    <script src="extensions/print/bootstrap-table-print.js"></script>
    
    <script>
    function PortraitPrintHTML() {

      var mywindow = window.open('', 'PRINT', 'height=400,width=600');
      mywindow.document.write('<html><head><title></title>');
      mywindow.document.write('<link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" rel="stylesheet">');
      mywindow.document.write('<style>');
      mywindow.document.write('@media print{');
      mywindow.document.write('@page {size:auto;-webkit-print-color-adjust: exact;}');
      mywindow.document.write('table {width:100%}');
      mywindow.document.write('.table-bordered th{vertical-align: middle!important;background: #ededed!important}');
      mywindow.document.write('.table-bordered th, .table-bordered td {border: 1px solid #000 !important;font-size:16px!important;color:#000!important}');
      mywindow.document.write('.print-report-header table td {font-size:18px!important;font-weight:500!important;}');
      mywindow.document.write('.table-footer td {background: #ededed!important}');
      mywindow.document.write('}');
      mywindow.document.write('</style>');
      mywindow.document.write('</head><body style="background-color: #fff !important;" >');
      mywindow.document.write(document.getElementById("printtable").innerHTML);
      mywindow.document.write('</body></html>');
      mywindow.document.close(); // necessary for IE >= 10
      mywindow.focus(); // necessary for IE >= 10*/

      setTimeout(function () {
        mywindow.print();
        var ival = setInterval(function () {
          mywindow.close();
          clearInterval(ival);
        }, 200);
      }, 500);
      return true;
    }
  </script>
  <!-- BRMS Report Print Kamrul Hasan Rijon-->
</body>
</html>
