using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericTransformer
{
    internal struct NumberStruct
    {
        private static Dictionary<int, string> DictTranslator = new Dictionary<int, string>()
        {
            {-1 ,"-1" },
            {0 ,"0" },
            {1 ,"1" },
            {2 ,"2" },
            {3 ,"3" },
            {4 ,"4" },
            {5 ,"5" },
            {6 ,"6" },
            {7 ,"7" },
            {8 ,"8" },
            {9 ,"9" },
            {10 ,"A" },
            {11 ,"B" },
            {12 ,"C" },
            {13 ,"D" },
            {14 ,"E" },
            {15 ,"F" },
        };
        public int MaxDigitValue { get; }
        private int PlusSize { get; }
        private int[] PlusDigits;
        private int MinusSize { get; }
        private int[] MinusDigits;

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
            bool isChecked;
            int startZerosCounter = 0;
            isChecked = false;
            for (int i = 0; i < MinusSize; i++)
            {
                if (i == MinusSize - 1)
                {
                    result.PlusDigits[0] = (number1.MinusDigits[i] + number2.MinusDigits[i]) / result.MaxDigitValue;
                }
                else
                {
                    result.MinusDigits[i + 1] = (number1.MinusDigits[i] + number2.MinusDigits[i]) / result.MaxDigitValue;
                }
                result.MinusDigits[i] = (result.MinusDigits[i] + number1.MinusDigits[i] + number2.MinusDigits[i]) % result.MaxDigitValue;
                if (isChecked == false && result.MinusDigits[i] == 0)
                {
                    startZerosCounter++;
                }
                else if (isChecked == false && result.MinusDigits[i] != 0)
                {
                    isChecked = true;
                }
            }
            int endZerosCounter = 0;
            for (int i = 0; i < PlusSize; i++)
            {
                if (i == PlusSize - 1)
                {
                    if(0 != (number1.PlusDigits[i] + number2.PlusDigits[i]) / result.MaxDigitValue)
                    {
                        result = new NumberStruct(result, ++PlusSize, MinusSize);
                        result.PlusDigits[i + 1] = (number1.PlusDigits[i] + number2.PlusDigits[i]) / result.MaxDigitValue;
                        endZerosCounter = 0;
                    }
                }
                else
                {
                    result.PlusDigits[i + 1] = (number1.PlusDigits[i] + number2.PlusDigits[i]) / result.MaxDigitValue;
                }
                result.PlusDigits[i] = (result.PlusDigits[i] + number1.PlusDigits[i] + number2.PlusDigits[i]) % result.MaxDigitValue;
                if (result.PlusDigits[i] == 0)
                {
                    endZerosCounter++; 
                }
                else
                {
                    endZerosCounter = 0;
                }
            }
            result = new NumberStruct(result, PlusSize - endZerosCounter, MinusSize - startZerosCounter);
            return result;
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
            bool isChecked;
            int startZerosCounter = 0;
            isChecked = false;
            for (int i = 0; i < MinusSize; i++)
            {
                if (i == MinusSize - 1)
                {
                    result.PlusDigits[0] = (result.MinusDigits[i] + number1.MinusDigits[i] - number2.MinusDigits[i] - result.MaxDigitValue + 1) / result.MaxDigitValue;
                }
                else
                {
                    result.MinusDigits[i + 1] = (result.MinusDigits[i] + number1.MinusDigits[i] - number2.MinusDigits[i] - result.MaxDigitValue + 1) /result.MaxDigitValue;
                }
                result.MinusDigits[i] = (result.MaxDigitValue + ((result.MinusDigits[i] + number1.MinusDigits[i] - number2.MinusDigits[i]) % result.MaxDigitValue)) % result.MaxDigitValue;
                if (isChecked == false && result.MinusDigits[i] == 0)
                {
                    startZerosCounter++;
                }
                else if (isChecked == false && result.MinusDigits[i] != 0)
                {
                    isChecked = true;
                }
            }
            int endZerosCounter = 0;
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
                if (result.PlusDigits[i] == 0)
                {
                    endZerosCounter++;
                }
                else
                {
                    endZerosCounter = 0;
                }
            }
            result = new NumberStruct(result, PlusSize - endZerosCounter, MinusSize - startZerosCounter);
            return result;
        }
        public static NumberStruct operator *(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            NumberStruct result = new NumberStruct();


            return result;
        }
        public static NumberStruct operator /(NumberStruct number1, NumberStruct number2)
        {
            if (number1.MaxDigitValue != number2.MaxDigitValue)
            {
                throw new Exception("You can't do this with numbers with different MaxDigitValue");
            }
            NumberStruct result = new NumberStruct();


            return result;
        }
        public NumberStruct(int MaxDigitValue, int PlusSize, int MinusSize)
        {
            this.MaxDigitValue = MaxDigitValue;
            this.PlusSize = PlusSize;
            this.PlusDigits = new int[PlusSize];
            this.MinusSize = MinusSize;
            this.MinusDigits = new int[MinusSize];
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
            PlusDigits[4] = 0;
            PlusDigits[3] = 0;
            PlusDigits[2] = 6;
            PlusDigits[1] = 0;
            PlusDigits[0] = 0;
            MinusDigits[4] = 0;
            MinusDigits[3] = 0;
            MinusDigits[2] = 6;
            MinusDigits[1] = 0;
            MinusDigits[0] = 0;
            Console.WriteLine(ToString());
        }
        public void TEST2()
        {
            PlusDigits[4] = 0;
            PlusDigits[3] = 0;
            PlusDigits[2] = 6;
            PlusDigits[1] = 0;
            PlusDigits[0] = 0;
            MinusDigits[4] = 0;
            MinusDigits[3] = 0;
            MinusDigits[2] = 5;
            MinusDigits[1] = 0;
            MinusDigits[0] = 0;
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = PlusSize - 1; i >= 0; i--)
            {
                sb.Append(DictTranslator[this.PlusDigits[i]]);
                sb.Append('|');
            }
            sb.Append('.');
            for (int i = MinusSize - 1; i >= 0; i--)
            {
                sb.Append('|');
                sb.Append(DictTranslator[this.MinusDigits[i]]);
            }
            return sb.ToString();
        }
    }
}
