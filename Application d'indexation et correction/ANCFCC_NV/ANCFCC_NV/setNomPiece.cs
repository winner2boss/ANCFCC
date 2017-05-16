using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace ANCFCC_NV
{
    public partial class setNomPiece : Telerik.WinControls.UI.RadForm
    {
        public DataTable listeNomPieces = new DataTable();
        Service service = new Service();

        public setNomPiece()
        {
            InitializeComponent();
        }

        private void annuler_chng_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void setNomPiece_Load(object sender, EventArgs e)
        {
            listeNomPieces = service.chargerListeNomPieces();
            listeNomPiece.DataSource = listeNomPieces;
            listeNomPiece.DisplayMember = listeNomPieces.Columns[1].ColumnName;
            listeNomPiece.ValueMember = listeNomPieces.Columns[0].ColumnName;
        }

        private void listeNomPiece_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (listeNomPiece.Text != "")
            {
                ok_btn.Enabled = true;
            }
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (listeNomPiece.Text == "")
            {
                MessageBox.Show("Merci de remplir le nom ", "Erreur");

            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        public string getText()
        {
            return listeNomPiece.Text;
        }


        //leave nom piece
        private void listeNomPiece_Leave(object sender, EventArgs e)
        {
            if (!listeNomPiece.Items.Contains(listeNomPiece.Text))
            {
                MessageBox.Show("Merci de selectionner un nom de piece parmis la liste de suggestion ! ", "Erreur");
                listeNomPiece.Text = "";
            }
        }
    }
}
