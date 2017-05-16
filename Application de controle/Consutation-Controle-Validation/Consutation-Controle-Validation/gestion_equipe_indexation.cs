using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Collections;
using Telerik.WinControls.UI;

namespace Consutation_Controle_Validation
{
    public partial class gestion_equipe_indexation : Telerik.WinControls.UI.RadForm
    {
        DataTable utilisateur = new DataTable();
        Service service = new Service();
        public int idutulisateur;
        public int id_groupe;
        public int id_equipe;
        public string idutilisateur_recuperer;

        public gestion_equipe_indexation(int idutulisateur, int id_groupe, int id_equipe)
        {
            this.idutulisateur = idutulisateur;
            this.id_groupe = id_groupe;
            this.id_equipe = id_equipe;

            InitializeComponent();
        }

        private void gestion_equipe_indexation_Load(object sender, EventArgs e)
        {
            //cas chef de projet ou administrateur
            if (id_groupe == 5 || id_groupe == 1)
            {
                ArrayList liste_groupe = new ArrayList();
                liste_groupe.Add("Superviseurs");
                liste_groupe.Add("Agents indexation juniors");
                liste_groupe.Add("Agents indexation confirmeés");
                liste_groupe.Add("Agents indexation seniors");
                equipeIndexation.DataSource = service.get_utilisateur_using_liste_groupe(liste_groupe);

                foreach (string groupe in liste_groupe)
                {
                    listeGroupes.Items.Add(groupe);
                }

                
                DataTable equipeDatables = service.get_totalites_equipes();
                listeequipe.DataSource = equipeDatables;
                listeequipe.DisplayMember = equipeDatables.Columns[0].ColumnName;
            }
            //cas superviseur
            else if (id_groupe == 2)
            {

            }
            else
            {
                MessageBox.Show("Vous avez pas le droit d'acceder à cette fenetre");
                this.Close();
            }
        }

        //selectionnement des pieces
        private void equipeIndexation_SelectionChanged(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in equipeIndexation.SelectedRows)
            {
                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.ColumnInfo.Name == "id_utilisateur")
                    {
                        idutilisateur_recuperer = cell.Value.ToString().Trim();                        
                    }
                    else if (cell.ColumnInfo.Name == "username")
                    {
                        txtlogin.Text = cell.Value.ToString().Trim();
                    }
                    else if (cell.ColumnInfo.Name == "equipe")
                    {
                        listeequipe.Text = cell.Value.ToString().Trim();
                    }
                    else if (cell.ColumnInfo.Name == "nom_groupe")
                    {
                        listeGroupes.Text = cell.Value.ToString().Trim();
                    }
                    else if (cell.ColumnInfo.Name == "Etat")
                    {
                        string etat = cell.Value.ToString().Trim();
                        if (etat == "Actif")
                        {
                            actif.IsChecked = true;
                            inactif.IsChecked = false;
                        }
                        else
                        {
                            inactif.IsChecked = true;
                            actif.IsChecked = false;
                        }
                    }
                }

            }
        }

        private void quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
