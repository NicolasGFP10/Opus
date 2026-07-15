using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opus.Data
{
    public class Conexao
    {
        private static string connectionString =
            "server=localhost;" +
            "database=OpusDB;" +
            "user=root;" +
            "password=ni21;" +
            "SslMode=None;";

        public static MySqlConnection ObterConexao()
        {
            return new MySqlConnection(connectionString);
        }
    }
}