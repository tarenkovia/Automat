using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.WebSockets;

namespace Lab1
{
    public class Program
    {
        public static void RunTask1(string input)
        {
            var newLexical = new Laboratory_lexical1();
            newLexical.Start(string.Join(Environment.NewLine, input));
            Console.WriteLine("Индекс\t Класс\t\t Тип\t\t      Значение");
            for (int i = 0; i < newLexical.Lexemes.Count; i++)
            {
                Console.WriteLine($"{(i + 1).ToString(),-8} {newLexical.Lexemes[i].Class.ToString(), -15} {newLexical.Lexemes[i].Type.ToString(),-20} {newLexical.Lexemes[i].Value.ToString()}");
            }
        }
        static void Main()
        {
            const string input = "SeLeCt 20 + B\ncAsE 40 b = b + 13\ndEfAuLt b = B - 50\neNd";
            Console.WriteLine("Конструкция, поступаемая на вход:");
            Console.WriteLine(input);
            Console.WriteLine();
            string newInput = input.ToLower();
            RunTask1(newInput);



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