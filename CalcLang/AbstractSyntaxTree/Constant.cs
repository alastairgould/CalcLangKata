namespace CalcLang.AbstractSyntaxTree;

public class Constant : Node
{
    public int Value { get; }

    public Constant(int value)
    {
        Value = value;
    }
}