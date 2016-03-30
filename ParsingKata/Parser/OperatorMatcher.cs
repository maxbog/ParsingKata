using System.Collections.Generic;
using System.Linq;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public class OperatorMatcher : ITokenMatcher<Operator?>
  {
    private readonly IEnumerable<Operator> _operators;

    public OperatorMatcher(params Operator[] operators)
    {
      _operators = operators;
    }

    public Operator? Match(TokenSource source)
    {
      var operatorMatches = source.Current
        .Filter(token => token.Type == TokenType.Operator)
        .Map(token => token.OperatorValue.HasValue && _operators.Contains(token.OperatorValue.Value))
        .OrElse(false);

      if (operatorMatches)
        return source.MatchOperator();

      return null;
    }
  }
}