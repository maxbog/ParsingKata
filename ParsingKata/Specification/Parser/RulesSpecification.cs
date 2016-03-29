using NSubstitute;
using NUnit.Framework;
using ParsingKata.Ast;
using ParsingKata.Parser;
using TddEbook.TddToolkit;

namespace ParsingKata.Specification.Parser
{
  public class RulesSpecification
  {
    [Test]
    public void ShouldThrowWhenParserWasNotAdded()
    {
      // GIVEN
      var source = Any.InstanceOf<TokenSource>();
      var reference = Any.InstanceOf<ParserReference>();

      var rules = new Rules();
      // WHEN
      Assert.That(() => rules.Parse(reference, source), Throws.InvalidOperationException);
    }

    [Test]
    public void ShouldPassParsingToAddedParse()
    {
      // GIVEN
      var source = Any.InstanceOf<TokenSource>();
      var reference = Any.InstanceOf<ParserReference>();
      var expression = Any.InstanceOf<IExpression>();

      var parser = Substitute.For<IParser>();
      parser.Parse(source).Returns(expression);

      var rules = new Rules();
      // WHEN
      rules.Add(reference, parser);
      var actual = rules.Parse(reference, source);

      // THEN
      Assert.That(actual, Is.EqualTo(expression));
    }
  }
}