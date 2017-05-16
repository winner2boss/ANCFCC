namespace Consutation_Controle_Validation
{
    partial class gestion_equipe_indexation
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
            this.equipeIndexation = new Telerik.WinControls.UI.RadGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.quitter = new Telerik.WinControls.UI.RadButton();
            this.listeGroupes = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.renitialiser = new Telerik.WinControls.UI.RadButton();
            this.save = new Telerik.WinControls.UI.RadButton();
            this.nouveau = new Telerik.WinControls.UI.RadButton();
            this.inactif = new Telerik.WinControls.UI.RadRadioButton();
            this.actif = new Telerik.WinControls.UI.RadRadioButton();
            this.Etat = new System.Windows.Forms.Label();
            this.listeequipe = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.Label();
            this.txtlogin = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.Label();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.equipeIndexation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipeIndexation.MasterTemplate)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeGroupes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.renitialiser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nouveau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeequipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // equipeIndexation
            // 
            this.equipeIndexation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.equipeIndexation.Location = new System.Drawing.Point(12, 12);
            // 
            // equipeIndexation
            // 
            this.equipeIndexation.MasterTemplate.AllowAddNewRow = false;
            this.equipeIndexation.MasterTemplate.AllowDeleteRow = false;
            this.equipeIndexation.MasterTemplate.AllowEditRow = false;
            this.equipeIndexation.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.equipeIndexation.Name = "equipeIndexation";
            this.equipeIndexation.ReadOnly = true;
            this.equipeIndexation.Size = new System.Drawing.Size(733, 168);
            this.equipeIndexation.TabIndex = 1;
            this.equipeIndexation.Text = "myGridView";
            this.equipeIndexation.SelectionChanged += new System.EventHandler(this.equipeIndexation_SelectionChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radGroupBox1);
            this.groupBox3.Controls.Add(this.quitter);
            this.groupBox3.Controls.Add(this.listeGroupes);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.renitialiser);
            this.groupBox3.Controls.Add(this.save);
            this.groupBox3.Controls.Add(this.nouveau);
            this.groupBox3.Controls.Add(this.inactif);
            this.groupBox3.Controls.Add(this.actif);
            this.groupBox3.Controls.Add(this.Etat);
            this.groupBox3.Controls.Add(this.listeequipe);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.Password);
            this.groupBox3.Controls.Add(this.txtlogin);
            this.groupBox3.Controls.Add(this.login);
            this.groupBox3.Location = new System.Drawing.Point(12, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(733, 216);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details :";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGroupBox1.Controls.Add(this.radGridView1);
            this.radGroupBox1.HeaderText = "Attribution des droits";
            this.radGroupBox1.Location = new System.Drawing.Point(238, 11);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(362, 195);
            this.radGroupBox1.TabIndex = 72;
            this.radGroupBox1.Text = "Attribution des droits";
            // 
            // quitter
            // 
            this.quitter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.quitter.EnableCodedUITests = true;
            this.quitter.Location = new System.Drawing.Point(607, 123);
            this.quitter.Name = "quitter";
            this.quitter.Size = new System.Drawing.Size(120, 28);
            this.quitter.TabIndex = 71;
            this.quitter.Text = "Quitter";
            this.quitter.Click += new System.EventHandler(this.quitter_Click);
            // 
            // listeGroupes
            // 
            this.listeGroupes.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listeGroupes.Location = new System.Drawing.Point(71, 139);
            this.listeGroupes.Name = "listeGroupes";
            this.listeGroupes.Size = new System.Drawing.Size(161, 20);
            this.listeGroupes.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Groupe :";
            // 
            // renitialiser
            // 
            this.renitialiser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.renitialiser.EnableCodedUITests = true;
            this.renitialiser.Location = new System.Drawing.Point(607, 53);
            this.renitialiser.Name = "renitialiser";
            this.renitialiser.Size = new System.Drawing.Size(120, 28);
            this.renitialiser.TabIndex = 63;
            this.renitialiser.Text = "Renitialiser";
            // 
            // save
            // 
            this.save.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.save.EnableCodedUITests = true;
            this.save.Location = new System.Drawing.Point(607, 87);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(120, 28);
            this.save.TabIndex = 64;
            this.save.Text = "Enregistrer";
            // 
            // nouveau
            // 
            this.nouveau.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.nouveau.EnableCodedUITests = true;
            this.nouveau.Location = new System.Drawing.Point(607, 19);
            this.nouveau.Name = "nouveau";
            this.nouveau.Size = new System.Drawing.Size(120, 28);
            this.nouveau.TabIndex = 58;
            this.nouveau.Text = "Nouveau";
            // 
            // inactif
            // 
            this.inactif.Location = new System.Drawing.Point(120, 169);
            this.inactif.Name = "inactif";
            this.inactif.Size = new System.Drawing.Size(51, 18);
            this.inactif.TabIndex = 68;
            this.inactif.TabStop = true;
            this.inactif.Text = "Inactif";
            this.inactif.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // actif
            // 
            this.actif.Location = new System.Drawing.Point(71, 169);
            this.actif.Name = "actif";
            this.actif.Size = new System.Drawing.Size(43, 18);
            this.actif.TabIndex = 67;
            this.actif.TabStop = true;
            this.actif.Text = "Actif";
            this.actif.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // Etat
            // 
            this.Etat.AutoSize = true;
            this.Etat.Location = new System.Drawing.Point(6, 174);
            this.Etat.Name = "Etat";
            this.Etat.Size = new System.Drawing.Size(33, 13);
            this.Etat.TabIndex = 66;
            this.Etat.Text = "Etat :";
            // 
            // listeequipe
            // 
            this.listeequipe.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listeequipe.Location = new System.Drawing.Point(71, 103);
            this.listeequipe.Name = "listeequipe";
            this.listeequipe.Size = new System.Drawing.Size(161, 20);
            this.listeequipe.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Equipe :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(71, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(153, 20);
            this.textBox1.TabIndex = 63;
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(6, 73);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(62, 13);
            this.Password.TabIndex = 62;
            this.Password.Text = "Password :";
            // 
            // txtlogin
            // 
            this.txtlogin.Location = new System.Drawing.Point(71, 32);
            this.txtlogin.Name = "txtlogin";
            this.txtlogin.Size = new System.Drawing.Size(153, 20);
            this.txtlogin.TabIndex = 61;
            // 
            // login
            // 
            this.login.AutoSize = true;
            this.login.Location = new System.Drawing.Point(6, 35);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(42, 13);
            this.login.TabIndex = 60;
            this.login.Text = "Login :";
            // 
            // radGridView1
            // 
            this.radGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radGridView1.Location = new System.Drawing.Point(6, 22);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(351, 159);
            this.radGridView1.TabIndex = 0;
            this.radGridView1.Text = "ddd";
            // 
            // gestion_equipe_indexation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 404);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.equipeIndexation);
            this.Name = "gestion_equipe_indexation";
            this.Text = "gestion_equipe_indexation";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.gestion_equipe_indexation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.equipeIndexation.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipeIndexation)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.quitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeGroupes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.renitialiser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nouveau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeequipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView equipeIndexation;
        private System.Windows.Forms.GroupBox groupBox3;
        private Telerik.WinControls.UI.RadButton nouveau;
        private System.Windows.Forms.TextBox txtlogin;
        private System.Windows.Forms.Label login;
        private Telerik.WinControls.UI.RadButton renitialiser;
        private Telerik.WinControls.UI.RadButton save;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDropDownList listeequipe;
        private System.Windows.Forms.Label Etat;
        private Telerik.WinControls.UI.RadRadioButton actif;
        private Telerik.WinControls.UI.RadRadioButton inactif;
        private Telerik.WinControls.UI.RadDropDownList listeGroupes;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadButton quitter;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
    }
}
