using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class DenunciaDAO
    {

        public int CadastrarDenuncia(Denuncia denuncia)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO Denuncias (Titulo, Descricao, Categoria, Status, Denunciante, Denunciado, Data) 
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