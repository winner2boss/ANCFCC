namespace Consutation_Controle_Validation
{
    partial class Livrable
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
            this.connect = new Telerik.WinControls.UI.RadButton();
            this.listelivrable = new Telerik.WinControls.UI.RadDropDownList();
            this.label13 = new System.Windows.Forms.Label();
            this.changesite = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.connect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listelivrable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.changesite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(130, 66);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(205, 24);
            this.connect.TabIndex = 44;
            this.connect.Text = "Se connecter";
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // listelivrable
            // 
            this.listelivrable.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listelivrable.Location = new System.Drawing.Point(166, 22);
            this.listelivrable.Name = "listelivrable";
            this.listelivrable.Size = new System.Drawing.Size(244, 20);
            this.listelivrable.TabIndex = 43;
            this.listelivrable.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.listelivrable_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Merci de choisir le livrable";
            // 
            // changesite
            // 
            this.changesite.Location = new System.Drawing.Point(130, 96);
            this.changesite.Name = "changesite";
            this.changesite.Size = new System.Drawing.Size(205, 24);
            this.changesite.TabIndex = 45;
            this.changesite.Text = "Changer le site";
            this.changesite.Click += new System.EventHandler(this.changesite_Click);
            // 
            // Livrable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 149);
            this.Controls.Add(this.changesite);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.listelivrable);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Livrable";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Livrable";
            this.ThemeName = "ControlDefault";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Livrable_FormClosing);
            this.Load += new System.EventHandler(this.Livrable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.connect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listelivrable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.changesite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton connect;
        private Telerik.WinControls.UI.RadDropDownList listelivrable;
        private System.Windows.Forms.Label label13;
        private Telerik.WinControls.UI.RadButton changesite;
    }
}
