using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmLoaiSach : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiSach()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void HienThi()
        {
            grvLoaiSach.DataSource = DataProvider.GetData("SELECT * FROM LOAISACH");
        }

        private void LockText()
        {
            txtMaLoai.Enabled = false;
            txtTenLoai.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void UnLockText()
        {
            txtMaLoai.Enabled = true;
            txtTenLoai.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void ClearText()
        {
            txtMaLoai.Text = string.Empty;
            txtTenLoai.Text = string.Empty;
        }

        private void frmLoaiSach_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaLoai.Text == "" || txtTenLoai.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM LOAISACH  WHERE MALOAI='" + txtMaLoai.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO LOAISACH(MALOAI, TENLOAI)
                                                             VALUES(N'" + txtMaLoai.Text + "', N'"
                                                                        + txtTenLoai.Text + "')");
                    XtraMessageBox.Show("Thêm thành công");
                    HienThi();
                    ClearText();
                    LockText();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn nhập đã tồn tại");
                    txtMaLoai.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE LOAISACH
                                                         SET MALOAI= N'" + txtMaLoai.Text
                                                                         + "',TENLOAI= N'" + txtTenLoai.Text +
                                                         "' WHERE MALOAI='" + txtMaLoai.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                HienThi();
                ClearText();
                LockText();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnLockText();
            txtMaLoai.Enabled = false;
            trangThai = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM DAUSACH WHERE MALOAI ='" + txtMaLoai.Text + "' ").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE FROM LOAISACH WHERE MALOAI ='" + txtMaLoai.Text + "'");
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
            txtMaLoai.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            txtTenLoai.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
        }
    }
}