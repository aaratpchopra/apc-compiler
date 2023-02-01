namespace APCCompiler.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        EndOfFileToken,
        BadToken,
        IdentifierToken,
        LogicalANDToken,
        LogicalORToken,
        BangToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,

        // Expressions
        LiteralExpression,
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression,
    }
}
