using WebExpress.Attribute;

namespace WebExpress.WebApp.WebResource
{
    [ID("SystemInformation")]
    [Title("systeminformation.label")]
    [Segment("systeminformation", "systeminformation.label")]
    [Path("/settings")]
    [Module("webexpress")]
    [Context("admin")]
    public sealed class AdminPageSystemInformation : PageTemplateWebApp
    {

    }
}

