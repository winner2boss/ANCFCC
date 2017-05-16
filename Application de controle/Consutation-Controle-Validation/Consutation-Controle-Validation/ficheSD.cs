using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Collections;

namespace Consutation_Controle_Validation
{
    public partial class ficheSD : Telerik.WinControls.UI.RadForm
    {
        ArrayList listeFormalite;
        ArrayList listeJours;
        ArrayList listesMois;
        ArrayList listeAnnes;
        //dictionnaire arabe francais
        Dictionary<string, string> formaliteARFR = new Dictionary<string, string>();
        Service service = new Service();
        //les indexes saisie au niveau de l'interface
        public string numSousDossier,formalite, volumeDepot, numDepot;
        public string dateDepot ;

        public ficheSD()
        {
            InitializeComponent();

            //charger les liste du ficher sous dossier
            //listeFormalite = Service.chargerListeFormalites();
            listeJours = Service.chargerListeDesjours();
            listesMois = Service.chargerListeDesmois();
            listeAnnes = Service.chargerListeDesAnnes();

            //liste formalites
            DataTable dtlisteFormalites = service.chargerlisteFormalites();
            Formalite.DataSource = dtlisteFormalites;
            Formalite.DisplayMember = dtlisteFormalites.Columns[0].ColumnName;
            Formalite.ValueMember = dtlisteFormalites.Columns[0].ColumnName;

            foreach (DataRow dr in dtlisteFormalites.Rows)
            {
                string key = dr["LIBELLE_FORMALITE"].ToString().Trim();
                string value = dr["LIBELLE_FORMALITE_ARAB"].ToString().Trim();
                if (!formaliteARFR.ContainsKey(key))
                {
                    formaliteARFR.Add(key, value);
                }

            }
        }

        //annulation
        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void chargement_listes()
        {
            chargerListeDesjours();
            chargerListeDesAnnes();
            chargerListeDesmois();
            //chargerListeFormalites();
        }

        //charger liste formalites
        private void chargerListeFormalites()
        {

            if (listeFormalite.Count != 0)
            {
                foreach (String name in listeFormalite)
                {
                    Formalite.Items.Add(name);
                }

            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des natures d'origines");
            }
        }

        //charger liste des jours
        private void chargerListeDesjours()
        {

            if (listeJours.Count != 0)
            {
                foreach (String jour in listeJours)
                {
                    jourDropDownList.Items.Add(jour);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des jours");
            }
        }

        //charger liste des annes
        private void chargerListeDesAnnes()
        {
            if (listeAnnes.Count != 0)
            {
                foreach (String anne in listeAnnes)
                {
                    anneDropDownList.Items.Add(anne);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des années");
            }
        }

        //charger lite des mois
        private void chargerListeDesmois()
        {

            if (listesMois.Count != 0)
            {
                foreach (String mois in listesMois)
                {
                    moisDropDownList.Items.Add(mois);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des mois");
            }
        }

        private void ficheSD_Load(object sender, EventArgs e)
        {
            chargement_listes();
        }

        private void Formalite_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (Formalite.Text != "")
            {
                if (formaliteARFR.ContainsKey(Formalite.Text))
                {
                    valueAR.Text = formaliteARFR[Formalite.Text];
                }
                else
                {
                    valueAR.Text = "";
                }
            }
        }

        private void btnSelectionner_Click(object sender, EventArgs e)
        {
        numSousDossier = txt_numero_sd.Text;
        formalite = Formalite.Text;
        volumeDepot = txt_volume_depot.Text;
        numDepot = txt_numero_depot.Text;
        dateDepot = jourDropDownList.Text + "/" + moisDropDownList.Text + "/" + anneDropDownList.Text;        
        this.Close();
        }

    }
}
