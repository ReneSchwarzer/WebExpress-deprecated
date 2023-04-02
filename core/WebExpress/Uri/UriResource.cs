namespace WebExpress.Uri
{
    /// <summary>
    /// A uri that references a resource.
    /// </summary>
    public class UriResource : UriRelative, IUriResource
    {
        ///// <summary>
        ///// Returns or sets the context path.
        ///// </summary>
        //public IResourceContext ResourceContext { get; private set; }

        ///// <summary>
        ///// Constructor
        ///// </summary>
        ///// <param name="resourceContext">The context of the resource.</param>
        ///// <param name="url">The actual uri called by the web browser.</param>
        ///// <param name="culture">The culture.</param>
        //internal UriResource(IResourceContext resourceContext, IEnumerable<IUriPathSegment> path, CultureInfo culture)
        //{
        //    if (resourceContext == null) return;

        //    foreach (var uriPathSegment in path)
        //    {
        //        Path.Add(uriPathSegment);
        //    }

        //}
    }
}
