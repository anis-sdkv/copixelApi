using System.Text;
using System.Text.Json;

namespace CopixelApi.Api.Utils;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) =>
        ToSnakeCase(name);
    private static string ToSnakeCase(string text)
    {
        var sb = new StringBuilder
        {
            Capacity = 0,
            Length = 0
        };
        sb.Append(char.ToLowerInvariant(text[0]));
        for (var i = 1; i < text.Length; ++i)
        {
            var c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
                sb.Append(c);
        }

        return sb.ToString();
    }
}