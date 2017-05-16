namespace Consutation_Controle_Validation
{
    partial class gestion_names_pieces
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
            this.listePieceGride = new Telerik.WinControls.UI.RadGridView();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.renitialiser = new Telerik.WinControls.UI.RadButton();
            this.namePiece = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.listePieceGride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listePieceGride.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.renitialiser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // listePieceGride
            // 
            this.listePieceGride.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listePieceGride.Location = new System.Drawing.Point(22, 12);
            // 
            // listePieceGride
            // 
            this.listePieceGride.MasterTemplate.AllowEditRow = false;
            this.listePieceGride.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.listePieceGride.Name = "listePieceGride";
            this.listePieceGride.Size = new System.Drawing.Size(535, 222);
            this.listePieceGride.TabIndex = 2;
            this.listePieceGride.Text = "ddd";
            // 
            // radButton2
            // 
            this.radButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radButton2.EnableCodedUITests = true;
            this.radButton2.Location = new System.Drawing.Point(437, 299);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(120, 28);
            this.radButton2.TabIndex = 73;
            this.radButton2.Text = "Quitter";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radButton1
            // 
            this.radButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.radButton1.EnableCodedUITests = true;
            this.radButton1.Location = new System.Drawing.Point(164, 299);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(120, 28);
            this.radButton1.TabIndex = 72;
            this.radButton1.Text = "Enregistrer";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // renitialiser
            // 
            this.renitialiser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.renitialiser.EnableCodedUITests = true;
            this.renitialiser.Location = new System.Drawing.Point(25, 298);
            this.renitialiser.Name = "renitialiser";
            this.renitialiser.Size = new System.Drawing.Size(120, 29);
            this.renitialiser.TabIndex = 71;
            this.renitialiser.Text = "Rénitialiser";
            this.renitialiser.Click += new System.EventHandler(this.renitialiser_Click);
            // 
            // namePiece
            // 
            this.namePiece.Location = new System.Drawing.Point(103, 261);
            this.namePiece.Name = "namePiece";
            this.namePiece.Size = new System.Drawing.Size(454, 20);
            this.namePiece.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Nom Piece :";
            // 
            // gestion_names_pieces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 343);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.renitialiser);
            this.Controls.Add(this.namePiece);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listePieceGride);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "gestion_names_pieces";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gestion_names_pieces";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.gestion_names_pieces_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listePieceGride.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listePieceGride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.renitialiser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView listePieceGride;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton renitialiser;
        private System.Windows.Forms.TextBox namePiece;
        private System.Windows.Forms.Label label1;
    }
}
