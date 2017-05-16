namespace Consutation_Controle_Validation
{
    partial class SelectionnerAgent
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
            this.agentIndexation = new System.Windows.Forms.Label();
            this.loginAgentIndexation = new System.Windows.Forms.Label();
            this.listeLabel = new System.Windows.Forms.Label();
            this.listeAgents = new Telerik.WinControls.UI.RadDropDownList();
            this.btnSelectionner = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.listeAgents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectionner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // agentIndexation
            // 
            this.agentIndexation.AutoSize = true;
            this.agentIndexation.Location = new System.Drawing.Point(12, 23);
            this.agentIndexation.Name = "agentIndexation";
            this.agentIndexation.Size = new System.Drawing.Size(105, 13);
            this.agentIndexation.TabIndex = 43;
            this.agentIndexation.Text = "Agent Indexation : ";
            // 
            // loginAgentIndexation
            // 
            this.loginAgentIndexation.AutoSize = true;
            this.loginAgentIndexation.Location = new System.Drawing.Point(162, 23);
            this.loginAgentIndexation.Name = "loginAgentIndexation";
            this.loginAgentIndexation.Size = new System.Drawing.Size(45, 13);
            this.loginAgentIndexation.TabIndex = 44;
            this.loginAgentIndexation.Text = "Show : ";
            // 
            // listeLabel
            // 
            this.listeLabel.AutoSize = true;
            this.listeLabel.Location = new System.Drawing.Point(12, 56);
            this.listeLabel.Name = "listeLabel";
            this.listeLabel.Size = new System.Drawing.Size(98, 13);
            this.listeLabel.TabIndex = 45;
            this.listeLabel.Text = "Liste des agents : ";
            // 
            // listeAgents
            // 
            this.listeAgents.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.listeAgents.Location = new System.Drawing.Point(127, 54);
            this.listeAgents.Name = "listeAgents";
            this.listeAgents.Size = new System.Drawing.Size(150, 20);
            this.listeAgents.TabIndex = 46;
            // 
            // btnSelectionner
            // 
            this.btnSelectionner.Location = new System.Drawing.Point(3, 90);
            this.btnSelectionner.Name = "btnSelectionner";
            this.btnSelectionner.Size = new System.Drawing.Size(153, 24);
            this.btnSelectionner.TabIndex = 47;
            this.btnSelectionner.Text = "Envoyer pour réindexation";
            this.btnSelectionner.Click += new System.EventHandler(this.btnSelectionner_Click);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(165, 90);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(115, 24);
            this.radButton1.TabIndex = 48;
            this.radButton1.Text = "Annuler";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // SelectionnerAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 135);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.btnSelectionner);
            this.Controls.Add(this.listeAgents);
            this.Controls.Add(this.listeLabel);
            this.Controls.Add(this.loginAgentIndexation);
            this.Controls.Add(this.agentIndexation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectionnerAgent";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selectionner Agent";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.Reindexation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listeAgents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectionner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label agentIndexation;
        private System.Windows.Forms.Label loginAgentIndexation;
        private System.Windows.Forms.Label listeLabel;
        private Telerik.WinControls.UI.RadDropDownList listeAgents;
        private Telerik.WinControls.UI.RadButton btnSelectionner;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}
