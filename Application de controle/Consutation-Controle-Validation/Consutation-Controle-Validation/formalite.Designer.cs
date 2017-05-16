namespace Consutation_Controle_Validation
{
    partial class formalite
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
            this.listeFormaliteGride = new Telerik.WinControls.UI.RadGridView();
            this.arfomalite = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.Label();
            this.frformalite = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.renitialiser = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.listeFormaliteGride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeFormaliteGride.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.renitialiser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // listeFormaliteGride
            // 
            this.listeFormaliteGride.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listeFormaliteGride.Location = new System.Drawing.Point(15, 10);
            // 
            // listeFormaliteGride
            // 
            this.listeFormaliteGride.MasterTemplate.AllowEditRow = false;
            this.listeFormaliteGride.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.listeFormaliteGride.Name = "listeFormaliteGride";
            this.listeFormaliteGride.Size = new System.Drawing.Size(701, 351);
            this.listeFormaliteGride.TabIndex = 1;
            this.listeFormaliteGride.Text = "ddd";
            // 
            // arfomalite
            // 
            this.arfomalite.Location = new System.Drawing.Point(137, 391);
            this.arfomalite.Name = "arfomalite";
            this.arfomalite.Size = new System.Drawing.Size(579, 20);
            this.arfomalite.TabIndex = 63;
            // 
            // login
            // 
            this.login.AutoSize = true;
            this.login.Location = new System.Drawing.Point(12, 394);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(94, 13);
            this.login.TabIndex = 62;
            this.login.Text = "Formalite Arabe :";
            // 
            // frformalite
            // 
            this.frformalite.Location = new System.Drawing.Point(137, 424);
            this.frformalite.Name = "frformalite";
            this.frformalite.Size = new System.Drawing.Size(579, 20);
            this.frformalite.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Formalite Français :";
            // 
            // renitialiser
            // 
            this.renitialiser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.renitialiser.EnableCodedUITests = true;
            this.renitialiser.Location = new System.Drawing.Point(137, 459);
            this.renitialiser.Name = "renitialiser";
            this.renitialiser.Size = new System.Drawing.Size(120, 29);
            this.renitialiser.TabIndex = 66;
            this.renitialiser.Text = "Rénitialiser";
            this.renitialiser.Click += new System.EventHandler(this.renitialiser_Click);
            // 
            // radButton1
            // 
            this.radButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radButton1.EnableCodedUITests = true;
            this.radButton1.Location = new System.Drawing.Point(276, 459);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(120, 28);
            this.radButton1.TabIndex = 67;
            this.radButton1.Text = "Enregistrer";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radButton2.EnableCodedUITests = true;
            this.radButton2.Location = new System.Drawing.Point(596, 459);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(120, 28);
            this.radButton2.TabIndex = 68;
            this.radButton2.Text = "Quitter";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // formalite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 518);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.renitialiser);
            this.Controls.Add(this.frformalite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.arfomalite);
            this.Controls.Add(this.login);
            this.Controls.Add(this.listeFormaliteGride);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "formalite";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formalite";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.formalite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listeFormaliteGride.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeFormaliteGride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.renitialiser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView listeFormaliteGride;
        private System.Windows.Forms.TextBox arfomalite;
        private System.Windows.Forms.Label login;
        private System.Windows.Forms.TextBox frformalite;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton renitialiser;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
    }
}
