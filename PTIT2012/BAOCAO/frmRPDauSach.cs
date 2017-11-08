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
    public partial class frmRPDauSach : Form
    {
        public frmRPDauSach()
        {
            InitializeComponent();
        }

        private void frmRPDauSach_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.DAUSACH' table. You can move, or remove it, as needed.
            this.dAUSACHTableAdapter.Fill(this.dataSet1.DAUSACH);
            this.reportViewer1.RefreshReport();
        
        }
    }
}
