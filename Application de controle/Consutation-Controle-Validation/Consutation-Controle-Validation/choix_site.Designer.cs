namespace Consutation_Controle_Validation
{
    partial class choix_site
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
            this.showlivrable = new Telerik.WinControls.UI.RadButton();
            this.listeBases = new Telerik.WinControls.UI.RadDropDownList();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.showlivrable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeBases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // showlivrable
            // 
            this.showlivrable.Location = new System.Drawing.Point(130, 84);
            this.showlivrable.Name = "showlivrable";
            this.showlivrable.Size = new System.Drawing.Size(205, 24);
            this.showlivrable.TabIndex = 47;
            this.showlivrable.Text = "Afficher les livrables";
            this.showlivrable.Click += new System.EventHandler(this.showlivrable_Click);
            // 
            // listeBases
            // 
            this.listeBases.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listeBases.Location = new System.Drawing.Point(166, 40);
            this.listeBases.Name = "listeBases";
            this.listeBases.Size = new System.Drawing.Size(244, 20);
            this.listeBases.TabIndex = 46;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "Merci de choisir le site";
            // 
            // choix_site
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 149);
            this.Controls.Add(this.showlivrable);
            this.Controls.Add(this.listeBases);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "choix_site";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix de site";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.choix_site_Load);
            ((System.ComponentModel.ISupportInitialize)(this.showlivrable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeBases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton showlivrable;
        private Telerik.WinControls.UI.RadDropDownList listeBases;
        private System.Windows.Forms.Label label13;
    }
}
