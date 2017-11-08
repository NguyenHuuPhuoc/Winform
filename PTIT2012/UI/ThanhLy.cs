using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmThanhLy : DevExpress.XtraEditors.XtraForm
    {
        public frmThanhLy()
        {
            InitializeComponent();
        }

        public bool isInsert;
        public string SoPTL;

        public event EventHandler LamMoi_Cick;

        public string cmd;

        private void LockText()
        {
            txtMa.Enabled = false;
            txtNgay.Enabled = false;
            txtTT.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = true;
        }

        private void UnLockText()
        {
            txtMa.Enabled = true;
            txtNgay.Enabled = false;
            txtTT.Enabled = false;
            btnLuu.Enabled = true;
            btnXoa.Enabled = false;
        }

        private void hienthiCT()
        {
            msds.DataSource = DataProvider.GetData(@"SELECT  MASACH, TIENTHANHLY
                                                     FROM CHITIETTHANHLY
                                                        WHERE CHITIETTHANHLY.IDTHANHLY='" + SoPTL + "'  ");
        }

        private void frmMuonSach_Load(object sender, EventArgs e)
        {
            txtTT.Text = PhanQuyen.MaTT;

            if (isInsert == false)
            {
                hienthiCT();
                cmd = @"SELECT SACH.MASACH, DAUSACH.TENDAUSACH
                        FROM SACH INNER JOIN DAUSACH
                                        ON SACH.MADAUSACH = DAUSACH.MADAUSACH";
                DataProvider.LoadComboxinXtg(cbSach, DataProvider.GetData(cmd), "TENDAUSACH", "MASACH", "Mã Sách", "Tên Sách");
                string sql = @"SELECT  CONVERT(nvarchar,THANHLY.NGAYTHANHLY,103) AS NGAYTHANHLY,
                                       THUTHU.HO + ' ' + THUTHU.TEN AS HOTENTT,
                                       CHITIETTHANHLY.MASACH
                               FROM THANHLY INNER JOIN THUTHU ON THANHLY.MATHUTHU = THUTHU.MATHUTHU
                                            INNER JOIN CHITIETTHANHLY ON THANHLY.IDTHANHLY = CHITIETTHANHLY.IDTHANHLY
                                    WHERE THANHLY.IDTHANHLY = '" + SoPTL + "'";
                txtMa.Text = SoPTL;
                txtNgay.Text = DataProvider.GetData(sql).Rows[0]["NGAYTHANHLY"].ToString();
                txtTT.Text = DataProvider.GetData(sql).Rows[0]["HOTENTT"].ToString();
                LockText();
            }
            else
            {
                txtMa.Text = DataProvider.AutoID("SElect * from THANHLY", "TL-");
                txtNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                hienthiCT();
                cmd = @"SELECT SACH.MASACH, DAUSACH.TENDAUSACH
                       FROM SACH INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                            WHERE TINHTRANG=N'Trống'";
                DataProvider.LoadComboxinXtg(cbSach, DataProvider.GetData(cmd), "TENDAUSACH", "MASACH", "Mã Sách", "Tên Sách");
                UnLockText();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    string masach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                    //                    DataProvider.SqlExecuteNonQuery(@"UPDATE SACH
                    //                                                        SET TINHTRANG =N'Trống'
                    //                                                        WHERE MASACH = '" + masach + "'");
                    //                    DataProvider.SqlExecuteNonQuery("DELETE FROM CHITIETTHANHLY WHERE IDTHANHLY='" + SoPTL + "'");
                    //                    DataProvider.SqlExecuteNonQuery("DELETE FROM THANHLY WHERE IDTHANHLY='" + SoPTL + "'");
                    cmd = @"EXECUTE SP_XOATHANHLY N'" + txtMa.Text
                                                      + "',N'" + masach + "'";
                    DataProvider.SqlExecuteNonQuery(cmd);
                }
                if (this.LamMoi_Cick != null)
                    this.LamMoi_Cick(sender, e);
                XtraMessageBox.Show("Xóa thành công");
                this.Close();
            }
            catch
            { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                if (isInsert == true)
                {
                    if (DataProvider.GetData("SELECT * FROM THANHLY WHERE IDTHANHLY =N'" + txtMa.Text + "'").Rows.Count == 0)
                    {
                        DataProvider.SqlExecuteNonQuery(@"INSERT INTO THANHLY(IDTHANHLY, NGAYTHANHLY, MATHUTHU)
                                                        VALUES  ('" + txtMa.Text
                                                                   + "' ,GETDATE(),'"
                                                                   + PhanQuyen.MaTT + "')");

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            string masach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                            string tientl = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();
                            //                            DataProvider.SqlExecuteNonQuery(@"INSERT INTO CHITIETTHANHLY (IDTHANHLY, MASACH, TIENTHANHLY)
                            //                                                                                        VALUES (N'" + txtMa.Text
                            //                                                                            + "',N'" + masach
                            //                                                                            + "' ,N'" + tientl + "')");
                            //                            DataProvider.SqlExecuteNonQuery(@"UPDATE SACH
                            //                                                                                        SET TINHTRANG= N'Sách thanh lý'
                            //                                                                                        WHERE MASACH='" + masach + "' ");
                            cmd = @"EXECUTE SP_SAVECTTHANHLY N'" + txtMa.Text
                                                                 + "',N'" + masach
                                                                 + "',N'" + tientl + "'";
                            DataProvider.SqlExecuteNonQuery(cmd);
                        }
                        if (this.LamMoi_Cick != null)
                            this.LamMoi_Cick(sender, e);
                        XtraMessageBox.Show("Thêm thành công");
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Mã bạn nhập đả tồn tại");
                        txtMa.Focus();
                    }
                }
            }
        }
    }
}