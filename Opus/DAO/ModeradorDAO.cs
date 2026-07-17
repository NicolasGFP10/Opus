using MySqlConnector;
using Opus.Data;
using Opus.Model;
using System;
using System.IO;
using System.Web;

namespace Opus.DAO
{
    public class ModeradorDAO
    {
        public void EntrarModerador(int id)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {

                    Usuario usuario = new Usuario();

                    conexao.Open();

                    string sql = @"SELECT mod_id FROM moderador WHERE usu_id = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int idMod = Convert.ToInt32(reader["mod_ID"]);

                        HttpContext.Current.Session["mod_ID"] = idMod;

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());

            }
        }

    }
}