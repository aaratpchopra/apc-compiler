using APCCompiler.CodeAnalysis;
using APCCompiler.CodeAnalysis.Binding;
using APCCompiler.CodeAnalysis.Syntax;

namespace APCCompiler
{
    internal static class Program
    {
        private static void Main()
        {
            bool showTree = false;
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                var syntaxTree = SyntaxTree.Parse(line);
                var compilation = new Compilation(syntaxTree);
                var result = compilation.Evaluate();
                var diagnostics = result.Diagnostics;

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing Parse Tree." : "Not Showing Parse Tree.");
                    continue;
                }

                if (line == "#clean")
                {
                    Console.Clear();
                    continue;
                }

                if (showTree)
                {

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var error in diagnostics)
                        Console.WriteLine(error);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(result.Value);
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            /* 
                +
               / \
              1   *
                 / \
                2   3

            ├── +
            │   └── 1
            ├── *
            │       |-- 2

             */

            indent += isLast ? "    " : "│   ";
            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
            {
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }
}
