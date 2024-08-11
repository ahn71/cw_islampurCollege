using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class SQLOperation
    {
        public static SqlConnection connection = new SqlConnection();
        public static SqlDataAdapter da;
        public static SqlDataReader dr;
        public static SqlCommand cmd;

        public static void ConnectionWithServer(string serverName, string databaseName)
        {
            try
            {
                SQLOperation.connection.ConnectionString = "Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Integrated Security=True";
                SQLOperation.connection.Open();
            }
            catch
            {
                SQLOperation.connection.Close();
            }
        }

        public static void ConnectionWithServer(string serverName, string databaseName, string userId, string userPassword, int timeOutTime)
        {
            try
            {
                SQLOperation.connection.ConnectionString = "User Id=" + (object)userId + ";Password=" + userPassword + ";Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Connection Timeout=" + (string)(object)timeOutTime;
                SQLOperation.connection.Open();
            }
            catch
            {
                SQLOperation.connection.Close();
            }
        }

        public static bool forSaveValue(string tableName, string[] setColumns, string[] setValues, SqlConnection setConnection)
        {
            if (setConnection.State.ToString() == "Closed") setConnection.Open();
            byte num = (byte)0;
            string str1 = "";
            string str2 = "";
            for (; (int)num < setColumns.Length; ++num)
            {
                if ((int)num == setColumns.Length - 1)
                {
                    str2 = str2 + "@" + setColumns[(int)num];
                    str1 = str1 + setColumns[(int)num];
                }
                else
                {
                    str2 = str2 + "@" + setColumns[(int)num] + ",";
                    str1 = str1 + setColumns[(int)num] + ",";
                }
            }
            SQLOperation.cmd = new SqlCommand("insert into " + tableName + " (" + str1 + ") values (" + str2 + ")", setConnection);
            for (byte index = (byte)0; (int)index < setColumns.Length; ++index)
                SQLOperation.cmd.Parameters.AddWithValue("@" + setColumns[(int)index], (object)setValues[(int)index]);
            SQLOperation.cmd.ExecuteNonQuery();
            setConnection.Close();
            return true;
        }

        public static bool forSaveValue(string tableName, string[] setColumns, string[] setValues, string forImage, SqlConnection setConnection)
        {
            if (setConnection.State.ToString() == "Closed") setConnection.Open();
            byte num1 = (byte)0;
            string str1 = "";
            string str2 = "";
            string str3 = "";
            byte num2 = (byte)0;
            PictureBox pictureBox = new PictureBox();
            byte[] buffer = new byte[1]
      {
        (byte) 1
      };
            for (; (int)num1 < setColumns.Length; ++num1)
            {
                if (setValues[(int)num1].ToLower().Contains(".png") || setValues[(int)num1].ToLower().ToLower().Contains(".jpg") || (setValues[(int)num1].ToLower().ToLower().Contains(".gif") || setValues[(int)num1].ToLower().ToLower().Contains(".bmp")) || setValues[(int)num1].ToLower().Contains(".jpeg") || setValues[(int)num1].ToLower().Contains(".ico"))
                {
                    FileStream fileStream = new FileStream(setValues[(int)num1], FileMode.OpenOrCreate, FileAccess.Read);
                    buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, Convert.ToInt32(fileStream.Length));
                    setValues[(int)num1] = buffer.ToString();
                    str3 = setColumns[(int)num1];
                    num2 = num1;
                }
                if ((int)num1 == setColumns.Length - 1)
                {
                    str2 = str2 + "@" + setColumns[(int)num1];
                    str1 = str1 + setColumns[(int)num1];
                }
                else
                {
                    str2 = str2 + "@" + setColumns[(int)num1] + ",";
                    str1 = str1 + setColumns[(int)num1] + ",";
                }
            }
            SQLOperation.cmd = new SqlCommand("insert into " + tableName + " (" + str1 + ") values (" + str2 + ")", setConnection);
            for (byte index = (byte)0; (int)index < setColumns.Length; ++index)
            {
                if (num2.Equals(index))
                    SQLOperation.cmd.Parameters.AddWithValue("@" + str3, (object)buffer);
                else
                    SQLOperation.cmd.Parameters.AddWithValue("@" + setColumns[(int)index], (object)setValues[(int)index]);
            }
            SQLOperation.cmd.ExecuteNonQuery();
            setConnection.Close();
            return true;
        }

        public static bool forUpdateValue(string tableName, string[] setColumns, string[] setValues, string identifierName, string identifierValue, SqlConnection setConnection)
        {
            if (setConnection.State.ToString() == "Closed") setConnection.Open();
            byte num = (byte)0;
            string str = "";
            for (; (int)num < setColumns.Length; ++num)
            {
                if ((int)num == setColumns.Length - 1)
                    str = str + setColumns[(int)num] + "=@" + setColumns[(int)num];
                else
                    str = str + setColumns[(int)num] + "=@" + setColumns[(int)num] + ",";
            }
            SQLOperation.cmd = new SqlCommand("Update " + tableName + " set " + str + " where " + identifierName + "=@" + identifierName, setConnection);
            for (byte index = (byte)0; (int)index < setColumns.Length; ++index)
                SQLOperation.cmd.Parameters.AddWithValue("@" + setColumns[(int)index], (object)setValues[(int)index]);
            SQLOperation.cmd.Parameters.AddWithValue("@" + identifierName, (object)identifierValue);
            SQLOperation.cmd.ExecuteNonQuery();
            setConnection.Close();
            return true;
        }

        public static bool forUpdateValue(string tableName, string[] setColumns, string[] setValues, string identifierName, string identifierValue, string OldImage, SqlConnection setConnection)
        {
            byte num1 = (byte)0;
            string str1 = "";
            string str2 = "";
            string str3 = "";
            PictureBox pictureBox = new PictureBox();
            byte[] buffer = new byte[1]
      {
        (byte) 1
      };
            byte num2 = (byte)0;
            for (; (int)num1 < setColumns.Length; ++num1)
            {
                if (setValues[(int)num1].ToLower().Contains(".png") || setValues[(int)num1].ToLower().ToLower().Contains(".jpg") || (setValues[(int)num1].ToLower().ToLower().Contains(".gif") || setValues[(int)num1].ToLower().ToLower().Contains(".bmp")) || setValues[(int)num1].ToLower().Contains(".jpeg") || setValues[(int)num1].ToLower().Contains(".ico"))
                {
                    FileStream fileStream = new FileStream(setValues[(int)num1], FileMode.OpenOrCreate, FileAccess.Read);
                    str3 = Path.GetFileName(setValues[(int)num1]);
                    buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, Convert.ToInt32(fileStream.Length));
                    setValues[(int)num1] = buffer.ToString();
                    str2 = setColumns[(int)num1];
                    num2 = num1;
                }
                else if ((int)num1 == setColumns.Length - 1)
                    str1 = str1 + setColumns[(int)num1] + "=@" + setColumns[(int)num1];
                else
                    str1 = str1 + setColumns[(int)num1] + "=@" + setColumns[(int)num1] + ",";
            }
            SQLOperation.cmd = new SqlCommand("Update " + tableName + " set " + str1 + " where " + identifierName + "=@" + identifierName, setConnection);
            for (byte index = (byte)0; (int)index < setColumns.Length; ++index)
            {
                if (!num2.Equals(index))
                    SQLOperation.cmd.Parameters.AddWithValue("@" + setColumns[(int)index], (object)setValues[(int)index]);
            }
            SQLOperation.cmd.Parameters.AddWithValue("@" + identifierName, (object)identifierValue);
            SQLOperation.cmd.ExecuteNonQuery();
            if (!str3.ToLower().Equals("noimage.png"))
            {
                SQLOperation.cmd = new SqlCommand(" update " + tableName + " set " + str2 + "=@" + str2 + " where " + identifierName + "=@" + identifierName, setConnection);
                SQLOperation.cmd.Parameters.AddWithValue("@" + str2, (object)buffer);
                SQLOperation.cmd.Parameters.AddWithValue("@" + identifierName, (object)identifierValue);
                SQLOperation.cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static bool forUpdateValueByTrustColumns(string tableName, string[] setColumns, string[] setValues, string[] identifierName, string[] identifierValue, SqlConnection setConnection)
        {
            byte num1 = (byte)0;
            string str1 = "";
            for (; (int)num1 < setColumns.Length; ++num1)
            {
                if ((int)num1 == setColumns.Length - 1)
                    str1 = str1 + setColumns[(int)num1] + "='" + setValues[(int)num1] + "'";
                else
                    str1 = str1 + setColumns[(int)num1] + "='" + setValues[(int)num1] + "',";
            }
            byte num2 = (byte)0;
            string str2 = "";
            for (; (int)num2 < identifierName.Length; ++num2)
            {
                if ((int)num2 == identifierName.Length - 1)
                    str2 = str2 + identifierName[(int)num2] + "=@" + identifierName[(int)num2];
                else
                    str2 = str2 + identifierName[(int)num2] + "=@" + identifierName[(int)num2] + " AND ";
            }
            SQLOperation.cmd = new SqlCommand("Update " + tableName + " set " + str1 + " where " + str2, setConnection);
            for (byte index = (byte)0; (int)index < identifierName.Length; ++index)
                SQLOperation.cmd.Parameters.AddWithValue("@" + identifierName[(int)index], (object)identifierValue[(int)index]);
            SQLOperation.cmd.ExecuteNonQuery();
            return true;
        }

        public static bool forDelete(string tableName, SqlConnection setConnection)
        {
            if (setConnection.State.ToString() == "Closed") setConnection.Open();
            SQLOperation.cmd = new SqlCommand("delete from " + tableName, setConnection);
            SQLOperation.cmd.ExecuteNonQuery();
            setConnection.Close();
            return true;
        }

        public static bool forDeleteRecordByIdentifier(string tableName, string identifierName, string identifierValue, SqlConnection setConnection)
        {
            if (setConnection.State.ToString() == "Closed") setConnection.Open();
            SQLOperation.cmd = new SqlCommand("delete from " + tableName + " where " + identifierName + " =@" + identifierName, setConnection);
            SQLOperation.cmd.Parameters.AddWithValue("@" + identifierName, (object)identifierValue);
            SQLOperation.cmd.ExecuteNonQuery();
            setConnection.Close();
            return true;
        }

        public static DataTable seletctAll(string tableName, SqlConnection setConnection)
        {
            DataTable dataTable = new DataTable();
            SQLOperation.da = new SqlDataAdapter("select *  from " + tableName, setConnection);
            SQLOperation.da.Fill(dataTable);
            return dataTable;
        }

        public static DataTable forSeletctByIdentifier(string tableName, string[] setColumns, string identifierName, string identifierValue, SqlConnection setConnection)
        {
            DataTable dataTable = new DataTable();
            byte num = (byte)0;
            string str = "";
            for (; (int)num < setColumns.Length; ++num)
                str = (int)num != setColumns.Length - 1 ? str + setColumns[(int)num] + "," : str + setColumns[(int)num];
            SQLOperation.cmd = new SqlCommand("select " + str + " from " + tableName + " where " + identifierName + "= @" + identifierName, setConnection);
            SQLOperation.cmd.Parameters.AddWithValue("@" + identifierName, (object)identifierValue);
            SQLOperation.da = new SqlDataAdapter(SQLOperation.cmd);
            SQLOperation.da.Fill(dataTable);
            return dataTable;
        }

        public static void selectBySetCommandInDatatable(string sqlcommand, DataTable dt, SqlConnection setConnection)
        {
            SQLOperation.da = new SqlDataAdapter(sqlcommand, setConnection);
            SQLOperation.da.Fill(dt);           
        }

        public static void selectBySetCommandInDatatable(string sqlcommand, string getIdentifierValue, DataTable dt, SqlConnection setConnection)
        {
            string[] strArray = sqlcommand.Split('@');
            SQLOperation.cmd = new SqlCommand(sqlcommand, setConnection);
            SQLOperation.cmd.Parameters.AddWithValue("@" + strArray[1], (object)getIdentifierValue);
            SQLOperation.da = new SqlDataAdapter(SQLOperation.cmd);
            SQLOperation.da.Fill(dt);
        }

        public static void selectBySetCommandInDataGridView(string sqlcommand, DataGridView dgv, SqlConnection setConnection)
        {
            DataTable dataTable = new DataTable();
            SQLOperation.da = new SqlDataAdapter(sqlcommand, setConnection);
            SQLOperation.da.Fill(dataTable);
            dgv.DataSource = (object)dataTable;
        }

        public static void setImage(string tableName, string imageFieldName, string uniqueFieldName, string uniqueFieldVale, PictureBox pb, SqlConnection setConnection)
        {
            SQLOperation.cmd = new SqlCommand("select " + imageFieldName + " from " + tableName + " where " + uniqueFieldName + "=@" + uniqueFieldName, setConnection);
            SQLOperation.cmd.Parameters.AddWithValue("@" + uniqueFieldName, (object)uniqueFieldVale);
            SQLOperation.da = new SqlDataAdapter(SQLOperation.cmd);
            DataTable dataTable;
            SQLOperation.da.Fill(dataTable = new DataTable());
            byte[] numArray = new byte[0];
            MemoryStream memoryStream = new MemoryStream((byte[])dataTable.Rows[0].ItemArray[0]);
            pb.Image = Image.FromStream((Stream)memoryStream);
            memoryStream.Close();
        }
    }
}
