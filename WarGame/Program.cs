using System;
using System.Collections.Generic;

namespace WarGame
{
    class Program
    {

        private static List<string> player1Deck = new List<string>();
        private static List<string> player2Deck = new List<string>();
        private static List<string> cardsToAdd = new List<string>();
        private static int rounds = 0;
        static void Main(string[] args)
        {
            int winner = 0;
            int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
            for (int i = 0; i < n; i++)
            {
                string cardp1 = Console.ReadLine(); // the n cards of player 1
                player1Deck.Add(cardp1);
            }
            int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
            for (int i = 0; i < m; i++)
            {
                string cardp2 = Console.ReadLine(); // the m cards of player 2
                player2Deck.Add(cardp2);
            }
            while (player1Deck.Count > 0 && player2Deck.Count > 0)
            {
                PlayRound();
                if (cardsToAdd.Count == 0)
                    rounds++;

            }
            if (player1Deck.Count == 0)
                winner = 2;
            if (player2Deck.Count == 0)
                winner = 1;
            string result;
            result = winner + " " + rounds;
            if (player2Deck.Count == 0 && cardsToAdd.Count > 0 ||
               player1Deck.Count == 0 && cardsToAdd.Count > 0)
                result = "PAT";

            Console.WriteLine(result);
        }
        private static void PlayRound()
        {
            switch (CompareCards(player1Deck[0],
                                     player2Deck[0]))
            {
                case 1:
                    AddAndRemoveCard(player1Deck, player1Deck, player1Deck[0]);
                    AddAndRemoveCard(player1Deck, player2Deck, player2Deck[0]);
                    UpdateCachedCards(player1Deck);
                    break;
                case 2:
                    AddAndRemoveCard(player2Deck, player2Deck, player2Deck[0]);
                    AddAndRemoveCard(player2Deck, player1Deck, player1Deck[0]);
                    UpdateCachedCards(player2Deck);
                    break;
                case 0:
                    for (int i = 0; i <= 3; i++)
                    {
                        if (player1Deck.Count > 0 && player2Deck.Count > 0)
                        {
                            AddAndRemoveCard(cardsToAdd, player1Deck, player1Deck[0]);
                            AddAndRemoveCard(cardsToAdd, player2Deck, player2Deck[0]);
                        }
                    }
                    break;
            }
        }
        private static void AddAndRemoveCard(List<string> toAdd, List<string> toRemove, string whatToAddOrRemove)
        {
            toAdd.Add(whatToAddOrRemove);
            toRemove.Remove(whatToAddOrRemove);
        }
        private static void UpdateCachedCards(List<string> deck)
        {
            for (int i = cardsToAdd.Count - 1; i == 0; i++)
            {
                deck.Add(cardsToAdd[i]);
            }
            cardsToAdd.Clear();
        }

        private static int CompareCards(string card1, string card2)
        {

            string rank1 = card1.Substring(0, 1);
            string rank2 = card2.Substring(0, 1);

            return CompareNumbers(TransformInNumber(rank1),
                                  TransformInNumber(rank2));
        }
        private static bool rankIsNumber(string rank) => int.TryParse(rank, out int number);
        private static int TransformInNumber(string rank)
        {
            int number = 0;
            if (!rankIsNumber(rank))
            {
                number = TransformIntoNumber(rank);
            }
            else
            {
                number = int.Parse(rank);
                if (number == 1)
                    number = 10; //znayu
            }
            return number;
        }
        private static int TransformIntoNumber(string rank)
        {
            switch (char.Parse(rank))
            {
                case 'J':
                    return 11;
                case 'Q':
                    return 12;
                case 'K':
                    return 13;
                case 'A':
                    return 14;
            }
            return 6;
        }
        private static int CompareNumbers(int number1, int number2)
        {
            if (number1 > number2)
                return 1;
            if (number1 < number2)
                return 2;
            if (number1 == number2)
                return 0;
            return 0;
        }
    }
}
