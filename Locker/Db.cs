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
        private Dates date = new Dates();
        private Crypt crypt = new Crypt();

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

            string sql = string.Format("SELECT * FROM admin WHERE username = '{0}' AND password = '{1}'", username, password);
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

            string sql = "SELECT Id, website, username, email, change FROM logins ";
            if (!chars.Equals(""))
            {
                sql = sql + string.Format("WHERE website LIKE '{0}'", search);
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

        public string ReadUsername(long id, string username = "")
        {

            string sql = string.Format("SELECT username FROM logins WHERE Id = {0}", id);

            SQLiteCommand cmd = new SQLiteCommand(sql, dbCon);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                username = (string)reader["username"];
            }

            return username;
        }


        public void ChangeUsername(string name, long id)
        {
            string sql = string.Format("UPDATE logins SET username = '{0}' WHERE Id = {1}", name, id);
            CmdExecution(sql);
        }

        public void ChangeWebsite(string name, long id)
        {
            string sql = string.Format("UPDATE logins SET website = '{0}' WHERE Id = {1}", name, id);
            CmdExecution(sql);
        }

        public void ChangeEmail(string email, long id)
        {
            string sql = string.Format("UPDATE logins SET email = '{0}' WHERE Id = {1}", email, id);
            CmdExecution(sql);
        }

        public void AddLogin(string name, string username, string email)
        {
            string now = date.DateNow();
            string pw = crypt.ObfuscatedPassword();
            string sql = string.Format("INSERT INTO logins (website, username, email, change, password) " +
                                        "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", name, username, email, now, pw);
            
            CmdExecution(sql);
        }

        public void DeleteLogin(long id)
        {
            string sql = string.Format("DELETE FROM logins WHERE Id = {0}", id);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
