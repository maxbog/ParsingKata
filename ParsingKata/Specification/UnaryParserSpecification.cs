using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ParsingKata.Ast;
using ParsingKata.Parser;
using ParsingKata.Tokenizer;
using TddEbook.TddToolkit;

namespace ParsingKata.Specification
{
  public class UnaryParserSpecification
  {
    [Test]
    public void ShouldParseNumberAsNumberExpression()
    {
      // GIVEN
      var number = Any.Integer();
      var numberExpr = Any.InstanceOf<IExpression>();
      ParserReference topLevelParser = Any.InstanceOf<ParserReference>();
      IRules rules = Substitute.For<IRules>();

      INodeFactory nodeFactory = Substitute.For<INodeFactory>();
      nodeFactory.CreateNumber(number).Returns(numberExpr);

      var source = new ListTokenSource(new List<Token> {Token.Representing(number)});

      var parser = new UnaryExpressionParser(nodeFactory, rules, topLevelParser);

      // WHEN
      var actual = parser.Parse(source);
      // THEN
      Assert.That(actual, Is.EqualTo(numberExpr));
    }

    [Test]
    public void ShouldParseExpressionInParentheses()
    {
      // GIVEN
      var topLevelExpr = Any.InstanceOf<IExpression>();

      var source = new ListTokenSource(new List<Token> { Token.Representing(Operator.LeftParen), Token.Representing(Operator.RightParen) });

      ParserReference topLevelParser = Any.InstanceOf<ParserReference>();
      IRules rules = Substitute.For<IRules>();
      rules.Parse(topLevelParser, source).Returns(topLevelExpr);

      INodeFactory nodeFactory = Substitute.For<INodeFactory>();

      var parser = new UnaryExpressionParser(nodeFactory, rules, topLevelParser);

      // WHEN
      var actual = parser.Parse(source);
      // THEN
      Assert.That(actual, Is.EqualTo(topLevelExpr));
    }

    [Test]
    public void ShouldFailWhenRightParenthesesIsMissing()
    {
      // GIVEN
      var topLevelExpr = Any.InstanceOf<IExpression>();

      var source = new ListTokenSource(new List<Token> { Token.Representing(Operator.LeftParen) });

      ParserReference topLevelParser = Any.InstanceOf<ParserReference>();
      IRules rules = Substitute.For<IRules>();
      rules.Parse(topLevelParser, source).Returns(topLevelExpr);

      INodeFactory nodeFactory = Substitute.For<INodeFactory>();

      var parser = new UnaryExpressionParser(nodeFactory, rules, topLevelParser);

      // WHEN
      var actual = parser.Parse(source);
      // THEN
      Assert.That(actual, Is.Null);
    }

    [Test]
    public void ShouldFailWhenTopLevelParserFails()
    {
      // GIVEN

      var source = new ListTokenSource(new List<Token> { Token.Representing(Operator.LeftParen), Token.Representing(Operator.RightParen) });

      ParserReference topLevelParser = Any.InstanceOf<ParserReference>();
      IRules rules = Substitute.For<IRules>();
      rules.Parse(topLevelParser, source).Returns((IExpression) null);

      INodeFactory nodeFactory = Substitute.For<INodeFactory>();

      var parser = new UnaryExpressionParser(nodeFactory, rules, topLevelParser);

      // WHEN
      var actual = parser.Parse(source);
      // THEN
      Assert.That(actual, Is.Null);
    }

    [Test]
    [TestCase(Operator.Divide)]
    [TestCase(Operator.Plus)]
    [TestCase(Operator.Minus)]
    [TestCase(Operator.Times)]
    [TestCase(Operator.RightParen)]
    public void ShouldFailWhenSourceBeginsWithOperatorOtherThanLeftParen(Operator oper)
    {
      // GIVEN
      var source = new ListTokenSource(new List<Token> { Token.Representing(oper)});

      ParserReference topLevelParser = Any.InstanceOf<ParserReference>();
      IRules rules = Substitute.For<IRules>();
      INodeFactory nodeFactory = Substitute.For<INodeFactory>();
      var parser = new UnaryExpressionParser(nodeFactory, rules, topLevelParser);

      // WHEN
      var actual = parser.Parse(source);
      // THEN
      Assert.That(actual, Is.Null);
    }
  }
}