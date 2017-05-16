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
    public partial class choixGroupeUtilisateurs : Telerik.WinControls.UI.RadForm
    {
        int idutilisateur;
        int idgroupe_utilisateur;
        int idequipe_utilisateur;
        Service service = new Service();

        public choixGroupeUtilisateurs(int idutilisateur)
        {
            this.idutilisateur = idutilisateur;
            idequipe_utilisateur = service.get_id_equipe(this.idutilisateur);
            idgroupe_utilisateur= service.get_id_groupe(this.idutilisateur);
            InitializeComponent();
        }

        private void annulation_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void agents_indexation_Click(object sender, EventArgs e)
        {
            //this.Hide();
            gestion_equipe_indexation gestion_equipe_indexation = new gestion_equipe_indexation(idutilisateur, idgroupe_utilisateur, idequipe_utilisateur);          
            gestion_equipe_indexation.ShowDialog();
        }
    }
}
