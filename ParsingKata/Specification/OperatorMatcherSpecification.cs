using System.Collections.Generic;
using NUnit.Framework;
using ParsingKata.Parser;
using TddEbook.TddToolkit;

namespace ParsingKata.Specification
{
  public class OperatorMatcherSpecification
  {
    [Test]
    public void ShouldMatchSpecifiedOperator()
    {
      // GIVEN
      var expectedOperator = Any.InstanceOf<Operator>();
      var source = new ListTokenSource(new List<Token> {Token.Representing(expectedOperator)});

      
      var matcher = new OperatorMatcher(expectedOperator);

      // WHEN
      var actual = matcher.Match(source);

      // THEN
      Assert.That(actual, Is.EqualTo(expectedOperator));

    }

    [Test]
    public void ShouldNotMatchOtherOperator()
    {
      // GIVEN
      var expectedOperator = Any.InstanceOf<Operator>();
      var otherOperator = Any.OtherThan(expectedOperator);
      var source = new ListTokenSource(new List<Token> { Token.Representing(otherOperator) });

      var matcher = new OperatorMatcher(expectedOperator);

      // WHEN
      var actual = matcher.Match(source);

      // THEN
      Assert.That(actual, Is.Null);
    }

    [Test]
    public void ShouldNotMatchOtherToken()
    {
      // GIVEN
      var expectedOperator = Any.InstanceOf<Operator>();
      var source = new ListTokenSource(new List<Token> { Any.InstanceOf<Token>() });

      var matcher = new OperatorMatcher(expectedOperator);

      // WHEN
      var actual = matcher.Match(source);

      // THEN
      Assert.That(actual, Is.Null);
    }
  }
}