using Xunit;
using System.IO;

namespace CalcLang.Tests;

public class CalcLangTests
{
    [Fact]
    public void AddOne()
    {
        const string program = "AddOne.calclang";
        
        var programOutput = RunProgram(program);

        Assert.Equal("X = 3", programOutput);
    }

    [Theory]
    [InlineData(2, "X = 4")]
    [InlineData(3, "X = 5")]
    [InlineData(4, "X = 6")]
    [InlineData(5, "X = 7")]
    [InlineData(6, "X = 8")]
    [InlineData(7, "X = 9")]
    public void AddN(int programInput, string output)
    {
        // Hint: Will require a change in Interpreter.cs
        const string program = "AddN.calclang";
        
        var programOutput = RunProgram(program, programInput);

        Assert.Equal(output, programOutput);
    }
    
    [Theory]
    [InlineData(1, "X = 9")]
    [InlineData(2, "X = 8")]
    [InlineData(3, "X = 7")]
    [InlineData(4, "X = 6")]
    [InlineData(5, "X = 5")]
    public void Subtract(int programInput, string output)
    {
        // Hint: Will require a change in Interpreter.cs
        const string program = "Subtract.calclang";
        
        var programOutput = RunProgram(program, programInput);

        Assert.Equal(output, programOutput);
    }
    
    [Theory]
    [InlineData(1, "X = 10")]
    [InlineData(2, "X = 20")]
    [InlineData(3, "X = 30")]
    [InlineData(4, "X = 40")]
    [InlineData(5, "X = 50")]
    public void Multiply(int programInput, string output)
    {
        // Hint: Will require a change in the parser, and interpreter. Token should already exist. abstract syntax tree node should already exist
        const string program = "Multiply.calclang";
        
        var programOutput = RunProgram(program, programInput);

        Assert.Equal(output, programOutput);
    }

    [Theory]
    [InlineData(2, "X = 50")]
    [InlineData(4, "X = 25")]
    [InlineData(50, "X = 2")]
    public void Divide(int programInput, string output)
    {
        // Hint: Will require a change in the lexer, parser, abstract syntax tree and interpreter
        const string program = "Divide.calclang";

        var programOutput = RunProgram(program, programInput);

        Assert.Equal(output, programOutput);
    }

    [Fact]
    public void DivideByZero()
    {
        // Hint: Will require a change in interpreter
        const string program = "DivideByZero.calclang";

        var programOutput = RunProgram(program);

        Assert.Equal("DIVIDE BY ZERO ERROR", programOutput);
    }
    
    [Fact]
    public void NestedExpressions()
    {
        const string program = "ProcedanceOperator.calclang";

        var programOutput = RunProgram(program);

        Assert.Equal("X = 5", programOutput);
    }
    
    [Theory]
    [InlineData(4, "X = 4")]
    [InlineData(5, "X = 15")]
    [InlineData(6, "X = 6")]
    public void Conditional(int n, string output)
    {
        //Good luck...
        const string program = "Conditional.calclang";

        var programOutput = RunProgram(program, n);

        Assert.Equal(output, programOutput);
    }
    
    private static string RunProgram(string fileName, int? replace = null)
    {
        var programFile = File.ReadAllText($"Programs/{fileName}");

        if (replace.HasValue)
        {
            programFile = programFile.Replace("{#}", replace.ToString());
        }
       
        //Parse Program
        var parser = new Parser(programFile);
        var program = parser.Parse();
       
        //Interpret program
        var interpreter = new Interpreter(program);
        var programOutput = interpreter.Run();
        
        return programOutput;
    }
}