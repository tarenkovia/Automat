using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Automat
    {
        public int countOfStates;
        public int countOfLetters;
        public string[] letters;
        Dictionary<int, Dictionary<int, string>> automat = new Dictionary<int, Dictionary<int, string>>();
        public Automat() { }   
        public Automat(string Path)
        {
            using(StreamReader file = new StreamReader(Path))
            {
                while(!file.EndOfStream)
                {
                    string str = file.ReadLine();
                    countOfLetters = int.Parse(str);
                    str = file.ReadLine();
                    letters = str.Split(' ');
                    str = file.ReadLine();
                    countOfStates = int.Parse(str);
                    for(int i = 0; i < countOfStates; i++)
                    {
                        Dictionary<int, string> w = new Dictionary<int, string>();
                        str = file.ReadLine();
                        int state = int.Parse(str);
                        str = file.ReadLine();
                        string[] words = str.Split(' ');
                        for(int j = 0;j < words.Length;j += 2)
                        {
                            w.Add(int.Parse(words[j]), words[j + 1]);
                        }
                        automat.Add(state, w);
                    }
                }
            }
        }

        public void ShowAutomat()
        {
            foreach(var w in letters)
            {
                Console.Write("     {0}", w);
            }
            Console.WriteLine();
            foreach (var state in automat)
            {
                if (state.Key == 1)
                {
                    Console.Write(" *{0}: ", state.Key);
                }
                else
                {
                    if(state.Key == 7) {
                        Console.Write("->{0}: ", state.Key);
                    }
                    else
                    {
                        Console.Write("  {0}: ", state.Key);
                    }
                }
                foreach(var way in state.Value)
                {
                    if(way.Value == "x")
                    {
                        Console.Write("{0}     {0}", way.Key);
                    }
                    else
                    {
                        Console.Write("{0}     ", way.Key);
                    }
                }
                Console.WriteLine();
            }
        }

        public void StartAutomat(string word)
        {
            int fstate = 1;
            int buf = 0;
            for (int i = 0; i < word.Length; i++) 
            {
                string letter = word[i].ToString();
                foreach (var state in automat)
                {
                    if (state.Key == fstate)
                    {
                        foreach (var way in state.Value)
                        {
                            if (way.Value.Equals(letter) || way.Value == "x")
                            {
                                buf = way.Key;
                                Console.WriteLine("{0} {1} - > {2}", fstate, word[i], buf);
                            }
                        }
                    }
                }
                fstate = buf;
            }

            if(fstate == 7)
            {
                Console.WriteLine("Слово подходит!");
            }
            else
            {
                Console.WriteLine("Слово не подходит!");
            }
        }
    }
}
