using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericTransformer
{
    internal struct NumberStruct
    {
        public int MaxDigitValue { get; }
        public int PlusSize { get; }
        public int[] PlusDigits { get;set; }
        public int MinusSize { get; }
        public int[] MinusDigits { get; set; }

        public static NumberStruct operator +(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            int PlusSize;
            int MinusSize;
            if(number1.PlusSize > number2.PlusSize)
            {
                PlusSize = number1.PlusSize;
            }
            else
            {
                PlusSize = number2.PlusSize;
            }
            if(number1.MinusSize > number2.MinusSize) 
            {
                MinusSize = number1.MinusSize;
            }
            else
            {
                MinusSize = number2.MinusSize;
            }
            NumberStruct result = new NumberStruct(number1.MaxDigitValue, PlusSize, MinusSize);
            number1 = new NumberStruct(number1, PlusSize, MinusSize);
            number2 = new NumberStruct(number2, PlusSize, MinusSize);
            for (int i = 0; i < MinusSize; i++)
            {
                if (i == MinusSize - 1)
                {
                    if ((result.MinusDigits[i] + number1.MinusDigits[i] + number2.MinusDigits[i]) / result.MaxDigitValue != 0)
                    {
                        result = new NumberStruct(result, 1 + PlusSize, MinusSize);
                        result.PlusDigits[0] += (result.MinusDigits[i] + number1.MinusDigits[i] + number2.MinusDigits[i]) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.MinusDigits[i + 1] += (result.MinusDigits[i] + number1.MinusDigits[i] + number2.MinusDigits[i]) / result.MaxDigitValue;
                }
                result.MinusDigits[i] = (result.MinusDigits[i] + number1.MinusDigits[i] + number2.MinusDigits[i]) % result.MaxDigitValue;
            }
            for (int i = 0; i < PlusSize; i++)
            {
                if (i == PlusSize - 1)
                {
                    if(0 != (number1.PlusDigits[i] + number2.PlusDigits[i] + result.PlusDigits[i]) / result.MaxDigitValue)
                    {
                        result = new NumberStruct(result, 1 + PlusSize, MinusSize);
                        result.PlusDigits[i + 1] += (result.PlusDigits[i] + number1.PlusDigits[i] + number2.PlusDigits[i]) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.PlusDigits[i + 1] += (result.PlusDigits[i] + number1.PlusDigits[i] + number2.PlusDigits[i]) / result.MaxDigitValue;
                }
                result.PlusDigits[i] = (result.PlusDigits[i] + number1.PlusDigits[i] + number2.PlusDigits[i]) % result.MaxDigitValue;
            }
            return result.CleanUp();
        }
        public static NumberStruct operator -(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            int PlusSize;
            int MinusSize;
            if (number1.PlusSize > number2.PlusSize)
            {
                PlusSize = number1.PlusSize;
            }
            else
            {
                PlusSize = number2.PlusSize;
            }
            if (number1.MinusSize > number2.MinusSize)
            {
                MinusSize = number1.MinusSize;
            }
            else
            {
                MinusSize = number2.MinusSize;
            }
            NumberStruct result = new NumberStruct(number1.MaxDigitValue, PlusSize, MinusSize);
            number1 = new NumberStruct(number1, PlusSize, MinusSize);
            number2 = new NumberStruct(number2, PlusSize, MinusSize);
            for (int i = 0; i < MinusSize; i++)
            {
                if (i == MinusSize - 1)
                {
                    if (PlusSize != 0)
                    {
                        result.PlusDigits[0] = (result.MinusDigits[i] + number1.MinusDigits[i] - number2.MinusDigits[i] - result.MaxDigitValue + 1) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.MinusDigits[i + 1] = (result.MinusDigits[i] + number1.MinusDigits[i] - number2.MinusDigits[i] - result.MaxDigitValue + 1) /result.MaxDigitValue;
                }
                result.MinusDigits[i] = (result.MaxDigitValue + ((result.MinusDigits[i] + number1.MinusDigits[i] - number2.MinusDigits[i]) % result.MaxDigitValue)) % result.MaxDigitValue;
            }
            for (int i = 0; i < PlusSize; i++)
            {
                if (i == PlusSize - 1)
                {

                }
                else
                {
                    result.PlusDigits[i + 1] = (result.PlusDigits[i] + number1.PlusDigits[i] - number2.PlusDigits[i] - result.MaxDigitValue + 1) / result.MaxDigitValue;
                }
                result.PlusDigits[i] = ((result.MaxDigitValue + result.PlusDigits[i] + number1.PlusDigits[i] % result.MaxDigitValue - number2.PlusDigits[i] % result.MaxDigitValue) % result.MaxDigitValue);
            }
            return result.CleanUp();
        }
        public static NumberStruct operator *(int number1, NumberStruct number2)
        {
            NumberStruct result = new NumberStruct(number2.MaxDigitValue, number2.PlusSize, number2.MinusSize);
            for (int i = 0; i < result.MinusSize; i++)
            {
                result.MinusDigits[i] = number1 * number2.MinusDigits[i];
            }
            for (int i = 0; i < result.PlusSize; i++)
            {
                result.PlusDigits[i] = number1 * number2.PlusDigits[i];
            }
            for (int i = 0; i < result.MinusSize; i++)
            {
                if(i == result.MinusSize - 1)
                {
                    if (result.PlusSize != 0)
                    {
                        result.PlusDigits[0] += (result.MinusDigits[i]) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.MinusDigits[i + 1] += (result.MinusDigits[i]) / result.MaxDigitValue;
                }
                result.MinusDigits[i] = (result.MinusDigits[i]) % result.MaxDigitValue;
            }
            for (int i = 0; i < result.PlusSize; i++)
            {
                if (i == result.PlusSize - 1)
                {
                    if ((result.PlusDigits[i]) / result.MaxDigitValue != 0)
                    {
                        result = new NumberStruct(result, result.PlusSize + 1, result.MinusSize);
                        result.PlusDigits[i + 1] += (result.PlusDigits[i]) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.PlusDigits[i + 1] += (result.PlusDigits[i]) / result.MaxDigitValue;
                }
                result.PlusDigits[i] = (result.PlusDigits[i]) % result.MaxDigitValue;
            }
            return result.CleanUp();
        }
        public static NumberStruct operator *(NumberStruct number1, int number2)
        {
            return (number2 * number1).CleanUp();
        }
        public static NumberStruct operator *(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            List<NumberStruct> numbers = new List<NumberStruct>();
            int PlusSize = 0;
            int MinusSize = 0;
            for(int i = 0; i < number1.MinusSize; i++)
            {
                NumberStruct number = (number2 * number1.MinusDigits[i]).ShiftTo(-number1.MinusSize + i);
                if (number.MinusSize > MinusSize)
                {
                    MinusSize = number.MinusSize;
                }
                numbers.Add(number);
            }
            for(int i = 0; i < number1.PlusSize; i++)
            {
                NumberStruct number = (number2 * number1.PlusDigits[i]).ShiftTo(i);
                if(number.MinusSize > PlusSize)
                {
                    PlusSize = number.MinusSize;
                }
                numbers.Add(number);
            }
            NumberStruct result = new NumberStruct(number1.MaxDigitValue, PlusSize, MinusSize);
            foreach(NumberStruct number in numbers)
            {
                result += number; 
            }
            return result.CleanUp();
        }

        public NumberStruct ShiftTo(int Index)
        {
            NumberStruct result = new NumberStruct(this.MaxDigitValue, (PlusSize + Index) < 0 ? 0 : PlusSize + Index, (MinusSize - Index) < 0 ? 0 : MinusSize - Index);
            for (int i = 0; i < MinusSize; i++)
            {
                if(i > result.MinusSize - 1)
                {
                    result.PlusDigits[i - result.MinusSize] = MinusDigits[i];
                }
                else
                {
                    result.MinusDigits[i] = MinusDigits[i];
                }
            }
            for (int i = 0; i < PlusSize; i++)
            {
                if(i + Index < 0)
                {
                    result.MinusDigits[result.MinusSize + i + Index] = PlusDigits[i];
                }
                else
                {
                    result.PlusDigits[i + Index] = PlusDigits[i];
                }
            }
            return result;
        }
        public static bool operator ==(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            int plusSize = 0;
            if (number1.PlusSize > number2.PlusSize)
            {
                plusSize = number1.PlusSize;
            }
            else
            {
                plusSize = number2.PlusSize;
            }
            int minusSize = 0;
            if (number1.MinusSize > number2.MinusSize)
            {
                minusSize = number1.MinusSize;
            }
            else
            {
                minusSize = number2.MinusSize;
            }
            number1 = new NumberStruct(number1, plusSize, minusSize);
            number2 = new NumberStruct(number2, plusSize, minusSize);
            for(int i = 0; i < number1.PlusSize; i++)
            {
                if (number1.PlusDigits[i] == number2.PlusDigits[i])
                {

                }
                else
                {
                    return false;
                }
            }
            for (int i = 0; i < number1.MinusSize; i++)
            {
                if (number1.MinusDigits[i] == number2.MinusDigits[i])
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static bool operator !=(NumberStruct number1, NumberStruct number2)
        {
            return !(number1 == number2);
        }
        public static bool operator <(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            return number2 > number1;
        }
        public static bool operator >(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            int plusSize = 0;
            if(number1.PlusSize > number2.PlusSize)
            {
                plusSize = number1.PlusSize;
            }
            else
            {
                plusSize = number2.PlusSize;
            }
            int minusSize = 0;
            if (number1.MinusSize > number2.MinusSize)
            {
                minusSize = number1.MinusSize;
            }
            else
            {
                minusSize = number2.MinusSize;
            }
            number1 = new NumberStruct(number1, plusSize, minusSize);
            number2 = new NumberStruct(number2, plusSize, minusSize);
            for(int i = number1.PlusSize - 1; i >= 0;i--)
            {
                if(number1.PlusDigits[i] > number2.PlusDigits[i])
                {
                    return true;
                }
                else if(number1.PlusDigits[i] < number2.PlusDigits[i])
                {
                    return false;
                }
                else
                {

                }
            }
            for (int i = number1.MinusSize - 1; i >= 0; i--)
            {
                if (number1.MinusDigits[i] > number2.MinusDigits[i])
                {
                    return true;
                }
                else if (number1.MinusDigits[i] < number2.MinusDigits[i])
                {
                    return false;
                }
                else
                {

                }
            }
            return false;
        }
        public static NumberStruct operator^(NumberStruct number,int pow)
        {
            NumberStruct result = new NumberStruct(number, number.PlusSize, number.MinusSize);
            if(pow >= 1)
            {
                for (int i = 0; i < pow - 1; i++)
                {
                    result = result * number;
                }
                return result;
            }
            else if(pow == 0)
            {
                return new NumberStruct(number.MaxDigitValue, 1);
            }
            else if(pow <= -1)
            {
                for (int i = 0; i < pow - 1; i++)
                {
                    result = result * number;
                }
                return new NumberStruct(number.MaxDigitValue, 1) / result;
            }
            return new NumberStruct();
        }
        static int CalculationAccuracy = 32;
        public static NumberStruct operator /(NumberStruct number1, NumberStruct number2)
        {
            number1 = number1.CleanUp();
            number2 = number2.CleanUp();
            if(number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            int TransformIndex = 0;
            if(number2.PlusSize != 0)
            {
                TransformIndex = -(number2.PlusSize - 1);
            }
            else
            {
                bool IsFirstDigitFounded = false;
                int StartIndex = number2.MinusSize - 1;
                while(IsFirstDigitFounded == false)
                {
                    if (number2.MinusDigits[StartIndex] != 0)
                    {
                        IsFirstDigitFounded = true;
                    }
                    else
                    {
                        TransformIndex++;
                    }
                    StartIndex--;
                }
            }
            NumberStruct result = new NumberStruct(number1.MaxDigitValue, number1.PlusSize + TransformIndex < 0 ? 0 : number1.PlusSize + TransformIndex + 1, CalculationAccuracy);
            NumberStruct approx = new NumberStruct(number1, number1.PlusSize, number1.MinusSize);
            approx.setZiro();
            int firstNumberDigitStartIndex;
            int SeccondNumberDigitStartIndex;
            if (number1.PlusSize > 0)
            {
                firstNumberDigitStartIndex = number1.PlusSize - 1;
            }
            else
            {
                int Index=0;
                while (number1.MinusDigits[number1.MinusSize + Index - 1] == 0)
                {
                    Index--;
                }
                firstNumberDigitStartIndex = Index - 1;
            }
            if (number2.PlusSize > 0)
            {
                SeccondNumberDigitStartIndex = number2.PlusSize - 1;
            }
            else
            {
                int Index = 0;
                while (number2.MinusDigits[number2.MinusSize + Index - 1] == 0)
                {
                    Index--;
                }
                SeccondNumberDigitStartIndex = Index - 1;
            }
            int digitIndex = firstNumberDigitStartIndex - SeccondNumberDigitStartIndex;
            while(digitIndex > - (CalculationAccuracy))
            {
                int digit = 0;
                while (number1 > approx && digit < number1.MaxDigitValue)
                {
                    approx += number2.ShiftTo(digitIndex);
                    digit++;
                }
                if(number1 == approx)
                {
                    if (digitIndex >= 0)
                    {
                        result.PlusDigits[digitIndex] = digit;
                    }
                    else
                    {
                        result.MinusDigits[result.MinusSize + digitIndex] = digit;
                    }
                    break;
                }
                approx -= number2.ShiftTo(digitIndex);
                digit--;
                if (digitIndex >= 0)
                {
                    result.PlusDigits[digitIndex] = digit;
                }
                else
                {
                    result.MinusDigits[result.MinusSize + digitIndex] = digit;
                }
                digitIndex--;
            }
            return result;
        }
        private NumberStruct CleanUp()
        {
            int plusSize = 0;
            int minusSize = 0;
            bool isNotZeroFounded = false;
            int i = 0;
            isNotZeroFounded = false;
            i = 0;
            while(isNotZeroFounded == false && i < MinusSize)
            {
                if(MinusDigits[i] != 0)
                {
                    isNotZeroFounded = true;
                }
                else
                {
                    minusSize++;
                }
                i++;
            }
            isNotZeroFounded = false;
            i = PlusSize - 1;
            while(isNotZeroFounded == false && i >= 0)
            {
                if (PlusDigits[i] != 0)
                {
                    isNotZeroFounded = true;
                }
                else
                {
                    plusSize++;
                }
                i--;
            }
            return new NumberStruct(this, PlusSize - plusSize, MinusSize - minusSize);
        }
        public NumberStruct Structurize()
        {
            NumberStruct result = new NumberStruct(this, PlusSize, MinusSize);
            for (int i = 0; i < MinusSize; i++)
            {
                if (i == MinusSize - 1)
                {
                    if (PlusSize != 0)
                    {
                        result.PlusDigits[0] += (result.MinusDigits[i]) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.MinusDigits[i + 1] += (result.MinusDigits[i]) / result.MaxDigitValue;
                }
                result.MinusDigits[i] = (result.MinusDigits[i]) % result.MaxDigitValue;
            }
            int PlusSize2 = PlusSize;
            for (int i = 0; i < PlusSize2; i++)
            {
                if (i == PlusSize2 - 1)
                {
                    if (0 != (result.PlusDigits[i]) / result.MaxDigitValue)
                    {
                        result = new NumberStruct(result, ++PlusSize2, MinusSize);
                        result.PlusDigits[i + 1] += (result.PlusDigits[i]) / result.MaxDigitValue;
                    }
                }
                else
                {
                    result.PlusDigits[i + 1] += (result.PlusDigits[i]) / result.MaxDigitValue;
                }
                result.PlusDigits[i] = (result.PlusDigits[i]) % result.MaxDigitValue;
            }
            return result.CleanUp();
        }
        public NumberStruct(int MaxDigitValue, int Value)
        {
            this.MaxDigitValue = MaxDigitValue;
            this.PlusDigits = new int[1];
            this.PlusSize = PlusDigits.Length;
            this.PlusDigits[0] = Value;
            this.MinusDigits = new int[0];
            this.MinusSize = MinusDigits.Length;
            this = this.Structurize();
        }
        public NumberStruct(int MaxDigitValue, int PlusSize, int MinusSize)
        {
            this.MaxDigitValue = MaxDigitValue;
            this.PlusSize = PlusSize;
            this.PlusDigits = new int[PlusSize];
            this.MinusSize = MinusSize;
            this.MinusDigits = new int[MinusSize];
        }
        public NumberStruct(int MaxDigitValue, int[] PlusDigits, int[] MinusDigits)
        {
            this.MaxDigitValue = MaxDigitValue;
            this.PlusDigits = PlusDigits;
            this.PlusSize = PlusDigits.Length;
            this.MinusDigits = MinusDigits;
            this.MinusSize = MinusDigits.Length;
        }
        public NumberStruct(NumberStruct number, int PlusSize, int MinusSize)
        {
            this.MaxDigitValue = number.MaxDigitValue;
            this.PlusSize = PlusSize;
            this.PlusDigits = new int[PlusSize];
            this.MinusSize = MinusSize;
            this.MinusDigits = new int[MinusSize];

            bool isEnd = false;
            int fromCounter = number.MinusSize - 1;
            int toCounter = MinusSize - 1;
            while(isEnd == false)
            {
                if (fromCounter == -1 || toCounter == -1)
                {
                    isEnd = true;
                }
                else
                {
                    this.MinusDigits[toCounter] = number.MinusDigits[fromCounter];
                }
                fromCounter--;
                toCounter--;
            }

            for (int i = 0; i < PlusSize && i < number.PlusSize; i++)
            {
                this.PlusDigits[i] = number.PlusDigits[i]; 
            }
        }
        private void setZiro()
        {
            for (int i = 0; i < PlusSize; i++)
            {
                this.PlusDigits[i] = 0;
            }
            for(int i = 0; i < MinusSize; i++)
            {
                this.MinusDigits[i] = 0;
            }
        }
        public void TEST1()
        {
            MinusDigits[4] = 5;
            MinusDigits[3] = 5;
            MinusDigits[2] = 5;
            MinusDigits[1] = 0;
            MinusDigits[0] = 0;
            Console.WriteLine(ToString());
            
        }
        public void TEST2()
        {
            MinusDigits[4] = 5;
            MinusDigits[3] = 5;
            MinusDigits[2] = 5;
            MinusDigits[1] = 0;
            MinusDigits[0] = 9;
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = PlusSize - 1; i >= 0; i--)
            {
                sb.Append(this.PlusDigits[i] < 10 ? this.PlusDigits[i].ToString() : (char)(this.PlusDigits[i] - 10 + 'A'));
                sb.Append('|');
            }
            sb.Append('.');
            for (int i = MinusSize - 1; i >= 0; i--)
            {
                sb.Append('|');
                sb.Append(this.MinusDigits[i] < 10 ? this.MinusDigits[i].ToString() : (char)(this.MinusDigits[i] - 10 + 'A'));
            }
            return sb.ToString();
        }
    }
}
