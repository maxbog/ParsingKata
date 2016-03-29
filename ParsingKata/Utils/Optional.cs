using System;

namespace ParsingKata.Utils
{
  public abstract class Optional<T>
  {
    public abstract bool IsPresent { get; }
    public static Optional<T> Empty { get; } = new Empty<T>();

    public static Optional<T> Of(T value)
    {
      if(value == null)
        throw new NullReferenceException();
      return new Some<T>(value);
    }
    public static Optional<T> OfNullable(T value)
    {
      if (value == null)
        return Empty;
      return new Some<T>(value);
    }

    public abstract T Get();

    public Optional<U> FlatMap<U>(Func<T, Optional<U>> mapper)
    {
      return IsPresent ? mapper(Get()) : Optional<U>.Empty;
    }


    public T OrElseGet(Func<T> other)
    {
      if (IsPresent)
        return Get();

      return other();
    }

    public Optional<T> Filter(Predicate<T> predicate)
    {
      return FlatMap(x => predicate(Get()) ? this : Empty);
    }

    public Optional<U> Map<U>(Func<T, U> mapper)
    {
      return FlatMap(x => Optional<U>.Of(mapper(x)));
    }


    public T OrElseThrow<X>(Func<X> other) where X : Exception
    {
      return OrElseGet(() => { throw other(); });
    }
    public T OrElse(T other)
    {
      return OrElseGet(() => other);
    }
  }
}