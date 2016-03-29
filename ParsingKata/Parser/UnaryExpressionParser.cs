using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public class UnaryExpressionParser : IParser
  {
    private readonly INodeFactory _nodeFactory;
    private readonly IRules _rules;
    private readonly ParserReference _topLevelParser;

    public UnaryExpressionParser(INodeFactory nodeFactory, IRules rules, ParserReference topLevelParser)
    {
      _nodeFactory = nodeFactory;
      _rules = rules;
      _topLevelParser = topLevelParser;
    }

    public IExpression Parse(TokenSource source)
    {
      if (source.Current.Map(current => current.Type == TokenType.Number).OrElse(false))
      {
        var matchedNumber = source.MatchNumber();
        if (!matchedNumber.HasValue)
          return null;
        return _nodeFactory.CreateNumber(matchedNumber.Value);
      }

      if (source.Current.Map(current => current == Token.Representing(Operator.LeftParen)).OrElse(false))
      {
        source.Match(Token.Representing(Operator.LeftParen));

        var innerExpression = _rules.Parse(_topLevelParser, source);

        if (innerExpression == null)
          return null;

        if (source.Match(Token.Representing(Operator.RightParen)) == null)
          return null;

        return innerExpression;
      }

      return null;
    }
  }
}