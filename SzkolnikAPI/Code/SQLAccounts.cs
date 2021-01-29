using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SzkolnikAPI.Code
{
    public static class SQLAccounts
    {
        static string path = "Data Source=./Database/Szkolnik.db;Version=3;";

        public static Account GetAccount(string login)
        {
            SQLiteConnection connection = new SQLiteConnection(path);
            connection.Open();

            string question = string.Format(@"SELECT * FROM Accounts WHERE login=""{0}"" ", login);

            SQLiteCommand cmd = new SQLiteCommand(question, connection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            Account final = new Account();

            while (rdr.Read())
            {
                final = new Account()
                {
                    login = rdr.GetString(1),
                    password = rdr.GetString(2),
                    token = rdr[3].GetType() == typeof(DBNull) ? "null" : rdr.GetString(3),
                };
            }

            return final;
        }

        public static void NewAccount(Account newtobase)
        {
            var con = new SQLiteConnection(path);
            con.Open();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "INSERT INTO Accounts(login, password, token) VALUES(@login, @password, @token);";
            cmd.Parameters.AddWithValue("@login", newtobase.login);
            cmd.Parameters.AddWithValue("@password", newtobase.password);
            cmd.Parameters.AddWithValue("@token", newtobase.password);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public static void UpdateToken(string login, string token)
        {
            var con = new SQLiteConnection(path);
            con.Open();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = string.Format(@"UPDATE Accounts SET token = ""{0}"" WHERE login = ""{1}""", token, login);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

    }
}
