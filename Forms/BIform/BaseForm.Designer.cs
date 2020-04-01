namespace SGRSalary.Forms
{
    partial class BaseForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsMenuConfirm = new System.Windows.Forms.ToolStripButton();
            this.tsMenuNew = new System.Windows.Forms.ToolStripButton();
            this.tsMenuEdit = new System.Windows.Forms.ToolStripButton();
            this.tsMenuDelete = new System.Windows.Forms.ToolStripButton();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.dgvData = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuConfirm,
            this.tsMenuNew,
            this.tsMenuEdit,
            this.tsMenuDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1067, 27);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsMenuConfirm
            // 
            this.tsMenuConfirm.Image = global::SGRSalary.Properties.Resources.check;
            this.tsMenuConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuConfirm.Name = "tsMenuConfirm";
            this.tsMenuConfirm.Size = new System.Drawing.Size(57, 24);
            this.tsMenuConfirm.Text = "ثبت";
            this.tsMenuConfirm.Click += new System.EventHandler(this.TsMenuConfirm_Click);
            // 
            // tsMenuNew
            // 
            this.tsMenuNew.Image = global::SGRSalary.Properties.Resources.add2;
            this.tsMenuNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuNew.Name = "tsMenuNew";
            this.tsMenuNew.Size = new System.Drawing.Size(63, 24);
            this.tsMenuNew.Text = "جدید";
            this.tsMenuNew.Click += new System.EventHandler(this.TsMenuNew_Click);
            // 
            // tsMenuEdit
            // 
            this.tsMenuEdit.Image = global::SGRSalary.Properties.Resources.edit;
            this.tsMenuEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuEdit.Name = "tsMenuEdit";
            this.tsMenuEdit.Size = new System.Drawing.Size(79, 24);
            this.tsMenuEdit.Text = "ویرایش";
            this.tsMenuEdit.Click += new System.EventHandler(this.TsMenuEdit_Click);
            // 
            // tsMenuDelete
            // 
            this.tsMenuDelete.Image = global::SGRSalary.Properties.Resources.delete;
            this.tsMenuDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuDelete.Name = "tsMenuDelete";
            this.tsMenuDelete.Size = new System.Drawing.Size(65, 24);
            this.tsMenuDelete.Text = "حذف";
            this.tsMenuDelete.Click += new System.EventHandler(this.TsMenuDelete_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 27);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1067, 219);
            this.pnlControls.TabIndex = 16;
            // 
            // dgvData
            // 
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 246);
            this.dgvData.MainView = this.gridView1;
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(1067, 308);
            this.dgvData.TabIndex = 17;
            this.dgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dgvData;
            this.gridView1.Name = "gridView1";
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BaseForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsMenuConfirm;
        private System.Windows.Forms.ToolStripButton tsMenuNew;
        private System.Windows.Forms.ToolStripButton tsMenuEdit;
        private System.Windows.Forms.ToolStripButton tsMenuDelete;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public System.Windows.Forms.Panel pnlControls;
        public DevExpress.XtraGrid.GridControl dgvData;
    }
}