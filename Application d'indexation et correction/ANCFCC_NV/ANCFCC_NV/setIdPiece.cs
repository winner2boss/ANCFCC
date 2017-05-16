using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Collections;

namespace ANCFCC_NV
{
    public partial class setIdPiece : Telerik.WinControls.UI.RadForm
    {
        public ArrayList listeID = new ArrayList();
        public string idselectionner = "";

        public setIdPiece()
        {
            InitializeComponent();
        }

        public string getText()
        {
            string[] words = listeIDChoix.Text.Split('-');
            return words[0];
        }

        private void setIdPiece_Load(object sender, EventArgs e)
        {            
            foreach (string idC in listeID)
            {
                if (idC != idselectionner)
                {
                    listeIDChoix.Items.Add(idC);
                }
            }
        }

        private void annuler_chng_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void chng_ordre_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void listeIDChoix_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (listeIDChoix.Text != "")
            {
                chng_ordre.Enabled = true;
            }
        }
    }
}
