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
using ANCFCC_NV.Properties;
using System.IO;
using System.Configuration;

namespace ANCFCC_NV
{
    public partial class Indexation : Telerik.WinControls.UI.RadForm
    {
        //static String mdbFile = ConfigurationManager.AppSettings["PATH_FILE_ACCESS"];
        Boolean demande_modification_sd = false;

        SousDossierF ficheSousDossier = new SousDossierF();
        DossierF ficheDossier = new DossierF();
        public Boolean correction = false;
        public int idUtilisateur;
        public int idLivrable;
        public string nomLivrable;
        string idDossierOuvertActuellement="";
        string namedossierouvertactuellement = "";
        string statueDossierOuvertActuellement = "";
        Service service = new Service();
        //datatables et variables relative
        DataTable listeDossiers = new DataTable();
        string id_doosier;
        string id_status_dossier;
        string ref_dossier;
        string nbr_Images;
        DataTable listePieces = new DataTable();
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
        // les boolean
        Boolean pageGardeIndexed = false;

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
        //dictionnaire arabe francais
        Dictionary<string, string> formaliteARFR = new Dictionary<string, string>();
        //dataTable date
        DataTable dateSQL = new DataTable();

        public Indexation()
        {
            InitializeComponent();
        }

        private void Indexation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //public void statistique_prod()
        //{
        //    dateSQL = service.getDateSQL();
        //    dateauj.Text = dateSQL.Rows[0]["jour"].ToString().Trim() + "/" + dateSQL.Rows[0]["mois"].ToString().Trim() + "/" + dateSQL.Rows[0]["anne"].ToString().Trim() + "  : " + service.getProductivite(idUtilisateur, 3).ToString();
        //}

        private void Indexation_Load(object sender, EventArgs e)
        {
            modifierButton.Enabled = false;
            //statistique_prod();

            //if (!File.Exists(mdbFile))
            //{
            //    MessageBox.Show("Erreur mdb file");
            //}

            //Description Livrable
            radGroupBox3.Text = "Mes dossiers du Livrable : " + nomLivrable.ToString().Trim();
            //charger les listes drow dows
            charger_les_dropdownliste();
            //chargement de la liste des dossier affectées
            recuperation_dossiers_affectes();
            // Action pour actualise la liste des dossiers
            this.actualise.Click += new EventHandler(actualise_Click);
            // Action ouverture dossier
            this.Ouvrir.Click += new EventHandler(ouvrir_Click);
            
            //action cloturer
            this.Cloturer.Click += new EventHandler(cloturer_Click);
            //action renitialiser
            this.reinitialiser_dossier.Click += new EventHandler(reinitialiser_dossier_Click);
            //Charger categorie dossier
            this.charger_cat_dossier.Click += new EventHandler(charger_cat_dossier_Click);
            //suprimer la piece
            this.delete_piece.Click += new EventHandler(delete_piece_Click);
            
            
            //menue TF
            Cloturer.Enabled = false;
            reinitialiser_dossier.Enabled = false;

        }

        public void charger_les_dropdownliste()
        {
            //liste nompieces            
            DataTable dtListePieces = service.chargerListeNomPieces();
            NomPiece.DataSource = dtListePieces;            
            NomPiece.DisplayMember = dtListePieces.Columns[1].ColumnName;
            NomPiece.ValueMember = dtListePieces.Columns[0].ColumnName;
            NomPiece.Items.Add("PAGE DE GARDE DU SOUS DOSSIER");
            NomPiece.Items.Add("FICHE DE CONTROLE");
            NomPiece.Items.Add("PAGE DE GARDE DU DOSSIER");
            NomPiece.Text = "";
            txtnbrpage2.Text = "";
            txtnumpage.Text = "";
            //liste formalites
            DataTable dtlisteFormalites = service.chargerlisteFormalites();
            Formalites.DataSource = dtlisteFormalites;
            Formalites.DisplayMember = dtlisteFormalites.Columns[0].ColumnName;
            Formalites.ValueMember = dtlisteFormalites.Columns[0].ColumnName;

            foreach (DataRow dr in dtlisteFormalites.Rows)
            {
                string key = dr["LIBELLE_FORMALITE"].ToString().Trim();
                string value = dr["LIBELLE_FORMALITE_ARAB"].ToString().Trim();
                if (!formaliteARFR.ContainsKey(key))
                {
                    formaliteARFR.Add(key, value);
                }

            }
            //Formalites.Text = "";
       
            //liste jours
            ArrayList listeJours = Service.chargerListeDesjours();
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

            //liste des mois
            ArrayList listesMois = Service.chargerListeDesmois();
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

            // liste des annes
            ArrayList listeAnnes = Service.chargerListeDesAnnes();
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

            //liste nature origine
            ArrayList listeNatureOrigine = Service.chargerlisteNatureOrigine();
            if (listeNatureOrigine.Count != 0)
            {
                foreach (String natureOrigine in listeNatureOrigine)
                {
                    txt_nature_orgine.Items.Add(natureOrigine);
                }
            }
            else
            {
                MessageBox.Show("Impossible de récuperer la liste des natures origines");
            }

            //liste indice origine           
            DataTable listeIndiceOrigine = Service.chargerlisteIndiceOrigineTable();
            txt_indice_orgine.DataSource = listeIndiceOrigine;
            txt_indice_orgine.DisplayMember = listeIndiceOrigine.Columns[1].ColumnName;
            txt_indice_orgine.ValueMember = listeIndiceOrigine.Columns[0].ColumnName;

            //liste indice titre
            DataTable listeIndiceTitre = Service.chargerlisteIndiceTitreTable();
            IndiceTitre.DataSource = listeIndiceTitre;
            IndiceTitre.DisplayMember = listeIndiceTitre.Columns[1].ColumnName;
            IndiceTitre.ValueMember = listeIndiceTitre.Columns[0].ColumnName;
            
        }

        public void recuperation_dossiers_affectes()
        {
            string dossierEncours = "";
            listeDossiers = service.getFolders(idUtilisateur, idLivrable,correction);
            //clear nodes
            this.unite_dossier.Nodes.Clear();
            RadTreeNode mesDossiers = new RadTreeNode();
            mesDossiers.Image = Resources.agentsaisie;
            mesDossiers.ContextMenu = MenueRacine;
            foreach (DataRow row in listeDossiers.Rows)
            {
                //recuperation des donnes
                id_doosier = row["id_dossier"].ToString().Trim();
                id_status_dossier = row["id_status"].ToString().Trim();
                ref_dossier = row["name_dossier"].ToString().Trim();
                nbr_Images = row["nb_image"].ToString().Trim();

                //creation d'arboresence
                RadTreeNode TF = new RadTreeNode();
                TF.Value = id_doosier;
                TF.Name = id_doosier;
                //nomination du dossier en se basant le statue
                TF.Text = generate_textDossier_using_statue(id_status_dossier);
                if (id_status_dossier == "2")
                {
                    dossierEncours = TF.Name;
                    idDossierOuvertActuellement = id_doosier;
                    namedossierouvertactuellement = ref_dossier;
                    statueDossierOuvertActuellement = id_status_dossier;
                }
                TF.Image = Resources.redFolder;
                TF.ContextMenu = MenuTF;
                mesDossiers.Nodes.Add(TF);
            }
            mesDossiers.ExpandAll();
            mesDossiers.Value = mesDossiers.Nodes.Count;
            mesDossiers.Name = mesDossiers.Nodes.Count.ToString();
            mesDossiers.Text = "Totale des dossiers trouvés : " + mesDossiers.Nodes.Count;
            this.unite_dossier.Nodes.Add(mesDossiers);
            
        }

        public string generate_textDossier_using_statue(string id_status_dossier)
        {
            string text = "";
            if (id_status_dossier == "2")
            {
                text = ref_dossier + " (" + nbr_Images + ") ( En cours )";
            }
            else if (id_status_dossier == "4")
            {
                text = ref_dossier + " (" + nbr_Images + ") ( En Instance )";
            }
            else
            {
                text = ref_dossier + " (" + nbr_Images + ")";
            }
            return text;
        }
        
        private void charger_cat_dossier_Click(object sender, EventArgs e)
        {
            string idVue = unite_dossier.SelectedNode.Name.ToString();
            DataRow[] result = listePieces.Select("id_vue = " + idVue + "");
            foreach (DataRow row in result)
            {
                string statueVue = row["id_status"].ToString().Trim();
                if (statueVue == "3")
                {
                    //partie sous dossier
                    ficheSousDossier.numero_sousDossier = row["num_sous_dos"].ToString().Trim();
                    ficheSousDossier.formalite = row["formalite"].ToString().Trim();
                    ficheSousDossier.volume_depot = row["volume_depot"].ToString().Trim();
                    ficheSousDossier.numero_depot = row["numero_depot"].ToString().Trim();
                    ficheSousDossier.dateDepot = row["date_depot"].ToString().Trim();
                    MessageBox.Show("N° sous dossier : " + ficheSousDossier.numero_sousDossier + "\n" + "Formalité :" + ficheSousDossier.formalite + "\n" + "Volume Dépot : " + ficheSousDossier.volume_depot + "\n" + "Numero Depot : " + ficheSousDossier.numero_depot + "\n" + "Date Dépot :" + ficheSousDossier.dateDepot,"Sous dossier charger");
                }
                else
                {
                    MessageBox.Show("Cette piece n'est pas indexer !");
                }
                
            }
            
        }

        private void actualise_Click(object sender, EventArgs e)
        {
            if (idDossierOuvertActuellement != "")
            {
                MessageBox.Show("Merci de cloturer le dossier : " + namedossierouvertactuellement + " en premier");
            }
            else
            {
                //deselection de l'arboresence
                unite_dossier.SelectedNodes.Clear();
                recuperation_dossiers_affectes();
                
            }
        }

        private void ouvrir_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement || idDossierOuvertActuellement=="")
            {                
                Ouvrir.Enabled = false;
                Cloturer.Enabled = true;
                reinitialiser_dossier.Enabled = true;

                //chargement des pieces
                RadTreeNode TF = unite_dossier.SelectedNode;
                listePieces.Clear();
                listePieces = service.getListePieceTF(TF.Value.ToString());
                idDossierOuvertActuellement = TF.Value.ToString();
                namedossierouvertactuellement = TF.Text.ToString();
                //recupération de statue dossier
                DataRow[] dossierRow = listePieces.Select("id_dossier = " + idDossierOuvertActuellement + "");
                statueDossierOuvertActuellement = dossierRow[0]["id_status"].ToString();
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
                    piece.Image = Resources.document;
                    piece.ContextMenu = MenuePiece;
                    //ajout de la piece au dossier
                    TF.Nodes.Add(piece);
                }
            }
            else
            {
                MessageBox.Show("Merci de cloturer le dossier : " + namedossierouvertactuellement + " en premier");
            }

            
        }

        private void instance_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement)
            {
                MessageBox.Show("En cours");
            }
            else
            {
                MessageBox.Show("Merci de cloturer le dossier : " + namedossierouvertactuellement + " en premier");
            }

            
        }

        private void cloturer_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement)
            {
                if (pageGardeIndexed == false)
                {
                    MessageBox.Show("La page de garde n'est pas indexer");
                }
                else
                {
                    DataRow[] piecesNonIndexed = listePieces.Select("id_status <> 3 AND id_status <> 5");
                    if (piecesNonIndexed.Length > 0)
                    {
                        MessageBox.Show("Merci d'indexer la totalité des pieces");
                    }
                    else
                    {
                        if (service.insertPices_inBD(idUtilisateur, listePieces))
                        {
                            if(service.updateTFinBD("3",ficheDossier, idUtilisateur, idDossierOuvertActuellement))
                            {
                                if (service.insert_Historique_Dossier(idUtilisateur, idDossierOuvertActuellement))
                                {
                                    if (service.insert_Operation_Dossier(idUtilisateur, idDossierOuvertActuellement, "3"))
                                    {
                                        MessageBox.Show("Indexation reussite");
                                        listePieces.Clear();
                                        //deselection de l'arboresence
                                        unite_dossier.SelectedNodes.Clear();
                                        recuperation_dossiers_affectes();
                                        //renitialisation des variables
                                        reset_after_closingfolder();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Historisation de l'operation dossier echoue ! ", "Erreur");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Historisation dossier echoue ! ","Erreur");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Indexation echoue");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Indexation echoue");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Merci de cloturer le dossier : " + namedossierouvertactuellement + " en premier");
            }
        }

        public void reset_after_closingfolder()
        {
            valueAR.Text = "";
            toolStrip1.Visible = false;
            pb.Image = null;
            indexer.Enabled = false;
            //renitialisation des variables
            idDossierOuvertActuellement = "";
            namedossierouvertactuellement = "";
            statueDossierOuvertActuellement = "";
            pageGardeIndexed = false;
            //activation du menue
            Ouvrir.Enabled = true;
            Cloturer.Enabled = false;
            reinitialiser_dossier.Enabled = false;
            //page garde
            pageGardeIndexed = false;            
            NomPiece.Items.Add("PAGE DE GARDE DU DOSSIER");
            //renitialisation de affichage
            NomPiece.Enabled = false;
            txtnbrpage2.Enabled = false;
            txtnumpage.Enabled = false;
            viderFichePiece();
            //charger les fiche
            ficheDossier = new DossierF();
            chargerFicheDossier(ficheDossier);
            ficheSousDossier = new SousDossierF();
            chargerFicheSousDossier(ficheSousDossier);
            //actualiser la prod
            //statistique_prod();

        }

        private void reinitialiser_dossier_Click(object sender, EventArgs e)
        {
            if (unite_dossier.SelectedNode.Value.ToString() == idDossierOuvertActuellement)
            {
                    //deselection de l'arboresence
                    unite_dossier.SelectedNodes.Clear();
                    //relance recherche
                    recuperation_dossiers_affectes();
                    //réactivation des options 
                    reset_after_closingfolder();
            }
            else
            {
                MessageBox.Show("Merci de cloturer le dossier : " + namedossierouvertactuellement + " en premier");
            }
        }

        public string afficher_indexes_piece(string idVueRecupered)
        {
            String chemin = "";
            string idVue = unite_dossier.SelectedNode.Name.ToString();
            DataRow[] result = listePieces.Select("id_vue = " + idVueRecupered + "");
            foreach (DataRow row in result)
            {

                //statuePieceSelectionnerActuellement = row["id_status"].ToString().Trim();
                chemin = row["url"].ToString();

                //partie piece
                NomPiece.Text = row["nom_piece"].ToString().Trim();
                txtnbrpage2.Text = row["nombre_page"].ToString().Trim();
                txtnumpage.Text = row["num_page"].ToString().Trim();

                //partie sous dossier
                txt_numero_sd.Text = row["num_sous_dos"].ToString().Trim();
                Formalites.Text = row["formalite"].ToString().Trim();
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
            }
            return chemin;
        }

        public void dispose_providers_sd()
        {
            numSousDossierProvider.Dispose();
            volumeDepotProvider.Dispose();
            numDepotProvider.Dispose();
        }

        public void dispose_providers_d()
        {
            NumOrigineProvider.Dispose();
            numTitreProvider.Dispose();
        }

        public void dispose_providers_piece()
        {
            NbrPagesProvider.Dispose();
            NumPagesProvider.Dispose();
        }

        public void dispose_providers()
        {
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

            dispose_providers_piece();
            dispose_providers_d();
            dispose_providers_sd();

        }
        
        //selectionnement de la piece
        private void unite_dossier_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (unite_dossier.SelectedNode.Level == 2)
            {                
                toolStrip1.Visible = true;
                indexer.Enabled = true;
                NomPiece.Enabled = true;
                txtnbrpage2.Enabled = true;
                txtnumpage.Enabled = true;

                this.dispose_providers();

                string idVue = unite_dossier.SelectedNode.Name.ToString();
                String chemin = afficher_indexes_piece(idVue);

                enabled_zones_saisie();

                if (chemin == "")
                {                    
                    this.pb.Width = 600;
                    this.pb.Height = 450;
                    this.pb.SizeMode = PictureBoxSizeMode.Zoom;                    
                    image = (Bitmap)Resources.vue_indisponible;
                    defImgHeight = image.Height;
                    defImgWidth = image.Width;
                    this.pb.Image = image;

                }
                else
                {
                    this.pb.Width = 600;
                    this.pb.Height = 450;
                    this.pb.SizeMode = PictureBoxSizeMode.Zoom;
                    image_stream = service.pathToStream(@chemin);
                    image = (Bitmap)Image.FromStream(image_stream, true, false);
                    defImgHeight = image.Height;
                    defImgWidth = image.Width;
                    this.pb.Image = image;
                }
            }
            else
            {
                modifierButton.Enabled = false;
                demande_modification_sd = false;
                toolStrip1.Visible = false;
            }
            
        }

        public void enabled_zones_saisie()
        {
            //affichage de zone dossier selon le nom de la piece
            if (NomPiece.Text == "PAGE DE GARDE DU SOUS DOSSIER")
            {
                modifierButton.Enabled = false;

                //afficher les champs du sousdossier
                txt_numero_sd.Enabled = true;
                Formalites.Enabled = true;
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
                modifierButton.Enabled = false;
                //masquer les champs du sous dossiers
                txt_numero_sd.Enabled = false;
                Formalites.Enabled = false;
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
            else if (NomPiece.Text == "" || NomPiece.Text == "FICHE DE CONTROLE")
            {
                modifierButton.Enabled = false;
                chargerFicheSousDossier(ficheSousDossier);
                //masquer les champs du sous dossiers
                txt_numero_sd.Enabled = false;
                Formalites.Enabled = false;
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
            else
            {
                modifierButton.Enabled = true;
                //masquer les champs du sous dossiers
                txt_numero_sd.Enabled = false;
                Formalites.Enabled = false;
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
            
            chargerFicheDossier(ficheDossier);
        }

        private void indexer_Click(object sender, EventArgs e)
        {
            if (NomPiece.Text != "")
            {
                if ((NomPiece.Text == "PAGE DE GARDE DU DOSSIER" && txt_nature_orgine.Text != "" && txt_numero_orgine.Text != "" && txt_indice_orgine.Text != "" && txt_numero_titre.Text != "" && IndiceTitre.Text != "") || (NomPiece.Text != "PAGE DE GARDE DU SOUS DOSSIER" && NomPiece.Text != "PAGE DE GARDE DU DOSSIER") || (NomPiece.Text == "PAGE DE GARDE DU SOUS DOSSIER" && txt_numero_sd.Text != "" && Formalites.Text != "" && txt_volume_depot.Text != "" && txt_numero_depot.Text != "" && jourDropDownList.Text != "" && moisDropDownList.Text != "" && anneDropDownList.Text != ""))
                {
                    if (!FichePieceErreur && !FicheDossierErreur && !FicheSousDossierErreur)
                    {
                        RadTreeNode pieceIndexer = unite_dossier.SelectedNode;
                        string idVue = pieceIndexer.Name.ToString();
                        string pieceName = NomPiece.Text;

                        //recuperation de la ligne piece au niveau du Dataset
                        DataRow[] pieceRow = listePieces.Select("id_vue='" + idVue.ToString() + "'");
                        //recuperation du nom de la piece enregistrer
                        string namePieceRecupered = pieceRow[0]["nom_piece"].ToString().Trim();
                        string namePieceInseret = pieceName;
                        if (namePieceRecupered == "PAGE DE GARDE DU DOSSIER" && pieceName != "PAGE DE GARDE DU DOSSIER")
                        {
                            pageGardeIndexed = false;
                            ficheDossier = new DossierF();
                            chargerFicheDossier(ficheDossier);
                            NomPiece.Items.Add("PAGE DE GARDE DU DOSSIER");
                        }
                        //partie piece
                        pieceRow[0]["nom_piece"] = pieceName;
                        pieceRow[0]["nombre_page"] = txtnbrpage2.Text.ToString();
                        pieceRow[0]["num_page"] = txtnumpage.Text.ToString();

                        if (pieceName == "PAGE DE GARDE DU DOSSIER")
                        {
                            pageGardeIndexed = true;
                            string natureOrigine = txt_nature_orgine.Text, numOrigine = txt_numero_orgine.Text, indiceOrigine = txt_indice_orgine.Text, indiceSpOrigine = txt_indice_speciale_orgine.Text, numTitre = txt_numero_titre.Text, indiceTitre = IndiceTitre.Text, indiceSpTitre = txt_indice_spciale_titre.Text;
                            ficheDossier = new DossierF(natureOrigine, numOrigine, indiceOrigine, indiceSpOrigine, numTitre, indiceTitre, indiceSpTitre);
                            chargerFicheDossier(ficheDossier);
                            ficheSousDossier = new SousDossierF();

                            //liste nompieces 
                            //NomPiece.Items.Clear();
                            DataTable dtListePieces = service.chargerListeNomPieces();
                            NomPiece.DataSource = dtListePieces;
                            NomPiece.DisplayMember = dtListePieces.Columns[1].ColumnName;
                            NomPiece.ValueMember = dtListePieces.Columns[0].ColumnName;
                            NomPiece.Items.Add("PAGE DE GARDE DU SOUS DOSSIER");
                            NomPiece.Items.Add("FICHE DE CONTROLE");

                        }
                        else if (pieceName == "FICHE DE CONTROLE")
                        {
                            ficheSousDossier = new SousDossierF();
                        }
                        else if (pieceName == "PAGE DE GARDE DU SOUS DOSSIER")
                        {
                            //les indexes enregistrer au niveau du data set
                            string numSousDossierSaved = pieceRow[0]["num_sous_dos"].ToString();
                            string formaliteSaved = pieceRow[0]["formalite"].ToString();
                            string volumeDepotSaved = pieceRow[0]["volume_depot"].ToString();
                            string numDepotSaved = pieceRow[0]["numero_depot"].ToString();
                            string dateDepotSaved = pieceRow[0]["date_depot"].ToString();
                            //les indexes saisie au niveau de l'interface
                            string numSousDossier = txt_numero_sd.Text, formalite = Formalites.Text, volumeDepot = txt_volume_depot.Text, numDepot = txt_numero_depot.Text;
                            string dateDepot = jourDropDownList.Text + "/" + moisDropDownList.Text + "/" + anneDropDownList.Text;
                            ficheSousDossier = new SousDossierF(numSousDossier, formalite, volumeDepot, numDepot, dateDepot);
                            //recherche sur dataset pour récuperer la liste des pieces qui font partie du meme dossier sous de la piece selectionner
                            DataRow[] pieceWithSameSD = listePieces.Select("num_sous_dos='" + numSousDossierSaved + "'" + "AND formalite='" + formaliteSaved + "'" + "AND volume_depot='" + volumeDepotSaved + "'" + "AND numero_depot='" + numDepotSaved + "'" + "AND date_depot='" + dateDepotSaved + "'");
                            foreach (DataRow piece in pieceWithSameSD)
                            {
                                string idVueInSD = piece["id_vue"].ToString();
                                piece["num_sous_dos"] = numSousDossier;
                                piece["formalite"] = formalite;
                                piece["volume_depot"] = volumeDepot;
                                piece["numero_depot"] = numDepot;
                                piece["date_depot"] = dateDepot;
                            }
                        }
                        //si le button modifier à été cliqué
                        if (modifierButton.Enabled == true)
                        {
                            pieceRow[0]["num_sous_dos"] = ficheSousDossier.numero_sousDossier;
                            pieceRow[0]["formalite"] = ficheSousDossier.formalite;
                            pieceRow[0]["volume_depot"] = txt_volume_depot.Text;
                            pieceRow[0]["numero_depot"] = txt_numero_depot.Text;
                            string dateDepot = jourDropDownList.Text + "/" + moisDropDownList.Text + "/" + anneDropDownList.Text;
                            pieceRow[0]["date_depot"] = dateDepot;
                        }
                        //remplissage de la fiche sus dossier
                        else
                        {
                            pieceRow[0]["num_sous_dos"] = ficheSousDossier.numero_sousDossier;
                            pieceRow[0]["formalite"] = ficheSousDossier.formalite;
                            pieceRow[0]["volume_depot"] = ficheSousDossier.volume_depot;
                            pieceRow[0]["numero_depot"] = ficheSousDossier.numero_depot;
                            pieceRow[0]["date_depot"] = ficheSousDossier.dateDepot;
                        }


                        //changement statue piece
                        pieceRow[0]["id_status"] = "3";
                        unite_dossier.GetNodeByName(idVue).Text = idVue + "-" + pieceName + "-" + txtnumpage.Text + "/" + txtnbrpage2.Text;
                        //affichage image de la piece
                        unite_dossier.GetNodeByName(idVue).Image = getImage_Piece_By_Statue_et_nom(pieceName);
                        if (unite_dossier.SelectedNode.NextNode != null)
                        {
                            unite_dossier.SelectedNode = pieceIndexer.NextNode;
                        }
                        else
                        {
                            unite_dossier.SelectedNode = pieceIndexer.Parent;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Merci de saisir des valeurs correctes","Erreur ! ");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir les champs obligatoires","Attention");
                }
            }
            else
            {
                MessageBox.Show("Merci de saisir le nom de la piece", "Erreur ! ");
            }

            //fin

        }

        public Bitmap getImage_Piece_By_Statue_et_nom(string nomPiece)
        {

            Bitmap image = Resources.document;

            if (nomPiece == "PAGE DE GARDE DU DOSSIER")
            {
                image = Resources.redFolder;
            }
            else if (nomPiece == "PAGE DE GARDE DU SOUS DOSSIER")
            {
                image = Resources.jauneDossier;
            }           
            else
            {
                
                    image = Resources.document_ok;                
            }
            return image;
        }

        private void chargerFicheDossier(DossierF ficheDossierRecuperer)
        {

            txt_nature_orgine.Text = ficheDossierRecuperer.nature_Orgine;
            txt_numero_orgine.Text = ficheDossierRecuperer.numero_Orgine;
            txt_indice_orgine.Text = ficheDossierRecuperer.indice_orgine;
            txt_indice_speciale_orgine.Text = ficheDossierRecuperer.indice_special_orgine;
            txt_numero_titre.Text = ficheDossierRecuperer.numero_Titre;
            IndiceTitre.Text = ficheDossierRecuperer.indice_titre;
            txt_indice_spciale_titre.Text = ficheDossierRecuperer.indice_special_titre;
            //fin fiche dossier            
        }

        private void chargerFicheSousDossier(SousDossierF ficheSousDossie)
        {
            txt_numero_sd.Text = ficheSousDossie.numero_sousDossier;
            Formalites.Text = ficheSousDossie.formalite;
            txt_volume_depot.Text = ficheSousDossie.volume_depot;
            txt_numero_depot.Text = ficheSousDossie.numero_depot;
            string daterecup = ficheSousDossie.dateDepot;
            string[] words = daterecup.Split('/');
            string jour = words[0];
            string mois = words[1];
            string annetotale = words[2];
            string anne = annetotale.Split(' ')[0];
            jourDropDownList.Text = jour;
            moisDropDownList.Text = mois;
            anneDropDownList.Text = anne;
        }

        //selectionnement du nom de la piece
        private void NomPiece_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {            
                //affichage de zone dossier selon le nom de la piece
                if (NomPiece.Text == "PAGE DE GARDE DU SOUS DOSSIER")
                {
                    modifierButton.Enabled = false;
                    FicheDossierErreur = false;
                    dispose_providers_d();

                    viderFicheSousDossier();
                    //afficher les champs du sousdossier
                    txt_numero_sd.Enabled = true;
                    Formalites.Enabled = true;
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
                    modifierButton.Enabled = false;
                    FicheSousDossierErreur = false;
                    dispose_providers_sd();

                    viderFicheSousDossier();
                    //masquer les champs du sous dossiers
                    txt_numero_sd.Enabled = false;
                    Formalites.Enabled = false;
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
                else if (NomPiece.Text == "FICHE DE CONTROLE")
                {
                    modifierButton.Enabled = false;
                    FicheSousDossierErreur = false;
                    FicheDossierErreur = false;
                    dispose_providers_sd();
                    dispose_providers_d();

                    viderFicheSousDossier();
                    //masquer les champs du sous dossiers
                    txt_numero_sd.Enabled = false;
                    Formalites.Enabled = false;
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
                else
                {
                    if (NomPiece.Text == "")
                    {
                        modifierButton.Enabled = false;
                    }
                    else
                    {
                        modifierButton.Enabled = true;
                    }
                    FicheSousDossierErreur = false;
                    FicheDossierErreur = false;
                    dispose_providers_sd();
                    dispose_providers_d();

                    chargerFicheSousDossier(ficheSousDossier);
                    //masquer les champs du sous dossiers
                    txt_numero_sd.Enabled = false;
                    Formalites.Enabled = false;
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
                //attibution de 1 1 pour le nombre des pages et totale des pages
                txtnbrpage2.Text = "1";
                txtnumpage.Text = "1";
                // charger les fiches            
                chargerFicheDossier(ficheDossier);
            
        }

        public void viderFicheDossier()
        {
            txt_nature_orgine.Text = "";
            txt_numero_orgine.Text = "";
            txt_indice_orgine.Text = "";
            txt_indice_speciale_orgine.Text = "";
            txt_numero_titre.Text = "";
            IndiceTitre.Text = "";
            txt_indice_spciale_titre.Text = "";
        }

        public void viderFicheSousDossier()
        {            
            txt_numero_sd.Text = "";
            Formalites.Text = "";
            valueAR.Text = "";
            txt_volume_depot.Text = "";
            txt_numero_depot.Text = "";
            jourDropDownList.Text = "";
            moisDropDownList.Text = "";
            anneDropDownList.Text = "";
        }

        public void viderFichePiece()
        {
            NomPiece.Text = "";
            txtnbrpage2.Text = "";
            txtnumpage.Text = "";
        }

        //button Agrafer
        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            //recuperation des pieces selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdPieces = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 2)
                    {
                        string idPiece = item.Name.ToString();
                        DataRow[] pieceRow = listePieces.Select("id_vue='" + idPiece + "'");
                        string statuePiece = pieceRow[0]["id_status"].ToString();
                        if (statuePiece == "1" || statuePiece == "0")
                        {
                            listeIdPieces.Add(idPiece);
                        }
                        else
                        {
                            listeIdPieces.Clear();
                            MessageBox.Show("Merci de selectionner que des pieces non indexes");
                            return;
                        }
                    }
                    else
                    {
                        listeIdPieces.Clear();
                        MessageBox.Show("Merci de selectionner que des images");
                        return;
                    }
                }
                if (listeIdPieces.Count > 0)
                {
                setNomPiece setNom = new setNomPiece();
                DialogResult dr = setNom.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    setNom.Close();
                }
                else if (dr == DialogResult.OK)
                {
                    string nom_piece = setNom.getText();
                    string nombre_page = nbrselection.ToString();
                    for (int i = 0; i < nbrselection; i++)
                    {
                        string idPiece = unite_dossier.SelectedNodes[i].Name.ToString();

                        //recuperation de la ligne piece au niveau du Dataset
                        DataRow[] pieceRow = listePieces.Select("id_vue='" + idPiece + "'");
                        //partie piece
                        pieceRow[0]["nom_piece"] = nom_piece;
                        pieceRow[0]["nombre_page"] = nombre_page;
                        pieceRow[0]["num_page"] = (i + 1).ToString();
                        //remplissage de la fiche sus dossier
                        pieceRow[0]["num_sous_dos"] = ficheSousDossier.numero_sousDossier;
                        pieceRow[0]["formalite"] = ficheSousDossier.formalite;
                        pieceRow[0]["volume_depot"] = ficheSousDossier.volume_depot;
                        pieceRow[0]["numero_depot"] = ficheSousDossier.numero_depot;
                        pieceRow[0]["date_depot"] = ficheSousDossier.dateDepot;
                        //changement statue piece
                        pieceRow[0]["id_status"] = "3";

                        unite_dossier.SelectedNodes[i].Text = idPiece + "-" + nom_piece + "-" + (i + 1).ToString() +"/" + nombre_page;
                        //affichage image de la piece
                        unite_dossier.SelectedNodes[i].Image = Resources.document_ok;
                    }

                    if (unite_dossier.SelectedNodes[nbrselection-1].NextNode != null)
                    {
                        unite_dossier.SelectedNode = unite_dossier.SelectedNodes[nbrselection - 1].NextNode;
                    }
                    else
                    {
                        unite_dossier.SelectedNode = unite_dossier.SelectedNodes[nbrselection - 1].Parent;
                    }
                }
                }
            }
            else
            {
                MessageBox.Show(" Aucune piece n'a été selectionner !");
            }
        }

        private void changeLivrable_Click(object sender, EventArgs e)
        {
            if (idDossierOuvertActuellement!="")
            {
                MessageBox.Show("Attention : Merci d'annuler , clôturer où mettre en instance le dossier : " + namedossierouvertactuellement + " avant d'effectuer cette operation");
            }
            else
            {
                this.Hide();
                Livrable livrable = new Livrable();
                livrable.idUtilisateur = idUtilisateur;
                livrable.Show();
            }
        }

        private void se_deconnecter_Click(object sender, EventArgs e)
        {
            if (idDossierOuvertActuellement != "")
            {
                MessageBox.Show("Attention : Merci d'annuler , clôturer où mettre en instance le dossier : " + namedossierouvertactuellement + " avant d'effectuer cette operation");
            }
            else
            {
                Application.Exit();
            }
        }

        private void delete_piece_Click(object sender, EventArgs e)
        {
            string idPiece = unite_dossier.SelectedNode.Name;
            DataRow[] pieceRow = listePieces.Select("id_vue='" + idPiece + "'");
            pieceRow[0]["id_status"] = "5";
            unite_dossier.SelectedNode.Image = Resources.document_delete;
            
        }
        
        //renitialiser les indexes
        private void renitialiser_Click(object sender, EventArgs e)
        {
            //recuperation des pieces selectionner
            int nbrselection = unite_dossier.SelectedNodes.Count;
            if (nbrselection > 0)
            {
                ArrayList listeIdPieces = new ArrayList();
                foreach (var item in unite_dossier.SelectedNodes)
                {
                    if (item.Level == 2)
                    {
                        string idPiece = item.Name.ToString();
                        DataRow[] pieceRow = listePieces.Select("id_vue='" + idPiece + "'");
                        string statuePiece = pieceRow[0]["id_status"].ToString();
                        if (statuePiece == "3")
                        {
                            listeIdPieces.Add(idPiece);
                        }
                        else
                        {
                            listeIdPieces.Clear();
                            MessageBox.Show("Merci de selectionner que des pieces indexes");
                            return;
                        }
                    }
                    else
                    {
                        listeIdPieces.Clear();
                        MessageBox.Show("Merci de selectionner que des images");
                        return;
                    }
                }

                if (listeIdPieces.Count > 0)
                {
                    Boolean pageGarde = false; ;
                    for (int i = 0; i < nbrselection; i++)
                    {
                        string idPiece = unite_dossier.SelectedNodes[i].Name.ToString();

                        //recuperation de la ligne piece au niveau du Dataset
                        DataRow[] pieceRow = listePieces.Select("id_vue='" + idPiece + "'");
                        //partie piece
                        if (pieceRow[0]["nom_piece"].ToString().Trim() == "PAGE DE GARDE DU DOSSIER")
                        {
                            pageGarde = true;
                        }
                        pieceRow[0]["nom_piece"] = "";
                        pieceRow[0]["nombre_page"] = "";
                        pieceRow[0]["num_page"] = "";
                        //remplissage de la fiche sus dossier
                        pieceRow[0]["num_sous_dos"] = "";
                        pieceRow[0]["formalite"] = "";
                        pieceRow[0]["volume_depot"] = "";
                        pieceRow[0]["numero_depot"] = "";
                        pieceRow[0]["date_depot"] = "01/01/1980";
                        //changement statue piece
                        pieceRow[0]["id_status"] = "1";

                        unite_dossier.SelectedNodes[i].Text = idPiece;
                        //affichage image de la piece
                        unite_dossier.SelectedNodes[i].Image = Resources.document;
                    }
                    if (pageGarde)
                    {
                        pageGardeIndexed = false;
                        ficheDossier = new DossierF();
                        chargerFicheDossier(ficheDossier);
                        NomPiece.Items.Add("PAGE DE GARDE DU DOSSIER");
                    }
                }
            }
            else
            {
                MessageBox.Show(" Aucune piece n'a été selectionner !");
            }
        }
        
        //-------------/
        //leave nombre pages
        private void txtnbrpage2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnbrpage2.Text))
            {
                NbrPagesProvider.Icon = Resources.error;
                NbrPagesProvider.SetError(txtnbrpage2, "Erreur Merci de saisie un chiffre");
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

        //leave numéro pages
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
        
        //-------------/
        //leave numero depot
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

        //leave volume depot
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

        //leave numero sd
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

        //-------------/
        //leave numero titre
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

        //leave numero origine
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

        //zoom
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

        //selectionnement de la formalité avec affichage arabe
        private void Formalites_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (Formalites.Text != "")
            {
                if (formaliteARFR.ContainsKey(Formalites.Text))
                {
                    valueAR.Text = formaliteARFR[Formalites.Text];
                }
                else
                {
                    valueAR.Text = "";
                }
            }
        }

        //press key for delete
        private void unite_dossier_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //button modifier sous dossier
        private void radButton1_Click(object sender, EventArgs e)
        {
            txt_volume_depot.Enabled = true;
            txt_numero_depot.Enabled = true;
            jourDropDownList.Enabled = true;
            moisDropDownList.Enabled = true;
            anneDropDownList.Enabled = true;
            
        }

        




        
    }
}
