namespace WebExpress.WebApp.Wql
{
    public class FilterAnd : IFilter
    {
        public IFilter LeftCondition { get; set; }
        public LogicalOperator? LogicalOperator { get; set; }
        public IFilter RightCondition { get; set; }
    }
}
