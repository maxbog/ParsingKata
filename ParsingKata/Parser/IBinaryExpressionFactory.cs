using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public interface IBinaryExpressionFactory
  {
    IExpression CreateBinaryExpression(Operator oper, IExpression left, IExpression right);
  }
}