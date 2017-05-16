using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Consutation_Controle_Validation
{
    class Generateur
    {
        public string id_livrable;
        string requetteGetListeDossiers = "SELECT * FROM dbo.VEtatDossierIndexed where id_livrable=('$idlivrable')";
        string parametreDate = " and date_index between convert(datetime, ('$datein'), 103) and convert(datetime, ('$dateout 23:59:00.000'), 103)";        
        string parametreStatueDossiers = " and id_status in ($idStatuesRecupered)";
        string parametreAgentIndexation = " and user_indexation in ($idAgentsRecupered)";
        string parameterTranche = " and ID_tranche in ($idTranchesRecupered)";
        string sanstrahnche = "and id_dossier not in (select ID_doss from dbo.affectation)";
        
        public string recherche()
        {
            string req = "";
            if (id_livrable == "0")
            {
                req = "SELECT * FROM dbo.VEtatDossierIndexed where 1=1";
            }
            else
            {
                req = requetteGetListeDossiers.Replace("$idlivrable", id_livrable);
            }
            return req;
        }

        public string rechercheByDate(String datein, String dateOut)
        {
            string req = parametreDate.Replace("$datein", datein);

            string req2 = req.Replace("$dateout", dateOut);
            return req2;
        }

        public string rechercheByTranche(String idTranchesRecupered)
        {
            string req = parameterTranche.Replace("$idTranchesRecupered", idTranchesRecupered);
            return req;
        }

        public string rechercheDossiersSansTranches()
        {
            return sanstrahnche;
        }

        public string rechercheByStatue(String idStatuesRecupered)
        {            
            string req = parametreStatueDossiers.Replace("$idStatuesRecupered", idStatuesRecupered);
            return req;
        }

        public string rechercheById(String idAgentsRecupered)
        {
            string req = parametreAgentIndexation.Replace("$idAgentsRecupered", idAgentsRecupered);
            return req;
        }

        

    }
}
