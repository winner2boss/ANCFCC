using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace ANCFCC_NV
{
    public partial class Livrable : Telerik.WinControls.UI.RadForm
    {
        public int idUtilisateur;
        Service service = new Service();

        public Livrable()
        {
            InitializeComponent();
        }

        private void Livrable_Load(object sender, EventArgs e)
        {
            //chargement des livrables
            DataTable dtLivrables = service.getListeLivrable();
            
            listelivrable.DataSource = dtLivrables;
            listelivrable.DisplayMember = dtLivrables.Columns[1].ColumnName;
            listelivrable.ValueMember = dtLivrables.Columns[0].ColumnName;
            listelivrable.Items.Add("Tous les livrables");
            if (listelivrable.SelectedItem !=null && listelivrable.SelectedItem.Text != "")
            {
                connect.Enabled = true;
            }
            else
            {
                connect.Enabled = false;
            }
            
            
        }

        private void Livrable_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void listelivrable_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (listelivrable.Text != "")
            {
                connect.Enabled = true;
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            if (operation.SelectedItem != null && operation.SelectedItem.Text != "")
            {
                this.Hide();
                IndexationNV indexation = new IndexationNV();
                indexation.idUtilisateur = idUtilisateur;
                if (listelivrable.SelectedItem.Text == "Tous les livrables")
                {
                    indexation.idLivrable = 0;
                }
                else
                {
                    indexation.idLivrable = Int32.Parse(listelivrable.SelectedItem.Value.ToString());
                }
                indexation.nomLivrable = listelivrable.Text;
                if (operation.SelectedItem.Text=="Correction")
                {
                    indexation.correction = true;
                }
                indexation.Show();
            }
            else
            {
                MessageBox.Show("Merci de choisir l'operation en premier");
            }
        }
    }
}
