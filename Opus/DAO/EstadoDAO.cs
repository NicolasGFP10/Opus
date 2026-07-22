using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System.Collections.Generic;

namespace Opus.DAO
{
    public class EstadoDAO
    {
        public List<Estado> ListarEstados()
        {
            List<Estado> lista = new List<Estado>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT est_ID,
                                      est_nome
                               FROM estado
                               ORDER BY est_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Estado estado = new Estado();

                    estado.ID = reader.GetInt32("est_ID");
                    estado.Nome = reader.GetString("est_nome");

                    lista.Add(estado);
                }
            }

            return lista;
        }

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

                return System.Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

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
            catch
            {
                return 500;
            }
        }

        public int ExcluirEstado(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"DELETE FROM estado
                                   WHERE est_ID=@id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return 200;
            }
            catch
            {
                return 500;
            }
        }
    }
}