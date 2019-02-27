using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Documents;
using static Locker.LockerMain;
using static Locker.MainPage;

namespace Locker
{
    class Db
    {
        private SQLiteConnection dbCon;

        public Db()
        {
            try
            {
                dbCon = new SQLiteConnection("Data Source=locker.db");
                dbCon.Open();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public Boolean VerifyPassword(string username, string password)
        {
            Boolean verified = false;

            string sql = "SELECT * FROM admin WHERE username = '" + username + "' AND password = '" + password + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbCon);
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                verified = true;
            }

            return verified;
        }

        public ArrayList SearchResults(string chars)
        {
            ArrayList results = new ArrayList();
            results.Clear();
            string search = chars + "%";

            string sql = "SELECT Id, website, username, email, change " +
                         "FROM logins ";
            if (!chars.Equals(""))
            {
                sql = sql + "WHERE website LIKE '" + search + "'";
            }
            SQLiteCommand cmd = new SQLiteCommand(sql, dbCon);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var result = new WebsiteInfo();

                result.Id = (long)reader["id"];
                result.Website = (string)reader["website"];
                result.Username = (string)reader["username"];
                result.Email = (string)reader["email"];
                result.ChangeDate = (string)reader["change"];

                results.Add(result);
            }

            return results;
        }

        public void ChangeUsername(string name, long id)
        {
            string sql = "UPDATE logins SET username = '" + name + "' " +
                         "WHERE Id = " + id;
            CmdExecution(sql);
        }

        public void ChangeWebsite(string name, long id)
        {
            string sql = "UPDATE logins SET website = '" + name + "' " +
                         "WHERE Id = " + id;
            CmdExecution(sql);
        }

        public void ChangeEmail(string email, long id)
        {
            string sql = "UPDATE logins SET email = '" + email + "' " +
                         "WHERE Id = " + id;
            CmdExecution(sql);
        }

        private void CmdExecution(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, dbCon);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
