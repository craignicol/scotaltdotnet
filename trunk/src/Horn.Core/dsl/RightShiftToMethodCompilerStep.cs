using System;
using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.Steps;

namespace Horn.Core.dsl
{
    public class RightShiftToMethodCompilerStep : AbstractTransformerCompilerStep
    {
        public override void OnBlockExpression(BlockExpression node)
        {
            foreach (Statement statement in node.Body.Statements)
            {
                MethodInvocationExpression expression = (MethodInvocationExpression)((ExpressionStatement) statement).Expression;

                foreach(Expression arg in expression.Arguments)
                {
                    if ((!(arg is BinaryExpression)) || ((BinaryExpression)arg).Operator != BinaryOperatorType.ShiftRight) 
                        continue;

                    var binaryExpression = (BinaryExpression) arg;

                    var replacementMethod = new MethodInvocationExpression(new ReferenceExpression("AddDependency"), 
                                                        binaryExpression.Left, 
                                                        binaryExpression.Right
                                                );

                    ReplaceCurrentNode(replacementMethod);

                    return;
                }
            }

            base.OnBlockExpression(node);
        }

        public override void Run()
        {
            Visit(CompileUnit);
        }
    }
}