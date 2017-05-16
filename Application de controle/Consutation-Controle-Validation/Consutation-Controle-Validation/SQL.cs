using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace Consutation_Controle_Validation
{
    public partial class SQL : Telerik.WinControls.UI.RadForm
    {
        public string requetteInitial;
        public string requetteGenerer;

        public SQL(string requetteInitial)
        {
            this.requetteInitial = requetteInitial;
            InitializeComponent();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            requetteGenerer = requetteInitial + " AND " + txtRequette.Text.ToString().Trim();
            Home pagePrincipal = new Home();
            pagePrincipal.reqResult = requetteGenerer;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void annulerBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
