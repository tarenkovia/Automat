namespace Lab1
{
    public class E_NKFAutomat
    {
        public int countOfStates;
        public int countOfLetters;
        public string[] letters;
        Dictionary<string, Dictionary<string, string>> EAutomat = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, Dictionary<string, string>> NFKautomat = new Dictionary<string, Dictionary<string, string>>();
        public string fState = " ";
        public List<string> lState = new List<string>();

        public E_NKFAutomat() { }
        public E_NKFAutomat(string Path)
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

                        for (int k = 0; k < countOfLetters; k++)
                        {
                            str = file.ReadLine();
                            string[] words = str.Split(' ');
                            w.Add(words[0], words[1]);
                        }
                        EAutomat.Add(State, w);
                    }
                }
            }
        }

        // Вывод e - недетминированного автомата
        public void ShowENFKAutomat()
        {
            foreach (var w in letters)
            {
                Console.Write("     \t{0}", w);
            }
            Console.WriteLine();

            foreach (var state in EAutomat)
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

        // общая идея:
        // первая функция просто находит по e-путям те состояни, которые приходят в конечное и добавляет их в список конечных стоятояний
        // вторая функция по e-путям помещает в очеред достижимые вершины, далле доставая из очереди очередную вершину, записываем её в массив
        // после чего, обрабатывая его, замняем в НКА


        public bool func1(string mbstate)
        {
            bool flag = false;
            Queue<string> q = new Queue<string>();
            q.Enqueue(mbstate);

            while(q.Count != 0)
            {
                string curState = q.Dequeue();
                if (lState.Contains(curState))
                {
                    flag = true;
                }
                foreach (var state in EAutomat)
                {
                    if (state.Key == curState)
                    {
                        foreach (var s in state.Value)
                        {
                            if (s.Key == "e" && s.Value != "-")
                            {
                                if (s.Value.Length > 1)
                                {
                                    for (int i = 0; i < s.Value.Length; i++)
                                    {
                                        q.Enqueue(s.Value[i].ToString());
                                    }
                                }
                                else
                                {
                                    q.Enqueue(s.Value);
                                }
                            }
                        }
                    }
                }
            }

            return flag;
        }

        public string[] func2(string mbstate) 
        {
            string[] res = new string[2];
            Dictionary<string, string> w = new Dictionary<string, string>();
            Queue<string> q = new Queue<string>();
            q.Enqueue(mbstate);
            string curState = q.Dequeue();
            foreach (var state in EAutomat)
            {
                if (state.Key == curState)
                {
                    foreach (var s in state.Value)
                    {
                        if (s.Key == "e" && s.Value != "-")
                        {
                            if (s.Value.Length > 1)
                            {
                                for (int i = 0; i < s.Value.Length; i++)
                                {
                                    q.Enqueue(s.Value[i].ToString());
                                }
                            }
                            else
                            {
                                q.Enqueue(s.Value);
                            }
                        }
                    }
                }
            }

            while(q.Count != 0)
            {
                curState = q.Dequeue();
                foreach(var state in EAutomat)
                {
                    if (state.Key == curState)
                    {
                        foreach (var s in state.Value)
                        {
                            if(s.Key == "0")
                            {
                                res[0] += s.Value;
                            }
                            if (s.Key == "1")
                            {
                                res[1] += s.Value;
                            }
                        }
                    }
                }
            }

            return res;
        }

        public void TranslateENFKToNFK()
        {
            foreach(var state in EAutomat)
            {
                if (func1(state.Key) && !lState.Contains(state.Key))
                {
                    //Console.WriteLine("Состояние {0} станет конечным!", state.Key);
                    lState.Add(state.Key);
                }
            }
            
            foreach (var state in EAutomat)
            {
                Dictionary<string, string> w = new Dictionary<string, string>();
                foreach (var s in state.Value)
                {
                    if(s.Key != "e")
                    w.Add(s.Key, s.Value);
                }
                NFKautomat.Add(state.Key, w);
            }

            foreach (var state in EAutomat)
            {
                var c = func2(state.Key);
                string c0 = c[0];
                string c1 = c[1];
                Dictionary<string, string> w = new Dictionary<string, string>();
                foreach (var s in NFKautomat)
                {
                    if(s.Key == state.Key)
                    {
                        foreach(var v in s.Value)
                        {
                            if(!String.IsNullOrEmpty(c0) && !c0.Contains("-") && v.Key == "0")
                            {
                                s.Value[v.Key] = c0;
                            }
                            if (!String.IsNullOrEmpty(c1) && !c1.Contains("-") && v.Key == "1")
                            {
                                s.Value[v.Key] = c1;
                            }
                        }
                    }
                }
            }
        }

        public void ShowNFKAutomat()
        {
            foreach (var w in letters)
            {
                if (w != "e")
                {
                    Console.Write("     \t{0}", w);
                }
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
    }
}
