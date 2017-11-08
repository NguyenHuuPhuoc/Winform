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
    public partial class frmloaiphi : DevExpress.XtraEditors.XtraForm
    {
        public frmloaiphi()
        {
            InitializeComponent();
        }
        bool trangThai = false;
        string IDPhi;
        void LockText()
        {
            txtLoaiPhi.Enabled = false;
            txtTienPhi.Enabled = false;
            btnthem.Enabled = true;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnluu.Enabled = false;
        }

        void UnLockText()
        {
            txtLoaiPhi.Enabled = true;
            txtTienPhi.Enabled = true;
            btnthem.Enabled = false;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnluu.Enabled = true;
        }
        void ClearText()
        {
            txtLoaiPhi.Text = string.Empty;
            txtTienPhi.Text = string.Empty;
        }
        void HienThi()
        {
            grvLoaiPhi.DataSource = DataProvider.GetData(@"SELECT* FROM LOAIPHI");
        }
        private void frmloaiphi_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = true;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (txtLoaiPhi.Text == "" || txtTienPhi.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }

            if (trangThai == true)
            {
                DataProvider.SqlExecuteNonQuery(@"INSERT INTO LOAIPHI (TENLOAIPHI, SOTIEN) 
                                                             VALUES(N'" + txtLoaiPhi.Text
                                                                        + "','" + txtTienPhi.Text + "')");
                XtraMessageBox.Show("Thêm thành công");
                HienThi();
                LockText();
                ClearText();

            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE LOAIPHI 
                                                         SET TENLOAIPHI =N'" + txtLoaiPhi.Text
                                                                             + "', SOTIEN ='" + txtTienPhi.Text
                                                                             + "'  WHERE IDLOAIPHI ='" + IDPhi + "'");
                XtraMessageBox.Show("Sửathành công");
                LockText();
                HienThi();
                ClearText();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM PHIEUTHU WHERE IDLOAIPHI=N'" + IDPhi + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE LOAIPHI where IDLOAIPHI = N'" + IDPhi + "'");
                XtraMessageBox.Show("Xóa thành công");
                LockText();
                HienThi();
                ClearText();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại bạn không được xóa");
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                IDPhi = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                txtLoaiPhi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
                txtTienPhi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            }
            catch
            { }
        }
    }
}