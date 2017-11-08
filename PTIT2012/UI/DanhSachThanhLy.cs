using System;

namespace PTIT2012
{
    public partial class frmDanhSachThanhLy : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachThanhLy()
        {
            InitializeComponent();
        }

        private void HienThi()
        {
            msds.DataSource = DataProvider.GetData(@"SELECT IDTHANHLY, NGAYTHANHLY
                                                     FROM THANHLY
                                                        ORDER BY NGAYTHANHLY DESC");
        }

        private void HienThi_Event(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThanhLy frm = new frmThanhLy();
            frm.ShowInTaskbar = false;
            frm.isInsert = true;
            frm.LamMoi_Cick += new EventHandler(HienThi_Event);
            frm.ShowDialog();
        }

        private void msds_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
                return;
            frmThanhLy frm = new frmThanhLy();
            frm.ShowInTaskbar = false;
            frm.isInsert = false;
            frm.SoPTL = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
            frm.LamMoi_Cick += new EventHandler(HienThi_Event);
            frm.ShowDialog();
        }

        private void frmDanhSachPhieuMuon_Load(object sender, EventArgs e)
        {
            HienThi();
        }
    }
}