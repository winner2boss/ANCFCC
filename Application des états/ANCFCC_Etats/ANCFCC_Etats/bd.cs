using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ANCFCC_Etats
{
    class bd
    {
        public static SqlConnection connectionAdministration = new SqlConnection(ConfigurationManager.ConnectionStrings["GEDConnectionString"].ToString());
        public SqlConnection connection;

        public string getchaineconnection(int idbase)
        {
            String chaineconnection = "";
            DataTable dt = new DataTable();
            try
            {
                //get informamtion bases
                string reqGetInformationsBase = "SELECT [name_base],[ip_base],[user_base],[pass_base] FROM [Administration_ANCFCC].[dbo].[TB_Bases] where id_base=('$idbase')";
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
                    chaineconnection = "Data Source=" + ip_base + ";Initial Catalog=" + name_base + ";User ID=" + user_base + ";Password=" + pass_base + "";
                    
                }

            }
            catch (Exception ex)
            {
                chaineconnection = "";
                ex.Message.ToString();
            }
            return chaineconnection;
        }

        public Boolean getinformationsBD(int idbase)
        {
            Boolean condition = true;
            DataTable dt = new DataTable();
            try
            {
                //get informamtion bases
                string reqGetInformationsBase = "SELECT [name_base],[ip_base],[user_base],[pass_base] FROM [Administration_ANCFCC].[dbo].[TB_Bases] where id_base=('$idbase')";
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

        public DataTable GetData(string req,string conn)
        {
            SqlConnection connectionbase = new SqlConnection(conn);
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(req, connectionbase);
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
                SqlDataAdapter da = new SqlDataAdapter(req, connectionAdministration);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        public DataTable getlisteagent()
        {
            DataTable dt = new DataTable();

            try
            {
                String reqFinal = "SELECT [id_user],[login]  FROM [dbo].[TB_Utilisateurs]";
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connectionAdministration);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;
        }

        public DataTable getlistebases()
        {
            DataTable dt = new DataTable();
            try
            {
                String reqFinal = "SELECT [id_base],[name_base] FROM [dbo].[TB_Bases]";
                SqlDataAdapter da = new SqlDataAdapter(reqFinal, connectionAdministration);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;
        }
              
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

        public DataTable getListeLivrable()
        {
            DataTable dt = new DataTable();
            try
            {
                //requette recuperation de la liste des livrables
                string reqGetLivrable = "SELECT [id_livrable],[nom_livrable] FROM [TB_Livrable]";
                SqlDataAdapter da = new SqlDataAdapter(reqGetLivrable, connection);
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
