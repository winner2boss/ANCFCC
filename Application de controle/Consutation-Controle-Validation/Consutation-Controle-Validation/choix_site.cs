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
    public partial class choix_site : Telerik.WinControls.UI.RadForm
    {
        Service service = new Service();
        public int idUtilisateur;

        public choix_site()
        {
            InitializeComponent();
        }

        private void choix_site_Load(object sender, EventArgs e)
        {
            //liste nompieces            
            DataTable dtListeSites = service.chargerlistebases();
            listeBases.DataSource = dtListeSites;
            listeBases.DisplayMember = dtListeSites.Columns[1].ColumnName;
            listeBases.ValueMember = dtListeSites.Columns[0].ColumnName;
        }

        private void showlivrable_Click(object sender, EventArgs e)
        {
            this.Hide();
            Livrable livrable = new Livrable();
            livrable.idUtilisateur = this.idUtilisateur;
            livrable.idbase = Convert.ToInt32(listeBases.SelectedItem.Value);
            livrable.Show(); 
        }
    }
}
