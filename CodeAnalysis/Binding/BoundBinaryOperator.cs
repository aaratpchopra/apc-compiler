using APCCompiler.CodeAnalysis.Syntax;

namespace APCCompiler.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryOperator
    {
        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
            : this(syntaxKind, kind, type, type, type)
        {
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandType, Type type)
            : this(syntaxKind, kind, operandType, operandType, type)
        {
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type type)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            LeftType = leftType;
            RightType = rightType;
            Type = type;
        }

        public SyntaxKind SyntaxKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type LeftType { get; }
        public Type RightType { get; }
        public Type Type { get; }

        private static BoundBinaryOperator[] _operators =
        {
            new BoundBinaryOperator(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.LogicalANDToken, BoundBinaryOperatorKind.LogicalAND, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.LogicalORToken, BoundBinaryOperatorKind.LogicalOR, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.EqualEqualToken, BoundBinaryOperatorKind.LogicalEqual, typeof(int), typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.NotEqualToken, BoundBinaryOperatorKind.LogicalNotEqual, typeof(int), typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.EqualEqualToken, BoundBinaryOperatorKind.LogicalEqual, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.NotEqualToken, BoundBinaryOperatorKind.LogicalNotEqual, typeof(bool)),
        };

        public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, Type leftType, Type rightType)
        {
            foreach (var op in _operators)
            {
                if (op.SyntaxKind == syntaxKind && op.LeftType == leftType && op.RightType == rightType)
                    return op;
            }
            return null;
        }
    }
}
