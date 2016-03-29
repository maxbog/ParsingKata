using System;
using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public class ArithmeticParser : IParser
  {
    private readonly ParserReference _addRef;
    private readonly IRules _rules;

    public ArithmeticParser()
    {
      INodeFactory nodeFactory = new NodeFactory();
      _rules = new Rules();

      var multRef = new ParserReference();
      _addRef = new ParserReference();
      var unaryRef = new ParserReference();

      _rules.Add(_addRef, new LeftAssociativeExpressionParser(
        new ExpressionCollector(_rules, multRef, TryMatchAdditiveOperator),
        nodeFactory
        ));

      _rules.Add(multRef, new LeftAssociativeExpressionParser(
        new ExpressionCollector(_rules, unaryRef, TryMatchMultiplicativeOperator),
        nodeFactory
        ));

      _rules.Add(unaryRef, new UnaryExpressionParser(
        nodeFactory,
        _rules,
        _addRef
        ));
    }

    private Operator? TryMatchAdditiveOperator(TokenSource source)
    {
      var operatorMatches = source.Current.Map(IsAdditiveOperator).OrElse(false);
      if (operatorMatches)
        return source.MatchOperator();

      return null;
    }

    private Operator? TryMatchMultiplicativeOperator(TokenSource source)
    {
      var operatorMatches = source.Current.Map(IsMultiplicativeOperator).OrElse(false);
      if (operatorMatches)
        return source.MatchOperator();

      return null;
    }

    private bool IsMultiplicativeOperator(Token token)
    {
      return token == Token.Representing(Operator.Times) || token == Token.Representing(Operator.Divide);
    }
    private bool IsAdditiveOperator(Token token)
    {
      return token == Token.Representing(Operator.Plus) || token == Token.Representing(Operator.Minus);
    }
    

    public IExpression Parse(TokenSource source)
    {
      return _rules.Parse(_addRef, source);
    }
  }
}