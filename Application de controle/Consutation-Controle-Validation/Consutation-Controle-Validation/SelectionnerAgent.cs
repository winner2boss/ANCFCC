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
    public partial class SelectionnerAgent : Telerik.WinControls.UI.RadForm
    {

        public int id_user_operation;
        public Boolean correction = false;

        Service service = new Service();
        ArrayList listeidDossiers = new ArrayList();

        public string idLivrable;

        public SelectionnerAgent(string idLivrable)
        {
            InitializeComponent();
            //chargement liste agents
            
        }

        private void Reindexation_Load(object sender, EventArgs e)
        {
            if (correction)
            {
                agentIndexation.Text = "Merci de choisir un agent pour correction :";
            }
        }

        public void affectation(ArrayList listeIdDossiersRecupered)
        {

            DataTable dtAgents = service.getListeAgents(idLivrable, "all");
            listeAgents.DataSource = dtAgents;
            listeAgents.DisplayMember = dtAgents.Columns[1].ColumnName;
            listeAgents.ValueMember = dtAgents.Columns[0].ColumnName;
            listeidDossiers = listeIdDossiersRecupered;
            this.Text = "Affectation";
            agentIndexation.Text = "Merci de choisir un agent pour affectation";
            loginAgentIndexation.Visible = false;
            btnSelectionner.Text = "Affecter le(s) dossier(s)";
        }

        public Boolean desaffectation(ArrayList listeIdDossiersRecupered)
        {
            Boolean condition = false;
            listeidDossiers = listeIdDossiersRecupered;
            if (correction)
            {
                if (service.desaffecterDossiersCorrection(id_user_operation, listeIdDossiersRecupered))
                {
                    condition = true;
                }
                else
                {
                    condition = false;
                }
            }
            else
            {
                if (service.desaffecterDossiers(id_user_operation, listeIdDossiersRecupered))
                {
                    condition = true;
                }
                else
                {
                    condition = false;
                }
            }

            return condition;            
        }

        public void reaffectation(ArrayList listeIdDossiersRecupered)
        {            
            if (this.desaffectation(listeIdDossiersRecupered))
            {
                DataTable dtAgents = service.getListeAgents(idLivrable, "all");
                listeAgents.DataSource = dtAgents;
                listeAgents.DisplayMember = dtAgents.Columns[1].ColumnName;
                listeAgents.ValueMember = dtAgents.Columns[0].ColumnName;
                listeidDossiers = listeIdDossiersRecupered;
                this.Text = "Réaffectation";
                agentIndexation.Text = "Merci de choisir un agent pour la réaffectation";
                loginAgentIndexation.Visible = false;
                btnSelectionner.Text = "Reaffecter le(s) dossier(s)";
            }
        }

        //button annuler
        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //button selectionner
        private void btnSelectionner_Click(object sender, EventArgs e)
        {
            string idAgentSelectionner = listeAgents.SelectedValue.ToString();
            if (correction)
            {
                if (service.affecterDossiersCorrection(id_user_operation, idAgentSelectionner, listeidDossiers))
                {
                    if (this.Text == "Réaffectation")
                    {
                        MessageBox.Show("Réaffectation reussite");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Affectation reussite");
                        this.Close();
                    }

                }
                else
                {
                    if (this.Text == "Réaffectation")
                    {
                        MessageBox.Show("Réaffectation Echoué");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Affectation Echoué");
                        this.Close();
                    }
                }
            }
            else
            {
                if (service.affecterDossiers(id_user_operation, idAgentSelectionner, listeidDossiers))
                {
                    if (this.Text == "Réaffectation")
                    {
                        MessageBox.Show("Réaffectation reussite");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Affectation reussite");
                        this.Close();
                    }

                }
                else
                {
                    if (this.Text == "Réaffectation")
                    {
                        MessageBox.Show("Réaffectation Echoué");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Affectation Echoué");
                        this.Close();
                    }
                }
            }
            
        }
    }
}
