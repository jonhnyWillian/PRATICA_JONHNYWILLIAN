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
                sqlconnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
                sqlconnection.Open();
            }
            catch (Exception e)
            {               
                throw new Exception("Erro ao abrir a conexão: " + e.Message);
            }
        }
     
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
                throw new Exception("Erro ao fechar a conexão: " + e.Message);
            }
        }

        protected string FormatString(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.ToUpper().Trim();
            }
            return text;
        }
        protected string FormatDate(DateTime? data)
        {
            var result = string.Empty;
            if (data != null)
            {
                result = "'" + data.Value.ToString("yyyy-MM-dd") + "'";
            }
            return result;
        }
        protected string FormatDecimal(decimal? valor)
        {
            var result = string.Empty;
            if (valor != null)
            {
                result = valor.Value.ToString().Replace(",", ".");
            }
            return result;
        }
    }
}