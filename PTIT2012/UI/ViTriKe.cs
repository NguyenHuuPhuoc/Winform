using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmViTriKe : DevExpress.XtraEditors.XtraForm
    {
        public frmViTriKe()
        {
            InitializeComponent();
        }

        private bool trangThai = false;
        private string IDKe;

        private void LockText()
        {
            txtKe.Enabled = false;

            btnThemVT.Enabled = true;
            btnSuaVT.Enabled = true;
            btnXoaVT.Enabled = true;
            btnLuaVT.Enabled = false;
        }

        private void UnLockText()
        {
            txtKe.Enabled = true;

            btnThemVT.Enabled = false;
            btnSuaVT.Enabled = false;
            btnXoaVT.Enabled = false;
            btnLuaVT.Enabled = true;
        }

        private void ClearText()
        {
            txtKe.Text = string.Empty;
        }

        private void HienThi()
        {
            grvViTri.DataSource = DataProvider.GetData("SELECT * FROM VITRIKE");
        }

        private void frmViTri_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
        }

        private void btnThemVT_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = true;
        }

        private void btnSuaVT_Click(object sender, EventArgs e)
        {
            UnLockText();
            trangThai = false;
        }

        private void btnLuaVT_Click(object sender, EventArgs e)
        {
            if (txtKe.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            if (trangThai == true)
            {
                DataProvider.SqlExecuteNonQuery(@"INSERT INTO VITRIKE
                                                              VALUES(N'" + txtKe.Text + "')");
                XtraMessageBox.Show("Thêm thành công");
                HienThi();
                LockText();
                ClearText();
            }
            else
            {
                DataProvider.SqlExecuteNonQuery("UPDATE VITRIKE SET TENVITRI = N'" + txtKe.Text +
                                                                "' WHERE IDVITRIKE = N'" + IDKe + "'");
                XtraMessageBox.Show("Sửa thành công");
                LockText();
                HienThi();
                ClearText();
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            LockText();
            IDKe = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            txtKe.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
        }

        private void btnXoaVT_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM VITRINGAN WHERE IDVITRIKE='" + IDKe + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE VITRIKE WHERE IDVITRIKE = N'" + IDKe + "'");
                XtraMessageBox.Show("Xóa thông tin thành công");
                LockText();
                HienThi();
                ClearText();
            }
            else
            {
                XtraMessageBox.Show("Ràng buộc khóa ngoại, bạn không được xóa");
            }
        }
    }
}