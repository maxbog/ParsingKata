using System.Collections.Generic;
using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public class ExpressionList
  {

    public ExpressionList(IExpression firstExpression, IEnumerable<Operation> nextOperations)
    {
      FirstExpression = firstExpression;
      NextOperations = nextOperations;
    }

    public IExpression FirstExpression { get; private set; }
    public IEnumerable<Operation> NextOperations { get; private set; }
  }

  public class Operation
  {
    private readonly IExpression _expression;
    private readonly Operator _oper;

    public Operation(Operator oper, IExpression ex)
    {
      _oper = oper;
      _expression = ex;
    }

    private bool Equals(Operation other)
    {
      return Equals(_expression, other._expression) && _oper == other._oper;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((Operation) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((_expression?.GetHashCode() ?? 0)*397) ^ (int) _oper;
      }
    }

    public static bool operator ==(Operation left, Operation right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Operation left, Operation right)
    {
      return !Equals(left, right);
    }

    public IExpression CreateExpression(IExpression left, IBinaryExpressionFactory expressionFactory)
    {
      return expressionFactory.CreateBinaryExpression(_oper, left, _expression);
    }
  }
}