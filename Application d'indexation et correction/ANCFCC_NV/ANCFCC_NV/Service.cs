using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;

namespace ANCFCC_NV
{
    class Service
    {
        static bd baseDonne = new bd();

        public int authentification(String username, String password)
        {
            int i = 0;
            DataTable dt = baseDonne.authentification(username, password);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                i = Int32.Parse(dt.Rows[0][0].ToString());                
            }
            return i;
        }


        public string getLoginAgent(int idagent)
        {
            string login = "";
            DataTable dt = baseDonne.GetData("SELECT [login] FROM [Administration_ANCFCC].[dbo].[TB_Utilisateurs]  where id_user=" + idagent + "");
            int x = dt.Rows.Count;
            if (x > 0)
            {
                login = dt.Rows[0][0].ToString().Trim();
                
            }
            return login;

        }

        public Boolean chargerLabase(int idutilisateur)
        {
            Boolean condition = false;
            int idbase = baseDonne.getIdBase(idutilisateur);
            if ( idbase != 0)
            {
                if(baseDonne.getinformationsBD(idbase))
                {
                condition = true;
                }
                else
                {
                    condition = false;
                }
                
            }
            return condition;            
        }

        public DataTable getListeLivrable()
        {
            DataTable dt = baseDonne.getListeLivrable();
            return dt;
        }

        public DataTable chargerListeNomPieces()
        {
            DataTable dt = baseDonne.getListeNamesPieces();
            return dt;
        }
        
       
        public DataTable chargerFicheDossier(string id_tf)
        {
            DataTable dt = baseDonne.chargerFicheDossier(id_tf);
            return dt;
        }

        public DataTable chargerlisteFormalites()
        {
            DataTable dt = baseDonne.chargerlisteFormalites();
            return dt;
        }

        public static ArrayList chargerlisteNatureOrigine()
        {
            ArrayList listeNatureOrigine = new ArrayList();
            listeNatureOrigine.Add("T");
            listeNatureOrigine.Add("R");
            return listeNatureOrigine;
        }

        public static DataTable chargerlisteIndiceOrigineTable()
        {
            DataTable dt = baseDonne.GetData("SELECT * FROM [dbo].[TB_indice_origine]");

            return dt;
        }

        public static DataTable chargerlisteIndiceTitreTable()
        {
            DataTable dt = baseDonne.GetData("SELECT * FROM [dbo].[TB_indice_titre]");

            return dt;
        }

        public static DataTable chargerlisteCategorie()
        {
            DataTable dt = baseDonne.GetData("SELECT distinct CATEGORIE FROM [dbo].[LISTE_FORMALITES]");

            return dt;
        }

        public DataTable chargerlisteFormalitesParCategorie(string categorie)
        {
            DataTable dt = baseDonne.GetData("SELECT LIBELLE_FORMALITE FROM [dbo].[LISTE_FORMALITES] where CATEGORIE = '" + categorie + "'");
            return dt;
        }

        public static ArrayList chargerListeDesjours()
        {
            ArrayList listeDesjours = new ArrayList();
            for (int i = 1; i < 32; i++)
            {
                if (i < 10)
                {
                    string value = "0" + i;
                    listeDesjours.Add(value);
                }
                else
                {
                    listeDesjours.Add(i.ToString());
                }
            }
            return listeDesjours;
        }

        public static ArrayList chargerListeDesmois()
        {
            ArrayList listeDesMois = new ArrayList();
            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    string value = "0" + i;
                    listeDesMois.Add(value);
                }
                else
                {
                    listeDesMois.Add(i.ToString());
                }

            }
            return listeDesMois;
        }

        public static ArrayList chargerListeDesAnnes()
        {
            ArrayList listeDesAnne = new ArrayList();
            for (int i = 1800; i < 2020; i++)
            {

                listeDesAnne.Add(i.ToString());

            }
            return listeDesAnne;
        }

        public DataTable getFolders(int iduser, int idlivrable, Boolean correction)
        {
            DataTable dt = baseDonne.getFolders(iduser, idlivrable, correction);
            return dt;
        }

        public DataTable getListePieceTF(String idTF)
        {
            DataTable dt = baseDonne.getListePieceTF(idTF);

            return dt;
        }

        public Stream pathToStream(String url)
        {
            Stream fs = File.OpenRead(@url);

            return fs;
        }

        public Boolean maj_nbrfolder(string iddossier)
        {
            if (baseDonne.SetData("update dbo.TB_Dossier set nb_image=(select COUNT(*) from dbo.TB_Vues where [id_dossier]=" + iddossier + ") where [id_dossier]=" + iddossier + ""))
            {
                    return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean dupliquer_vue(string idvue_adupliquer,string idfirstvue)
        {
            //la requette permet de dupliquer la vue
            if (baseDonne.SetData("INSERT INTO [TB_Vues] ([numero_ordre],[bar_code],[id_dossier],[id_status],[url],[date_index],[date_modif],[date_cont],[date_cor],[date_cont_cor],[user_index],[user_modif],[user_cont],[user_cor],[user_cont_cor],[num_sous_dos],[categorie],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page])  select " + idfirstvue + ",[bar_code],[id_dossier],[id_status],[url],[date_index],[date_modif],[date_cont],[date_cor],[date_cont_cor],[user_index],[user_modif],[user_cont],[user_cor],[user_cont_cor],[num_sous_dos],[categorie],[formalite],[volume_depot],[numero_depot],[date_depot],[nom_piece],[nombre_page],[num_page] from dbo.TB_Vues where id_vue=" + idvue_adupliquer + ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean renitialiser_pieces(DataTable listePieces)
        {
            Boolean condition = true;

            foreach (DataRow row in listePieces.Rows)
            {
                string id_vue = row["id_vue"].ToString().Trim();
                if (!baseDonne.renitialiserPiece(id_vue))
                {
                    condition = false;
                    return condition;
                }
            }

            return condition;
        }

        public Boolean insertPices_inBD_instance(int idutilisateur, DataTable listePieces)
        {

            Boolean condition = true;

            foreach (DataRow row in listePieces.Rows)
            {
                string nom_piece = row["nom_piece"].ToString().Trim();
                string id_vue = row["id_vue"].ToString().Trim();
                string id_statue = row["id_status"].ToString().Trim();
                if (id_statue != "1" && nom_piece!="")
                {
                    string num_ordre = row["numero_ordre"].ToString().Trim();
                    string num_sous_dos = row["num_sous_dos"].ToString().Trim();
                    string formalite = row["formalite"].ToString().Trim();
                    string volume_depot = row["volume_depot"].ToString().Trim();
                    string numero_depot = row["numero_depot"].ToString().Trim();
                    string date_depot = row["date_depot"].ToString().Trim();
                    string nombre_page = row["nombre_page"].ToString().Trim();
                    string num_page = row["num_page"].ToString().Trim();

                    
                    if (id_statue == "5")
                    {
                        nom_piece = "Image supprimer";
                        num_sous_dos = "0";
                        formalite = "";
                        volume_depot = "0";
                        numero_depot = "0";
                        date_depot = "01/01/1990";
                        nombre_page = "0";
                        num_page = "0";
                    }     

                    string req = "UPDATE [TB_Vues] SET [id_status] = " + id_statue + ",[numero_ordre] ='" + num_ordre + "' ,[date_index] = GETDATE(),[user_index] = " + idutilisateur + ",[num_sous_dos] = " + num_sous_dos + "";
                    string req2 = ",[formalite] = '" + formalite + "',[volume_depot] = " + volume_depot + ",[numero_depot] = " + numero_depot + ",[date_depot] = '" + date_depot + "',[nom_piece] = '" + nom_piece + "'";
                    string req3 = ",[nombre_page] = " + nombre_page + ",[num_page] = " + num_page + " WHERE ([id_vue]= " + id_vue + ")";
                    string reqFinal = req + req2 + req3;

                    if (!baseDonne.SetData(reqFinal))
                    {
                        condition = false;
                        return condition;
                    }

                }
                else
                {
                    if (!baseDonne.renitialiserPiece(id_vue))
                    {
                        condition = false;
                        return condition;
                    }
                }
            }
            return condition;

        }

        public Boolean insertPices_inBD(int idutilisateur,DataTable listePieces)
        {
            Boolean condition = true;

            //update ligne par ligne
            foreach (DataRow row in listePieces.Rows)
            {
                    string nom_piece = row["nom_piece"].ToString().Trim();                
                    string num_ordre = row["numero_ordre"].ToString().Trim();
                    string num_sous_dos = row["num_sous_dos"].ToString().Trim();
                    string formalite = row["formalite"].ToString().Trim();
                    string volume_depot = row["volume_depot"].ToString().Trim();
                    string numero_depot = row["numero_depot"].ToString().Trim();
                    string date_depot = row["date_depot"].ToString().Trim();
                    string nombre_page = row["nombre_page"].ToString().Trim();
                    string num_page = row["num_page"].ToString().Trim();
                    string id_statue = row["id_status"].ToString().Trim();

                    if (id_statue == "5")
                    {
                        nom_piece = "Image supprimer";
                        num_sous_dos = "0";
                        formalite = "";
                        volume_depot = "0";
                        numero_depot = "0";
                        date_depot = "01/01/1990";
                        nombre_page = "0";
                        num_page = "0";
                    }

                    string id_vue = row["id_vue"].ToString().Trim();

                    string req = "UPDATE [TB_Vues] SET [id_status] = " + id_statue + ",[numero_ordre] ='" + num_ordre + "' ,[date_index] = GETDATE(),[user_index] = " + idutilisateur + ",[num_sous_dos] = '" + num_sous_dos + "'";
                    string req2 = ",[formalite] = '" + formalite + "',[volume_depot] = " + volume_depot + ",[numero_depot] = " + numero_depot + ",[date_depot] = '" + date_depot + "',[nom_piece] = '" + nom_piece + "'";
                    string req3 = ",[nombre_page] = " + nombre_page + ",[num_page] = " + num_page + " WHERE ([id_vue]= " + id_vue + ")";
                    string reqFinal = req + req2 + req3;

                    if (!baseDonne.SetData(reqFinal))
                    {
                        condition = false;
                        return condition;
                    }
                    else
                    {
                        //historiser le document
                        if (!baseDonne.insert_Historique_Vue(idutilisateur.ToString(), id_vue))
                        {
                            condition = false;
                            return condition;
                        }
                        else
                        {
                            //enregistrer operation vue
                            if (!baseDonne.insert_Operation_Vue(idutilisateur.ToString(), id_vue, id_statue))
                            {
                                condition = false;
                                return condition;
                            }
                        }
                    }
                
            }

            return condition;
        }

        public Boolean updateTFinBD(String statue,DossierF dossierF, int idUser, String id_dossier)
        {
            Boolean condition = false;
            if (dossierF.indice_orgine != null && dossierF.indice_orgine !="")
            {
                if (baseDonne.updateTFinBD(statue,dossierF.indice_special_orgine, dossierF.numero_Orgine, dossierF.nature_Orgine, dossierF.indice_orgine, dossierF.numero_Titre, dossierF.indice_titre, dossierF.indice_special_titre, idUser, id_dossier))
                {
                    condition = true;
                }
            }
            else
            {
                if (baseDonne.changerStatueDossier(statue, id_dossier))
                {
                    condition = true;
                }
            }
            return condition;
        }

        public Boolean changerStatueDossier(String statue, String id_dossier)
        {
            Boolean condition = false;
            if (baseDonne.changerStatueDossier(statue, id_dossier))
            {
                condition = true;
            }
            return condition;
        }

        public Boolean insert_Historique_Dossier(int idutilisateur, String id_dossier)
        {
            Boolean condition = true;
            if (!baseDonne.insert_Historique_Dossier(idutilisateur.ToString(),id_dossier))
            {
                condition = false;
            }
            return condition;
        }

        public Boolean insert_Operation_Dossier(int idutilisateur, string idDossier, string idStatue)
        {
            Boolean condition = true;
            if (!baseDonne.insert_Operation_Dossier(idutilisateur.ToString(), idDossier, idStatue))
            {
                condition = false;
            }
            return condition;
        }

        public DataTable getDateSQL()
        {
            string requette = "select MONTH(GETDATE()) as mois ,YEAR(GETDATE()) as anne,DAY(GETDATE()) as jour";
            DataTable dt = baseDonne.GetData(requette);
            return dt;
        }

        public int getProductivite(int iduser, string date ,string col)
        {

            string requette = "select sum(" + col + ") from [dbo].[etats] where [id_user]=" + iduser + " and [date_action]='" + date + "'";
            
            int i = 0;
            DataTable dt = baseDonne.GetData(requette);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                string variable = dt.Rows[0][0].ToString().Trim();
                if (variable == "")
                {
                    i = 0;
                }
                else
                {
                    i = Int32.Parse(dt.Rows[0][0].ToString().Trim());
                }
            }
            return i;

        }

        public Boolean update_etats(int idLivrable, int id_user, string date, string what, string value)
        {
            Boolean condition = false;
            if (baseDonne.update_etats(idLivrable, id_user, date, what, value))
            {
                condition =  true;
            }

            return condition;
        }

        public string getdate()
        {
            string date = "";
            DataTable dt = baseDonne.GetData("select convert(varchar(10),getdate(),103)");
            date = dt.Rows[0][0].ToString();
            return date;
        }

        public Boolean renitialiser_ordre(string id_dossier)
        {
            if (baseDonne.SetData("EXEC dbo.renitialiser_tf @Id_dossier = " + id_dossier + ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
