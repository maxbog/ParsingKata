namespace ParsingKata.Ast
{
  public class DivideExpression : BinaryExpression
  {
    public DivideExpression(IExpression left, IExpression right) : base(left, right)
    {
    }
  }
}