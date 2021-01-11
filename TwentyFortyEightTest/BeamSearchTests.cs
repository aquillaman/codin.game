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
            
            // seed: 36244611
            // score: 52
            // data: 2,4,16,4,0,2,8,4,0,0,0,4,0,0,0,0
            // R
            
            int seed = 8801316;
            int score = 52;
            int[] data = 
            {
                2,4,16,4,0,2,8,4,0,0,0,4,0,0,0,0
            };

            var search = new BeamSearch(Size, 1);

            var searchBestMoves = search.SearchBestMoves(seed, score, data);

            Assert.True(false);
        }
        
    }
}