using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Automats
{
    internal class NKFAutomat
    {
        public int countOfStates;
        public int countOfLetters;
        public string[] letters;
        Dictionary<string, Dictionary<string, string>> NFKautomat = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, Dictionary<string, string>> automat = new Dictionary<string, Dictionary<string, string>>();
        public string fState = " ";
        public List<string> lState = new List<string>();

        public NKFAutomat() { }
        public NKFAutomat(string Path)
        {
            using (StreamReader file = new StreamReader(Path))
            {
                while (!file.EndOfStream)
                {
                    string str = file.ReadLine();
                    countOfLetters = int.Parse(str);

                    str = file.ReadLine();
                    letters = str.Split(' ');

                    str = file.ReadLine();
                    countOfStates = int.Parse(str);

                    for (int i = 0; i < countOfStates; i++)
                    {
                        string State = " ";
                        Dictionary<string, string> w = new Dictionary<string, string>();
                        str = file.ReadLine();
                        if (str.StartsWith("->*"))
                        {
                            fState = str[3].ToString();
                            lState.Add(str[3].ToString());
                            State = str[3].ToString(); ;
                        }
                        else
                        {
                            if (str.StartsWith("->"))
                            {
                                fState = str[2].ToString();
                                State = str[2].ToString();
                            }
                            else
                            {
                                if (str.StartsWith("*"))
                                {
                                    lState.Add(str[1].ToString());
                                    State = str[1].ToString();
                                }
                                else
                                {
                                    State = str;
                                }
                            }
                        }

                        for (int k = 0; k < 2; k++)
                        {
                            str = file.ReadLine();
                            string[] words = str.Split(' ');
                            w.Add(words[0], words[1]);
                        }
                        NFKautomat.Add(State, w);
                    }
                }
            }
        }

        // Вывод недетминированного автомата
        public void ShowNFKAutomat()
        {
            foreach (var w in letters)
            {
                Console.Write("     \t{0}", w);
            }
            Console.WriteLine();

            foreach (var state in NFKautomat)
            {
                if (state.Key == fState && lState.Contains(state.Key))
                {
                    Console.Write("->*{0}: ", state.Key);
                }
                else
                {
                    if (state.Key == fState)
                    {
                        Console.Write("->{0}: ", state.Key);
                    }
                    else
                    {
                        if (lState.Contains(state.Key))
                        {
                            Console.Write(" *{0}: ", state.Key);
                        }
                        else
                        {
                            Console.Write("  {0}: ", state.Key);
                        }
                    }
                }
                foreach (var way in state.Value)
                {
                    Console.Write("\t{0}", way.Value);
                }
                Console.WriteLine();
            }
        }

        public void startNKFautomat(string word)
        {
            Queue<string> qWord = new Queue<string>();
            for (int i = 0; i < word.Length; i++)
            {
                qWord.Enqueue(word[i].ToString());
            }
            Queue<string> q = new Queue<string>();
            string letter = "";
            int k = 0;
            q.Enqueue(fState);
            string buf = "";
            while (q.Count != 0)
            {
                string el = q.Dequeue();
                if (k == 0)
                {
                    letter = qWord.Dequeue();
                }
                k--;
                foreach (var state in NFKautomat)
                {
                    if (state.Key == el)
                    {
                        foreach (var way in state.Value)
                        {
                            if (way.Key.Equals(letter))
                            {
                                if (way.Value.Length > 1)
                                {
                                    string[] splitEl = new string[way.Value.Length];
                                    for (int j = 0; j < splitEl.Length; j++)
                                    {
                                        splitEl[j] = way.Value[j].ToString();
                                    }

                                    foreach (var element in splitEl)
                                    {
                                        q.Enqueue(element);
                                    }
                                    k += splitEl.Length;
                                }
                                else
                                {
                                    q.Enqueue(way.Value);
                                    buf = way.Value;
                                }
                                Console.WriteLine("{0} {1} - > {2}", el, letter, buf);
                            }
                        }
                    }
                }
            }

            if (LStateContainsState(buf, lState))
            {
                Console.WriteLine("The word is appropriate!");
            }
            else
            {
                Console.WriteLine("The word is not appropriate!");
            }
        }

        //Перевод из недетерминированного автомата в детерминированный
        public void TranslateNFKToKDA()
        {
            Queue<string> q = new Queue<string>();
            List<string> listCopy = new List<string>();
            Dictionary<string, string> g = new Dictionary<string, string>();
            string k1 = "";
            string k2 = "";
            foreach (var state in NFKautomat)
            {
                if (state.Key.Equals(fState))
                {
                    foreach (var way in state.Value)
                    {
                        q.Enqueue(way.Value);
                        listCopy.Add(way.Value);
                        if (way.Key == "r")
                        {
                            k1 += way.Value;
                        }
                        if (way.Key == "b")
                        {
                            k2 += way.Value;
                        }
                    }
                }
            }
            k1 = GetUniqueAndSortString(k1);
            k2 = GetUniqueAndSortString(k2);
            g.Add("r", k1);
            g.Add("b", k2);
            automat.Add(fState, g);

            while (q.Count != 0)
            {
                g = new Dictionary<string, string>();
                k1 = "";
                k2 = "";
                string el = q.Dequeue();
                foreach (var state in NFKautomat)
                {
                    if (el.Contains(state.Key))
                    {
                        foreach (var way in state.Value)
                        {
                            if (way.Key == "r")
                            {
                                k1 += way.Value;
                            }
                            if (way.Key == "b")
                            {
                                k2 += way.Value;
                            }
                        }
                    }
                }
                k1 = GetUniqueAndSortString(k1);
                k2 = GetUniqueAndSortString(k2);
                if (!q.Contains(k1) && !listCopy.Contains(k1))
                {
                    q.Enqueue(k1);
                    listCopy.Add(k1);
                }
                if (!q.Contains(k2) && !listCopy.Contains(k2))
                {
                    q.Enqueue(k2);
                    listCopy.Add(k2);
                }
                g.Add("r", k1);
                g.Add("b", k2);
                automat.Add(el, g);
            }
        }

        //Превращение строки в сортированную и уникальную по элементам строку
        public string GetUniqueAndSortString(string key)
        {
            List<int> newEl = new List<int>();
            for (int i = 0; i < key.Length; i++)
            {
                newEl.Add(key[i]);
            }

            List<int> noDupes = newEl.Distinct().ToList();
            noDupes.Sort();

            char[] str = new char[noDupes.Count];
            for (int i = 0; i < noDupes.Count; i++)
            {
                str[i] = Convert.ToChar(noDupes[i]);
            }
            string s = new string(str);
            return s;
        }

        //Вывод детерминированного автомата
        public void ShowTransletionAutomat()
        {
            foreach (var w in letters)
            {
                Console.Write("\t     {0}", w);
            }
            Console.WriteLine();

            foreach (var state in automat)
            {
                if (state.Key == fState && lState.Contains(state.Key))
                {
                    Console.Write("->*{0,5}:", state.Key);
                }
                else
                {
                    if (state.Key == fState)
                    {
                        Console.Write("->{0,5}:", state.Key);
                    }
                    else
                    {
                        if (LStateContainsState(state.Key, lState))
                        {
                            Console.Write("* {0,5}:", state.Key);
                        }
                        else
                        {
                            Console.Write("  {0,5}:", state.Key);
                        }
                    }
                }
                foreach (var way in state.Value)
                {
                    Console.Write("{0,6}  ", way.Value);
                }
                Console.WriteLine();
            }
        }

        //Проверка на содержание конечного состояния в новом автомате
        public bool LStateContainsState(string key, List<string> lState)
        {
            bool flag = false;
            foreach (var s in lState)
            {
                if (key.Contains(s))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public void StartAutomat(string word)
        {
            string fstate = fState;
            string buf = "";
            for (int i = 0; i < word.Length; i++)
            {
                string letter = word[i].ToString();
                foreach (var state in automat)
                {
                    if (state.Key == fstate)
                    {
                        foreach (var way in state.Value)
                        {
                            if (way.Key.Equals(letter))
                            {
                                buf = way.Value;
                                Console.WriteLine("{0} {1} - > {2}", fstate, word[i], buf);
                            }
                        }
                    }
                }
                fstate = buf;
            }

            if (LStateContainsState(fstate, lState))
            {
                Console.WriteLine("The word is appropriate!");
            }
            else
            {
                Console.WriteLine("The word is not appropriate!");
            }
        }
    }
}
