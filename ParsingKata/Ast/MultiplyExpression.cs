namespace ParsingKata.Ast
{
  public class MultiplyExpression : BinaryExpression
  {
    public MultiplyExpression(IExpression left, IExpression right) : base(left, right)
    {
    }
  }
}