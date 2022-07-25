using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class TextRange
{
    public static readonly char[] punctuation = "!\"#$%&'()*+, -./:;<=>?@[\\]^_`{|}~".ToArray();
    public static readonly char[] asciiLowerCase = "abcdefghijklmnopqrstuvwxyz".ToArray();
    public static readonly char[] asciiUpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
    public static readonly char[] asciiLetters = asciiLowerCase.Union(asciiUpperCase).ToArray();
    public static readonly char[] decdigits = "0123456789".ToArray();
    public static readonly char[] hexDigits = "0123456789abcdefABCDEF".ToArray();
    public static readonly char[] octDigits = "01234567".ToArray();

    public char[] Chars { get; }
    public BasicRange<int> TextLengthRange { get; }

    public bool GenerateMode { get; }

    public string[] Words { get; }

    public TextRange(char[] chars, BasicRange<int> textLengthRange)
    {
        Chars = chars.Distinct().ToArray();
        TextLengthRange = textLengthRange;
        GenerateMode = true;
    }

    public TextRange(string[] words)
    {
        Words = words;
        GenerateMode = false;
    }
}
