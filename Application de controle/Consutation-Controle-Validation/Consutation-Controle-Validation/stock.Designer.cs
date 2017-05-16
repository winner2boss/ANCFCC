namespace Consutation_Controle_Validation
{
    partial class stock
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem9 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem10 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem11 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem12 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem13 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem14 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem15 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem16 = new Telerik.WinControls.UI.RadListDataItem();
            this.listeSites = new Telerik.WinControls.UI.RadDropDownList();
            this.listeLabel = new System.Windows.Forms.Label();
            this.inclurelivrables = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.listeSites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inclurelivrables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // listeSites
            // 
            this.listeSites.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem9.Text = "Tous les sites";
            radListDataItem9.TextWrap = true;
            radListDataItem10.Text = "Maarif";
            radListDataItem10.TextWrap = true;
            radListDataItem11.Text = "El Fida";
            radListDataItem11.TextWrap = true;
            radListDataItem12.Text = "Marrakech";
            radListDataItem12.TextWrap = true;
            radListDataItem13.Text = "EL jadida";
            radListDataItem13.TextWrap = true;
            radListDataItem14.Text = "Fes";
            radListDataItem14.TextWrap = true;
            radListDataItem15.Text = "Agadir";
            radListDataItem15.TextWrap = true;
            radListDataItem16.Text = "Tanger";
            radListDataItem16.TextWrap = true;
            this.listeSites.Items.Add(radListDataItem9);
            this.listeSites.Items.Add(radListDataItem10);
            this.listeSites.Items.Add(radListDataItem11);
            this.listeSites.Items.Add(radListDataItem12);
            this.listeSites.Items.Add(radListDataItem13);
            this.listeSites.Items.Add(radListDataItem14);
            this.listeSites.Items.Add(radListDataItem15);
            this.listeSites.Items.Add(radListDataItem16);
            this.listeSites.Location = new System.Drawing.Point(53, 23);
            this.listeSites.Name = "listeSites";
            this.listeSites.Size = new System.Drawing.Size(158, 20);
            this.listeSites.TabIndex = 48;
            // 
            // listeLabel
            // 
            this.listeLabel.AutoSize = true;
            this.listeLabel.Location = new System.Drawing.Point(12, 26);
            this.listeLabel.Name = "listeLabel";
            this.listeLabel.Size = new System.Drawing.Size(35, 13);
            this.listeLabel.TabIndex = 47;
            this.listeLabel.Text = "Site : ";
            // 
            // inclurelivrables
            // 
            this.inclurelivrables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inclurelivrables.Enabled = false;
            this.inclurelivrables.Location = new System.Drawing.Point(53, 62);
            this.inclurelivrables.Name = "inclurelivrables";
            // 
            // 
            // 
            this.inclurelivrables.RootElement.StretchHorizontally = true;
            this.inclurelivrables.RootElement.StretchVertically = true;
            this.inclurelivrables.Size = new System.Drawing.Size(158, 18);
            this.inclurelivrables.TabIndex = 66;
            this.inclurelivrables.Text = "Inclure Les dossiers affectés";
            // 
            // stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 224);
            this.Controls.Add(this.inclurelivrables);
            this.Controls.Add(this.listeSites);
            this.Controls.Add(this.listeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "stock";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verification du stock";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.listeSites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inclurelivrables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList listeSites;
        private System.Windows.Forms.Label listeLabel;
        private Telerik.WinControls.UI.RadCheckBox inclurelivrables;
    }
}
