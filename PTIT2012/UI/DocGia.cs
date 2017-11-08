using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmDocGia : DevExpress.XtraEditors.XtraForm
    {
        public frmDocGia()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void HienThi()
        {
            grvDocGia.DataSource = DataProvider.GetData(@"SELECT MADG, HO, TEN,
                                                                CASE WHEN PHAI= 'TRUE' THEN 'Nam' ELSE N'Nữ' END AS PHAI,
                                                                DIACHI, NGAYSINH, EMAIL
                                                        FROM DOCGIA");
        }

        private void LockText()
        {
            txtMa.Enabled = false;
            txtHo.Enabled = false;
            txtTen.Enabled = false;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            txtDiaChi.Enabled = false;
            dtNgaySinh.Enabled = false;
            txtEmail.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            lblGT.Enabled = false;
        }

        private void UnlockText()
        {
            txtMa.Enabled = true;
            txtHo.Enabled = true;
            txtTen.Enabled = true;
            rdNam.Enabled = true;
            rdNu.Enabled = true;
            txtDiaChi.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtEmail.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            lblGT.Enabled = true;
        }

        private void ClearText()
        {
            txtMa.Text = string.Empty;
            txtHo.Text = string.Empty;
            txtTen.Text = string.Empty;
            rdNam.Checked = false;
            rdNu.Checked = false;
            txtDiaChi.Text = string.Empty;
            dtNgaySinh.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void frmDocGia_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnlockText();
            //txtMa.Text = DataProvider.AutoID("SELECT * FROM DOCGIA", "DG");
            trangThai = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnlockText();
            trangThai = false;
            txtMa.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMa.Text == "" || txtHo.Text == "" || txtTen.Text == "" || txtDiaChi.Text == "" ||
                dtNgaySinh.Text == "" || txtEmail.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            bool gioiTinh = true;
            if (rdNu.Checked == true)
            {
                gioiTinh = false;
            }
            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM DOCGIA WHERE MADG='" + txtMa.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO DOCGIA (MADG, HO, TEN, PHAI, DIACHI, NGAYSINH, EMAIL)
                                                             VALUES (N'" + txtMa.Text
                                                                         + "',N'" + txtHo.Text
                                                                         + "',N'" + txtTen.Text
                                                                         + "',N'" + gioiTinh + "',N'"
                                                                         + txtDiaChi.Text + "',N'"
                                                                         + ConvertSQL.DaytoSQLInsert(dtNgaySinh.Text)
                                                                         + "',N'" + txtEmail.Text + "')");
                    XtraMessageBox.Show("Bạn đã thêm thành công");
                    LockText();
                    ClearText();
                    HienThi();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn vừa nhập đã tồn tại");
                    txtMa.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE DOCGIA
                                                         SET HO = N'" + txtHo.Text
                                                                      + "', TEN = N'" + txtTen.Text
                                                                      + "', PHAI = N'" + gioiTinh
                                                                      + "', DIACHI = N'" + txtDiaChi.Text
                                                                      + "', NGAYSINH = N'" + ConvertSQL.DaytoSQLInsert(dtNgaySinh.Text)
                                                                      + "', EMAIL = N'" + txtEmail.Text
                                                                      + "' WHERE MADG = N'" + txtMa.Text + "'");
                XtraMessageBox.Show("Đã sửa thành công");
                LockText();
                ClearText();
                HienThi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM PHIEUMUONSACH WHERE MADG='" + txtMa.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM PHIEUTRASACH WHERE MADG='" + txtMa.Text + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE FROM DOCGIA WHERE MADG='" + txtMa.Text + "'");
                XtraMessageBox.Show("Xóa thành công");
                LockText();
                ClearText();
                HienThi();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại, bạn không được xóa");
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            LockText();
            txtMa.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            txtHo.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            txtTen.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString().Equals("Nam"))
                rdNam.Checked = true;
            else
                rdNu.Checked = true;
            txtDiaChi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[4]).ToString();
            txtEmail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[6]).ToString();
            dtNgaySinh.Text = DateTime.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[5]).ToString()).ToString("dd/MM/yyyy");
        }
    }
}