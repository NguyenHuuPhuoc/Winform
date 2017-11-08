using Microsoft.Reporting.WinForms;
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
    public partial class frmRPInPTS : Form
    {
        public frmRPInPTS()
        {
            InitializeComponent();
        }
        public string SoPhieu;
        private void frmRPInPTS_Load(object sender, EventArgs e)
        {
            //TODO: This line of code loads data into the 'dataSet1.CHITIETPHIEUTRA' table. You can move, or remove it, as needed.
            // this.cHITIETPHIEUTRATableAdapter.Fill(this.dataSet1.CHITIETPHIEUTRA);
            this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportEmbeddedResource = "PTIT2012.BAOCAO.rptInPTS.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            string cmd = @"SELECT SACH.MASACH,
                                 DAUSACH.TENDAUSACH,
                                 CHITIETPHIEUTRA.GHICHU 
                           FROM SACH INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                                     INNER JOIN CHITIETPHIEUTRA ON SACH.MASACH = CHITIETPHIEUTRA.MASACH 
                                WHERE CHITIETPHIEUTRA.SOPT  = '" + SoPhieu + "'  ";
            Microsoft.Reporting.WinForms.ReportDataSource newDataSource = 
                new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", DataProvider.GetData(cmd));

            string cmd1 = @"SELECT THUTHU.HO + ' ' + THUTHU.TEN AS TenTT, 
                                   DOCGIA.HO + ' ' + DOCGIA.TEN AS TenDG,
                                  CONVERT(NVARCHAR, PHIEUTRASACH.NGAYTRA,103) AS NGAYTRA
                            FROM THUTHU INNER JOIN PHIEUTRASACH ON THUTHU.MATHUTHU = PHIEUTRASACH.MATHUTHU
                                        INNER JOIN DOCGIA ON PHIEUTRASACH.MADG = DOCGIA.MADG
                                WHERE SOPT='" + SoPhieu + "'";
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("SoPT", SoPhieu);
            param[1] = new ReportParameter("ThuThu", DataProvider.GetData(cmd1).Rows[0][0].ToString());
            param[2] = new ReportParameter("NgayTra", DataProvider.GetData(cmd1).Rows[0][2].ToString());
            param[3] = new ReportParameter("DocGia", DataProvider.GetData(cmd1).Rows[0]["TenDG"].ToString());
            reportViewer1.LocalReport.SetParameters(param);
            reportViewer1.LocalReport.DataSources.Add(newDataSource);
            reportViewer1.LocalReport.DisplayName = "Báo Cáo";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }
    }
}
