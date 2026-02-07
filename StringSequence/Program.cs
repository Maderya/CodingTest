using System;
using DynamicSequence;

public class stringControl
{

    // --- Answer for Number 1 to 3 ---
    public static string Divisible(int i)
    {
        Dictionary<int, string> newDict = new Dictionary<int, string>();
        string stringReplacement = "";

        newDict.Add(3, "foo");
        newDict.Add(4, "baz");
        newDict.Add(5, "bar");
        newDict.Add(7, "jazz");
        newDict.Add(9, "huzz");

        foreach (var item in newDict)
        {
            if (i % item.Key == 0)
            {
                stringReplacement = stringReplacement + item.Value;
            }
        }

        if (stringReplacement == "")
        {
            stringReplacement = i.ToString();
        }
        return stringReplacement;
    }

    public static void Main()
    {
        // Sequence Max Number
        int n = 35;
        string result;

        for (int i = 1; i <= n; i++)
        {
            result = Divisible(i);
            Console.Write($"{result} ");

        }
        Console.Write("\n");

        // Answer for number 4
        DynamicStringSequence newSequence = new DynamicStringSequence();
        newSequence.AddRule(3, "foo");
        newSequence.AddRule(4, "baz");
        newSequence.AddRule(5, "bar");
        newSequence.AddRule(7, "jazz");
        newSequence.AddRule(9, "huzz");

        for (int i = 1; i <= n; i++)
        {
            result = newSequence.GetDivisible(i);
            Console.Write($"{result} ");
        }
    }
}
