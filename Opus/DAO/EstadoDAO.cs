using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class EstadoDAO
    {

        //=========================
        // Verifica se já existe
        //=========================

        public bool EstadoExiste(string nome)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                               FROM estado
                               WHERE est_nome = @nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@nome", nome);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        //=========================
        // Cadastro
        //=========================

        public int CadastrarEstado(Estado estado)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO estado(est_nome)
                                   VALUES(@nome)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@nome", estado.Nome);

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
        // Listagem
        //=========================

        public List<Estado> ListarEstados()
        {
            List<Estado> lista = new List<Estado>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT *
                               FROM estado
                               ORDER BY est_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Estado estado = new Estado();

                    estado.ID = Convert.ToInt32(reader["est_ID"]);
                    estado.Nome = reader["est_nome"].ToString();

                    lista.Add(estado);
                }
            }

            return lista;
        }

        //=========================
        // Excluir
        //=========================

        public int ExcluirEstado(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"DELETE
                                   FROM estado
                                   WHERE est_ID = @id";

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