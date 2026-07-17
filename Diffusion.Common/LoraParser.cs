using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Diffusion.Common;

public readonly struct LoraToken
{
    public LoraToken(string name, string weight)
    {
        Name = name;
        Weight = weight;
    }

    public string Name { get; }
    public string Weight { get; }
}

public static class LoraParser
{
    private static readonly Regex LoraRegex = new Regex("<lora:([^:>]+):([^>]+)>", RegexOptions.Compiled);

    public static IEnumerable<LoraToken> ParseLoraTokens(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            yield break;
        }

        foreach (Match m in LoraRegex.Matches(prompt))
        {
            var rawName = m.Groups[1].Value;
            var name = Path.GetFileName(rawName.Replace((char)47, Path.DirectorySeparatorChar));
            yield return new LoraToken(name, m.Groups[2].Value);
        }
    }
}
