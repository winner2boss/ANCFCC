namespace Consutation_Controle_Validation
{
    partial class gestionEquipes
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
            this.grideEquipe = new Telerik.WinControls.UI.RadGridView();
            this.add = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.txtnomequipe = new System.Windows.Forms.TextBox();
            this.button = new Telerik.WinControls.UI.RadButton();
            this.statutPiece = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grideEquipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grideEquipe.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grideEquipe
            // 
            this.grideEquipe.Location = new System.Drawing.Point(12, 12);
            // 
            // grideEquipe
            // 
            this.grideEquipe.MasterTemplate.AllowAddNewRow = false;
            this.grideEquipe.MasterTemplate.AllowDeleteRow = false;
            this.grideEquipe.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grideEquipe.Name = "grideEquipe";
            this.grideEquipe.ReadOnly = true;
            this.grideEquipe.Size = new System.Drawing.Size(242, 241);
            this.grideEquipe.TabIndex = 58;
            this.grideEquipe.Text = "grideEquipe";
            this.grideEquipe.SelectionChanged += new System.EventHandler(this.grideEquipe_SelectionChanged);
            // 
            // add
            // 
            this.add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.add.EnableCodedUITests = true;
            this.add.Location = new System.Drawing.Point(260, 14);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(174, 28);
            this.add.TabIndex = 59;
            this.add.Text = "Ajouter";
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // radButton2
            // 
            this.radButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radButton2.EnableCodedUITests = true;
            this.radButton2.Location = new System.Drawing.Point(260, 48);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(174, 28);
            this.radButton2.TabIndex = 61;
            this.radButton2.Text = "Fermer";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // txtnomequipe
            // 
            this.txtnomequipe.Location = new System.Drawing.Point(270, 118);
            this.txtnomequipe.Name = "txtnomequipe";
            this.txtnomequipe.Size = new System.Drawing.Size(153, 20);
            this.txtnomequipe.TabIndex = 62;
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button.EnableCodedUITests = true;
            this.button.Location = new System.Drawing.Point(288, 146);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(118, 28);
            this.button.TabIndex = 63;
            this.button.Text = "Enregistrer";
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // statutPiece
            // 
            this.statutPiece.AutoSize = true;
            this.statutPiece.Location = new System.Drawing.Point(307, 102);
            this.statutPiece.Name = "statutPiece";
            this.statutPiece.Size = new System.Drawing.Size(79, 13);
            this.statutPiece.TabIndex = 64;
            this.statutPiece.Text = "Nom Equipe : ";
            // 
            // gestionEquipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 265);
            this.Controls.Add(this.statutPiece);
            this.Controls.Add(this.button);
            this.Controls.Add(this.txtnomequipe);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.add);
            this.Controls.Add(this.grideEquipe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "gestionEquipes";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des equipes";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.gestionEquipes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grideEquipe.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grideEquipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grideEquipe;
        private Telerik.WinControls.UI.RadButton add;
        private Telerik.WinControls.UI.RadButton radButton2;
        private System.Windows.Forms.TextBox txtnomequipe;
        private Telerik.WinControls.UI.RadButton button;
        private System.Windows.Forms.Label statutPiece;
    }
}
