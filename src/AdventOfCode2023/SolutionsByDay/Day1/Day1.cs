using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day1
{
    internal class Day1 : DaySolution
    {
        // Store "number" word references as digit words
        // Will be referenced to check the current buffer later
        private readonly Dictionary<string, int> DigitWords = new()
            {
                {"one", 1},
                {"two", 2},
                {"three", 3},
                {"four", 4},
                {"five", 5},
                {"six", 6},
                {"seven", 7},
                {"eight", 8},
                {"nine", 9},
            };

        public int GetDay1Part1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day1.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Store an accumulator for the sum
            int currentSum = 0;

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                // Create a buffer to store current line digits in
                string currentDigitBuffer = "";

                foreach (char c in line)
                {
                    // Add only digits to the buffer
                    if (char.IsDigit(c))
                    {
                        currentDigitBuffer += c;
                    }
                }

                // Only keep the first and last digits
                if (currentDigitBuffer.Length == 1)
                {
                    // Special case, duplicate the digit
                    currentSum += int.Parse(new string(currentDigitBuffer.ToCharArray()[0], 2));
                }
                else if (currentDigitBuffer.Length > 0)
                {
                    // Turn the digit buffer into an array of characters, only use the first and last character to
                    // create an integer with
                    char[] digitBufferAsCharArray = currentDigitBuffer.ToCharArray();
                    currentSum += int.Parse(
                        digitBufferAsCharArray[0].ToString() + digitBufferAsCharArray[digitBufferAsCharArray.Length - 1].ToString()
                        );
                }
            }

            return currentSum;
        }

        public int GetDay1Part2Solution()
        {

            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day1.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Store an accumulator for the sum
            int currentSum = 0;

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                // General buffer for any non-digit characters
                string currentBuffer = "";

                // A list of strings that are either numeric digits, or words of numeric digits
                List<string> currentDigitLikeBuffer = [];

                // Iterate lexically, character by character
                foreach (char c in line)
                {
                    // Only add non-digit characters to the buffer
                    if (char.IsDigit(c))
                    {
                        currentDigitLikeBuffer.Add(c.ToString());
                        currentBuffer = "";
                    }
                    else
                    {
                        currentBuffer += c;

                        // Check if the currentBuffer contains any of the digit words
                        foreach (string digitWord in DigitWords.Keys)
                        {
                            if (currentBuffer.Contains(digitWord))
                            {
                                // Add that digit word to the list of digit-like buffer
                                // Clear the current buffer, but keep the latest character
                                // in case the next numeric digit word starts with that character
                                // E.g. "oneight" we would leave the "e" in "one" in the buffer
                                currentDigitLikeBuffer.Add(digitWord);
                                currentBuffer = c.ToString();
                            }
                        }
                    }
                }

                // Iterate the digit-like buffer and create an integer based off of the first and last values in the
                // the list
                if (currentDigitLikeBuffer.Count == 1)
                {
                    // Duplicate the first index
                    int sumToAdd = int.Parse(
                        new string(
                            GetIntFromDigitBufferValue(currentDigitLikeBuffer[0]).ToString().ToCharArray()[0],
                            2
                            )
                        );
                    currentSum += sumToAdd;
                }
                else if (currentDigitLikeBuffer.Count > 0)
                {
                    // Use first and last index
                    string fromFirstIndex = GetIntFromDigitBufferValue(currentDigitLikeBuffer[0]).ToString();
                    string fromLastIndex = GetIntFromDigitBufferValue(currentDigitLikeBuffer[currentDigitLikeBuffer.Count - 1]).ToString();

                    int sumToAdd = int.Parse(fromFirstIndex + fromLastIndex);
                    currentSum += sumToAdd;
                }

            }

            return currentSum;
        }

        /// <summary>
        /// Fetches an integer from a string buffer value. Can either be a digit "1" or word "one"
        /// </summary>
        /// <param name="bufferValue"></param>
        private int GetIntFromDigitBufferValue(string bufferValue)
        {
            if (bufferValue.ToCharArray().Length == 1)
            {
                return int.Parse(bufferValue);
            }
            else
            {
                // It's a word
                int digitValue;
                DigitWords.TryGetValue(bufferValue, out digitValue);
                return digitValue;
            }
        }
    }
}
