using System;

// --- Answer for Number 4 ---
namespace DynamicSequence;

public class DynamicStringSequence
{
    private Dictionary<int, string> _sequenceRule;

    public DynamicStringSequence()
    {
        _sequenceRule = new Dictionary<int, string>();
    }

    public void AddRule(int number, string text)
    {
        _sequenceRule.Add(number, text);
    }

    public string GetDivisible(int i)
    {
        string stringReplacement = "";

        // --- Check if Rule is empty or not ---
        if (_sequenceRule.Count == 0)
        {
            Console.WriteLine("No Rule Added, please add some rules first !!! ");
            throw new Exception();
        }

        // --- Do Sequence Divisible ---
        foreach (var item in _sequenceRule)
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

}

