using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class AutonomoDAO
    {

        public bool AutonomoExiste(string cnpj, string email, string telefone)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"SELECT COUNT(*)
                       FROM autonomo
                       WHERE aut_cnpj = @cnpj
                          OR aut_email_corp = @email
                          OR aut_telefone_corp = @telefone;";


                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@cnpj", cnpj);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefone", telefone);

                int quantidade = Convert.ToInt32(cmd.ExecuteScalar());

                return quantidade > 0;
            }
        }

        public int CadastrarAutonomo(Autonomo autonomo)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO autonomo
                                   (aut_CNPJ,
                                    aut_email_corp,
                                    aut_telefone_corp,
                                    aut_descricao,
                                    aut_data_cadastro,
                                    aut_status,
                                    usu_ID)

                                   VALUES
                                   (@cnpj,
                                    @email,
                                    @telefone,
                                    @descricao,
                                    @data,
                                    @status,
                                    @ID)";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@cnpj", autonomo.CNPJ);
                    cmd.Parameters.AddWithValue("@email", autonomo.EmailCorp);
                    cmd.Parameters.AddWithValue("@telefone", autonomo.TelefoneCorp);
                    cmd.Parameters.AddWithValue("@descricao", autonomo.Descricao);
                    cmd.Parameters.AddWithValue("@data", autonomo.DataCadastro);
                    cmd.Parameters.AddWithValue("@status", autonomo.Status);
                    cmd.Parameters.AddWithValue("@ID", autonomo.usu_ID);

                    cmd.ExecuteNonQuery();
                }

                return 200;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public void EntrarAutonomo(int id)
        {

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {

                conexao.Open();

                string sql = @"SELECT aut_ID, 
                               aut_email_corp, 
                               aut_telefone_corp,
                               aut_descricao
                               FROM autonomo WHERE
                               usu_ID = @id AND
                               aut_status = 1;";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int idAut = Convert.ToInt32(reader["aut_ID"]);
                    string emailCorp = reader["aut_email_corp"].ToString();
                    string telefoneCorp = reader["aut_telefone_corp"].ToString();
                    string descricao = reader["aut_descricao"].ToString();

                    HttpContext.Current.Session["aut_ID"] = idAut;
                    HttpContext.Current.Session["aut_email_corp"] = emailCorp;
                    HttpContext.Current.Session["aut_telefone_corp"] = telefoneCorp;
                    HttpContext.Current.Session["aut_descricao"] = descricao;

                }
            }
        }

        public int EditarAutonomo(Autonomo autonomo)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {

                    conexao.Open();

                    string sql = @"UPDATE autonomo
                                   SET aut_telefone_corp = @telefone,
                                       aut_email_corp = @email,
                                       aut_descricao = @descricao
                                       WHERE aut_ID = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@telefone", autonomo.TelefoneCorp);
                    cmd.Parameters.AddWithValue("@email", autonomo.EmailCorp);
                    cmd.Parameters.AddWithValue("@descricao", autonomo.Descricao);
                    cmd.Parameters.AddWithValue("@id", autonomo.ID);
                    cmd.ExecuteNonQuery();
                }
                return 200;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public void DesativarAutonomo(string id)
        {
            try
            {

                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"UPDATE autonomo
                               SET aut_status = 0
                               WHERE aut_ID = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public List<AutonomoPesquisaView> PesquisarAutonomos(int servicoID, int cidadeID)
        {
            List<AutonomoPesquisaView> lista = new List<AutonomoPesquisaView>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            SELECT
                a.aut_ID,
                u.usu_nome,
                u.usu_imagem,

                COALESCE(AVG(av.ava_nota), 0) AS nota_media,
                COUNT(av.ava_ID) AS quantidade_avaliacoes

            FROM autonomo a

            INNER JOIN usuario u
                ON a.usu_ID = u.usu_ID

            LEFT JOIN avaliacao av
                ON av.aut_ID = a.aut_ID

            WHERE a.aut_status = 1
              AND u.usu_status = 1

              AND EXISTS (
                  SELECT 1
                  FROM autonomo_servico ats
                  WHERE ats.aut_ID = a.aut_ID
                    AND ats.ser_ID = @servico
              )

              AND EXISTS (
                  SELECT 1
                  FROM autonomo_cidade atc
                  WHERE atc.aut_ID = a.aut_ID
                    AND atc.cid_ID = @cidade
              )

            GROUP BY
                a.aut_ID,
                u.usu_nome,
                u.usu_imagem

            ORDER BY nota_media DESC, quantidade_avaliacoes DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@servico", servicoID);
                    cmd.Parameters.AddWithValue("@cidade", cidadeID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AutonomoPesquisaView autonomo =
                                new AutonomoPesquisaView();

                            autonomo.ID = Convert.ToInt32(reader["aut_ID"]);

                            autonomo.Nome = reader["usu_nome"].ToString();

                            autonomo.Foto = "~/Uploads/Usuario/" + reader["usu_imagem"].ToString();

                            autonomo.NotaMedia = Convert.ToDecimal(reader["nota_media"]);

                            autonomo.QuantidadeAvaliacoes = Convert.ToInt32(reader["quantidade_avaliacoes"]);

                            lista.Add(autonomo);
                        }
                    }
                }
            }

            return lista;
        }

        public AutonomoPesquisaView BuscarPerfil(int autonomoID)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
            SELECT
                a.aut_ID,
                a.usu_ID,
                a.aut_descricao,
                u.usu_nome,
                u.usu_imagem,
                a.aut_telefone_corp,
                a.aut_email_corp,
                u.usu_telefone,

                COALESCE(AVG(av.ava_nota), 0) AS nota_media,
                COUNT(av.ava_ID) AS quantidade_avaliacoes FROM autonomo a
            
                INNER JOIN usuario u
                ON a.usu_ID = u.usu_ID
                
                LEFT JOIN avaliacao av
                ON av.aut_ID = a.aut_ID
            
                WHERE a.aut_ID = @autonomo
              AND a.aut_status = 1
              AND u.usu_status = 1
            GROUP BY
                a.aut_ID,
                a.aut_descricao,
                u.usu_nome,
                u.usu_imagem;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@autonomo", autonomoID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            string telefone;

                            if (string.IsNullOrEmpty(reader["aut_telefone_corp"].ToString()))
                            {

                                telefone = reader["usu_telefone"].ToString();

                            } else
                            {
                                
                                telefone = reader["aut_telefone_corp"].ToString();
                                
                            }

                            return new AutonomoPesquisaView
                            {
                                ID = Convert.ToInt32(reader["aut_ID"]),
                                Nome = reader["usu_nome"].ToString(),
                                Foto = reader["usu_imagem"].ToString(),
                                Descricao = reader["aut_descricao"].ToString(),
                                NotaMedia = Convert.ToDecimal(reader["nota_media"]),
                                QuantidadeAvaliacoes = Convert.ToInt32(reader["quantidade_avaliacoes"]),
                                Telefone = telefone,
                                Email = reader["aut_email_corp"].ToString(),
                                UsuarioID = Convert.ToInt32(reader["usu_ID"]),

                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}