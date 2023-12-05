using System;
using System.Collections.Generic;

namespace EightPuzzleProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the start state of the puzzle (9 integers, separated by spaces):");
            int[,] start = ReadPuzzle();
            Console.WriteLine("Start puzzle:");
            PrintPuzzle(start);

            Console.WriteLine("Enter the goal state of the puzzle (9 integers, separated by spaces):");
            int[,] goal = ReadPuzzle();
            Console.WriteLine("Goal puzzle:");
            PrintPuzzle(goal);

            List<Tuple<int[,], int>> solution = Solve(start, goal);   
            if (solution == null)
            {
                Console.WriteLine("No solution found");
                return;
            }

            Console.WriteLine("Solution:");
            foreach (var step in solution)
            {
                Console.WriteLine("solution {0}:", step.Item2);
                PrintPuzzle(step.Item1);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        // to covert the number that you give to int and convert 1D to 2D matrix
        static int[,] ReadPuzzle()
        {
            int[,] puzzle = new int[3, 3];
            string[] input = Console.ReadLine().Split();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    puzzle[i, j] = int.Parse(input[i * 3 + j]);
                }
            }

            return puzzle;
        }

        // to make sure that the program find the goal
        static bool IsSolved(int[,] puzzle, int[,] goal)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzle[i, j] != goal[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // to find the blank place which is zero
        static void FindZero(int[,] puzzle, out int row, out int col)
        {
            row = col = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzle[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        return;
                    }
                }
            }
        }

        // this will start to solve the program
        static List<Tuple<int[,], int>> Solve(int[,] start, int[,] goal)
        {
            List<Tuple<int[,], int>> solution = new List<Tuple<int[,], int>>();
            Queue<Tuple<int[,], int>> q = new Queue<Tuple<int[,], int>>();
            q.Enqueue(Tuple.Create(start, 0));

            while (q.Count > 0)
            {
                Tuple<int[,], int> state = q.Dequeue();
                int[,] puzzle = state.Item1;
                int step = state.Item2;
                if (IsSolved(puzzle, goal))
                {
                    solution.Add(state);
                    return solution;
                }
                int row, col;
                FindZero(puzzle, out row, out col);

                if (row > 0)
                {
                    int[,] newPuzzle = (int[,])puzzle.Clone();
                    newPuzzle[row, col] = newPuzzle[row - 1, col];
                    newPuzzle[row - 1, col] = 0;
                    q.Enqueue(Tuple.Create(newPuzzle, step + 1));
                    solution.Add(Tuple.Create(newPuzzle, step + 1));
                }

                if (row < 2)
                {
                    int[,] newPuzzle = (int[,])puzzle.Clone();
                    newPuzzle[row, col] = newPuzzle[row + 1, col];
                    newPuzzle[row + 1, col] = 0;
                    q.Enqueue(Tuple.Create(newPuzzle, step + 1));
                    solution.Add(Tuple.Create(newPuzzle, step + 1));
                }

                if (col > 0)
                {
                    int[,] newPuzzle = (int[,])puzzle.Clone();
                    newPuzzle[row, col] = newPuzzle[row, col - 1];
                    newPuzzle[row, col - 1] = 0;
                    q.Enqueue(Tuple.Create(newPuzzle, step + 1));
                    solution.Add(Tuple.Create(newPuzzle, step + 1));
                }

                if (col < 2)
                {
                    int[,] newPuzzle = (int[,])puzzle.Clone();
                    newPuzzle[row, col] = newPuzzle[row, col + 1];
                    newPuzzle[row, col + 1] = 0;
                    q.Enqueue(Tuple.Create(newPuzzle, step + 1));
                    solution.Add(Tuple.Create(newPuzzle, step + 1));
                }
            }

            return null;
        }

        // print the input as 3*3 matrix
        static void PrintPuzzle(int[,] puzzle)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0} ", puzzle[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}