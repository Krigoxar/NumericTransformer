namespace NumericTransformer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NumberStruct number1 = new NumberStruct(16, 5, 5);
            number1.TEST1();
            NumberStruct number2 = new NumberStruct(16, 5, 5);
            number2.TEST2();
            Console.WriteLine(number1 * number2);
        }
    }
}