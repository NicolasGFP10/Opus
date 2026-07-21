using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Web;

namespace Opus.DAO
{
    public class AutonomoCidadeDAO
    {

        //=========================
        // Verifica se já cadastrou
        //=========================

        public bool CidadeJaCadastrada(int cidade, int autonomo)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                               FROM autonomo_cidade
                               WHERE cid_ID = @cidade
                               AND aut_ID = @autonomo";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@cidade", cidade);
                cmd.Parameters.AddWithValue("@autonomo", autonomo);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        //=========================
        // Cadastro
        //=========================

        public int AdicionarCidade(AutonomoCidade cidade)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO autonomo_cidade
                                   (cid_ID, aut_ID)
                                   VALUES
                                   (@cidade, @autonomo)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@cidade", cidade.CidadeID);
                    cmd.Parameters.AddWithValue("@autonomo", cidade.AutonomoID);

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
        // Lista as cidades do autônomo
        //=========================

        public List<CidadeView> ListarCidades(int autonomo)
        {
            List<CidadeView> lista = new List<CidadeView>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT

                            ac.cie_ID,
                            c.cid_nome,
                            e.est_nome

                            FROM autonomo_cidade ac

                            INNER JOIN cidade c
                            ON ac.cid_ID = c.cid_ID

                            INNER JOIN estado e
                            ON c.est_ID = e.est_ID

                            WHERE ac.aut_ID = @autonomo

                            ORDER BY
                            e.est_nome,
                            c.cid_nome";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@autonomo", autonomo);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CidadeView cidade = new CidadeView();

                    cidade.ID = Convert.ToInt32(reader["cie_ID"]);
                    cidade.Cidade = reader["cid_nome"].ToString();
                    cidade.Estado = reader["est_nome"].ToString();

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
                                   FROM autonomo_cidade
                                   WHERE cie_ID = @id";

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