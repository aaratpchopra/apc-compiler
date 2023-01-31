using APCCompiler.CodeAnalysis.Syntax;
using APCCompiler.CodeAnalysis.Binding;

namespace APCCompiler.CodeAnalysis
{
    internal sealed class Evaluator
    {
        private readonly BoundExpression _root;
        public Evaluator(BoundExpression root)
        {
            _root = root;
        }

        public object Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private object EvaluateExpression(BoundExpression root)
        {
            if (root is BoundLiteralExpression n)
                return n.Value;

            if (root is BoundUnaryExpression u)
            {
                var operand = (int) EvaluateExpression(u.Operand);

                return u.OperatorKind switch
                {
                    BoundUnaryOperatorKind.Identity => operand,
                    BoundUnaryOperatorKind.Negation => -operand,
                    _ => throw new Exception($"Unexpected Unary Operator: {u.OperatorKind}")
                };
            }

            if (root is BoundBinaryExpression b)
            {
                var left = (int) EvaluateExpression(b.Left);
                var right = (int) EvaluateExpression(b.Right);

                return b.OperatorKind switch
                {
                    BoundBinaryOperatorKind.Addition => left + right,
                    BoundBinaryOperatorKind.Subtraction => left - right,
                    BoundBinaryOperatorKind.Multiplication => left * right,
                    BoundBinaryOperatorKind.Division => left / right,
                    _ => throw new Exception($"Unexpected binary operator: {b.OperatorKind}")
                };
            }

            throw new Exception($"Unexpected node: {root.Kind}");
        }
    }
}
