namespace Horn.Core.Utils.CmdLine
{
    public class Parameter
    {
        public string Key { get; private set; }

        public bool Required { get; private set; }

        public string[] Values { get; private set; }

        public bool Reoccurs { get; private set; }

        public Parameter(string key, bool required, string[] values, bool reoccurs)
        {
            Key = key;
            Required = required;
            Values = values;
            Reoccurs = reoccurs;
        }
    }
}