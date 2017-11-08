using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmThuPhi : DevExpress.XtraEditors.XtraForm
    {
        public frmThuPhi()
        {
            InitializeComponent();
        }

        private bool trangThai = false;

        private void LockText()
        {
            luDocGia.Enabled = false;
            luPhi.Enabled = false;
            txtMaPT.Enabled = false;
            txtNgayThu.Enabled = false;
            txtMaTT.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            //btnHuy.Enabled = false;
        }

        private void UnLockText()
        {
            luDocGia.Enabled = true;
            luPhi.Enabled = true;
            txtMaPT.Enabled = true;
            txtMaTT.Enabled = false;
            txtNgayThu.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            //btnHuy.Enabled = true;
        }

        private void ClearText()
        {
            txtMaPT.Text = string.Empty;
            luPhi.EditValue = null;
            luDocGia.EditValue = null;
            txtNgayThu.Text = string.Empty;
            txtMaTT.Text = string.Empty;
        }

        private void HienThi()
        {
            grvThuPhi.DataSource = DataProvider.GetData(@"SELECT PHIEUTHU.IDPHIEUTHU, PHIEUTHU.NGAYTHU, PHIEUTHU.MATHUTHU,
                                                                 DOCGIA.TEN, LOAIPHI.TENLOAIPHI, LOAIPHI.SOTIEN
                                                          FROM DOCGIA INNER JOIN PHIEUTHU ON DOCGIA.MADG = PHIEUTHU.MADG
                                                                      INNER JOIN LOAIPHI ON PHIEUTHU.IDLOAIPHI = LOAIPHI.IDLOAIPHI");
        }

        private void frmThuPhi_Load(object sender, EventArgs e)
        {
            HienThi();
            txtMaTT.Text = PhanQuyen.MaTT;
            LockText();
            DataProvider.DataLookUpEdit(luDocGia, "SELECT * FROM DOCGIA", "MADG", "TEN", "Tên Độc Giả", "Mã Độ Giả");
            DataProvider.DataLookUpEdit(luPhi, "SELECT  IDLOAIPHI, TENLOAIPHI FROM LOAIPHI", "IDLOAIPHI", "TENLOAIPHI", "Tên Loại Phí", "Mã Phí");
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            UnLockText();
            txtMaTT.Text = PhanQuyen.MaTT;
            trangThai = true;
            txtMaPT.Text = DataProvider.AutoID("SELECT * FROM PHIEUTHU", "PTHU-");
            txtNgayThu.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (luDocGia.Text == "" || luPhi.Text == "" || txtMaPT.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }

            if (trangThai == true)
            {
                if (DataProvider.GetData("SELECT * FROM PHIEUTHU WHERE IDPHIEUTHU='" + txtMaPT.Text + "'").Rows.Count == 0)
                {
                    DataProvider.SqlExecuteNonQuery(@"INSERT INTO PHIEUTHU
                                                              VALUES(N'" + txtMaPT.Text
                                                                          + "','" + ConvertSQL.DaytoSQLInsert(txtNgayThu.Text)
                                                                          + "',N'" + txtMaTT.Text + "','" + luPhi.Text
                                                                          + "','" + luDocGia.Text + "')");
                    XtraMessageBox.Show("Thêm thành công");
                    HienThi();
                    LockText();
                    ClearText();
                }
                else
                {
                    XtraMessageBox.Show("Mã bạn nhập đả tồn tại");
                    txtMaPT.Focus();
                }
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE PHIEUTHU
                                                        SET MADG ='" + luDocGia.Text
                                                                     + "', IDLOAIPHI ='" + luPhi.Text
                                                                     + "' WHERE IDPHIEUTHU ='" + txtMaPT.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                LockText();
                HienThi();
                ClearText();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = false;
            txtMaPT.Enabled = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DataProvider.SqlExecuteNonQuery("DELETE PHIEUTHU WHERE IDPHIEUTHU = '" + txtMaPT.Text + "'");
            XtraMessageBox.Show("Xóa thông tin thành công");
            LockText();
            HienThi();
            ClearText();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaPT.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                txtNgayThu.Text = DateTime.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString()).ToString("dd/MM/yyyy");
                luDocGia.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
                luPhi.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
                txtMaTT.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[5]).ToString();
            }
            catch { }
            if (PhanQuyen.MaTT != txtMaTT.Text)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                XtraMessageBox.Show("Bạn không phải thủ thư tạo phiếu thu phí");
            }
            else
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
            }
        }

        //private void btnHuy_Click(object sender, EventArgs e)
        //{
        //    frmThuPhi_Load(sender, e);
        //    //HienThi();
        //}
    }
}