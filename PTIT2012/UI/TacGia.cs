using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmTacGia : DevExpress.XtraEditors.XtraForm
    {
        public frmTacGia()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void HienThi()
        {
            grvTacGia.DataSource = DataProvider.GetData(@"SELECT MATACGIA, HO, TEN,
                                                                CASE WHEN PHAI='TRUE' THEN 'Nam' ELSE N'Nữ' END AS PHAI,
                                                                DIACHI, NGAYSINH, EMAIL
                                                        FROM TACGIA");
        }

        private void LockText()
        {
            txtMaTacGia.Enabled = false;
            txtHo.Enabled = false;
            txtTen.Enabled = false;
            txtDiaChi.Enabled = false;
            txtEmail.Enabled = false;
            dtNgaySinh.Enabled = false;
            lblGT.Enabled = false;
            rdbNam.Enabled = false;
            rdbNu.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void UnLockText()
        {
            txtMaTacGia.Enabled = true;
            txtHo.Enabled = true;
            txtTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            dtNgaySinh.Enabled = true;
            btnLuu.Enabled = true;
            lblGT.Enabled = true;
            rdbNam.Enabled = true;
            rdbNu.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void ClearText()
        {
            txtMaTacGia.Text = string.Empty;
            txtHo.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtEmail.Text = string.Empty;
            dtNgaySinh.Text = string.Empty;
            rdbNam.Checked = false;
            rdbNu.Checked = false;
        }

        private void frmTacGia_Load(object sender, EventArgs e)
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
            if (txtMaTacGia.Text == "" || txtHo.Text == "" || txtTen.Text == "" ||
                txtDiaChi.Text == "" || txtEmail.Text == "" || dtNgaySinh.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            bool gioiTinh = true;
            if (rdbNu.Checked == true)
            {
                gioiTinh = false;
            }
            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM TACGIA WHERE MATACGIA='" + txtMaTacGia.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO TACGIA(MATACGIA, HO, TEN, PHAI, DIACHI, NGAYSINH, EMAIL)
                                                              VALUES(N'" + txtMaTacGia.Text
                                                                         + "',N'" + txtHo.Text + "',N'"
                                                                         + txtTen.Text + "',N'" + gioiTinh
                                                                         + "',N'" + txtDiaChi.Text
                                                                         + "',N'" + ConvertSQL.DaytoSQLInsert(dtNgaySinh.Text)
                                                                         + "',N'" + txtEmail.Text + "')");
                    XtraMessageBox.Show("Thêm thành công");
                    HienThi();
                    LockText();
                    ClearText();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn nhập đã tồn tại");
                    txtMaTacGia.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE TACGIA
                                                         SET HO = N'" + txtHo.Text
                                                                      + "', TEN = N'" + txtTen.Text
                                                                      + "', PHAI = N'" + gioiTinh
                                                                      + "', DIACHI = N'" + txtDiaChi.Text
                                                                      + "', NGAYSINH = N'" + ConvertSQL.DaytoSQLInsert(dtNgaySinh.Text)
                                                                      + "', EMAIL = N'" + txtEmail.Text
                                                                      + "' WHERE MATACGIA = N'" + txtMaTacGia.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                HienThi();
                LockText();
                ClearText();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnLockText();
            txtMaTacGia.Enabled = false;
            trangThai = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM SANGTAC WHERE MATACGIA ='" + txtMaTacGia.Text + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE FROM TACGIA WHERE MATACGIA =N'" + txtMaTacGia.Text + "'");
                XtraMessageBox.Show("Xóa thành công");
                HienThi();
                ClearText();
                LockText();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại, bạn không được xóa");
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            txtMaTacGia.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            txtHo.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            txtTen.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            txtDiaChi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
            txtEmail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[4]).ToString();
            dtNgaySinh.Text = DateTime.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[5]).ToString()).ToString("dd/MM/yyyy");
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[6]).ToString().Equals("Nam"))
                rdbNam.Checked = true;
            else
                rdbNu.Checked = true;
        }
    }
}