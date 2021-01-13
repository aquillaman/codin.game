using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using TwentyFortyEight;

namespace TwentyFortyEightTest
{
    [TestFixture]
    public class BeamSearchTests
    {
        private const int Size = 4;

        [Test]
        public void TestBeamSearch()
        {
            // seed: 29217049
            // score: 14400
            // data: 0,32,2,8,0,1024,512,64,0,32,256,4,0,0,4,2

            int seed = 29217049;
            int score = 14400;
            int[] data =
            {
                0,   32,   2,  8,
                0, 1024, 512, 64,
                0,   32, 256,  4,
                0,    0,   4,  2
            };

            var search = new BeamSearch(Size, 10);
            var searchBestMoves = search.SearchBestMoves(seed, score, data);
            Assert.Null(searchBestMoves);
        }
    }
}