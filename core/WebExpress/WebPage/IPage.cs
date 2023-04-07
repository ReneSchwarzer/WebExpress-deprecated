using WebExpress.WebUri;
using WebExpress.WebResource;

namespace WebExpress.WebPage
{
    public interface IPage : IResource
    {
        /// <summary>
        /// Returns or sets the page title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Redirect to another page.
        /// The function throws the RedirectException.
        /// </summary>
        /// <param name="url">The uri to redirect to.</param>
        void Redirecting(IUri url);
    }
}
