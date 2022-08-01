using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Base.Parameters;

public class StringParameter : GenericParameter<string>
{
    public static readonly char[] punctuation = "!\"#$%&'()*+, -./:;<=>?@[\\]^_`{|}~".ToArray();
    public static readonly char[] asciiLowerCase = "abcdefghijklmnopqrstuvwxyz".ToArray();
    public static readonly char[] asciiUpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
    public static readonly char[] asciiLetters = asciiLowerCase.Union(asciiUpperCase).ToArray();
    public static readonly char[] decdigits = "0123456789".ToArray();
    public static readonly char[] hexDigits = "0123456789abcdefABCDEF".ToArray();
    public static readonly char[] octDigits = "01234567".ToArray();

    public IntParameter Length { get; } = new IntParameter() { Min = 1, Max = 6 };
    public char[] CharactersSet { get; set; } = (char[])asciiUpperCase.Clone();

    public StringParameter(string defaultValue = "") : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Length.Randomize(r);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Length; i++)
            sb.Append(CharactersSet[r.Next(0, CharactersSet.Length)]);
        Value = sb.ToString();
    }
}