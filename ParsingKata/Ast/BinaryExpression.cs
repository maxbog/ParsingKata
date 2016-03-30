namespace ParsingKata.Ast
{
  public abstract class BinaryExpression : IExpression
  {
    protected readonly IExpression Left;
    protected readonly IExpression Right;

    protected BinaryExpression(IExpression left, IExpression right)
    {
      Left = left;
      Right = right;
    }

    private bool Equals(BinaryExpression other)
    {
      return Equals(Left, other.Left) && Equals(Right, other.Right);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((BinaryExpression) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((Left?.GetHashCode() ?? 0)*397) ^ (Right?.GetHashCode() ?? 0);
      }
    }

    public static bool operator ==(BinaryExpression left, BinaryExpression right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(BinaryExpression left, BinaryExpression right)
    {
      return !Equals(left, right);
    }
  }
}