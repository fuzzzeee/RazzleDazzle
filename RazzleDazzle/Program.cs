using System;
using System.Collections.Generic;
using System.Linq;

namespace RazzleDazzle
{
    class Program
    {
        static void Main()
        {
            for (var i = 0; i < 1000; i++)
            {
                var game  = new Game();
                game.Play();
                Console.WriteLine($"Won {game.Prizes} prizes after spending {game.Spent:C} on {game.Turns} turns");
            }
            Console.Read();
        }
    }

    class Game
    {
        private static readonly Random _rand = new Random();
        private static readonly Dictionary<int, int> _statistics = new Dictionary<int, int>();
        private static readonly Dictionary<int, Square> _squares = new Dictionary<int, Square>
        {
            { 8, new Square(100) },
            { 9, new Square(100) },
            { 10, new Square(50) },
            { 11, new Square(30) },
            { 12, new Square(50) },
            { 13, new Square(50) },
            { 14, new Square(20) },
            { 15, new Square(15) },
            { 16, new Square(10) },
            { 17, new Square(5) },
            { 18, new Square(SquareCondition.Prize) },
            { 19, new Square(SquareCondition.Prize) },
            { 20, new Square(SquareCondition.Prize) },
            { 21, new Square(SquareCondition.Prize) },
            { 22, new Square() },
            { 23, new Square() },
            { 24, new Square() },
            { 25, new Square() },
            { 26, new Square() },
            { 27, new Square() },
            { 28, new Square() },
            { 29, new Square(SquareCondition.PayDouble) },
            { 30, new Square() },
            { 31, new Square() },
            { 32, new Square() },
            { 33, new Square() },
            { 34, new Square() },
            { 35, new Square(SquareCondition.Prize) },
            { 36, new Square(SquareCondition.Prize) },
            { 37, new Square(SquareCondition.Prize) },
            { 38, new Square(SquareCondition.Prize) },
            { 39, new Square(5) },
            { 40, new Square(5) },
            { 41, new Square(15) },
            { 42, new Square(20) },
            { 43, new Square(50) },
            { 44, new Square(50) },
            { 45, new Square(30) },
            { 46, new Square(50) },
            { 47, new Square(100) },
            { 48, new Square(100) },
        };
        
        private long _points, _prizes, _cost = 1, _spent, _turns;

        public long Prizes => _prizes;

        public long Spent => _spent;

        public long Turns => _turns;

        public void Play()
        {
            while (true)
            {
                var rolls = new int[8];
                for (var roll = 0; roll < rolls.Length; roll++)
                {
                    rolls[roll] = _rand.Next(1, 7);
                }
                var total = rolls.Sum();
                lock (_statistics)
                {
                    if (!_statistics.ContainsKey(total))
                        _statistics.Add(total, 1);
                    else
                        _statistics[total]++;
                }
                var square = _squares[total];
                switch (square.Condition)
                {
                    case SquareCondition.Prize:
                        _prizes++;
                        break;
                    case SquareCondition.PayDouble:
                        _cost *= 2;
                        break;
                }
                _points += square.Points;
                _spent += _cost;
                _turns++;
                if (_points >= 100)
                    return;
            }
        }

        class Square
        {
            public Square()
            {
            }

            public Square(int points)
            {
                Points = points;
            }

            public Square(SquareCondition condition)
            {
                Condition = condition;
            }

            public int Points { get; }
            public SquareCondition Condition { get; }
        }

        enum SquareCondition
        {
            None,
            Prize,
            PayDouble,
        }
    }
}
