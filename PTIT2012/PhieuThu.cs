using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PTIT2012
{
    public partial class frmPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieuThu()
        {
            InitializeComponent();
        }
        bool trangThai = false;
        void LockText()
        {
            sluDocGia.Enabled = false;
            sluLoaiPhi.Enabled = false;
            txtMaPT.Enabled = false;
            txtMaTT.Enabled = false;
            txtNgayThu.Enabled = false;
            btnLuu.Enabled = false;
            //
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
        }
        void UnLockText()
        {
            sluDocGia.Enabled = true;
            sluLoaiPhi.Enabled = true;
            txtMaPT.Enabled = true;
            txtMaTT.Enabled = false;
            txtNgayThu.Enabled = true;
            btnLuu.Enabled = true;
            //
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }
        void ClearText()
        {
            txtMaPT.Text = string.Empty;
            sluLoaiPhi.Text = string.Empty;
            sluDocGia.Text = string.Empty;
            txtNgayThu.Text = string.Empty;
            txtMaTT.Text = string.Empty;
        }
        void HienThi()
        {
            grvPhieuThu.DataSource = DataProvider.GetData(@"SELECT PHIEUTHU.IDPHIEUTHU, PHIEUTHU.NGAYTHU, PHIEUTHU.MATHUTHU,
                                                                   DOCGIA.MADG,DOCGIA.TEN, 
                                                                   LOAIPHI.IDLOAIPHI, LOAIPHI.TENLOAIPHI, LOAIPHI.SOTIEN 
                                                          FROM DOCGIA INNER JOIN PHIEUTHU ON DOCGIA.MADG = PHIEUTHU.MADG 
                                                                      INNER JOIN LOAIPHI ON PHIEUTHU.IDLOAIPHI = LOAIPHI.IDLOAIPHI");
        }
        private void frmPhieuThu_Load(object sender, EventArgs e)
        {
            txtMaTT.Text = PhanQuyen.MaTT;
            HienThi();
            DataProvider.DataSearchLookUpEdit(sluDocGia, "SELECT * FROM DOCGIA", "TEN", "MADG");
            DataProvider.DataSearchLookUpEdit(sluLoaiPhi, "SELECT * FROM LOAIPHI", "TENLOAIPHI", "IDLOAIPHI");
            LockText();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            UnLockText();
            txtMaTT.Text = PhanQuyen.MaTT;
            trangThai = true;
            txtMaPT.Text = DataProvider.AutoID("SELECT * FROM PHIEUTHU", "PTHU-");
            txtNgayThu.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = false;
            txtMaPT.Enabled = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (sluDocGia.Text == "" || sluLoaiPhi.Text == "" || txtMaPT.Text == "")
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
                                                                          + "',N'" + txtMaTT.Text + "','" + sluLoaiPhi.EditValue
                                                                          + "','" + sluDocGia.EditValue + "')");
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
                                                        SET MADG ='" + sluDocGia.EditValue
                                                                     + "', IDLOAIPHI ='" + sluLoaiPhi.EditValue
                                                                     + "' WHERE IDPHIEUTHU ='" + txtMaPT.Text + "'");
                XtraMessageBox.Show("Sửa thành công");
                LockText();
                HienThi();
                ClearText();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataProvider.SqlExecuteNonQuery("DELETE PHIEUTHU WHERE IDPHIEUTHU = '" + txtMaPT.Text + "'");
            XtraMessageBox.Show("Xóa thông tin thành công");
            LockText();
            HienThi();
            ClearText();
        }
        private void gridView3_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaPT.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, gridView3.Columns["IDPHIEUTHU"]).ToString();
                txtNgayThu.Text = DateTime.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, gridView3.Columns["NGAYTHU"]).ToString()).ToString("dd/MM/yyyy");
                sluDocGia.EditValue = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, gridView3.Columns["MADG"]).ToString();
                sluLoaiPhi.EditValue = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, gridView3.Columns["IDLOAIPHI"]).ToString();
                txtMaTT.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, gridView3.Columns["MATHUTHU"]).ToString();
            }
            catch { }
            if (PhanQuyen.MaTT != txtMaTT.Text)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                XtraMessageBox.Show("Bạn không phải thủ thư tạo phiếu thu phí, bạn không có quyền thực hiện nghiệp vụ");
            }
            else
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
            }
        }
    }
}