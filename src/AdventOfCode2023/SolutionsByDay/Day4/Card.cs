namespace AdventOfCode2023.SolutionsByDay.Day4
{
    internal class Card
    {
        public int CardId;
        public List<int> WinningNumbers = [];
        public List<int> CardNumbers = [];

        /// <summary>
        /// Creates an instance of a Card from the provided string card line.
        /// Uses a lexer to parse the string line with a parser state enum.
        /// </summary>
        public static Card ParseCardLine(string line)
        {
            CardParserState parserState = CardParserState.CardLabel;
            string currentBuffer = "";
            Card currentCard = new();

            foreach (char c in line)
            {
                switch (parserState)
                {
                    case CardParserState.CardLabel:
                        if (c != ' ' && !Char.IsDigit(c))
                        {
                            // It's a character or space, ignore
                        }
                        else if (Char.IsDigit(c))
                        {
                            // It's a digit, add to current buffer and switch parser state
                            currentBuffer += c;
                            parserState = CardParserState.CardId;
                        }

                        break;
                    case CardParserState.CardId:
                        if (Char.IsDigit(c))
                        {
                            // Add digit to current buffer
                            currentBuffer += c;
                        }
                        else if (c == ':')
                        {
                            // CardId is over, set the property of the current card
                            // empty the buffer
                            // set state to parsing winning numbers
                            currentCard.CardId = Int32.Parse(currentBuffer);
                            currentBuffer = "";
                            parserState = CardParserState.WinningNumbers;
                        }

                        break;
                    case CardParserState.WinningNumbers:
                        if (c == ' ')
                        {
                            // A space means check the buffer
                            // If there is anything in the buffer, then add it to the WinningNumbers
                            // of the current card
                            // then empty the buffer
                            if (currentBuffer.Length > 0)
                            {
                                currentCard.WinningNumbers.Add(Int32.Parse(currentBuffer));
                                currentBuffer = "";
                            }
                        }
                        else if (Char.IsDigit(c))
                        {
                            // It's a digit, add it to the current buffer
                            currentBuffer += c;
                        }
                        else if (c == '|')
                        {
                            // It's a bar, which ends the winning numbers list
                            // If there is anything in the buffer, then add it to the WinningNumbers
                            // of the current card
                            // then empty the buffer
                            // Then set the state to parsing the card numbers
                            if (currentBuffer.Length > 0)
                            {
                                currentCard.WinningNumbers.Add(Int32.Parse(currentBuffer));
                                currentBuffer = "";
                            }

                            parserState = CardParserState.CardNumbers;
                        }

                        break;
                    case CardParserState.CardNumbers:
                        if (c == ' ')
                        {
                            // A space means check the buffer
                            // If there is anything in the buffer, then add it to the CardNumbers
                            // of the current card
                            // then empty the buffer
                            if (currentBuffer.Length > 0)
                            {
                                currentCard.CardNumbers.Add(Int32.Parse(currentBuffer));
                                currentBuffer = "";
                            }
                        }
                        else if (Char.IsDigit(c))
                        {
                            // It's a digit, add it to the current buffer
                            currentBuffer += c;
                        }

                        break;
                    default:
                        break;
                }
            }

            // If there is anything left in the current buffer, that means the line has ended without a space
            // to signify to consume the buffer as a CardNumber
            // Do so now
            if (currentBuffer.Length > 0)
            {
                currentCard.CardNumbers.Add(Int32.Parse(currentBuffer));
            }

            return currentCard;
        }

        /// <summary>
        /// Calculates the total points this card is worth by finding the number of WinningNumbers that appear
        /// in the CardNumbers list
        /// </summary>
        /// <returns></returns>
        public int CalculateTotalPoints()
        {
            List<int> winningNumbersInCardNumbers = WinningNumbers.Intersect(CardNumbers).ToList();
            int currentPoints = 0;
            for (int i = 0; i < winningNumbersInCardNumbers.Count; i++)
            {
                if (i == 0)
                {
                    currentPoints = 1;
                }
                else
                {
                    currentPoints *= 2;
                }
            }

            return currentPoints;
        }

        /// <summary>
        /// Calculates the number of WinningNumbers that appear in CardNumbers
        /// </summary>
        /// <returns></returns>
        public int CalculateWinningCardNumbers()
        {
            return WinningNumbers.Intersect(CardNumbers).Count();
        }
    }
}
