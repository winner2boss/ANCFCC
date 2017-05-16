namespace Consutation_Controle_Validation
{
    partial class detailsRequette
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            this.myGridView = new Telerik.WinControls.UI.RadGridView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ListeChoixExtention = new Telerik.WinControls.UI.RadListControl();
            this.btnExporter = new Telerik.WinControls.UI.RadButton();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.waiting = new Telerik.WinControls.UI.RadWaitingBar();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListeChoixExtention)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExporter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // myGridView
            // 
            this.myGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.myGridView.Location = new System.Drawing.Point(13, 13);
            // 
            // myGridView
            // 
            this.myGridView.MasterTemplate.AllowAddNewRow = false;
            this.myGridView.MasterTemplate.AllowDeleteRow = false;
            this.myGridView.MasterTemplate.AllowEditRow = false;
            this.myGridView.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.myGridView.Name = "myGridView";
            this.myGridView.ReadOnly = true;
            this.myGridView.Size = new System.Drawing.Size(593, 308);
            this.myGridView.TabIndex = 0;
            this.myGridView.Text = "myGridView";
            // 
            // ListeChoixExtention
            // 
            this.ListeChoixExtention.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            radListDataItem1.Text = "Expotrer en Excel";
            radListDataItem1.TextWrap = true;
            this.ListeChoixExtention.Items.Add(radListDataItem1);
            this.ListeChoixExtention.Location = new System.Drawing.Point(610, 13);
            this.ListeChoixExtention.Name = "ListeChoixExtention";
            this.ListeChoixExtention.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ListeChoixExtention.Size = new System.Drawing.Size(134, 131);
            this.ListeChoixExtention.TabIndex = 54;
            this.ListeChoixExtention.Text = "listeChoix";
            this.ListeChoixExtention.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ListeChoixExtention_SelectedIndexChanged);
            // 
            // btnExporter
            // 
            this.btnExporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExporter.EnableCodedUITests = true;
            this.btnExporter.Enabled = false;
            this.btnExporter.Location = new System.Drawing.Point(610, 180);
            this.btnExporter.Name = "btnExporter";
            this.btnExporter.Size = new System.Drawing.Size(134, 33);
            this.btnExporter.TabIndex = 55;
            this.btnExporter.Text = "Exporter";
            this.btnExporter.Click += new System.EventHandler(this.btnExporter_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // waiting
            // 
            this.waiting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.waiting.Location = new System.Drawing.Point(610, 150);
            this.waiting.Name = "waiting";
            this.waiting.Size = new System.Drawing.Size(134, 24);
            this.waiting.TabIndex = 56;
            this.waiting.Text = "waiting";
            this.waiting.Visible = false;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // detailsRequette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 360);
            this.Controls.Add(this.waiting);
            this.Controls.Add(this.btnExporter);
            this.Controls.Add(this.ListeChoixExtention);
            this.Controls.Add(this.myGridView);
            this.MinimizeBox = false;
            this.Name = "detailsRequette";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Détails";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.detailsRequette_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListeChoixExtention)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExporter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView myGridView;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Telerik.WinControls.UI.RadListControl ListeChoixExtention;
        private Telerik.WinControls.UI.RadButton btnExporter;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Telerik.WinControls.UI.RadWaitingBar waiting;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;



    }
}
