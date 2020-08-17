using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionCreator : MonoBehaviour
{
    public int OperatorNumber = 3;  // 運算元的數量

    private List<string> expression;

    public string GetExpression()
    {
        expression = CreateExpression();
        string s = "";
        foreach(string str in expression)
        {
            s += str;
        }

        return s + "=" + GetAnswer(expression);
    }

    public int GetAnswer(List<string> exp)
    {
        Stack<int> numStack = new Stack<int>();
        Stack<char> opStack = new Stack<char>();

        for(int i = 0; i < exp.Count; i++)
        {
            if (IsNumber(exp[i]))
            {
                numStack.Push(int.Parse(exp[i]));
            }
            else
            {
                if(opStack.Count > 0)
                {
                    // 如果OpStack最上層有東西而且它的優先度大於等於目前的優先度
                    if(GetPriority(opStack.Peek()) >= GetPriority(exp[i][0]) && opStack.Peek() == '*')
                    {
                        int number2 = numStack.Pop();
                        int number1 = numStack.Pop();
                        char op = opStack.Pop();
                        int value = GetValue(number1, number2, op);
                        numStack.Push(value); 
                    }
                }
                opStack.Push(exp[i][0]);
            }
        }

        if(opStack.Peek() == '*')
        {
            int number2 = numStack.Pop();
            int number1 = numStack.Pop();
            char op = opStack.Pop();
            int value = GetValue(number1, number2, op);
            numStack.Push(value);
        }

        Stack<int> reNumStack = new Stack<int>();
        Stack<char> reOpStack = new Stack<char>();

        while (numStack.Count > 0)
            reNumStack.Push(numStack.Pop());
        while (opStack.Count > 0)
            reOpStack.Push(opStack.Pop());

        while (reOpStack.Count > 0)
        {
            int number1 = reNumStack.Pop();
            int number2 = reNumStack.Pop();
            char op = reOpStack.Pop();
            int value = GetValue(number1, number2, op);
            reNumStack.Push(value);
        }

        return reNumStack.Pop();
    }

    public int GetPriority(char ch)
    {
        if (ch == '+' || ch == '-')
            return 0;
        return 1;
    }

    public List<string> CreateExpression()
    {
        List<string> expression = new List<string>();

        for(int i = 0; i < OperatorNumber * 2 + 1; i++)
        {
            if (i % 2 == 0)
                expression.Add(GetNumber());
            else
                expression.Add(Translation(GetOperator(true)));
        }

        return expression;
    }

    private int GetValue(int n1, int n2, char op)
    {
        switch (op)
        {
            case '+':
                return n1 + n2;
            case '-':
                return n1 - n2;
            case '*':
                return n1 * n2;
            default:
                return 0;
        }
    }

    private Operator GetOperator(bool hasMultDivide)
    {
        return (Operator)Random.Range(0, hasMultDivide? 3 : 2);
    }

    private bool IsNumber(string str)
    {
        return str[0] >= '0' && str[0] <= '9';
    }

    private string GetNumber()
    {
        return Random.Range(1, 10).ToString();
    }

    private string Translation(Operator op)
    {
        switch (op)
        {
            case Operator.Add:
                return "+";
            case Operator.Sub:
                return "-";
            case Operator.Mult:
                return "*";
            default:
                return "+";
        }
    }
}

public enum Operator
{
    Add,        
    Sub,        
    Mult
}
