using DevExpress.XtraEditors;
using PTIT2012.BAOCAO;
using System;

namespace PTIT2012
{
    public partial class frmMuonSach : DevExpress.XtraEditors.XtraForm
    {
        public frmMuonSach()
        {
            InitializeComponent();
        }

        public bool isInsert;
        public string SoPM;
        public bool quaHan = false;
        public int tongSo = 0;

        public event EventHandler LamMoi_Cick;

        public string cmd;

        private void hienthiCT()
        {
            msds.DataSource = DataProvider.GetData(@"SELECT CHITIETPHIEUMUON.MASACH, CHITIETPHIEUMUON.NGAYTRA,
                                                            DAUSACH.MADAUSACH, DAUSACH.TENDAUSACH,
                                                            CHITIETPHIEUMUON.TINHTRANG, CHITIETPHIEUMUON.GHICHU
                                                     FROM CHITIETPHIEUMUON INNER JOIN SACH ON CHITIETPHIEUMUON.MASACH = SACH.MASACH
                                                                           INNER JOIN DAUSACH ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                                                     WHERE SOPM='" + SoPM + "'");
        }

        private void LockText()
        {
            txtMa.Enabled = true;
            txtNgay.Enabled = false;
            txtTT.Enabled = false;
            btnXoa.Enabled = false;
            btnin.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void UnLockText()
        {
            txtMa.Enabled = false;
            txtNgay.Enabled = false;
            txtTT.Enabled = false;
            btnXoa.Enabled = true;
            btnin.Enabled = true;
            btnLuu.Enabled = false;
            cbDG.Enabled = false;
        }

        private void frmMuonSach_Load(object sender, EventArgs e)
        {
            cmd = @"SELECT MADG, HO + ' ' +TEN AS TEN FROM DOCGIA";
            //DataProvider.DataLookUpEdit(cbDG,cmd, "MADG", "TEN", "Tên ĐG", "Mã ĐG");
            DataProvider.DataLookUpEdit(cbDG, cmd, "TEN", "MADG", "Mã ĐG", "Tên ĐG");
            txtTT.Text = PhanQuyen.MaTT;
            if (isInsert == false)
            {
                UnLockText();
                hienthiCT();
                cmd = @"SELECT SACH.MASACH, DAUSACH.TENDAUSACH
                        FROM SACH INNER JOIN DAUSACH
                                        ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                            WHERE SACH.KIEUSACH= N'Cho mượn' ";
                DataProvider.LoadComboxinXtg(cbSach, DataProvider.GetData(cmd), "TENDAUSACH", "MASACH", "Mã Sách", "Tên Sách");
                string sql = @"SELECT PHIEUMUONSACH.SOPM, PHIEUMUONSACH.MADG,
                                      DOCGIA.HO + ' ' + DOCGIA.TEN AS HOTEN,
                                      THUTHU.HO + ' ' + THUTHU.TEN AS HOTENTT,
                                      CONVERT(nvarchar,PHIEUMUONSACH.NGAYMUON,103) AS NGAYMUON
                              FROM PHIEUMUONSACH INNER JOIN THUTHU ON PHIEUMUONSACH.MATHUTHU = THUTHU.MATHUTHU
                                                 INNER JOIN DOCGIA ON PHIEUMUONSACH.MADG = DOCGIA.MADG
                                    WHERE PHIEUMUONSACH.SOPM = '" + SoPM + "'";
                txtMa.Text = SoPM;
                txtNgay.Text = DataProvider.GetData(sql).Rows[0]["NGAYMUON"].ToString();
                txtTT.Text = DataProvider.GetData(sql).Rows[0]["HOTENTT"].ToString();
                cbDG.EditValue = DataProvider.GetData(sql).Rows[0]["MADG"].ToString();
            }
            else
            {
                LockText();
                txtMa.Text = DataProvider.AutoID("SELECT * FROM PHIEUMUONSACH", "PM-");
                hienthiCT();
                txtNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                cmd = @"SELECT SACH.MASACH, DAUSACH.TENDAUSACH
                        FROM SACH INNER JOIN DAUSACH
                                    ON SACH.MADAUSACH = DAUSACH.MADAUSACH
                        WHERE TINHTRANG=N'Trống' AND SACH.KIEUSACH=N'Cho mượn' ";
                DataProvider.LoadComboxinXtg(cbSach, DataProvider.GetData(cmd), "TENDAUSACH", "MASACH", "Mã Sách", "Tên Sách");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string maSach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                //SP
                cmd = @"EXECUTE SP_XOAPHIEUVACTPMUONSACH N'" + SoPM
                                                                 + "', N'" + maSach + "'";
                DataProvider.SqlExecuteNonQuery(cmd);
                // PROJECT
                //DataProvider.SqlExecuteNonQuery("DELETE FROM CHITIETPHIEUMUON WHERE SOPM='" + SoPM + "'");
                //DataProvider.SqlExecuteNonQuery("DELETE FROM PHIEUMUONSACH WHERE SOPM='" + SoPM + "'");
                //DataProvider.SqlExecuteNonQuery("UPDATE SACH SET TINHTRANG =N'Trống' WHERE MASACH = '" + maSach + "'");
            }
            if (this.LamMoi_Cick != null)
                this.LamMoi_Cick(sender, e);
            XtraMessageBox.Show("Xóa thành công");
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (quaHan == true)
                return;
            if ((tongSo + gridView1.RowCount > 3))
            {
                XtraMessageBox.Show("Độc giả đã mượn vượt quá số sách quy định là 3");
                return;
            }
            if (gridView1.RowCount > 0)
            {
                if (cbDG.Text == "" || txtMa.Text == "")
                {
                    XtraMessageBox.Show("Bạn nhập thiếu thông tin");
                    return;
                }
                if (isInsert == true)
                {
                    if (DataProvider.GetData("SELECT * FROM PHIEUMUONSACH WHERE SOPM =N'" + txtMa.Text + "'").Rows.Count == 0)
                    {
                        DataProvider.SqlExecuteNonQuery(@"INSERT INTO PHIEUMUONSACH(SOPM, MADG, NGAYMUON, MATHUTHU, TINHTRANG)
                                                                  VALUES('" + txtMa.Text
                                                                           + "','" + cbDG.EditValue
                                                                           + "',GETDATE(),'"
                                                                           + PhanQuyen.MaTT
                                                                           + "','true')");

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            string maSach = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                            string ngay = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")).AddDays(2).ToString("dd/MM/yyyy");
                            string ghiChu = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();
                            //SP
                            cmd = @"EXECUTE SP_SAVECTPHIEUMUON N'" + txtMa.Text
                                                                      + "', N'" + maSach
                                                                      + "', N'" + ConvertSQL.DaytoSQLInsert(ngay)
                                                                      + "', N'" + ghiChu
                                                                      + "','False', 'False'";
                            DataProvider.SqlExecuteNonQuery(cmd);
                            // PROJECT
                            //                            DataProvider.SqlExecuteNonQuery(@"INSERT INTO CHITIETPHIEUMUON (SOPM, MASACH, NGAYTRA, GHICHU, TINHTRANG,GIAHAN)
                            //                                                                    VALUES (N'" + txtMa.Text
                            //                                                                                + "',N'" + maSach
                            //                                                                                + "',N'" + ConvertSQL.DaytoSQLInsert(ngay)
                            //                                                                                + "',N'" + ghiChu
                            //                                                                                + "','False','False')");
                            //                            DataProvider.SqlExecuteNonQuery(@"UPDATE SACH
                            //                                                                SET TINHTRANG=N'Sách đang mượn'
                            //                                                                WHERE MASACH='" + maSach + "' ");
                        }
                        if (this.LamMoi_Cick != null)
                            this.LamMoi_Cick(sender, e);
                        XtraMessageBox.Show("Thêm thành công");
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("Mã bạn vừa nhập đã tồn tại");
                        txtMa.Focus();
                    }
                }
            }
        }

        private void cbDG_EditValueChanged(object sender, EventArgs e)
        {
            cmd = @"SELECT * FROM CHITIETPHIEUMUON
                                    INNER JOIN PHIEUMUONSACH ON CHITIETPHIEUMUON.SOPM = PHIEUMUONSACH.SOPM
                                    INNER JOIN DOCGIA ON PHIEUMUONSACH.MADG = DOCGIA.MADG
                            WHERE DOCGIA.MaDG='" + cbDG.EditValue + "' AND CHITIETPHIEUMUON.TINHTRANG='False'";
            tongSo = DataProvider.GetData(cmd).Rows.Count;
            if (tongSo >= 3)
            {
                XtraMessageBox.Show("Độc giả này đã mượn 3 cuốn. Không được mượn tiếp");
                return;
            }

            quaHan = checkQH(cbDG.EditValue);
            if (quaHan == true)
            {
                XtraMessageBox.Show("Độc giả này có sách quá hạn, Nên không được mượn tiếp");
                return;
            }
        }

        private bool checkQH(object madg)
        {
            cmd = @"SELECT CONVERT(nvarchar, CHITIETPHIEUMUON.NGAYTRA, 103) AS NGAYTRA,
                           PHIEUMUONSACH.NGAYMUON
                    FROM   PHIEUMUONSACH INNER JOIN CHITIETPHIEUMUON ON PHIEUMUONSACH.SOPM = CHITIETPHIEUMUON.SOPM
                   WHERE  (CHITIETPHIEUMUON.TINHTRANG = 'false') AND (CHITIETPHIEUMUON.NGAYTRA < GETDATE())
                           AND (PHIEUMUONSACH.MADG = '" + madg + "')";
            if (DataProvider.GetData(cmd).Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnin_Click(object sender, EventArgs e)
        {
            frmRPInPMS fr = new frmRPInPMS();
            fr.SoPhieu = txtMa.Text;
            fr.ShowInTaskbar = false;
            fr.ShowDialog();
        }
    }
}