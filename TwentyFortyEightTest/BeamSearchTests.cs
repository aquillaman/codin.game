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
            // seed: 41351857
            // score: 48968
            // data: 8,128,16,8,16,32,4096,512,4,64,32,8,256,8,4,0

            int seed = 41351857;
            int score = 48968;
            int[] data =
            {
                8, 128, 16, 8, 16, 32, 4096, 512, 4, 64, 32, 8, 256, 8, 4, 0
            };

            var search = new BeamSearch(Size, 10);
            var searchBestMoves = search.SearchBestMoves(seed, score, data);
            Assert.Null(searchBestMoves);
        }
    }
}