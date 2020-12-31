using System;
using System.Collections.Generic;
using System.IO;

namespace TwentyFortyEight
{
    internal class Solution
    {
        public static void Main(string[] args)
        {
            var grid = new Grid(4, 100500);
            int[] data = new int[4*4]
            {
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
            };
            
            grid.SetData(data);
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    public class Grid
    {
        private long _seed;
        public readonly int Size;
        private int[] _buffer;
        private readonly int[,] _cells;

        public Grid(int size, int seed = 100500)
        {
            Size = size;
            _seed = seed;
            _cells = new int[size, size];
            _buffer = new int[size];
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: MoveVertical(0, Size, 1); break;
                case Direction.Down: MoveVertical(0, Size, -1); break;
                case Direction.Left: MoveHorizontal(0, Size, 1); break;
                case Direction.Right: MoveHorizontal(0, Size, -1); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void MoveVertical(int min, int max, int dir)
        {
            for (int col = min; col < max; col++)
            {
                GetCol(col, dir, ref _buffer);
                Move(ref _buffer);
                SetCol(col, dir, ref _buffer);
            }
        }

        private void MoveHorizontal(int min, int max, int dir)
        {
            for (int row = min; row < max; row++)
            {
                GetRow(row, dir, ref _buffer);
                Move(ref _buffer);
                SetRow(row, dir, ref _buffer);
            }
        }

        private static void Move(ref int[] values)
        {
            int min = 0;
            for (int max = 1; max < values.Length; max++)
            {
                for (int i = max; i > min; i--)
                {
                    min = Move(i, i-1, min, ref values);  
                }
            }
        }

        #region Row accessors

        public void GetRow(int row, int dir, ref int[] values)
        {
            for (int col = 0; col < Size; col++)
            {
                values[col] = _cells[dir < 0 ? Size - 1 - col : col, row];
            }
        }

        public void SetRow(int row, int dir, ref int[] values)
        {
            for (int col = 0; col < Size; col++)
            {
                _cells[dir < 0 ? Size - 1 - col : col, row] = values[col];
            }
        }

        public void GetCol(int col, int dir, ref int[] values)
        {
            for (int row = 0; row < Size; row++)
            {
                values[row] = _cells[col, dir < 0 ? Size - 1 - row : row];
            }
        }

        public void SetCol(int col, int dir, ref int[] values)
        {
            for (int row = 0; row < Size; row++)
            {
                _cells[col, dir < 0 ? Size - 1 - row : row] = values[row];
            }
        }

        #endregion

        private static int Move(int source, int target, int min, ref int[] values)
        {
            if (source == target) throw new InvalidOperationException($"source == target {source}=={target}");

            if (values[source] == 0) return min;

            if (values[target] == 0)
            {
                values[target] = values[source];
                values[source] = 0;
                return min;
            }

            if (values[source] == values[target])
            {
                values[target] += values[source];
                values[source] = 0;
                return source;
            }

            return min;
        }

        public void SetData(int[] data)
        {
            if (data.Length != Size * Size)
            {
                throw new InvalidDataException($"data.Length != grid size {data.Length} != {Size * Size}");
            }

            var i = 0;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    _cells[x, y] = data[i++];
                }
            }
        }
        
        public int[] GetData()
        {
            var i = 0;
            int[] data = new int[Size * Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    data[i++] = _cells[x, y];
                }
            }

            return data;
        }
        
        private void SpawnTile() {
            List<int> freeCells = new List<int>();
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    if (_cells[x, y] == 0) freeCells.Add(x + y * Size);
                }
            }

            int spawnIndex = freeCells[(int) _seed % freeCells.Count];
            int value = (_seed & 0x10) == 0 ? 2 : 4;

            _cells[spawnIndex % Size , spawnIndex / Size] = value;

            _seed = _seed * _seed % 50515093L;
        }
    }
}