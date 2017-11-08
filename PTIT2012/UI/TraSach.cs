using DevExpress.XtraEditors;
using PTIT2012.BAOCAO;
using System;

namespace PTIT2012
{
    public partial class frmTraSach : DevExpress.XtraEditors.XtraForm
    {
        public frmTraSach()
        {
            InitializeComponent();
        }

        public bool isInsert;
        public string SoPT;

        public event EventHandler LamMoi_Cick;

        public string cmd;

        private void HienThiCT()
        {
            msds.DataSource = DataProvider.GetData(@"SELECT SACH.MASACH, DAUSACH.TENDAUSACH, '' AS TINHTRANG,
                                                            CHITIETPHIEUTRA.GHICHU, CHITIETPHIEUMUON.SOPM
                                                     FROM SACH INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                                                               INNER JOIN CHITIETPHIEUTRA ON SACH.MASACH = CHITIETPHIEUTRA.MASACH
                                                               INNER JOIN PHIEUTRASACH ON CHITIETPHIEUTRA.SOPT = PHIEUTRASACH.SOPT
                                                               INNER JOIN CHITIETPHIEUMUON ON SACH.MASACH = CHITIETPHIEUMUON.MASACH
                                                           WHERE CHITIETPHIEUTRA.SOPT ='" + SoPT + "'");
        }

        private void frmMuonSach_Load(object sender, EventArgs e)
        {
            txtTT.Text = PhanQuyen.MaTT;
            if (isInsert == false)
            {
                cmd = @"SELECT MADG, HO + ' ' +TEN AS TEN FROM DOCGIA";
                DataProvider.DataLookUpEdit(cbDG, cmd, "TEN", "MADG", "Mã ĐG", "Tên ĐG");
                string sql = @"SELECT PHIEUTRASACH.SOPT, PHIEUTRASACH.MADG,
                                      DOCGIA.HO + ' ' + DOCGIA.TEN AS HOTEN,
                                      THUTHU.HO + ' ' + THUTHU.TEN AS HOTENTT,
                                      CONVERT(nvarchar,PHIEUTRASACH.NGAYTRA,103) AS NGAYTRA
                                FROM PHIEUTRASACH INNER JOIN THUTHU ON PHIEUTRASACH.MATHUTHU = THUTHU.MATHUTHU
                                                  INNER JOIN DOCGIA ON PHIEUTRASACH.MADG = DOCGIA.MADG
                                     WHERE  PHIEUTRASACH.SOPT = '" + SoPT + "'";
                txtMa.Text = SoPT;
                txtNgay.Text = DataProvider.GetData(sql).Rows[0]["NGAYTRA"].ToString();
                txtTT.Text = DataProvider.GetData(sql).Rows[0]["HOTENTT"].ToString();
                cbDG.EditValue = DataProvider.GetData(sql).Rows[0]["MADG"].ToString();
                HienThiCT();
                colTT.Visible = false;
                msds.Enabled = true;
                btnLuu.Enabled = false;
                txtMa.Enabled = false;
                txtNgay.Enabled = false;
                txtTT.Enabled = false;
                cbDG.Enabled = false;
            }
            else
            {
                cmd = @"SELECT  MADG, HO + ' ' +TEN AS TEN FROM DOCGIA";
                txtMa.Text = DataProvider.AutoID("SELECT * FROM PHIEUTRASACH", "PT-");
                txtNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                DataProvider.DataLookUpEdit(cbDG, cmd, "TEN", "MADG", "Mã ĐG", "Tên ĐG");
                btnLuu.Enabled = true;
                btnxoa.Enabled = false;
                btnin.Enabled = false;
                txtTT.Enabled = false;
                txtNgay.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    string maSach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                    string sopm = gridView1.GetRowCellValue(i, gridView1.Columns[4]).ToString();
                    DataProvider.SqlExecuteNonQuery(@"UPDATE SACH
                                                        SET TINHTRANG =N'Sách đang mượn'
                                                        WHERE MASACH = '" + maSach + "'");
                    DataProvider.SqlExecuteNonQuery(@"UPDATE CHITIETPHIEUMUON
                                                        SET TINHTRANG='False'
                                                        WHERE SOPM='" + sopm + "'");
                }

                DataProvider.SqlExecuteNonQuery("DELETE FROM CHITIETPHIEUTRA WHERE SOPT='" + SoPT + "'");
                DataProvider.SqlExecuteNonQuery("DELETE FROM PHIEUTRASACH WHERE SOPT='" + SoPT + "'");
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
                if (cbDG.Text == "" || txtMa.Text == "")
                {
                    XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                    return;
                }
                if (isInsert == true)
                {
                    if (DataProvider.GetData("SELECT * FROM PHIEUTRASACH WHERE SOPT =N'" + txtMa.Text + "'").Rows.Count == 0)
                    {
                        DataProvider.SqlExecuteNonQuery(@"INSERT INTO PHIEUTRASACH (SOPT, MADG, NGAYTRA, MATHUTHU)
                                                         VALUES  ('" + txtMa.Text
                                                      + "','" + cbDG.EditValue.ToString()
                                                      + "',GETDATE(),'"
                                                      + PhanQuyen.MaTT + "')");

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            string maSach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                            string ngay = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();
                            string ghiChu = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();
                            string tinhTrang = gridView1.GetRowCellValue(i, gridView1.Columns[3]).ToString();
                            string sopm = gridView1.GetRowCellValue(i, gridView1.Columns[4]).ToString();
                            if (tinhTrang == "True")
                            {
                                DataProvider.SqlExecuteNonQuery(@"INSERT INTO CHITIETPHIEUTRA (SOPT, MASACH, GHICHU)
                                                                    VALUES (N'" + txtMa.Text
                                                                                    + "',N'"
                                                                                    + maSach + "' ,N'"
                                                                                    + ghiChu + "')");
                                DataProvider.SqlExecuteNonQuery(@"UPDATE SACH
                                                                SET TINHTRANG=N'Trống'
                                                                WHERE MASACH='" + maSach + "' ");
                                DataProvider.SqlExecuteNonQuery(@"UPDATE CHITIETPHIEUMUON
                                                                    SET TINHTRANG='True'
                                                                    WHERE MASACH='" + maSach + "'AND SOPM='" + sopm + "' ");
                            }
                        }
                        if (this.LamMoi_Cick != null)
                            this.LamMoi_Cick(sender, e);
                        XtraMessageBox.Show("Thêm dữ liệu thành công");
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Mã bạn vừa nhập đã tồn tại");
                        txtMa.Focus();
                    }
                }
            }
        }

        private void cbDG_EditValueChanged(object sender, EventArgs e)
        {
            msds.DataSource = DataProvider.GetData(@"SELECT  SACH.MASACH, DAUSACH.TENDAUSACH, '' AS GHICHU,
                                                             CONVERT(bit, 'true') AS TINHTRANG,
                                                             PHIEUMUONSACH.SOPM
                                                     FROM SACH INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                                                               INNER JOIN CHITIETPHIEUMUON ON SACH.MASACH = CHITIETPHIEUMUON.MASACH
                                                               INNER JOIN PHIEUMUONSACH ON CHITIETPHIEUMUON.SOPM = PHIEUMUONSACH.SOPM
                                                          WHERE CHITIETPHIEUMUON.TINHTRANG='false'
                                                                AND MADG='" + cbDG.EditValue.ToString() + "'");
        }

        private void btnin_Click(object sender, EventArgs e)
        {
            frmRPInPTS fr = new frmRPInPTS();
            fr.SoPhieu = txtMa.Text;
            fr.ShowInTaskbar = false;
            fr.ShowDialog();
        }
    }
}