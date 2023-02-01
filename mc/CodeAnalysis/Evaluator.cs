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
                var operand = EvaluateExpression(u.Operand);

                return u.Op.Kind switch
                {
                    BoundUnaryOperatorKind.Identity => (int) operand,
                    BoundUnaryOperatorKind.Negation => -(int) operand,
                    BoundUnaryOperatorKind.LogicalNegation => !(bool) operand,
                    _ => throw new Exception($"Unexpected Unary Operator: {u.Op.Kind}")
                };
            }

            if (root is BoundBinaryExpression b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                return b.Op.Kind switch
                {
                    BoundBinaryOperatorKind.Addition => (int) left + (int) right,
                    BoundBinaryOperatorKind.Subtraction => (int) left - (int) right,
                    BoundBinaryOperatorKind.Multiplication => (int) left * (int) right,
                    BoundBinaryOperatorKind.Division => (int) left / (int) right,
                    BoundBinaryOperatorKind.LogicalAND => (bool) left && (bool) right,
                    BoundBinaryOperatorKind.LogicalOR => (bool) left || (bool) right,
                    BoundBinaryOperatorKind.LogicalEqual => Equals(left, right),
                    BoundBinaryOperatorKind.LogicalNotEqual=> !Equals(left, right),
                    _ => throw new Exception($"Unexpected binary operator: {b.Op.Kind}")
                };
            }

            throw new Exception($"Unexpected node: {root.Kind}");
        }
    }
}
