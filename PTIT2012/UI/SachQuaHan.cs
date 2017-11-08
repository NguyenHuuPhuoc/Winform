using PTIT2012.BAOCAO;
using System;

namespace PTIT2012
{
    public partial class frmSachQuaHan : DevExpress.XtraEditors.XtraForm
    {
        public frmSachQuaHan()
        {
            InitializeComponent();
        }

        private void frmSachQuaHan_Load(object sender, EventArgs e)
        {
            try
            {
                msds.DataSource = DataProvider.GetData(@"SELECT SACH.MASACH, DAUSACH.TENDAUSACH,
                                                                PHIEUMUONSACH.SOPM,
                                                                CONVERT(nvarchar, CHITIETPHIEUMUON.NGAYTRA, 103) AS NGAYTRA,
                                                                (+ DOCGIA.HO+' '+ DOCGIA.TEN) AS HOTEN
                                                         FROM SACH
                                                              INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                                                              INNER JOIN CHITIETPHIEUMUON ON SACH.MASACH = CHITIETPHIEUMUON.MASACH
                                                              INNER JOIN PHIEUMUONSACH ON CHITIETPHIEUMUON.SOPM = PHIEUMUONSACH.SOPM
                                                              INNER JOIN DOCGIA ON PHIEUMUONSACH.MADG = DOCGIA.MADG
                                                        WHERE (CHITIETPHIEUMUON.TINHTRANG = 'false') AND (CHITIETPHIEUMUON.NGAYTRA < GETDATE())");
                gridView1.ExpandAllGroups();
            }
            catch
            {
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmRptInQuaHan frm = new frmRptInQuaHan();
            frm.ShowInTaskbar = false;
            frm.ShowDialog();
        }
    }
}