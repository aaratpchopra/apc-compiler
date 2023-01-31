namespace APCCompiler.CodeAnalysis.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openedParenthesisToken, ExpressionSyntax expression, SyntaxToken closedParenthesisToken)
        {
            OpenedParenthesisToken = openedParenthesisToken;
            Expression = expression;
            ClosedParenthesisToken = closedParenthesisToken;
        }

        public SyntaxToken OpenedParenthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken ClosedParenthesisToken { get; }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenedParenthesisToken;
            yield return Expression;
            yield return ClosedParenthesisToken;
        }
    }
}
