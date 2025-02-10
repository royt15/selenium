/*
 * Selenium C# for Kaljapp
 * Just trying out Selenium and C#...
 * 
 * See Test files for actual tests
 * 
 */
internal class Program
{

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello!");
        PrintArgs(args);
    }

    private static void PrintArgs(string[] args)
    {
        Console.WriteLine("Command line args: " + args.Length);
        foreach (var item in args)
        {
            Console.WriteLine($"\t{item}");
        }
    }

}
