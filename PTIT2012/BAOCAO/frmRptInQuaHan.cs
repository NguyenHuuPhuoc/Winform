using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTIT2012.BAOCAO
{
    public partial class frmRptInQuaHan : Form
    {
        public frmRptInQuaHan()
        {
            InitializeComponent();
        }

        private void frmRptInQuaHan_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();

            reportViewer1.LocalReport.ReportEmbeddedResource = "PTIT2012.BAOCAO.rptSachQuaHan.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource newDataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1",
                DataProvider.GetData(@"SELECT SACH.MASACH, DAUSACH.TENDAUSACH, PHIEUMUONSACH.SOPM, 
                                              CONVERT(nvarchar, CHITIETPHIEUMUON.NGAYTRA, 103) AS NGAYTRA, 
                                              (DOCGIA.HO+' '+ DOCGIA.TEN) AS HOTEN
                                    FROM SACH INNER JOIN
                                         DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH INNER JOIN
                                         CHITIETPHIEUMUON ON SACH.MASACH = CHITIETPHIEUMUON.MASACH INNER JOIN
                                         PHIEUMUONSACH ON CHITIETPHIEUMUON.SOPM = PHIEUMUONSACH.SOPM INNER JOIN
                                         DOCGIA ON PHIEUMUONSACH.MADG = DOCGIA.MADG
                                    WHERE (CHITIETPHIEUMUON.TINHTRANG = 'false') AND (CHITIETPHIEUMUON.NGAYTRA < GETDATE())"));
            reportViewer1.LocalReport.DataSources.Add(newDataSource);
            reportViewer1.LocalReport.DisplayName = "Báo Cáo";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }
    }
}
