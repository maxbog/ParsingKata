namespace ParsingKata.Ast
{
  public class AddExpression : BinaryExpression
  {
    public AddExpression(IExpression left, IExpression right) : base(left, right)
    {
    }
  }
}