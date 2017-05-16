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
    public partial class formalite : Telerik.WinControls.UI.RadForm
    {
        Service service = new Service();
        DataTable dtformalites = new DataTable();
        public int idutilisateur=0;

        public formalite()
        {
            InitializeComponent();
        }

        private void formalite_Load(object sender, EventArgs e)
        {
            dtformalites = service.chargerlisteFormalitesTable();
            this.listeFormaliteGride.DataSource = dtformalites;
        }

        private void renitialiser_Click(object sender, EventArgs e)
        {
            arfomalite.Text = "";
            frformalite.Text = "";
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (arfomalite.Text != "" && frformalite.Text != "")
            {
                if (service.insertFormalite(frformalite.Text.ToString(), arfomalite.Text.ToString(), "Nouveau", idutilisateur))
                {
                    MessageBox.Show("Operation Reussie");
                    dtformalites = service.chargerlisteFormalitesTable();
                    this.listeFormaliteGride.DataSource = dtformalites;
                    arfomalite.Text = "";
                    frformalite.Text = "";
                }
                else
                {
                    MessageBox.Show("Operation Echoué");
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les deux champs");
            }
        }
    }
}
