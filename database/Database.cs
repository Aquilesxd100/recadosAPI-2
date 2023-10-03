using MySql.Data.MySqlClient;

namespace recados_api
{
    public class Database{
        private static string connStr = Env.dataBaseURL;
        public static MySqlConnection conexao = new MySqlConnection(connStr);

    }
}