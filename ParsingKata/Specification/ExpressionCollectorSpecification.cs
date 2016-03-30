using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using ParsingKata.Ast;
using ParsingKata.Parser;
using ParsingKata.Tokenizer;
using TddEbook.TddToolkit;

namespace ParsingKata.Specification
{
  public class ExpressionCollectorSpecification
  {
    [Test]
    public void ShouldCollectOnlyFirstExpressionWhenOperatorIsNotMatchedAfterFirstExpression()
    {
      // GIVEN
      var firstExpression = Any.InstanceOf<IExpression>();
      var source = Any.InstanceOf<TokenSource>();

      var matcher = Substitute.For<ITokenMatcher<Operator?>>();
      matcher.Match(source).Returns((Operator?) null);

      var rules = Substitute.For<IRules>();
      var lowerLevelParser = Any.InstanceOf<ParserReference>();

      rules.Parse(lowerLevelParser, source).Returns(firstExpression);

      var collector = new ExpressionCollector(rules, lowerLevelParser, matcher);

      // WHEN
      ExpressionList result = collector.CollectExpressions(source);

      // THEN
      Assert.That(result.FirstExpression, Is.EqualTo(firstExpression));
      Assert.That(result.NextOperations, Is.Empty);
    }

    [Test]
    public void ShouldCollectFirstExpressionAndOneOperationWhenOperatorDoesNotMatchAfterSecondExpression()
    {
      // GIVEN
      var oper = Any.InstanceOf<Operator>();
      var firstExpression = Any.InstanceOf<IExpression>();
      var secondExpression = Any.InstanceOf<IExpression>();
      var source = Any.InstanceOf<TokenSource>();

      var matcher = Substitute.For<ITokenMatcher<Operator?>>();
      matcher.Match(source).Returns(oper, (Operator?)null);

      var rules = Substitute.For<IRules>();
      var lowerLevelParser = Any.InstanceOf<ParserReference>();
      rules.Parse(lowerLevelParser, source).Returns(firstExpression, secondExpression);

      var collector = new ExpressionCollector(rules, lowerLevelParser, matcher);

      // WHEN
      ExpressionList result = collector.CollectExpressions(source);

      // THEN
      Assert.That(result.FirstExpression, Is.EqualTo(firstExpression));
      Assert.That(result.NextOperations.First(), Is.EqualTo(new Operation(oper, secondExpression)));
    }

    [Test]
    public void ShouldCollectFirstExpressionAndRestOfExpressionsAsOperations()
    {
      // GIVEN
      var expectedExpressions = Any.List<IExpression>();
      var expectedOperators = new List<Operator?>();
      var expectedOperations = new List<Operation>();
        expectedExpressions.ForEach(ex => 
      {
        var oper = Any.InstanceOf<Operator>();
        expectedOperators.Add(oper);
        expectedOperations.Add(new Operation(oper, ex));
      });

      var firstExpression = Any.InstanceOf<IExpression>();
      var source = Any.InstanceOf<TokenSource>();

      var matcher = Substitute.For<ITokenMatcher<Operator?>>();
      matcher.Match(source).Returns(expectedOperators.First(), expectedOperators.Skip(1).Concat(Enumerable.Repeat((Operator?)null, 1)).ToArray());
      
      var rules = Substitute.For<IRules>();
      var lowerLevelParser = Any.InstanceOf<ParserReference>();
      rules.Parse(lowerLevelParser, source).Returns(firstExpression, expectedExpressions.ToArray());

      var collector = new ExpressionCollector(rules, lowerLevelParser, matcher);
      // WHEN
      ExpressionList result = collector.CollectExpressions(source);

      // THEN
      Assert.That(result.FirstExpression, Is.EqualTo(firstExpression));
      Assert.That(result.NextOperations, Is.EquivalentTo(expectedOperations));
    }
  }
}