using System;
using System.Collections.Generic;
using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public class Rules : IRules
  {
    private readonly Dictionary<ParserReference, IParser> _rules = new Dictionary<ParserReference, IParser>();

    public void Add(ParserReference reference, IParser parser)
    {
      _rules.Add(reference, parser);
    }

    public IExpression Parse(ParserReference reference, TokenSource source)
    {
      if(!_rules.ContainsKey(reference))
        throw new InvalidOperationException();
      return _rules[reference].Parse(source);
    }

  }
}