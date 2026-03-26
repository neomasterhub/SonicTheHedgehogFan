using System.Text;

public static class StringBuilderExtensions
{
  public static StringBuilder AddParLine(
    this StringBuilder sb,
    string name,
    float value,
    int digits = 0,
    string comment = null,
    bool addComment = true)
  {
    return sb.AddParLine(name, value.ToString($"F{digits}"), comment, addComment);
  }

  public static StringBuilder AddParLine<TValue>(
    this StringBuilder sb,
    string name,
    TValue value,
    string comment = null,
    bool addComment = true)
  {
    sb.Append(name).Append(": ").Append(value?.ToString());

    if (addComment && !string.IsNullOrWhiteSpace(comment))
    {
      sb.Append(comment);
    }

    return sb.AppendLine();
  }
}
