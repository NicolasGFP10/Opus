using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class ServicoDAO
    {
        public bool ValidarServico(string nome)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                       FROM servico
                       WHERE ser_nome = @nome;";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@nome", nome);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        public int CadastrarServico(Model.Servico servico)
        {
            try
            {
                if (ValidarServico(servico.Nome))
                {
                    return 409;
                }

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO servico
                                   (ser_nome)
                                   VALUES
                                   (@nome)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@nome", servico.Nome);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public List<Servico> ListarServicos()
        {
            List<Servico> lista = new List<Servico>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql =
                @"SELECT ser_ID,
                 ser_nome
          FROM servico
          ORDER BY ser_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Servico servico = new Servico();

                    servico.ID = Convert.ToInt32(reader["ser_ID"]);
                    servico.Nome = reader["ser_nome"].ToString();

                    lista.Add(servico);
                }
            }

            return lista;
        }

        public int ExcluirServico(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"DELETE FROM servico
                           WHERE ser_ID = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 500;
            }
        }
    }
}