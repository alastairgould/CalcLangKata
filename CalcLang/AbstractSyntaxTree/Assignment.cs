namespace CalcLang.AbstractSyntaxTree;

public class Assignment : Node
{
    public Variable Variable { get; }
    public Node Value { get; }

    public Assignment(Variable variable, Node value)
    {
        Variable = variable;
        Value = value;
    }
}