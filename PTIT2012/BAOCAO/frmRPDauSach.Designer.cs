namespace PTIT2012.BAOCAO
{
    partial class frmRPDauSach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dAUSACHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new PTIT2012.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dAUSACHTableAdapter = new PTIT2012.DataSet1TableAdapters.DAUSACHTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dAUSACHBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // dAUSACHBindingSource
            // 
            this.dAUSACHBindingSource.DataMember = "DAUSACH";
            this.dAUSACHBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dAUSACHBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PTIT2012.BAOCAO.rptDauSach.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(564, 261);
            this.reportViewer1.TabIndex = 0;
            // 
            // dAUSACHTableAdapter
            // 
            this.dAUSACHTableAdapter.ClearBeforeFill = true;
            // 
            // frmRPDauSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 261);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmRPDauSach";
            this.Text = "Báo Cáo Đầu Sách";
            this.Load += new System.EventHandler(this.frmRPDauSach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dAUSACHBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource dAUSACHBindingSource;
        private DataSet1TableAdapters.DAUSACHTableAdapter dAUSACHTableAdapter;
    }
}