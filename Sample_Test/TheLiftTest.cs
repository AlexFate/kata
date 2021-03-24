using Xunit;
using System;
using CodeWars;

namespace Sample_Test
{
    public sealed class TheLiftTest
    {
        [Fact]
        public void TestMoveUp()
        {
            int[][] queues =
            {
                Array.Empty<int>(), // G
                Array.Empty<int>(), // 1
                new[] { 5, 5, 5 },  // 2
                Array.Empty<int>(), // 3
                Array.Empty<int>(), // 4
                Array.Empty<int>(), // 5
                Array.Empty<int>(), // 6
            };
            var result = Kata.TheLift(queues, 5);
            Assert.Equal(new[] { 0, 2, 5, 0 }, result);
        }

        [Fact]
        public void TestMoveUpUp()
        {
            int[][] queues =
            {
                Array.Empty<int>(), // G
                new[] { 3 },        // 1
                new[] { 4 },        // 2
                Array.Empty<int>(), // 3
                new[] { 5 },        // 4
                Array.Empty<int>(), // 5
                Array.Empty<int>(), // 6
            };
            var result = Kata.TheLift(queues, 5);
            Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 0 }, result);
        }
        
        [Fact]
        public void TestMoveUpLowCapacity()
        {
            int[][] queues =
            {
                Array.Empty<int>(), // G
                new[] { 3 },        // 1
                new[] { 5,5,6 },        // 2
                Array.Empty<int>(), // 3
                new[] { 5 },        // 4
                Array.Empty<int>(), // 5
                Array.Empty<int>(), // 6
            };
            var result = Kata.TheLift(queues, 2);
            Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 2, 5, 6, 0 }, result);
        }
        
        [Fact]
        public void TestMoveDown()
        {
            int[][] queues =
            {
                Array.Empty<int>(), // G
                Array.Empty<int>(), // 1
                new[] { 1, 1 },     // 2
                Array.Empty<int>(), // 3
                Array.Empty<int>(), // 4
                Array.Empty<int>(), // 5
                Array.Empty<int>(), // 6
            };
            var result = Kata.TheLift(queues, 5);
            Assert.Equal(new[] { 0, 2, 1, 0 }, result);
        }              
        
        [Fact]
        public void TestMoveDownDown()
        {
            int[][] queues =
            {
                Array.Empty<int>(), // G
                new[] { 0 },        // 1
                Array.Empty<int>(), // 2
                Array.Empty<int>(), // 3
                new[] { 2 },        // 4
                new[] { 3 },        // 5
                Array.Empty<int>(), // 6
            };
            var result = Kata.TheLift(queues, 5);
            Assert.Equal(new[] { 0, 5, 4, 3, 2, 1, 0 }, result);
        }          
        
        [Fact]
        public void TestMoveDownLowCapacity()
        {
            int[][] queues =
            {
                Array.Empty<int>(), // G
                new[] { 0 },        // 1
                Array.Empty<int>(), // 2
                Array.Empty<int>(), // 3
                new[] { 2, 0 },     // 4
                new[] { 3, 1 },     // 5
                Array.Empty<int>(), // 6
            };
            var result = Kata.TheLift(queues, 2);
            Assert.Equal(new[] { 0, 5, 4, 3, 1, 0, 4, 2, 0 }, result);
        }

        [Fact]
        public void TestMoveInChangedDirection()
        {
            int[][] queues =
            {
                new[] { 1 },    // G -> n
                new[] { 3 },    // 1 -> 1
                new[] { 3,5,6 },// 2 -> 5,6 -> 
                new[] { 2, 4 }, // 3 -> 3,3,2 -> 3,3,3
                new[] { 2, 0 }, // 4 -> 2,0,4 -> 
                new[] { 3, 1, 6 }, // 5 -> 3,1 -> 1
                new[] { 0 },    // 6 -> 0,6 -> 6
            };
            var result = Kata.TheLift(queues, 2);
            //Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1, 0 }, result);
        }        
        
        [Fact]
        public void TestFireDrill()
        {
            int[][] queues =
            {
                Array.Empty<int>(),
                new [] {0,0,0,0},
                new [] {0,0,0,0},
                new [] {0,0,0,0},
                new [] {0,0,0,0},
                new [] {0,0,0,0},
                new [] {0,0,0,0},
            };
            var result = Kata.TheLift(queues, 5);
            Assert.Equal(25, result.Length);
        }        
        
        [Fact]
        public void TestTrickyQueues()
        {
            int[][] queues =
            {
                Array.Empty<int>(),     //0
                new [] {0,0,0,6,},      //1
                Array.Empty<int>(),     //2
                Array.Empty<int>(),     //3
                Array.Empty<int>(),     //4
                new [] {6,6,0,0,0,6,},  //5
                Array.Empty<int>(),     //6
            };
            var result = Kata.TheLift(queues, 5);
            Assert.Equal(5, result[4]);
        }
    }
}