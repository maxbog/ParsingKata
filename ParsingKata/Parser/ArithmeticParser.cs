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

      _rules.Add(
        _addRef, 
        new LeftAssociativeExpressionParser(
          new ExpressionCollector(
            _rules, 
            multRef, 
            new OperatorMatcher(Operator.Plus, Operator.Minus)
          ),
          nodeFactory
        )
      );

      _rules.Add(
        multRef, 
        new LeftAssociativeExpressionParser(
          new ExpressionCollector(
            _rules, 
            unaryRef,
            new OperatorMatcher(Operator.Times, Operator.Divide)),
          nodeFactory
        )
      );

      _rules.Add(
        unaryRef, 
        new UnaryExpressionParser(
          nodeFactory,
          _rules,
          _addRef
        )
      );
    }

    public IExpression Parse(TokenSource source)
    {
      return _rules.Parse(_addRef, source);
    }
  }
}