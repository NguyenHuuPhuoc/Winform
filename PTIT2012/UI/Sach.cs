using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmSach : DevExpress.XtraEditors.XtraForm
    {
        public frmSach()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void LockText()
        {
            txtMaSach.Enabled = false;
            sluDauSach.Enabled = false;
            cbxKieuSach.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void UnLockText()
        {
            txtMaSach.Enabled = true;
            sluDauSach.Enabled = true;
            cbxKieuSach.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void ClearText()
        {
            txtMaSach.Text = string.Empty;
            cbxKieuSach.Text = string.Empty;
            sluDauSach.Text = string.Empty;
        }

        private void HienThi()
        {
            grvSach.DataSource = DataProvider.GetData(@"SELECT SACH.MASACH, DAUSACH.MADAUSACH, DAUSACH.TENDAUSACH, SACH.KIEUSACH
                                                        FROM SACH INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH");
        }

        private void frmLoaiSach_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
            DataProvider.DataSearchLookUpEdit(sluDauSach, "SELECT MADAUSACH,TENDAUSACH FROM DAUSACH", "TENDAUSACH", "MADAUSACH");
            // gridView1.ExpandAllGroups();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = true;
            //txtMaLoai.Text = DataProvider.AutoID("SElect * from SACH", "SACH-");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnLockText();
            txtMaSach.Enabled = false;
            trangThai = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "" || sluDauSach.Text == "" || cbxKieuSach.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            if (trangThai == true)
            {
                if (DataProvider.GetData(" SELECT * FROM SACH WHERE MASACH='" + txtMaSach.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO SACH (MASACH, MADAUSACH, TINHTRANG,KIEUSACH)
                                                                 VALUES(N'" + txtMaSach.Text
                                                                            + "',N'" + sluDauSach.EditValue
                                                                            + "',N'Trống',N'"
                                                                            + cbxKieuSach.Text + "')");
                    XtraMessageBox.Show("Thêm thành công");
                    LockText();
                    HienThi();
                    ClearText();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn nhập đả tồn tại");
                    txtMaSach.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE SACH
                                                         SET MADAUSACH = N'" + sluDauSach.EditValue.ToString()
                                                                             + "',KIEUSACH = N'" + cbxKieuSach.Text
                                                                             + "' WHERE MASACH = N'" + txtMaSach.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                LockText();
                HienThi();
                ClearText();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM CHITIETPHIEUMUON WHERE MASACH=N'" + txtMaSach.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM CHITIETPHIEUTRA WHERE MASACH=N'" + txtMaSach.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM CHITIETTHANHLY WHERE MASACH=N'" + txtMaSach.Text + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE SACH WHERE MASACH = N'" + txtMaSach.Text + "'");
                XtraMessageBox.Show("Xóa thành công");
                ClearText();
                HienThi();
                LockText();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại, bạn không được xóa");
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaSach.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
                sluDauSach.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                cbxKieuSach.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
            }
            catch { }
        }
    }
}