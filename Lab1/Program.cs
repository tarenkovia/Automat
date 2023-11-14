namespace Lab1
{
    public class Program
    {
        static void FormatOut(string str)
        {
            Console.Write("{0, 6} ", str);
        }

        public static void RunTask1(string input)
        {
            var newLexical = new Laboratory_lexical1();
            newLexical.Start(string.Join(Environment.NewLine, input));
            for (int i = 0; i < newLexical.Lexemes.Count; i++)
            {
                Console.WriteLine($"Индекс:{i+1,-3}Класс:{newLexical.Lexemes[i].Class.ToString(), -15} Тип:{newLexical.Lexemes[i].Type.ToString(),-20} Значение:{newLexical.Lexemes[i].Value.ToString()}");
            }
        }

        public static void RunTask2(List<string> input)
        {
            var analyser = new Laboratory_lexical2();
            try
            {
                var result = analyser.Run(string.Join(Environment.NewLine, input));
                Console.WriteLine("Результат: ");
                Console.WriteLine(result ? "Успешно проинициализированно" : "Неподходящая конструкция");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RunTask3(List<string> input)
        {
            var analyser = new Laboratory_lexical3();
            try
            {
                var result = analyser.Run(string.Join(Environment.NewLine, input), out List<Entry> entryList);
                Console.Write("Результат: ");
                Console.WriteLine(result ? "Все прошло успешно" : "Неподходящая конструкция");
                foreach (var entry in entryList)
                {
                    if (entry.EntryType is EntryType.Var or EntryType.Const) FormatOut(entry.Value);
                    else if (entry.EntryType == EntryType.Cmd) FormatOut(entry.Cmd.ToString());
                    else if (entry.EntryType == EntryType.CmdPtr) FormatOut($"{entry.CmdPtr}");
                }
                Console.WriteLine();
                for (int i = 0; i < entryList.Count + 1; i++)
                {
                    FormatOut($"{i}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }
        static void Main()
        {
            List<string> input = new()
            {
                "select 20 >= b",
                "case 40",
                "	b = b + 13",
                "case 89",
                "   b = b + 89",
                "default b = b + 60",
                "end"
            };
            //List<string> newInput = new();
            //Console.WriteLine("Конструкция, поступаемая на вход:");
            //foreach (var str in input)
            //{
            //    newInput.Add(str.ToLower());
            //}
            //foreach (var str in newInput)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.WriteLine();
            RunTask3(input);



            //List<string> input = new()
            //{
            //    "SeLeCt 20 >= B",
            //    "cAsE 40",
            //    "b = ( b + 13 ) - 2",
            //    "cAsE 89",
            //    "b = ( b + 89 ) - 5",
            //    "cAsE 48",
            //    "b = ( b + 13 ) - 2",
            //    "cAsE 79",
            //    "b = ( b + 89 ) - 5",
            //    "default b = b + 19",
            //    "eNd"
            //};
            //List<string> newInput = new();
            //Console.WriteLine("Конструкция, поступаемая на вход:");
            //foreach(var str in input)
            //{
            //    newInput.Add(str.ToLower());
            //}
            //foreach (var str in newInput)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.WriteLine();
            //RunTask2(newInput);



            //string path = "C:/Users/taren/source/repos/Lab1/Lab1/input.txt";
            //Automat A = new Automat(path);
            //A.ShowAutomat();
            //Console.WriteLine();
            //Console.WriteLine("Enter a word: ");
            //string word = Console.ReadLine();
            //Console.WriteLine();
            //A.StartAutomat(word);
            //Console.ReadKey();

            //string path = "C:/Users/taren/source/repos/Lab1/Lab1/inputForNKF.txt";
            //NKFAutomat A = new NKFAutomat(path);
            //Console.WriteLine("Недерминированный конечный автомат - ");
            //A.ShowNFKAutomat();
            //A.TranslateNFKToKDA();
            //Console.WriteLine();
            //Console.WriteLine("Дерминированный конечный автомат - ");
            //A.ShowTransletionAutomat();

            //Console.WriteLine();
            //Console.WriteLine("Enter a word: ");
            //string sword = Console.ReadLine();
            //Console.WriteLine();
            //A.startNKFautomat(sword);

            //Console.WriteLine();
            //Console.WriteLine("Enter a word: ");
            //string word = Console.ReadLine();
            //Console.WriteLine();
            //A.StartAutomat(word);

            //string path = "C:/Users/taren/source/repos/Lab1/Lab1/inputForENKF.txt";
            //E_NKFAutomat A = new E_NKFAutomat(path);
            //Console.WriteLine("E - Недерминированный конечный автомат - ");
            //A.ShowENFKAutomat();
            //A.TranslateENFKToNFK();
            //Console.WriteLine();
            //Console.WriteLine("Недерминированный конечный автомат - ");
            //A.ShowNFKAutomat();
            //Console.WriteLine("Enter a word: ");
            //string sword = Console.ReadLine();
            //Console.WriteLine();
            //A.startNKFautomat(sword);
        }
    }
}