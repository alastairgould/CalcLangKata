namespace CalcLang.Lexer;

public class Lexer
{
    private string _remainingText;
    private readonly List<TokenDefinition> _tokenDefinitions;
    private Token _nextToken;
    
    public Lexer(string text)
    {
        _remainingText = text;
        _tokenDefinitions = new List<TokenDefinition>();
        
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Add, "^ADD"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Print, "^PRINT"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Subtract, "^SUBTRACT"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Multiply, "^MULTIPLY"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Integer, @"^[0-9]+"));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Equals, "^="));
        _tokenDefinitions.Add(new TokenDefinition(TokenType.Variable, "^[A-Z]"));

        _nextToken = ParseNextToken();
    }

    public Token NextToken()
    {
        var tokenToReturn = _nextToken;
        _nextToken = ParseNextToken();
        return tokenToReturn;
    }
    
    public bool IsNextToken(TokenType tokenType)
    {
        return _nextToken.TokenType == tokenType;
    }

    private Token ParseNextToken()
    {
        while (!string.IsNullOrWhiteSpace(_remainingText))
        {
            var match = FindMatch(_remainingText);
            
            if (match.IsMatch)
            {
                var token = new Token(match.TokenType, match.Value);
                _remainingText = match.RemainingText;
                return token;
            }
            
            _remainingText = _remainingText.Substring(1);
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