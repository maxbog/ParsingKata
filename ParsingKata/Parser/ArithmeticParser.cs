using ParsingKata.Ast;
using ParsingKata.Tokenizer;

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

      var multRef = new ParserReference("mult");
      _addRef = new ParserReference("add");
      var unaryRef = new ParserReference("unary");

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
      var parsed = _rules.Parse(_addRef, source);
      if (!source.Eol)
        return null;
      return parsed;
    }
  }
}