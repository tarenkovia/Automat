﻿using System;
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
            string path = "C:/Users/taren/source/repos/Lab1/Lab1/input.txt";
            Automat A = new Automat(path);
            A.ShowAutomat();
            Console.WriteLine();
            Console.WriteLine("Введите слово: ");
            string word = Console.ReadLine();
            Console.WriteLine();
            A.StartAutomat(word);
        }
    }
}