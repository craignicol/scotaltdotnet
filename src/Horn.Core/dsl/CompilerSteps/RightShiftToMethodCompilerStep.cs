using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.Steps;

namespace Horn.Core.Dsl
{
    public class RightShiftToMethodCompilerStep : AbstractTransformerCompilerStep
    {

        public override void OnBlockExpression(BlockExpression node)
        {
            var dependencies = new ArrayLiteralExpression();

            foreach (Statement statement in node.Body.Statements)
            {
                if(statement is Boo.Lang.Compiler.Ast.Block)
                    continue;

                MethodInvocationExpression expression = (MethodInvocationExpression)((ExpressionStatement) statement).Expression;

                foreach(Expression arg in expression.Arguments)
                {
                    if ((!(arg is BinaryExpression)) || ((BinaryExpression)arg).Operator != BinaryOperatorType.ShiftRight) 
                        continue;

                    var binaryExpression = (BinaryExpression) arg;

                    //HACK: Need a better Expression type for pass a list of strings into a method
                    dependencies.Items.Add(
                            new StringLiteralExpression(string.Format("{0}|{1}", 
                                                            binaryExpression.Left.ToString().Trim('\''), 
                                                            binaryExpression.Right.ToString().Trim('\'')))
                         );
                }
            }

            if (dependencies.Items.Count == 0)
                return;

            var replacementMethod = new MethodInvocationExpression(new ReferenceExpression("AddDependencies"),
                                                dependencies
                                        );

            ReplaceCurrentNode(replacementMethod);
        }

        public override void Run()
        {
            Visit(CompileUnit);
        }



    }
}