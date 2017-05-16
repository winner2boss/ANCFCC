using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;

namespace Consutation_Controle_Validation
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

        //public ArrayList getListeDesDatesDindexation(String anne,String mois, String idLivrable)
        //{
        //    ArrayList listeDates = new ArrayList();
        //    DataTable dt = baseDonne.getListeDesDatesDindexation(anne,mois, idLivrable);
        //    int x = dt.Rows.Count;
        //    if (x > 0)
        //    {
        //        for (int i = 0; i < x; i++)
        //        {
        //            listeDates.Add(dt.Rows[i][0].ToString());
        //        }
        //    }
        //    return listeDates;
        //}

        public DataTable getStatusDossier()
        {
            DataTable listeStatusDossier = new DataTable();
            listeStatusDossier = baseDonne.GetData("SELECT [id_status] ,[libelle] FROM [Administration_ANCFCC].[dbo].[TB_Status] where type_objet=1 order by id_status");    
            return listeStatusDossier;
        }

        public DataTable getListeAgents(string idlivrable,string operation)
        {
            DataTable listeAgents = new DataTable();
            if (operation == "indexation")
            {
                listeAgents = baseDonne.GetData("select [id_user],[login],[matricule] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier] where id_livrable=('" + idlivrable + "'))");
            }
            else
            {
                listeAgents = baseDonne.GetData("select [id_user],[login],[matricule] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs]");
            }
            return listeAgents;
        }

        public DataTable getListeAgents(string operation)
        {
            DataTable listeAgents = new DataTable();
            if (operation == "indexation")
            {
                listeAgents = baseDonne.GetData("select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where [id_user] in (SELECT distinct [user_index] FROM [TB_Dossier])");
            }
            else
            {
                listeAgents = baseDonne.GetData("select [id_user],[login] from [Administration_ANCFCC].[dbo].[TB_Utilisateurs] where actif=1");
            }
            return listeAgents;
        }

        public DataTable getProd_Dates(string idlivrable)
        {
            DataTable listeProd_Dates = new DataTable();
            listeProd_Dates = baseDonne.GetData("SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where id_livrable=('"+idlivrable+"') and date_index IS NOT NULL order by CAST(date_index AS date)");
            return listeProd_Dates;
        }

        public DataTable getProd_Dates()
        {
            DataTable listeProd_Dates = new DataTable();
            listeProd_Dates = baseDonne.GetData("SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where date_index IS NOT NULL order by CAST(date_index AS date)");
            return listeProd_Dates;
        }

        public DataTable getProd_Dates_Sup(string dateCHoisie)
        {
            DataTable listeProd_Dates = new DataTable();
            listeProd_Dates = baseDonne.GetData("SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where  date_index IS NOT NULL and date_index > '" + dateCHoisie + "' order by CAST(date_index AS date)");
            return listeProd_Dates;

        }

        public DataTable getProd_Dates_Inf(string dateCHoisie)
        {
            DataTable listeProd_Dates = new DataTable();
            listeProd_Dates = baseDonne.GetData("SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where date_index IS NOT NULL and date_index < '" + dateCHoisie + "' order by CAST(date_index AS date)");
            return listeProd_Dates;

        }
        
        public DataTable getProd_Dates_Sup(string idlivrable,string dateCHoisie)
        {
            DataTable listeProd_Dates = new DataTable();
            listeProd_Dates = baseDonne.GetData("SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where id_livrable=('" + idlivrable + "') and date_index IS NOT NULL and date_index > '" + dateCHoisie + "' order by CAST(date_index AS date)");
            return listeProd_Dates;

        }

        public DataTable getProd_Dates_Inf(string idlivrable, string dateCHoisie)
        {
            DataTable listeProd_Dates = new DataTable();
            listeProd_Dates = baseDonne.GetData("SELECT distinct CAST(date_index AS date) FROM [TB_Dossier] where id_livrable=('" + idlivrable + "') and date_index IS NOT NULL and date_index < '" + dateCHoisie + "' order by CAST(date_index AS date)");
            return listeProd_Dates;

        }

        public Dictionary<string, string> getListeAgentsIndexantSurCetteDate(String jour, String mois, String anne, String idLivrable)
        {

            Dictionary<string, string> listeAgents = new Dictionary<string, string>();


            DataTable dt = baseDonne.getListeAgentsIndexantSurCetteDate(jour, mois, anne, idLivrable);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    listeAgents.Add(dt.Rows[i][1].ToString().Trim(), dt.Rows[i][0].ToString().Trim());
                }

            }

            return listeAgents;
        }

        public Dictionary<string, string> getListeAgentsIndexantSurCetteDate(String jour, String mois, String anne)
        {

            Dictionary<string, string> listeAgents = new Dictionary<string, string>();


            DataTable dt = baseDonne.getListeAgentsIndexantSurCetteDate(jour, mois, anne);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    listeAgents.Add(dt.Rows[i][1].ToString().Trim(), dt.Rows[i][0].ToString().Trim());
                }

            }

            return listeAgents;
        }
        
        public Dictionary<string, string> getListeLivrable()
        {
            Dictionary<string, string> livrableParIdetname = new Dictionary<string, string>();
            DataTable dt = baseDonne.getListeLivrable();
            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    livrableParIdetname.Add(dt.Rows[i][1].ToString().Trim(), dt.Rows[i][0].ToString().Trim());
                }
            }
            return livrableParIdetname;
        }

        public ArrayList getValuesOfDictionnary(Dictionary<string, string> dictionnary)
        {
            ArrayList listeValues = new ArrayList();
            ArrayList listeKeys = new ArrayList();

            //récuperation des index pieces par dossier 
            Dictionary<string, string>.KeyCollection keys = dictionnary.Keys;
            foreach (string key in keys)
            {
                listeKeys.Add(key);
            }

            foreach (string key in listeKeys)
            {
                listeValues.Add(dictionnary[key]);
            }

            return listeValues;
        }

        public ArrayList getKeysOfDictionnary(Dictionary<string, string> dictionnary)
        {
            ArrayList listeKeys = new ArrayList();

            //récuperation des index pieces par dossier 
            Dictionary<string, string>.KeyCollection keys = dictionnary.Keys;

            foreach (string key in keys)
            {
                listeKeys.Add(key);
            }

            return listeKeys;
        }

        public string getKey(string Value, Dictionary<string, string> dictio)
        {
            if (dictio.ContainsValue(Value))
            {
                var ListValueData = new List<string>();
                var ListKeyData = new List<string>();
                var Values = dictio.Values;
                var Keys = dictio.Keys;
                foreach (var item in Values)
                {
                    ListValueData.Add(item);
                }
                var ValueIndex = ListValueData.IndexOf(Value);

                foreach (var item in Keys)
                {
                    ListKeyData.Add(item);
                }
                return ListKeyData[ValueIndex];

            }
            return string.Empty;
        }

        //public DataTable getListeFolder(String agentChoisie, String jourChoisie, String moisChoisie, String anneChoisie, String idLivrable, String statueChoisie)
        public DataTable getListeFolder(String req)
        {
            DataTable dt = baseDonne.GetData(req);            
            return dt;
        }

        //public Dictionary<string, string> getListeFoldersWhithIDAndStatue(String agentChoisie, String jourChoisie, String moisChoisie, String anneChoisie, String idLivrable, String statueChoisie)
        //{
        //    DataTable dt = baseDonne.getListeFoldersWhithIDAndStatue(agentChoisie, jourChoisie, moisChoisie, anneChoisie, idLivrable, statueChoisie);
        //    Dictionary<string, string> listeDossierByID = new Dictionary<string, string>();

        //    int x = dt.Rows.Count;
        //    if (x > 0)
        //    {
        //        for (int i = 0; i < x; i++)
        //        {
        //            String id = dt.Rows[i][0].ToString().Trim();
        //            String nameDossier=dt.Rows[i][1].ToString().Trim();
        //            listeDossierByID.Add(id, nameDossier);
        //        }
        //    }
        //    return listeDossierByID;
        //}

        public DataTable getListePieceTF(String idTF)
        {
            DataTable dt = baseDonne.getListePieceTF(idTF);
            DataTable table = new DataTable();
            table.Columns.Add("id_vue", typeof(int));
            table.Columns.Add("numero_ordre", typeof(int));
            table.Columns.Add("id_status", typeof(string));
            table.Columns.Add("url", typeof(string));
            table.Columns.Add("num_sous_dos", typeof(string));
            table.Columns.Add("formalite", typeof(string));
            table.Columns.Add("volume_depot", typeof(string));
            table.Columns.Add("numero_depot", typeof(string));
            table.Columns.Add("date_depot", typeof(string));
            table.Columns.Add("nom_piece", typeof(string));
            table.Columns.Add("nombre_page", typeof(string));
            table.Columns.Add("num_page", typeof(string));
            
            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    
                    String id_vue = dt.Rows[i][0].ToString().Trim();
                    String numero_ordre = dt.Rows[i][1].ToString().Trim();                    
                    String id_status = dt.Rows[i][2].ToString().Trim();
                    String url = dt.Rows[i][3].ToString().Trim();
                    String num_sous_dos = dt.Rows[i][4].ToString().Trim();
                    String formalite = dt.Rows[i][5].ToString().Trim();
                    String volume_depot = dt.Rows[i][6].ToString().Trim();
                    String numero_depot = dt.Rows[i][7].ToString().Trim();
                    String date_depot = dt.Rows[i][8].ToString().Trim();
                    String nom_piece = dt.Rows[i][9].ToString().Trim();
                    String nombre_page = dt.Rows[i][10].ToString().Trim();
                    String num_page = dt.Rows[i][11].ToString().Trim();
                     table.Rows.Add(id_vue,numero_ordre, id_status, url, num_sous_dos, formalite, volume_depot, numero_depot,date_depot, nom_piece, nombre_page, num_page);
                }
            }

            Dictionary<string, string> dictionnaire = new Dictionary<string, string>();
            String idFirstOrdre = "";
            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                if (count == 0)
                {
                    idFirstOrdre = row["numero_ordre"].ToString();
                }
                dictionnaire.Add(row["id_vue"].ToString(),row["numero_ordre"].ToString());
                count++;
            }
            dictionnaire = this.remettreEnOrdre(dictionnaire, idFirstOrdre);
            ArrayList idFromDictionnaire = getKeysOfDictionnary(dictionnaire);


            DataTable table2 = new DataTable();
            table2.Columns.Add("id_vue", typeof(int));
            table2.Columns.Add("numero_ordre", typeof(int));
            table2.Columns.Add("id_status", typeof(string));
            table2.Columns.Add("url", typeof(string));
            table2.Columns.Add("num_sous_dos", typeof(string));
            table2.Columns.Add("formalite", typeof(string));
            table2.Columns.Add("volume_depot", typeof(string));
            table2.Columns.Add("numero_depot", typeof(string));
            table2.Columns.Add("date_depot", typeof(string));
            table2.Columns.Add("nom_piece", typeof(string));
            table2.Columns.Add("nombre_page", typeof(string));
            table2.Columns.Add("num_page", typeof(string));

            foreach (string id in idFromDictionnaire)
            {
                DataRow[] result = table.Select("id_vue = " + id + "");
                foreach (DataRow row in result)
                {
                    table2.Rows.Add(row["id_vue"], row["numero_ordre"], row["id_status"], row["url"], row["num_sous_dos"], row["formalite"], row["volume_depot"], row["numero_depot"], row["date_depot"], row["nom_piece"], row["nombre_page"], row["num_page"]);
                }
                
            }
            return table2;
        }

        //service permet de remetre en ordre le dictionnaire en utilisant le premier id ordre sasie
        public Dictionary<string, string> remettreEnOrdre(Dictionary<string, string> dictioIdwhitname, string idFirstOrdre)
        {
            Dictionary<string, string> dictio = new Dictionary<string, string>();

            string id = "";
            string valueProvisoir = "";

            for (int i = 0; i < dictioIdwhitname.Count; i++)
            {
                if (i == 0)
                {
                    id = this.getKey(idFirstOrdre, dictioIdwhitname);
                    valueProvisoir = id;
                    dictio.Add(id, idFirstOrdre);
                }
                else
                {
                    id = this.getKey(valueProvisoir, dictioIdwhitname);
                    if (id != "")
                    {
                        dictio.Add(id, valueProvisoir);
                        valueProvisoir = id;
                    }
                }

            }
            return dictio;
        }

        public DataTable getIndexTF(String idTF)
        {
            DataTable dt = baseDonne.getIndexTF(idTF);
            DataTable table = new DataTable();

            table.Columns.Add("numero_origine", typeof(string));
            table.Columns.Add("nature_origine", typeof(string));
            table.Columns.Add("indice_origine", typeof(string));
            table.Columns.Add("indice_sporigine", typeof(string));
            table.Columns.Add("numero_titre", typeof(string));
            table.Columns.Add("indice_titre", typeof(string));
            table.Columns.Add("indice_sptitre", typeof(string));

            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {

                    String numero_origine = dt.Rows[i][0].ToString().Trim();
                    String nature_origine = dt.Rows[i][1].ToString().Trim();
                    String indice_origine = dt.Rows[i][2].ToString().Trim();
                    String indice_sporigine = dt.Rows[i][3].ToString().Trim();
                    String numero_titre = dt.Rows[i][4].ToString().Trim();
                    String indice_titre = dt.Rows[i][5].ToString().Trim();
                    String indice_sptitre = dt.Rows[i][6].ToString().Trim();
                    table.Rows.Add(numero_origine, nature_origine, indice_origine, indice_sporigine, numero_titre, indice_titre, indice_sptitre);
                }
            }
            return table;

        }

        public Stream pathToStream(String url)
        {
            Stream fs = File.OpenRead(@url);
            
            return fs;
        }

        public Boolean deleteVue(string idVue)
        {
            Boolean condition = false;
            if (baseDonne.updateStatuePiece("5", idVue))
            {
            condition=true;
            }
            return condition;
        }

        public Boolean validerVue(int idUtilisateur,string idVue)
        {
            Boolean condition = false;
            if (baseDonne.updateStatuePiece("6", idVue))
            {
                condition = true;
                if (!baseDonne.insert_Historique_Vue(idUtilisateur.ToString(), idVue))
                {
                    condition = false;
                    return condition;
                }
                else
                {
                    //enregistrer operation vue
                    if (!baseDonne.insert_Operation_Vue(idUtilisateur.ToString(), idVue, "6"))
                    {
                        condition = false;
                        return condition;
                    }
                }
            }

            return condition;
        }

        public Boolean corrigerVue(int idUtilisateur, string idVue)
        {
            Boolean condition = false;
            if (baseDonne.updateStatuePiece("10", idVue))
            {
                condition = true;
                if (!baseDonne.insert_Historique_Vue(idUtilisateur.ToString(), idVue))
                {
                    condition = false;
                    return condition;
                }
                else
                {
                    //enregistrer operation vue
                    if (!baseDonne.insert_Operation_Vue(idUtilisateur.ToString(), idVue, "10"))
                    {
                        condition = false;
                        return condition;
                    }
                }
            }

            return condition;
        }

        public Boolean corrigerVue(string idVue)
        {
            Boolean condition = false;
            if (baseDonne.updateStatuePiece("10", idVue))
            {
                condition = true;
            }
            return condition;
        }

        public Boolean rejeterVue(int idUtilisateur,string idVue)
        {
            Boolean condition = false;
            if (baseDonne.updateStatuePiece("7", idVue))
            {
                condition = true;
                if (!baseDonne.insert_Historique_Vue(idUtilisateur.ToString(), idVue))
                {
                    condition = false;
                    return condition;
                }
                else
                {
                    //enregistrer operation vue
                    if (!baseDonne.insert_Operation_Vue(idUtilisateur.ToString(), idVue, "7"))
                    {
                        condition = false;
                        return condition;
                    }
                }
            }
            return condition;
        }

        public Boolean changerStatueDossier(int idagent, string idDossier, int numStatue)
        {
            Boolean condition = false;
            if (baseDonne.changerStatueDossier(idagent,numStatue, idDossier))
            {
                condition = true;
                if (!baseDonne.insert_Historique_Dossier(idagent.ToString(), idDossier))
                {
                    condition = false;
                    return condition;
                }
                else
                {
                    if (!baseDonne.insert_Operation_Dossier(idagent.ToString(), idDossier, numStatue.ToString()))
                    {
                        condition = false;
                        return condition;
                    }
                }
            }
            return condition;
        }

        public string FileSizeFormat(long lSize)
        {
            double size = lSize;
            int index = 0;
            for (; size > 1024; index++)
                size /= 1024;
            return size.ToString("0.000 " + new[] { "B", "KB", "MB", "GB", "TB" }[index]);
        }

        public static ArrayList chargerListeNomPieces()
        {
            ArrayList listeNomPieces = new ArrayList();

            DataTable dt = baseDonne.getListeNamesPieces();
            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    if (i != 0)
                    {
                        listeNomPieces.Add(dt.Rows[i][1].ToString().Trim());
                    }
                    else
                    {
                        listeNomPieces.Add("PAGE DE GARDE DU DOSSIER");
                        listeNomPieces.Add("FICHE DE CONTROLE");
                        listeNomPieces.Add("PAGE DE GARDE DU SOUS DOSSIER");
                    }
                }
            }
            return listeNomPieces;
        }

        public Boolean updateFichePieces(String idVue ,String namePiece,String nbrPages,String numPages)
        {
            Boolean condition = false;

            if (baseDonne.updateFichePieces(idVue ,namePiece,nbrPages,numPages))
            {
                condition = true;
            }
            
            return condition;
        }

        public Boolean updateFicheDossier(string idDossier, string natureOrigine, string numOrigine, string indiceOrigine, string indiceSpOrigine, string numTitre, string indiceTitre, string indiceSpTitre)
        {
            Boolean condition = false;

            if (baseDonne.updateFicheDossier(idDossier,natureOrigine,numOrigine,indiceOrigine,indiceSpOrigine,numTitre,indiceTitre,indiceSpTitre))
            {
                condition = true;
            }

            return condition;
        }

        public Boolean updateFicheSousDossier(string idPiece, string numSousDossier, string formalite, string volumeDepot, string numDepot, string dateDepot)
        {
            Boolean condition = false;
            if (baseDonne.updateFicheSousDossier(idPiece,numSousDossier,formalite,volumeDepot,numDepot,dateDepot))
            {
                condition = true;
            }
            return condition;
        }

        //charger la liste natureOrigine
        public static ArrayList chargerlisteNatureOrigine()
        {
            ArrayList listeNatureOrigine = new ArrayList();
            listeNatureOrigine.Add("T");
            listeNatureOrigine.Add("R");
            return listeNatureOrigine;
        }

        //charger la liste indices origines
        public static DataTable chargerlisteIndiceOrigine()
        {           
            DataTable dt = baseDonne.GetData("SELECT * FROM [dbo].[TB_indice_origine]");
            return dt;
        }

        //charger la liste indice titre
        public static DataTable chargerlisteIndiceTitre()
        {

            DataTable dt = baseDonne.GetData("SELECT * FROM [dbo].[TB_indice_titre]");
            return dt;
           
        }

        ////charger la liste des mois et anne par livrable
        //public static ArrayList getListeMoisAndANNEParLivrable(String idLivrable)
        //{
        //    ArrayList listeMoisANNE = new ArrayList();
        //    DataTable dt = baseDonne.getListeMoisAndANNEParLivrable(idLivrable);
        //    int x = dt.Rows.Count;
        //    if (x > 0)
        //    {
        //        for (int i = 0; i < x; i++)
        //        {
        //            listeMoisANNE.Add(dt.Rows[i][0].ToString().Trim() + "/" + dt.Rows[i][1].ToString().Trim());
        //        }
        //    }
        //    return listeMoisANNE;
        //}


        //charger liste formalites
        public static ArrayList chargerListeFormalites()
        {
            ArrayList listeFormalites = new ArrayList();
            DataTable dt = baseDonne.chargerListeFormalites();
            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    listeFormalites.Add(dt.Rows[i][0].ToString().Trim());
                }
            }
            return listeFormalites;
        }

        public DataTable chargerlisteFormalitesTable()
        {
            DataTable dt = baseDonne.chargerlisteFormalitesTable();
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

        public Boolean desaffecterDossiersCorrection(int id_user_operation, ArrayList listeIDsDossiers)
        {
            Boolean condition = true;

            string partDossiers = "";
            int i = 0;
            foreach (string dossier in listeIDsDossiers)
            {
                if (i == 0)
                {
                    partDossiers = dossier;
                }
                else
                {
                    partDossiers = partDossiers + "," + dossier;
                }
                i++;
            }

            string requette = "UPDATE [TB_Dossier] SET [id_status] = 7 ,[user_cor] = NULL WHERE [id_dossier] in (" + partDossiers + ")";
            if (baseDonne.SetData(requette))
            {
                condition = true;
                //creation historique dossier
                foreach (string dossier in listeIDsDossiers)
                {
                    if (baseDonne.insert_Historique_Dossier(id_user_operation.ToString(), dossier))
                    {
                        //creation historique table operation
                        baseDonne.insert_Operation_Dossier(id_user_operation.ToString(), dossier, "7");
                    }
                }
            }
            else
            {
                condition = false;
            }
            return condition;
        }

        public Boolean desaffecterDossiers(int id_user_operation,ArrayList listeIDsDossiers)
        {
            Boolean condition = true;

            string partDossiers = "";
            int i = 0;
            foreach (string dossier in listeIDsDossiers)
            {
                if (i == 0)
                {
                    partDossiers = dossier;
                }
                else
                {
                    partDossiers = partDossiers + "," + dossier;
                }
                i++;
            }

            string requette = "UPDATE [TB_Dossier] SET [id_status] = 0 ,[id_user] = NULL ,[date_affect] = NULL WHERE [id_dossier] in (" + partDossiers + ")";
            if (baseDonne.SetData(requette))
            {
                condition = true;
                //creation historique dossier
                foreach (string dossier in listeIDsDossiers)
                {
                    if (baseDonne.insert_Historique_Dossier(id_user_operation.ToString(), dossier))
                    {
                        //creation historique table operation
                        baseDonne.insert_Operation_Dossier(id_user_operation.ToString(), dossier, "0");
                    }
                }
            }
            else
            {
                condition = false;
            }
            return condition;           
        }

        public Boolean affecterDossiersCorrection(int id_user_operation, string idUser, ArrayList listeIDsDossiers)
        {
            Boolean condition = true;

            string partDossiers = "";
            int i = 0;
            foreach (string dossier in listeIDsDossiers)
            {
                if (i == 0)
                {
                    partDossiers = dossier;
                }
                else
                {
                    partDossiers = partDossiers + "," + dossier;
                }
                i++;
            }

            string requette = "UPDATE [TB_Dossier] SET [id_status] = 14 ,[user_cor] = " + idUser + " WHERE [id_dossier] in (" + partDossiers + ")";
            if (baseDonne.SetData(requette))
            {
                condition = true;
                //creation historique dossier
                foreach (string dossier in listeIDsDossiers)
                {
                    if (baseDonne.insert_Historique_Dossier(id_user_operation.ToString(), dossier))
                    {
                        //creation historique table operation
                        baseDonne.insert_Operation_Dossier_casAffectation(id_user_operation.ToString(), idUser.ToString(), dossier, "14");

                    }
                }


            }
            else
            {
                condition = false;
            }
            return condition;
        }
        
        public Boolean affecterDossiers(int id_user_operation,string idUser,ArrayList listeIDsDossiers)
        {
            Boolean condition = true;

            string partDossiers = "";
            int i = 0;
            foreach (string  dossier in listeIDsDossiers)
            {
                if(i==0)
                {
                    partDossiers = dossier;
                }
                else
                {
                    partDossiers = partDossiers + "," + dossier;
                }
                i++;
            }

            string requette = "UPDATE [TB_Dossier] SET [id_status] = 1 ,[id_user] = " + idUser + " ,[date_affect] = GETDATE() WHERE [id_dossier] in (" + partDossiers + ")";
            if (baseDonne.SetData(requette))
            {
                condition = true;
                //creation historique dossier
                foreach (string dossier in listeIDsDossiers)
                {
                    if (baseDonne.insert_Historique_Dossier(id_user_operation.ToString(), dossier))
                    {
                        //creation historique table operation
                        baseDonne.insert_Operation_Dossier_casAffectation(id_user_operation.ToString(),idUser.ToString(), dossier, "1");
                        
                    }
                }
                
                
            }
            else
            {
                condition = false;
            }
            return condition;
        }

        public Boolean verifier_le_droit(int id_droit,int id_utilisateur)
        {
            Boolean condition = false;

            DataTable dt = baseDonne.GetData("select * from [Administration_ANCFCC].dbo.attribution_droits where id_droit=" + id_droit + " and id_user=" + id_utilisateur + "");
            int x = dt.Rows.Count;
            if (x > 0)
            {
                condition = true;
            }
            else
            {
                condition = false;
            }
            return condition;
        }

        public DataTable get_totalites_equipes()
        {
            /****** Script de la commande SelectTopNRows à partir de SSMS  ******/

            string req = "SELECT [nom_equipe] FROM [TB_Equipe] ";

            DataTable dt = baseDonne.GetData(req);

            return dt;
        }

        public Boolean addequipe(string nomEquipe,int type_equipe)
        {
            Boolean condition = false;

            string req = "INSERT INTO [TB_Equipe] ([nom_equipe],[type_equipe]) VALUES ('" + nomEquipe + "'," +" "+ type_equipe+")";

            if (baseDonne.SetData(req))
            {
                condition = true;
            }
            else
            {
                condition = false;
            }

            return condition;
        }
        
        public Boolean verifier_existance_equipe(string nomequipe)
        {
            Boolean condition = false;
            string req = "select [id_equipe] from [TB_Equipe] where [nom_equipe]='" + nomequipe + "'";
            DataTable dt = baseDonne.GetData(req);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                condition = true;
            }
            else
            {
                condition = false;
            }
            return condition;
        }

        public Boolean mettre_ajour_name_equipe(string ancien_nom,string nouveau_nom)
        {
            Boolean condition = false;

            string req = "UPDATE [TB_Equipe] SET [nom_equipe] = '" + nouveau_nom + "' where [nom_equipe] = '" + ancien_nom + "'";

            condition = baseDonne.SetData(req);
           

            return condition;
        }

        public int get_id_equipe(int id_utilisateur)
        {
            int i = 0;

            string req = "SELECT ISNULL([id_equipe], 0 ) FROM [TB_Utilisateurs] where id_user=" + id_utilisateur + "";
            
            DataTable dt = baseDonne.GetData(req);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                i = Int32.Parse(dt.Rows[0][0].ToString());
            }

            return i;
        }

        public int get_id_groupe(int id_utilisateur)
        {
            int i = 0;

            string req = "SELECT [id_group] FROM [TB_Utilisateurs] where id_user=" + id_utilisateur + "";

            DataTable dt = baseDonne.GetData(req);
            int x = dt.Rows.Count;
            if (x > 0)
            {
                i = Int32.Parse(dt.Rows[0][0].ToString());
            }

            return i;
        }

        //recupération des utilisateur avec précision des groupes
        public DataTable get_utilisateur_using_liste_groupe(ArrayList listegroupe)
        {
            string req = "SELECT *   FROM [VUtilisateurs_Equipes_Groupes] where [nom_groupe] in ('Superviseurs','Agents indexation juniors','Agents indexation confirmeés','Agents indexation seniors')";
            DataTable dt = baseDonne.GetData(req);  
            return dt;
        }

        public DataTable convert_result_recherche(DataTable tdresult)
        {
            
            DataTable table = new DataTable();
            table.Columns.Add("ID dossier", typeof(string));
            table.Columns.Add("Nom dossier", typeof(string));
            table.Columns.Add("Nombre des vues", typeof(int));
            table.Columns.Add("Statue dossier", typeof(string));
            //table.Columns.Add("Scanné le", typeof(int));
            table.Columns.Add("Scanné le", typeof(string));
            table.Columns.Add("Scanné par", typeof(string));
            table.Columns.Add("Affecter à", typeof(string));
            table.Columns.Add("Indexer par", typeof(string));
            table.Columns.Add("Indexer le", typeof(string));
            table.Columns.Add("Verifier par", typeof(string));
            table.Columns.Add("Verifier le", typeof(string)); 
            table.Columns.Add("Controller par", typeof(string));
            table.Columns.Add("Controller le", typeof(string));
            table.Columns.Add("Corriger par", typeof(string));
            //table.Columns.Add("Corriger le", typeof(string));

            int x = tdresult.Rows.Count;
            
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    String id_dossier = tdresult.Rows[i][0].ToString().Trim();
                    String nomdossier = tdresult.Rows[i][4].ToString().Trim();
                    int nbrVues = Int32.Parse(tdresult.Rows[i][5].ToString().Trim());
                    String statuedossier = tdresult.Rows[i][1].ToString().Trim();
                    //int scannerle = Int32.Parse(tdresult.Rows[i][10].ToString().Trim());
                    String scannerle = tdresult.Rows[i][10].ToString().Trim();
                    String scannerpar = tdresult.Rows[i][11].ToString().Trim();
                    String affecter_a = tdresult.Rows[i][12].ToString().Trim();
                    String indexer_par = tdresult.Rows[i][13].ToString().Trim();
                    String indexer_le = tdresult.Rows[i][16].ToString().Trim();
                    String controller_par = tdresult.Rows[i][14].ToString().Trim();
                    String controller_le = tdresult.Rows[i][18].ToString().Trim();
                    String verifier_par = tdresult.Rows[i][15].ToString().Trim();
                    String verifier_le = tdresult.Rows[i][17].ToString().Trim();
                    String corriger_par = tdresult.Rows[i][21].ToString().Trim();
                    //String corriger_le = tdresult.Rows[i][22].ToString().Trim();

                    table.Rows.Add(id_dossier, nomdossier, nbrVues, statuedossier, scannerle, scannerpar, affecter_a, indexer_par, indexer_le, verifier_par, verifier_le, controller_par, controller_le, corriger_par);
                }
            }
            return table;
        }

        public DataTable get_Historique_Operation_Dossier(string idDossier)
        {
            string req = "SELECT [id_status],[date_operation],[user_operation],[affecter_a] FROM [TB_Operation_Dossier] where [id_dossier]='" + idDossier + "'";
            string req2 = "select op.[id_status],op.[date_operation],u.login,u2.login from dbo.TB_Operation_Dossier as op inner join Administration_ANCFCC.dbo.TB_Utilisateurs as u on u.id_user=op.user_operation left join Administration_ANCFCC.dbo.TB_Utilisateurs as u2 on u2.id_user=op.affecter_a where [id_dossier]='" + idDossier + "' order by op.[date_operation]";
            DataTable dt = baseDonne.GetData(req2);
            
            DataTable table = new DataTable();

            table.Columns.Add("Operation", typeof(string));
            table.Columns.Add("Date Operation", typeof(string));
            table.Columns.Add("Effectuer par", typeof(string));
            table.Columns.Add("Affecter à", typeof(string));           

            int x = dt.Rows.Count;
            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {                    
                    String operation = generate_operation(dt.Rows[i][0].ToString().Trim());
                    String date_operation = dt.Rows[i][1].ToString().Trim();
                    String effectuer_par = dt.Rows[i][2].ToString().Trim();
                    String affecter_a = dt.Rows[i][3].ToString().Trim();
                    
                    table.Rows.Add(operation, date_operation, effectuer_par, affecter_a);
                }
            }
            return table;

        }

        public string generate_operation(string statueDossier)
        {
            string operation = "";

            if (statueDossier == "1")
            {
                operation = "Affectation";
            }
            else if(statueDossier == "3")
            {
                operation = "Indexation";
            }
            else if(statueDossier == "8")
            {
                operation = "Verification";
            }
            else if (statueDossier == "7")
            {
                operation = "Rejet";
            }
            else if (statueDossier == "6")
            {
                operation = "Validation";
            }
            else if (statueDossier == "0")
            {
                operation = "Désaffectation";
            }
            else if (statueDossier == "9")
            {
                operation = "Envoie pour livraison";
            }
            else if (statueDossier == "11")
            {
                operation = "Livraison";
            }
            else if (statueDossier == "14")
            {
                operation = "En cours de correction";
            }
            else if (statueDossier == "10")
            {
                operation = "Corriger";
            }
            else if (statueDossier == "12")
            {
                operation = "Erreur de scan";
            }
            return operation;
        }
        
        public Boolean insertFormalite(string formalitefr, string formalitear, string categorie, int utilisateur)
        {
            Boolean condition = true;

            if (!baseDonne.insertFormalite(formalitefr, formalitear, categorie, utilisateur))
            {
                condition = false;
            }

            return condition;
        }

        public DataTable chargerListeNomPiecesTable()
        {
            DataTable dt = baseDonne.getListeNamesPiecesTable();
            return dt;
        }


        public Boolean insertNamePiece(string namepiece, int utilisateur)
        {
            Boolean condition = true;
            if(!baseDonne.insertNamePiece(namepiece,utilisateur))
            {
                 condition = false;
            }
            return condition;
        }

        public DataTable chargerlistebases()
        {
            DataTable dt = baseDonne.chargerlistebases();
            return dt;
        }

        public Boolean chargerLabase(int idbase)
        {
            Boolean condition = false;
            if (idbase != 0)
            {
                if (baseDonne.getinformationsBD(idbase))
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

        public Boolean update_etats(int idLivrable, int id_user, string date, string what, string value)
        {
            Boolean condition = false;
            if (baseDonne.update_etats(idLivrable, id_user, date, what, value))
            {
                condition = true;
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


        public DataTable chargerlisteFormalites()
        {
            DataTable dt = baseDonne.chargerlisteFormalites();
            return dt;
        }

        public DataTable chargerlisteTranches()
        {
            DataTable dt = baseDonne.GetData("select id_tranche,tranche from TB_Tranche");
            return dt;
        }

        public DataTable chargerlisteTranchesLivres()
        {
            DataTable dt = baseDonne.GetData("select id_tranche,tranche from TB_Tranche where livree=1");
            return dt;
        }

        public DataTable chargerlisteTranchesEnCours()
        {
            DataTable dt = baseDonne.GetData("select id_tranche,tranche from TB_Tranche where livree is null");
            return dt;
        }

    }

}
