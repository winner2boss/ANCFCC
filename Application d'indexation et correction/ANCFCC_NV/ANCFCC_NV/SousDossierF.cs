using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANCFCC_NV
{
    class SousDossierF
    {
        public string numero_sousDossier { set; get; }
        public string formalite { set; get; }
        public string volume_depot { set; get; }
        public string numero_depot { set; get; }
        public string dateDepot { set; get; }
       

        public SousDossierF()
        {
            numero_sousDossier = "0";
            formalite = "AUCUN";
            volume_depot = "0";
            numero_depot = "0";
            dateDepot = "01/01/1990";           
        }

        public SousDossierF(string numero_sousDossier, string formalite, string volume_depot, string numero_depot, string dateDepot)
        {
            this.numero_sousDossier = numero_sousDossier;
            this.formalite = formalite;
            this.volume_depot = volume_depot;
            this.numero_depot = numero_depot;
            this.dateDepot = dateDepot;            
        }
    }
}
