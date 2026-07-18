using MySqlConnector;
using Opus.Data;
using Opus.Model;
using Opus.View.Telas.Moderador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class RegiaoDAO
    {

        public bool RegiaoExiste(string estado, string cidade)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                       FROM regiao
                       WHERE reg_estado = @estado
                          AND reg_cidade = @cidade;";


                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@cidade", cidade);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        public int CadastrarRegiao(Model.Regiao regiao)
        {
            try
            {

                if (RegiaoExiste(regiao.Estado, regiao.Cidade) == true)
                {
                    return 409;
                }

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO regiao (reg_estado, reg_cidade)
                               VALUES (@estado, @cidade);";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@estado", regiao.Estado);
                    cmd.Parameters.AddWithValue("@cidade", regiao.Cidade);

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

        public List<Model.Regiao> ListarRegioes()
        {
            List<Model.Regiao> lista = new List<Model.Regiao>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql =
                @"SELECT reg_ID,
                reg_estado,
                reg_cidade
                FROM regiao
                ORDER BY reg_estado DESC";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Model.Regiao regiao = new Model.Regiao();

                    regiao.ID = Convert.ToInt32(reader["reg_ID"]);
                    regiao.Estado = reader["reg_estado"].ToString();
                    regiao.Cidade = reader["reg_cidade"].ToString();

                    lista.Add(regiao);
                }
            }

            return lista;
        }

        public int EditarRegiao(Model.Regiao regiao)
        {
            if (RegiaoExiste(regiao.Estado, regiao.Cidade))
            {
                return 409;
            }

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"UPDATE regiao 
                SET reg_estado = @estado,
                reg_cidade = @cidade
                WHERE reg_id = @id;";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", regiao.ID);
                cmd.Parameters.AddWithValue("@estado", regiao.Estado);
                cmd.Parameters.AddWithValue("@cidade", regiao.Cidade);

                cmd.ExecuteNonQuery();
            }

            return 200;
        }

        public void ExcluirRegiao(int id)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"DELETE FROM regiao WHERE reg_id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}