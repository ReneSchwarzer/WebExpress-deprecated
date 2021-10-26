namespace WebExpress.UI.WebComponent
{
    public interface IComponent
    {
        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        void Initialization(IComponentContext context);
    }
}
