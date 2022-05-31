namespace CalcLang.AbstractSyntaxTree;

public class Multiply : Node
{
    public Node FirstParameter { get; } 
    
    public Node SecondParameter { get; } 
    
    public Multiply(Node firstParameter, Node secondParameter)
    {
        this.FirstParameter = firstParameter;
        this.SecondParameter = secondParameter;
    }
}