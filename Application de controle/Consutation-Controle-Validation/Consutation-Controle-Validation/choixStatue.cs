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
    public partial class choixStatue : Telerik.WinControls.UI.RadForm
    {
        string idDossier;
        string statueDossier;
        int idUtilisateur;
        public int idlivrable;
        Service service = new Service();

        public choixStatue(string idDossier, string statueDossier, int idUtilisateur)
        {
            this.idDossier = idDossier;
            this.statueDossier = statueDossier;
            this.idUtilisateur = idUtilisateur;
            InitializeComponent();
        }

        //button annuler
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //chargement de la page
        private void choixStatue_Load(object sender, EventArgs e)
        {
            //button à livrer
            Boolean droitAlivrer = service.verifier_le_droit(11, idUtilisateur);
            if (droitAlivrer && (statueDossier == "6" || statueDossier == "10"))
            {
                btnAlivrer.Enabled = true;
            }
            else
            {
                btnAlivrer.Enabled = false;
            }

            //button valider et rejeter
            Boolean droitController = service.verifier_le_droit(2, idUtilisateur);
            if (droitController && statueDossier == "6")
            {
                btnValider.Enabled = false;
                btnRejeter.Enabled = true;
            }
            else if (droitController && statueDossier == "7")
            {
                btnValider.Enabled = true;
                btnRejeter.Enabled = false;
            }
            else if (droitController && (statueDossier == "3" || statueDossier == "8" || statueDossier == "10"))
            {
                btnValider.Enabled = true;
                btnRejeter.Enabled = true;
            }
            else
            {
                btnValider.Enabled = false;
                btnRejeter.Enabled = false;
            }

            //button verifier
            Boolean droitcorrection = service.verifier_le_droit(1, idUtilisateur);
            if (droitcorrection && (statueDossier == "3" || statueDossier == "7" || statueDossier == "8" || statueDossier == "10"))
            {
                btnVerifier.Enabled = true;
            }
            else
            {
                btnVerifier.Enabled = false;
            }

            //button erreur scan
            if (droitcorrection)
            {
                btnErreurScan.Enabled = true;
            }
            else
            {
                btnErreurScan.Enabled = false;
            }
        }

        //button verification
        private void btnVerifier_Click(object sender, EventArgs e)
        {

            if (service.changerStatueDossier(idUtilisateur, idDossier, 8))
            {
                MessageBox.Show("Operation reussite !! ");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur !! ");
            }
        }

        //button valider
        private void btnValider_Click(object sender, EventArgs e)
        {
            if (service.changerStatueDossier(idUtilisateur, idDossier, 6))
            {
                //insertion etat dossier valider
                //prod en nbr dossier valides
                service.update_etats(idlivrable, idUtilisateur, service.getdate(), "col3", "1");
                //prod en br des dossiers controller
                service.update_etats(idlivrable, idUtilisateur, service.getdate(), "col5", "1");
                MessageBox.Show("Operation reussite !! ");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur !! ");
            }
        }

        //button rejeter
        private void btnRejeter_Click(object sender, EventArgs e)
        {
            if (service.changerStatueDossier(idUtilisateur, idDossier, 7))
            {
                //insertion etat dossier rejet
                //prod en nbr dossier valides
                service.update_etats(idlivrable, idUtilisateur, service.getdate(), "col4", "1");
                //prod en br des dossiers controller
                service.update_etats(idlivrable, idUtilisateur, service.getdate(), "col5", "1");
                MessageBox.Show("Operation reussite !! ");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur !! ");
            }
        }

        //button à livrer
        private void btnAlivrer_Click(object sender, EventArgs e)
        {
            if (service.changerStatueDossier(idUtilisateur, idDossier, 9))
            {
                MessageBox.Show("Operation reussite !! ");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur !! ");
            }
        }

        //erreur scan
        private void btnErreurScan_Click(object sender, EventArgs e)
        {
            if (service.changerStatueDossier(idUtilisateur, idDossier, 12))
            {
                MessageBox.Show("Operation reussite !! ");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erreur !! ");
            }
        }
    }
}
