﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.BancoDados;
using WpfTechPharma.Interfaces;

namespace WpfTechPharma.Modelos
{
    internal class ServicoDAO : IDAO<Servico>
    {
        private static Conexao conexao;

        public ServicoDAO() 
        {
            conexao = new Conexao();
        }

        public string Insert(Servico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "call cadastrar_servico " +
                    "(@nome, " +
                    "@valorVenda, " +
                    "@duracao, " +
                    "@tipo)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@valorVenda", t.ValorVenda);
                query.Parameters.AddWithValue("@duracao", t.Duracao);
                query.Parameters.AddWithValue("@tipo", t.Tipo);

                var result = (string)query.ExecuteScalar();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Update(Servico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText =
                    "update " +
                    "Servico " +
                    "set " +
                    "serv_nome = @nome, " +
                    "serv_valor_venda = @valorVenda, " +
                    "serv_duracao = @duracao, " +
                    "serv_tipo = @tipo " +
                    "where " +
                    "(serv_id = @id)";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@valorVenda", t.ValorVenda);
                query.Parameters.AddWithValue("@duracao", t.Duracao);
                query.Parameters.AddWithValue("@tipo", t.Tipo);
                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao atualizar o serviço. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Delete(Servico t)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "delete from Servico where (serv_id = @id)";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Erro ao remover o serviço. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public Servico GetById(int id)
        {
            try
            {
                var query = conexao.Query();
                query.CommandText = "select * from Servico where (serv_id = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum serviço foi encotrado!");
                }

                var servico = new Servico();

                while (reader.Read())
                {
                    servico.Id = AuxiliarDAO.GetInt(reader, "serv_id");
                    servico.Nome = AuxiliarDAO.GetString(reader, "serv_nome");
                    servico.ValorVenda = AuxiliarDAO.GetFloat(reader, "serv_valor_venda");
                    servico.Duracao = AuxiliarDAO.GetString(reader, "serv_duracao");
                    servico.Tipo = AuxiliarDAO.GetString(reader, "serv_tipo");
                }

                return servico;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }

        public List<Servico> List()
        {
            try
            {
                List<Servico> listaServico = new List<Servico>();

                var query = conexao.Query();
                query.CommandText = "select * from Servico";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum serviço foi encotrado!");
                }

                while (reader.Read())
                {
                    listaServico.Add(new Servico()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "serv_id"),
                        Nome = AuxiliarDAO.GetString(reader, "serv_nome"),
                        ValorVenda = AuxiliarDAO.GetFloat(reader, "serv_valor_venda"),
                        Duracao = AuxiliarDAO.GetString(reader, "serv_duracao"),
                        Tipo = AuxiliarDAO.GetString(reader, "serv_tipo")
                    });
                }

                return listaServico;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
