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
    public partial class frmRPInPMS : Form
    {
        public frmRPInPMS()
        {
            InitializeComponent();
        }
             public string SoPhieu;
        private void frmRPInPMS_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'dataSet1.CHITIETPHIEUMUON' table. You can move, or remove it, as needed.
            //this.cHITIETPHIEUMUONTableAdapter.Fill(this.dataSet1.CHITIETPHIEUMUON);
            this.reportViewer1.RefreshReport();
            reportViewer1.LocalReport.ReportEmbeddedResource = "PTIT2012.BAOCAO.rptInPMS.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            string cmd = @"SELECT SACH.MASACH,
                                  DAUSACH.TENDAUSACH,
                                  CHITIETPHIEUMUON.NGAYTRA 
                           FROM CHITIETPHIEUMUON INNER JOIN SACH ON CHITIETPHIEUMUON.MASACH = SACH.MASACH
                                            INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                            WHERE CHITIETPHIEUMUON.SOPM  = '" + SoPhieu + "'  ";
            Microsoft.Reporting.WinForms.ReportDataSource newDataSource
                    = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", DataProvider.GetData(cmd));
            string cmd1 = @"SELECT (THUTHU.HO+' '+ THUTHU.TEN) AS TenTT,
                                  (DOCGIA.HO+' ' + DOCGIA.TEN) AS TenDG,
                                  CONVERT(NVARCHAR, PHIEUMUONSACH.NGAYMUON,103) AS NGAYMUON
                            FROM PHIEUMUONSACH INNER JOIN THUTHU ON PHIEUMUONSACH.MATHUTHU = THUTHU.MATHUTHU
                                               INNER JOIN DOCGIA ON PHIEUMUONSACH.MADG = DOCGIA.MADG 
                                WHERE SOPM='" + SoPhieu + "'";
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("SoPM", SoPhieu);
            param[1] = new ReportParameter("ThuThu", DataProvider.GetData(cmd1).Rows[0][0].ToString());
            param[2] = new ReportParameter("NgayMuon", DataProvider.GetData(cmd1).Rows[0][2].ToString());
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
