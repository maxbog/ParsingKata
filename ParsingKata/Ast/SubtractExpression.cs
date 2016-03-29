namespace ParsingKata.Ast
{
  public class SubtractExpression : BinaryExpression
  {
    public SubtractExpression(IExpression left, IExpression right) : base(left, right)
    {
    }
  }
}