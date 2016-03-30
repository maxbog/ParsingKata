using System;

namespace ParsingKata.Utils
{
  public class Empty<T> : Optional<T>
  {
    private const int HashCode = 43;
    public override bool IsPresent { get; } = false;
    public override T Get()
    {
      throw new InvalidOperationException();
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return true;
    }

    public override int GetHashCode()
    {
      return HashCode;
    }

    public static bool operator ==(Empty<T> left, Empty<T> right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Empty<T> left, Empty<T> right)
    {
      return !Equals(left, right);
    }
  }
}