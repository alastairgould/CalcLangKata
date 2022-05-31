namespace CalcLang.AbstractSyntaxTree;

public class Add : Node
{
    public Node FirstParameter { get; } 
    
    public Node SecondParameter { get; } 
    
    public Add(Node firstParameter, Node secondParameter)
    {
        this.FirstParameter = firstParameter;
        this.SecondParameter = secondParameter;
    }
}