using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Locker
{
    class Crypt
    {

        private List<char> CharPool()
        {
            List<char> charList = new List<char>();
            int passwordLength = 20;
            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();

            for (int i = 'a'; i <= 'z'; i++)
            {
                charList.Add((char)i);
            }

            for (int i = 'A'; i <= 'Z'; i++)
            {
                charList.Add((char)i);
            }

            for (int i = '0'; i <= '9'; i++)
            {
                charList.Add((char)i);
            }

            for (int i = '!'; i <= '?'; i++)
            {
                charList.Add((char)i);
            }

            for (int i = 0; i < passwordLength; i++)
            {
                int index = rnd.Next(0, charList.Count);
                builder.Append(charList[index]);
            }

            return charList;
        }

        private void ShuffleList(List<char> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                char value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private List<char> GeneratePassword()
        {
            List<char> pool = CharPool();
            ShuffleList(pool);

            List<char> password = new List<char>();
            int passwordLength = 20;

            for (int i = 0; i < passwordLength; i++)
            {
                password.Add(pool[i]);
            }

            return password;
        }

        public string ObfuscatedPassword()
        {
            List<char> password = GeneratePassword();
            List<char> pool = CharPool();
            StringBuilder builder = new StringBuilder();

            foreach (var item in password)
            {
                builder.Append(pool.IndexOf(item));
                builder.Append(" ");
            }

            return builder.ToString();
        }

        public string ClarifiedPassword(string pw)
        {
            List<char> pool = CharPool();
            StringBuilder builder = new StringBuilder();

            String[] trimmed = pw.Split(' ');

            foreach (var item in trimmed)
            {
                if (Int32.TryParse(item, out int index))
                {
                    builder.Append(pool[index]);
                }
            }

            MessageBox.Show(builder.ToString());
            return builder.ToString();

        }




    }
}
