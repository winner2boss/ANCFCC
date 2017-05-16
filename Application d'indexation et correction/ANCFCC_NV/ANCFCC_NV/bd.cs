using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ANCFCC_NV
{
    class bd
    {
        public static SqlConnection connectionAdministration = new SqlConnection(ConfigurationManager.ConnectionStrings["GEDConnectionString"].ToString());
        public SqlConnection connection;

        string reqGetIdBase = "SELECT [id_base] FROM [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where id_user=('$iduser')";
        string reqGetInformationsBase = "SELECT [name_base],[ip_base],[user_base],[pass_base] FROM [Administration_ANCFCC].[dbo].[TB_Bases] where id_base=('$idbase')";
      
        string reqAuthentification = "SELECT [id_user] FROM [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where login=('$user') and pass=('$password')";
        string reqGetLivrable = "SELECT [id_livrable],[nom_livrable] FROM [TB_Livrable] where etat=0";

        //requette recuperation des dossiers affectes avec livrable
        string reqGetFolders = "SELECT [id_dossier],[name_dossier],[id_status],[nb_image] FROM dbo.VEtatDossierIndexed where (user_affectation=('$iduser') and id_livrable=('$idlivrable') and  id_status=1 ) or ( user_affectation=('$iduser') and id_livrable=('$idlivrable') and id_status=2 ) or ( user_affectation=('$iduser') and id_livrable=('$idlivrable') and id_status=4 )";
        //requette recuperation des dossiers affectes sans livrable
        string reqGetFoldersWithoutLivrble = "SELECT [id_dossier],[name_dossier],[id_status],[nb_image] FROM dbo.VEtatDossierIndexed where (user_affectation=('$iduser') and  id_status=1 ) or ( user_affectation=('$iduser') and id_status=2 ) or ( user_affectation=('$iduser') and id_status=4)";

        //requette recuperation des dossiers affectes avec livrable pour correction
        string reqGetFoldersCorrection = "SELECT [id_dossier],[name_dossier],[id_status],[nb_image] FROM dbo.VEtatDossierIndexed where (user_cor=('$iduser') and id_livrable=('$idlivrable') and  id_status=14) or ( user_cor=('$iduser') and id_livrable=('$idlivrable') and id_status=14 ) or ( user_cor=('$iduser') and id_livrable=('$idlivrable') and id_status=14 )";
        //requette recuperation des dossiers affectes avec livrable pour correction
        string reqGetFoldersCorrectionWithoutlivrable = "SELECT [id_dossier],[name_dossier],[id_status],[nb_image] FROM dbo.VEtatDossierIndexed where (user_cor=('$iduser') and  id_status=14) or ( user_cor=('$iduser')  and id_status=14 ) or ( user_cor=('$iduser') and id_status=14 )";


        //requette pour recuperer les information des pieces pour un TF selectionner
        string reqGetListePiece = "SELECT [id_dossier],[id_vue],[numero_ordre],[id_status],[url],[num_sous_dos],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page] FROM [TB_Vues] where [id_dossier]=('$dossierChoisie') order by numero_ordre ";
        // requette pour recuperer les information du TF en utilisant id tf
        string reqGetInfoTF = "SELECT *  FROM [dbo].[TB_Dossier] where id_dossier=($iddossier)";
        //les listes à chargers
        //cette requette permet de récuperer la liste des nom pour les pieces
        string reqSelectNamesPieces = "SELECT distinct * FROM [Piece]";
        //requette prmet de recuperer la liste des formalites
        string reqSelectListeFormalite = "SELECT [LIBELLE_FORMALITE],[LIBELLE_FORMALITE_ARAB] FROM [LISTE_FORMALITES]";
        //requette permet de mettre à jour le statue dossier
        string reqUpdateStatueDossier = "UPDATE [dbo].[TB_Dossier]  SET [id_status] = ('$statueDossier')  WHERE (id_dossier=('$idDossierChoisie'))";
        //cette requete permet d'inserer les indexes du dossiers et le changement de son statue vers indexés
        string reqInsertIndexesFolder = "UPDATE [TB_Dossier] SET [id_status]=($idstatue),[numero_origine]=('$numero_origine'),[nature_origine]=('$nature_origine'),[indice_origine]=('$indice_origine'),[indice_sporigine]=('$indice_sporigine'),[numero_titre]=('$numero_titre'),[indice_titre]=('$indice_titre'),[indice_sptitre]=('$indice_sptitre'),[date_index]=GETDATE(),[user_index]=($user_index)  WHERE id_dossier=('$id_dossier') ";
        //cette requette permet de renitialiser les indexes de la piece
        string reqRenitialiserIndexesPiece = "UPDATE [dbo].[TB_Vues] SET [id_status] = 1,[user_index] = null,[date_index] = null,[num_sous_dos] = null,[categorie] = null,[formalite] = null,[volume_depot] = null,[numero_depot] = null,[date_depot] = null,[nom_piece] = null,[nombre_page] = null,[num_page] = null WHERE id_vue=($idvue)";
        //string reqRenitialiserIndexesPiece = "UPDATE [dbo].[TB_Vues] SET [num_sous_dos] = null,[categorie] = null,[formalite] = null,[volume_depot] = null,[numero_depot] = null,[date_depot] = null,[nom_piece] = null,[nombre_page] = null,[num_page] = null WHERE id_vue=($idvue)";
        
        //requettes historiques
        string reqInsertHistoriqueDossier = "INSERT INTO TB_Historique_Dossier ([user_operation],[date_operation],[id_dossier],[id_status],[id_phase],[id_user],[id_unite],[id_livrable],[name_dossier],[url],[nb_image],[numero_origine],[nature_origine],[indice_origine],[indice_sporigine],[numero_titre],[indice_titre],[indice_sptitre],[date_inject],[date_affect],[date_index],[date_cont],[date_cor],[date_cont_cor],[user_inject],[user_index],[user_cont],[user_cor],[user_cont_cor]) select ('$id_utilisateur'),GETDATE(),[id_dossier],[id_status],[id_phase],[id_user],[id_unite],[id_livrable],[name_dossier],[url],[nb_image],[numero_origine],[nature_origine],[indice_origine],[indice_sporigine],[numero_titre],[indice_titre],[indice_sptitre],[date_inject],[date_affect],[date_index],[date_cont],[date_cor],[date_cont_cor],[user_inject],[user_index],[user_cont],[user_cor],[user_cont_cor] from [TB_Dossier] where id_dossier=('$id_dossier')";
        string reqInsertHistoriqueVue = "INSERT INTO [TB_Historique_Vue] ([user_operation],[date_operation],[id_vue],[bar_code],[id_dossier],[id_status],[url],[numero_ordre],[date_index],[date_modif],[date_cont],[date_cor],[date_cont_cor],[user_index],[user_modif],[user_cont],[user_cor],[user_cont_cor],[num_sous_dos],[categorie],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page]) SELECT ('$id_utilisateur'),getdate(),[id_vue],[bar_code],[id_dossier],[id_status],[url],[numero_ordre],[date_index],[date_modif],[date_cont],[date_cor],[date_cont_cor],[user_index],[user_modif],[user_cont],[user_cor],[user_cont_cor],[num_sous_dos],[categorie],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page] FROM [TB_Vues] where id_vue=('$id_vue')";
        
        //requettes operations
        string reqOperationDossier = "INSERT INTO [TB_Operation_Dossier] ([user_operation],[date_operation],[id_dossier],[id_status]) VALUES(('$id_utilisateur'),GETDATE(),('$id_dossier'),('$id_status'))";
        string reqOperationVue = "INSERT INTO [TB_Operation_Vue] ([user_operation],[date_operation],[id_status],[id_vue]) VALUES (('$id_utilisateur'),GETDATE(),('$id_status'),('$id_vue'))";

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

        public int getIdBase(int idUtilisateur)
        {
            int i = 0;
            DataTable dt = new DataTable(); 
            try
            {
                string req = reqGetIdBase.Replace("$iduser", idUtilisateur.ToString().Trim());
                SqlDataAdapter da = new SqlDataAdapter(req, connectionAdministration);
                da.Fill(dt);
                int x = dt.Rows.Count;
                if (x > 0)
                {
                    i = Int32.Parse(dt.Rows[0][0].ToString());
                }
                
            }
            catch (Exception ex)
            {
                i = 0;
                ex.Message.ToString();
            }

            return i;
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
                    if(!this.initialiser_connection(chaineconnection))
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
        
        public Boolean update_etats(int idLivrable, int id_user, string date, string what, string value)
        {
            Boolean condition = false;
            try
            {

            int valeur = 0;
            SqlCommand cmd = new SqlCommand("select COUNT(1) from dbo.etats where id_user= " + id_user + " and [date_action]='" + date + "' and [livrable]="+idLivrable, connection);
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
                
                SqlCommand cmd2 = new SqlCommand("select isnull(" + what + ",0) from dbo.etats where id_user=" + id_user + " and date_action='" + date + "' and livrable="+idLivrable, connection);
                int valeurcolumn = int.Parse(cmd2.ExecuteScalar().ToString());
                int newvalue = valeurcolumn + Convert.ToInt32(value);                
                if(updateLigneEtat(idLivrable,id_user,date,what,newvalue.ToString()))
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

        public Boolean renitialiserPiece(string idPiece)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                
                string reqFinal = reqRenitialiserIndexesPiece.Replace("$idvue", idPiece);                
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
        
        public Boolean changerStatueDossier(string statueDossier, string idDossierChoisie)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string reqFinal = "";
                string req1 = reqUpdateStatueDossier.Replace("$idDossierChoisie", idDossierChoisie);
                reqFinal = req1.Replace("$statueDossier", statueDossier.ToString());
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

        public DataTable chargerFicheDossier(String id_tf)
        {
            DataTable dt = new DataTable();
            try
            {
                string req = reqGetInfoTF.Replace("$iddossier", id_tf.ToString());
                SqlDataAdapter da = new SqlDataAdapter(req, connection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
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

        public DataTable getFolders(int iduser, int idlivrable, Boolean correction)
        {
            DataTable dt = new DataTable();
            try
            {
                if (correction)
                {
                    if (idlivrable != 0)
                    {
                        string req = reqGetFoldersCorrection.Replace("$iduser", iduser.ToString());
                        string req2 = req.Replace("$idlivrable", idlivrable.ToString());
                        SqlDataAdapter da = new SqlDataAdapter(req2, connection);
                        da.Fill(dt);
                    }
                    else
                    {
                        string req = reqGetFoldersCorrectionWithoutlivrable.Replace("$iduser", iduser.ToString());
                        SqlDataAdapter da = new SqlDataAdapter(req, connection);
                        da.Fill(dt);
                    }
                }
                else
                {
                    if (idlivrable != 0)
                    {
                        string req = reqGetFolders.Replace("$iduser", iduser.ToString());
                        string req2 = req.Replace("$idlivrable", idlivrable.ToString());
                        SqlDataAdapter da = new SqlDataAdapter(req2, connection);
                        da.Fill(dt);
                    }
                    else
                    {
                        string req = reqGetFoldersWithoutLivrble.Replace("$iduser", iduser.ToString());
                        SqlDataAdapter da = new SqlDataAdapter(req, connection);
                        da.Fill(dt);
                    }
                }
               
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
        
        public Boolean updateTFinBD(String statue,String indice_sporigine,String numero_origine, String nature_origine, String indice_origine, String numero_titre, String indice_titre, String indice_sptitre, int idUser, String id_dossier)
        {
            Boolean condition = false;
            DataTable dt = new DataTable();
            try
            {
                string req1 = reqInsertIndexesFolder.Replace("$numero_origine", numero_origine);
                string req2 = req1.Replace("$nature_origine", nature_origine);
                string req3 = req2.Replace("$indice_origine", indice_origine);
                string req4 = req3.Replace("$numero_titre", numero_titre);
                string req5 = req4.Replace("$indice_titre", indice_titre);
                string req6 = req5.Replace("$indice_sptitre", indice_sptitre);
                string req7 = req6.Replace("$user_index", "'"+idUser.ToString().Trim()+"'");

                if (statue == "4")
                {
                    req7 = req6.Replace("$user_index", "null");
                }
                
                string req8 = req7.Replace("$idstatue", statue.ToString());
                string req9 = req8.Replace("$indice_sporigine", indice_sporigine); 
                string reqFinal = req9.Replace("$id_dossier", id_dossier);
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
                condition = false;
                ex.Message.ToString();
            }

            return condition;
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
        
    }
}
