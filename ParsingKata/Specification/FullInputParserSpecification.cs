using NSubstitute;
using NUnit.Framework;
using ParsingKata.Ast;
using ParsingKata.Parser;
using ParsingKata.Tokenizer;
using TddEbook.TddToolkit;

namespace ParsingKata.Specification
{
  public class FullInputParserSpecification
  {
    [Test]
    public void ShouldPassWhenWholeInputWasConsumed()
    {
      // GIVEN
      var source = Substitute.For<TokenSource>();
      source.Eol.Returns(true);

      var expected = Any.InstanceOf<IExpression>();

      var internalParser = Any.InstanceOf<ParserReference>();
      var rules = Substitute.For<IRules>();
      rules.Parse(internalParser, source).Returns(expected);

      var parser = new FullInputParser(rules, internalParser);

      // WHEN
      var parsed = parser.Parse(source);

      // THEN
      Assert.That(parsed, Is.EqualTo(expected));
    }

    [Test]
    public void ShouldFailWhenSourceHasSomeTokensLeft()
    {
      // GIVEN
      var source = Substitute.For<TokenSource>();
      source.Eol.Returns(false);

      var expected = Any.InstanceOf<IExpression>();

      var internalParser = Any.InstanceOf<ParserReference>();
      var rules = Substitute.For<IRules>();
      rules.Parse(internalParser, source).Returns(expected);

      var parser = new FullInputParser(rules, internalParser);

      // WHEN
      var parsed = parser.Parse(source);

      // THEN
      Assert.That(parsed, Is.Null);
    }
  }
}