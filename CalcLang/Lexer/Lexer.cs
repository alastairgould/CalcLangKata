namespace CalcLang.Lexer;

public class Lexer
{
    private string remainingText;
    private List<TokenDefinition> _tokenDefinitions;
    private Token nextToken;
    
    public Lexer(string text)
    {
        remainingText = text;
        _tokenDefinitions = new List<TokenDefinition>();
        
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Add, "^ADD"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Print, "^PRINT"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Subtract, "^SUBTRACT"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Multiply, "^MULTIPLY"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Integer, @"^[0-9]+"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Equals, "^="));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Variable, "^[A-Z]"));

        nextToken = ParseNextToken();
    }

    public Token NextToken()
    {
        var tokenToReturn = nextToken;
        nextToken = ParseNextToken();
        return tokenToReturn;
    }
    
    public bool IsNextToken(TokenType tokenType)
    {
        return nextToken.TokenType == tokenType;
    }

    private Token ParseNextToken()
    {
        while (!string.IsNullOrWhiteSpace(remainingText))
        {
            var match = FindMatch(remainingText);
            
            if (match.IsMatch)
            {
                var token = new Token(match.TokenType, match.Value);
                remainingText = match.RemainingText;
                return token;
            }
            
            remainingText = remainingText.Substring(1);
        }

        return new Token(TokenType.EndOfFile, string.Empty);
    }
    
    private TokenDefinition.TokenMatch FindMatch(string lqlText)
    {
        foreach (var tokenDefinition in _tokenDefinitions)
        {
            var match = tokenDefinition.Match(lqlText);
            if (match.IsMatch)
                return match;
        }

        return new TokenDefinition.TokenMatch() {  IsMatch = false };
    }
}