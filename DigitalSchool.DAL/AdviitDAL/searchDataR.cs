using System;
using System.Data;

namespace DS.DAL.AdviitDAL
{
    public static class searchData
    {
        public static DataTable search(DataTable SourceDataTable, string SearchFieldName, string SearchItem)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (SourceDataTable.Rows.Count == 0)
                    return SourceDataTable;
                int index1 = -1;
                SearchFieldName = SearchFieldName.ToLower();
                string[] strArray = new string[SourceDataTable.Columns.Count];
                SearchItem = SearchItem.ToLower();
                for (int index2 = 0; index2 < SourceDataTable.Columns.Count; ++index2)
                {
                    if (SourceDataTable.Columns[index2].ColumnName.ToLower().CompareTo(SearchFieldName) == 0)
                        index1 = index2;
                    dataTable.Columns.Add(SourceDataTable.Columns[index2].ColumnName);
                }
                for (int index2 = 0; index2 < SourceDataTable.Rows.Count; ++index2)
                {
                    if (SourceDataTable.Rows[index2].ItemArray[index1].ToString().ToLower().CompareTo(SearchItem) == 0)
                    {
                        for (int index3 = 0; index3 < SourceDataTable.Columns.Count; ++index3)
                            strArray[index3] = SourceDataTable.Rows[index2].ItemArray[index3].ToString();
                        dataTable.Rows.Add((object[])strArray);
                    }
                    if (index2 > 10000)
                        break;
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return dataTable;
            }
        }

        public static DataTable search(DataTable SourceDataTable, string SearchFieldName, string SearchItem, bool AbortForNotFound)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (SourceDataTable.Rows.Count == 0)
                    return SourceDataTable;
                int index1 = -1;
                SearchFieldName = SearchFieldName.ToLower();
                string[] strArray = new string[SourceDataTable.Columns.Count];
                SearchItem = SearchItem.ToLower();
                for (int index2 = 0; index2 < SourceDataTable.Columns.Count; ++index2)
                {
                    if (SourceDataTable.Columns[index2].ColumnName.ToLower().CompareTo(SearchFieldName) == 0)
                        index1 = index2;
                    dataTable.Columns.Add(SourceDataTable.Columns[index2].ColumnName);
                }
                for (int index2 = 0; index2 < SourceDataTable.Rows.Count && SourceDataTable.Rows[index2].ItemArray[index1].ToString().ToLower().CompareTo(SearchItem) == 0; ++index2)
                {
                    for (int index3 = 0; index3 < SourceDataTable.Columns.Count; ++index3)
                        strArray[index3] = SourceDataTable.Rows[index2].ItemArray[index3].ToString();
                    dataTable.Rows.Add((object[])strArray);
                    if (index2 > 10000)
                        break;
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return dataTable;
            }
        }

        public static DataTable search(DataTable SourceDataTable, string SearchFieldName, string SearchItem, string OutputFieldName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (SourceDataTable.Rows.Count == 0)
                    return SourceDataTable;
                int index1 = -1;
                int index2 = -1;
                SearchFieldName = SearchFieldName.ToLower();
                OutputFieldName = OutputFieldName.ToLower();
                for (int index3 = 0; index3 < SourceDataTable.Columns.Count; ++index3)
                {
                    if (SourceDataTable.Columns[index3].ColumnName.ToLower().CompareTo(SearchFieldName) == 0)
                        index2 = index3;
                    else if (SourceDataTable.Columns[index3].ColumnName.ToLower().CompareTo(OutputFieldName) == 0)
                        index1 = index3;
                    if (index1 > -1 && index2 > -1)
                        break;
                }
                SearchItem = SearchItem.ToLower();
                dataTable.Columns.Add(SourceDataTable.Columns[index1].ColumnName);
                for (int index3 = 0; index3 < SourceDataTable.Rows.Count; ++index3)
                {
                    if (SourceDataTable.Rows[index3].ItemArray[index2].ToString().ToLower().CompareTo(SearchItem) == 0)
                        dataTable.Rows.Add(new object[1]
            {
              (object) SourceDataTable.Rows[index3].ItemArray[index2].ToString()
            });
                    if (index3 > 10000)
                        break;
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return dataTable;
            }
        }

        public static string searchSingle(DataTable DT, int SearchFieldIndex, string SearchItem, int OutputFieldIndex)
        {
            try
            {
                if (DT.Rows.Count == 0)
                    return "";
                SearchItem = SearchItem.ToLower();
                for (int index = 0; index < DT.Rows.Count; ++index)
                {
                    if (DT.Rows[index].ItemArray[SearchFieldIndex].ToString().ToLower().CompareTo(SearchItem) == 0)
                        return DT.Rows[index].ItemArray[SearchFieldIndex].ToString();
                    if (index > 10000)
                        break;
                }
                return "";
            }
            catch (Exception ex)
            {
                sqlDB.dbError = ex.Message;
                return "";
            }
        }

        public static string setMessage(string errorMessage)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                return errorMessage;
            }
        }
    }
}
