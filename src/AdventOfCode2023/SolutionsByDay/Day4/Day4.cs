using System.Numerics;

namespace AdventOfCode2023.SolutionsByDay.Day4
{
    internal class Day4 : DaySolution
    {
        public int GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day4.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Running total of points
            int totalPoints = 0;

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                // Parse the current line as a Card object
                Card card = Card.ParseCardLine(line);
                totalPoints += card.CalculateTotalPoints();
            }

            return totalPoints;
        }

        public int GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day4.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Dictionary where the key is the CardId and the value is the
            // total number of that card we have (original + any copies = total)
            Dictionary<int, int> totalCardsAndCopies = [];

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                // Parse the current line as a Card object
                Card card = Card.ParseCardLine(line);

                // Try to add 1 to the current number of card copies of this Id
                // If it fails, then add 1 instead
                if (!totalCardsAndCopies.TryAdd(card.CardId, 1))
                {
                    totalCardsAndCopies[card.CardId]++;
                }

                // Find the number of CardNumbers that won (are in the WinningNumbers)
                int numberOfNumbersThatWon = card.CalculateWinningCardNumbers();

                // Get the total number of copies of the current card being iterated
                int numberOfCopiesOfThisCard = totalCardsAndCopies[card.CardId];

                // Numerically iterate numbers above this CardId to match the numberOfNumbersThatWon
                // and add +1 to the copies
                for (int i = 1; i <= numberOfNumbersThatWon; i++)
                {
                    int cardIdWonCopyOf = card.CardId + i;

                    // Try to add to the copies of the cardId iterated on
                    if (!totalCardsAndCopies.TryAdd(cardIdWonCopyOf, 1 * numberOfCopiesOfThisCard))
                    {
                        // It failed, so create a new key and set it to 1 copy count
                        totalCardsAndCopies[cardIdWonCopyOf] += 1 * numberOfCopiesOfThisCard;
                    }
                }
            }

            return totalCardsAndCopies.Sum(pair => pair.Value);
        }
    }
}
