namespace APCCompiler.CodeAnalysis
{
    enum SyntaxKind
    {
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
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression
    }
}
