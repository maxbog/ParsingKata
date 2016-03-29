using System.Numerics;

namespace ParsingKata.Ast
{
  public interface IExpression
  {
     
  }

  class NumberExpression : IExpression
  {
    private BigInteger _value;

    public NumberExpression(BigInteger value)
    {
      _value = value;
    }

    protected bool Equals(NumberExpression other)
    {
      return _value.Equals(other._value);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((NumberExpression) obj);
    }

    public override int GetHashCode()
    {
      return _value.GetHashCode();
    }

    public static bool operator ==(NumberExpression left, NumberExpression right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(NumberExpression left, NumberExpression right)
    {
      return !Equals(left, right);
    }
  }
}