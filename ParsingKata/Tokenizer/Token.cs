using System.Numerics;

namespace ParsingKata.Tokenizer
{
  public class Token
  {
    public TokenType Type { get; }
    
    private Token(BigInteger numberValue)
    {
      Type = TokenType.Number;
      NumberValue = numberValue;
      OperatorValue = null;
    }

    private Token(Operator operatorValue)
    {
      Type = TokenType.Operator;
      NumberValue = null;
      OperatorValue = operatorValue;
    }
    
    public BigInteger? NumberValue { get; }

    public Operator? OperatorValue { get; }

    public static Token Representing(BigInteger value)
    {
      return new Token(value);
    }
    
    public static Token Representing(Operator value)
    {
      return new Token(value);
    }

    private bool Equals(Token other)
    {
      return Type == other.Type && NumberValue.Equals(other.NumberValue) && OperatorValue == other.OperatorValue;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((Token) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = (int) Type;
        hashCode = (hashCode*397) ^ NumberValue.GetHashCode();
        hashCode = (hashCode*397) ^ OperatorValue.GetHashCode();
        return hashCode;
      }
    }

    public static bool operator ==(Token left, Token right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Token left, Token right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      switch (Type)
      {
        case TokenType.Number:
          return $"Token[type=number, number={NumberValue}]";
        case TokenType.Operator:
          return $"Token[type=operator, oper={OperatorValue}]";
      }
      return "Token[unknown]";
    }
  }
}