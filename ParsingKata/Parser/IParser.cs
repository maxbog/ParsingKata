using System;
using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public interface IParser
  {
    IExpression Parse(TokenSource source);
  }
}