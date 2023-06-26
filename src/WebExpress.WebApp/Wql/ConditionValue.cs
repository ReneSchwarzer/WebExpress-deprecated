namespace WebExpress.WebApp.Wql
{
    public class ConditionValue : ICondition
    {
        public Attribute Attribute { get; set; }
        public Operator? Operator { get; set; }
        public Value Value { get; set; }
    }
}
