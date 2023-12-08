using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day7
{
    internal class CardHand
    {
        /// <summary>
        /// List of cards in hand. E.g. "2" "A" "A" "A" "2"
        /// </summary>
        public char[] Cards = [];
        public long Bid = 0;
        public char? Wildcard = null;

        /// <summary>
        /// A dictionary where the key is the card and the value is the count of that card in the hand
        /// </summary>
        public Dictionary<char, int>? CountOfCards = null;

        /// <summary>
        /// The strength of this hand is determined by its Poker hand equivalent. This is calculated manually by a method below.
        /// </summary>
        public HandStrength? CardHandStrength = null;

        /// <summary>
        /// The card that 
        /// </summary>
        // public KeyValuePair<char, int>? HighestAppearingCard = null;

        public static int CompareHands(CardHand hand1, CardHand hand2)
        {
            for (int i = 0; i < 5; i++)
            {
                char hand1Card = hand1.Cards[i];
                char hand2Card = hand2.Cards[i];
                CardStrengths.Strengths.TryGetValue(hand1Card, out int hand1CardStrength);
                CardStrengths.Strengths.TryGetValue(hand2Card, out int hand2CardStrength);
                if (hand1CardStrength > hand2CardStrength)
                {
                    return 1;
                }
                else if (hand1CardStrength < hand2CardStrength)
                {
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Calculates a dictionary where the key is the card and the value is the number of that card in the hand.
        /// Cards that are not in play are not set - as in, there are no keys that would be 0.
        /// 
        /// Then, this method will set the CountOfCards property to the result
        /// </summary>
        /// <returns></returns>
        public void CalculateCountOfCards()
        {
            Dictionary<char, int> result = [];
            foreach (char card in Cards)
            {
                if (result.TryGetValue(card, out int value))
                {
                    result[card] = ++value;
                }
                else
                {
                    result.Add(card, 1);
                }
            }

            CountOfCards = result;
        }

        /// <summary>
        /// Calculates the hand type strength of this hand by sorting the list of cards and their number of appearances
        /// from greatest to least, and then comparing the number of duplicate cards to determine what hand they have.
        /// 
        /// It is then assigned to the CardHandStrength property.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void CalculateHandStrength()
        {
            if (CountOfCards == null)
            {
                throw new Exception("CalculateCountOfCards() must be called before this method.");
            }

            // Check for a five-of-a-kind where there is only one entry in the dictionary of card counts
            if (CountOfCards.Count == 5)
            {
                CardHandStrength = HandStrength.FiveOfAKind;
            }

            // Get the top card in the hand
            // Convert the dictionary to a list of KeyValuePairs
            List<KeyValuePair<char, int>> countOfCardsAsList = CountOfCards.ToList();

            // Sort them by card with the highest count
            countOfCardsAsList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            // A wild card has been set, deal with it
            if (Wildcard != null)
            {
                // Find the highest non-wildcard card
                KeyValuePair<char, int> highestNonWildCard = countOfCardsAsList.Where(pair => pair.Key != Wildcard).FirstOrDefault();

                if (!highestNonWildCard.Equals(default(KeyValuePair<char, int>)))
                {
                    // Get the number of wildcards
                    bool hasWildcards = CountOfCards.TryGetValue((char)Wildcard, out int numWildcards);
                    if (hasWildcards)
                    {
                        CountOfCards[highestNonWildCard.Key] += numWildcards;

                        // Remove the wildcard from the CountOfCards dictionary, it's been used up
                        CountOfCards.Remove((char)Wildcard);
                    }

                    // Redefine the count cards list and sort them
                    // to account for the wildcard modifications
                    countOfCardsAsList = CountOfCards.ToList();
                    countOfCardsAsList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                }
            }

            KeyValuePair<char, int> highestCard = countOfCardsAsList[0];

            if (highestCard.Value == 5)
            {
                CardHandStrength = HandStrength.FiveOfAKind;
                return;
            }

            if (highestCard.Value == 4)
            {
                CardHandStrength = HandStrength.FourOfAKind;
                return;
            }

            // 3 of the same type
            // Is there a next high of 2 of the same type?
            // That would be a full house, else a ThreeOfAKind
            if (highestCard.Value == 3){
                if (countOfCardsAsList[1].Value == 2)
                {
                    CardHandStrength = HandStrength.FullHouse;
                }
                else
                {
                    CardHandStrength = HandStrength.ThreeOfAKind;
                }

                return;
            }

            if (highestCard.Value == 2)
            {
                // Check if the next highest card also appears twice
                if (countOfCardsAsList[1].Value == 2)
                {
                    // Two pairs
                    CardHandStrength = HandStrength.TwoPair;
                }
                else
                {
                    // Only one pair
                    CardHandStrength = HandStrength.OnePair;
                }

                return;
            }

            // If all else fails, this is just a "high card" scenario
            CardHandStrength = HandStrength.HighCard;
        }
    }
}
