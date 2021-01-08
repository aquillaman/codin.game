using System;
using System.Collections.Generic;
using System.IO;

namespace TwentyFortyEight
{
    internal class Player
    {
        private const int Size = 4;
        private static readonly Grid Grid = new Grid(Size);
        
        public static void Main(string[] args)
        {
            int[] data = new int[Size*Size];
            // game loop
            while (true)
            {
                int index = 0;
                int seed = int.Parse(Console.ReadLine()); // needed to predict the next spawns
                int score = int.Parse(Console.ReadLine());

                Console.Error.WriteLine($"seed: {seed}");
                Console.Error.WriteLine($"score: {score}");
               
                for (int i = 0; i < Size; i++)
                {
                    string[] inputs = Console.ReadLine().Split(' ');
                    for (int j = 0; j < Size; j++)
                    {
                        int cell = int.Parse(inputs[j]);

                        data[index++] = cell;
                    }
                }
                
                Console.Error.WriteLine($"data: {string.Join(",", data)}");

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine(SearchBestMoves(seed, score, data));
            }
        }

        private static string SearchBestMoves(int seed, int score, int[] data)
        {
            int predictionLevel = 0;
            int maxPredictionLevel = 4;
            string[] dirs = {"L","U","R","D"};

            var parents = new Queue<Node>(new[] {new Node {Data = data, Seed = seed}});

            var count = parents.Count;
            while (predictionLevel < maxPredictionLevel)
            {
                var parent = parents.Dequeue();
                
                for (int i = 0; i < dirs.Length; i++)
                {
                    Grid.Seed = parent.Seed;
                    Grid.Score = parent.Score;
                    Grid.SetData(parent.Data);
                    
                    Grid.Move(i);
                    
                    Node node = Node.Create(dirs[i], parent);
                    node.Seed = Grid.Seed;
                    node.Score = Grid.Score;
                    node.Data = Grid.GetData();
                    
                    parents.Enqueue(node);
                }

                if (--count <= 0)
                {
                    predictionLevel++;
                    count = parents.Count;
                }
            }

            int max = int.MinValue;
            Node maxNode = null;
            var array = parents.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var node = array[i];

                if (max < node.Score)
                {
                    max = node.Score;
                    maxNode = node;
                }
            }

            return maxNode.Path;
        }
    }

    public class Node
    {
        public int[] Data;
        public int Score;
        public long Seed;
        public string Path;

        public static Node Create(string dir, Node parent)
        {
            return new Node
            {
                Path = parent.Path + dir,
            };
        }
    }

    public enum Direction
    {
        Left,
        Up,
        Down,
        Right,
    }

    public class Grid
    {
        public long Seed;
        public int Score { get; set; }

        public readonly int Size;
        private int[] _buffer;
        private bool _changed;
        private readonly int[,] _cells;


        public Grid(int size)
        {
            Size = size;
            _cells = new int[size, size];
            _buffer = new int[size];
        }
        
        public Grid(int size, int seed = 100500) : this(size)
        {
            Seed = seed;
        }

        public void Move(int direction)
        {
            Move((Direction) direction);
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

            if (_changed)
            {
                SpawnTile();
                _changed = false;
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

        private void Move(ref int[] values)
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
        
        
        private int Move(int source, int target, int min, ref int[] values)
        {
            if (source == target) throw new InvalidOperationException($"source == target {source}=={target}");

            if (values[source] == 0) return min;

            if (values[target] == 0)
            {
                _changed = true;
                
                values[target] = values[source];
                values[source] = 0;
                return min;
            }

            if (values[source] == values[target])
            {
                _changed = true;
                
                values[target] *= 2;
                values[source] = 0;
                Score += values[target];
                return source;
            }

            return min;
        }

        private void SpawnTile()
        {
            List<int> freeCells = new List<int>();
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (_cells[x, y] == 0) freeCells.Add(x + y * Size);
                }
            }

            int spawnIndex = freeCells[(int) Seed % freeCells.Count];
            int value = (Seed & 0x10) == 0 ? 2 : 4;

            _cells[spawnIndex % Size, spawnIndex / Size] = value;

            Seed = Seed * Seed % 50515093L;
        }

        #region Data accessors

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
        
        public void SetData(int x, int y, int value)
        {
            if (x < 0 || y < 0 || x > Size - 1 || y > Size - 1)
            {
                throw new InvalidDataException($"x: {x} or y: {y} out of grid size: {Size}");
            }
            
            _cells[x, y] = value;
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

        #endregion
    }
}