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

            string sql = "SELECT t1.ID AS id, t1.website AS website, t2.username AS username, t2.email AS email, t2.change AS change " +
                         "FROM pages AS t1, logins AS t2 " +
                         "WHERE t1.ID = t2.websiteId ";
            if (!chars.Equals(""))
            {
                sql = sql + "AND website LIKE '" + search + "'";
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
                         "WHERE websiteId = " + id;

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
