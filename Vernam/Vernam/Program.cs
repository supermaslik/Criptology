using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vernam
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMethod();
            Console.ReadKey();

        }

        static void MainMethod()
        {
            string alph = "абвгдежзийклмнопрстуфхцчшыьэюя -.";
            string text = "тут можно найти какой-то текст";
            string key = "ключ";

            var pad = new OnTimePad(alph);
            string encrypt = pad.Crypt(text, key);
            string decrypt = pad.Crypt(encrypt, key);

            Console.WriteLine("Оригинал: " + text);
            Console.WriteLine("Шифротекст: " + encrypt);
            Console.WriteLine("Расшифровка: " + decrypt);
        }
    }

    class OnTimePad
    {
        Dictionary<char, int> alph = new Dictionary<char, int>();
        Dictionary<int, char> alph_r = new Dictionary<int, char>();

        public OnTimePad(IEnumerable<char> Alphabet)
        {
            int i = 0;
            foreach (char c in Alphabet)
            {
                alph.Add(c, i);
                alph_r.Add(i++, c);
            }
        }
        /// <summary>
        /// Версия с оператором xor
        ///
        /// Желательно, чтобы длина алфавита была степень двойки (например 32)
        /// </summary>
        public string Crypt(string Text, string Key)
        {
            char[] key = Key.ToCharArray();
            char[] text = Text.ToCharArray();
            var sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                int ind;
                if (alph.TryGetValue(text[i], out ind))
                {
                    sb.Append(alph_r[(ind ^ alph[key[i % key.Length]]) % alph.Count]);
                }
            }

            return sb.ToString();
        }
    }
}
