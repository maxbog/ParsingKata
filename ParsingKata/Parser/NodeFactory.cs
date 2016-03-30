using System.Numerics;
using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public class NodeFactory : INodeFactory
  {
    public IExpression CreateBinaryExpression(Operator oper, IExpression left, IExpression right)
    {
      switch (oper)
      {
        case Operator.Plus:
          return new AddExpression(left, right);
        case Operator.Minus:
          return new SubtractExpression(left, right);
        case Operator.Times:
          return new MultiplyExpression(left, right);
        case Operator.Divide:
          return new DivideExpression(left, right);
      }
      return null;
    }

    public IExpression CreateNumber(BigInteger matchNumber)
    {
      return new NumberExpression(matchNumber);
    }
  }
}