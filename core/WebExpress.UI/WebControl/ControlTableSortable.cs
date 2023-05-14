namespace WebExpress.UI.WebControl
{
    //public class ControlTableSortable : ControlTable
    //{
    //    /// <summary>
    //    /// Liefert oder setzt die Tags
    //    /// </summary>
    //    private Dictionary<int, string> Tag { get; set; }

    //    /// <summary>
    //    /// Liefert oder setzt die Sortierungsreihenfolge
    //    /// </summary>
    //    public SortOrder Order { get; protected set; }

    //    /// <summary>
    //    /// Liefert oder setzt die ausgewählte Sortierungsspalte
    //    /// </summary>
    //    private int SelectedColumnID { get; set; }

    //    /// <summary>
    //    /// Liefert oder setzt die ausgewählte Sortierungsspalte
    //    /// </summary>
    //    public string SelectedColumn
    //    {
    //        get { try { return Tag[SelectedColumnID]; } catch { return string.Empty; } }
    //    }

    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    /// <param name="page">Die zugehörige Seite</param>
    //    /// <param name="id">The id.</param>
    //    public ControlTableSortable(IPage page, string id = null)
    //        : base(page, id)
    //    {
    //        Init();
    //    }

    //    /// <summary>
    //    /// Initialization
    //    /// </summary>
    //    private void Init()
    //    {
    //        Tag = new Dictionary<int, string>();

    //        if (string.IsNullOrWhiteSpace(ID))
    //        {
    //            AddParam("order", ParameterScope.Local);
    //            AddParam("column", ParameterScope.Local);
    //        }
    //        else
    //        {
    //            AddParam(ID + "_order", ParameterScope.Session);
    //            AddParam(ID + "_column", ParameterScope.Session);
    //        }

    //        var order = string.IsNullOrWhiteSpace(ID) ? GetParam("order") : GetParam(ID + "_order");
    //        switch (order.ToLower())
    //        {
    //            case "a":
    //                Order = SortOrder.Ascending;
    //                break;
    //            case "d":
    //                Order = SortOrder.Descending;
    //                break;
    //            default:
    //                Order = SortOrder.Unspecified;
    //                break;
    //        }

    //        var column = string.IsNullOrWhiteSpace(ID) ? GetParam("column") : GetParam(ID + "_column");
    //        try
    //        {
    //            SelectedColumnID = Convert.ToInt32(column);
    //        }
    //        catch
    //        {
    //            SelectedColumnID = -1;
    //        }
    //    }

    //    /// <summary>
    //    /// Fügt eine Spalte hinzu
    //    /// </summary>
    //    /// <param name="name">Name der Spalte</param>
    //    /// <returns></returns>
    //    public override void AddColumn(string name)
    //    {
    //        AddColumn(name, name);
    //    }

    //    /// <summary>
    //    /// Fügt eine Spalte hinzu
    //    /// </summary>
    //    /// <param name="name">Name der Spalte</param>
    //    /// <param name="tag">Der interne Name</param>
    //    public override void AddColumn(string name, string tag)
    //    {
    //        //int i = Columns.Count;

    //        //if (string.IsNullOrWhiteSpace(ID))
    //        //{
    //        //    Columns.Add(new ControlLink(Page, null)
    //        //    {
    //        //        Text = name,
    //        //        Class = (SelectedColumnID == i ? Order == SortOrder.Ascending ? "sort_up" : Order == SortOrder.Descending ? "sort_down" : string.Empty : string.Empty),
    //        //        Params = Parameter.Create
    //        //        (
    //        //            new Parameter("order", Order == SortOrder.Ascending ? 'd' : Order == SortOrder.Descending ? 'a' : 'a') { Scope = ParameterScope.Local },
    //        //            new Parameter("column", i) { Scope = ParameterScope.Local }
    //        //        )
    //        //    });
    //        //}
    //        //else
    //        //{
    //        //    Columns.Add(new ControlLink(Page, null)
    //        //    {
    //        //        Text = name,
    //        //        Class = (SelectedColumnID == i ? Order == SortOrder.Ascending ? "sort_up" : Order == SortOrder.Descending ? "sort_down" : string.Empty : string.Empty),
    //        //        Params = Parameter.Create
    //        //        (
    //        //            new Parameter(ID + "_order", Order == SortOrder.Ascending ? 'd' : Order == SortOrder.Descending ? 'a' : 'a') { Scope = ParameterScope.Session },
    //        //            new Parameter(ID + "_column", i) { Scope = ParameterScope.Session }
    //        //        )
    //        //    });
    //        //}

    //        //Tag.Add(i, tag);
    //    }

    //    /// <summary>
    //    /// Convert to html.
    //    /// </summary>
    //    /// <returns>The control as html.</returns>
    //    public override IHtmlNode ToHtml()
    //    {
    //        return base.ToHtml();
    //    }
    //}
}
