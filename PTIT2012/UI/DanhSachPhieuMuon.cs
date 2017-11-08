using System;

namespace PTIT2012
{
    public partial class frmDanhSachPhieuMuon : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachPhieuMuon()
        {
            InitializeComponent();
        }

        private void HienThi()
        {
            msds.DataSource = DataProvider.GetData(@"SELECT PHIEUMUONSACH.SOPM, PHIEUMUONSACH.MADG,
                                                            DOCGIA.HO + ' ' + DOCGIA.TEN AS HOTEN,
                                                            THUTHU.HO + ' ' + THUTHU.TEN AS HOTENTT, PHIEUMUONSACH.NGAYMUON
                                                     FROM PHIEUMUONSACH INNER JOIN THUTHU ON PHIEUMUONSACH.MATHUTHU = THUTHU.MATHUTHU
                                                                        INNER JOIN DOCGIA ON PHIEUMUONSACH.MADG = DOCGIA.MADG
                                                            ORDER BY PHIEUMUONSACH.NGAYMUON DESC");
        }

        private void HienThi_Event(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmMuonSach frm = new frmMuonSach();
            frm.ShowInTaskbar = false;
            frm.isInsert = true;
            frm.LamMoi_Cick += new EventHandler(HienThi_Event);
            frm.ShowDialog();
        }

        private void msds_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.RowCount <= 0)
                    return;
                frmMuonSach frm = new frmMuonSach();
                frm.ShowInTaskbar = false;
                frm.isInsert = false;
                frm.SoPM = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                frm.LamMoi_Cick += new EventHandler(HienThi_Event);
                frm.ShowDialog();
            }
            catch
            { }
        }

        private void frmDanhSachPhieuMuon_Load(object sender, EventArgs e)
        {
            HienThi();
        }
    }
}