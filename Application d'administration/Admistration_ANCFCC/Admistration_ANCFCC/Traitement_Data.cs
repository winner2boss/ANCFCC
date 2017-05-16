using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace Admistration_ANCFCC
{
    class Traitement_Data
    {
        private SqlConnection cn_admin = new SqlConnection(ConfigurationManager.ConnectionStrings["cnAdministration"].ToString());
        private SqlDataAdapter da_admin;
        private SqlCommandBuilder cmd_admin_builder;
        private SqlCommand cmd_admin;

        public SqlConnection getConnection()
        {
            return cn_admin;
        }

        public DataTable getData(string req)
        {
            da_admin = new SqlDataAdapter(req, cn_admin);
            DataTable dt = new DataTable();
            da_admin.Fill(dt);
            return dt;
        }
        public bool setData(String req)
        {
            try
            {
                da_admin = new SqlDataAdapter(req, cn_admin);
                DataTable dt = new DataTable();
                da_admin.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return false;
        }

    }
}
