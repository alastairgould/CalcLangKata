namespace CalcLang.AbstractSyntaxTree;

public class Print : Node
{
    public Variable Variable { get; }

    public Print(Variable variable)
    {
        Variable = variable;
    }
}