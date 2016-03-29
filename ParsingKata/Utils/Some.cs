using System.Collections.Generic;

namespace ParsingKata.Utils
{
  public class Some<T> : Optional<T>
  {
    private readonly T _value;

    public Some(T value)
    {
      _value = value;
    }

    public override bool IsPresent { get; } = true;
    public override T Get()
    {
      return _value;
    }

    private bool Equals(Some<T> other)
    {
      return EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Some<T>) obj);
    }

    public override int GetHashCode()
    {
      return EqualityComparer<T>.Default.GetHashCode(_value);
    }

    public static bool operator ==(Some<T> left, Some<T> right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Some<T> left, Some<T> right)
    {
      return !Equals(left, right);
    }
  }
}