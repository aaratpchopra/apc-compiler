using APCCompiler.CodeAnalysis.Syntax;

namespace APCCompiler.CodeAnalysis
{
    public sealed class Evaluator
    {
        private readonly ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax root)
        {
            if (root is LiteralExpressionSyntax n)
                return (int)n.LiteralToken.Value;

            if (root is UnaryExpressionSyntax u)
            {
                var operand = EvaluateExpression(u.Operand);

                return u.OperatorToken.Kind switch
                {
                    SyntaxKind.PlusToken => operand,
                    SyntaxKind.MinusToken => -operand,
                    _ => throw new Exception($"Unexpected Unary Operator: {u.OperatorToken.Kind}")
                };
            }

            if (root is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                return b.OperatorToken.Kind switch
                {
                    SyntaxKind.PlusToken => left + right,
                    SyntaxKind.MinusToken => left - right,
                    SyntaxKind.StarToken => left * right,
                    SyntaxKind.SlashToken => left / right,
                    _ => throw new Exception($"Unexpected binary operator: {b.OperatorToken.Kind}")
                };
            }

            if (root is ParenthesizedExpressionSyntax p)
                return EvaluateExpression(p.Expression);

            throw new Exception($"Unexpected node: {root.Kind}");
        }
    }
}
