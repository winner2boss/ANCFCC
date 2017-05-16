namespace ANCFCC_NV
{
    partial class setIdPiece
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
            this.listeIDChoix = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.annuler_chng = new Telerik.WinControls.UI.RadButton();
            this.chng_ordre = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.listeIDChoix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.annuler_chng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chng_ordre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // listeIDChoix
            // 
            this.listeIDChoix.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listeIDChoix.Location = new System.Drawing.Point(205, 12);
            this.listeIDChoix.Name = "listeIDChoix";
            this.listeIDChoix.Size = new System.Drawing.Size(421, 20);
            this.listeIDChoix.TabIndex = 49;
            this.listeIDChoix.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.listeIDChoix_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Merci de choisir ID de la piece :";
            // 
            // annuler_chng
            // 
            this.annuler_chng.Location = new System.Drawing.Point(514, 62);
            this.annuler_chng.Name = "annuler_chng";
            this.annuler_chng.Size = new System.Drawing.Size(110, 24);
            this.annuler_chng.TabIndex = 47;
            this.annuler_chng.Text = "Annuler";
            this.annuler_chng.Click += new System.EventHandler(this.annuler_chng_Click);
            // 
            // chng_ordre
            // 
            this.chng_ordre.Enabled = false;
            this.chng_ordre.Location = new System.Drawing.Point(357, 62);
            this.chng_ordre.Name = "chng_ordre";
            this.chng_ordre.Size = new System.Drawing.Size(110, 24);
            this.chng_ordre.TabIndex = 46;
            this.chng_ordre.Text = "Changer Ordre";
            this.chng_ordre.Click += new System.EventHandler(this.chng_ordre_Click);
            // 
            // setIdPiece
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 98);
            this.Controls.Add(this.listeIDChoix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.annuler_chng);
            this.Controls.Add(this.chng_ordre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "setIdPiece";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changement d\'ordre";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.setIdPiece_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listeIDChoix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.annuler_chng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chng_ordre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList listeIDChoix;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton annuler_chng;
        private Telerik.WinControls.UI.RadButton chng_ordre;
    }
}
