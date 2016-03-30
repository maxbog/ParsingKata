namespace ParsingKata.Parser
{
  public class ParserReference
  {
    private readonly string _name;

    public ParserReference(string name)
    {
      _name = name;
    }

    public override string ToString()
    {
      return _name;
    }
  }
}