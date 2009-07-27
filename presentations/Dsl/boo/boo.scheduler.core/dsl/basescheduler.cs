using System;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using boo.scheduler.core.domain;

namespace boo.scheduler.core.dsl
{
    public abstract class BaseScheduler
    {
        public virtual DateTime now
        {
            get
            {
                Task.StartingTime = DateTime.Now;

                return Task.StartingTime;
            }
            set { Task.StartingTime = DateTime.Now; }
        }

        public virtual DateTime StartingTime
        {
            get { return Task.StartingTime; }
            set { Task.StartingTime = value; }
        }

        public virtual TimeSpan Repetition
        {
            get { return Task.Frequency; }
            set { Task.Frequency = value; }
        }

        public Task Task { get; private set; }

        public abstract void Prepare();

        [Meta]
        public static Expression clients(BlockExpression block)
        {
            if (block.Body.Statements.Count > 1)
                throw new ArgumentOutOfRangeException("Only one include statement can be added to clients block");

            var expression =
                (MethodInvocationExpression)((ExpressionStatement)block.Body.Statements[0]).Expression;

            var clientList = new ArrayLiteralExpression();

            foreach (Expression argument in expression.Arguments)
            {
                var client = new MethodInvocationExpression(new ReferenceExpression("Client"),
                                                                                       argument);

                clientList.Items.Add(client);
            }

            return new MethodInvocationExpression(new ReferenceExpression("ParseClients"), clientList);
        }

        [Extension]
        public static TimeSpan Days(int number)
        {
            return TimeSpan.FromDays(number);
        }

        [Meta]
        public static Expression parameters(BlockExpression block)
        {
            if (block.Body.Statements.Count > 1)
                throw new ArgumentOutOfRangeException("Only one list statement can be added to parameters block");

            var expression =
                (MethodInvocationExpression)((ExpressionStatement)block.Body.Statements[0]).Expression;

            var parameterList = new ArrayLiteralExpression();

            foreach (Expression argument in expression.Arguments)
            {
                var parameter = new MethodInvocationExpression(new ReferenceExpression("Parameter"),
                                                                                       argument);

                parameterList.Items.Add(parameter);
            }

            return new MethodInvocationExpression(new ReferenceExpression("ParseParameters"), parameterList);
        }

        [Meta]
        public static Expression task(StringLiteralExpression taskName, BlockExpression action)
        {
            return new MethodInvocationExpression(
                                    new ReferenceExpression("CreateTask"),
                                    taskName,
                                    action
                                );
        }

        public virtual void CreateTask(string taskName, Action taskDetail)
        {
            Task = new Task(taskName, TimeSpan.Zero);

            taskDetail();
        }

        public virtual void disabled()
        {
            Task.Enabled = false;
        }

        public virtual void enabled()
        {
            Task.Enabled = true;
        }

        public void every(TimeSpan timeSpan)
        {
            Task.Frequency = timeSpan;
        }

        public virtual void ParseClients(Client[] clients)
        {
            Task.Clients.AddRange(clients);
        }

        public virtual void ParseParameters(Parameter[] parameters)
        {
            Task.Parameters.AddRange(parameters);
        }

        public virtual void query(string query)
        {
            Task.Query = query;
        }

        public virtual void starting(DateTime date)
        {
            Task.StartingTime = date;
        }

        public virtual void subject(string text)
        {
            Task.Subject = text;
        }
    }
}