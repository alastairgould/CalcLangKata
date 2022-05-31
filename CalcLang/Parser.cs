﻿namespace CalcLang;

using CalcLang.AbstractSyntaxTree;
using CalcLang.Lexer;

public class Parser
{
    private readonly Lexer.Lexer _lexer;

    public Parser(string code)
    {
        _lexer = new Lexer.Lexer(code);
    }
    
    public Program Parse()
    {
        //If you want to look into this style of parsing, look into recursive descent on google
        
        var program = new Program();

        // Stop parsing when end of file
        while (!_lexer.IsNextToken(TokenType.EndOfFile))
        {
            if (_lexer.IsNextToken(TokenType.Variable))
            {
                //Must be an assignment statement of some kind, either a constant or some kind of expression. 
                //So handle parsing print
                var assignmentStatement = Assignment();
                program.AddStatement(assignmentStatement);
            }

            if (_lexer.IsNextToken(TokenType.Print))
            {
                // Must be a statement to print. So handle parsing for a print statement
                var printStatement = Print();
                program.AddStatement(printStatement);
            }
        }

        return program;
    }
    
    private Node Assignment()
    {
        //Parse the variable to assign to
        var variableToken = Accept(TokenType.Variable);
        var variableToAssignTo = new Variable(variableToken.Value);
        
        //Carry on past the equals sign
        Accept(TokenType.Equals);

        if (_lexer.IsNextToken(TokenType.Integer))
        {
            //Assignment the value from a constant
            var constantToAssign = Constant();
            return new Assignment(variableToAssignTo, constantToAssign);
        }
        
        if (_lexer.IsNextToken(TokenType.Add))
        {
            //Assignment the value from addition
            var addExpression = Add();
            return new Assignment(variableToAssignTo, addExpression);
        }
        
        if (_lexer.IsNextToken(TokenType.Subtract))
        {
            //Assignment the value from subtraction
            var subtractExpression = Subtract();
            return new Assignment(variableToAssignTo, subtractExpression);
        }

        throw new InvalidOperationException("Expected either add keyword or constant");
    }
    
    private Node Add()
    {
        Accept(TokenType.Add);
        
        //Grab the First parameter
        var firstParameter = ConstantOrVariable();
        
        //Get the second parameter
        var secondParameter = ConstantOrVariable();

        return new Add(firstParameter, secondParameter);
    }

    private Node Subtract()
    {
        Accept(TokenType.Subtract);
        
        //Grab the First parameter
        var firstParameter = ConstantOrVariable();
        
        //Get the second parameter
        var secondParameter = ConstantOrVariable();

        return new Subtract(firstParameter, secondParameter);
    }
    
    private Constant Constant()
    {
        var token = Accept(TokenType.Integer);
        return new Constant(int.Parse(token.Value));
    }
    
    private Variable Variable()
    {
        var token = Accept(TokenType.Variable);
        return new Variable(token.Value);
    }
    
    private Print Print()
    {
        Accept(TokenType.Print);
        var variable = Variable();
        return new Print(variable);
    }

    private Node ConstantOrVariable()
    {
        if (_lexer.IsNextToken(TokenType.Variable))
        {
            return Variable();
        }
        
        if (_lexer.IsNextToken(TokenType.Integer))
        {
            return Constant();
        }

        throw new InvalidOperationException("Expected either a constant or variable");
    }

    private Token Accept(TokenType tokenType)
    {
        var token = _lexer.NextToken();
        
        if (token.TokenType != tokenType)
        {
            throw new InvalidOperationException("Unexpected symbol " + token.TokenType);
        }

        return token;
    }
}