using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Data;
using System.Data.SqlClient;

namespace PTIT2012
{
    public class DataProvider
    {
        public static SqlConnection sqlConnection = new SqlConnection("Server=PHUOC-PC;Database=PTIT;Trusted_Connection=True;");

        //
        // Lay Data
        //
        public static DataTable GetData(string query)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        //
        // Thuc thi truy van Insert, Update, Delete
        //
        public static int SqlExecuteNonQuery(string query)
        {
            int Result = 0;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            Result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return Result;
        }

        //
        // LookUpEdit
        //
        public static void DataLookUpEdit(LookUpEdit lookUpEdit, string sqlConnection, string disMember, string valMember, string colNameVal, string colNameDis)
        {
            lookUpEdit.Properties.DataSource = GetData(sqlConnection);
            lookUpEdit.Properties.DisplayMember = disMember;            //Trường Muốn Hiển Thị Trên control
            lookUpEdit.Properties.ValueMember = valMember;              //Thông tin EditValue ẩn trên control
            LookUpColumnInfoCollection lookUpColl = lookUpEdit.Properties.Columns;
            lookUpColl.Add(new LookUpColumnInfo(valMember, 0, colNameVal));
            lookUpColl.Add(new LookUpColumnInfo(disMember, 0, colNameDis));
            lookUpEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
        }

        //
        //SearchLookUpEdit
        //
        public static void DataSearchLookUpEdit(SearchLookUpEdit sLookUpEdit, string sqlConnection, string disMember, string valMember) //string colNameDis, string colNameVal)
        {
            sLookUpEdit.Properties.DataSource = GetData(sqlConnection);
            sLookUpEdit.Properties.DisplayMember = disMember;
            sLookUpEdit.Properties.ValueMember = valMember;
            sLookUpEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
        }

        //
        //ComboBoxEdit
        //
        public static void LoadComboxinXtg(RepositoryItemLookUpEdit rILookUpEdit, DataTable dataTable, string disMember, string valMember, string colNameVal, string colNameDis)
        {
            rILookUpEdit.DataSource = dataTable;
            rILookUpEdit.DisplayMember = disMember;
            rILookUpEdit.ValueMember = valMember;
            LookUpColumnInfoCollection lookUpColl = rILookUpEdit.Columns;
            lookUpColl.Add(new LookUpColumnInfo(valMember, 0, colNameVal));
            lookUpColl.Add(new LookUpColumnInfo(disMember, 0, colNameDis));
            rILookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
        }

        public static string AutoID(string sql, string tienTo)
        {
            string temp = "";
            if (GetData(sql).Rows.Count == 0)
                temp = tienTo + 1;
            else
            {
                temp = tienTo + (GetData(sql).Rows.Count + 1);
            }
            return temp;
        }
    }
}