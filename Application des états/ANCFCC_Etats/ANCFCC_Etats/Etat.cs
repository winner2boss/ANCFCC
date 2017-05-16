using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;

namespace ANCFCC_Etats
{
    public partial class Etat : Telerik.WinControls.UI.RadForm
    {
        requetteur reqeutteur = new requetteur();
        service service = new service();
        bd basedonne = new bd();
        Boolean condition = false;
        public string fileName;
        public bool openExportFile;

        public Etat()
        {
            InitializeComponent();
        }

        private void Etat_Load(object sender, EventArgs e)
        {
            //chargement liste agents
            DataTable dtAgents = service.getlisteagent();
            listeAgentsBD.DataSource = dtAgents;
            listeAgentsBD.DisplayMember = dtAgents.Columns[1].ColumnName;
            listeAgentsBD.ValueMember = dtAgents.Columns[0].ColumnName;  

            //chargement liste des bases
            DataTable dtlistesites = service.getlistebases();
            listesites.DataSource = dtlistesites;
            listesites.DisplayMember = dtlistesites.Columns[1].ColumnName;
            listesites.ValueMember = dtlistesites.Columns[0].ColumnName;

            listelivrable.Text = "Tous les livrables";
            
            
            
        }

        //selectionnement du site
        private void listesites_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //MessageBox.Show(listesites.SelectedValue.ToString());

            if (condition)
            {
                
                if (listesites.Text != "")
                {
                    int idbase = Convert.ToInt16(listesites.SelectedValue);
                    
                    if (service.chargerLabase(idbase))
                    {
                        DataTable livrableParIdetname = service.getListeLivrable();                                       
                        listelivrable.DataSource = livrableParIdetname;
                        listelivrable.DisplayMember = livrableParIdetname.Columns[1].ColumnName;
                        listelivrable.ValueMember = livrableParIdetname.Columns[0].ColumnName;
                        listelivrable.Items.Add("Tous les livrables");         
                    }
                    else
                    {
                        MessageBox.Show("Erreur récuperation de base");
                        Application.Exit();
                    }
                }
            }
            else
            {
                condition = true;
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            if (listeoperation.Text != "")
            {
                
                    int idbase = Convert.ToInt16(listesites.SelectedValue);
                    string chaine = basedonne.getchaineconnection(idbase);
                    if (chaine != "")
                    {
                        string reqbase = "";
                        if (listeoperation.Text == "Indexation")
                        {
                            reqbase = reqeutteur.getrequetteIndexation().Trim();
                        }
                        else
                        {
                            reqbase = reqeutteur.getrequetteControlle().Trim();
                        }
                        //selectionnement des agents
                        string listeagents = "";
                        int i = 0;
                        foreach (var item in listeAgentsBD.SelectedItems)
                        {
                            if (i == 0)
                            {
                                i++;
                                listeagents = item.Value.ToString();
                            }
                            else
                            {
                                listeagents = listeagents + "," + item.Value.ToString();
                            }
                        }
                        string parameter1 = reqeutteur.getParameterAgent(listeagents);

                        //selectionnement des livrables
                        string listeLivrable = "";
                        string parameter2 = "";
                        if (listelivrable.Text != "Tous les livrables")
                        {
                            listeLivrable = listelivrable.SelectedValue.ToString().Trim();
                            parameter2 = reqeutteur.getParameterLivrable(listeLivrable);
                        }
                        else
                        {
                            parameter2 = "";
                        }

                        //selectionnement par date
                        string parameter3 = "";
                        if (toutelesdates.Checked)
                        {
                            parameter3 = "";
                        }
                        else
                        {
                            string datele = entrele.SelectedDate.ToString();
                            string dateet = etle.SelectedDate.ToString();
                            parameter3 = reqeutteur.getParameterDates(datele, dateet);
                        }

                        string reqfinal = reqbase + " " + parameter1 + " " + parameter2 + " " + parameter3;
                        DataTable dt = basedonne.GetData(reqfinal, chaine);                        
                        gridetats.DataSource = dt;
                        //locker_composants();
                        btnExporter.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Erreur récuperation de base");
                    }
                
            }
            else
            {
                MessageBox.Show("Merci de choisir l'operation en premier ! ", "Erreur");
            }
        }

        private void allAgents_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (allAgents.Checked == true)
            {
                foreach (var agent in listeAgentsBD.Items)
                {
                    agent.Selected = true;
                }
            }
            else
            {
                foreach (var agent in listeAgentsBD.Items)
                {
                    agent.Selected = false;
                }
            }
        }

        private void toutelesdates_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (toutelesdates.Checked == true)
            {
                entrele.Enabled=false;
                etle.Enabled = false;
            }
            else
            {
                entrele.Enabled = true;
                etle.Enabled = true;
            }
            
        }

        private void btnExporter_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "CSV File (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(this.gridetats.ThemeName);
                RadMessageBox.Show("Merci de saisir le nom du fichier");
                return;
            }
            fileName = this.saveFileDialog.FileName;

            openExportFile = true;
            this.RunExportToCSV(fileName, ref openExportFile);
            //this.RunExportToExcelML(fileName, ref openExportFile);
            MessageBox.Show("Operation reussite");
            btnExporter.Enabled = false;
        }

        public void locker_composants()
        {
            allAgents.Enabled = false;
            listeAgentsBD.Enabled = false;
            toutelesdates.Enabled = false;
            entrele.Enabled = false;
            etle.Enabled = false;
            listesites.Enabled = false;
            listelivrable.Enabled = false;
            listeoperation.Enabled = false;

            //enable recherche
            connect.Enabled = false;
        }

        public void unlock_composants()
        {
            allAgents.Enabled = true;
            listeAgentsBD.Enabled = true;
            toutelesdates.Enabled = true;
            entrele.Enabled = true;
            etle.Enabled = true;
            listesites.Enabled = true;
            listelivrable.Enabled = true;
            listeoperation.Enabled = true;

            //enable recherche
            connect.Enabled = true;
        }

        private void newsearche_Click(object sender, EventArgs e)
        {
            unlock_composants();
        }

        //function exeport CSV
        private void RunExportToCSV(string fileName, ref bool openExportFile)
        {
            ExportToCSV csvExporter = new ExportToCSV(this.gridetats);
            csvExporter.SummariesExportOption = SummariesOption.ExportAll;
            try
            {
                csvExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(this.gridetats.ThemeName);
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(this.gridetats.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        //function exeport Excel
        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(this.gridetats);
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;

            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(this.gridetats.ThemeName);
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(this.gridetats.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }

        }

        //etat globale
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            etat_globale etat = new etat_globale();
            etat.ShowDialog();
        }
    }
}
