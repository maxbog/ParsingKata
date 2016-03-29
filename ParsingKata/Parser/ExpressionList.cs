﻿using System.Collections.Generic;
using ParsingKata.Ast;

namespace ParsingKata.Parser
{
  public class ExpressionList
  {

    public ExpressionList(IExpression firstExpression, IEnumerable<Operation> nextOperations)
    {
      FirstExpression = firstExpression;
      NextOperations = nextOperations;
    }

    public IExpression FirstExpression { get; private set; }
    public IEnumerable<Operation> NextOperations { get; private set; }
  }

  public class Operation
  {
    public IExpression Expression { get; }
    public Operator Oper { get; }

    public Operation(Operator oper, IExpression ex)
    {
      Oper = oper;
      Expression = ex;
    }

    protected bool Equals(Operation other)
    {
      return Equals(Expression, other.Expression) && Oper == other.Oper;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Operation) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((Expression?.GetHashCode() ?? 0)*397) ^ (int) Oper;
      }
    }

    public static bool operator ==(Operation left, Operation right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Operation left, Operation right)
    {
      return !Equals(left, right);
    }

    public IExpression CreateExpression(IExpression left, IBinaryExpressionFactory expressionFactory)
    {
      return expressionFactory.CreateBinaryExpression(Oper, left, Expression);
    }
  }
}