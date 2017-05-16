namespace Consutation_Controle_Validation
{
    partial class Historique_Operation_Dossier
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
            this.gridHistoDossierOperation = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistoDossierOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistoDossierOperation.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridHistoDossierOperation
            // 
            this.gridHistoDossierOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridHistoDossierOperation.Location = new System.Drawing.Point(12, 12);
            // 
            // gridHistoDossierOperation
            // 
            this.gridHistoDossierOperation.MasterTemplate.AllowAddNewRow = false;
            this.gridHistoDossierOperation.MasterTemplate.AllowDeleteRow = false;
            this.gridHistoDossierOperation.MasterTemplate.AllowEditRow = false;
            this.gridHistoDossierOperation.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridHistoDossierOperation.Name = "gridHistoDossierOperation";
            this.gridHistoDossierOperation.ReadOnly = true;
            this.gridHistoDossierOperation.Size = new System.Drawing.Size(537, 337);
            this.gridHistoDossierOperation.TabIndex = 1;
            this.gridHistoDossierOperation.Text = "myGridView";
            // 
            // Historique_Operation_Dossier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 361);
            this.Controls.Add(this.gridHistoDossierOperation);
            this.Name = "Historique_Operation_Dossier";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historique des operations dossier";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.Historique_Operation_Dossier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridHistoDossierOperation.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistoDossierOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridHistoDossierOperation;
    }
}
