namespace ANCFCC_NV
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            this.connect = new Telerik.WinControls.UI.RadButton();
            this.listelivrable = new Telerik.WinControls.UI.RadDropDownList();
            this.label13 = new System.Windows.Forms.Label();
            this.operation = new Telerik.WinControls.UI.RadDropDownList();
            this.op = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.connect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listelivrable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(159, 99);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(244, 24);
            this.connect.TabIndex = 44;
            this.connect.Text = "Se connecter";
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // listelivrable
            // 
            this.listelivrable.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listelivrable.Location = new System.Drawing.Point(159, 23);
            this.listelivrable.Name = "listelivrable";
            this.listelivrable.Size = new System.Drawing.Size(244, 20);
            this.listelivrable.TabIndex = 43;
            this.listelivrable.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.listelivrable_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Merci de choisir le livrable";
            // 
            // operation
            // 
            this.operation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem1.Text = "Indexation";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "Correction";
            radListDataItem2.TextWrap = true;
            this.operation.Items.Add(radListDataItem1);
            this.operation.Items.Add(radListDataItem2);
            this.operation.Location = new System.Drawing.Point(159, 58);
            this.operation.Name = "operation";
            this.operation.Size = new System.Drawing.Size(244, 20);
            this.operation.TabIndex = 45;
            // 
            // op
            // 
            this.op.AutoSize = true;
            this.op.Location = new System.Drawing.Point(5, 60);
            this.op.Name = "op";
            this.op.Size = new System.Drawing.Size(148, 13);
            this.op.TabIndex = 46;
            this.op.Text = "Merci de choisir l\'operation";
            // 
            // Livrable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 135);
            this.Controls.Add(this.op);
            this.Controls.Add(this.operation);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.listelivrable);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
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
            ((System.ComponentModel.ISupportInitialize)(this.operation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton connect;
        private Telerik.WinControls.UI.RadDropDownList listelivrable;
        private System.Windows.Forms.Label label13;
        private Telerik.WinControls.UI.RadDropDownList operation;
        private System.Windows.Forms.Label op;
    }
}
