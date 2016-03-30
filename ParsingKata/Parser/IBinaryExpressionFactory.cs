using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public interface IBinaryExpressionFactory
  {
    IExpression CreateBinaryExpression(Operator oper, IExpression left, IExpression right);
  }
}