using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmThuThu : DevExpress.XtraEditors.XtraForm
    {
        public frmThuThu()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void HienThi()
        {
            grvThuThu.DataSource = DataProvider.GetData(@"SELECT MATHUTHU, HO, TEN,
                                                                CASE WHEN PHAI='TRUE' THEN 'Nam' ELSE N'Nữ' END AS PHAI,
                                                                DIACHI, NGAYSINH, EMAIL, MATKHAU,
                                                                CASE WHEN ISADMIN='TRUE' THEN 'Admin' ELSE N'User' END AS ISADMIN
                                                        FROM THUTHU");
        }

        private void LockText()
        {
            txtMaThuThu.Enabled = false;
            txtHo.Enabled = false;
            txtTen.Enabled = false;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            dtNgaySinh.Enabled = false;
            txtEmail.Enabled = false;
            txtMK.Enabled = false;
            txtDiaChi.Enabled = false;
            ckAdmin.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void UnlockText()
        {
            txtMaThuThu.Enabled = true;
            txtHo.Enabled = true;
            txtTen.Enabled = true;
            rdNam.Enabled = true;
            rdNu.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtEmail.Enabled = true;
            txtMK.Enabled = true;
            txtDiaChi.Enabled = true;
            ckAdmin.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }

        private void ClearText()
        {
            txtMaThuThu.Text = string.Empty;
            txtHo.Text = string.Empty;
            txtTen.Text = string.Empty;
            rdNam.Checked = true;
            txtDiaChi.Text = string.Empty;
            dtNgaySinh.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void frmThuThu_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnlockText();
            trangThai = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnlockText();
            trangThai = false;
            txtMaThuThu.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaThuThu.Text == "" || txtHo.Text == "" || txtTen.Text == "" || txtDiaChi.Text == "" ||
                dtNgaySinh.Text == "" || txtEmail.Text == "" || txtMK.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            bool gioiTinh = true;
            if (rdNu.Checked == true)
                gioiTinh = false;
            bool admin = false;
            if (ckAdmin.Checked == true)
                admin = true;
            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM THUTHU WHERE MATHUTHU='" + txtMaThuThu.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO THUTHU
                                                             VALUES (N'" + txtMaThuThu.Text
                                                                         + "',N'" + txtHo.Text
                                                                         + "',N'" + txtTen.Text
                                                                         + "',N'" + gioiTinh + "',N'"
                                                                         + txtDiaChi.Text + "',N'"
                                                                         + ConvertSQL.DaytoSQLInsert(dtNgaySinh.Text)
                                                                         + "',N'" + txtEmail.Text + "',N'" + txtMK.Text
                                                                         + "','" + admin + "')");
                    XtraMessageBox.Show("Thêm thành công");
                    LockText();
                    ClearText();
                    HienThi();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn vừa nhập đã tồn tại");
                    txtMaThuThu.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE THUTHU
                                                         SET HO = N'" + txtHo.Text
                                                                      + "', TEN = N'" + txtTen.Text
                                                                      + "', PHAI = N'" + gioiTinh
                                                                      + "', DIACHI = N'" + txtDiaChi.Text
                                                                      + "', NGAYSINH = N'"
                                                                      + ConvertSQL.DaytoSQLInsert(dtNgaySinh.Text)
                                                                      + "', EMAIL = N'" + txtEmail.Text
                                                                      + "', IsAdmin='" + admin
                                                                      + "' WHERE MATHUTHU = N'" + txtMaThuThu.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                LockText();
                ClearText();
                HienThi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM PHIEUMUONSACH WHERE MATHUTHU= N'" + txtMaThuThu.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM PHIEUTRASACH WHERE MATHUTHU= N'" + txtMaThuThu.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM PHIEUTHU WHERE MATHUTHU= N'" + txtMaThuThu.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM NHAP WHERE MATHUTHU= N'" + txtMaThuThu.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM THANHLY WHERE MATHUTHU= N'" + txtMaThuThu.Text + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE FROM THUTHU WHERE MATHUTHU='" + txtMaThuThu.Text + "'");
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
            LockText();
            txtMaThuThu.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            txtHo.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            txtTen.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString().Equals("Nam"))
                rdNam.Checked = true;
            else
                rdNu.Checked = true;
            txtDiaChi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[4]).ToString();
            dtNgaySinh.Text = DateTime.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[5]).ToString()).ToString("dd/MM/yyyy");
            txtEmail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[6]).ToString();
            txtMK.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[7]).ToString();
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[8]).ToString().Equals("Admin"))
                ckAdmin.Checked = true;
            else
                ckAdmin.Checked = false;
        }
    }
}