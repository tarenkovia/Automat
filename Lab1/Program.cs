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
        static void Main()
        {
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

            string path = "C:/Users/taren/source/repos/Lab1/Lab1/inputForENKF.txt";
            E_NKFAutomat A = new E_NKFAutomat(path);
            Console.WriteLine("E - Недерминированный конечный автомат - ");
            A.ShowENFKAutomat();
            A.TranslateENFKToNFK();
            Console.WriteLine();
            Console.WriteLine("Недерминированный конечный автомат - ");
            A.ShowNFKAutomat();
        }
    }
}