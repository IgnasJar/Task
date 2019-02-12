// Author: Ignas Jarašūnas - jarasignas@gmail.com
using System;
using System.IO;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            HeapParser heap = new HeapParser();
            Console.WriteLine("Computing heap...");
            // Define data textfile path (File must end with the last digits row)
            // File path is ..\Task\Task\bin\Debug
            heap.Calculate(Directory.GetCurrentDirectory() + @"\data.txt");
            Console.ReadLine();
        }

    }
}
