using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace gcreate.Tetroswap
{
    public class Solver
    {
        string root;
        int rows;
        int columns;
        
        public Solver(string root, int rows, int columns)
        {
            this.root = root;
            this.rows = rows;
            this.columns = columns;
        }

        public string Solve()
        {
            var queue = new Queue<string>();
            var states = new HashSet<string>();
            states.Add(root);

            foreach (var transposition in GetTranspositions())
            {
                queue.Enqueue(transposition);
                states.Add(GetState(transposition));
            }

            while (queue.Count > 0)
            {
                var transpositions = queue.Dequeue();
                if (Match(GetState(transpositions)))
                    return transpositions;
                foreach (var transposition in GetTranspositions())
                {
                    var transpositionsAdd = transpositions + transposition;
                    if (!states.Contains(GetState(transpositionsAdd)))
                    {
                        queue.Enqueue(transpositionsAdd);
                        states.Add(GetState(transpositionsAdd));
                    }
                }
            }

            return null;
        }

        IEnumerable<string> SplitInParts(string s) {
            for (var i = 0; i < s.Length; i += 3)
                yield return s.Substring(i, Math.Min(3, s.Length - i));
        }

        string GetState(string transpositions)
        {
            var state = new StringBuilder(root);
            foreach (var transposition in SplitInParts(transpositions))
            {
                var v = int.Parse(transposition.Substring(0, 1));
                var a = int.Parse(transposition.Substring(1, 1));
                var b = int.Parse(transposition.Substring(2, 1));

                if (v == 0)
                {
                    for (int i = 0; i < columns; i++)
                    {
                        var idxA = (a * columns) + i;
                        var idxB = (b * columns) + i;
                    
                        var aChar = state[idxA];
                        var bChar = state[idxB];
                        if (aChar != ' ' && bChar != ' ')
                        {
                            state[idxA] = bChar;
                            state[idxB] = aChar;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rows; i++)
                    {
                        var idxA = (i * columns) + a;
                        var idxB = (i * columns) + b;

                        var aChar = state[idxA];
                        var bChar = state[idxB];
                        if (aChar != ' ' && bChar != ' ')
                        {
                            state[idxA] = bChar;
                            state[idxB] = aChar;
                        }
                    }
                }
            }
            return state.ToString();
        }

        List<string> GetTranspositions()
        {
            var list = new List<string>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = i + 1; j < rows; j++) 
                {
                    list.Add(string.Format("0{0}{1}", i, j));
                }
            }
            for (int i = 0; i < columns; i++)
            {
                for (int j = i + 1; j < columns; j++) 
                {
                    list.Add(string.Format("1{0}{1}", i, j));
                }
            }
            return list;
        }

        bool Match(string state)
        {
            var grid  = new StringBuilder(state);
            var visited = new HashSet<int>();

            for (int i = 0; i < grid.Length; i++)
            {
                var box = grid[i];
                if (box != ' ' && box != '_')
                {
                    var matches = 0;

                    var stack = new Stack<int>();
                    stack.Push(i);

                    while (stack.Count > 0)
                    {            
                        var idx = stack.Pop();
                        if (!visited.Contains(idx))
                        {
                            var row = idx / columns;
                            var col = idx - (row * columns);
                            
                            if (row-1 > 0 && box == grid[((row-1)*columns)+col]) {
                                stack.Push(((row-1)*columns)+col);
                            }

                            if (col+1 < columns && box == grid[(row*columns)+col+1]) {
                                stack.Push((row*columns)+col+1);
                            }

                            if (row+1 < rows && box == grid[((row+1)*columns)+col]) {
                                stack.Push(((row+1)*columns)+col);
                            }

                            if (col-1 > 0 && box == grid[(row*columns)+col-1]) {
                                stack.Push((row*columns)+col-1);
                            }

                            matches++;
                            visited.Add(idx);
                        }
                        else
                            matches = 4;
                    }

                    if (matches < 4)
                        return false;
                }
            }

            return true;
        }
    }
}