using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANCFCC_NV
{
    class DossierF
    {
        public string nature_Orgine { set; get; }
        public string numero_Orgine { set; get; }
        public string indice_orgine { set; get; }
        public string indice_special_orgine { set; get; }
        public string numero_Titre { set; get; }
        public string indice_titre { set; get; }
        public string indice_special_titre { set; get; }

        public DossierF() { }

        public DossierF(string nature_Orgine, string numero_Orgine, string indice_orgine, string indice_special_orgine, string numero_Titre, string indice_titre, string indice_special_titre)
        {
            this.nature_Orgine = nature_Orgine;
            this.numero_Orgine = numero_Orgine;
            this.indice_orgine = indice_orgine;
            this.indice_special_orgine = indice_special_orgine;
            this.numero_Titre = numero_Titre;
            this.indice_titre = indice_titre;
            this.indice_special_titre = indice_special_titre;
        }
    }
}
