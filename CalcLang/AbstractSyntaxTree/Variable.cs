namespace CalcLang.AbstractSyntaxTree;

public class Variable : Node
{
    public string VariableName { get; }

    public Variable(string variableName)
    {
        this.VariableName = variableName;
    }
}