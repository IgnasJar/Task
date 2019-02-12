// Author: Ignas Jarašūnas - jarasignas@gmail.com
//1. You will start from the top and move downwards to the last possible child.
//2. You must proceed by changing between even and odd numbers subsequently.
//3. You must reach to the bottom of the pyramid.
//4. Assume that there is at least one valid path to the bottom.
//5. If there are multiple paths, which result in the same maximum amount, you can choose any of
//them.
//** Task note. Each node has 2 children and cannot traverse beyond their scope **

using System;
using System.IO;
using System.Linq;

namespace Task
{
   public class HeapParser
    {   
       private int[][] numberArray;   // number heap array(jagged)
       private int heapLength = 0;   // max heap length

       private int maxSum = 0;       // max sum
       private int[] finalPath;      // max sum path

        //Build heap from text file
        private bool Build_heap(String path)
        {
            try
            {   // validate filepath          
                if (!File.Exists(path))
                {
                    Console.WriteLine("File not found");
                    return false;
                } // if (!File.Exists(path))
                heapLength = 0;
                var lines = File.ReadAllLines(path);
                numberArray = new int[lines.Count()][];
                foreach (var line in lines)
                { 
                    // get heapline from string
                    string[] digits = line.Trim().Split(' ');
                    // append jagged heap array with row array
                    numberArray[heapLength] = digits.Select(int.Parse).ToArray(); 
                    heapLength++;
                }
                Console.WriteLine();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ocurred while building the heap");
                Console.WriteLine(e.Message);
                return false;
            }
        }// bool Build_heap

        // Compute heap
        public void Calculate(String path)
        {
            // read the file and check if reading failed
            if (!Build_heap(path))
            {
                Console.WriteLine("Failed to build the heap");
                return;
            } //   if (!Build_heap(path))
            finalPath = new int[heapLength];
            // initiate recursive heap parsing and determine if the first number is even
            GetPath(0, 0, numberArray[0][0] % 2 == 0, finalPath);
            Console.Write("Max sum: ");
            Console.WriteLine(maxSum);
            Console.Write("Path: ");
            foreach (int digit in finalPath)
            {
                Console.Write(digit);
                Console.Write(" ");
            }

        } // public void Calculate

        // Treverse heap args: 1. digit index in the row 2. row number 3. row type 4. taversed path array
        private void GetPath(int position, int arrayRow, bool isEven, int[] path)
        {
            // if it's the last row and the current number is valid 
            if (arrayRow == heapLength - 1 && (isEven == (numberArray[arrayRow][position] % 2 == 0)))
            {
                path[arrayRow] = numberArray[arrayRow][position];
                // calculate sum of the current path 
                int sum = path.Sum();
                if (sum > maxSum)
                {
                    maxSum = sum;
                    finalPath = path;
                } //if (sum > maxSum)
                return;
            } // if (arrayRow == heapLength - 1 && (isEven ...
            // traverse heap until the last row
            else if (arrayRow < heapLength - 1) 
            {   
                // Check if row type matches the number type (is valid number)
                if (isEven == (numberArray[arrayRow][position] % 2 == 0))
                {
                    int arrayLength = numberArray[arrayRow + 1].Length;
                    // append traversed path
                    path[arrayRow] = numberArray[arrayRow][position];
                    // check if the next row entry is within bounds
                    if (position < arrayLength)     GetPath(position, arrayRow + 1, !isEven, path);
                    if (position + 1 < arrayLength) GetPath(position + 1, arrayRow + 1, !isEven, path);
                } // if (is_valid_number)

            } //  if (arrayRow < heapLength)

        }// void GetPath

    }// class HeapParser
}//  namespace Task
