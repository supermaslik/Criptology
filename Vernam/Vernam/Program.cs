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

        static void MainMethod(string text = "тут находится какой-то текст text check", string key = "ключ")
        {
            string alph = "абвгдежзийклмнопрстуфхцчшыьэюя -.";
            text = text.CheckText(alph);
            key = key.CheckText(alph);



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
                else
                    throw new ArgumentException();
            }

            return sb.ToString();
        }
    }

    public static class StringHandler {

        public static string CheckText(this string text, string alph)
        {
            text = text.ToLower();

            StringBuilder stringToReturn;
            StringBuilder untraceChars;
            CreateValidString(text, alph, out stringToReturn, out untraceChars);


            if (untraceChars.ToString().Length != 0)
                ShowUntraceChars(untraceChars.ToString());

            return stringToReturn.ToString();

        }
        private static void CreateValidString(string text, string alph, out StringBuilder stringToReturn, out StringBuilder untraceChars)
        {
            stringToReturn = new StringBuilder();
            untraceChars = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
                if (alph.Contains(text[i]))
                    stringToReturn.Append(text[i]);
                else
                    untraceChars.Append(text[i]);
        }
        private static void ShowUntraceChars(string UntraceChars)
        {
            Console.WriteLine("В введённом тексте были найдены невалидные символы: ");
            for(int i = 0; i < UntraceChars.Length; i++)
            {
                Console.Write(UntraceChars[i] + " ");
            }
            Console.WriteLine("\nВ последствии они будут выкинуты из текста");
            Console.WriteLine();
        }
    }
}
