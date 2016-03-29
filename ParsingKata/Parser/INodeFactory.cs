using System.Numerics;
using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public interface INodeFactory : IBinaryExpressionFactory
  {
    IExpression CreateNumber(BigInteger matchNumber);
  }
}