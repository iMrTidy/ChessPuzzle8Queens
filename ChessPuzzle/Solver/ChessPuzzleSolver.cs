using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPuzzle
{
    class ChessPuzzleSolver
    {
        Func<Queen, Queen, bool> isIntersect = (q1, q2) => q1.Row == q2.Row || q1.Column == q2.Column || q1.RowDiagonal == q2.RowDiagonal || q1.ColumnDiagonal == q2.ColumnDiagonal;

        List<List<Queen>> solutions = new List<List<Queen>>();
        public List<List<Queen>> Solutions
        {
            get { return solutions; }
            set { solutions = value; }
        }

        public void Solve()
        {
            List<List<Queen>> allQueenPos = new List<List<Queen>>();
            FillPositions(allQueenPos); //Generate all possible queen positions in order to check iteratively which are valid.
            FindValidPositions(allQueenPos);
            ExcludeSimilarSolutions(Solutions);
        }

        private void ExcludeSimilarSolutions(List<List<Queen>> solutions)
        {
            for (int i = 0; i < solutions.Count(); i++)
            {
                var curr = solutions.ElementAt(i);
                var similar = GenerateSimilarSolutions(curr);
                for (int k = solutions.Count() - 1; k > i; k--)
                {
                    var check = solutions.ElementAt(k);
                    foreach (var item in similar)
                    {
                        if (IsEqualSolution(check, item))
                        {
                            solutions.Remove(check);
                        }
                    }
                }
            }
        }

        private bool IsEqualSolution(List<Queen> check, List<Queen> solution)
        {
            for (int i = 0; i < check.Count; i++)
            {
                var checkItm = check.ElementAt(i);
                var solItm = solution.ElementAt(i);
                if (checkItm.Column != solItm.Column)
                {
                    return false;
                }
            }
            return true;
        }

        private List<List<Queen>> GenerateSimilarSolutions(List<Queen> solution)
        {
            var mirrored = MirrorSolution(solution);
            var rotated1 = RotateSolution90Degrees(solution);
            var mirrored1 = MirrorSolution(rotated1);
            var rotated2 = RotateSolution90Degrees(rotated1);
            var mirrored2 = MirrorSolution(rotated2);
            var rotated3 = RotateSolution90Degrees(rotated2);
            var mirrored3 = MirrorSolution(rotated3);
            var similar = new List<List<Queen>> { solution, mirrored, rotated1, mirrored1, rotated2, mirrored2, rotated3, mirrored3 };
            return similar;
        }

        private List<Queen> RotateSolution90Degrees(List<Queen> solution)
        {
            var rotated = new List<Queen>();
            foreach (var item in solution)
            {
                var q = new Queen { Row = item.Column, Column = 9 - item.Row };
                rotated.Add(q);
            }
            rotated = rotated.OrderBy(x => x.Row).ToList();
            return rotated;
        }

        private List<Queen> MirrorSolution(List<Queen> solution)
        {
            var mirrored = new List<Queen>();
            foreach (var item in solution)
            {
                var q = new Queen { Row = item.Row, Column = 9 - item.Column };
                mirrored.Add(q);
            }
            return mirrored;
        }

        private void FindValidPositions(List<List<Queen>> allQueenPos)
        {
            Queen q = new Queen();
            for (int r = 0; r < allQueenPos.Count; r++)
            {
                var qRow = allQueenPos.ElementAt(r);
                var cStart = GetAndClearSetColPos(allQueenPos, r, qRow); //In order to continue checking from the next column and clear set statuses.
                for (int c = cStart; c < qRow.Count; c++)
                {
                    q = qRow.ElementAt(c);
                    if (IsPositionAllowed(allQueenPos, q))
                    {
                        q.IsSet = true;
                        break;
                    }
                }
                if (q.IsSet)
                {
                    if (q.Row == 8)
                    {
                        var sol = allQueenPos.SelectMany(x => x.Where(y => y.IsSet)).ToList();
                        solutions.Add(sol);
                        r -= 2;
                    }
                }
                else
                {
                    if (r == 0) //This means that the iteration got back to the beginning i.e. all iteratoins are done.
                    {
                        break;
                    }
                    r -= 2; //Jump to the previous row, -2 because for loop will add back 1.
                }
            }
        }

        private int GetAndClearSetColPos(List<List<Queen>> allQueenPos, int r, List<Queen> qRow)
        {
            int ret = 0;
            var setPos = qRow.Where(x => x.IsSet);
            if (setPos.Count() > 0)
            {
                ret = setPos.First().Column;
            }
            allQueenPos.ForEach(x => x.ForEach(y => y.IsSet = y.Row >= r + 1 ? false : y.IsSet));
            return ret;
        }

        private bool IsPositionAllowed(List<List<Queen>> allQueenPos, Queen qToCheck)
        {
            var queensSet = allQueenPos.SelectMany(x => x.Where(y => y.IsSet == true)).ToList();
            return CheckPositions(queensSet, qToCheck);
        }

        private bool CheckPositions(List<Queen> queensList, Queen qToCheck)
        {
            foreach (var q in queensList)
            {
                if (isIntersect(q, qToCheck))
                {
                    return false;
                }
            }
            return true;
        }

        private void FillPositions(List<List<Queen>> allQueensPos)
        {
            FillInRowPositions(allQueensPos, 1, 4); //Only upto 4 because the 4-8 will give mirror solutions.
            for (int i = 2; i <= 8; i++)
            {
                FillInRowPositions(allQueensPos, i, 8);
            }
        }

        private void FillInRowPositions(List<List<Queen>> allQueensPos, int row, int columns)
        {
            List<Queen> rowQs = new List<Queen>();
            for (int column = 1; column <= columns; column++)
            {
                rowQs.Add(new Queen() { Row = row, Column = column });
            }
            allQueensPos.Add(rowQs);
        }
    }
}
