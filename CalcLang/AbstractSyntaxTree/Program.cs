namespace CalcLang.AbstractSyntaxTree;

public class Program : Node
{
    public List<Node> Statements { get; }

    public Program(List<Node> statements)
    {
        Statements = statements;
    }
}