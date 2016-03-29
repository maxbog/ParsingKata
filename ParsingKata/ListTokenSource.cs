using System.Collections.Generic;
using ParsingKata.Utils;

namespace ParsingKata
{
  public class ListTokenSource : TokenSource
  {
    private int _currentToken;
    private readonly List<Token> _input;

    public ListTokenSource(List<Token> input)
    {
      _input = input;
      _currentToken = 0;
    }

    public override void Advance() => _currentToken++;

    public override Optional<Token> Current => Eol ? Optional<Token>.Empty : Optional<Token>.Of(_input[_currentToken]);

    public override bool Eol => _currentToken >= _input.Count;
  }
}