using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Consutation_Controle_Validation
{
    public partial class ViewReport : Telerik.WinControls.UI.RadForm
    {
        bd basedonnee = new bd();
        DataTable dt;
        public string id_livrable { set; get; }
        public string nom_livrable { set; get; }
        public ViewReport()
        {
            InitializeComponent();
        }

        private void ViewReport_Load(object sender, EventArgs e)
        {
            dt = basedonnee.GetData("SELECT * FROM  VEtatGlobal where [Code Livrable]="+id_livrable);            
            ReportDocument myReport = new ReportDocument();
            string cheminRapport = Application.StartupPath + "\\etatGlobal.rpt";
            myReport.Load(cheminRapport);
            myReport.SetDataSource(dt);
            myReport.SetParameterValue("livrable", nom_livrable);
            crystalReportViewer.ReportSource = myReport;   
            
        }
    }
}
