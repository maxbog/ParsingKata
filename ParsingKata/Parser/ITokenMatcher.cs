namespace ParsingKata.Parser
{
  public interface ITokenMatcher<out T>
  {
    T Match(TokenSource source);
  }
}