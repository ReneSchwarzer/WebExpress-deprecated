using WebExpress.WebApp.Wql;
using Xunit;

namespace WebExpress.Test.Wql
{
    public class UnitTestWqlParser
    {
        [Fact]
        public void TestParseEmpty()
        {
            var wql = Parser.Parse("");
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseFilter()
        {
            var wql = Parser.Parse("x = 1");
            Assert.NotNull(wql.Filter);
            Assert.NotNull(wql.Filter as Filter);
            Assert.Equal("x", ((wql.Filter as Filter).Condition as ConditionValue).Attribute.Name);
            Assert.Equal(Operator.Equal, ((wql.Filter as Filter).Condition as ConditionValue).Operator.Value);
            Assert.Equal(1, ((wql.Filter as Filter).Condition as ConditionValue).Value.NumberValue);
        }

        [Fact]
        public void TestParseOrder()
        {
            var wql = Parser.Parse("order by x");
            Assert.NotNull(wql.Order);
            Assert.Single(wql.Order.Attributes);
            Assert.Equal("x", wql.Order.Attributes[0].Attribute.Name);
        }

        [Fact]
        public void TestParsePartitioning()
        {
            var wql = Parser.Parse("take 10 skip 20");
            Assert.NotNull(wql.Partitioning);
            Assert.Equal(10, wql.Partitioning.Take);
            Assert.Equal(20, wql.Partitioning.Skip);
        }

        [Fact]
        public void TestParseComplex()
        {
            var wql = Parser.Parse("x = 1 and y > 2 or z in ('a', 'b', 'c') order by x desc, y take 10 skip 20");

            Assert.NotNull(wql.Filter);
            //Assert.NotNull(wql.Filter.Condition);
            //Assert.NotNull(wql.Filter.Condition.LeftCondition);
            //Assert.Equal(LogicalOperator.And, wql.Filter.Condition.LogicalOperator);
            //Assert.NotNull(wql.Filter.Condition.RightCondition);

            //Assert.Equal("x", wql.Filter.Condition.LeftCondition.LeftCondition.Attribute.Name);
            //Assert.Equal(Operator.Equal, wql.Filter.Condition.LeftCondition.LeftCondition.Operator);
            //Assert.Equal(1, wql.Filter.Condition.LeftCondition.LeftCondition.RightValue.NumberValue);

            //Assert.Equal("y", wql.Filter.Condition.LeftCondition.RightCondition.Attribute.Name);
            //Assert.Equal(Operator.GreaterThan, wql.Filter.Condition.LeftCondition.RightCondition.Operator);
            //Assert.Equal(2, wql.Filter.Condition.LeftCondition.RightCondition.RightValue.NumberValue);

            //Assert.Equal(LogicalOperator.Or, wql.Filter.Condition.LogicalOperator);

            //Assert.Equal("z", wql.Filter.Condition.RightCondition.Attribute.Name);
            //Assert.Equal(Operator.In, wql.Filter.Condition.RightCondition.Operator);
            ////Assert.Equal("a", wql.Filter.Condition.RightCondition.RightValue.StringValues[0]);
            ////Assert.Equal("b", wql.Filter.Condition.RightCondition.RightValue.StringValues[1]);
            ////Assert.Equal("c", wql.Filter.Condition.RightCondition.RightValue.StringValues[2]);

            //Assert.NotNull(wql.Order);
            //Assert.Equal(2, wql.Order.Attributes.Count);
            //Assert.Equal("x", wql.Order.Attributes[0].Attribute.Name);
            //Assert.True(wql.Order.Attributes[0].Descending);
            //Assert.Equal("y", wql.Order.Attributes[1].Attribute.Name);
            //Assert.False(wql.Order.Attributes[1].Descending);

            //Assert.NotNull(wql.Partitioning);
            //Assert.Equal(10, wql.Partitioning.Take);
            //Assert.Equal(20, wql.Partitioning.Skip);
        }
    }
}
