using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_runner
{
    public class Day22
    {        
        public int Part1(string input)
        {
            var decks = Parse(input);

            while (!decks.Any(x => x.IsEmpty))
            {
                var ordered = decks.OrderBy(x => x.Top).ToArray();
                var winner = ordered.Last();
                var loser = ordered.First();

                winner.Put(winner.Take(), loser.Take());
            }

            return decks.Single(x => !x.IsEmpty).CalculateScore();
        }

        public PlayerDeck[] Parse(string input)
        {
            PlayerDeck ParseDeck(string i) => new(i.Split(Environment.NewLine).Skip(1).Select(int.Parse));
                
            var playerParts = input.Split(Environment.NewLine + Environment.NewLine);
            return new[]
            {
                ParseDeck(playerParts[0]),
                ParseDeck(playerParts[1])
            };
        }

        public class PlayerDeck
        {
            private readonly Queue<int> _cards;

            public PlayerDeck(IEnumerable<int> cards) => _cards = new(cards);

            public int Take() => _cards.Dequeue();

            public void Put(int winner, int loser)
            {
                _cards.Enqueue(winner);
                _cards.Enqueue(loser);
            }

            public int? Top => _cards.TryPeek(out var card) ? card : null;
            
            public bool IsEmpty => !_cards.TryPeek(out _);

            public int CalculateScore() => _cards.Reverse().Select((card, n) => card * (n+1)).Sum();
        }
    }


}