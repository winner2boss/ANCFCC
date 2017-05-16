namespace Consutation_Controle_Validation
{
    partial class SQL
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
            this.txtRequette = new System.Windows.Forms.TextBox();
            this.connect = new Telerik.WinControls.UI.RadButton();
            this.annulerBtn = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.connect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.annulerBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRequette
            // 
            this.txtRequette.Location = new System.Drawing.Point(12, 9);
            this.txtRequette.Multiline = true;
            this.txtRequette.Name = "txtRequette";
            this.txtRequette.Size = new System.Drawing.Size(487, 72);
            this.txtRequette.TabIndex = 4;
            // 
            // connect
            // 
            this.connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.connect.EnableCodedUITests = true;
            this.connect.Location = new System.Drawing.Point(505, 9);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(219, 33);
            this.connect.TabIndex = 51;
            this.connect.Text = "Lancer la recherche";
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // annulerBtn
            // 
            this.annulerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.annulerBtn.EnableCodedUITests = true;
            this.annulerBtn.Location = new System.Drawing.Point(505, 48);
            this.annulerBtn.Name = "annulerBtn";
            this.annulerBtn.Size = new System.Drawing.Size(219, 33);
            this.annulerBtn.TabIndex = 52;
            this.annulerBtn.Text = "Annuler";
            this.annulerBtn.Click += new System.EventHandler(this.annulerBtn_Click);
            // 
            // SQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 102);
            this.Controls.Add(this.annulerBtn);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.txtRequette);
            this.Name = "SQL";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SQL";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.connect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.annulerBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRequette;
        private Telerik.WinControls.UI.RadButton connect;
        private Telerik.WinControls.UI.RadButton annulerBtn;
    }
}
