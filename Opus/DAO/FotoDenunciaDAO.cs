using Opus.Data;
using Opus.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.DAO
{
    public class FotoDenunciaDAO
    {
        public void Cadastrar(FotoDenuncia foto)
        {
            using (MySqlConnection conexao = Conexao.ObterConexao())
            {
                conexao.Open();

                string sql = @"
                    INSERT INTO foto_denuncia
                    (
                        fot_imagem,
                        den_id
                    )
                    VALUES
                    (
                        @imagem,
                        @id
                    );";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@imagem", foto.CaminhoFoto);
                cmd.Parameters.AddWithValue("@id", foto.DenunciaID);

                cmd.ExecuteNonQuery();
            }
        }
    }
}