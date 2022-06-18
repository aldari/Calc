namespace Calc
{
    readonly public struct Token
    {
        public Token(TokenType type, double value = 0)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type {get;}
        public double Value { get; }
    }
}
