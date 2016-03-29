namespace ParsingKata.Ast
{
  public abstract class BinaryExpression : IExpression
  {
    private readonly IExpression _left;
    private readonly IExpression _right;

    protected BinaryExpression(IExpression left, IExpression right)
    {
      _left = left;
      _right = right;
    }

    protected bool Equals(BinaryExpression other)
    {
      return Equals(_left, other._left) && Equals(_right, other._right);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((BinaryExpression) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((_left != null ? _left.GetHashCode() : 0)*397) ^ (_right != null ? _right.GetHashCode() : 0);
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