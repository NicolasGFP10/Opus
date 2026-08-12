using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class CidadeDAO
    {
        // =====================================================
        // LISTAR CIDADES DE UM ESTADO
        // Usado pelo Autônomo
        // =====================================================

        public List<Cidade> ListarCidades(int estado)
        {
            List<Cidade> lista = new List<Cidade>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT cid_ID,
                                      cid_nome
                               FROM cidade
                               WHERE est_ID = @estado
                               ORDER BY cid_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@estado", estado);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cidade cidade = new Cidade();

                    cidade.ID = reader.GetInt32("cid_ID");
                    cidade.Nome = reader.GetString("cid_nome");

                    lista.Add(cidade);
                }
            }

            return lista;
        }

        // =====================================================
        // LISTAR TODAS AS CIDADES
        // Usado pelo Moderador
        // =====================================================

        public List<CidadeView> ListarTodasCidades()
        {
            List<CidadeView> lista = new List<CidadeView>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT
                                    c.cid_ID,
                                    c.cid_nome,
                                    e.est_nome

                               FROM cidade c

                               INNER JOIN estado e
                               ON c.est_ID = e.est_ID

                               ORDER BY
                                    e.est_nome,
                                    c.cid_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CidadeView cidade = new CidadeView();

                    cidade.ID = Convert.ToInt32(reader["cid_ID"]);
                    cidade.Cidade = reader["cid_nome"].ToString();
                    cidade.Estado = reader["est_nome"].ToString();

                    lista.Add(cidade);
                }
            }

            return lista;
        }

        // =====================================================
        // VERIFICA SE CIDADE EXISTE
        // =====================================================

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

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // =====================================================
        // CADASTRAR CIDADE
        // =====================================================

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

                    cmd.Parameters.AddWithValue(
                        "@nome",
                        cidade.Nome);

                    cmd.Parameters.AddWithValue(
                        "@estado",
                        cidade.EstadoID);

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

        // =====================================================
        // EXCLUIR CIDADE
        // =====================================================

        public int ExcluirCidade(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"DELETE FROM cidade
                                   WHERE cid_ID = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue(
                        "@id",
                        id);

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