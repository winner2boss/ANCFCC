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


namespace Consutation_Controle_Validation
{
    public partial class detailsRequette : Telerik.WinControls.UI.RadForm
    {
        DataTable resultRequette = new DataTable();

        public string fileName;
        public bool openExportFile;
        Service service = new Service();
        

        //constructeur
        public detailsRequette(DataTable resultRequette)
        {            
            this.resultRequette = service.convert_result_recherche(resultRequette);

            InitializeComponent();
        }

        //loead 
        private void detailsRequette_Load(object sender, EventArgs e)
        {
            
            myGridView.DataSource = resultRequette;
            //saveFileDialog.Filter = "CSV File (*.csv)|*.csv";
            
            
        }

        //function exeport CSV
        private void RunExportToCSV(string fileName, ref bool openExportFile)
        {
            ExportToCSV csvExporter = new ExportToCSV(this.myGridView);
            
            csvExporter.SummariesExportOption = SummariesOption.ExportAll; 
            try 
            { 
                csvExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(this.myGridView.ThemeName);                
            } 
                catch (IOException ex) 
            {
                    RadMessageBox.SetThemeName(this.myGridView.ThemeName);
                    RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error); 
            }
        }

        //function exeport Excel
        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(this.myGridView);
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;

            try
            {
                excelExporter.RunExport(fileName); 
                RadMessageBox.SetThemeName(this.myGridView.ThemeName);
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(this.myGridView.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }

        }

        //selectionnement du choix
        private void ListeChoixExtention_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (ListeChoixExtention.SelectedItem.Text != "" && ListeChoixExtention.SelectedItem!=null)
            {
                btnExporter.Enabled = true;
            }
            else
            {
                btnExporter.Enabled = false;
            }
        }

        //button exporter
        private void btnExporter_Click(object sender, EventArgs e)
        {
            if (ListeChoixExtention.SelectedItem.Text == "Exporter en CSV")
            {
                saveFileDialog.Filter = "CSV File (*.csv)|*.csv";
            }
            else
            {
                saveFileDialog.Filter = "Excel (*.xls)|*.xls"; 
            }

            btnExporter.Enabled = false;            
            waiting.Visible = true;            

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(this.myGridView.ThemeName);
                RadMessageBox.Show("Merci de saisir le nom du fichier");
                return;
            }
            fileName = this.saveFileDialog.FileName;


            waiting.StartWaiting();
            //appelation thread 
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync(1);
            }
        }

        //thread 1 départ
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            openExportFile = true;
            //appelation thread 
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync(1);
            }
            
        }

        //thread 1 progress
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
        
        //thread 1 progress fin
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            
            waiting.StopWaiting();
            waiting.Visible = false;
        }

        //thread 2 départ
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ListeChoixExtention.Text == "Exporter en CSV")
            {
                RunExportToCSV(fileName, ref openExportFile);
            }
            else
            {
                RunExportToExcelML(fileName, ref openExportFile);
            }          
            
            
        }

        //thread 1 progress
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        //thread 3 progress fin
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            waiting.StopWaiting();
            
            
        } 


    }
}
