using System.Numerics;

namespace ParsingKata.Ast
{
  class NumberExpression : IExpression
  {
    private readonly BigInteger _value;

    public NumberExpression(BigInteger value)
    {
      _value = value;
    }

    private bool Equals(NumberExpression other)
    {
      return _value.Equals(other._value);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
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


    public override string ToString()
    {
      return $"[Num {_value}]";
    }
  }
}