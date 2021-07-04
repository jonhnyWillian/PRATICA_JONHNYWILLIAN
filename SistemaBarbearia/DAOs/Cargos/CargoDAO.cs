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

        #region INSERT UPDATE DELETE
        public bool InsertCargo(Cargo cargos)
        {

            try
            {
                Open();
                string insertCargo = @"INSERT INTO CARGO (dsCargo, flSituacao ,dtCadastro) VALUES(@dsCargo, @flSituacao, @dtCadastro)";
                SQL = new SqlCommand(insertCargo, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsCargo", cargos.dsCargo.ToUpper());
                SQL.Parameters.AddWithValue("@flSituacao", cargos.flSituacao.ToUpper());
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

        public bool UpdateCargo(Cargo cargo)
        {
            try
            {
                Open();
                string updateCargo = @"UPDATE CARGO SET dsCargo = @dsCargo, flSituacao = @flSituacao , dtUltAlteracao = @dtUltAlteracao  WHERE IdCargo = @id";
                SqlCommand sql = new SqlCommand(updateCargo, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", cargo.IdCargo);
                sql.Parameters.AddWithValue("@dsCargo", cargo.dsCargo.ToUpper());
                sql.Parameters.AddWithValue("@flSituacao", cargo.flSituacao.ToUpper());
                sql.Parameters.AddWithValue("@dtUltAlteracao", cargo.dtUltAlteracao = DateTime.Now);


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

        public bool DeleteCargo(int Id)
        {
            try
            {
                Open();
                string deleteCargo = "DELETE FROM CARGO WHERE IdCargo = @Id";
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

        #endregion

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
                        IdCargo = Convert.ToInt32(Dr["IdCargo"]),
                        dsCargo = Convert.ToString(Dr["dsCargo"]), 
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
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
                var cargoVM = new CargoVM();
                string selectEditCargo = @"SELECT* FROM CARGO WHERE IdCargo =" + Id;
                SQL = new SqlCommand(selectEditCargo, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    cargoVM.IdCargo = Convert.ToInt32(Dr["IdCargo"]);
                    cargoVM.dsCargo = Dr["dsCargo"].ToString();
                    cargoVM.flSituacao = Dr["flSituacao"].ToString();
                    cargoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    cargoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return cargoVM;
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

        protected string BuscarCargo(int? IdCargo, string dsCargo)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (IdCargo != null)
            {
                where = "WHERE IdCargo = " + IdCargo;
            }
            if (!string.IsNullOrEmpty(dsCargo))
            {
                var query = dsCargo.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsCargo LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM Cargo " + where;
            return sqlSelectPais;
        }

        public List<SelectCargoVM> SelectCargo(int? Id, string Text)
        {
            try
            {

                var sqlSelectCargo = this.BuscarCargo(Id, Text);
                Open();
                SQL = new SqlCommand(sqlSelectCargo, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectCargoVM>();

                while (Dr.Read())
                {
                    var cargo = new SelectCargoVM
                    {
                        Id = Convert.ToInt32(Dr["IdCargo"]),
                        Text = Convert.ToString(Dr["dsCargo"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };

                    list.Add(cargo);
                }

                return list;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                Close();
            }
        }

    }
}