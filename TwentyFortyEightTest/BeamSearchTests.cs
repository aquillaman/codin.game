using System;
using System.Collections.Generic;
using NUnit.Framework;
using TwentyFortyEight;

namespace TwentyFortyEightTest
{
    [TestFixture]
    public class BeamSearchTests
    {
        private const int Size = 4;
        private static readonly Grid Grid = new Grid(Size);
        
        [Test]
        public void TestBeamSearch()
        {
            // seed: 19045731
            // score: 0
            // data: 0,0,0,0,0,4,0,0,0,4,0,0,0,0,0,0
            // U
            
            // seed: 28693008
            // score: 8
            // data: 0,8,0,0,0,0,0,0,0,0,0,0,0,2,0,0
                
            int seed = 19045731;
            int score = 0;
            int[] data = new int[Size*Size]
            {
                0,0,0,0,0,4,0,0,0,4,0,0,0,0,0,0
            };

            var moves = SearchBestMoves(seed, score, data);

            Assert.True(false);
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
}