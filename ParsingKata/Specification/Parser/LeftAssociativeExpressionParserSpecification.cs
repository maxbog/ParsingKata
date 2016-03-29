using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ParsingKata.Ast;
using ParsingKata.Parser;
using TddEbook.TddToolkit;

namespace ParsingKata.Specification.Parser
{
  public class LeftAssociativeExpressionParserSpecification
  {
    [Test]
    public void ShouldReturnFirstExpressionWhenOperationListIsEmpty()
    {
      // GIVEN
      var source = Any.InstanceOf<TokenSource>();
      var list = new ExpressionList(Any.InstanceOf<IExpression>(), new List<Operation>());
      var expressionFactory = Any.InstanceOf<IBinaryExpressionFactory>();

      var collector = Substitute.For<IExpressionCollector>();
      collector.CollectExpressions(source).Returns(list);

      var parser = new LeftAssociativeExpressionParser(collector, expressionFactory);
      // WHEN
      IExpression result = parser.Parse(source);

      // THEN
      Assert.That(result, Is.EqualTo(list.FirstExpression));
    }

    [Test]
    public void ShouldReturnCombinedExpressionWhenListHasOneElement()
    {
      // GIVEN
      var source = Any.InstanceOf<TokenSource>();
      var firstExpression = Any.InstanceOf<IExpression>();
      var secondExpression = Any.InstanceOf<IExpression>();
      var combinedExpression = Any.InstanceOf<IExpression>();
      Operator firstOperator = Any.InstanceOf<Operator>();

      var list = new ExpressionList(firstExpression, new List<Operation> {new Operation(firstOperator, secondExpression)});

      var expressionFactory = Substitute.For<IBinaryExpressionFactory>();
      expressionFactory.CreateBinaryExpression(firstOperator, firstExpression, secondExpression)
        .Returns(combinedExpression);

      var collector = Substitute.For<IExpressionCollector>();
      collector.CollectExpressions(source).Returns(list);

      var parser = new LeftAssociativeExpressionParser(collector, expressionFactory);
      // WHEN
      IExpression result = parser.Parse(source);

      // THEN
      Assert.That(result, Is.EqualTo(combinedExpression));
    }

    [Test]
    public void ShouldReturnCombinedExpressionWhenListHasTwoElements()
    {
      // GIVEN
      var source = Any.InstanceOf<TokenSource>();
      var firstExpression = Any.InstanceOf<IExpression>();
      var secondExpression = Any.InstanceOf<IExpression>();
      var thirdExpression = Any.InstanceOf<IExpression>();

      var firstCombinedExpression = Any.InstanceOf<IExpression>();
      var secondCombinedExpression = Any.InstanceOf<IExpression>();

      Operator firstOperator = Any.InstanceOf<Operator>();
      Operator secondOperator = Any.InstanceOf<Operator>();

      var list = new ExpressionList(firstExpression, 
        new List<Operation>
        {
          new Operation(firstOperator, secondExpression),
          new Operation(secondOperator, thirdExpression)
        });

      var expressionFactory = Substitute.For<IBinaryExpressionFactory>();
      expressionFactory.CreateBinaryExpression(firstOperator, firstExpression, secondExpression)
        .Returns(firstCombinedExpression);
      expressionFactory.CreateBinaryExpression(secondOperator, firstCombinedExpression, thirdExpression)
        .Returns(secondCombinedExpression);

      var collector = Substitute.For<IExpressionCollector>();
      collector.CollectExpressions(source).Returns(list);

      var parser = new LeftAssociativeExpressionParser(collector, expressionFactory);
      // WHEN
      IExpression result = parser.Parse(source);

      // THEN
      Assert.That(result, Is.EqualTo(secondCombinedExpression));
    }
  }
}