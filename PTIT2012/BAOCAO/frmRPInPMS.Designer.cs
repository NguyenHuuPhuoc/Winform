namespace PTIT2012.BAOCAO
{
    partial class frmRPInPMS
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
            this.cHITIETPHIEUMUONBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new PTIT2012.DataSet1();
            this.cHITIETPHIEUMUONTableAdapter = new PTIT2012.DataSet1TableAdapters.CHITIETPHIEUMUONTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.cHITIETPHIEUMUONBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // cHITIETPHIEUMUONBindingSource
            // 
            this.cHITIETPHIEUMUONBindingSource.DataMember = "CHITIETPHIEUMUON";
            this.cHITIETPHIEUMUONBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cHITIETPHIEUMUONTableAdapter
            // 
            this.cHITIETPHIEUMUONTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.cHITIETPHIEUMUONBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PTIT2012.BAOCAO.rptInPMS.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(718, 524);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmRPInPMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 524);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmRPInPMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Phiếu Mượn Sách";
            this.Load += new System.EventHandler(this.frmRPInPMS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cHITIETPHIEUMUONBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource cHITIETPHIEUMUONBindingSource;
        private DataSet1TableAdapters.CHITIETPHIEUMUONTableAdapter cHITIETPHIEUMUONTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}