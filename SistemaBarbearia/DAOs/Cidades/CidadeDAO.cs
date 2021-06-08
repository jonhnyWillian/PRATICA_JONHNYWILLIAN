﻿using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.ViewModels.Cidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Cidades
{
    public class CidadeDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertCidade(Cidade cidade)
        {

            try
            {
                Open();
                string insertCidade = @"INSERT INTO CIDADE (nmCidade, ddd, dtCadastro) VALUES(@nmCidade, @ddd, @dtCadastro)";
                SQL = new SqlCommand(insertCidade, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmCidade", cidade.nmCidade.ToUpper());
                SQL.Parameters.AddWithValue("@ddd", cidade.DDD.ToUpper());
                SQL.Parameters.AddWithValue("@dtCadastro", cidade.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateCidade(Cidade cidade)
        {
            try
            {
                Open();
                string updateCidade = @"UPDATE CIDADE SET nmCidade = @nmCidade, ddd = @ddd, dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateCidade, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", cidade.Id);
                sql.Parameters.AddWithValue("@nmCidade", cidade.nmCidade.ToUpper());
                sql.Parameters.AddWithValue("@ddd", cidade.DDD.ToUpper());
                sql.Parameters.AddWithValue("@dtCadastro", cidade.dtCadastro);
                sql.Parameters.AddWithValue("@dtUltAlteracao", cidade.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteCidade(int Id)
        {
            try
            {
                Open();
                string deletePais = "DELETE FROM CIDADE WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deletePais, sqlconnection);
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
                throw new Exception("Erro ao excluir Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        public IEnumerable<Cidade> SelecionarCidade()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM CIDADE", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Cidade>();
                while (Dr.Read())
                {
                    var cidade = new Cidade()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        nmCidade = Convert.ToString(Dr["nmCidade"]),
                        DDD = Convert.ToString(Dr["ddd"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(cidade);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public CidadeVM GetCidade(int? Id)
        {
            try
            {
                Open();
                var cidadeVM = new CidadeVM();
                string selectEditCidade = @"SELECT* FROM CIDADE WHERE id =" + Id;
                SQL = new SqlCommand(selectEditCidade, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    cidadeVM.Id = Convert.ToInt32(Dr["id"]);
                    cidadeVM.nmCidade = Dr["nmCidade"].ToString();
                    cidadeVM.DDD = Dr["ddd"].ToString();
                    cidadeVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    
                    cidadeVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);


                }
                return cidadeVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

       
    }
}