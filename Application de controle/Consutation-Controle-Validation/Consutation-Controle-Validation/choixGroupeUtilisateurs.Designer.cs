namespace Consutation_Controle_Validation
{
    partial class choixGroupeUtilisateurs
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
            this.agents_indexation = new Telerik.WinControls.UI.RadButton();
            this.agents_controle = new Telerik.WinControls.UI.RadButton();
            this.annulation = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.agents_indexation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.agents_controle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.annulation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // agents_indexation
            // 
            this.agents_indexation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.agents_indexation.EnableCodedUITests = true;
            this.agents_indexation.Location = new System.Drawing.Point(12, 12);
            this.agents_indexation.Name = "agents_indexation";
            this.agents_indexation.Size = new System.Drawing.Size(222, 39);
            this.agents_indexation.TabIndex = 52;
            this.agents_indexation.Text = "Les agents d\'indexation";
            this.agents_indexation.Click += new System.EventHandler(this.agents_indexation_Click);
            // 
            // agents_controle
            // 
            this.agents_controle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.agents_controle.EnableCodedUITests = true;
            this.agents_controle.Location = new System.Drawing.Point(12, 57);
            this.agents_controle.Name = "agents_controle";
            this.agents_controle.Size = new System.Drawing.Size(222, 39);
            this.agents_controle.TabIndex = 53;
            this.agents_controle.Text = "Les agents de controle";
            // 
            // annulation
            // 
            this.annulation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.annulation.EnableCodedUITests = true;
            this.annulation.Location = new System.Drawing.Point(2, 220);
            this.annulation.Name = "annulation";
            this.annulation.Size = new System.Drawing.Size(243, 30);
            this.annulation.TabIndex = 57;
            this.annulation.Text = "Annuler";
            this.annulation.Click += new System.EventHandler(this.annulation_Click);
            // 
            // choixGroupeUtilisateurs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 262);
            this.Controls.Add(this.annulation);
            this.Controls.Add(this.agents_controle);
            this.Controls.Add(this.agents_indexation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "choixGroupeUtilisateurs";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des Utilisateurs";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.agents_indexation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.agents_controle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.annulation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton agents_indexation;
        private Telerik.WinControls.UI.RadButton agents_controle;
        private Telerik.WinControls.UI.RadButton annulation;
    }
}
