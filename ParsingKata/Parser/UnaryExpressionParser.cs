using ParsingKata.Ast;
using ParsingKata.Tokenizer;

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
        return ParseNumber(source);
      }

      if (source.Current.Map(current => current == Token.Representing(Operator.LeftParen)).OrElse(false))
      {
        return ParseInnerExpression(source);
      }

      return null;
    }

    private IExpression ParseInnerExpression(TokenSource source)
    {
      source.Match(Token.Representing(Operator.LeftParen));

      var innerExpression = _rules.Parse(_topLevelParser, source);

      if (innerExpression == null)
        return null;

      if (source.Match(Token.Representing(Operator.RightParen)) == null)
        return null;

      return innerExpression;
    }

    private IExpression ParseNumber(TokenSource source)
    {
      var matchedNumber = source.MatchNumber();
      if (!matchedNumber.HasValue)
        return null;
      return _nodeFactory.CreateNumber(matchedNumber.Value);
    }
  }
}