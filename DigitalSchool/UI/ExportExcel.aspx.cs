using DS.DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI
{
    public partial class ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (exlFileUpload.HasFile)
            {
                try
                {
                    string filePath = Server.MapPath("~/App_Data/") + exlFileUpload.FileName;
                    exlFileUpload.SaveAs(filePath);

                    List<ExcelData> excelDataList = ReadExcelFile(filePath);
                    UpdateDatabase(excelDataList);

                  
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                
            }
        }

        private List<ExcelData> ReadExcelFile(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelDataList = new List<ExcelData>();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var studentId = int.Parse(worksheet.Cells[row, 1].Text);
                    var registrationNumber = worksheet.Cells[row, 2].Text;

                    excelDataList.Add(new ExcelData
                    {
                        StudentId = studentId,
                        RegistrationNumber = registrationNumber
                    });
                }
            }

            return excelDataList;
        }

        private void UpdateDatabase(List<ExcelData> excelDataList)
        {
            foreach (var excelData in excelDataList)
            {
                string query = "UPDATE currentStudentInfo SET RegistrationNo =" + excelData.RegistrationNumber + " WHERE StudentId = " + excelData.StudentId + "";
                CRUD.ExecuteNonQuery(query);
            }


        }

        public class ExcelData
        {
            public int StudentId { get; set; }
            public string RegistrationNumber { get; set; }
        }
    }
}