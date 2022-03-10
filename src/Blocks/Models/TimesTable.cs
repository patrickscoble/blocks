using System;

namespace Blocks.Models
{
    public class TimesTable
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public int Answer { get { return FirstNumber * SecondNumber; } }

        public int CalculateFirstIncorrectNumber()
        {
            Random random = new Random();
            int next = random.Next(1, 5);

            switch (next)
            {
                case 1: return Answer - FirstNumber;
                case 2: return Answer + SecondNumber;
                case 3: return Answer - 1;
                case 4: return Answer + 2;
                case 5: return Answer - 3;
            };

            return Answer - FirstNumber;
        }

        public int CalculateSecondIncorrectNumber()
        {
            Random random = new Random();
            int next = random.Next(1, 5);

            switch (next)
            {
                case 1: return Answer + FirstNumber;
                case 2: return Answer - SecondNumber;
                case 3: return Answer + 1;
                case 4: return Answer - 2;
                case 5: return Answer + 3;
            };

            return Answer + FirstNumber;
        }

        public string ToString(bool includeAnswer)
        {
            if (includeAnswer)
            {
                return $"{FirstNumber} x {SecondNumber} = {Answer}";
            }

            return $"{FirstNumber} x {SecondNumber}";
        }
    }
}
