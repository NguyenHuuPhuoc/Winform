using DevExpress.XtraEditors;
using System;

namespace PTIT2012
{
    public partial class frmViTriNgan : DevExpress.XtraEditors.XtraForm
    {
        public frmViTriNgan()
        {
            InitializeComponent();
        }

        private bool trangThai = false;
        private string IDNgan;

        private void LockText()
        {
            txtNgan.Enabled = false;
            sluKe.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void UnLockText()
        {
            txtNgan.Enabled = true;
            sluKe.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void ClearText()
        {
            txtNgan.Text = string.Empty;
            //cbKe.ClosePopup();
            sluKe.Text = string.Empty;
        }

        private void HienThi()
        {
            grvViTri.DataSource = DataProvider.GetData(@"SELECT VITRIKE.IDVITRIKE,VITRIKE.TENVITRI,
                                                                VITRINGAN.IDNGAN, VITRINGAN.TENVITRI AS VITRINGAN
                                                        FROM VITRINGAN
                                                             INNER JOIN VITRIKE ON VITRINGAN.IDVITRIKE = VITRIKE.IDVITRIKE");
        }

        private void frmViTri_Load(object sender, EventArgs e)
        {
            HienThi();
            LockText();
            DataProvider.DataSearchLookUpEdit(sluKe, "SELECT * FROM VITRIKE", "TENVITRI", "IDVITRIKE");
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
            if (txtNgan.Text == "" || sluKe.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            if (trangThai == true)
            {
                DataProvider.SqlExecuteNonQuery(@"INSERT INTO VITRINGAN(IDVITRIKE, TENVITRI)
                                                             VALUES(N'" + sluKe.EditValue
                                                                        + "',N'" + txtNgan.Text + "')");
                XtraMessageBox.Show("Thêm thành công");
                HienThi();
                LockText();
                ClearText();
            }
            else
            {
                DataProvider.SqlExecuteNonQuery(@"UPDATE VITRINGAN
                                                         SET IDVITRIKE =N'" + sluKe.EditValue
                                                                            + "', TENVITRI =N'" + txtNgan.Text
                                                                            + "' WHERE IDNGAN ='" + IDNgan + "'");
                XtraMessageBox.Show("Sửa thành công");
                HienThi();
                LockText();
                ClearText();
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                LockText();
                IDNgan = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                sluKe.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString();
                txtNgan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[2]).ToString();
            }
            catch
            { }
        }

        private void btnXoaVT_Click(object sender, EventArgs e)
        {
            if (DataProvider.GetData("SELECT * FROM DAUSACH WHERE IDNGAN = N'" + IDNgan + "'").Rows.Count == 0)
            {
                DataProvider.SqlExecuteNonQuery("DELETE VITRINGAN WHERE IDNGAN = N'" + IDNgan + "'");
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