using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public class ArithmeticParser : IParser
  {
    private readonly IRules _rules;
    private readonly ParserReference _fullInputRef;

    public ArithmeticParser()
    {
      INodeFactory nodeFactory = new NodeFactory();
      _rules = new Rules();

      var multRef = new ParserReference("mult");
      var addRef = new ParserReference("add");
      var atomRef = new ParserReference("atom");
      _fullInputRef = new ParserReference("fullInput");

      _rules.Add(
        addRef, 
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
            atomRef,
            new OperatorMatcher(Operator.Times, Operator.Divide)),
          nodeFactory
        )
      );

      _rules.Add(
        atomRef, 
        new AtomExpressionParser(
          nodeFactory,
          _rules,
          addRef
        )
      );
      _rules.Add(
        _fullInputRef,
        new FullInputParser(_rules, addRef)
      );
    }

    public IExpression Parse(TokenSource source)
    {
      return _rules.Parse(_fullInputRef, source);
    }
  }
}