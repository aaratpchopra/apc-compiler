using APCCompiler.CodeAnalysis.Binding;
using APCCompiler.CodeAnalysis.Syntax;
using System.Collections;

namespace APCCompiler.CodeAnalysis
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics);
        }

        private void Report(TextSpan span, string message)
        {
            var dignostics = new Diagnostic(span, message);
            _diagnostics.Add(dignostics);
        }

        public void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            var message = $"The number {text} is not a valid type: {type}.";
            Report(span, message);
        }

        public void ReportBadCharacter(int position, char character)
        {
            var message = $"Bad character input: '{character}'.";
            var span = new TextSpan(position, 1);
            Report(span, message);                                                                                      
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind providedKind)
        {
            var message = $"Unexpected token: {actualKind}, expected token: {providedKind}."; // actualKind | providedKind
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string op, Type leftType, Type rightType)
        {
            var message = $"Binary operator '{op}' is not defined for types {leftType} and {rightType}";
            Report(span, message);
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string op, Type type)
        {
            var message = $"Unary operator '{op}' is not defined for type {type}";
            Report(span, message);
        }
    }
}
