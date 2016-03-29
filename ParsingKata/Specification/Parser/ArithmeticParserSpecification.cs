using System.Collections.Generic;
using NUnit.Framework;
using ParsingKata.Ast;
using ParsingKata.Parser;

namespace ParsingKata.Specification.Parser
{
  public class ArithmeticParserSpecification
  {
    [Test]
    [TestCaseSource(nameof(ParserCaseSource))]
    public void ShouldParseInputCorrectly(List<Token> tokens, IExpression expectedParserOutput)
    {
      // GIVEN
      ArithmeticParser arithmeticParser = new ArithmeticParser();

      // WHEN
      var ast = arithmeticParser.Parse(new ListTokenSource(tokens));
      // THEN
      Assert.That(ast, Is.EqualTo(expectedParserOutput));
    }

    public object[][] ParserCaseSource()
    {
      Tokenizer lexer = new Tokenizer();
      return new[]
      {
        new object[] 
        {
          lexer.Tokenize("1+2"),
          new AddExpression(
            new NumberExpression(1),
            new NumberExpression(2))
        },

        new object[]
        {
          lexer.Tokenize("1+2*3"),
          new AddExpression(
            new NumberExpression(1),
            new MultiplyExpression(
              new NumberExpression(2),
              new NumberExpression(3)))
        },

        new object[]
        {
          lexer.Tokenize("(1+2)*3"),
          new MultiplyExpression(
            new AddExpression(
              new NumberExpression(1),
                new NumberExpression(2)),
                new NumberExpression(3))
        },

        new object[]
        {
          lexer.Tokenize("1*2+3*4"),
          new AddExpression( 
            new MultiplyExpression(
                new NumberExpression(1),
                new NumberExpression(2)),
            new MultiplyExpression(
                new NumberExpression(3),
                new NumberExpression(4)))
        },
      };
    }

  }
}