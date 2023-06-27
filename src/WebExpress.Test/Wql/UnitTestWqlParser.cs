using System.Linq;
using WebExpress.WebApp.Wql;
using Xunit;

namespace WebExpress.Test.Wql
{
    public class UnitTestWqlParser : IClassFixture<UnitTestWqlWqlFixture>
    {
        public UnitTestWqlParser(UnitTestWqlWqlFixture fixture)
        {

        }

        [Fact]
        public void TestParseEmpty()
        {
            var wql = WqlParser.Parse("");
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterEquals()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();
            var wql = WqlParser.Parse("firstname='Olivia'");
            var res = wql?.Apply(testData.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal(1, res.Count());
            Assert.Equal("FirstName = 'Olivia'", wql.ToString());
            Assert.Equal("Olivia", item.FirstName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterLike()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();
            var wql = WqlParser.Parse("firstname ~ 'Oliv'");
            var res = wql?.Apply(testData.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal(1, res.Count());
            Assert.Equal("FirstName ~ 'Oliv'", wql.ToString());
            Assert.Equal("Olivia", item.FirstName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterSet()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("firstname in ('FirstName33', 'FirstName55')");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.Skip(1).FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal(2, res.Count());
            Assert.Equal("FirstName in ('FirstName33', 'FirstName55')", wql.ToString());
            Assert.Equal("FirstName33", item1.FirstName);
            Assert.Equal("FirstName55", item2.FirstName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterLowerCase()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("firstname  =  'olivia'");
            var res = wql?.Apply(testData.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal(1, res.Count());
            Assert.Equal("FirstName = 'olivia'", wql.ToString());
            Assert.Equal("Olivia", item.FirstName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterAnd()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("FirstName = 'FirstName23' and lastname = \"LastName23\"");
            var res = wql?.Apply(testData.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal(1, res.Count());
            Assert.Equal("(FirstName = 'FirstName23' and LastName = 'LastName23')", wql.ToString());
            Assert.Equal("FirstName23", item.FirstName);
            Assert.Equal("LastName23", item.LastName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterOr()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("FirstName = 'FirstName55' or LastName = 'LastName33'");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.Skip(1).FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal(2, res.Count());
            Assert.Equal("(FirstName = 'FirstName55' or LastName = 'LastName33')", wql.ToString());
            Assert.Equal("FirstName55", item1.FirstName);
            Assert.Equal("LastName33", item2.LastName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseOrderByFirstName()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("orderby firstname");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("order by FirstName asc", wql.ToString());
            Assert.Equal("Ava", item1.FirstName);
            Assert.Equal("Xantia", item2.FirstName);
            Assert.Null(wql.Filter);
            Assert.NotNull(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseOrderByFirstNameAsc()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("order by firstname asc");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("order by FirstName asc", wql.ToString());
            Assert.Equal("Ava", item1.FirstName);
            Assert.Equal("Xantia", item2.FirstName);
            Assert.Null(wql.Filter);
            Assert.NotNull(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseOrderByFirstNameDesc()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("order by firstname desc");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("order by FirstName desc", wql.ToString());
            Assert.Equal("Xantia", item1.FirstName);
            Assert.Equal("Ava", item2.FirstName);
            Assert.Null(wql.Filter);
            Assert.NotNull(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseOrderByFirstNameAndLastName()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("order by firstname, lastname");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("order by FirstName asc, LastName asc", wql.ToString());
            Assert.Equal("Ava", item1.FirstName);
            Assert.Equal("Xantia", item2.FirstName);
            Assert.Null(wql.Filter);
            Assert.NotNull(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParsePartitioningTake()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("take 10");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("take 10", wql.ToString());
            Assert.Equal("Emma", item1.FirstName);
            Assert.Equal("Noah", item2.FirstName);
            Assert.Equal(10, res.Count());
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }

        [Fact]
        public void TestParsePartitioningSkip()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("skip 10");
            var res = wql?.Apply(testData.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal("skip 10", wql.ToString());
            Assert.Equal("Isabella", item.FirstName);
            Assert.Equal(testData.Count() - 10, res.Count());
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }

        [Fact]
        public void TestParsePartitioningTakeSkip()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("take 10 skip 5");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("take 10 skip 5", wql.ToString());
            Assert.Equal("Sophia", item1.FirstName);
            Assert.Equal("Noah", item2.FirstName);
            Assert.Equal(5, res.Count());
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }

        [Fact]
        public void TestParsePartitioningSkipTake()
        {
            var testData = UnitTestWqlTestData.GenerateTestData();

            var wql = WqlParser.Parse("skip 5 take 10");
            var res = wql?.Apply(testData.AsQueryable());
            var item1 = res?.FirstOrDefault();
            var item2 = res?.LastOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal("skip 5 take 10", wql.ToString());
            Assert.Equal("Sophia", item1.FirstName);
            Assert.Equal("FirstName3", item2.FirstName);
            Assert.Equal(10, res.Count());
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }

        [Fact]
        public void TestParseComplex()
        {
            var wql = WqlParser.Parse("x = 1 and y > 2 or z in ('a', 'b', 'c') order by x desc, y take 10 skip 20");

            Assert.Equal("(x = 1 and (y > 2 or z in ('a', 'b', 'c'))) order by x desc, y asc take 10 skip 20", wql.ToString());
            Assert.NotNull(wql.Filter);
            Assert.NotNull(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }
    }
}
