using System.Collections.Generic;
using System.Numerics;

namespace ParsingKata.Tokenizer
{
  public class Tokenizer
  {
    private static readonly Dictionary<char, Operator> OperatorMap = new Dictionary<char, Operator> {
        {'+', Operator.Plus},
        {'-', Operator.Minus},
        {'/', Operator.Divide},
        {'*', Operator.Times},
        {'(', Operator.LeftParen},
        {')', Operator.RightParen},
    };

    public List<Token> Tokenize(string input)
    {
      List<Token> tokens = new List<Token>();
      TokenizerState state = new TokenizerState(input);
      while (!state.Eol)
      {
        if (OperatorMap.ContainsKey(state.CurrentChar))
        {
          tokens.Add(Token.Representing(TokenizeSingleOperator(state)));
        }
        else if (char.IsDigit(state.CurrentChar))
        {
          tokens.Add(Token.Representing(TokenizeSingleNumber(state)));
        }
        else if (char.IsWhiteSpace(state.CurrentChar))
        {
          state.Advance();
        }
        else {
          return null;
        }
      }
      return tokens;
    }

    private Operator TokenizeSingleOperator(TokenizerState state)
    {
      Operator oper = OperatorMap[state.CurrentChar];
      state.Advance();
      return oper;
    }
    
    private BigInteger TokenizeSingleNumber(TokenizerState state)
    {
      string currentNumber = "";

      while (!state.Eol && char.IsDigit(state.CurrentChar))
      {
        currentNumber += state.CurrentChar;
        state.Advance();
      }
      return BigInteger.Parse(currentNumber);
    }

    private class TokenizerState
    {
      public TokenizerState(string input)
      {
        _input = input;
        _currentIdx = 0;
      }

      readonly string _input;
      int _currentIdx;

      public char CurrentChar => _input[_currentIdx];

      public bool Eol => _currentIdx >= _input.Length;

      public void Advance() => ++_currentIdx;
    }
  }
}