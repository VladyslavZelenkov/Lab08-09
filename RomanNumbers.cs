using System;
using System.Collections.Generic;
using System.Text;

public struct RomanNumber
{
    
    private static List<(int arabic, string roman, int next)> conversionTable = new List<(int arabic, string roman, int next)>()
    {
        (1000, "M", 1000), (900, "CM", 90), (500, "D", 100), (400, "CD", 90),
        (100, "C", 100), (90, "XC", 9), (50, "L", 10), (40, "XL", 9),
        (10, "X", 100), (9, "IX", 0), (5, "V", 1), (4, "IV", 0), (1, "I", 1)
    };

    // Start positions in the conversion table based on the 'next' value for efficiency
    private static Dictionary<int, int> start = new Dictionary<int, int>();

    private int val; // The integer value of the Roman numeral

    static RomanNumber()
    {
        // Initialize the start dictionary for quick lookup
        for (int i = 0; i < conversionTable.Count; ++i)
            start[conversionTable[i].arabic] = i;

        // Add a termination condition to the dictionary
        start[0] = conversionTable.Count;
    }

    // Constructor: Converts a Roman numeral string to its integer value
    public RomanNumber(string s)
    {
        val = 0;
        int count = 0; // Counter for repetitions
        int i, ii;
        if (string.IsNullOrWhiteSpace(s))
            throw new ArgumentException("Invalid Roman numeral.");

        s = s.Trim();
        ii = i = 0;

        while (s != string.Empty && i < conversionTable.Count)
        {
            if (s.StartsWith(conversionTable[i].roman)) // Match the Roman numeral at position i
            {
                val += conversionTable[i].arabic; // Add the Arabic value
                s = s.Substring(conversionTable[i].roman.Length); // Remove the matched Roman numeral

                // Check if we need to move to the next valid numeral
                if (i != start[conversionTable[i].next])
                    ii = start[conversionTable[i].next];
                else if (++count == 3) // Allow at most 3 repetitions (e.g., "III")
                    ii = i + 1;
            }
            else
            {
                ii = i + 1; // Move to the next Roman numeral in the table
            }

            if (ii > i) // If we've moved to a new numeral, reset the repetition counter
            {
                i = ii;
                count = 0;
            }
        }

        // If there are leftover characters, the Roman numeral is invalid
        if (s != string.Empty)
            throw new ArgumentException("Invalid Roman numeral.");
    }

    // Implicit conversion from RomanNumber to int
    public static implicit operator int(RomanNumber x) => x.val;

    // Hash code based on the integer value
    public override int GetHashCode() => val.GetHashCode();

    // Converts the integer value back to a Roman numeral string
    public override string ToString()
    {
        int v = val;
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < conversionTable.Count; ++i)
        {
            // Append Roman numerals as long as their Arabic value fits into v
            while (v >= conversionTable[i].arabic && v > 0)
            {
                v -= conversionTable[i].arabic;
                sb.Append(conversionTable[i].roman);
            }
        }

        return sb.ToString();
    }
}
