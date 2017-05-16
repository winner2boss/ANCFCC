namespace ANCFCC_NV
{
    partial class setNomPiece
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
            this.annuler_chng = new Telerik.WinControls.UI.RadButton();
            this.ok_btn = new Telerik.WinControls.UI.RadButton();
            this.listeNomPiece = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.annuler_chng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ok_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeNomPiece)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // annuler_chng
            // 
            this.annuler_chng.Location = new System.Drawing.Point(540, 51);
            this.annuler_chng.Name = "annuler_chng";
            this.annuler_chng.Size = new System.Drawing.Size(110, 24);
            this.annuler_chng.TabIndex = 52;
            this.annuler_chng.Text = "Annuler";
            this.annuler_chng.Click += new System.EventHandler(this.annuler_chng_Click);
            // 
            // ok_btn
            // 
            this.ok_btn.Enabled = false;
            this.ok_btn.Location = new System.Drawing.Point(540, 12);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(110, 24);
            this.ok_btn.TabIndex = 51;
            this.ok_btn.Text = "OK";
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // listeNomPiece
            // 
            this.listeNomPiece.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.listeNomPiece.Location = new System.Drawing.Point(76, 26);
            this.listeNomPiece.Name = "listeNomPiece";
            this.listeNomPiece.Size = new System.Drawing.Size(444, 22);
            this.listeNomPiece.TabIndex = 50;
            this.listeNomPiece.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.listeNomPiece_SelectedIndexChanged);
            this.listeNomPiece.Leave += new System.EventHandler(this.listeNomPiece_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Nom piece :";
            // 
            // setNomPiece
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 90);
            this.Controls.Add(this.annuler_chng);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.listeNomPiece);
            this.Controls.Add(this.label1);
            this.Name = "setNomPiece";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "setNomPiece";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.setNomPiece_Load);
            ((System.ComponentModel.ISupportInitialize)(this.annuler_chng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ok_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeNomPiece)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton annuler_chng;
        private Telerik.WinControls.UI.RadButton ok_btn;
        private Telerik.WinControls.UI.RadDropDownList listeNomPiece;
        private System.Windows.Forms.Label label1;
    }
}
