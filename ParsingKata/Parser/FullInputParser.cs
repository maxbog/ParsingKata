using ParsingKata.Ast;
using ParsingKata.Tokenizer;

namespace ParsingKata.Parser
{
  public class FullInputParser : IParser
  {
    private readonly IRules _rules;
    private readonly ParserReference _internalParser;

    public FullInputParser(IRules rules, ParserReference internalParser)
    {
      _rules = rules;
      _internalParser = internalParser;
    }

    public IExpression Parse(TokenSource source)
    {
      var parsed = _rules.Parse(_internalParser, source);
      if (source.Eol)
        return parsed;

      return null;
    }
  }
}