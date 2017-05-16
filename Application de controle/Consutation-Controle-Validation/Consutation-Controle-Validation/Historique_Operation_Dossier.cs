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
    public partial class Historique_Operation_Dossier : Telerik.WinControls.UI.RadForm
    {
        DataTable detaisHistoriqueDossier = new DataTable();
        public string idDossier;
        public string nameCarton;
        Service service = new Service();

        public Historique_Operation_Dossier(string idDossier, string nameCarton)
        {
            this.nameCarton = nameCarton;
            this.idDossier = idDossier;
            InitializeComponent();
        }

        private void Historique_Operation_Dossier_Load(object sender, EventArgs e)
        {
            this.Text="Historique des opérations effectuées sur le dossier : "+ nameCarton;
            detaisHistoriqueDossier = service.get_Historique_Operation_Dossier(idDossier);
            gridHistoDossierOperation.DataSource = detaisHistoriqueDossier;
        }
    }
}
