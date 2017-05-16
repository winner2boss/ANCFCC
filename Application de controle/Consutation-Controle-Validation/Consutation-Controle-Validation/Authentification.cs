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
    public partial class Authentification : Telerik.WinControls.UI.RadForm
    {
        Service service = new Service();
        String username;
        String password;

        public Authentification()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Text;

            if (service.authentification(username, password) == 0)
            {
                MessageBox.Show("Username où le password est incorrect");
            }
            else
            {
                this.Hide();
                //Livrable livrable = new Livrable();
                //livrable.idUtilisateur = service.authentification(username, password);
                //livrable.Show();                 
                choix_site site = new choix_site();
                site.idUtilisateur = service.authentification(username, password);
                site.Show();  
            }

        }

        private void Annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
