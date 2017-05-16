using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Admistration_ANCFCC
{
    public partial class Administration : Telerik.WinControls.UI.RadForm
    {

        Traitement_Data tData = new Traitement_Data();
        bool nouvau = false;
        int idequipe;
        string typeEquipe = null;

        Boolean maj_utilisateur = true;
        int idutilisateurselected;

        public Administration()
        {
            InitializeComponent();
        }

        private void Administration_Load(object sender, EventArgs e)
        {

            // Chargé type equipe
            DataTable dt = tData.getData("select id_type_equipe,  libelle from tb_type_equipe ");
            dplisttypeEquipes.DataSource = dt;
            dplisttypeEquipes.DisplayMember = dt.Columns[1].ToString();
            dplisttypeEquipes.ValueMember = dt.Columns[0].ToString();


            // Chargé equipe
            DataTable dtequipe = tData.getData("select * from tb_equipe ");
            dpequipe.DataSource = dtequipe;
            dpequipe.DisplayMember = dtequipe.Columns[1].ToString();
            dpequipe.ValueMember = dtequipe.Columns[0].ToString();


            // Chargé groupe
            DataTable dtgroupe = tData.getData("select * from tb_group ");
            dpgroupe.DataSource = dtgroupe;
            dpgroupe.DisplayMember = dtgroupe.Columns[1].ToString();
            dpgroupe.ValueMember = dtgroupe.Columns[0].ToString();


            // Chargé marché
            DataTable dtmarche = tData.getData("select * from tb_bases ");
            dpbases.DataSource = dtmarche;
            dpbases.DisplayMember = dtmarche.Columns[1].ToString();
            dpbases.ValueMember = dtmarche.Columns[0].ToString();
            
            getListEquipe();
            getlisteutilisateur();
            
            
        }
                
        private void radButton1_Click(object sender, EventArgs e)
        {
            GEquipe.Show();
        }

        private void GEquipe_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSaveEquipe_Click(object sender, EventArgs e)
        {
            if (nouvau)
            {
                if (tData.setData("insert into tb_equipe(nom_equipe,type_equipe) values ('" + txtNequipe.Text + "'," + dplisttypeEquipes.SelectedValue + ")"))
                    getListEquipe();
                else
                    MessageBox.Show("erreur");
            }
            else
            {
                if (!dplisttypeEquipes.Text.Equals(typeEquipe))
                {
                    if (tData.setData("update tb_equipe set nom_equipe='" + txtNequipe.Text + "',type_equipe= " + dplisttypeEquipes.SelectedValue + " where id_equipe =" + idequipe))
                        getListEquipe();
                    else
                        MessageBox.Show("erreur");
                }
                else
                {
                    if (tData.setData("update tb_equipe set nom_equipe='" + txtNequipe.Text + "' where id_equipe =" + idequipe))
                        getListEquipe();
                    else
                        MessageBox.Show("erreur");
                }
            }
        }

        private void brnNEquipe_Click(object sender, EventArgs e)
        {
            txtNequipe.Text = null;
            dplisttypeEquipes.Text = null;
            nouvau = true;
            txtNequipe.Enabled = true;
            dplisttypeEquipes.Enabled = true;
            btnSaveEquipe.Enabled = true;
        }

        public void getListEquipe()
        {
            grdequipe.DataSource = tData.getData("select Tequipe.id_equipe as [Numéro Equipe],Tequipe.nom_equipe as [Nom equipe]  , tTyepEquipe.libelle as [Libellé] from TB_equipe Tequipe , tb_type_equipe tTyepEquipe where Tequipe.type_equipe=tTyepEquipe.id_type_equipe");
        }

        public void getlisteutilisateur()
        {
            gridutilisateur.DataSource = tData.getData("select * from vutilisateurs");
        }


        private void grdequipe_SelectionChanged(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in grdequipe.SelectedRows)
            {
                idequipe = Int32.Parse(row.Cells[0].Value.ToString());
                txtNequipe.Text = row.Cells[1].Value.ToString();
                dplisttypeEquipes.Text = row.Cells[2].Value.ToString();
                typeEquipe = row.Cells[2].Value.ToString();
                txtNequipe.Enabled = true;
                dplisttypeEquipes.Enabled = true;
                btnSaveEquipe.Enabled = true;
                nouvau = false;
            }

        }

        private void btnActualiser_Click(object sender, EventArgs e)
        {
            getListEquipe();
            getlisteutilisateur();
        }

        //nouveau
        private void radButton6_Click(object sender, EventArgs e)
        {
            maj_utilisateur = false;
            renitialiser_pageemployer();
        }

        //enregistrer employer
        private void btnsave_Click(object sender, EventArgs e)
        {
            string login = txtlogin.Text;
            string password = txtpassword.Text;
            string txtnom = nom.Text;
            string txtprenom = prenom.Text;
            string txtmatricule = matricule.Text;
            string txtemail = email.Text;
            string txttelephone = telephone.Text;


            if (login == "" )
            {
                MessageBox.Show("Erreur , Merci de remplir toutes les champs");
            }
            else
            {
                if( txtpassword.Enabled==true && password=="")
                {
                    MessageBox.Show("Erreur , Merci de remplir toutes les champs");
                }
                else
                {
                string idequipe = dpequipe.SelectedItem.Value.ToString();
                string idgroupe = dpgroupe.SelectedItem.Value.ToString();
                string idbase = dpbases.SelectedItem.Value.ToString();
                string act = "";
                if (actif.IsChecked)
                {
                    act = "1";
                }
                else
                {
                    act = "0";
                }
                if (maj_utilisateur)
                {
                    string requettepart = "UPDATE [dbo].[TB_Utilisateurs] SET [actif] = " + act + ",[id_equipe] = " + idequipe + " ,[id_group] = " + idgroupe + " ,[id_base] = " + idbase + " ,[login] = '" + login + "',[nom] = '" + txtnom + "',[prenom] = '" + txtprenom + "',[matricule] = '" + txtmatricule + "',[email] = '" + txtemail + "',[telephone] = '" + txttelephone + "' WHERE [id_user]=" + idutilisateurselected + "";
                    if (tData.setData(requettepart))
                    {
                        MessageBox.Show("Operation reussie");
                        getlisteutilisateur();
                        renitialiser_pageemployer();
                    }
                    else
                    {
                        MessageBox.Show("Operation Echoué");
                    }
                }
                else
                {

                    string requettepart1 = "INSERT INTO [dbo].[TB_Utilisateurs] ([actif],[id_equipe],[id_group],[id_base],[login],[pass],[nom],[prenom],[matricule],[email],[telephone])";
                    string requettepart2 = " VALUES (" + act + "," + idequipe + "," + idgroupe + "," + idbase + ",'" + login + "','" + password + "','" + txtnom + "','" + txtprenom + "','" + txtmatricule + "','" + txtemail + "','" + txttelephone + "')";


                    if (tData.setData(requettepart1 + requettepart2))
                    {
                        MessageBox.Show("Operation reussie");
                        getlisteutilisateur();
                        renitialiser_pageemployer();
                    }
                    else
                    {
                        MessageBox.Show("Operation Echoué");
                    }
                }
                }
            
            }
        }

        public void renitialiser_pageemployer()
        {
            txtlogin.Text = "";
            txtpassword.Enabled = true;
            txtpassword.Text = "";
            actif.IsChecked = true;
            inactif.IsChecked = false;

            
        }


        //selectionnement des utilisateur au niveau de la gride
        private void gridutilisateur_SelectionChanged(object sender, EventArgs e)
        {
            foreach (GridViewDataRowInfo row in gridutilisateur.SelectedRows)
            {
                idutilisateurselected = Int32.Parse(row.Cells[0].Value.ToString().Trim());
                txtlogin.Text = row.Cells[2].Value.ToString().Trim();
                txtpassword.Enabled = false;
                if (row.Cells[1].Value.ToString().Trim() == "Actif")
                {
                    actif.IsChecked = true;
                    inactif.IsChecked = false;
                }
                else
                {
                    actif.IsChecked = false;
                    inactif.IsChecked = true;
                }
                dpequipe.Text = row.Cells[3].Value.ToString().Trim();
                dpgroupe.Text = row.Cells[4].Value.ToString().Trim();
                dpbases.Text = row.Cells[5].Value.ToString().Trim();
                nom.Text = row.Cells[6].Value.ToString().Trim();
                prenom.Text = row.Cells[7].Value.ToString().Trim();
                matricule.Text = row.Cells[8].Value.ToString().Trim();
                email.Text = row.Cells[9].Value.ToString().Trim();
                telephone.Text = row.Cells[10].Value.ToString().Trim();
                

                maj_utilisateur = true;
            }
        }
    }
}
