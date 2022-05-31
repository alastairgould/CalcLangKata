namespace CalcLang;

using CalcLang.AbstractSyntaxTree;

public class Interpreter
{
    private readonly Program _program;
    private readonly Dictionary<string, int> _variables;
    private string _terminalOutput = "";

    public Interpreter(Program program)
    {
        _program = program;
        _variables = new Dictionary<string, int>();
    }

    public string Run()
    {
        foreach (var statement in _program.Statements)
        {
            HandleStatement(statement); 
        }

        return _terminalOutput.Trim();
    }

    private void HandleStatement(Node statement)
    {
        switch (statement)
        {
            case Assignment assignment:
                HandleAssignmentStatement(assignment);
                return;
            case Print print:
                HandlePrintStatement(print);
                return;
        }

        throw new InvalidOperationException("Tried to execute unsupported statement");
    }

    private void HandleAssignmentStatement(Assignment statement)
    {
        var valueExpression = statement.Value;
        var value = EvaluateExpression(valueExpression);
        _variables[statement.Variable.VariableName] = value;
    }
    
    private void HandlePrintStatement(Print statement)
    {
        var variableName = statement.Variable.VariableName;
        _terminalOutput = _terminalOutput + variableName + " = " + _variables[variableName] + Environment.NewLine;
    }

    private int EvaluateExpression(Node node)
    {
        return node switch
        {
            Constant constantExpression => GetValueFromConstant(constantExpression),
            Add addExpression => EvaluateAddExpression(addExpression),
            Subtract subtractExpression => EvaluateSubtractExpression(subtractExpression),
            _ => throw new InvalidOperationException("Tried to execute unsupported expression")
        };
    }
    
    private int EvaluateAddExpression(Add add)
    {
        var firstValue = GetValueFromConstOrVariable(add.FirstParameter);
        var secondValue = GetValueFromConstOrVariable(add.SecondParameter);
        return firstValue + 1;
    }
    
    private int EvaluateSubtractExpression(Subtract subtract)
    {
        throw new NotImplementedException();
    }

    private int GetValueFromConstOrVariable(Node node)
    {
        return node switch
        {
            Constant constant => GetValueFromConstant(constant),
            Variable variable => GetValueFromVariable(variable),
            _ => throw new InvalidOperationException(
                "Tried to extract value something that isn't a constant or variable")
        };
    }
    
    private int GetValueFromConstant(Constant constant)
    {
        return constant.Value;
    }
    
    private int GetValueFromVariable(Variable constant)
    {
        return _variables[constant.VariableName];
    }
}