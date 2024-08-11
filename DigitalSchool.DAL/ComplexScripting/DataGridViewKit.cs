using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class DataGridViewKit
    {
        public static void alignmentCenter(bool withHeader, string column, DataGridView dataGridViewName)
        {
            if (withHeader)
                dataGridViewName.Columns[column].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewName.Columns[column].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public static void columnSize(int size, string column, DataGridView dataGridViewName)
        {
            dataGridViewName.Columns[column].Width = size;
        }

        public static void fontSize(bool isBold, int size, string column, DataGridView dataGridViewName)
        {
            if (isBold)
                dataGridViewName.Columns[column].DefaultCellStyle.Font = new Font(dataGridViewName.Font.FontFamily, (float)size, FontStyle.Bold);
            else
                dataGridViewName.Columns[column].DefaultCellStyle.Font = new Font(dataGridViewName.Font.FontFamily, (float)size, FontStyle.Bold);
        }
    }
}
