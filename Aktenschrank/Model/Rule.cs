namespace Aktenschrank.Model
{
    public class Rule
    {
        private string _title;

        private string _description;


        private Rule _subRules;


        private RuleType _ruleType;

        private Dictionary<string, string> _ruleData;


        public Rule(string title, RuleType ruleType)
        {
            _title = title;
            _ruleType = ruleType;

            _ruleData = new Dictionary<string, string>();
        }

        public string Title { get { return _title; } }
        public string Description { get { return _description; } }

        public Rule SubRules { get { return _subRules; } }

        public RuleType RuleType { get { return _ruleType; } }
        public Dictionary<string, string> RuleData { get { return _ruleData; } }

    }
}