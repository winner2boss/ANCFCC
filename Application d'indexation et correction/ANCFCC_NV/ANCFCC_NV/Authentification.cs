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
    public partial class Authentification_ : Telerik.WinControls.UI.RadForm
    {
        Service service = new Service();
        String username;
        String password;

        public Authentification_()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Text;
            int iduser=service.authentification(username, password);
            if (iduser == 0)
            {
                MessageBox.Show("Username où le password est incorrect");
            }
            else
            {
                this.Hide();
                //chargement de la base sel(on utilisateur
                if (service.chargerLabase(iduser))
                {
                    Livrable livrable = new Livrable();
                    livrable.idUtilisateur = iduser;
                    livrable.Show();
                }
                else
                {
                    MessageBox.Show("Erreur récuperation de base");
                    Application.Exit();
                }             
                

            }
        }

        private void Annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
