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
    public partial class Livrable : Telerik.WinControls.UI.RadForm
    {
        public int idUtilisateur;
        Service service = new Service();
        Dictionary<string, string> livrableParIdetname = new Dictionary<string, string>();
        public int idbase;

        public Livrable()
        {
            InitializeComponent();
        }

        private void listelivrable_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (listelivrable.Text != "")
            {
                connect.Enabled = true;
            }
        }

        private void Livrable_Load(object sender, EventArgs e)
        {
            if (service.chargerLabase(idbase))
            {
                livrableParIdetname = service.getListeLivrable();

                Dictionary<string, string>.KeyCollection keys = livrableParIdetname.Keys;

                foreach (string key in keys)
                {
                    listelivrable.Items.Add(key);
                }
                connect.Enabled = false;
            }
            else
            {
                MessageBox.Show("Erreur récuperation de base");
                Application.Exit();
            }
        }

        private void Livrable_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.idUtilisateur = idUtilisateur;
            home.idLivrable = Int32.Parse(livrableParIdetname[listelivrable.Text]);
            home.nomLivrable = listelivrable.Text;
            home.idbase = idbase;

            home.Show();

            //this.Hide();
            //ANCFCC home = new ANCFCC();
            //home.idUtilisateur = idUtilisateur;
            //home.idLivrable = Int32.Parse(livrableParIdetname[listelivrable.Text]);
            //home.nomLivrable = listelivrable.Text;
            //home.Show();
        }

        private void changesite_Click(object sender, EventArgs e)
        {
            this.Hide();            
            choix_site site = new choix_site();
            site.Show(); 
        }
    }
}
