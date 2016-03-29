using System.Collections.Generic;
using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public class ExpressionCollector : IExpressionCollector
  {
    private readonly IRules _rules;
    private readonly ParserReference _lowerLevelParser;
    private readonly TokenMatcher _matcher;

    public ExpressionCollector(IRules rules, ParserReference lowerLevelParser, TokenMatcher matcher)
    {
      _rules = rules;
      _lowerLevelParser = lowerLevelParser;
      _matcher = matcher;
    }

    public ExpressionList CollectExpressions(TokenSource source)
    {
      var firstExpression = _rules.Parse(_lowerLevelParser, source);
      if (firstExpression == null)
        return null;

      var nextExpressions = new List<Operation>();
      if (!CollectTailExpression(source, nextExpressions))
        return null;

      return new ExpressionList(firstExpression, nextExpressions);
    }

    private bool CollectTailExpression(TokenSource source, List<Operation> operations)
    {
      var oper = _matcher(source);
      if (oper.HasValue)
      {
        var currentExpression = _rules.Parse(_lowerLevelParser, source);
        if (currentExpression == null)
          return false;
        operations.Add(new Operation(oper.Value, currentExpression));
        return CollectTailExpression(source, operations);
      }
      return true;
    }
  }
}