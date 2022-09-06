namespace NumericTransformer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NumberStruct number = new NumberStruct(10, 5, 5);
            number.TEST1();
            NumberStruct number2 = new NumberStruct(10, 5, 5);
            number2.TEST2();
            Console.WriteLine((number - number2).ToString());
        }
    }
}