namespace Lab1
{
    public class Program
    {
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
        static void Main()
        {
            //const string input = "SeLeCt 20 >= B\ncAsE 40 b = (b + 13) - 2\ndEfAuLt b = B - 50\neNd";
            //Console.WriteLine("Конструкция, поступаемая на вход:");
            //Console.WriteLine(input);
            //Console.WriteLine();
            //string newInput = input.ToLower();
            //RunTask1(newInput);

            List<string> input = new()
            {
                "SeLeCt 20 >= B",
                "cAsE 40",
                "b = b + 13",
                "dEfAuLt b = B - 50",
                "eNd"
            };
            List<string> newInput = new();
            Console.WriteLine("Конструкция, поступаемая на вход:");
            foreach(var str in input)
            {
                newInput.Add(str.ToLower());
            }
            foreach (var str in newInput)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
            RunTask2(newInput);



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