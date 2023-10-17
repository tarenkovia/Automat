using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Automats
{
    class Automat
    {
        public int countOfStates;
        public int countOfLetters;
        public string[] letters;
        Dictionary<int, Dictionary<string, int>> automat = new Dictionary<int, Dictionary<string, int>>();
        public int fState = 0;
        public List<int> lState = new List<int>();
        public Automat() { }
        public Automat(string Path)
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

                    string[] st = str.Split(' ');

                    for (int i = 0; i < countOfStates; i++)
                    {
                        int State = 0;
                        Dictionary<string, int> w = new Dictionary<string, int>();
                        str = file.ReadLine();
                        if (str.StartsWith("->*"))
                        {
                            fState = int.Parse(str[3].ToString());
                            lState.Add(int.Parse(str[3].ToString()));
                            State = int.Parse(str[3].ToString());
                        }
                        else
                        {
                            if (str.StartsWith("->"))
                            {
                                fState = int.Parse(str[2].ToString());
                                State = int.Parse(str[2].ToString());
                            }
                            else
                            {
                                if (str.StartsWith("*"))
                                {
                                    //lState.Add(str[1]);
                                    lState.Add(int.Parse(str[1].ToString()));
                                    State = int.Parse(str[1].ToString());
                                }
                                else
                                {
                                    State = int.Parse(str);
                                }
                            }
                        }


                        //string[] state = str.Split(" "); ;
                        //if (state.Length > 2)
                        //{
                        //    for (int j = 1; j < state.Length; j++)
                        //    {
                        //        if (state[j] == "->")
                        //        {
                        //            fState = int.Parse(state[i + j]);
                        //            State = int.Parse(state[j + 1]);
                        //        }
                        //        else
                        //        {
                        //            if (state[j] == "*")
                        //            {
                        //                lState.Add(int.Parse(state[i + j]));
                        //                State = int.Parse(state[j + 1]);
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    State = int.Parse(state[0]);
                        //}

                        str = file.ReadLine();
                        string[] words = str.Split(' ');
                        for (int j = 0; j < words.Length; j += 2)
                        {
                            w.Add(words[j + 1], int.Parse(words[j]));
                        }
                        automat.Add(State, w);
                    }
                }
            }
        }

        public void ShowAutomat()
        {
            foreach (var w in letters)
            {
                Console.Write("     {0}", w);
            }
            Console.WriteLine();
            foreach (var state in automat)
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
                    Console.Write("{0}     ", way.Value);
                }
                Console.WriteLine();

            }
        }

        //public bool ApprovedWord(string word)
        //{
        //    bool flag = false;
        //    var wordLetters = word.ToCharArray();
        //    var uniqLetters = wordLetters.Select(x => x.ToString()).ToHashSet().ToArray();

        //    foreach ( var letter in uniqLetters)
        //    {
        //        if(letters.Any(x => x == letter)) 
        //        {
        //            flag = true;
        //        }
        //        else
        //        {
        //            flag = false;
        //        }
        //    }

        //    return flag;
        //}

        public void StartAutomat(string word)
        {
            // if (!String.IsNullOrEmpty(word))
            //{
            //if (ApprovedWord(word))
            //{
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

            if (lState.Contains(fstate))
            {
                Console.WriteLine("The word is appropriate!");
            }
            else
            {
                Console.WriteLine("The word is not appropriate!");
            }
        }
        //    else
        //    {
        //        Console.WriteLine("Uncorrect word!");
        //    }
        //}
        //else
        //{
        //    if (lState.Contains(fState))
        //    {
        //        Console.WriteLine("The word is appropriate!");
        //    }
        //}
    }
}
