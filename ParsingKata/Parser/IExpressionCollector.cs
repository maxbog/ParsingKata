using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public interface IExpressionCollector
  {
    ExpressionList CollectExpressions(TokenSource source);
  }
}