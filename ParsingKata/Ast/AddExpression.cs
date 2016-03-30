namespace ParsingKata.Ast
{
  public class AddExpression : BinaryExpression
  {
    public AddExpression(IExpression left, IExpression right) : base(left, right)
    {
    }

    public override string ToString()
    {
      return $"[Add {Left} {Right}]";
    }
  }
}