using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace SistemaBarbearia.DataBase
{
    public class DataAccess
    {

        public static SqlConnection sqlconnection;

        protected SqlCommand SQL;

        protected SqlDataReader Dr;
        protected void Open()
        {
            try
            {
                //Outra maneira de criar uma connection string (usando o Web.Config
                sqlconnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
                sqlconnection.Open();
            }
            catch (Exception e)
            {
                // Caso dê algum problema, enviar uma mensagem informando o erro:
                throw new Exception("Erro ao abrir a conexão: " + e.Message);
            }
        }
        //Método para fechar a conexão:
        protected void Close()
        {
            try
            {
                if (sqlconnection != null)
                {
                    sqlconnection.Close();
                }
            }
            catch (Exception e)
            {
                // Caso dê algum problema, enviar uma mensagem informando o erro:
                throw new Exception("Erro ao fechar a conexão: " + e.Message);
            }
        }
    }
}