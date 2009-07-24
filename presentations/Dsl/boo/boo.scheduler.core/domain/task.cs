using System;
using System.Collections.Generic;
using System.Threading;
using log4net;

namespace boo.scheduler.core.domain
{
    public class Task
    {
        private TimeSpan _frequency;
        private string _emailTemplate;
        private DateTime _nextPollTime;
        private static readonly ILog _log = LogManager.GetLogger(typeof(Task));
        protected List<string> _clients = new List<string>();

        public List<string> Clients
        {
            get
            {
                return _clients;
            }
        }

        public string EmailTemplate
        {
            get { return _emailTemplate; }
            set { _emailTemplate = value; }
        }

        public bool Enabled { get; set; }

        public virtual TimeSpan Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;

                SetNextPollTime();
            }
        }

        public virtual string Name { get; set; }

        public virtual DateTime NextPollTime
        {
            get
            {
                return _nextPollTime;
            }
        }

        public virtual string Query { get; set; }

        public bool ServiceStarted { get; set; }

        public virtual bool ShouldContinueAfterException
        {
            get { return true; }
        }

        public DateTime StartingTime { get; set; }

        public string Subject { get; set; }

        public List<Parameter> Parameters { get; set; }

        public virtual void Execute()
        {
            Console.WriteLine("Main execution block.");
        }

        public virtual void Run()
        {
        }

        protected virtual void SuspendTask()
        {
            Thread.Sleep(Frequency);
        }

        private void SetNextPollTime()
        {
            _nextPollTime = DateTime.Now.AddDays(_frequency.Days);
        }

        public Task(string name, TimeSpan frequency)
        {
            Name = name;

            EmailTemplate = string.Format("{0}.txt", name.Replace(" ", ""));

            Frequency = frequency;

            Parameters = new List<Parameter>();
        }
    }
}
