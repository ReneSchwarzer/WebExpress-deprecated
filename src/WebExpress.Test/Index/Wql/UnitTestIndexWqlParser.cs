using System.Linq;
using Xunit;

namespace WebExpress.Test.Index.Wql
{
    public class UnitTestIndexWqlParser : IClassFixture<UnitTestIndexWqlFixture>
    {
        protected UnitTestIndexWqlFixture Fixture { get; set; }

        public UnitTestIndexWqlParser(UnitTestIndexWqlFixture fixture)
        {
            Fixture = fixture;

        }

        [Fact]
        public void TestParseEmptyFromQueryable()
        {
            var wql = Fixture.ExecuteWql("");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
            Assert.True(Fixture.TestDataA.Count() == res.Count());
        }

        [Fact]
        public void TestParseEmptyFromIndex()
        {
            var wql = Fixture.ExecuteWql("");
            var res = wql?.Apply();
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
            Assert.True(Fixture.TestDataA.Count() == res.Count());
        }

        [Fact]
        public void TestParseSimpleFilterEqualsFromQueryable()
        {
            var wql = Fixture.ExecuteWql("firstname='Olivia'");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
        public void TestParseSimpleFilterEqualsFromIndex()
        {
            var wql = Fixture.ExecuteWql("firstname='Olivia'");
            var res = wql?.Apply();
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
        public void TestParseSimpleFilterLoremIpsumEqualsFromQueryable()
        {
            var wql = Fixture.ExecuteWql("description~'lorem ipsum'");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.True(res.Count() > 2);
            Assert.Equal("Description ~ 'lorem ipsum'", wql.ToString());
            Assert.Contains("lorem", item.Description);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterLoremIpsumEqualsFromIndex()
        {
            var wql = Fixture.ExecuteWql("description='lorem ipsum'");
            var res = wql?.Apply();
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.True(res.Count() > 2);
            Assert.Equal("Description = 'lorem ipsum'", wql.ToString());
            Assert.Contains("lorem", item.Description);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterLike()
        {
            var wql = Fixture.ExecuteWql("firstname ~ 'Oliv'");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("firstname in ('FirstName33', 'FirstName55')");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("firstname  ~  'olivia'");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal(1, res.Count());
            Assert.Equal("FirstName ~ 'olivia'", wql.ToString());
            Assert.Equal("Olivia", item.FirstName);
            Assert.NotNull(wql.Filter);
            Assert.Null(wql.Order);
            Assert.Null(wql.Partitioning);
        }

        [Fact]
        public void TestParseSimpleFilterAnd()
        {
            var wql = Fixture.ExecuteWql("FirstName = 'FirstName23' and lastname = \"LastName23\"");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("FirstName = 'FirstName55' or LastName = 'LastName33'");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("orderby firstname");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("order by firstname asc");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("order by firstname desc");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("order by firstname, lastname");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("take 10");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("skip 10");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
            var item = res?.FirstOrDefault();

            Assert.NotNull(res);
            Assert.NotNull(item);
            Assert.Equal("skip 10", wql.ToString());
            Assert.Equal("Isabella", item.FirstName);
            Assert.Equal(Fixture.TestDataA.Count() - 10, res.Count());
            Assert.Null(wql.Filter);
            Assert.Null(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }

        [Fact]
        public void TestParsePartitioningTakeSkip()
        {
            var wql = Fixture.ExecuteWql("take 10 skip 5");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("skip 5 take 10");
            var res = wql?.Apply(Fixture.TestDataA.AsQueryable());
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
            var wql = Fixture.ExecuteWql("Salutation = 'Mr.' and id > 2 or description in ('lorem', 'ipsum', 'aenean') order by id desc, description take 10 skip 1");
            var res = wql?.Apply();

            Assert.Equal("(Salutation = 'Mr.' and (Id > 2 or Description in ('lorem', 'ipsum', 'aenean'))) order by Id desc, Description asc take 10 skip 1", wql.ToString());
            Assert.NotNull(wql.Filter);
            Assert.NotNull(wql.Order);
            Assert.NotNull(wql.Partitioning);
        }
    }
}
