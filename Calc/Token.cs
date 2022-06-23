namespace Calc
{
    readonly public struct Token
    {
        public Token(TokenType type, double value = 0, string name = null)
        {
            Type = type;
            Value = value;
            Name = name;
        }

        public TokenType Type {get;}
        public double Value { get; }
        public string Name { get; }
    }
}
