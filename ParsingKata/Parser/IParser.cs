using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public interface IParser
  {
    IExpression Parse(TokenSource source);
  }
}