using System.Collections;
namespace NumericTransformer
{
    internal class Program
    {
        private static NumberStruct EnterFromGayBoard()
        {
            Console.WriteLine("Введите разрядность системы исчесления из которой будет осущ перевод");
            int MaxDigitValue = Convert.ToInt32(Console.ReadLine());
            string input;
            Console.WriteLine("Введите число");
            input = Console.ReadLine();
            int Index;
            Index = 0;
            List<int> PlusDigitsArrL = new List<int>();
            while (input.Substring(Index, 1) != ".")
            {
                PlusDigitsArrL.Add(input.Substring(Index, 1).ToCharArray()[0] <= '9'? input.Substring(Index, 1).ToCharArray()[0] - '0' : input.Substring(Index, 1).ToCharArray()[0] - 'W');
                Index++;
            }
            Index++;
            List<int> MinusDigitsArrL = new List<int>();
            while (Index < input.Length && input.Substring(Index, 1) != ".")
            {
                MinusDigitsArrL.Add(input.Substring(Index, 1).ToCharArray()[0] <= '9' ? input.Substring(Index, 1).ToCharArray()[0] - '0' : input.Substring(Index, 1).ToCharArray()[0] - 'W');
                Index++;
            }
            MinusDigitsArrL.Reverse();
            PlusDigitsArrL.Reverse();
            int[] MinusDigits = new int[MinusDigitsArrL.Count];
            int[] PlusDigits = new int[PlusDigitsArrL.Count];
            for (int i = 0; i < MinusDigitsArrL.Count; i++)
            {
                MinusDigits[i] = MinusDigitsArrL[i];
            }
            for (int i = 0; i < PlusDigitsArrL.Count; i++)
            {
                PlusDigits[i] = PlusDigitsArrL[i];
            }
            NumberStruct number = new NumberStruct(MaxDigitValue, PlusDigits, MinusDigits);
            return number;
        }
        private static NumberStruct ConvertToAnotherNumberSystem(NumberStruct number,int NumberSystem)
        {
            List<NumberStruct> numbers = new List<NumberStruct>();

            for(int i = 0; i < number.PlusSize; i++)
            {
                numbers.Add(new NumberStruct(NumberSystem, number.PlusDigits[i]) * (new NumberStruct(NumberSystem, number.MaxDigitValue) ^ i));
            }
            
            for(int i = 0; i < number.MinusSize; i++)
            {
                NumberStruct b = new NumberStruct(NumberSystem, number.MaxDigitValue);
                NumberStruct a = b ^ (-number.MinusSize + i);
                numbers.Add(new NumberStruct(NumberSystem, number.MinusDigits[i]) * a);
            }
            NumberStruct result = new NumberStruct(NumberSystem,0);
            foreach(NumberStruct parts in numbers)
            {
                result = result + parts;
            }
            return result;
        }
        static void Main(string[] args)
        {
            NumberStruct number = EnterFromGayBoard();
            Console.WriteLine("Введите разрядность системы исчесления в которую будет осущ перевод");
            int MaxDigitValue = Convert.ToInt32(Console.ReadLine());
            NumberStruct result = ConvertToAnotherNumberSystem((NumberStruct)number, MaxDigitValue);
            Console.WriteLine(result.ToString());
        }
    }
}