using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANCFCC_Etats
{
    class requetteur
    {
        string reqIndexation1 = "select col1 as 'Nombre des vues',col2 as 'Nombre des dossiers',LTRIM(RTRIM(login)) as operateur,date_action,LTRIM(RTRIM(nom_livrable)) as Livrable from [dbo].[etats] ";
        string reqIndexation2 = "LEFT JOIN Administration_ANCFCC.dbo.TB_Utilisateurs on ";
        string reqIndexation3 = "[dbo].[etats].id_user = Administration_ANCFCC.dbo.TB_Utilisateurs.id_user LEFT JOIN [dbo].tb_livrable on ";
        string reqIndexation4 = "[dbo].[etats].livrable = [dbo].tb_livrable.id_livrable";

        string reqControlle1 = "select col3 as 'Nbr des dossiers validés',col4 as 'Nbr des dossiers rejetés',col5 as 'Nbr des dossiers controllés',col6 as 'Nbr des vues validées',col7 as 'Nbr des vues rejetées',col8 as 'Nbr des vues corrigées',col9 as 'Nbr des vues controllées',LTRIM(RTRIM(login)) as operateur,date_action,LTRIM(RTRIM(nom_livrable)) as Livrable from [dbo].[etats] ";
        string reqControlle2 = "LEFT JOIN Administration_ANCFCC.dbo.TB_Utilisateurs on ";
        string reqControlle3 = "[dbo].[etats].id_user = Administration_ANCFCC.dbo.TB_Utilisateurs.id_user LEFT JOIN [dbo].tb_livrable on ";
        string reqControlle4 = "[dbo].[etats].livrable = [dbo].tb_livrable.id_livrable";


        public string getrequetteIndexation()
        {
            return reqIndexation1 + reqIndexation2 + reqIndexation3 + reqIndexation4;
        }

        public string getrequetteControlle()
        {
            return reqControlle1 + reqControlle2 + reqControlle3 + reqControlle4;
        }

        public string getParameterAgent(string listeagent)
        {
            string parameter = "where [dbo].[etats].id_user in (" + listeagent + ")";  
            return parameter;
        }

        public string getParameterLivrable(string listeLivrables)
        {
            string parameter = "and [dbo].[etats].livrable in (" + listeLivrables + ")";            
            return parameter;
        }

        public string getParameterDates(string debut, string fin)
        {
            string parameter = "and [dbo].[etats].date_action between convert(datetime, ('" + debut + "'), 103) and convert(datetime, ('" + fin + "'), 103) ";
            return parameter;
        }

    }
}
