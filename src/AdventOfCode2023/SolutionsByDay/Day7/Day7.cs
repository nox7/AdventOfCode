using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day7
{
    internal class Day7 : DaySolution
    {
        public long GetPart1Solution(char? wildCard = null)
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day7.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            List<CardHand> allHands = [];
            List<CardHand> fiveOfAKindHands = [];
            List<CardHand> fourOfAKindHands = [];
            List<CardHand> fullHouseHands = [];
            List<CardHand> threeOfAKindHands = [];
            List<CardHand> twoPairHands = [];
            List<CardHand> onePairHands = [];
            List<CardHand> highCardHands = [];

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                CardHand cardHand = new();
                string[] handAndBid = line.Split(' ');
                cardHand.Wildcard = wildCard;
                cardHand.Cards = handAndBid[0].ToCharArray();
                cardHand.Bid = Int64.Parse(handAndBid[1]);
                cardHand.CalculateCountOfCards();
                cardHand.CalculateHandStrength();
                allHands.Add(cardHand);

                // Put it into the correct list by hand type
                switch (cardHand.CardHandStrength)
                {
                    case HandStrength.HighCard:
                        highCardHands.Add(cardHand);
                        break;
                    case HandStrength.OnePair:
                        onePairHands.Add(cardHand);
                        break;
                    case HandStrength.TwoPair:
                        twoPairHands.Add(cardHand);
                        break;
                    case HandStrength.ThreeOfAKind:
                        threeOfAKindHands.Add(cardHand);
                        break;
                    case HandStrength.FullHouse:
                        fullHouseHands.Add(cardHand);
                        break;
                    case HandStrength.FourOfAKind:
                        fourOfAKindHands.Add(cardHand);
                        break;
                    case HandStrength.FiveOfAKind:
                        fiveOfAKindHands.Add(cardHand);
                        break;
                    default:
                        break;
                }
            }

            // Sort each list by the greatest winning hand according to the AoC algorithm that says
            // Iterate each card in the hand from left-to-right, whichever hand has the highest value card
            // is greater than the comparing hand
            fiveOfAKindHands.Sort(CardHand.CompareHands);
            fourOfAKindHands.Sort(CardHand.CompareHands);
            fullHouseHands.Sort(CardHand.CompareHands);
            threeOfAKindHands.Sort(CardHand.CompareHands);
            twoPairHands.Sort(CardHand.CompareHands);
            onePairHands.Sort(CardHand.CompareHands);
            highCardHands.Sort(CardHand.CompareHands);

            long totalWinnings = 0;
            int currentRank = 1;
            // Starting from the highCardHands, calculate the rank * bid and add it to the accumulator
            highCardHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });
            onePairHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });
            twoPairHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });
            threeOfAKindHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });
            fullHouseHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });
            fourOfAKindHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });
            fiveOfAKindHands.ForEach(cardHand => { totalWinnings += currentRank * cardHand.Bid; ++currentRank; });

            return totalWinnings;
        }

        public long GetPart2Solution()
        {
            // Set J as the lowest valued card
            CardStrengths.Strengths['J'] = 0;
            // Part is is the same as Part 1 thanks to the method CardHand.CalculateHandStrength();
            return GetPart1Solution('J');
        }
    }
}
