using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmNhaSanXuat : DevExpress.XtraEditors.XtraForm
    {
        public frmNhaSanXuat()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void HienThi()
        {
            grvNhaXuatBan.DataSource = DataProvider.GetData("SELECT * FROM NHASANXUAT");
        }

        private void LockText()
        {
            txtMaNhaSanXuat.Enabled = false;
            txtTenNSX.Enabled = false;
            txtDiaChiNSX.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void UnLockText()
        {
            txtMaNhaSanXuat.Enabled = true;
            txtTenNSX.Enabled = true;
            txtDiaChiNSX.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }

        private void ClearText()
        {
            txtMaNhaSanXuat.Text = string.Empty;
            txtTenNSX.Text = string.Empty;
            txtDiaChiNSX.Text = string.Empty;
        }

        private void frmNhaSanXuat_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            trangThai = true;
            UnLockText();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangThai = false;
            txtMaNhaSanXuat.Enabled = false;
            UnLockText();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNhaSanXuat.Text == "" || txtTenNSX.Text == "" || txtDiaChiNSX.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM NHASANXUAT WHERE MANHASANXUAT = N'" + txtMaNhaSanXuat.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO NHASANXUAT(MANHASANXUAT, TEN, DIACHI)
                                                             VALUES(N'" + txtMaNhaSanXuat.Text
                                                                        + "',N'" + txtTenNSX.Text
                                                                        + "',N'" + txtDiaChiNSX.Text + "')");
                    XtraMessageBox.Show("Thêm thành công");
                    HienThi();
                    LockText();
                    ClearText();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn nhập đã tồn tại");
                    txtMaNhaSanXuat.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE NHASANXUAT
                                                        SET MANHASANXUAT =N'" + txtMaNhaSanXuat.Text
                                                                              + "', TEN =N'" + txtTenNSX.Text
                                                                              + "', DIACHI =N'" + txtDiaChiNSX.Text
                                                                              + "' WHERE MANHASANXUAT =N'" + txtMaNhaSanXuat.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                HienThi();
                LockText();
                ClearText();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM DAUSACH WHERE MANHASANXUAT = N'" + txtMaNhaSanXuat.Text + "' ").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE FROM NHASANXUAT WHERE MANHASANXUAT = N'" + txtMaNhaSanXuat.Text + "'");
                XtraMessageBox.Show("Xóa thành công");
                HienThi();
                LockText();
                ClearText();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại, bạn không được xóa");
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            txtMaNhaSanXuat.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            txtTenNSX.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            txtDiaChiNSX.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
        }
    }
}