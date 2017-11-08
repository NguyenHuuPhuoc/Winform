using PTIT2012.BAOCAO;
using System;
using System.Windows.Forms;

namespace PTIT2012
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private Form CheckTonTai(Type formtype)
        {
            foreach (Form f in this.MdiChildren)// duyệt từng form con
            {
                if (f.GetType() == formtype)
                    return f;
            }
            return null;
        }

        private void mnLoaiSach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmLoaiSach));
            if (frm != null)       // nếu tồn tại
                frm.Activate();     //kích hoạt
            else
            {
                frmLoaiSach fr = new frmLoaiSach();
                fr.MdiParent = this;  // ôm lên form cha
                fr.Show();            // show form con lên
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmNhaSanXuat));
            if (frm != null)
                frm.Activate();
            else
            {
                frmNhaSanXuat fr = new frmNhaSanXuat();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void mnThuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmThuThu));
            if (frm != null)
                frm.Activate();
            else
            {
                frmThuThu fr = new frmThuThu();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmViTriKe));
            if (frm != null)
                frm.Activate();
            else
            {
                frmViTriKe fr = new frmViTriKe();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmViTriNgan));
            if (frm != null)
                frm.Activate();
            else
            {
                frmViTriNgan fr = new frmViTriNgan();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmTacGia));
            if (frm != null)
                frm.Activate();
            else
            {
                frmTacGia fr = new frmTacGia();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmDauSach));
            if (frm != null)
                frm.Activate();
            else
            {
                frmDauSach fr = new frmDauSach();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void btnThongTinDocGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmDocGia));
            if (frm != null)
                frm.Activate();
            else
            {
                frmDocGia fr = new frmDocGia();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void mnPM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmDanhSachPhieuMuon));
            if (frm != null)
                frm.Activate();
            else
            {
                frmDanhSachPhieuMuon fr = new frmDanhSachPhieuMuon();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void mnTraSach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmDanhSachPhieuTra));
            if (frm != null)
                frm.Activate();
            else
            {
                frmDanhSachPhieuTra fr = new frmDanhSachPhieuTra();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void mnSach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmSach));
            if (frm != null)
                frm.Activate();
            else
            {
                frmSach fr = new frmSach();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmRPDauSach));
            if (frm != null)
                frm.Activate();
            else
            {
                frmRPDauSach fr = new frmRPDauSach();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmRPDocGia));
            if (frm != null)
                frm.Activate();
            else
            {
                frmRPDocGia fr = new frmRPDocGia();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void mnGiaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmGiaHan));
            if (frm != null)
                frm.Activate();
            else
            {
                frmGiaHan fr = new frmGiaHan();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void mnQuaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmSachQuaHan));
            if (frm != null)
                frm.Activate();
            else
            {
                frmSachQuaHan fr = new frmSachQuaHan();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (PhanQuyen.Role.Equals("False"))
            {
                mnThuThu.Enabled = false;
            }
        }

        private void mnLoaiPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmloaiphi));
            if (frm != null)
                frm.Activate();
            else
            {
                frmloaiphi fr = new frmloaiphi();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmloaiphi));
            if (frm != null)
                frm.Activate();
            else
            {
                frmloaiphi fr = new frmloaiphi();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmloaiphi));
            if (frm != null)
                frm.Activate();
            else
            {
                frmloaiphi fr = new frmloaiphi();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmThuPhi));
            if (frm != null)
                frm.Activate();
            else
            {
                frmThuPhi fr = new frmThuPhi();
                fr.MdiParent = this;
                fr.Show();
            }
            //Form frm = CheckTonTai(typeof(frmPhieuThu));
            //if (frm != null)
            //    frm.Activate();
            //else
            //{
            //    frmPhieuThu fr = new frmPhieuThu();
            //    fr.MdiParent = this;
            //    fr.Show();
            //}
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDangNhap fr = new frmDangNhap();
            this.Visible = false;
            fr.Show();
        }

        private void mndsthanhly_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = CheckTonTai(typeof(frmDanhSachThanhLy));
            if (frm != null)
                frm.Activate();
            else
            {
                frmDanhSachThanhLy fr = new frmDanhSachThanhLy();
                fr.MdiParent = this;
                fr.Show();
            }
        }
    }
}