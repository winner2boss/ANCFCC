using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Consutation_Controle_Validation
{
    public partial class gestionEquipes : Telerik.WinControls.UI.RadForm
    {
        DataTable equipes = new DataTable();
        Service service = new Service();
        string action = "editer";
        string nameRecupered = "";
        

        public gestionEquipes()
        {
            InitializeComponent();
        }

        private void gestionEquipes_Load(object sender, EventArgs e)
        {
            equipes = service.get_totalites_equipes();
            this.grideEquipe.DataSource = equipes;
            
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            foreach (GridViewDataRowInfo row in grideEquipe.SelectedRows)
            {
                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.ColumnInfo.Name == "nom_equipe")
                    {                        
                        string nameequipe = cell.Value.ToString();
                        service.addequipe(nameequipe,1);
                    }
                }

            }
            grideEquipe.Refresh();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //maj gride
        private void maj_Click(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in grideEquipe.Rows)
            {
                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.ColumnInfo.Name == "nom_equipe")
                    {
                        string nameequipeInsered = cell.Value.ToString();

                        service.addequipe(nameequipeInsered, 1);
                    }
                }

            }
            grideEquipe.Refresh();
        }

        //close button
        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //editer
        private void radButton1_Click(object sender, EventArgs e)
        {
            action = "editer";

            foreach (GridViewDataRowInfo row in grideEquipe.SelectedRows)
            {
                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.ColumnInfo.Name == "nom_equipe")
                    {
                        string nameRecupered = cell.Value.ToString();
                        txtnomequipe.Text = nameRecupered;                        
                    }
                }

            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            action = "ajouter";
            button.Text = "Ajouter L'equipe";
            txtnomequipe.Text = "";            
            grideEquipe.ClearSelection();
        }

        //button enregistrer
        private void button_Click(object sender, EventArgs e)
        {
            if (txtnomequipe.Text != "")
            {
                if (action == "editer")
                {
                    if (service.mettre_ajour_name_equipe(nameRecupered, txtnomequipe.Text))
                    {
                        MessageBox.Show("Operation reussite ");
                        equipes = service.get_totalites_equipes();
                        this.grideEquipe.DataSource = equipes;
                    }
                    else
                    {
                        MessageBox.Show("Operation echoué ");
                    }

                }
                else if (action == "ajouter")
                {
                    //verification si le nom equipe existe déja 
                    string equipeinseret = txtnomequipe.Text;
                    if (service.verifier_existance_equipe(equipeinseret))
                    {
                        MessageBox.Show("merci de choisir un autre nom d'equipe");
                    }
                    else
                    {
                        if (service.addequipe(txtnomequipe.Text, 1))
                        {
                            MessageBox.Show("Operation reussite ");
                            equipes = service.get_totalites_equipes();
                            this.grideEquipe.DataSource = equipes;
                            
                        }
                        else
                        {
                            MessageBox.Show("Operation echoué ");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Merci de choisir une action : Ajouter/Editer ");
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir le champ nom equipe");
            }

        }

        //selection rows
        private void grideEquipe_SelectionChanged(object sender, EventArgs e)
        { 
            action = "editer";
            button.Text = "Mettre à jour";

            foreach (GridViewDataRowInfo row in grideEquipe.SelectedRows)
            {
                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.ColumnInfo.Name == "nom_equipe")
                    {
                        nameRecupered = cell.Value.ToString();
                        txtnomequipe.Text = nameRecupered;
                    }
                }

            }

        }
    }
}
