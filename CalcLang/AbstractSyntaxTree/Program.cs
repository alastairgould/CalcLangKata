namespace CalcLang.AbstractSyntaxTree;

public class Program : Node
{
    public List<Node> Statements { get; }

    public Program()
    {
        Statements = new List<Node>();
    }
    
    public void AddStatement(Node node)
    {
        this.Statements.Add(node);
    }
}