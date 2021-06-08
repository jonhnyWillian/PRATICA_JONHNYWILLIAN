using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Cargos;
using SistemaBarbearia.ViewModels.Cargos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Cargos
{
    public class CargoDAO : DataAccess
    {
        public bool AdicionarCargo(Cargo cargos)
        {

            try
            {
                Open();
                string insertCargo = @"INSERT INTO CARGO (dsCargo, dtCadastro) VALUES(@dsCargo, @dtCadastro)";
                SQL = new SqlCommand(insertCargo, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsCargo", cargos.dsCargo.ToUpper());                
                SQL.Parameters.AddWithValue("@dtCadastro", cargos.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo CARGO: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public IEnumerable<Cargo> SelecionarCargo()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM CARGO", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Cargo>();
                while (Dr.Read())
                {
                    var servico = new Cargo()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        dsCargo = Convert.ToString(Dr["dsCargo"]),                      
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(servico);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cargo: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public CargoVM GetCargo(int? Id)
        {
            try
            {
                Open();
                var servicoVM = new CargoVM();
                string selectEditCargo = @"SELECT* FROM CARGO WHERE id =" + Id;
                SQL = new SqlCommand(selectEditCargo, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    servicoVM.Id = Convert.ToInt32(Dr["id"]);
                    servicoVM.dsCargo = Dr["dsCargo"].ToString();              
                    servicoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);

                }
                return servicoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cargo: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateCargo(Cargo servico)
        {
            try
            {
                Open();
                string updateCargo = @"UPDATE CARGO SET dsCargo = @dsCargo, dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateCargo, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", servico.Id);
                sql.Parameters.AddWithValue("@dsCargo", servico.dsCargo.ToUpper());               
                sql.Parameters.AddWithValue("@dtUltAlteracao", servico.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Cargo: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool ExcluirCargo(int Id)
        {
            try
            {
                Open();
                string deleteCargo = "DELETE FROM CARGO WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteCargo, sqlconnection);
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
                throw new Exception("Erro ao excluir Cargo: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}