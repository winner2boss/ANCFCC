using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ANCFCC_Etats
{
    class service
    {
        bd basedonne = new bd();

        public DataTable getlisteagent()
        {
            DataTable dt = basedonne.getlisteagent();
            return dt;
        }

        public DataTable getlistebases()
        {
            DataTable dt = basedonne.getlistebases();
            return dt;
        }

        public Boolean chargerLabase(int idbase)
        {
            Boolean condition = false;
            if (idbase != 0)
            {
                if (basedonne.getinformationsBD(idbase))
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
            DataTable dt = basedonne.getListeLivrable();

            return dt;
        }
    
    }
}
