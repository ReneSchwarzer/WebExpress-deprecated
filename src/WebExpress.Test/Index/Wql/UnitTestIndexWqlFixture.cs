using WebExpress.WebApp.WebIndex.Wql;

namespace WebExpress.Test.Index.Wql
{
    public class UnitTestIndexWqlFixture : UnitTestIndexFixture
    {
        public UnitTestIndexWqlFixture()
        {
            IndexManager.Register<UnitTestIndexTestDocumentA>();

            IndexManager.ReIndex(TestDataA);
        }

        public override void Dispose()
        {
        }

        public IWqlStatement<UnitTestIndexTestDocumentA> ExecuteWql(string wql)
        {
            return IndexManager.ExecuteWql<UnitTestIndexTestDocumentA>(wql);
        }
    }
}
