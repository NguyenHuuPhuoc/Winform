using DevExpress.XtraEditors;
using System;
using System.Globalization;

namespace PTIT2012
{
    public partial class frmGiaHan : DevExpress.XtraEditors.XtraForm
    {
        public frmGiaHan()
        {
            InitializeComponent();
        }

        private string cmd;

        private void HienThi()
        {
            cmd = @"EXECUTE SP_HIENTHIGHAN N'" + txtMS.Text + "'";
            msds.DataSource = DataProvider.GetData(cmd);
        }

        private void txtMS_EditValueChanged(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string maSach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                //string ngay = DateTime.Parse(gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString()).AddDays(3).ToString("dd/MM/yyyy");
                string ngay = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();
                string dt = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(5).ToString("dd/MM/yyyy");
                string tinhTrang = gridView1.GetRowCellValue(i, gridView1.Columns[3]).ToString();
                string sopm = gridView1.GetRowCellValue(i, gridView1.Columns[4]).ToString();
                if (tinhTrang == "True")
                {
                    cmd = @"EXECUTE SP_GIAHAN N'" + maSach
                                                  + "',N'" + sopm
                                                  + "',N'" + ConvertSQL.DaytoSQLInsert(dt) + "'";
                    DataProvider.SqlExecuteNonQuery(cmd);
                }
            }
            XtraMessageBox.Show("Gia hạn thành công");
            HienThi();
        }
    }
}