using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public interface IRules
  {
    void Add(ParserReference reference, IParser parser);
    IExpression Parse(ParserReference reference, TokenSource source);
  }
}