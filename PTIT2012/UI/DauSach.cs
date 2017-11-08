using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmDauSach : DevExpress.XtraEditors.XtraForm
    {
        public frmDauSach()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void LockText()
        {
            txtMaDauSach.Enabled = false;
            sluMaNSX.Enabled = false;
            txtSoTrang.Enabled = false;
            txtNamXuatBan.Enabled = false;
            speSoLuong.Enabled = false;
            txtTenDauSach.Enabled = false;
            sluMaLoai.Enabled = false;
            sluNgan.Enabled = false;
            speDonGia.Enabled = false;
            cbxKhoSach.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void UnLockText()
        {
            txtMaDauSach.Enabled = true;
            sluMaNSX.Enabled = true;
            txtSoTrang.Enabled = true;
            txtNamXuatBan.Enabled = true;
            speSoLuong.Enabled = true;
            txtTenDauSach.Enabled = true;
            sluMaLoai.Enabled = true;
            sluNgan.Enabled = true;
            speDonGia.Enabled = true;
            cbxKhoSach.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void ClearText()
        {
            txtMaDauSach.Text = string.Empty;
            txtSoTrang.Text = string.Empty;
            txtNamXuatBan.Text = string.Empty;
            txtTenDauSach.Text = string.Empty;
            cbxKhoSach.Text = string.Empty;
            speDonGia.Text = string.Empty;
            speSoLuong.Text = string.Empty;
            //sluMaLoai.ClosePopup();
            //sluMaNSX.ClosePopup();
            //sluNgan.ClosePopup();
            sluMaLoai.Text = string.Empty;
            sluMaNSX.Text = string.Empty;
            sluNgan.Text = string.Empty;
        }

        private void HienThi()
        {
            grvDauSach.DataSource = DataProvider.GetData(@"SELECT  DAUSACH.MADAUSACH, DAUSACH.MANHASANXUAT,
                                                                   DAUSACH.MALOAI, DAUSACH.TENDAUSACH,
                                                                   DAUSACH.SOTRANG, DAUSACH.DONGIA, DAUSACH.NAMXUATBAN,
                                                                   DAUSACH.KHOSACH, DAUSACH.SOLUONG,
                                                                   VITRINGAN.TENVITRI,VITRINGAN.IDNGAN
                                                          FROM DAUSACH INNER JOIN VITRINGAN ON DAUSACH.IDNGAN = VITRINGAN.IDNGAN ");
        }

        private void frmDauSach_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
            DataProvider.DataSearchLookUpEdit(sluMaLoai, "SELECT * FROM LOAISACH", "TENLOAI", "MALOAI");
            DataProvider.DataSearchLookUpEdit(sluMaNSX, "SELECT * FROM NHASANXUAT", "TEN", "MANHASANXUAT");
            DataProvider.DataSearchLookUpEdit(sluNgan, "SELECT * FROM VITRINGAN", "TENVITRI", "IDNGAN");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = false;
            txtMaDauSach.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaDauSach.Text == "" || txtTenDauSach.Text == "" || txtSoTrang.Text == "" || speDonGia.Text == "" ||
                sluMaLoai.Text == "" || txtNamXuatBan.Text == "" || cbxKhoSach.Text == "" ||
                speSoLuong.Text == "" || sluMaNSX.Text == "" || sluNgan.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM DAUSACH WHERE MADAUSACH= '" + txtMaDauSach.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO DAUSACH
                                                                  VALUES(N'" + txtMaDauSach.Text
                                                                        + "',N'" + sluMaNSX.EditValue
                                                                        + "',N'" + sluMaLoai.EditValue
                                                                        + "',N'" + txtTenDauSach.Text
                                                                        + "',N'" + txtSoTrang.Text
                                                                        + "',N'" + speDonGia.Text
                                                                        + "',N'" + txtNamXuatBan.Text
                                                                        + "',N'" + cbxKhoSach.Text
                                                                        + "',N'" + sluNgan.EditValue
                                                                        + "',N'" + speSoLuong.Text + "')");

                    XtraMessageBox.Show("Thêm thành công");
                    LockText();
                    HienThi();
                    ClearText();
                }
                else
                {
                    XtraMessageBox.Show("Mả bạn nhập đã tổn tại");
                    txtMaDauSach.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE DAUSACH
                                                         SET MADAUSACH =N'" + txtMaDauSach.Text
                                                             + "' ,MANHASANXUAT = N'" + sluMaNSX.EditValue.ToString()
                                                             + "', MALOAI = N'" + sluMaLoai.EditValue.ToString()
                                                             + "', TENDAUSACH = N'" + txtTenDauSach.Text
                                                             + "', SOTRANG = '" + txtSoTrang.Text
                                                             + "', DONGIA = '" + speDonGia.Text
                                                             + "', NAMXUATBAN = '" + txtNamXuatBan.Text
                                                             + "', KHOSACH = N'" + cbxKhoSach.Text
                                                             + "', IDNGAN = '" + sluNgan.EditValue.ToString()
                                                             + "', SOLUONG = '" + speSoLuong.Text
                                                             + "' WHERE MADAUSACH = N'" + txtMaDauSach.Text + "' ");
                XtraMessageBox.Show("Sửa thành công");
                LockText();
                HienThi();
                ClearText();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM SANGTAC WHERE MADAUSACH ='" + txtMaDauSach.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM NHAP WHERE MADAUSACH ='" + txtMaDauSach.Text + "'").Rows.Count == 0 &&
                DataProvider.GetData("SELECT * FROM SACH WHERE MADAUSACH ='" + txtMaDauSach.Text + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE FROM DAUSACH WHERE MADAUSACH='" + txtMaDauSach.Text + "'");
                XtraMessageBox.Show("Xóa thành công");
                LockText();
                HienThi();
                ClearText();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại, bạn không được xóa");
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            txtMaDauSach.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            sluMaNSX.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            sluMaLoai.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            txtTenDauSach.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
            txtSoTrang.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[4]).ToString();
            speDonGia.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[5]).ToString();
            txtNamXuatBan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[6]).ToString();
            cbxKhoSach.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[7]).ToString();
            speSoLuong.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[8]).ToString();
            sluNgan.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[10]).ToString();
        }
    }
}