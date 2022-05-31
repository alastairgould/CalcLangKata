namespace CalcLang.AbstractSyntaxTree;

public class Subtract : Node
{
    public Node FirstParameter { get; } 
    
    public Node SecondParameter { get; } 
    
    public Subtract(Node firstParameter, Node secondParameter)
    {
        this.FirstParameter = firstParameter;
        this.SecondParameter = secondParameter;
    }
}