using System;

namespace PTIT2012
{
    public partial class frmDanhSachPhieuTra : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachPhieuTra()
        {
            InitializeComponent();
        }

        private void HienThi()
        {
            msds.DataSource = DataProvider.GetData(@"SELECT DOCGIA.HO + ' ' + DOCGIA.TEN AS HOTEN,
                                                            THUTHU.HO + ' ' + THUTHU.TEN AS HOTENTT,
                                                            PHIEUTRASACH.SOPT, PHIEUTRASACH.MADG, PHIEUTRASACH.NGAYTRA
                                                    FROM THUTHU INNER JOIN PHIEUTRASACH ON THUTHU.MATHUTHU = PHIEUTRASACH.MATHUTHU
                                                                INNER JOIN DOCGIA ON PHIEUTRASACH.MADG = DOCGIA.MADG
                                                            ORDER BY PHIEUTRASACH.NGAYTRA DESC");
        }

        private void HienThi_Event(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmTraSach frm = new frmTraSach();
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
                {
                    return;
                }
                else
                {
                    frmTraSach frm = new frmTraSach();
                    frm.ShowInTaskbar = false;
                    frm.isInsert = false;
                    frm.SoPT = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                    frm.LamMoi_Cick += new EventHandler(HienThi_Event);
                    frm.ShowDialog();
                }
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