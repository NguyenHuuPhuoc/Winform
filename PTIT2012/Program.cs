using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using PTIT2012.BAOCAO;

namespace PTIT2012
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            //Application.Run(new DeMo());
           //Application.Run(new frmRPDauSach());
           // Application.Run(new frmMain());
            //Application.Run(new frmPhieuThu());
           Application.Run(new frmDangNhap());
            //Application.Run(new frmDanhSachPhieuMuon());
            //Application.Run(new frmDanhSachPhieuTra());
            //Application.Run(new frmDanhSachThanhLy());
            //Application.Run(new frmDauSach());
            //Application.Run(new frmDocGia());
            //Application.Run(new frmGiaHan());
            //Application.Run(new frmloaiphi());
            //Application.Run(new frmLoaiSach());
            //Application.Run(new frmMuonSach());   
            //Application.Run(new frmNhaSanXuat());
            //Application.Run(new frmSach());
            //Application.Run(new frmSachQuaHan());
            //Application.Run(new frmSangTac());
            //Application.Run(new frmTacGia());
            //Application.Run(new frmThanhLy());
            //Application.Run(new frmthuphi());
            //Application.Run(new frmThuThu());
            //Application.Run(new frmTraSach());
            //Application.Run(new frmViTriKe());
            //Application.Run(new frmViTriNgan());
        }
    }
}
