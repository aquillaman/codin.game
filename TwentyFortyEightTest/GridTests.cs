using System;
using NUnit.Framework;
using TwentyFortyEight;

namespace TwentyFortyEightTest
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void TestGridCtor()
        {
            Assert.NotNull(new Grid(4));
        }

        [Test]
        public void TestGridSize()
        {
            var size = 4;
            var grid = new Grid(size);
            Assert.True(grid.Size == size);
        }

        [Test]
        public void TestGridDataSize()
        {
            const int size = 4;
            const int dataSize = size * size;

            var grid = new Grid(size);
            Assert.True(grid.GetData().Length == dataSize);
        }

        [Test]
        public void TestGridDefaultData()
        {
            const int size = 4;
            const int dataSize = size * size;

            var testData = new int[dataSize];
            var gridData = new Grid(size).GetData();

            bool equal = true;
            for (int i = 0; i < dataSize; i++)
            {
                equal &= testData[i] == gridData[i];
            }

            Assert.True(equal);
        }

        [Test]
        public void TestGridSetExternalData()
        {
            const int size = 8;
            const int dataSize = size * size;
            var grid = new Grid(size, 100500);

            var data = new int[dataSize]
            {
                2, 2, 2, 0, 2, 0, 2, 2,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            };
            
            var check = new int[dataSize]
            {
                4, 4, 4, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            };

            grid.SetData(data);
            grid.Move(Direction.Left);
            var gridData = grid.GetData();

            bool equal = true;
            for (int i = 0; i < dataSize; i++)
            {
                equal &= check[i] == gridData[i];
            }

            Assert.True(equal);
        }

        [Test]
        public void TestGridSetRow()
        {
            const int size = 4;
            const int dataSize = size * size;
            var grid = new Grid(size);

            var row0 = new[] {0, 1, 0, 1};
            var row1 = new[] {1, 0, 1, 0};
            var row2 = new[] {0, 1, 0, 1};
            var row3 = new[] {1, 0, 1, 0};

            var testData = new[]
            {
                0, 1, 0, 1,
                1, 0, 1, 0,
                0, 1, 0, 1,
                1, 0, 1, 0,
            };

            grid.SetRow(0, 1, ref row0);
            grid.SetRow(1, 1, ref row1);
            grid.SetRow(2, 1, ref row2);
            grid.SetRow(3, 1, ref row3);

            var gridData = grid.GetData();

            bool equal = true;
            for (int i = 0; i < dataSize; i++)
            {
                equal &= testData[i] == gridData[i];
            }

            Assert.True(equal);
        }

        [Test]
        public void TestGridGetRow()
        {
            const int size = 4;
            var grid = new Grid(size);

            var row0 = new[] {0, 1, 0, 1};
            var row1 = new[] {1, 0, 1, 0};
            var row2 = new[] {0, 1, 0, 1};
            var row3 = new[] {1, 0, 1, 0};

            var testData = new[]
            {
                0, 1, 0, 1,
                1, 0, 1, 0,
                0, 1, 0, 1,
                1, 0, 1, 0,
            };

            grid.SetData(testData);

            bool equal = true;
            int[] row = new int[size];

            grid.GetRow(0, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row0[i] == row[i];
            }

            grid.GetRow(1, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row1[i] == row[i];
            }

            grid.GetRow(2, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row2[i] == row[i];
            }

            grid.GetRow(3, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row3[i] == row[i];
            }

            Assert.True(equal);
        }

        [Test]
        public void TestGridSetCol()
        {
            const int size = 4;
            const int dataSize = size * size;
            var grid = new Grid(size);

            var col0 = new[] {0, 1, 2, 3};
            var col1 = new[] {3, 2, 1, 0};
            var col2 = new[] {0, 1, 2, 3};
            var col3 = new[] {3, 2, 1, 0};

            var testData = new[]
            {
                0, 3, 0, 3,
                1, 2, 1, 2,
                2, 1, 2, 1,
                3, 0, 3, 0,
            };

            grid.SetCol(0, 1, ref col0);
            grid.SetCol(1, 1, ref col1);
            grid.SetCol(2, 1, ref col2);
            grid.SetCol(3, 1, ref col3);

            var gridData = grid.GetData();

            bool equal = true;
            for (int i = 0; i < dataSize; i++)
            {
                equal &= testData[i] == gridData[i];
            }

            Assert.True(equal);
        }

        [Test]
        public void TestGridGetCol()
        {
            const int size = 4;
            var grid = new Grid(size);

            var col0 = new[] {0, 1, 2, 3};
            var col1 = new[] {3, 2, 1, 0};
            var col2 = new[] {0, 1, 2, 3};
            var col3 = new[] {3, 2, 1, 0};

            var testData = new[]
            {
                0, 3, 0, 3,
                1, 2, 1, 2,
                2, 1, 2, 1,
                3, 0, 3, 0,
            };

            grid.SetData(testData);

            bool equal = true;
            int[] row = new int[size];

            grid.GetCol(0, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= col0[i] == row[i];
            }

            grid.GetCol(1, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= col1[i] == row[i];
            }

            grid.GetCol(2, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= col2[i] == row[i];
            }

            grid.GetCol(3, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= col3[i] == row[i];
            }

            Assert.True(equal);
        }

        [Test]
        public void TestGridMoveRight()
        {
            const int size = 4;
            var grid = new Grid(size);
           
            var row = new[] {0, 0, 0, 0};
            var row0 = new[] {1, 0, 0, 0};
            var testRow0 = new[] {0, 0, 0, 1};

            grid.SetRow(0, 1, ref row0);
            grid.Move(Direction.Right);
            grid.GetRow(0, 1, ref row);

            bool equal = true;
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == testRow0[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveInversed()
        {
            const int size = 4;
            var grid = new Grid(size);
           
            var row = new[] {0, 0, 0, 0};
            var row0 = new[] {1, 0, 0, 0};
            var testRow0 = new[] {0, 0, 0, 1};

            grid.SetRow(0, -1, ref row0);
            grid.Move(Direction.Left);
            grid.GetRow(0, -1, ref row);

            bool equal = true;
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == testRow0[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveLeft()
        {
            const int size = 4;
            var grid = new Grid(size);
           
            var row = new[] {0, 0, 0, 0};
            var row0 = new[] {0, 0, 0, 1};
            var testRow0 = new[] {1, 0, 0, 0};

            grid.SetRow(0, 1, ref row0);
            grid.Move(Direction.Left);
            grid.GetRow(0, 1, ref row);

            bool equal = true;
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == testRow0[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveUp()
        {
            const int size = 4;
            var grid = new Grid(size);
           
            var col = new[] {0, 0, 0, 0};
            var col0 = new[] {0, 0, 0, 1};
            var check = new[] {1, 0, 0, 0};

            grid.SetCol(0, 1, ref col0);
            grid.Move(Direction.Up);
            grid.GetCol(0, 1, ref col);

            bool equal = true;
            for (int i = 0; i < size; i++)
            {
                equal &= col[i] == check[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveDown()
        {
            const int size = 4;
            var grid = new Grid(size);
           
            var col = new[] {0, 0, 0, 0};
            var col0 = new[] {1, 0, 0, 0};
            var check = new[] {0, 0, 0, 1};

            grid.SetCol(0, 1, ref col0);
            grid.Move(Direction.Down);
            grid.GetCol(0, 1, ref col);

            bool equal = true;
            for (int i = 0; i < size; i++)
            {
                equal &= col[i] == check[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveRightDownLeftUp()
        {
            const int size = 4;
            var grid = new Grid(size);
           
            var row = new[] {0, 0, 0, 0};
            var row0 = new[] {1, 0, 0, 0};
            var testRow0 = new[] {1, 0, 0, 0};

            grid.SetRow(0, 1, ref row0);
            grid.Move(Direction.Right);
            grid.Move(Direction.Down);
            grid.Move(Direction.Left);
            grid.Move(Direction.Up);
            grid.GetRow(0, 1, ref row);

            bool equal = true;
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == testRow0[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveRight_2_0_2_0()
        {
            const int size = 4;
           
            bool equal = true;
            var grid = new Grid(size);
            var row = new[] {0, 0, 0, 0};
            
            var row0 = new[] {2, 0, 2, 4};
            var check0 = new[] {4, 4, 0, 0};
            
            var row1 = new[] {2, 2, 2, 4};
            var check1 = new[] {4, 2, 4, 0};
            
            var row2 = new[] {2, 0, 0, 2};
            var check2 = new[] {4, 0, 0, 0};

            grid.SetRow(0, 1, ref row0);
            grid.SetRow(1, 1, ref row1);
            grid.SetRow(2, 1, ref row2);
            grid.Move(Direction.Left);
            
            grid.GetRow(0, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == check0[i];
            }
            
            grid.GetRow(1, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == check1[i];
            }
            
            grid.GetRow(2, 1, ref row);
            for (int i = 0; i < size; i++)
            {
                equal &= row[i] == check2[i];
            }

            Assert.True(equal);
        }
        
        [Test]
        public void TestGridMoveRight_2_0_2_0_()
        {
            const int size = 4;
            const int dataSize = size * size;
           
            bool equal = true;
            var grid = new Grid(size);
            
            var data0 = new[]
            {
                2, 0, 2, 4,
                2, 2, 2, 4,
                2, 0, 0, 2,
                0, 0, 0, 0,
            };
            
            var check0 = new[]
            {
                4, 4, 0, 0,
                4, 2, 4, 0,
                4, 0, 0, 0,
                0, 0, 0, 0,
            };

            grid.SetData(data0);
            grid.Move(Direction.Left);

            var data = grid.GetData();
            for (int i = 0; i < dataSize; i++)
            {
                equal &= data[i] == check0[i];
            }
            
            Assert.True(equal);
            
            var data1 = new[]
            {
                2, 0, 0, 4,
                2, 2, 0, 4,
                2, 4, 4, 2,
                0, 0, 0, 0,
            };
            
            var check1 = new[]
            {
                0, 0, 2, 4,
                0, 0, 4, 4,
                0, 2, 8, 2,
                0, 0, 0, 0,
            };

            grid.SetData(data1);
            grid.Move(Direction.Right);

            data = grid.GetData();
            for (int i = 0; i < dataSize; i++)
            {
                equal &= data[i] == check1[i];
            }
            
            Assert.True(equal);
            
            var data2 = new[]
            {
                2, 2, 2, 2,
                0, 2, 4, 0,
                0, 0, 4, 0,
                4, 4, 2, 0,
            };
            
            var check2 = new[]
            {
                0, 0, 0, 0,
                0, 0, 2, 0,
                2, 4, 8, 0,
                4, 4, 2, 2,
            };

            grid.SetData(data2);
            grid.Move(Direction.Down);

            data = grid.GetData();
            for (int i = 0; i < dataSize; i++)
            {
                equal &= data[i] == check2[i];
            }
            
            Assert.True(equal);
        }
    }
}