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
    public partial class gestion_names_pieces : Telerik.WinControls.UI.RadForm
    {
        Service service = new Service();
        DataTable listePiece = new DataTable();
        public int idutilisateur = 0;

        public gestion_names_pieces()
        {
            InitializeComponent();
        }

        private void gestion_names_pieces_Load(object sender, EventArgs e)
        {
            listePiece = service.chargerListeNomPiecesTable();
            this.listePieceGride.DataSource = listePiece;
        }

        //button quitter
        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //renitialiser
        private void renitialiser_Click(object sender, EventArgs e)
        {
            namePiece.Text = "";
        }

        //button enregistrer
        private void radButton1_Click(object sender, EventArgs e)
        {
            if (namePiece.Text != "")
            {
                if (service.insertNamePiece(namePiece.Text, idutilisateur))
                {
                    MessageBox.Show("Operation Reussie");
                    listePiece = service.chargerListeNomPiecesTable();
                    this.listePieceGride.DataSource = listePiece;
                    namePiece.Text = "";
                }
                else
                {
                    MessageBox.Show("Erreur");
                }
            }
            else
            {
                MessageBox.Show("Erreur");
            }
        }
    }
}
