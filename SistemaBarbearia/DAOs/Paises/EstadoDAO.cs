using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Paises;
using SistemaBarbearia.ViewModels.Paises;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Paises
{
    public class EstadoDAO : DataAccess
    {
        public bool AdicionarEstado(Estado estado)
        {

            try
            {
                Open();
                string insertEstado = @"INSERT INTO ESTADO (nmEstado, dsUF, dtCadastro, idPais) VALUES(@nmEstado, @dsUF, @dtCadastro, @idPais)";
                SQL = new SqlCommand(insertEstado, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmEstado", estado.nmEstado.ToUpper());
                SQL.Parameters.AddWithValue("@dsUF", estado.dsUF.ToUpper());
                SQL.Parameters.AddWithValue("idPais", estado.pais.Id);
                SQL.Parameters.AddWithValue("@dtCadastro", estado.dtCadastro = DateTime.Now);

                int i = SQL.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Adicionar Novo Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public IEnumerable<Estado> SelecionarEstado()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM ESTADO ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Models.Paises.Estado>();
                while (Dr.Read())
                {
                    var estado = new Models.Paises.Estado()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        nmEstado = Convert.ToString(Dr["nmEstado"]),
                        dsUF = Convert.ToString(Dr["dsUF"]),
                        idPais = Convert.ToInt32(Dr["idPais"]),


                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        //dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(estado);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public EstadoVM GetEstado(int? Id)
        {
            try
            {
                Open();
                var estadoVM = new EstadoVM();
                string selectEditPais = @"SELECT* FROM ESTADO WHERE id =" + Id;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    estadoVM.Id = Convert.ToInt32(Dr["id"]);
                    estadoVM.nmEstado = Dr["nmEstado"].ToString();
                    estadoVM.dsUF = Dr["dsUFa"].ToString();
                    estadoVM.idPais = Convert.ToInt32(Dr["idPais"]);
                    estadoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    estadoVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return estadoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public EstadoVM filtraEstado(string nmEstado)
        {
            try
            {
                Open();
                var estadoVM = new EstadoVM();
                string selectEditPais = @"SELECT* FROM ESTADO WHERE nmEstado =" + nmEstado;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    estadoVM.Id = Convert.ToInt32(Dr["id"]);
                    estadoVM.nmEstado = Dr["nmEstado"].ToString();
                    estadoVM.dsUF = Dr["dsUF"].ToString();
                    estadoVM.idPais = Convert.ToInt32(Dr["idPais"]);
                    estadoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    estadoVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return estadoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateEstado(Estado estado)
        {
            try
            {
                Open();
                string updateEstado = @"UPDATE ESTADO SET nmEstado = @nmEstado, dsUF = @dsSigla, idPais = @idPais ,dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateEstado, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", estado.Id);
                sql.Parameters.AddWithValue("@nmEstado", estado.nmEstado.ToUpper());
                sql.Parameters.AddWithValue("@dsUF", estado.dsUF.ToUpper());
                sql.Parameters.AddWithValue("@idPais", estado.idPais);
                sql.Parameters.AddWithValue("@dtUltAlteracao", estado.dtUltAlteracao = DateTime.Now);

                int i = sql.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Atualizar Esatdo: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool ExcluirEstado(int Id)
        {
            try
            {
                Open();
                string deleteEstado = "DELETE FROM Estado WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteEstado, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@Id", Id);

                int i = sql.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao excluir Esatdo: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}