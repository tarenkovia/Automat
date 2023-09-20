using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Automat
    {
        public int countOfStates;
        public int countOfLetters;
        public string[] letters;
        Dictionary<int, Dictionary<string, int>> automat = new Dictionary<int, Dictionary<string, int>>();
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
                        Dictionary<string, int> w = new Dictionary<string, int>();
                        str = file.ReadLine();
                        int state = int.Parse(str);
                        str = file.ReadLine();
                        string[] words = str.Split(' ');
                        for(int j = 0;j < words.Length;j += 2)
                        {
                            w.Add(words[j + 1], int.Parse(words[j]));
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
                    Console.Write("->{0}: ", state.Key);
                }
                else
                {
                    if(state.Key == 7) {
                        Console.Write(" *{0}: ", state.Key);
                    }
                    else
                    {
                        Console.Write("  {0}: ", state.Key);
                    }
                }
                foreach(var way in state.Value)
                {
                    Console.Write("{0}     ",way.Value);
                }
                Console.WriteLine();
            }
        }

        public bool ApprovedWord(string word)
        {
            bool flag = false;
            var wordLetters = word.ToCharArray();
            var uniqLetters = wordLetters.Select(x => x.ToString()).ToHashSet().ToArray();

            foreach ( var letter in uniqLetters)
            {
                if(letters.Any(x => x == letter)) 
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public void StartAutomat(string word)
        {
            if (ApprovedWord(word))
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

                if (fstate == 7)
                {
                    Console.WriteLine("The word is appropriate!");
                }
                else
                {
                    Console.WriteLine("The word is not appropriate!");
                }
            }
            else
            {
                Console.WriteLine("Uncorrect word!");
            }
        }
    }
}
