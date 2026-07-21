using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class CidadeDAO
    {

        //=========================
        // Verifica se já existe
        //=========================

        public bool CidadeExiste(string nome, int estado)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                               FROM cidade
                               WHERE cid_nome = @nome
                               AND est_ID = @estado";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@estado", estado);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        //=========================
        // Cadastro
        //=========================

        public int CadastrarCidade(Cidade cidade)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO cidade
                                   (cid_nome, est_ID)
                                   VALUES
                                   (@nome, @estado)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@nome", cidade.Nome);
                    cmd.Parameters.AddWithValue("@estado", cidade.EstadoID);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

                return 500;
            }
        }

        //=========================
        // Lista cidades de um Estado
        //=========================

        public List<Cidade> ListarCidades(int estado)
        {
            List<Cidade> lista = new List<Cidade>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT
                               cid_ID,
                               cid_nome,
                               est_ID
                               FROM cidade
                               WHERE est_ID = @estado
                               ORDER BY cid_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@estado", estado);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cidade cidade = new Cidade();

                    cidade.ID = Convert.ToInt32(reader["cid_ID"]);
                    cidade.Nome = reader["cid_nome"].ToString();
                    cidade.EstadoID = Convert.ToInt32(reader["est_ID"]);

                    lista.Add(cidade);
                }
            }

            return lista;
        }

        //=========================
        // Excluir
        //=========================

        public int ExcluirCidade(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"DELETE
                                   FROM cidade
                                   WHERE cid_ID = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

                return 500;
            }
        }
    }
}