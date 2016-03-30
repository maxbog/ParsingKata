using System.Linq;
using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public class LeftAssociativeExpressionParser : IParser
  {
    private readonly IExpressionCollector _collector;
    private readonly IBinaryExpressionFactory _expressionFactory;

    public LeftAssociativeExpressionParser(IExpressionCollector collector, IBinaryExpressionFactory expressionFactory)
    {
      _collector = collector;
      _expressionFactory = expressionFactory;
    }

    public IExpression Parse(TokenSource source)
    {
      var operations = _collector.CollectExpressions(source);
      if (operations == null)
        return null;
      return LeftAssociativeCombine(operations);
    }

    private IExpression LeftAssociativeCombine(ExpressionList operations)
    {
      return operations.NextOperations
        .Aggregate(
          operations.FirstExpression, 
          (current, operation) => operation.CreateExpression(current, _expressionFactory));
    }
  }
}