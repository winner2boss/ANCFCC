using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Collections;
using Telerik.WinControls.UI;
using Consutation_Controle_Validation.Properties;
using System.IO;

namespace Consutation_Controle_Validation
{
    public partial class Home : Telerik.WinControls.UI.RadForm
    {
        
        public int idUtilisateur;
        public int idLivrable;
        public string nomLivrable;

        //couche service
        Service service = new Service();        
        //result req 
        public string reqResult = "";
        //parametre pour chargement fiche dossier
        string namePiecePrecedementSelectionner = "";
        //id dossier ouvert actuellement
        string idDossierOuvertActuellement="";
        //name dossier ouvert actuellement
        string namedossierouvertactuellement = "";
        //id statue dossier ouvert actuellement
        string statueDossierOuvertActuellement;
        //id statue îece selectionner actuellement
        string statuePieceSelectionnerActuellement;
        //action appliquer sur le dossier
        string action = "";
        //condition de modification
        //id de la base
        public int idbase;
        //fiche piece
        Boolean FichePieceErreur = false;
        Boolean nbrPagesFichePieceErreur = false;
        Boolean numPagesFichePieceErreur = false;
        //fiche sous dossier
        Boolean FicheSousDossierErreur = false;
        Boolean numSousDossierErreur = false;
        Boolean volumeDepotErreur = false;
        Boolean numeroDepotErreur = false;
        //fiche dossier
        Boolean FicheDossierErreur = false;
        Boolean numOrigineErreur = false;
        Boolean numTitreErreur = false;
        //-----------------------------------------------------------------
            //charger les DATA tables du formulaire  
        //-----------------------------------------------------------------
        public static DataTable listeTotaliteUtilisateurs = new DataTable();
        DataTable resultRecherche = new DataTable();
        DataTable indexFicheDossier = new DataTable();
        DataTable listePieces = new DataTable();
        //-----------------------------------------------------------------
            //charger les listes du formulaire  
        //-----------------------------------------------------------------
        //charger les noms des pieces
        ArrayList listeNomPieces = Service.chargerListeNomPieces();        
        //charger les liste du ficher sous dossier
        ArrayList listeFormalite = Service.chargerListeFormalites();
        ArrayList listeJours = Service.chargerListeDesjours();
        ArrayList listesMois = Service.chargerListeDesmois();
        ArrayList listeAnnes = Service.chargerListeDesAnnes();        
        //charger les listes fiche dossier
        ArrayList listeNaturesOrigines=Service.chargerlisteNatureOrigine();
        DataTable listeIndiceOrigine = Service.chargerlisteIndiceOrigine();
        DataTable listeIndiceDetitre = Service.chargerlisteIndiceTitre();
        //-----------------------------------------------------------------
            //information des images
        //-----------------------------------------------------------------
        /*Image*/
        Bitmap image = null;
        Stream image_stream;
        //value zoom
        double zoom = 1;
        int defImgWidth;
        int defImgHeight;
        /*Image*/
        //-----------------------------------------------------------------
            //les values statistiques recherche
        //-----------------------------------------------------------------
        int nbrNonAffecter = 0, nbrNonindexer = 0, nbrIndexer = 0, nbrVerifiers = 0, nbrValides = 0, nbrRejeter = 0, nbrAlivrer = 0, nbrLivrer = 0, nbrInstance = 0, nbrErreurscan = 0;
        //-----------------------------------------------------------------

        public string id_user_indexation;
        public string id_user_affectation;
        public string id_user_controle;
        public string nbr_Images;
        public string ref_dossier;
        public string name_status_dossier;
        public string id_user_verification;

        //dictionnaire arabe francais
        Dictionary<string, string> formaliteARFR = new Dictionary<string, string>();

        //pour gerer l'affichage des buttons valider et rejeter
        Dictionary<string, string> listeIdPieceWithstatue = new Dictionary<string, string>();
        
        //id statue recupered par defaut 0
        string idSeulStatueRecuperer = "0";        

        //edition marginal
        Boolean modification = false;


        //chargement liste des indices titre
        private void chargerListeDesindicesTitre()
        {

            IndiceTitre.DataSource = listeIndiceDetitre;
            IndiceTitre.DisplayMember = listeIndiceDetitre.Columns[1].ColumnName;
            IndiceTitre.ValueMember = listeIndiceDetitre.Columns[0].ColumnName;

        }

        //chargement liste des indices d'origines
        private void chargerListeDesindicesOrigines()
        {

            
            txt_indice_orgine.DataSource = listeIndiceOrigine;
            txt_indice_orgine.DisplayMember = listeIndiceOrigine.Columns[1].ColumnName;
            txt_indice_orgine.ValueMember = listeIndiceOrigine.Columns[0].ColumnName;

            
        }

        //chargement liste des natures d'origines
        private void chargerListeDesNaturesOrigines()
        {

            if (listeNaturesOrigines.Count != 0)
            {
                foreach (String name in listeNaturesOrigines)
                {
                    txt_nature_orgine.Items.Add(name);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des natures d'origines");
            }
        }
        
        //charger liste des annes
        private void chargerListeDesAnnes()
        {
            if (listeAnnes.Count != 0)
            {
                foreach (String anne in listeAnnes)
                {
                    anneDropDownList.Items.Add(anne);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des années");
            }
        }
        
        //charger lite des mois
        private void chargerListeDesmois()
        {

            if (listesMois.Count != 0)
            {
                foreach (String mois in listesMois)
                {
                    moisDropDownList.Items.Add(mois);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des mois");
            }
        }
        
        //charger liste des jours
        private void chargerListeDesjours()
        {

            if (listeJours.Count != 0)
            {
                foreach (String jour in listeJours)
                {
                    jourDropDownList.Items.Add(jour);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des jours");
            }
        }
        
        //charger liste formalites
        private void chargerListeFormalites()
        {

            if (listeFormalite.Count != 0)
            {
                foreach (String name in listeFormalite)
                {
                    Formalite.Items.Add(name);
                }
                
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des natures d'origines");
            }
        }

        public Home()
        {
            InitializeComponent();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {            
            Application.Exit();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
            livrableActuel.Text = "Livrable Actuel : "+nomLivrable;            
            //clear value result
            clearValuesTextes();
            //fin clear value

            //chargement des dates
            DataTable dtDatesEntre = service.getProd_Dates();
            entreDate.DataSource = dtDatesEntre;
            entreDate.DisplayMember = dtDatesEntre.Columns[0].ColumnName;

            DataTable dtDatesEt = service.getProd_Dates();
            etdate.DataSource = dtDatesEt;
            etdate.DisplayMember = dtDatesEt.Columns[0].ColumnName;            

            //chargement liste agents
            DataTable dtAgents = service.getListeAgents("indexation");
            listeAgentsBD.DataSource = dtAgents;
            listeAgentsBD.DisplayMember = dtAgents.Columns[1].ColumnName;
            listeAgentsBD.ValueMember = dtAgents.Columns[0].ColumnName;  

            //chargement des statues            
            DataTable dtStatus = service.getStatusDossier();
            listeStatus.DataSource = dtStatus;
            listeStatus.DisplayMember = dtStatus.Columns[1].ColumnName;
            listeStatus.ValueMember = dtStatus.Columns[0].ColumnName;

            //chargement des tranches
            DataTable dtTraches = service.chargerlisteTranches();
            if (dtTraches.Rows.Count != 0)
            {
                listeTranches.DataSource = dtTraches;
                listeTranches.DisplayMember = dtTraches.Columns[1].ColumnName;
                listeTranches.ValueMember = dtTraches.Columns[0].ColumnName;
            }
            
            listeTotaliteUtilisateurs = service.getListeAgents(idLivrable.ToString(), "all");

            // Action de Cloture de dossier 
            this.Ouvrir.Click += new EventHandler(Ouvrir_Click);
            // cacher les bouton de controle image
            toolStrip1.Visible = false;
            //Action de validation de dossier
            this.validerDossier.Click += new EventHandler(validerDossier_Click);
            //Action de rejet de dossier
            this.rejeterDossier.Click += new EventHandler(rejeterDossier_Click);
            //action verification de dossier
            this.validerVerification.Click += new EventHandler(validerVerificationDossier_Click);
            //Action de reindexation
            this.reindexation.Click += new EventHandler(reindexation_Click);
            //action ouvertur dossier en mode verification
            this.verificationDossier.Click += new EventHandler(verificationDossier_Click);
            //action validerProdJournee valider la totalité des dossiers
            this.validerProdJournee.Click += new EventHandler(validerProdJournee_Click);
            //action rejeterProdJourne pour rejeter la totalités des dossiers
            this.rejeterProdJourne.Click += new EventHandler(rejeterProdJourne_Click);
            //action envoyer la prod valider pour livraison
            this.envoyerDatePourLivraison.Click += new EventHandler(envoyerDatePourLivraison_Click);
            //action de supression
            this.Suprimer.Click += new EventHandler(suprimer_Click);

            
            //actions nouvea menue

            //ouverture simple    
            this.Ouvrir_dossier.Click += new EventHandler(Ouvrir_dossier_Click);
            //cloturer dossier
            Cloturer_dossier.Enabled = false;
            this.Cloturer_dossier.Click += new EventHandler(Cloturer_dossier_Click);
            //renvoyer pour reindexation
            Envoyer_reedexation.Enabled = false;
            //changer statue
            Changer_statut.Enabled = false;
            this.Changer_statut.Click += new EventHandler(Changer_statut_Click);
            //historique
            this.Historique_dossier.Click += new EventHandler(historique_dossier_Click);

           

            //desactiver les options de validation
            validerDossier.Enabled = false;
            rejeterDossier.Enabled = false;
            reindexation.Enabled = false;
            validerVerification.Enabled = false;
            //charger les listes formulaires
            chargerLesListesDuFormulaire();

            //droit sur le menue en haut
            menue_droit();
        }

        public void menue_droit()
        {
            //droit affectation
            Boolean droitAffectation = service.verifier_le_droit(5,idUtilisateur);
            if (droitAffectation)
            {
                menue2Affecter.Enabled = true;
                affectationCorrection.Enabled = true;
            }
            else
            {
                menue2Affecter.Enabled = false;
                affectationCorrection.Enabled = false;
            }

            //droit desaffectation
            Boolean droitdesaffectationn = service.verifier_le_droit(6, idUtilisateur);
            if (droitdesaffectationn)
            {
                menue2Dessafecté.Enabled = true;
                desaffecterCorrection.Enabled = true;
            }
            else
            {
                menue2Dessafecté.Enabled = false;
                desaffecterCorrection.Enabled = false;
            }

            //doit sur le menue controle
            Boolean droitcontrole = service.verifier_le_droit(2, idUtilisateur);
            if (droitcontrole)
            {
                menuecontrole.Enabled = true;
            }
            else
            {
                menuecontrole.Enabled = false;
            }

           //droit ajout formalite , indice , liste piece
            Boolean droitAjoutListeDeroulante = service.verifier_le_droit(13, idUtilisateur);
            if(droitAjoutListeDeroulante)
            {
                formalitemenue.Enabled = true;
                menueListePiece.Enabled = true;
                menueindice.Enabled = true;
            }
            else
            {
                formalitemenue.Enabled = false;
                menueListePiece.Enabled = false;
                menueindice.Enabled = false;
            }


            //droit pour include tout les livrables
            Boolean droitInclureTousLesLivrables = service.verifier_le_droit(14, idUtilisateur);
            if (droitInclureTousLesLivrables)
            {
                inclurelivrables.Enabled = true;                
            }

            //droit suprimer les vues sur application de controle
            Boolean suprimerVues = service.verifier_le_droit(15, idUtilisateur);
            if (suprimerVues)
            {
                Suprimer.Enabled = true;
            }
            else
            {
                Suprimer.Enabled = false;
            }

        }
        
        //charger les listes formulaires
        private void chargerLesListesDuFormulaire()
        {
            //chargement des listes des noms des pieces
            NomPiece.Items.Clear();
            foreach (String name in listeNomPieces)
            {
                NomPiece.Items.Add(name);
            }
            //charger la liste des annés
            chargerListeDesAnnes();
            //charger la liste des mois
            chargerListeDesmois();
            //charger la liste des jours
            chargerListeDesjours();
            //charger la liste des formalites
            //chargerListeFormalites();
            //chargement de la liste natures origines
            chargerListeDesNaturesOrigines();
            //chargement de la liste des indices d'origine
            chargerListeDesindicesOrigines();
            //charger les indices titre
            chargerListeDesindicesTitre();
            //chargement dictionnaire


            //liste formalites
            DataTable dtlisteFormalites = service.chargerlisteFormalites();
            Formalite.DataSource = dtlisteFormalites;
            Formalite.DisplayMember = dtlisteFormalites.Columns[0].ColumnName;
            Formalite.ValueMember = dtlisteFormalites.Columns[0].ColumnName;
            
            foreach (DataRow dr in dtlisteFormalites.Rows)
            {
                string key = dr["LIBELLE_FORMALITE"].ToString().Trim();
                string value = dr["LIBELLE_FORMALITE_ARAB"].ToString().Trim();
                if (!formaliteARFR.ContainsKey(key))
                {
                    formaliteARFR.Add(key, value);
                }

            }
        }

        //envoyer le dossier pour reindexation
        private void reindexation_Click(object sender, EventArgs e)
        {
            
            //SelectionnerAgent reindexation = new SelectionnerAgent();
            //reindexation.name_agent = unite_dossier.SelectedNode.Parent.Name.ToString();
            //reindexation.ShowDialog();
        }

        //envoyer la prod d'une date valide pour livraison
        private void envoyerDatePourLivraison_Click(object sender, EventArgs e)
        {            

            int nbrselection = unite_dossier.SelectedNode.Nodes.Count;
            for (int i = 0; i < nbrselection; i++)
            {
                service.changerStatueDossier(idUtilisateur,unite_dossier.SelectedNode.Nodes[i].Value.ToString(), 9);
            }
            relancer_recherche();
        }

        //rejeter la prod de la totalité des dossiers
        private void rejeterProdJourne_Click(object sender, EventArgs e)
        {

            int nbrselection = unite_dossier.SelectedNode.Nodes.Count;
            for (int i = 0; i < nbrselection; i++)
            {

                service.changerStatueDossier(idUtilisateur,unite_dossier.SelectedNode.Nodes[i].Value.ToString(), 7);
            }
            relancer_recherche();
        }

        //valider la prod de la totalité des dossiers
        private void validerProdJournee_Click(object sender, EventArgs e)
        {
           
            int nbrselection = unite_dossier.SelectedNode.Nodes.Count;
            for (int i = 0; i < nbrselection; i++)
            {

                service.changerStatueDossier(idUtilisateur,unite_dossier.SelectedNode.Nodes[i].Value.ToString(), 6);
            }                
            relancer_recherche(); 
        }

        //button ouvrir pour verification
        private void verificationDossier_Click(object sender, EventArgs e)
        {
            action = "verification";
            //desactiver la possibilité d'ouvrire un autre dossier
            Ouvrir.Enabled = false;
            //desactiver la possibilité d'ouvrire un autre dossier            

            //activer les option de rejet et de validation
            validerDossier.Enabled = false;
            rejeterDossier.Enabled = false;
            //activer les option de rejet et de validation

            //desactivation de du choix de verification
            verificationDossier.Enabled = false;
            //desactivation de du choix de verification

            //activation du choix validation de verification
            reindexation.Enabled = true;
            validerVerification.Enabled = true;
            //activation du choix validation de verification

            //afficher les option de controle image
            toolStrip1.Visible = true;
            this.pb.Image = null;
            //fin afficher les option de controle image
            RadTreeNode TF = unite_dossier.SelectedNode;
            listeIdPieceWithstatue.Clear();
            listePieces.Clear();
            listePieces = service.getListePieceTF(TF.Value.ToString());
            idDossierOuvertActuellement = TF.Value.ToString();
            namedossierouvertactuellement = TF.Name.ToString();
            foreach (DataRow row in listePieces.Rows)
            {
                RadTreeNode piece = new RadTreeNode();
                piece.Value = row["id_vue"];
                piece.Name = row["id_vue"].ToString();
                piece.Text = row["nom_piece"].ToString();
                if (row["nom_piece"].ToString() == "PAGE DE GARDE DU DOSSIER" && row["id_status"].ToString() != "7")
                {
                    piece.Image = Resources.redFolder;
                }
                else if (row["nom_piece"].ToString() == "PAGE DE GARDE DU SOUS DOSSIER" && row["id_status"].ToString() != "7")
                {
                    piece.Image = Resources.jauneDossier;
                }
                else
                {
                    if (row["id_status"].ToString() == "6")
                    {
                        piece.Image = Resources.document_ok;
                    }
                    else if (row["id_status"].ToString() == "7")
                    {
                        piece.Image = Resources.document_rejet;
                    }
                    else
                    {
                        piece.Image = Resources.document;
                    }
                }
                piece.ContextMenu = MenuePiece;
                TF.Nodes.Add(piece);
                listeIdPieceWithstatue.Add(row["id_vue"].ToString(), row["id_status"].ToString());
            }
            //fiche dossier
            indexFicheDossier.Clear();
            indexFicheDossier = service.getIndexTF(TF.Value.ToString());
            DataRow index = indexFicheDossier.Rows[0];
            txt_nature_orgine.Text = index["numero_origine"].ToString();
            txt_numero_orgine.Text = index["nature_origine"].ToString();
            txt_indice_orgine.Text = index["indice_origine"].ToString();
            txt_indice_speciale_orgine.Text = index["indice_sporigine"].ToString();
            txt_numero_titre.Text = index["numero_titre"].ToString();
            IndiceTitre.Text = index["indice_titre"].ToString();
            txt_indice_spciale_titre.Text = index["indice_sptitre"].ToString();
            //fin fiche dossier            
        }



        private void suprimer_Click(object sender, EventArgs e)
        {
            string idPiece = unite_dossier.SelectedNode.Name;
            DataRow[] pieceRow = listePieces.Select("id_vue='" + idPiece + "'");
            pieceRow[0]["id_status"] = "5";
            if (service.updateFichePieces(idPiece.ToString(), "Image suprimer", "", "") && service.deleteVue(idPiece.ToString()))
            {
                unite_dossier.SelectedNode.Text = "Image suprimer";
                unite_dossier.SelectedNode.Image = Resources.document_delete;
            }
        }

        //ouvrir simple
        private void Ouvrir_dossier_Click(object sender, EventArgs e)
        {
            //desactivation de l'option d'ouverture
            Ouvrir_dossier.Enabled = false;
            //activation de la possibilité de cloturer aprés ouverutre
            Cloturer_dossier.Enabled = true;
            //activation de la possibilité de changer le statue aprés ouverutre
            Changer_statut.Enabled = true;

            //afficher les option de controle image
            toolStrip1.Visible = true;
            this.pb.Image = null;
            //fin afficher les option de controle image

            //chargement des pieces
            RadTreeNode TF = unite_dossier.SelectedNode;
            listeIdPieceWithstatue.Clear();
            listePieces.Clear();
            listePieces = service.getListePieceTF(TF.Value.ToString());
            idDossierOuvertActuellement = TF.Value.ToString();
            namedossierouvertactuellement = TF.Name.ToString();

            //recupération de statue dossier
            DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossierOuvertActuellement + "");
            statueDossierOuvertActuellement = dossierRow[0]["id_status"].ToString();
            //------ enabled buttons reindexation---------
            enable_reindexation(statueDossierOuvertActuellement);            
            //---------------------------------------------

            // variables dataTablesRecuperée
            string id_vue = "";
            string nom_piece = "";
            string id_statue = "";

            foreach (DataRow row in listePieces.Rows)
            {
                id_vue = row["id_vue"].ToString().Trim();
                nom_piece = row["nom_piece"].ToString().Trim();
                id_statue = row["id_status"].ToString().Trim();

                RadTreeNode piece = new RadTreeNode();
                piece.Value = id_vue;
                piece.Name = id_vue;

                //affichage nom de la piece
                if (nom_piece != "")
                {
                    piece.Text = nom_piece;
                }
                else
                {
                    piece.Text = id_vue;
                }

                //affichage image de la piece
                piece.Image = getImage_Piece_By_Statue_et_nom(nom_piece, id_statue);
                //ajout de la piece au dossier
                piece.ContextMenu = MenuePiece;
                TF.Nodes.Add(piece);
                //dictionnary !
                listeIdPieceWithstatue.Add(id_vue, id_statue);
            }

            //préparation fiche dossier
            indexFicheDossier.Clear();
            indexFicheDossier = service.getIndexTF(TF.Value.ToString());
            DataRow index = indexFicheDossier.Rows[0];
            txt_nature_orgine.Text = index["nature_origine"].ToString();
            txt_numero_orgine.Text = index["numero_origine"].ToString();
            txt_indice_orgine.Text = index["indice_origine"].ToString();
            txt_indice_speciale_orgine.Text = index["indice_sporigine"].ToString();
            txt_numero_titre.Text = index["numero_titre"].ToString();
            IndiceTitre.Text = index["indice_titre"].ToString();
            txt_indice_spciale_titre.Text = index["indice_sptitre"].ToString();
            //fin fiche dossier 

        }

        //cloture simple
        private void Cloturer_dossier_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value == idDossierOuvertActuellement)
            {
                
                unite_dossier.SelectedNode.Nodes.Clear();

                //desactivation de la possibilité de cloturer
                Cloturer_dossier.Enabled = false;

                //possibilité d'ouvrir un autre dossier
                Ouvrir_dossier.Enabled = true;

                //desactivation de la possibilité de changer le statue aprés ouverutre
                Changer_statut.Enabled = false;
            }
            else
            {
                MessageBox.Show("Merci declôturer où changer le statut du dossier : " + namedossierouvertactuellement + " avant d'effectuer cette operation ");
            }
        }


        //charger historique dossier statue
        private void historique_dossier_Click(object sender, EventArgs e)
        {
            string idDossier = unite_dossier.SelectedNode.Value.ToString();
            string namecarton = unite_dossier.SelectedNode.Text.ToString().Trim();
            Historique_Operation_Dossier historique_dossier = new Historique_Operation_Dossier(idDossier, namecarton);
            historique_dossier.idDossier = idDossier;
            historique_dossier.nameCarton = namecarton;
            historique_dossier.ShowDialog();
            
        
        }

        //chnager statue
        private void Changer_statut_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value == idDossierOuvertActuellement)
            {
                choixStatue choixstatue = new choixStatue(idDossierOuvertActuellement,statueDossierOuvertActuellement,idUtilisateur);
                choixstatue.idlivrable = idLivrable;
                choixstatue.ShowDialog();
                relancer_recherche();
            }
            else
            {
                MessageBox.Show("Merci declôturer où changer le statut du dossier : " + namedossierouvertactuellement + " avant d'effectuer cette operation ");
            }
        }

        //get image piece by statue et nom
        public Bitmap getImage_Piece_By_Statue_et_nom(string nomPiece,string id_statue)
        {

            Bitmap image = Resources.document;

            if (nomPiece == "PAGE DE GARDE DU DOSSIER" && id_statue != "6")
            {
                //à changer
                image = Resources.redFolder;
            }
            else if (nomPiece == "PAGE DE GARDE DU DOSSIER" && id_statue != "7")
            {
                //à changer
                image = Resources.redFolder;
            }
            else if (nomPiece == "PAGE DE GARDE DU SOUS DOSSIER" && id_statue != "6")
            {
                //à changer
                image = Resources.jauneDossier;
            }
            else if (nomPiece == "PAGE DE GARDE DU SOUS DOSSIER" && id_statue != "7")
            {
                //à changer
                image = Resources.jauneDossier;
            }
            else
            {
                if (id_statue == "6")
                {
                    //document valider
                    image = Resources.document_ok;
                }
                else if (id_statue == "7")
                {
                    //document rejeter
                    image = Resources.document_rejet;
                }
                else if (id_statue == "8")
                {
                    //document verifier
                    image = Resources.document_verifier;
                }
                else if (id_statue == "10")
                {
                    //document verifier
                    image = Resources.document_corriger;
                }
                else if (id_statue == "5")
                {
                    //document suprimer
                    image = Resources.document_delete;
                }                
                else
                {
                    image = Resources.document;
                }
            }
            return image;
        }

        //button réeindexation enable disable en fonction du statue dossier et droit user
        public void enable_reindexation(string id_statue_dossier)
        {
            Boolean droit = service.verifier_le_droit(4,idUtilisateur);

            if ((id_statue_dossier == "3" || id_statue_dossier == "7" || id_statue_dossier == "8" || id_statue_dossier == "8") && droit)
            {
                Envoyer_reedexation.Enabled = true;
            }            
            else
            {
                Envoyer_reedexation.Enabled = false;
            }
            
        }

        //valider la verification
        private void validerVerificationDossier_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement)
            {
            service.changerStatueDossier(idUtilisateur,unite_dossier.SelectedNode.Value.ToString(), 8);
            relancer_recherche();

            //desactiver la possibilité d'ouvrire un autre dossier
            Ouvrir.Enabled = true;
            //desactiver la possibilité d'ouvrire un autre dossier            

            //activer les option de rejet et de validation
            validerDossier.Enabled = false;
            rejeterDossier.Enabled = false;
            //activer les option de rejet et de validation

            //desactivation de du choix de verification
            verificationDossier.Enabled = true;
            //desactivation de du choix de verification
            }
            else
            {
                MessageBox.Show("Merci de valider la verification du dossier " + namedossierouvertactuellement + " en premier");
            }
        }

        //valider dossier
        private void validerDossier_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement)
            {
                service.changerStatueDossier(idUtilisateur,unite_dossier.SelectedNode.Value.ToString(), 6);
                relancer_recherche();

                //desactiver la possibilité d'ouvrire un autre dossier
                Ouvrir.Enabled = true;
                //desactiver la possibilité d'ouvrire un autre dossier

                //activer les option de rejet et de validation
                validerDossier.Enabled = false;
                rejeterDossier.Enabled = false;
                //activer les option de rejet et de validation

                //desactivation de du choix de verification
                verificationDossier.Enabled = true;
                //desactivation de du choix de verification
            }
            else
            {
                MessageBox.Show("Merci de valider/rejeter le dossier " + namedossierouvertactuellement + " en premier");
            }
        }

        //rejeter dossier
        private void rejeterDossier_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement)
            {
                service.changerStatueDossier(idUtilisateur,unite_dossier.SelectedNode.Value.ToString(), 7);            
            relancer_recherche();

            //desactiver la possibilité d'ouvrire un autre dossier
            Ouvrir.Enabled = true;
            //desactiver la possibilité d'ouvrire un autre dossier            

            //activer les option de rejet et de validation
            validerDossier.Enabled = false;
            rejeterDossier.Enabled = false;
            //activer les option de rejet et de validation

            //desactivation de du choix de verification
            verificationDossier.Enabled = true;
            //desactivation de du choix de verification
            }
            else
            {
                MessageBox.Show("Merci de valider/rejeter le dossier " + namedossierouvertactuellement + " en premier");
            }
        }

        private void listeAgents_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            
        }

        //recherche
        private void connect_Click(object sender, EventArgs e)
        {
           

            //renitialisation des composant de l'interface
            composant_interface_clear();

            //generateur requette
            reqResult = generateurRequette();

            //la recherche composer pour recuperer les dossier
            int nbrDossier = lancer_recherche(reqResult);

            //generer le menue racine
            menue_racine(nbrDossier);


            

        }

        //option menue racine
        public void menue_racine(int nbrDossier)
        {

            if ((idSeulStatueRecuperer == "3" || idSeulStatueRecuperer == "8" || idSeulStatueRecuperer == "10") && nbrDossier > 0)
            {
                rejeterProdJourne.Enabled = true;
                validerProdJournee.Enabled = true;
                envoyerDatePourLivraison.Enabled = true;
            }
            else if (idSeulStatueRecuperer == "6" && nbrDossier > 0)
            {
                rejeterProdJourne.Enabled = true;
                validerProdJournee.Enabled = false;
                envoyerDatePourLivraison.Enabled = true;
            }
            else if (idSeulStatueRecuperer == "7" && nbrDossier > 0)
            {
                rejeterProdJourne.Enabled = false;
                validerProdJournee.Enabled = true;
                envoyerDatePourLivraison.Enabled = false;
            }
            else
            {
                rejeterProdJourne.Enabled = false;
                validerProdJournee.Enabled = false;
                envoyerDatePourLivraison.Enabled = false;
            }
        }

        public void composant_interface_clear()
        {
            statutPiece.Text = "Statut :";
            statutPiece.ForeColor = Color.Black;
            typeLabel.Text = "Type :";
            tailleLabel.Text = "Taille :";

            //desactiver la possibilité d'ouvrire un autre dossier
            Ouvrir.Enabled = true;
            //desactiver la possibilité d'ouvrire un autre dossier            

            //activer les option de rejet et de validation
            validerDossier.Enabled = false;
            rejeterDossier.Enabled = false;
            //activer les option de rejet et de validation

            //desactivation de du choix de verification
            verificationDossier.Enabled = true;
            reindexation.Enabled = false;
            validerVerification.Enabled = false;
            //desactivation de du choix de verification

            //vider totalités des indexes
            viderTotaliterIndexes();
            //marquer les button de controlle
            btnValider.Enabled = false;
            btnRejeter.Enabled = false;
            corriger.Enabled = false;
            //fin masquer les buttons de controle
            //masquer la saisie 
            disabledZoneSaisie();
            //fin maquer la saisie
            //cacher les option de control image
            toolStrip1.Visible = false;
            this.pb.Image = null;
            //fin cacher les option de control image
        }

        public string getNameUser(string idUser)
        {
            if (idUser != "")
            {
                DataRow[] utilisateur = listeTotaliteUtilisateurs.Select("id_user = " + idUser + "");
                string username = utilisateur[0]["login"].ToString().Trim() + " [" + utilisateur[0]["matricule"].ToString().Trim() + "]";
                return username;
            }
            else
                return "";
        }

        public void renitialiser_menueContext()
        {
            //desactivation de la possibilité de cloturer
            Cloturer_dossier.Enabled = false;
            //possibilité d'ouvrir un autre dossier
            Ouvrir_dossier.Enabled = true;
            //disable envoyer reindexation
            Envoyer_reedexation.Enabled = false;
            //possibilité de changer le statue
            Changer_statut.Enabled = false;
        }

        public int lancer_recherche(string reqResult)
        {
            //renitialiser le menue contexte
            renitialiser_menueContext();

            //la recherche composer pour recuperer les dossier
            vider_statistiques();
            resultRecherche.Clear();
            resultRecherche = service.getListeFolder(reqResult);
            //deselection de l'arboresence
            unite_dossier.SelectedNodes.Clear();
            //clear nodes
            this.unite_dossier.Nodes.Clear();
            RadTreeNode result = new RadTreeNode();
            result.Image = Resources.agentsaisie;
            result.ContextMenu = MenueProdTotale;
            int totaleimages = 0;
            int totaledesdossiers = 0;
            foreach (DataRow row in resultRecherche.Rows)
            {
                
                nbr_Images = row["nb_image"].ToString().Trim();
                totaleimages=totaleimages+Convert.ToInt32(nbr_Images);                
                totaledesdossiers++;
                string id_doosier = row["id_dossier"].ToString().Trim();
                string id_status_dossier = row["id_status"].ToString().Trim();
                statistiques_recherche(id_status_dossier);
                if (sansarboresence.Checked == false)
                {
                    row["libelle"] = row["libelle"].ToString().Trim();
                    //recuperation des donnes        
                    ref_dossier = row["name_dossier"].ToString().Trim();
                    name_status_dossier = row["libelle"].ToString().Trim();
                    id_user_indexation = row["user_indexation"].ToString().Trim();
                    id_user_affectation = row["user_affectation"].ToString().Trim();
                    id_user_verification = row["user_verification"].ToString().Trim();
                    id_user_controle = row["user_controle"].ToString().Trim();

                    //creation d'arboresence
                    RadTreeNode TF = new RadTreeNode();
                    TF.Value = id_doosier;
                    TF.Name = ref_dossier;
                    //nomination dudossier en se basant le statue
                    TF.Text = generate_textDossier_using_statue(id_status_dossier);
                    TF.Image = Resources.redFolder;
                    
                    //creation menue en se basant sur statue dossier
                    RadContextMenu rm = generate_ContextMenu(id_status_dossier);
                    if (rm != null)
                    {
                        TF.ContextMenu = rm;
                    }
                    result.Nodes.Add(TF);   
                }
            }
            result.ExpandAll();
            result.Value = result.Nodes.Count;
            result.Name = result.Nodes.Count.ToString();
            result.Text = "Dossiers : " + totaledesdossiers + " Vues : " + totaleimages;
            this.unite_dossier.Nodes.Add(result);
            show_statistiques();
            return result.Nodes.Count;
            //fin recherche
        }
    
        public string generate_textDossier_using_statue(string id_status_dossier)
        {
            string text = "";

            //nomination et couleur dossier en se basant le statue
            if (id_status_dossier == "1" && id_user_affectation != "")
            {
                text = ref_dossier + " (" + nbr_Images + ") ( " + name_status_dossier + " à " + getNameUser(id_user_affectation) + " )";
            }
            else if (id_status_dossier == "3" && id_user_indexation != "")
            {
                text = ref_dossier + " (" + nbr_Images + ") ( " + name_status_dossier + " par " + getNameUser(id_user_affectation) + " )";
            }
            else if ((id_status_dossier == "8") && id_user_verification != "")
            {
                text = ref_dossier + " (" + nbr_Images + ") ( " + name_status_dossier + " par " + getNameUser(id_user_verification) + " )";
            }  
            else if ((id_status_dossier == "6" || id_status_dossier == "7") && id_user_controle != "")
            {
                text = ref_dossier + " (" + nbr_Images + ") ( " + name_status_dossier + " par " + getNameUser(id_user_controle) + " )";
            }            
            else
            {
                text = ref_dossier + " (" + nbr_Images + ") ( " + name_status_dossier + " )";
            }

            return text;
        }

        public RadContextMenu generate_ContextMenu(string idStatue)
        {
            //RadContextMenu menue = new RadContextMenu();

            //if (idStatue == "10" || idStatue == "9")
            //{
            //    menue = MenueDossiers;
            //}
            //else if (idStatue == "3" || idStatue == "6" || idStatue == "7" || idStatue == "8")
            //{
            //    menue = MenueTF;
            //}
            //else
            //{
            //    menue = MenueDossiers;
            //}

            return MenueDossiers;
        }

        public void statistiques_recherche(string statue)
        {
            if (statue == "0")
            {
                nbrNonAffecter++;
            }
            else if (statue == "1")
            {
                nbrNonindexer++;
            }
            else if (statue == "3")
            {
                nbrIndexer++;
            }
            else if (statue == "4")
            {
                nbrInstance++;
            }
            else if (statue == "8")
            {
                nbrVerifiers++;
            }
            else if (statue == "6")
            {
                nbrValides++;
            }
            else if (statue == "7")
            {
                nbrRejeter++;
            }
            else if (statue == "9")
            {
                nbrAlivrer++;
            }                
            else if (statue == "11")
            {
                nbrLivrer++;
            }
            else if (statue == "12")
            {
                nbrErreurscan++;
            }
        }
        
        public void show_statistiques()
        {
            //affichage statistique
            valueNonAff.Text = nbrNonAffecter.ToString();
            labelNonIndexes.Text = nbrNonindexer.ToString();
            labelIndexerValue.Text = nbrIndexer.ToString();
            label1ValidValues.Text = nbrValides.ToString();
            labelVarifierValues.Text = nbrVerifiers.ToString();
            labelRejeterValues.Text = nbrRejeter.ToString();
            labelAlivrerValues.Text = nbrAlivrer.ToString();
            labelLivrerValue.Text = nbrLivrer.ToString();
            LabelEninstanceValue.Text = nbrInstance.ToString();
            labelErreurScan.Text = nbrErreurscan.ToString();
            
        }

        public string generateurRequette()
        {
            Generateur generateur = new Generateur();
            if (inclurelivrables.Checked == false)
            {
                generateur.id_livrable = this.idLivrable.ToString();
            }
            else
            {
                generateur.id_livrable = "0";
            }
            idSeulStatueRecuperer = "0";
                
                // si nous avons selectionner toutes les dates
                if (touteslesdates.IsChecked)
                {
                    reqResult = generateur.recherche();
                }
                else
                {
                    string datein = entreDate.SelectedItem.ToString().Trim();
                    string dateout = etdate.SelectedItem.ToString().Trim();
                    reqResult = generateur.recherche() + generateur.rechercheByDate(datein, dateout);
                   
                }
                //si nous avons selectionner des agent
                int nbrAgent = listeAgentsBD.SelectedItems.Count;
                if (nbrAgent > 0)
                {
                    string idAgentsRecupered = "";
                    int i = 0;
                    foreach (var item in listeAgentsBD.SelectedItems)
                    {
                        {
                            if (i == 0)
                                idAgentsRecupered = item.Value.ToString();
                        }

                        idAgentsRecupered = idAgentsRecupered + "," + item.Value.ToString();
                        i++;
                    }
                    reqResult = reqResult + generateur.rechercheById(idAgentsRecupered);
                }

                //cas nous avons selectionner des statues
                int nbrStatus = listeStatus.SelectedItems.Count;
                if (nbrStatus > 0)
                {
                    string idStatuesRecupered = "";
                    int i = 0;
                    foreach (var item in listeStatus.SelectedItems)
                    {
                        {
                            if (i == 0)
                            {
                                idStatuesRecupered = item.Value.ToString();
                                idSeulStatueRecuperer = idStatuesRecupered;
                            }
                            else
                            {
                                idSeulStatueRecuperer = "0";
                                idStatuesRecupered = idStatuesRecupered + "," + item.Value.ToString();
                            }
                        }

                        
                        
                        i++;
                    }
                    reqResult = reqResult + generateur.rechercheByStatue(idStatuesRecupered);
                }

                //cas nous avons selectionner des tranches
                int nbrTranches = listeTranches.SelectedItems.Count;
                if (nbrTranches > 0)
                {
                    string idTranche = "";
                    int i = 0;
                    foreach (var item in listeTranches.SelectedItems)
                    {
                        {
                            if (i == 0)
                            {
                                idTranche = item.Value.ToString();
                            }
                            else
                            {

                                idTranche = idTranche + "," + item.Value.ToString();
                            }
                        }



                        i++;
                    }
                    reqResult = reqResult + generateur.rechercheByTranche(idTranche);
                }
                else
                {
                    reqResult = reqResult + generateur.rechercheDossiersSansTranches();
                    //MessageBox.Show("Aucune tranche");
                }
                

            return reqResult;
        }

        private void relancer_recherche()
        {
            //renitialisation des composant de l'interface
            composant_interface_clear();            

            //la recherche composer pour recuperer les dossier
            int nbrDossier = lancer_recherche(reqResult);

            //generer le menue racine
            menue_racine(nbrDossier);

        }        

        public void chargerFicheDossier()
        {
            //fiche dossier
            indexFicheDossier.Clear();
            indexFicheDossier = service.getIndexTF(idDossierOuvertActuellement);
            DataRow index = indexFicheDossier.Rows[0];
            txt_nature_orgine.Text = index["nature_origine"].ToString();
            txt_numero_orgine.Text = index["numero_origine"].ToString();
            txt_indice_orgine.Text = index["indice_origine"].ToString();
            txt_indice_speciale_orgine.Text = index["indice_sporigine"].ToString();
            txt_numero_titre.Text = index["numero_titre"].ToString();
            IndiceTitre.Text = index["indice_titre"].ToString();
            txt_indice_spciale_titre.Text = index["indice_sptitre"].ToString();
            //fin fiche dossier            
        }

        public string afficher_indexes_piece(string idVueRecupered)
        {
            String chemin = "";
            string idVue = unite_dossier.SelectedNode.Name.ToString();
            DataRow[] result = listePieces.Select("id_vue = " + idVueRecupered + "");
            foreach (DataRow row in result)
            {

                statuePieceSelectionnerActuellement = row["id_status"].ToString().Trim();
                chemin = row["url"].ToString();

                //partie piece
                NomPiece.Text = row["nom_piece"].ToString().Trim();
                txtnbrpage2.Text = row["nombre_page"].ToString().Trim();
                txtnumpage.Text = row["num_page"].ToString().Trim();

                //partie sous dossier
                txt_numero_sd.Text = row["num_sous_dos"].ToString().Trim();
                Formalite.Text = row["formalite"].ToString().Trim();
                txt_volume_depot.Text = row["volume_depot"].ToString().Trim();
                txt_numero_depot.Text = row["numero_depot"].ToString().Trim();
                string daterecup = row["date_depot"].ToString().Trim();
                string[] words = daterecup.Split('/');
                if (daterecup != "" && words.Length > 0)
                {
                    string jour = words[0];
                    string mois = words[1];
                    string annetotale = words[2];
                    string anne = annetotale.Split(' ')[0];
                    jourDropDownList.Text = jour;
                    moisDropDownList.Text = mois;
                    anneDropDownList.Text = anne;
                }
                else
                {
                    jourDropDownList.Text = "";
                    moisDropDownList.Text = "";
                    anneDropDownList.Text = "";
                }
                if (formaliteARFR.ContainsKey(Formalite.Text))
                {
                    valueAR.Text = formaliteARFR[Formalite.Text];
                }
                else
                {
                    valueAR.Text = "";
                }
            }
            return chemin;
        }

        public void enabled_zones_using_statueDossier_droit(string statueDossier)
        {
            Boolean droitCorrection = service.verifier_le_droit(1,idUtilisateur);
            if ((statueDossier == "3" || statueDossier == "7" || statueDossier == "8" || statueDossier == "10") && droitCorrection)
            {
                //enable droit de corriger pour ce dossier
                corriger.Enabled = true;
                //il peut tjr modifier ces deux champs
                txtnbrpage2.Enabled = true;
                txtnumpage.Enabled = true;

                //affichage de zone dossier selon le nom de la piece
                if (NomPiece.Text == "PAGE DE GARDE DU SOUS DOSSIER")
                {
                    //arreter la modification du nom de la piece
                    NomPiece.Enabled = false;
                    //afficher les champs du sousdossier
                    txt_numero_sd.Enabled = true;
                    Formalite.Enabled = true;
                    txt_volume_depot.Enabled = true;
                    txt_numero_depot.Enabled = true;
                    jourDropDownList.Enabled = true;
                    moisDropDownList.Enabled = true;
                    anneDropDownList.Enabled = true;
                    //masquer les champs du dossier
                    txt_nature_orgine.Enabled = false;
                    txt_numero_orgine.Enabled = false;
                    txt_indice_orgine.Enabled = false;
                    txt_indice_speciale_orgine.Enabled = false;
                    txt_numero_titre.Enabled = false;
                    IndiceTitre.Enabled = false;
                    txt_indice_spciale_titre.Enabled = false;

                }
                else if (NomPiece.Text == "PAGE DE GARDE DU DOSSIER")
                {
                    //arreter la modification du nom de la piece
                    NomPiece.Enabled = false;
                    //masquer les champs du sous dossiers
                    txt_numero_sd.Enabled = false;
                    Formalite.Enabled = false;
                    txt_volume_depot.Enabled = false;
                    txt_numero_depot.Enabled = false;
                    jourDropDownList.Enabled = false;
                    moisDropDownList.Enabled = false;
                    anneDropDownList.Enabled = false;
                    //aficher les champs du dossier
                    txt_nature_orgine.Enabled = true;
                    txt_numero_orgine.Enabled = true;
                    txt_indice_orgine.Enabled = true;
                    txt_indice_speciale_orgine.Enabled = true;
                    txt_numero_titre.Enabled = true;
                    IndiceTitre.Enabled = true;
                    txt_indice_spciale_titre.Enabled = true;
                }
                else
                {
                    //arreter la modification du nom de la piece
                    NomPiece.Enabled = true;                    
                    //masquer les champs du sous dossiers
                    txt_numero_sd.Enabled = false;
                    Formalite.Enabled = false;
                    txt_volume_depot.Enabled = false;
                    txt_numero_depot.Enabled = false;
                    jourDropDownList.Enabled = false;
                    moisDropDownList.Enabled = false;
                    anneDropDownList.Enabled = false;
                    //masquer les champs du dossier
                    txt_nature_orgine.Enabled = false;
                    txt_numero_orgine.Enabled = false;
                    txt_indice_orgine.Enabled = false;
                    txt_indice_speciale_orgine.Enabled = false;
                    txt_numero_titre.Enabled = false;
                    IndiceTitre.Enabled = false;
                    txt_indice_spciale_titre.Enabled = false;
                }

            }
            else
            {
                //disabled droit de corriger pour ce dossier
                corriger.Enabled = false;
                //arreter la modification du nom de la piece
                NomPiece.Enabled = false;
                txtnbrpage2.Enabled = false;
                txtnumpage.Enabled = false;
                //masquer les champs du sous dossiers
                txt_numero_sd.Enabled = false;
                Formalite.Enabled = false;
                txt_volume_depot.Enabled = false;
                txt_numero_depot.Enabled = false;
                jourDropDownList.Enabled = false;
                moisDropDownList.Enabled = false;
                anneDropDownList.Enabled = false;
                //masquer les champs du dossier
                txt_nature_orgine.Enabled = false;
                txt_numero_orgine.Enabled = false;
                txt_indice_orgine.Enabled = false;
                txt_indice_speciale_orgine.Enabled = false;
                txt_numero_titre.Enabled = false;
                IndiceTitre.Enabled = false;
                txt_indice_spciale_titre.Enabled = false;
            }            

        }

        public void enabled_buttons_controle(string statuePiece, string statueDossier)
        {
            Boolean droitControle = service.verifier_le_droit(2, idUtilisateur);
            if ((statueDossier == "3" || statueDossier == "7" || statueDossier == "8" || statueDossier == "10") && droitControle)
            {
                //desactivation du button valider rejeter
                if (statuePiece == "6")
                {
                    statutPiece.Text = "Statut : Validé";
                    statutPiece.ForeColor = Color.Green;
                    btnValider.Enabled = false;
                    btnRejeter.Enabled = true;
                }
                else if (statuePiece == "7")
                {
                    statutPiece.Text = "Statut : Rejetée";
                    statutPiece.ForeColor = Color.Red;
                    btnValider.Enabled = true;
                    btnRejeter.Enabled = false;
                }
                else if (statuePiece == "5")
                {
                    statutPiece.Text = "Statut : Suprimer";
                    statutPiece.ForeColor = Color.Red;
                    btnValider.Enabled = true;
                    btnRejeter.Enabled = true;
                }
                else if (statuePiece == "10")
                {
                    statutPiece.Text = "Statut : Corriger";
                    statutPiece.ForeColor = Color.Blue;
                    btnValider.Enabled = true;
                    btnRejeter.Enabled = true;
                }
                else
                {
                    statutPiece.Text = "Statut : Non controller";
                    statutPiece.ForeColor = Color.Red;
                    btnValider.Enabled = true;
                    btnRejeter.Enabled = true;
                }
            }
            else
            {
                //filtrage statue piece valider rejeter
                if (statuePiece == "6")
                {
                    statutPiece.Text = "Statut : Validé";
                    statutPiece.ForeColor = Color.Green;
                }
                else if (statuePiece == "7")
                {
                    statutPiece.Text = "Statut : Rejetée";

                    statutPiece.ForeColor = Color.Red;
                }
                else if (statuePiece == "10")
                {
                    statutPiece.Text = "Statut : Corriger";

                    statutPiece.ForeColor = Color.Blue;
                }
                else
                {
                    statutPiece.Text = "Statut : Non controller";
                    statutPiece.ForeColor = Color.Red;
                }
                btnValider.Enabled = false;
                btnRejeter.Enabled = false;
            }
        }

        //selectionnement des pieces 
        private void unite_dossier_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            modification = false;
            // si on selectionne la piece
            if (unite_dossier.SelectedNode.Level == 2)
            {
                editer.Enabled = true;

                if (namePiecePrecedementSelectionner == "PAGE DE GARDE DU DOSSIER")
                {
                    chargerFicheDossier();
                }

                namePiecePrecedementSelectionner = unite_dossier.SelectedNode.Text;

                //fiche piece
                FichePieceErreur = false;
                nbrPagesFichePieceErreur = false;
                numPagesFichePieceErreur = false;
                //fiche sous dossier
                FicheSousDossierErreur = false;
                numSousDossierErreur = false;
                volumeDepotErreur = false;
                numeroDepotErreur = false;
                //fiche dossier
                FicheDossierErreur = false;
                numOrigineErreur = false;
                numTitreErreur = false;
                this.dispose_providers();
                statutPiece.Text = "Statut :";
                //afficher les butttons de controlle d'image
                toolStrip1.Visible = true;                
                //--------affichage des indexes--------------------------------------------                      
                string idVue = unite_dossier.SelectedNode.Name.ToString();
                String chemin = afficher_indexes_piece(idVue);                           
                //--------- enabled buttons zone textes des pieces
                enabled_zones_using_statueDossier_droit(statueDossierOuvertActuellement);
                //---------------- enabled buttons de correction               
                statuePieceSelectionnerActuellement=  listeIdPieceWithstatue[idVue];
                enabled_buttons_controle(statuePieceSelectionnerActuellement, statueDossierOuvertActuellement);
                if (chemin == "")
                {
                    chemin = "C:/Users/Mustapha/Desktop/images/non_disponible.jpg";
                }                
                this.pb.Width = 600;
                this.pb.Height = 450;
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                image_stream = service.pathToStream(@chemin);
                FileStream fileStream = image_stream as FileStream;
                if (fileStream != null)
                {
                    string extention = Path.GetExtension(fileStream.Name);                    
                    string taille = service.FileSizeFormat(fileStream.Length);
                    typeLabel.Text = "Type : " + extention;
                    tailleLabel.Text = "Taille : " + taille;
                }
                else
                {
                    typeLabel.Text = "Type :";
                    tailleLabel.Text = "Taille :";
                }
                image = (Bitmap)Image.FromStream(image_stream, true, false);
                defImgHeight = image.Height;
                defImgWidth = image.Width;
                this.pb.Image = image;
            }
            else
            {
                editer.Enabled = false;
                statutPiece.Text = "Statut :";
                statutPiece.ForeColor = Color.Black;
                typeLabel.Text = "Type :";
                tailleLabel.Text = "Taille :";
                //masquer l'image
                this.pb.Image = null;
                //masquer les butttons de controlle d'image
                toolStrip1.Visible = false;
                //master button de controle
                btnValider.Enabled = false;
                btnRejeter.Enabled = false;
                corriger.Enabled = false;
                //master button de controle
                //vider les indexes
                viderIndexes();
                //fin vider les indexes
                //masquer la saisie 
                disabledZoneSaisie();

                int nbrselection = unite_dossier.SelectedNodes.Count;
             
            }
        }

        //vider les indexes des fiches du formulaire
        private void viderIndexes()
        {
            //partie piece
            NomPiece.Text = "";
            txtnbrpage2.Text = "";
            txtnumpage.Text = "";
            //partie sous dossier
            txt_numero_sd.Text = "";
            Formalite.Text = "";
            txt_volume_depot.Text = "";
            txt_numero_depot.Text = "";
            jourDropDownList.Text = "";
            moisDropDownList.Text = "";
            anneDropDownList.Text = "";
        }
        private void viderTotaliterIndexes()
        {
            viderIndexes();
            //partie dossier
            txt_nature_orgine.Text = "";
            txt_numero_orgine.Text = "";
            txt_indice_orgine.Text = "";
            txt_indice_speciale_orgine.Text = "";
            txt_numero_titre.Text = "";
            IndiceTitre.Text = "";
            txt_indice_spciale_titre.Text = "";
        }

        //droit de saisie
        private void enabledZoneSaisie()
        {
            //partie piece
            NomPiece.Enabled = true;
            txtnbrpage2.Enabled = true;
            txtnumpage.Enabled = true;
            //partie sous dossier
            txt_numero_sd.Enabled = true;
            Formalite.Enabled = true;
            txt_volume_depot.Enabled = true;
            txt_numero_depot.Enabled = true;
            //partie dossier
            txt_nature_orgine.Enabled = true;
            txt_numero_orgine.Enabled = true;
            txt_indice_orgine.Enabled = true;
            txt_indice_speciale_orgine.Enabled = true;
            txt_numero_titre.Enabled = true;
            IndiceTitre.Enabled = true;
            txt_indice_spciale_titre.Enabled = true;
        }
        private void disabledZoneSaisie()
        {
            //partie piece
            NomPiece.Enabled = false;
            txtnbrpage2.Enabled = false;
            txtnumpage.Enabled = false;
            //partie sous dossier
            txt_numero_sd.Enabled = false;
            Formalite.Enabled = false;
            txt_volume_depot.Enabled = false;
            txt_numero_depot.Enabled = false;
            //partie dossier
            txt_nature_orgine.Enabled = false;
            txt_numero_orgine.Enabled = false;
            txt_indice_orgine.Enabled = false;
            txt_indice_speciale_orgine.Enabled = false;
            txt_numero_titre.Enabled = false;
            IndiceTitre.Enabled = false;
            txt_indice_spciale_titre.Enabled = false;
        }

        //buttons de la capture images
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Normal;
                zoom = zoom + 0.2;
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(this.pb.Width * zoom), Convert.ToInt32(this.pb.Width * zoom * defImgHeight / defImgWidth));
                this.pb.SizeMode = PictureBoxSizeMode.AutoSize;
                this.pb.Image = bmp;
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(this.pb.Width), Convert.ToInt32(this.pb.Width * defImgHeight / defImgWidth));
                this.pb.Image = bmp;
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Normal;
                if (zoom > 0.3) //jangan sampai zoom - 0.2 hasilnya nol
                {
                    zoom = zoom - 0.2;
                    Bitmap bmp = new Bitmap(image, Convert.ToInt32(this.pb.Width * zoom), Convert.ToInt32(this.pb.Width * zoom * defImgHeight / defImgWidth));
                    this.pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    this.pb.Image = bmp;
                }
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                this.pb.Image = image;
                this.pb.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                this.pb.Image = image;
                this.pb.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        //buttons valider et rejeter
        private void btnValider_Click(object sender, EventArgs e)
        {
            
            if (unite_dossier.SelectedNode.NextNode!=null)
            {
                string idVue = unite_dossier.SelectedNode.Value.ToString();
                //tester si la vue est déja rejeter
                if (listeIdPieceWithstatue[idVue] == "7")
                {
                    //decrementer le nbr des vues rejeter
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col7", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                else if (listeIdPieceWithstatue[idVue] == "10")
                //tester si la vue est déja corriger                        
                {
                    //decrementer le nbr des vues corriger
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col8", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                if (listeIdPieceWithstatue[idVue] == "5")
                {
                    RadTreeNode treeUnite = unite_dossier.SelectedNode;
                    listeIdPieceWithstatue[idVue] = "6";
                    unite_dossier.SelectedNode = treeUnite.NextNode;
                }
                else
                {
                    service.validerVue(idUtilisateur, idVue);
                    listeIdPieceWithstatue[idVue] = "6";
                    RadTreeNode treeUnite = unite_dossier.SelectedNode;
                    treeUnite.Image = Resources.document_ok;
                    unite_dossier.SelectedNode = treeUnite.NextNode;
                }
            }
            else
            {
                string idVue = unite_dossier.SelectedNode.Value.ToString();
                if (listeIdPieceWithstatue[idVue] == "7")
                {
                    //decrementer le nbr des vues rejeter
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col7", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                else if (listeIdPieceWithstatue[idVue] == "10")
                //tester si la vue est déja corriger                        
                {
                    //decrementer le nbr des vues corriger
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col8", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }


                if (listeIdPieceWithstatue[idVue] == "5")
                {
                    RadTreeNode treeUnite = unite_dossier.SelectedNode;
                    listeIdPieceWithstatue[idVue] = "6";
                    unite_dossier.SelectedNode = treeUnite.Parent;
                }
                else
                {
                    service.validerVue(idUtilisateur, idVue);
                    listeIdPieceWithstatue[idVue] = "6";
                    RadTreeNode treeUnite = unite_dossier.SelectedNode;
                    treeUnite.Image = Resources.document_ok;
                    unite_dossier.SelectedNode = treeUnite.Parent;
                }
            }
            
            //insertion etat vue valider
            //prod en nbr des vues valides
            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col6", "1");
            //prod en br des vues controller
            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "1");
        }
        private void btnRejeter_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.NextNode != null)
            {
                string idVue = unite_dossier.SelectedNode.Value.ToString();
                //tester si la vue est déja valider
                if (listeIdPieceWithstatue[idVue] == "6")
                {
                    //decrementer le nbr des vues rejeter
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col6", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                else if (listeIdPieceWithstatue[idVue] == "10")
                //tester si la vue est déja corriger                        
                {
                    //decrementer le nbr des vues corriger
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col8", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                service.rejeterVue(idUtilisateur,idVue);
                listeIdPieceWithstatue[idVue] = "7";
                RadTreeNode treeUnite = unite_dossier.SelectedNode;
                treeUnite.Image = Resources.document_rejet;
                unite_dossier.SelectedNode = treeUnite.NextNode;
            }
            else
            {
                string idVue = unite_dossier.SelectedNode.Value.ToString();
                //tester si la vue est déja valider
                if (listeIdPieceWithstatue[idVue] == "6")
                {
                    //decrementer le nbr des vues rejeter
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col6", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                else if (listeIdPieceWithstatue[idVue] == "10")
                //tester si la vue est déja corriger                        
                {
                    //decrementer le nbr des vues corriger
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col8", "-1");
                    //decrementer prod en br des vues controller
                    service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                }
                service.rejeterVue(idUtilisateur,idVue);
                listeIdPieceWithstatue[idVue] = "7";
                RadTreeNode treeUnite = unite_dossier.SelectedNode;
                treeUnite.Image = Resources.document_rejet;
                unite_dossier.SelectedNode = treeUnite.Parent;
            }

            //insertion etat vue rejeter
            //prod en nbr des vues rejeter
            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col7", "1");
            //prod en br des vues controller
            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "1");
            
        }        
        
        //click droit sur un dossier pour le corriger
        private void corriger_Click(object sender, EventArgs e)
        {
            if (FichePieceErreur || FicheDossierErreur || FicheSousDossierErreur)
            {
                MessageBox.Show("Attention il y'a des Erreurs de saisie");
            }
            else
            {
                RadTreeNode pieceRecupered = unite_dossier.SelectedNode;
                pieceRecupered.Image = Resources.document_ok;
                
                string idVue = pieceRecupered.Name.ToString();
                string pieceName = NomPiece.Text;
                if (service.updateFichePieces(idVue.ToString(), pieceName, txtnbrpage2.Text.ToString(), txtnumpage.Text.ToString()))
                {
                    DataRow[] pieceRow = listePieces.Select("id_vue='" + idVue.ToString() + "'");
                    pieceRow[0]["nom_piece"] = pieceName;                    
                    pieceRow[0]["nombre_page"] = txtnbrpage2.Text.ToString();
                    pieceRow[0]["num_page"] = txtnumpage.Text.ToString();
                    unite_dossier.GetNodeByName(idVue).Text = pieceName;
                    if (pieceName == "PAGE DE GARDE DU DOSSIER")
                    {
                        string natureOrigine = txt_nature_orgine.Text, numOrigine = txt_numero_orgine.Text, indiceOrigine = txt_indice_orgine.Text, indiceSpOrigine = txt_indice_speciale_orgine.Text, numTitre = txt_numero_titre.Text, indiceTitre = IndiceTitre.Text, indiceSpTitre = txt_indice_spciale_titre.Text;
                        if (service.updateFicheDossier(idDossierOuvertActuellement.ToString(), natureOrigine, numOrigine, indiceOrigine, indiceSpOrigine, numTitre, indiceTitre, indiceSpTitre))
                        {
                            chargerFicheDossier();
                        }
                        else
                        {
                            MessageBox.Show("Erreur BD , Merci de contacter votre superviseur");
                        }
                    }
                    else if(pieceName == "PAGE DE GARDE DU SOUS DOSSIER")
                    {
                        //les indexes enregistrer au niveau du data set
                        string numSousDossierSaved = pieceRow[0]["num_sous_dos"].ToString();
                        string formaliteSaved = pieceRow[0]["formalite"].ToString();
                        string volumeDepotSaved = pieceRow[0]["volume_depot"].ToString();
                        string numDepotSaved = pieceRow[0]["numero_depot"].ToString();
                        string dateDepotSaved = pieceRow[0]["date_depot"].ToString();
                        //les indexes saisie au niveau de l'interface
                        string numSousDossier = txt_numero_sd.Text, formalite = Formalite.Text, volumeDepot = txt_volume_depot.Text, numDepot = txt_numero_depot.Text;
                        string dateDepot = jourDropDownList.Text + "/" + moisDropDownList.Text + "/" + anneDropDownList.Text;
                        //recherche sur dataset pour récuperer la liste des pieces qui font partie du meme dossier sous de la piece selectionner
                        DataRow[] pieceWithSameSD = listePieces.Select("num_sous_dos='" + numSousDossierSaved + "'" + "AND formalite='" + formaliteSaved + "'" + "AND volume_depot='" + volumeDepotSaved + "'" + "AND numero_depot='" + numDepotSaved + "'" + "AND date_depot='" + dateDepotSaved + "'");
                        foreach (DataRow piece in pieceWithSameSD)
                        {                            
                            string idVueInSD = piece["id_vue"].ToString();
                            if (service.updateFicheSousDossier(idVueInSD, numSousDossier, formalite, volumeDepot, numDepot, dateDepot))
                            {
                                piece["num_sous_dos"] = numSousDossier;
                                piece["formalite"] = formalite;
                                piece["volume_depot"] = volumeDepot;
                                piece["numero_depot"] = numDepot;
                                piece["date_depot"] = dateDepot;
                            }
                            else
                            {
                                MessageBox.Show("Erreur BD , Impossible de changer la fiche dossier , Merci de contacter votre superviseur");
                            }
                        }
                        
                        
                    }

                    if (modification)
                    {
                        //les indexes saisie au niveau de l'interface
                        string numSousDossier = txt_numero_sd.Text, formalite = Formalite.Text, volumeDepot = txt_volume_depot.Text, numDepot = txt_numero_depot.Text;
                        string dateDepot = jourDropDownList.Text + "/" + moisDropDownList.Text + "/" + anneDropDownList.Text;

                        if (service.updateFicheSousDossier(idVue, numSousDossier, formalite, volumeDepot, numDepot, dateDepot))
                        {
                            pieceRow[0]["num_sous_dos"] = numSousDossier;
                            pieceRow[0]["formalite"] = formalite;
                            pieceRow[0]["volume_depot"] = volumeDepot;
                            pieceRow[0]["numero_depot"] = numDepot;
                            pieceRow[0]["date_depot"] = dateDepot;
                        }

                    }
                    //changement statue piece vers corriger ou valider selon le profil
                    Boolean droitdecontrolle = service.verifier_le_droit(2, idUtilisateur);
                    if (droitdecontrolle)
                    {
                        //si la vue est déja rejeter
                        if (listeIdPieceWithstatue[idVue] == "7")
                        {
                            //decrementer le nbr des vues rejeter
                            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col7", "-1");
                            //decrementer prod en br des vues controller
                            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                        }
                        else if (listeIdPieceWithstatue[idVue] == "6")
                        //tester si la vue est déja valider                        
                        {
                            //decrementer le nbr des vues rejeter
                            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col6", "-1");
                            //decrementer prod en br des vues controller
                            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                        }
                        else if (listeIdPieceWithstatue[idVue] == "10")
                        //tester si la vue est déja corriger                        
                        {
                            //decrementer le nbr des vues corriger
                            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col8", "-1");
                            //decrementer prod en br des vues controller
                            service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "-1");
                        }
                        //statue valider
                        //service.validerVue(idUtilisateur, idVue);
                        service.corrigerVue(idUtilisateur, idVue);
                        listeIdPieceWithstatue[idVue] = "10";
                        ////prod en nbr des vues valides
                        //service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col6", "1");
                        //prod en nbr des vues corriger
                        service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col8", "1");
                        //prod en br des vues controller
                        service.update_etats(idLivrable, idUtilisateur, service.getdate(), "col9", "1");
                        pieceRecupered.Image = Resources.document_corriger;
                    }
                    else
                    {    
                        //statue corriger
                        service.corrigerVue(idVue);
                        listeIdPieceWithstatue[idVue] = "10";
                    }

                }
                else
                {
                    MessageBox.Show("Erreur BD , Merci de contacter votre superviseur");
                }

                

                if (unite_dossier.SelectedNode.NextNode != null)
                {
                    
                    unite_dossier.SelectedNode = pieceRecupered.NextNode;
                }
                else
                {
                    unite_dossier.SelectedNode = pieceRecupered.Parent;
                }
            }
        }

        //click droit sur un dossier pour le controller
        private void Ouvrir_Click(object sender, EventArgs e)
        {
            action = "controle";
            //desactiver la possibilité d'ouvrire un autre dossier
            Ouvrir.Enabled = false;
            //desactiver la possibilité d'ouvrire un autre dossier            

            //activer les option de rejet et de validation
            validerDossier.Enabled = true;
            rejeterDossier.Enabled = true;
            //activer les option de rejet et de validation

            //desactivation de du choix de verification
            verificationDossier.Enabled = false;
            //desactivation de du choix de verification

            //afficher les option de controle image
            toolStrip1.Visible = true;
            this.pb.Image = null;
            //fin afficher les option de controle image
            RadTreeNode TF = unite_dossier.SelectedNode;
            listeIdPieceWithstatue.Clear();
            listePieces.Clear();
            listePieces = service.getListePieceTF(TF.Value.ToString());
            idDossierOuvertActuellement = TF.Value.ToString();
            namedossierouvertactuellement = TF.Name.ToString();
            foreach (DataRow row in listePieces.Rows)
            {
                RadTreeNode piece = new RadTreeNode();
                piece.Value = row["id_vue"];
                piece.Name = row["id_vue"].ToString();
                piece.Text = row["nom_piece"].ToString();
                if (row["nom_piece"].ToString() == "PAGE DE GARDE DU DOSSIER" && row["id_status"].ToString() != "7")
                {
                    piece.Image = Resources.redFolder;
                }
                else if (row["nom_piece"].ToString() == "PAGE DE GARDE DU SOUS DOSSIER" && row["id_status"].ToString() != "7")
                {
                    piece.Image = Resources.jauneDossier;
                }
                else
                {
                    if (row["id_status"].ToString() == "6")
                    {
                        piece.Image = Resources.document_ok;
                    }
                    else if (row["id_status"].ToString() == "7")
                    {
                        piece.Image = Resources.document_rejet;
                    }
                    else
                    {
                        piece.Image = Resources.document;
                    }
                }

                piece.ContextMenu = MenuePiece;
                TF.Nodes.Add(piece);
                listeIdPieceWithstatue.Add(row["id_vue"].ToString(), row["id_status"].ToString());
            }
            chargerFicheDossier();

        }

        //diposer tout les providers aprés selectionnement d'une nouvelle piece
        private void dispose_providers()
        {
            NbrPagesProvider.Dispose();
            NumPagesProvider.Dispose();
            NumOrigineProvider.Dispose();
            numTitreProvider.Dispose();
            numSousDossierProvider.Dispose();
            volumeDepotProvider.Dispose();
            numDepotProvider.Dispose();
        }

        //leave au niveau des champs qui ont un provider
        private void txt_numero_orgine_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_numero_orgine.Text))
            {
                    NumOrigineProvider.Icon = Resources.error;
                    NumOrigineProvider.SetError(txt_numero_orgine, "Erreur Merci de saisie un chiffre");
                    numOrigineErreur = true;
                    FicheDossierErreur = true;
                    
            }
            else
            {
                int n;
                    bool isNumeric = int.TryParse(txt_numero_orgine.Text, out n);
                    if (isNumeric)
                    {
                        NumOrigineProvider.Icon = Resources.check;
                        NumOrigineProvider.SetError(txt_numero_orgine, "OK");
                        numOrigineErreur = false;
                        if (!numOrigineErreur && !numTitreErreur)
                        {
                            FicheDossierErreur = false;
                        }
                        else
                        {
                            FicheDossierErreur = true;
                        }
                    }
                    else
                    {
                        NumOrigineProvider.Icon = Resources.error;
                        NumOrigineProvider.SetError(txt_numero_orgine, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                        numOrigineErreur = true;
                        FicheDossierErreur = true;
                    }
            }
        }
        private void txt_numero_titre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_numero_titre.Text))
            {
                numTitreProvider.Icon = Resources.error;
                numTitreProvider.SetError(txt_numero_titre, "Erreur Merci de saisie un chiffre");
                numTitreErreur = true;
                FicheDossierErreur = true;

            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(txt_numero_titre.Text, out n);
                if (isNumeric)
                {
                    numTitreProvider.Icon = Resources.check;
                    numTitreProvider.SetError(txt_numero_titre, "OK");
                    numTitreErreur = false;
                    if (!numOrigineErreur && !numTitreErreur)
                    {
                        FicheDossierErreur = false;
                    }
                    else
                    {
                        FicheDossierErreur = true;
                    }
                }
                else
                {
                    numTitreProvider.Icon = Resources.error;
                    numTitreProvider.SetError(txt_numero_titre, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                    numTitreErreur = true;
                    FicheDossierErreur = true;
                }
            }
        }

        private void txt_numero_sd_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_numero_sd.Text))
            {
                numSousDossierProvider.Icon = Resources.error;
                numSousDossierProvider.SetError(txt_numero_sd, "Erreur Merci de saisie un chiffre");
                numSousDossierErreur = true;
                FicheSousDossierErreur = true;                
            }
            else
            {
                    int n;
                    bool isNumeric = int.TryParse(txt_numero_sd.Text, out n);
                    if (isNumeric)
                    {
                        numSousDossierProvider.Icon = Resources.check;
                        numSousDossierProvider.SetError(txt_numero_sd, "OK");
                        numSousDossierErreur = false;
                        if (!numSousDossierErreur && !volumeDepotErreur && !numeroDepotErreur)
                        {
                            FicheSousDossierErreur = false;
                        }
                        else
                        {
                            FicheSousDossierErreur = true;
                        }
                    }
                    else
                    {
                        numSousDossierProvider.Icon = Resources.error;
                        numSousDossierProvider.SetError(txt_numero_sd, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                        numSousDossierErreur = true;
                        FicheSousDossierErreur = true;
                    }
            }
        }

        private void txt_volume_depot_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_volume_depot.Text))
            {
                volumeDepotProvider.Icon = Resources.error;
                volumeDepotProvider.SetError(txt_volume_depot, "Erreur Merci de saisie un chiffre");
                volumeDepotErreur = true;
                FicheSousDossierErreur = true; 
            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(txt_volume_depot.Text, out n);
                    if (isNumeric)
                    {
                        volumeDepotProvider.Icon = Resources.check;
                        volumeDepotProvider.SetError(txt_volume_depot, "OK");
                        volumeDepotErreur = false;
                        if (!numSousDossierErreur && !volumeDepotErreur && !numeroDepotErreur)
                        {
                            FicheSousDossierErreur = false;
                        }
                        else
                        {
                            FicheSousDossierErreur = true;
                        }
                    }
                    else
                    {
                        volumeDepotProvider.Icon = Resources.error;
                        volumeDepotProvider.SetError(txt_volume_depot, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                        volumeDepotErreur = true;
                        FicheSousDossierErreur = true;
                    }
            }
        }
        
        private void txt_numero_depot_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_numero_depot.Text))
            {
                numDepotProvider.Icon = Resources.error;
                numDepotProvider.SetError(txt_numero_depot, "Erreur Merci de saisie un chiffre");
                numeroDepotErreur = true;
                FicheSousDossierErreur = true;
            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(txt_numero_depot.Text, out n);
                if (isNumeric)
                {
                    numDepotProvider.Icon = Resources.check;
                    numDepotProvider.SetError(txt_numero_depot, "OK");
                    numeroDepotErreur = false;
                    if (!numSousDossierErreur && !volumeDepotErreur && !numeroDepotErreur)
                    {
                        FicheSousDossierErreur = false;
                    }
                    else
                    {
                        FicheSousDossierErreur = true;
                    }
                }
                else
                {
                    numDepotProvider.Icon = Resources.error;
                    numDepotProvider.SetError(txt_numero_depot, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                    numeroDepotErreur = true;
                    FicheSousDossierErreur = true;
                }
            }
        }
        
        private void txtnbrpage2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnbrpage2.Text))
            {
                NbrPagesProvider.Icon = Resources.error;
                NbrPagesProvider.SetError(txtnbrpage2, "FichePieceErreur Merci de saisie un chiffre");
                nbrPagesFichePieceErreur = true;
                FichePieceErreur = true;
            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(txtnbrpage2.Text, out n);
                if (isNumeric)
                {
                    NbrPagesProvider.Icon = Resources.check;
                    NbrPagesProvider.SetError(txtnbrpage2, "OK");
                    nbrPagesFichePieceErreur = false;
                    if (!nbrPagesFichePieceErreur && !numPagesFichePieceErreur)
                    {
                        FichePieceErreur = false;
                    }
                    else
                    {
                        FichePieceErreur = true;
                    }
                }
                else
                {
                    NbrPagesProvider.Icon = Resources.error;
                    NbrPagesProvider.SetError(txtnbrpage2, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                    nbrPagesFichePieceErreur = true;
                    FichePieceErreur = true;
                }
            }
        }
        
        private void txtnumpage_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnumpage.Text))
            {
                NumPagesProvider.Icon = Resources.error;
                NumPagesProvider.SetError(txtnumpage, "Erreur Merci de saisie un chiffre");
                numPagesFichePieceErreur = true;
                FichePieceErreur = true;
            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(txtnumpage.Text, out n);
                if (isNumeric)
                {
                    NumPagesProvider.Icon = Resources.check;
                    NumPagesProvider.SetError(txtnumpage, "OK");
                    numPagesFichePieceErreur = false;
                    if (!nbrPagesFichePieceErreur && !numPagesFichePieceErreur)
                    {
                        FichePieceErreur = false;
                    }
                    else
                    {
                        FichePieceErreur = true;
                    }

                }
                else
                {
                    NumPagesProvider.Icon = Resources.error;
                    NumPagesProvider.SetError(txtnumpage, "Merci de s'aisir un chiffre et pas une/des lettre(s)");
                    numPagesFichePieceErreur = true;
                    FichePieceErreur = true;
                }
            }

        }

        private void ChLivrable_Click(object sender, EventArgs e)
        {
             


        }

        private void EntreDeuxDates_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (EntreDeuxDates.IsChecked)
            {
                entreDate.Enabled = true;
                etdate.Enabled = true;
            }
            else
            {
                entreDate.Enabled = false;
                etdate.Enabled = false;
            }
        }

        public void clearValuesTextes()
        {
            valueNonAff.Text = "";
            labelNonIndexes.Text = "";
            labelIndexerValue.Text = "";
            label1ValidValues.Text = "";
            labelVarifierValues.Text = "";
            labelRejeterValues.Text = "";
            labelAlivrerValues.Text = "";
            labelLivrerValue.Text = "";
            LabelEninstanceValue.Text = "";
            labelErreurScan.Text = "";
        }

        public void vider_statistiques()
        {
            nbrNonAffecter = 0;
            nbrNonindexer = 0;
            nbrIndexer = 0;
            nbrVerifiers = 0;
            nbrValides = 0;
            nbrRejeter = 0;
            nbrAlivrer = 0;
            nbrLivrer = 0;
            nbrInstance = 0;
            nbrErreurscan = 0;

        }
                
        private void allAgents_ToggleStateChanged(object sender, StateChangedEventArgs args)
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

        private void allStatut_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (allStatut.Checked == true)
            {
                foreach (var statut in listeStatus.Items)
                {
                    statut.Selected = true;
                }
            }
            else
            {
                foreach (var statut in listeStatus.Items)
                {
                    statut.Selected = false;
                }
            }
        }

        public void generatrapportglobal()
        {
            this.Hide();
            ViewReport view = new ViewReport();
            view.nom_livrable = nomLivrable;
            view.id_livrable = idLivrable.ToString();
            view.ShowDialog();
        }               

        private void detailExcel_Click(object sender, EventArgs e)
        {
            detailsRequette details = new detailsRequette(resultRecherche);            
            details.ShowDialog();
        }

        private void menue2ChangerLivrable_Click(object sender, EventArgs e)
        {
            this.Hide();
            Livrable livrable = new Livrable();
            livrable.idUtilisateur = idUtilisateur;
            livrable.idbase = this.idbase;
            livrable.Show();
        }

        private void menue2SeDeconecter_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authentification aut = new Authentification();
            aut.Show();
            
        }

        private void menue2Assistant_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En cours");
        }

        private void menue2SQL_Click(object sender, EventArgs e)
        {
            Generateur generateur = new Generateur();
            if (inclurelivrables.Checked == false)
            {
                generateur.id_livrable = this.idLivrable.ToString();
            }
            else
            {
                generateur.id_livrable = "0";
            }
            
            reqResult = generateur.recherche();


            SQL sql = new SQL(reqResult);

            var result = sql.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.reqResult = sql.requetteGenerer;
                relancer_recherche();
            }
        }

        //operation affecter 
        private void menue2Affecter_Click(object sender, EventArgs e)
        {

            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {

                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");
                        string statueDOssier = dossierRow[0]["id_status"].ToString();
                        if (statueDOssier == "0")
                        {
                            listeIdDossiers.Add(idDossier);
                        }
                        else
                        {
                            listeIdDossiers.Clear();
                            MessageBox.Show("Merci de selectionner que des dossiers non afféctés ");
                            return;
                        }
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers non afféctés ");
                        return;
                    }

                }
                if (listeIdDossiers.Count > 0)
                {
                    SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                    selectAgent.affectation(listeIdDossiers);
                    selectAgent.ShowDialog();
                    //desselectionnement des dossier      
                    unite_dossier.SelectedNodes.Clear();
                    //relance de la recherche
                    relancer_recherche();
                }
            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
            }



        }

        //operation désaffecté
        private void menue2Dessafecté_Click(object sender, EventArgs e)
        {

            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {

                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");
                        string statueDOssier = dossierRow[0]["id_status"].ToString();
                        if (statueDOssier == "1" || statueDOssier == "4")
                        {
                            listeIdDossiers.Add(idDossier);
                        }
                        else
                        {
                            listeIdDossiers.Clear();
                            MessageBox.Show("Merci de selectionner que des dossiers afféctés ");
                            return;
                        }
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers afféctés ");
                        return;
                    }

                }
                if (listeIdDossiers.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Voulez vous affecter les dossiers à un autre agent ?", "Désaffectation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                        selectAgent.id_user_operation = idUtilisateur;
                        selectAgent.reaffectation(listeIdDossiers);
                        selectAgent.ShowDialog();
                        relancer_recherche();

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                        selectAgent.id_user_operation = idUtilisateur;
                        if (selectAgent.desaffectation(listeIdDossiers))
                        {
                            MessageBox.Show("Desaffectation reussite");
                        }
                        else
                        {
                            MessageBox.Show("Erreur Desaffectation");
                        }
                    }
                    //desselectionnement des dossier      
                    unite_dossier.SelectedNodes.Clear();
                    //relance de la recherche
                    relancer_recherche();
                }
            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
            }
        }

        private void sendreindexation_Click(object sender, EventArgs e)
        {
            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");
                        string statueDOssier = dossierRow[0]["id_status"].ToString();
                        if (statueDOssier == "7")
                        {
                            listeIdDossiers.Add(idDossier);
                        }
                        else
                        {
                            listeIdDossiers.Clear();
                            MessageBox.Show("Merci de selectionner que des dossiers rejetés ");
                            return;
                        }
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers rejetés ");
                        return;
                    }
                }

                if (listeIdDossiers.Count > 0)
                {
                    //SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                    //selectAgent.id_user_operation = idUtilisateur;
                    //selectAgent.affectation(listeIdDossiers);
                    //selectAgent.ShowDialog();
                    ////desselectionnement des dossier      
                    //unite_dossier.SelectedNodes.Clear();
                    ////relance de la recherche
                    //relancer_recherche();
                    MessageBox.Show(listeIdDossiers.Count.ToString());
                }

            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
            }
        }


        //operation affectation
        private void menue2Affecter_Click_1(object sender, EventArgs e)
        {
            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");
                        string statueDOssier = dossierRow[0]["id_status"].ToString();
                        if (statueDOssier == "0" || statueDOssier == "12")
                        {
                            listeIdDossiers.Add(idDossier);
                        }
                        else
                        {
                            listeIdDossiers.Clear();
                            MessageBox.Show("Merci de selectionner que des dossiers non afféctés ou les dossiers en erreur de scan");
                            return;
                        }
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers non afféctés ou les dossiers en erreur de scan");
                        return;
                    }

                }
                if (listeIdDossiers.Count > 0)
                {
                    SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                    selectAgent.id_user_operation = idUtilisateur;
                    selectAgent.affectation(listeIdDossiers);
                    selectAgent.ShowDialog();
                    //desselectionnement des dossier      
                    unite_dossier.SelectedNodes.Clear();
                    //relance de la recherche
                    relancer_recherche();
                }
            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
            }

        }

        //button gestion des equipes
        private void gestion_equipes_Click(object sender, EventArgs e)
        {
            gestionEquipes gestion_equipe = new gestionEquipes();
            gestion_equipe.ShowDialog();

        }

        private void menueGestionUtilisateurs_Click(object sender, EventArgs e)
        {
            choixGroupeUtilisateurs utilistauers = new choixGroupeUtilisateurs(idUtilisateur);
            utilistauers.ShowDialog();
        }

        private void entreDate_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            DataTable dtDatesEt = service.getProd_Dates_Sup(entreDate.Text.ToString().Trim());
            etdate.DataSource = dtDatesEt;
            etdate.DisplayMember = dtDatesEt.Columns[0].ColumnName;            

        }

        //zoom in
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Normal;
                zoom = zoom + 0.2;
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(this.pb.Width * zoom), Convert.ToInt32(this.pb.Width * zoom * defImgHeight / defImgWidth));
                this.pb.SizeMode = PictureBoxSizeMode.AutoSize;
                this.pb.Image = bmp;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(this.pb.Width), Convert.ToInt32(this.pb.Width * defImgHeight / defImgWidth));
                this.pb.Image = bmp;
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Normal;
                if (zoom > 0.3) //jangan sampai zoom - 0.2 hasilnya nol
                {
                    zoom = zoom - 0.2;
                    Bitmap bmp = new Bitmap(image, Convert.ToInt32(this.pb.Width * zoom), Convert.ToInt32(this.pb.Width * zoom * defImgHeight / defImgWidth));
                    this.pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    this.pb.Image = bmp;
                }
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                this.pb.Image = image;
                this.pb.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                this.pb.Image = image;
                this.pb.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        //menue formalités
        private void radMenuItem4_Click(object sender, EventArgs e)
        {
            formalite form = new formalite();
            form.idutilisateur=idUtilisateur;
            form.ShowDialog();
        }

        private void menueListePiece_Click(object sender, EventArgs e)
        {
            gestion_names_pieces namesPieces = new gestion_names_pieces();
            namesPieces.idutilisateur = idUtilisateur;
            namesPieces.ShowDialog();
        }

        private void change_site_Click(object sender, EventArgs e)
        {
            this.Hide();
            choix_site site = new choix_site();
            site.idUtilisateur = idUtilisateur;
            site.Show(); 
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //selectionnement des formalités
        private void Formalite_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (Formalite.Text != "")
            {
                if (formaliteARFR.ContainsKey(Formalite.Text))
                {
                    valueAR.Text = formaliteARFR[Formalite.Text];
                }
                else
                {
                    valueAR.Text = "";
                }
            }
        }

        private void editer_Click(object sender, EventArgs e)
        {
            modification = true;
            txt_numero_sd.Enabled = true;
            Formalite.Enabled = true;
            txt_volume_depot.Enabled = true;
            txt_numero_depot.Enabled = true;
            jourDropDownList.Enabled = true;
            moisDropDownList.Enabled = true;
            anneDropDownList.Enabled = true;
        }

        //calculer le nombre des vues
        private void radButton1_Click(object sender, EventArgs e)
        {
            
            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                int nbrVues = 0;
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");                        
                        string nbr_Images = dossierRow[0]["nb_image"].ToString().Trim();
                        nbrVues = nbrVues + Convert.ToInt32(nbr_Images);
                        listeIdDossiers.Add(idDossier);
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers");
                        totalevues.Text = "Totale des vues selectionnées : 0";
                        return;
                    }

                }
                if (listeIdDossiers.Count > 0)
                {
                    totalevues.Text = "Totale des vues selectionnées : " + nbrVues;
                }
                else
                {
                    totalevues.Text = "Totale des vues selectionnées : 0";
                }
            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
                totalevues.Text = "Totale des vues selectionnées : 0";
            }
        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (alltraches.Checked == true)
            {
                foreach (var tranche in listeTranches.Items)
                {
                    tranche.Selected = true;
                }
            }
            else
            {
                foreach (var tranche in listeTranches.Items)
                {
                    tranche.Selected = false;
                }
            }
        }

        //changer la fiche sous dossier
        private void radMenuItem5_Click(object sender, EventArgs e)
        {
            
            //recuperation des pieces selectionnées
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdPieces = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 2)
                    {
                        string idPiece = item.Value.ToString();
                        listeIdPieces.Add(idPiece);
                    }
                    else
                    {
                        listeIdPieces.Clear();
                        MessageBox.Show("Merci de selectionner que des pieces");
                        return;
                    }

                }
                if (listeIdPieces.Count > 0)
                {
                    //changement du forme pour remplir la fiche sous dossier
                    ficheSD fichesd = new ficheSD();
                    fichesd.ShowDialog();
                    string numSousDossier = fichesd.numSousDossier;
                    string formalite = fichesd.formalite;
                    string volumeDepot = fichesd.volumeDepot;
                    string numDepot = fichesd.numDepot;
                    string dateDepot = fichesd.dateDepot;
                    Boolean condition = true;
                    foreach (string idvue in listeIdPieces)
                    {
                        DataRow[] piece = listePieces.Select("id_vue='" + idvue.ToString() + "'");
                        if (service.updateFicheSousDossier(idvue, numSousDossier, formalite, volumeDepot, numDepot, dateDepot))
                        {
                            piece[0]["num_sous_dos"] = numSousDossier;
                            piece[0]["formalite"] = formalite;
                            piece[0]["volume_depot"] = volumeDepot;
                            piece[0]["numero_depot"] = numDepot;
                            piece[0]["date_depot"] = dateDepot;
                        }
                        else
                        {
                            condition = false;
                            MessageBox.Show("Erreur BD , Impossible de changer la fiche dossier , Merci de contacter votre superviseur");
                        }
                    }
                    
                    //desselectionnement des pieces      
                    unite_dossier.SelectedNodes.Clear();

                    if (condition)
                    {
                        MessageBox.Show("Opération reussite");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show(" Aucun piece n'est selectionner !");
            }


        }


        //stock actuel
        private void radMenuItem5_Click_1(object sender, EventArgs e)
        {

        }

        //affectation correction
        private void radMenuItem7_Click(object sender, EventArgs e)
        {
            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");
                        string statueDOssier = dossierRow[0]["id_status"].ToString();
                        if (statueDOssier == "7")
                        {
                            listeIdDossiers.Add(idDossier);
                        }
                        else
                        {
                            listeIdDossiers.Clear();
                            MessageBox.Show("Merci de selectionner que des dossiers rejetés");
                            return;
                        }
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers rejetés");
                        return;
                    }

                }
                if (listeIdDossiers.Count > 0)
                {
                    SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                    selectAgent.id_user_operation = idUtilisateur;
                    selectAgent.correction = true;
                    selectAgent.affectation(listeIdDossiers);
                    selectAgent.ShowDialog();
                    //desselectionnement des dossier      
                    unite_dossier.SelectedNodes.Clear();
                    //relance de la recherche
                    relancer_recherche();
                }
            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
            }
        }

        //desafectation correction
        private void radMenuItem8_Click(object sender, EventArgs e)
        {

            //recuperation des dossier selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdDossiers = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {

                    if (item.Level == 1)
                    {
                        string idDossier = item.Value.ToString();
                        DataRow[] dossierRow = resultRecherche.Select("id_dossier = " + idDossier + "");
                        string statueDOssier = dossierRow[0]["id_status"].ToString();
                        if (statueDOssier == "14")
                        {
                            listeIdDossiers.Add(idDossier);
                        }
                        else
                        {
                            listeIdDossiers.Clear();
                            MessageBox.Show("Merci de selectionner que des dossiers en cours de correction ");
                            return;
                        }
                    }
                    else
                    {
                        listeIdDossiers.Clear();
                        MessageBox.Show("Merci de selectionner que des dossiers en cours de correction ");
                        return;
                    }

                }
                if (listeIdDossiers.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Voulez vous affecter les dossiers à un autre agent ?", "Désaffectation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                        selectAgent.id_user_operation = idUtilisateur;
                        selectAgent.correction = true;
                        selectAgent.reaffectation(listeIdDossiers);
                        selectAgent.ShowDialog();
                        relancer_recherche();

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        SelectionnerAgent selectAgent = new SelectionnerAgent(idLivrable.ToString());
                        selectAgent.id_user_operation = idUtilisateur;
                        selectAgent.correction = true;
                        if (selectAgent.desaffectation(listeIdDossiers))
                        {
                            MessageBox.Show("Desaffectation reussite");
                        }
                        else
                        {
                            MessageBox.Show("Erreur Desaffectation");
                        }
                    }
                    //desselectionnement des dossier      
                    unite_dossier.SelectedNodes.Clear();
                    //relance de la recherche
                    relancer_recherche();
                }
            }
            else
            {
                MessageBox.Show(" Aucun dossier n'est selectionner !");
            }
        }

        //filtrage des tranches
        private void listeFiltreTranche_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            alltraches.Checked = false;
            if (listeFiltreTranche.Text == "Livrées")
            {
                //chargement des tranches
                DataTable dtTraches = service.chargerlisteTranchesLivres();
                if (dtTraches.Rows.Count != 0)
                {
                    listeTranches.DataSource = dtTraches;
                    listeTranches.DisplayMember = dtTraches.Columns[1].ColumnName;
                    listeTranches.ValueMember = dtTraches.Columns[0].ColumnName;
                }
            }
            else if (listeFiltreTranche.Text == "En cours")
            {
                //chargement des tranches
                DataTable dtTraches = service.chargerlisteTranchesEnCours();
                if (dtTraches.Rows.Count != 0)
                {
                    listeTranches.DataSource = dtTraches;
                    listeTranches.DisplayMember = dtTraches.Columns[1].ColumnName;
                    listeTranches.ValueMember = dtTraches.Columns[0].ColumnName;
                }
            }
            else
            {
                //chargement des tranches
                DataTable dtTraches = service.chargerlisteTranches();
                if (dtTraches.Rows.Count != 0)
                {
                    listeTranches.DataSource = dtTraches;
                    listeTranches.DisplayMember = dtTraches.Columns[1].ColumnName;
                    listeTranches.ValueMember = dtTraches.Columns[0].ColumnName;
                }
            }
                
        }

        


        
    }
}
