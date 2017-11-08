using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace PTIT2012
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (txtTenDN.Text == "" || txtPass.Text == "")
            {
                XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                return;
            }
            else
            {
                if (DataProvider.GetData("SELECT * FROM THUTHU WHERE MATHUTHU = '" + txtTenDN.Text + "' AND  MATKHAU='" +
                    txtPass.Text + "'").Rows.Count == 1)
                {
                    PhanQuyen.MaTT = txtTenDN.Text;
                    PhanQuyen.Role = DataProvider.GetData("SELECT * FROM THUTHU WHERE MATHUTHU ='" + txtTenDN.Text
                        + "' AND MATKHAU='" + txtPass.Text + "'").Rows[0]["ISADMIN"].ToString();
                    frmMain fr = new frmMain();
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    XtraMessageBox.Show("Thông tin đăng nhập chưa chính xác");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}