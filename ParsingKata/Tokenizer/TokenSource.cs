using System;
using System.Numerics;
using ParsingKata.Utils;

namespace ParsingKata.Tokenizer
{
  public abstract class TokenSource
  {
    protected abstract void Advance();
    public abstract Optional<Token> Current { get; }
    public abstract bool Eol { get; }

    private T AdvanceAndReturn<T>(T value)
    {
      Advance();
      return value;
    }

    public Optional<Token> Match(Token token)
    {
      return Current
        .Filter(current => current == token)
        .Map(AdvanceAndReturn);
    }


    private Optional<T> MatchTokenType2<T>(TokenType tokenType, Func<Token, T> func)
    {
      return Current
        .Filter(current => current.Type == tokenType)
        .Map(token => AdvanceAndReturn(func(token)));
    }

    public BigInteger? MatchNumber()
    {
      return  MatchTokenType2(TokenType.Number, current => current.NumberValue)
        .OrElse(default(BigInteger?));
    }

    public Optional<Operator> MatchOperator()
    {
      Optional<Operator> value = MatchTokenType2(
        TokenType.Operator, 
        current => current.OperatorValue)
        .OrElse(Optional<Operator>.Empty);
      return value;
    }
  }
}