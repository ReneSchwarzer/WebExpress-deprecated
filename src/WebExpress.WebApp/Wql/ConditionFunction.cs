namespace WebExpress.WebApp.Wql
{
    public class ConditionFunction : ICondition
    {
        public Attribute Attribute { get; set; }
        public Operator? Operator { get; set; }
        public Function Function { get; set; }
    }
}
