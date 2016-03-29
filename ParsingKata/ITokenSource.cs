using System;
using System.Numerics;
using ParsingKata.Utils;

namespace ParsingKata
{
  public abstract class TokenSource
  {
    public abstract void Advance();
    public abstract Optional<Token> Current { get; }
    public abstract bool Eol { get; }

    private T AdvanceAndReturn<T>(T value)
    {
      Advance();
      return value;
    }
    public Token Match(Token token)
    {
      return Current
        .Filter(current => current == token)
        .Map(AdvanceAndReturn)
        .OrElse(null);
    }


    private T MatchTokenType<T>(TokenType tokenType, Func<Token, T> func)
    {
      return Current
        .Filter(current => current.Type == tokenType)
        .Map(token => AdvanceAndReturn(func(token)))
        .OrElse(default(T));
    }
    
    public BigInteger? MatchNumber()
    {
      return MatchTokenType(TokenType.Number, current => current.NumberValue);
    }

    public Operator? MatchOperator()
    {
      return MatchTokenType(TokenType.Operator, current => current.OperatorValue);
    }
  }
}