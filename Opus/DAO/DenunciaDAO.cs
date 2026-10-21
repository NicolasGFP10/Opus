using MySqlConnector;
using Opus.Data;
using Opus.Model;
using Opus.View.Telas.Moderador;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class DenunciaDAO
    {

        public int CadastrarDenuncia(Model.Denuncia denuncia)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO denuncia (den_titulo, den_descricao, den_categoria, den_status, den_ID_denunciante, den_ID_denunciado, den_data_envio) 
                                   VALUES (@Titulo, @Descricao, @Categoria, @Status, @Denunciante, @Denunciado, @Data);";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@Titulo", denuncia.Titulo);
                    cmd.Parameters.AddWithValue("@Descricao", denuncia.Descricao);
                    cmd.Parameters.AddWithValue("@Categoria", denuncia.Categoria);
                    cmd.Parameters.AddWithValue("@Status", denuncia.Status);
                    cmd.Parameters.AddWithValue("@Denunciante", denuncia.Denunciante);
                    cmd.Parameters.AddWithValue("@Denunciado", denuncia.Denunciado);
                    cmd.Parameters.AddWithValue("@Data", denuncia.Data);

                    cmd.ExecuteNonQuery();

                    int denunciaID = Convert.ToInt32(cmd.LastInsertedId);

                    return denunciaID;
                }
            }
            catch
            {
                return -500;
            }
        }

        public List<DenunciaView> ListarDenuncias(int filtro)
        {
            Dictionary<int, DenunciaView> denuncias = new Dictionary<int, DenunciaView>();

            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql;

                if (filtro == 0)
                {

                    sql = @"
            SELECT
                d.den_id,
                d.den_titulo,
                d.den_descricao,
                d.den_categoria,
                d.den_status,
                d.den_ID_denunciante,
                d.den_ID_denunciado,
                d.den_data_envio,

                u1.usu_nome AS nome_denunciante,
                u1.usu_imagem AS imagem_denunciante,

                u2.usu_nome AS nome_denunciado,
                u2.usu_imagem AS imagem_denunciado,

                f.fot_ID,
                f.fot_imagem

            FROM denuncia d

            INNER JOIN usuario u1
                ON d.den_ID_denunciante = u1.usu_ID

            INNER JOIN usuario u2
                ON d.den_ID_denunciado = u2.usu_ID

            LEFT JOIN foto_denuncia f
                ON d.den_id = f.den_id

            ORDER BY d.den_data_envio DESC, f.fot_ID;
        ";

                }
                else if (filtro == 1)
                {
                    sql = @"
                        SELECT
                        d.den_id,
                        d.den_titulo,
                        d.den_descricao,
                        d.den_categoria,
                        d.den_status,
                        d.den_ID_denunciante,
                        d.den_ID_denunciado,
                        d.den_data_envio,

                        u1.usu_nome AS nome_denunciante,
                        u1.usu_imagem AS imagem_denunciante,

                        u2.usu_nome AS nome_denunciado,
                        u2.usu_imagem AS imagem_denunciado,

                        f.fot_ID,
                        f.fot_imagem

                        FROM denuncia d

                        INNER JOIN usuario u1
                        ON d.den_ID_denunciante = u1.usu_ID

                        INNER JOIN usuario u2
                        ON d.den_ID_denunciado = u2.usu_ID

                        LEFT JOIN foto_denuncia f
                        ON d.den_id = f.den_id

                        WHERE d.den_status = 1

                        ORDER BY d.den_data_envio DESC, f.fot_ID;
                        ";
                }
                else
                {

                    sql = @"
                        SELECT
                        d.den_id,
                        d.den_titulo,
                        d.den_descricao,
                        d.den_categoria,
                        d.den_status,
                        d.den_ID_denunciante,
                        d.den_ID_denunciado,
                        d.den_data_envio,

                        u1.usu_nome AS nome_denunciante,
                        u1.usu_imagem AS imagem_denunciante,

                        u2.usu_nome AS nome_denunciado,
                        u2.usu_imagem AS imagem_denunciado,

                        f.fot_ID,
                        f.fot_imagem

                        FROM denuncia d

                        INNER JOIN usuario u1
                        ON d.den_ID_denunciante = u1.usu_ID

                        INNER JOIN usuario u2
                        ON d.den_ID_denunciado = u2.usu_ID

                        LEFT JOIN foto_denuncia f
                        ON d.den_id = f.den_id

                        WHERE d.den_status = 0

                        ORDER BY d.den_data_envio DESC, f.fot_ID;
                        ";
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int denunciaID = Convert.ToInt32(reader["den_id"]);

                        // Verifica se a denúncia já foi adicionada
                        if (!denuncias.ContainsKey(denunciaID))
                        {

                            string categoria, status;

                            switch (reader["den_categoria"])
                            {
                                case 1:
                                    categoria = "Assédio sexual";
                                    break;

                                case 2:
                                    categoria = "Discriminação";
                                    break;

                                case 3:
                                    categoria = "Ameaças ou violência";
                                    break;

                                case 4:
                                    categoria = "Linguagem ofensiva";
                                    break;

                                case 5:
                                    categoria = "Fraude ou golpe";
                                    break;

                                case 6:
                                    categoria = "Não fornecimento do código de avaliação";
                                    break;

                                case 7:
                                    categoria = "Perfil falso ou informações enganosas";
                                    break;

                                case 8:
                                    categoria = "Cobrança indevida";
                                    break;

                                case 9:
                                    categoria = "Uso indevido de dados pessoais";
                                    break;

                                case 10:
                                    categoria = "Spam ou publicidade indevida";
                                    break;

                                case 11:
                                    categoria = "Outro";
                                    break;

                                default:
                                    categoria = "Outro";
                                    break;

                            }

                            if (Convert.ToInt32(reader["den_status"]) == 1)
                            {
                                status = "Pendente";
                            }
                            else
                            {
                                status = "Resolvida";
                            }

                            DenunciaView denuncia = new DenunciaView
                            {
                                ID = denunciaID,

                                Titulo = reader["den_titulo"].ToString(),
                                Descricao = reader["den_descricao"].ToString(),

                                Categoria = categoria,

                                Status = status,

                                DenuncianteID = Convert.ToInt32(reader["den_ID_denunciante"]),

                                NomeDenunciante = reader["nome_denunciante"].ToString(),

                                ImagemDenunciante = reader["imagem_denunciante"].ToString(),

                                DenunciadoID = Convert.ToInt32(reader["den_ID_denunciado"]),

                                NomeDenunciado = reader["nome_denunciado"].ToString(),

                                ImagemDenunciado = reader["imagem_denunciado"].ToString(),

                                DataEnvio = Convert.ToDateTime(reader["den_data_envio"]),

                                Fotos = new List<FotoDenuncia>()
                            };

                            denuncias.Add(denunciaID, denuncia);
                        }

                        // Recupera a denúncia já cadastrada no Dictionary
                        DenunciaView denunciaAtual = denuncias[denunciaID];

                        // Adiciona a foto somente se existir
                        if (reader["fot_ID"] != DBNull.Value)
                        {
                            FotoDenuncia foto = new FotoDenuncia
                            {
                                DenunciaID = denunciaID,
                                CaminhoFoto =
                                    reader["fot_imagem"].ToString()
                            };

                            denunciaAtual.Fotos.Add(foto);
                        }
                    }
                }
            }

            return denuncias.Values.ToList();
        }

        public int AlterarStatus(int id, int status)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string sql;

                    conexao.Open();

                    if (status == 0)
                    {
                        sql = @"UPDATE denuncia SET den_status = 1 WHERE den_id = @id;";
                    }

                    else
                    {
                        sql = @"UPDATE denuncia SET den_status = 0 WHERE den_id = @id;";
                    }

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    return 200;
                }
            }
            catch
            {
                return 500;
            }
        }
    }
}