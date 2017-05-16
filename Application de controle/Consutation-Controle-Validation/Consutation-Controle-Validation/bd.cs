using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Consutation_Controle_Validation
{
    class bd
    {
        public static SqlConnection connectionAdministration = new SqlConnection(ConfigurationManager.ConnectionStrings["GEDConnectionString"].ToString());
        public SqlConnection connection;

        //requette d'authentification
        string reqAuthentification = "SELECT [id_user] FROM [TB_Utilisateurs] where login=('$user') and pass=('$password')";
        //requette recuperation de la liste des livrables
        string reqGetLivrable = "SELECT [id_livrable],[nom_livrable] FROM [TB_Livrable]";
        //requette recuperation des mois/anne de production par livrable
        string reqGetMoisAnneParLivrable = "SELECT distinct MONTH(date_index),YEAR(date_index) FROM [TB_Dossier] where id_livrable=('$idlivrable') and date_index IS NOT NULL";
        //requette pour le recuperation des listes des dates d'indexation en utilisant le mois et anne choisie
        string reqGetlisteDateDindexation = "SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where MONTH(date_index)=('$mois') and YEAR(date_index)=('$anne') and id_livrable=('$idlivrable') and date_index IS NOT NULL";
        //requette pour la recuperation des listes des d'indexation pour toutes la dates choises
        string reqGetlisteDateDindexationAllDates = "SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where id_livrable=('$idlivrable') and date_index IS NOT NULL order by CAST(date_index AS date)";
        //requette pour la recuperation des utilisateurs qui ont travailler sur la date selectionner
        string reqGetListeUsers = "select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier] where MONTH(date_index)=('$mois') and day(date_index)=('$jour') and year(date_index)=('$anne') and id_livrable=('$idlivrable'))";
        //requette pour la recuperation des utilisateurs qui ont travailler pour toutes mois/annes mais en précisant le mois et anné
        string reqGetListeUsersAllDatesWithoutDay = "select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier] where MONTH(date_index)=('$mois') and year(date_index)=('$anne') and id_livrable=('$idlivrable'))";
        //requette pour la recuperation des utilisateurs qui ont travailler pour toutes les journées mais en précisant la journée
        string reqGetListeUsersAllDatesWithoutYearDay = "select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier] where day(date_index)=('$jour') and id_livrable=('$idlivrable'))";
        //requette pour la recuperation des utilisateurs qui ont travailler pour toutes les jours du mois selectionner
        string reqGetListeUsersAllDates = "select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier] where id_livrable=('$idlivrable'))";
        //requette pour la recuperation des utilisateurs qui ont travailler pour toutes les jours du mois selectionner without livrable
        string reqGetListeUsersAllDatesWithoutLivrable = "select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier])";
            
        //requette pour la recuperation des dossier , leurs id et leurs statue ( avec precision des satues dossiers + id utilisateur )
        string reqGetDossiers = "SELECT [id_dossier],[id_status],[libelle],[name_dossier],[nb_image],[user_index] FROM dbo.VEtatDossierIndexed where [user_index]=($iduserindex) and day(date_index)=('$jour') and MONTH(date_index)=('$mois') and year(date_index)=('$anne') and id_livrable=('$idlivrable') and [id_status]=('$statueChoisie')";
        //requette pour la recuperation des dossier , leurs id et leurs statue pour toutes les dates ( avec precision des satues dossiers + id utilisateur ) 
        string reqGetDossiersAllDates = "SELECT [id_dossier],[id_status],[libelle],[name_dossier],[nb_image],[user_index] FROM dbo.VEtatDossierIndexed where [user_index]=($iduserindex) and id_livrable=('$idlivrable') and [id_status]=('$statueChoisie')";
        //requette pour la recuperation des dossier , leurs id et leurs statue pour toutes les dates ( avec precision des satues dossiers ) 
        string reqGetDossiersAllDatesWithoutAgent = "SELECT [id_dossier],[id_status],[libelle],[name_dossier],[nb_image],[user_index] FROM dbo.VEtatDossierIndexed where id_livrable=('$idlivrable') and [id_status]=('$statueChoisie')";
        //requette pour la recuperation des dossier , leurs id et leurs statue pour toutes les journées mais avec précision du M et anne ( avec precision des satues dossiers + id utilisateur ) 
        string reqGetDossiersAllDatesWithoutDay = "SELECT [id_dossier],[id_status],[libelle],[name_dossier],[nb_image],[user_index] FROM dbo.VEtatDossierIndexed where [user_index]=($iduserindex) and id_livrable=('$idlivrable') and [id_status]=('$statueChoisie') and MONTH(date_index)=('$mois') and year(date_index)=('$anne')";
        
        
        //requette pour recuperer les information des pieces pour un TF selectionner
        string reqGetListePiece = "SELECT [id_vue],[numero_ordre],[id_status],[url],[num_sous_dos],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page] FROM [TB_Vues] where [id_dossier]=('$dossierChoisie') order by numero_ordre ";
        //requette pour recuperer les indexes du dossier
        string reqGetIndexesDossier = "SELECT [numero_origine],[nature_origine],[indice_origine],[indice_sporigine],[numero_titre],[indice_titre],[indice_sptitre] from [TB_Dossier]  where [id_dossier]=('$dossierChoisie') ";
        //requette pour mettre a jour la piece vers statue valider
        string reqUpdateStatuePiece = "UPDATE [TB_Vues] SET [id_status] = ('$statuePiece') WHERE id_vue=('$idPieceChoisie')";
        
        //requette pour mettre à jour statue du dossier , avec la date operation et l'utilisateur
        string reqUpdateStatueDossierVerification = "UPDATE [dbo].[TB_Dossier]  SET [id_status] = ('$statueDossier'),[date_verif] = GETDATE(),[user_verif] = ('$agent_operation')  WHERE (id_dossier=('$idDossierChoisie'))";
        string reqUpdateStatueDossierControle = "UPDATE [dbo].[TB_Dossier]  SET [id_status] = ('$statueDossier'),[date_cont] = GETDATE(),[user_cont] = ('$agent_operation')  WHERE (id_dossier=('$idDossierChoisie'))";

        string reqUpdateStatueDossier = "UPDATE [dbo].[TB_Dossier]  SET [id_status] = ('$statueDossier')  WHERE (id_dossier=('$idDossierChoisie'))";

        //cette requette permet de récuperer la liste des nom pour les pieces
        string reqSelectNamesPieces = "SELECT distinct * FROM [Piece]";
        //cette requette permet de modifier les indexes de la fiche piece
        string reqUpdateFichePiece = "UPDATE [dbo].[TB_Vues] SET [nom_piece] = ('$nom_piece'),[nombre_page] = ('$nombre_page'),[num_page] = ('$num_page')  WHERE [id_vue]=('$id_vue')";
        //cette requette permet de modifier les indexes de la fiche dossier
        string reqUpdateFicheDossier = "UPDATE [dbo].[TB_Dossier] SET [numero_origine] = ('$numero_origine'),[nature_origine] = ('$nature_origine'),[indice_origine] = ('$indice_origine'),[indice_sporigine] = ('$indice_sporigine'),[numero_titre] = ('$numero_titre'),[indice_titre] = ('$indice_titre'),[indice_sptitre] = ('$indice_sptitre') WHERE id_dossier=('$idDossier') ";
        //cette requette permet de modifierles indexes de la fiche dossier
        string reqUpdateFicheSousDossier = "UPDATE [dbo].[TB_Vues] SET [date_depot] = ('$date_depot'),[num_sous_dos] = ('$num_sous_dos'),[formalite] = ('$formalite'),[volume_depot] = ('$volume_depot'),[numero_depot] = ('$numero_depot') where ( [id_vue] =('$id_vue'))";
        //requette prmet de recuperer la liste des formalites
        string reqSelectListeFormalite = "SELECT [LIBELLE_FORMALITE],[LIBELLE_FORMALITE_ARAB] FROM [LISTE_FORMALITES]";

        //requette prmet de recuperer la liste des formalites
        string reqSelectListeFormaliteTable = "SELECT [LIBELLE_FORMALITE],[LIBELLE_FORMALITE_ARAB] FROM [LISTE_FORMALITES]";
        //cette requette permet de récuperer la liste des nom pour les pieces
        string reqSelectNamesPiecesTable = "SELECT distinct [PIECE] FROM [Piece]";

        //requettes historiques
        string reqInsertHistoriqueDossier = "INSERT INTO TB_Historique_Dossier ([user_operation],[date_operation],[id_dossier],[id_status],[id_phase],[id_user],[id_unite],[id_livrable],[name_dossier],[url],[nb_image],[numero_origine],[nature_origine],[indice_origine],[indice_sporigine],[numero_titre],[indice_titre],[indice_sptitre],[date_inject],[date_affect],[date_index],[date_cont],[date_cor],[date_cont_cor],[user_inject],[user_index],[user_cont],[user_cor],[user_cont_cor]) select ('$id_utilisateur'),GETDATE(),[id_dossier],[id_status],[id_phase],[id_user],[id_unite],[id_livrable],[name_dossier],[url],[nb_image],[numero_origine],[nature_origine],[indice_origine],[indice_sporigine],[numero_titre],[indice_titre],[indice_sptitre],[date_inject],[date_affect],[date_index],[date_cont],[date_cor],[date_cont_cor],[user_inject],[user_index],[user_cont],[user_cor],[user_cont_cor] from [TB_Dossier] where id_dossier=('$id_dossier')";
        string reqInsertHistoriqueVue = "INSERT INTO [TB_Historique_Vue] ([user_operation],[date_operation],[id_vue],[bar_code],[id_dossier],[id_status],[url],[numero_ordre],[date_index],[date_modif],[date_cont],[date_cor],[date_cont_cor],[user_index],[user_modif],[user_cont],[user_cor],[user_cont_cor],[num_sous_dos],[categorie],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page]) SELECT ('$id_utilisateur'),getdate(),[id_vue],[bar_code],[id_dossier],[id_status],[url],[numero_ordre],[date_index],[date_modif],[date_cont],[date_cor],[date_cont_cor],[user_index],[user_modif],[user_cont],[user_cor],[user_cont_cor],[num_sous_dos],[categorie],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page] FROM [TB_Vues] where id_vue=('$id_vue')";

        //requettes operations
        string reqOperationDossier = "INSERT INTO [TB_Operation_Dossier] ([user_operation],[date_operation],[id_dossier],[id_status]) VALUES(('$id_utilisateur'),GETDATE(),('$id_dossier'),('$id_status'))";
        string reqOperationDossier_CasAffectation = "INSERT INTO [TB_Operation_Dossier] ([user_operation],[date_operation],[id_dossier],[id_status],[affecter_a]) VALUES(('$id_utilisateur'),GETDATE(),('$id_dossier'),('$id_status'),('$affecter_a'))";
        string reqOperationVue = "INSERT INTO [TB_Operation_Vue] ([user_operation],[date_operation],[id_status],[id_vue]) VALUES (('$id_utilisateur'),GETDATE(),('$id_status'),('$id_vue'))";


        //insert liste déroulabtes
        string reqInsertLigneFormalites = "INSERT INTO [dbo].[LISTE_FORMALITES] ([LIBELLE_FORMALITE],[LIBELLE_FORMALITE_ARAB],[CATEGORIE],[AJOUTER_PAR]) VALUES (('$LIBELLEFORMALITE'),(N'$ARABEFORMALITE'),('$Categorie'),($ajouterpar))";
        string reqInsertligneNamePiece = "INSERT INTO [dbo].[PIECE] ([PIECE],[AJOUTER_PAR]) VALUES (('$namepiece'),($ajouterpar))";

        //selection de la base
        string reqselectbase = " SELECT [id_base],[name_base] FROM [TB_Bases]";
        //get informamtion bases
        string reqGetInformationsBase = "SELECT [name_base],[ip_base],[user_base],[pass_base] FROM [Administration_ANCFCC].[dbo].[TB_Bases] where id_base=('$idbase')";



        //les états
        string reqInsertLigneEtat = "INSERT INTO dbo.etats ($what, id_user, date_action, livrable) VALUES ($value,$id_user,'$date',$livrable)";
        string reqUpdateLigneEtat = "update dbo.etats set $what=$newvalue where id_user=$id_user and date_action='$date' and livrable=$livrable";


        public Boolean initialiser_connection(String chaineconnection)
        {
            this.connection = new SqlConnection(chaineconnection);
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public DataTable authentification(String login, String password)
        {
            DataTable dt = new DataTable();
            try
            {
                string req = reqAuthentification.Replace("$user", login);
                string req2 = req.Replace("$password", password);
                SqlDataAdapter da = new SqlDataAdapter(req2, connectionAdministration);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public Boolean updateFicheSousDossier(string idPiece, string numSousDossier, string formalite, string volumeDepot, string numDepot, string dateDepot)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqUpdateFicheSousDossier.Replace("$date_depot", dateDepot);
                string req2 = req1.Replace("$id_vue", idPiece);
                string req3 = req2.Replace("$num_sous_dos", numSousDossier);
                string req4 = req3.Replace("$formalite", formalite);
                string req5 = req4.Replace("$volume_depot", volumeDepot);
                string reqFinal = req5.Replace("$numero_depot", numDepot);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return condition;
        }

        public Boolean updateFichePieces(String idVue, String namePiece, String nbrPages, String numPages)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();

            try
            {
                string req1 = reqUpdateFichePiece.Replace("$nom_piece", namePiece);
                string req2 = req1.Replace("$nombre_page", nbrPages);
                string req3 = req2.Replace("$num_page", numPages);
                string reqFinal = req3.Replace("$id_vue", idVue);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return condition;
        }
        

        public Boolean updateFicheDossier(string idDossier, string natureOrigine, string numOrigine, string indiceOrigine, string indiceSpOrigine, string numTitre, string indiceTitre, string indiceSpTitre)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();

            try
            {
                string req1 = reqUpdateFicheDossier.Replace("$idDossier", idDossier);
                string req2 = req1.Replace("$nature_origine", natureOrigine);
                string req3 = req2.Replace("$numero_origine", numOrigine);
                string req4 = req3.Replace("$indice_origine", indiceOrigine);
                string req5 = req4.Replace("$indice_sporigine", indiceSpOrigine);
                string req6 = req5.Replace("$numero_titre", numTitre);
                string req7 = req6.Replace("$indice_titre", indiceTitre);
                string reqFinal = req7.Replace("$indice_sptitre", indiceSpTitre);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return condition;
        }

        public Boolean updateStatuePiece(String statue, String idVue)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqUpdateStatuePiece.Replace("$statuePiece", statue);
                string reqFinal = req1.Replace("$idPieceChoisie", idVue);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return condition;
        }
        
        public DataTable getIndexTF(String idTF)
        {
            DataTable dt = new DataTable();
            try
            {
                string req = reqGetIndexesDossier.Replace("$dossierChoisie", idTF);
                SqlDataAdapter da = new SqlDataAdapter(req, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }
        
        public DataTable getListePieceTF(string idTF)
        {
            DataTable dt = new DataTable();
            try
            {
                string req = reqGetListePiece.Replace("$dossierChoisie", idTF);
                SqlDataAdapter da = new SqlDataAdapter(req, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;
        }

        public DataTable getListeMoisAndANNEParLivrable(String idLivrable)
        {
            DataTable dt = new DataTable();
            try
            {
                string reqFinal = reqGetMoisAnneParLivrable.Replace("$idlivrable", idLivrable);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;
        }

        public DataTable getListeAgentsIndexantSurCetteDate(String jour, String mois, String anne, String idLivrable)
        {
            DataTable dt = new DataTable();

            try
            {
                string reqFinal = "";

                if (jour.Equals("all") && mois.Equals("all") && anne.Equals("all"))
                {
                    reqFinal = reqGetListeUsersAllDates.Replace("$idlivrable", idLivrable);
                }
                else if (!mois.Equals("all") && !anne.Equals("all") && jour.Equals("all"))
                {
                    string req1 = reqGetListeUsersAllDatesWithoutDay.Replace("$mois", mois);
                    string req2 = req1.Replace("$anne", anne);
                    reqFinal = req2.Replace("$idlivrable", idLivrable);
                }
                else if (mois.Equals("all") && anne.Equals("all") && !jour.Equals("all"))
                {
                    string req = reqGetListeUsersAllDatesWithoutYearDay.Replace("$jour", jour);
                    reqFinal = req.Replace("$idlivrable", idLivrable);
                }
                else
                {
                    string req = reqGetListeUsers.Replace("$mois", mois);
                    string req2 = req.Replace("$jour", jour);
                    string req3 = req2.Replace("$anne", anne);
                    reqFinal = req3.Replace("$idlivrable", idLivrable);
                }
                
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;

        }

        public DataTable getListeAgentsIndexantSurCetteDate(String jour, String mois, String anne)
        {
            DataTable dt = new DataTable();

            try
            {
                string reqFinal = reqGetListeUsersAllDatesWithoutLivrable;
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;

        }
        
        public DataTable getListeFoldersWhithIDAndStatue(String agentChoisie, String jourChoisie, String moisChoisie, String anneChoisie, String idLivrable, String statueChoisie)
        {
            DataTable dt = new DataTable();
            try
            {
            string reqFinal = "";
            if (jourChoisie.Equals("all") && anneChoisie.Equals("all") && moisChoisie.Equals("all"))
            {
                if(agentChoisie.Equals("all"))
                {
                string req = reqGetDossiersAllDatesWithoutAgent.Replace("$idlivrable", idLivrable);
                reqFinal = req.Replace("$statueChoisie", statueChoisie);
                }
                else
                {
                string req = reqGetDossiersAllDates.Replace("$idlivrable", idLivrable);
                string req1 = req.Replace("$iduserindex", agentChoisie);
                reqFinal = req1.Replace("$statueChoisie", statueChoisie);
                }
            }
            else if (jourChoisie.Equals("all") && !anneChoisie.Equals("all") && !moisChoisie.Equals("all"))
            {
                string req = reqGetDossiersAllDatesWithoutDay.Replace("$mois", moisChoisie);
                string req1 = req.Replace("$anne", anneChoisie);
                string req2 = req1.Replace("$idlivrable", idLivrable);
                string req3 = req2.Replace("$iduserindex", agentChoisie);
                reqFinal = req3.Replace("$statueChoisie", statueChoisie);
            }
            else
            {
                string req = reqGetDossiers.Replace("$mois", moisChoisie);
                string req2 = req.Replace("$jour", jourChoisie);
                string req3 = req2.Replace("$anne", anneChoisie);
                string req4 = req3.Replace("$idlivrable", idLivrable);
                string req5 = req4.Replace("$iduserindex", agentChoisie);
                reqFinal = req5.Replace("$statueChoisie", statueChoisie);
            }            
            
            SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
            da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;        

        }

        public DataTable getListeDesDatesDindexation(String anne,String mois,String idLivrable)
        {
            DataTable dt = new DataTable();
            try
            {
                string reqFinal="";
                if (anne.Equals("all") && mois.Equals("all"))
                {
                    reqFinal = reqGetlisteDateDindexationAllDates.Replace("$idlivrable", idLivrable);
                }
                else
                {
                    string req = reqGetlisteDateDindexation.Replace("$mois", mois);
                    string req1 = req.Replace("$anne", anne);
                    reqFinal = req1.Replace("$idlivrable", idLivrable);
                }
                
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public DataTable getListeLivrable()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(reqGetLivrable, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public Boolean changerStatueDossier(int idagent,int statueDossier,string idDossierChoisie)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string reqFinal="";
                if (statueDossier == 8)
                {
                    string req1 = reqUpdateStatueDossierVerification.Replace("$idDossierChoisie", idDossierChoisie);
                    string req2 = req1.Replace("$agent_operation", idagent.ToString());
                    reqFinal = req2.Replace("$statueDossier", statueDossier.ToString());
                }
                else if (statueDossier == 6 || statueDossier == 7)
                {
                    string req1 = reqUpdateStatueDossierControle.Replace("$idDossierChoisie", idDossierChoisie);
                    string req2 = req1.Replace("$agent_operation", idagent.ToString());
                    reqFinal = req2.Replace("$statueDossier", statueDossier.ToString());
                }
                else
                {
                    string req1 = reqUpdateStatueDossier.Replace("$idDossierChoisie", idDossierChoisie);
                    reqFinal = req1.Replace("$statueDossier", statueDossier.ToString());
                }
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return condition;
        }

        public DataTable getListeNamesPieces()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(reqSelectNamesPieces, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public DataTable chargerListeFormalites()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(reqSelectListeFormalite, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public DataTable GetData(string req)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(req, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public Boolean SetData(string req)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {                
                SqlDataAdapter da = new SqlDataAdapter(req, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return condition;
        }

        public Boolean insert_Historique_Dossier(string idutilisateur, string idDossier)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqInsertHistoriqueDossier.Replace("$id_utilisateur", idutilisateur);
                string reqFinal = req1.Replace("$id_dossier", idDossier);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public Boolean insert_Operation_Dossier(string idutilisateur, string idDossier, string idStatue)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqOperationDossier.Replace("$id_utilisateur", idutilisateur);
                string req2 = req1.Replace("$id_status", idStatue);
                string reqFinal = req2.Replace("$id_dossier", idDossier);

                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public Boolean insert_Operation_Dossier_casAffectation(string idutilisateur,string idUserAffectation, string idDossier, string idStatue)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqOperationDossier_CasAffectation.Replace("$id_utilisateur", idutilisateur);
                string req2 = req1.Replace("$id_status", idStatue);
                string req3 = req2.Replace("$affecter_a", idUserAffectation);
                string reqFinal = req3.Replace("$id_dossier", idDossier);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;

            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public Boolean insert_Operation_Vue(string idutilisateur, string idVue, string idStatue)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqOperationVue.Replace("$id_utilisateur", idutilisateur);
                string req2 = req1.Replace("$id_status", idStatue);
                string reqFinal = req2.Replace("$id_vue", idVue);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public Boolean insert_Historique_Vue(string idutilisateur, string idVue)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqInsertHistoriqueVue.Replace("$id_utilisateur", idutilisateur);
                string reqFinal = req1.Replace("$id_vue", idVue);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public DataTable chargerlisteFormalitesTable()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(reqSelectListeFormaliteTable, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public Boolean insertFormalite(string formalitefr, string formalitear, string categorie, int utilisateur)
        {
            Boolean condition = false;

            DataTable dt = new DataTable();
            try
            {
                string req1 = reqInsertLigneFormalites.Replace("$LIBELLEFORMALITE", formalitefr);
                string req2 = req1.Replace("$ARABEFORMALITE", formalitear);
                string req3 = req2.Replace("$ajouterpar", utilisateur.ToString());
                string reqFinal = req3.Replace("$Categorie", categorie);
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return condition;

        }

        

        public Boolean insertNamePiece(string namepiece, int utilisateur)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqInsertligneNamePiece.Replace("$namepiece", namepiece);
                string reqFinal = req1.Replace("$ajouterpar", utilisateur.ToString());                
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return condition;
        }



        public DataTable getListeNamesPiecesTable()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(reqSelectNamesPiecesTable, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public DataTable chargerlistebases()
        {

            DataTable dt = new DataTable();
            try
            {

                SqlDataAdapter da = new SqlDataAdapter(reqselectbase, connectionAdministration);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }


        public Boolean getinformationsBD(int idbase)
        {
            Boolean condition = true;
            DataTable dt = new DataTable();
            try
            {
                string req = reqGetInformationsBase.Replace("$idbase", idbase.ToString().Trim());
                SqlDataAdapter da = new SqlDataAdapter(req, connectionAdministration);
                da.Fill(dt);
                int x = dt.Rows.Count;
                if (x > 0)
                {
                    string name_base = dt.Rows[0][0].ToString().Trim();
                    string ip_base = dt.Rows[0][1].ToString().Trim();
                    string user_base = dt.Rows[0][2].ToString().Trim();
                    string pass_base = dt.Rows[0][3].ToString().Trim();
                    String chaineconnection = "Data Source=" + ip_base + ";Initial Catalog=" + name_base + ";User ID=" + user_base + ";Password=" + pass_base + "";
                    if (!this.initialiser_connection(chaineconnection))
                    {
                        condition = false;
                    }
                }

            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public Boolean update_etats(int idLivrable, int id_user, string date, string what, string value)
        {
            Boolean condition = false;
            try
            {

                int valeur = 0;
                SqlCommand cmd = new SqlCommand("select COUNT(1) from dbo.etats where id_user= " + id_user + " and [date_action]='" + date + "' and [livrable]=" + idLivrable, connection);
                valeur = int.Parse(cmd.ExecuteScalar().ToString());

                if (valeur == 0)
                {
                    if (this.insertLigneetat(idLivrable, id_user, date, what, value))
                    {
                        condition = true;
                    }
                }
                else
                {

                    SqlCommand cmd2 = new SqlCommand("select isnull(" + what + ",0) from dbo.etats where id_user=" + id_user + " and date_action='" + date + "' and livrable=" + idLivrable, connection);
                    int valeurcolumn = int.Parse(cmd2.ExecuteScalar().ToString());
                    int newvalue = 0;
                    

                    if (Convert.ToInt32(value) == -1)
                    {
                        if (valeurcolumn != 0)
                        newvalue = valeurcolumn -1;
                    }
                    else
                    {
                        newvalue = valeurcolumn + Convert.ToInt32(value);
                    }
                    if (updateLigneEtat(idLivrable, id_user, date, what, newvalue.ToString()))
                    {
                        condition = true;
                    }
                }
            }
            catch (Exception ex)
            {
                condition = false;
                ex.Message.ToString();
            }
            return condition;
        }

        public Boolean updateLigneEtat(int idLivrable, int id_user, string date, string what, string newvalue)
        {
            Boolean condition = false;

            DataTable dt = new DataTable();
            try
            {
                string req1 = reqUpdateLigneEtat.Replace("$what", what);
                string req2 = req1.Replace("$newvalue", newvalue);
                string req3 = req2.Replace("$id_user", id_user.ToString());
                string req4 = req3.Replace("$date", date);
                string reqFinal = req4.Replace("$livrable", idLivrable.ToString());
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return condition;
        }


        public Boolean insertLigneetat(int idLivrable, int id_user, string date, string what, string value)
        {
            Boolean condition = false;

            DataTable dt = new DataTable();
            try
            {
                string req1 = reqInsertLigneEtat.Replace("$what", what);
                string req2 = req1.Replace("$value", value);
                string req3 = req2.Replace("$id_user", id_user.ToString());
                string req4 = req3.Replace("$date", date);
                string reqFinal = req4.Replace("$livrable", idLivrable.ToString());
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connection);
                da.Fill(dt);
                condition = true;
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return condition;
        }

        public DataTable chargerlisteFormalites()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(reqSelectListeFormalite, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

    }
}
