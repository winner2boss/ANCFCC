namespace Consutation_Controle_Validation
{
    partial class choixStatue
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
            this.btnVerifier = new Telerik.WinControls.UI.RadButton();
            this.btnValider = new Telerik.WinControls.UI.RadButton();
            this.btnRejeter = new Telerik.WinControls.UI.RadButton();
            this.btnAlivrer = new Telerik.WinControls.UI.RadButton();
            this.btnAnnuler = new Telerik.WinControls.UI.RadButton();
            this.btnErreurScan = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnVerifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnValider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRejeter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlivrer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAnnuler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnErreurScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVerifier
            // 
            this.btnVerifier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerifier.EnableCodedUITests = true;
            this.btnVerifier.Location = new System.Drawing.Point(12, 3);
            this.btnVerifier.Name = "btnVerifier";
            this.btnVerifier.Size = new System.Drawing.Size(195, 29);
            this.btnVerifier.TabIndex = 51;
            this.btnVerifier.Text = "Verifier";
            this.btnVerifier.Click += new System.EventHandler(this.btnVerifier_Click);
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.EnableCodedUITests = true;
            this.btnValider.Location = new System.Drawing.Point(12, 38);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(195, 29);
            this.btnValider.TabIndex = 52;
            this.btnValider.Text = "Valider";
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnRejeter
            // 
            this.btnRejeter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRejeter.EnableCodedUITests = true;
            this.btnRejeter.Location = new System.Drawing.Point(12, 73);
            this.btnRejeter.Name = "btnRejeter";
            this.btnRejeter.Size = new System.Drawing.Size(195, 29);
            this.btnRejeter.TabIndex = 53;
            this.btnRejeter.Text = "Rejeter";
            this.btnRejeter.Click += new System.EventHandler(this.btnRejeter_Click);
            // 
            // btnAlivrer
            // 
            this.btnAlivrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlivrer.EnableCodedUITests = true;
            this.btnAlivrer.Location = new System.Drawing.Point(12, 148);
            this.btnAlivrer.Name = "btnAlivrer";
            this.btnAlivrer.Size = new System.Drawing.Size(195, 29);
            this.btnAlivrer.TabIndex = 54;
            this.btnAlivrer.Text = "A Livrer";
            this.btnAlivrer.Click += new System.EventHandler(this.btnAlivrer_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.EnableCodedUITests = true;
            this.btnAnnuler.Location = new System.Drawing.Point(0, 183);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(219, 28);
            this.btnAnnuler.TabIndex = 55;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnErreurScan
            // 
            this.btnErreurScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnErreurScan.EnableCodedUITests = true;
            this.btnErreurScan.Location = new System.Drawing.Point(12, 108);
            this.btnErreurScan.Name = "btnErreurScan";
            this.btnErreurScan.Size = new System.Drawing.Size(195, 29);
            this.btnErreurScan.TabIndex = 56;
            this.btnErreurScan.Text = "Erreur Scan";
            this.btnErreurScan.Click += new System.EventHandler(this.btnErreurScan_Click);
            // 
            // choixStatue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 223);
            this.Controls.Add(this.btnErreurScan);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnAlivrer);
            this.Controls.Add(this.btnRejeter);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.btnVerifier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "choixStatue";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changer Statut";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.choixStatue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnVerifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnValider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRejeter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlivrer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAnnuler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnErreurScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnVerifier;
        private Telerik.WinControls.UI.RadButton btnValider;
        private Telerik.WinControls.UI.RadButton btnRejeter;
        private Telerik.WinControls.UI.RadButton btnAlivrer;
        private Telerik.WinControls.UI.RadButton btnAnnuler;
        private Telerik.WinControls.UI.RadButton btnErreurScan;
    }
}
