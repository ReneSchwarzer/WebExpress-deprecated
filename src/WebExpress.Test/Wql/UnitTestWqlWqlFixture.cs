using WebExpress.WebApp.Wql;
using WebExpress.WebApp.Wql.Condition;

namespace WebExpress.Test.Wql
{
    public class UnitTestWqlWqlFixture
    {
        public UnitTestWqlWqlFixture()
        {
            WqlParser.Register<WqlExpressionConditionBinaryEqual>();
            WqlParser.Register<WqlExpressionConditionBinaryLike>();

            WqlParser.Register<WqlExpressionConditionSetIn>();
        }

        public void Dispose()
        {

        }
    }
}
