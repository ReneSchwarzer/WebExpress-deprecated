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
    //    private int SelectedColumnId { get; set; }

    //    /// <summary>
    //    /// Liefert oder setzt die ausgewählte Sortierungsspalte
    //    /// </summary>
    //    public string SelectedColumn
    //    {
    //        get { try { return Tag[SelectedColumnId]; } catch { return string.Empty; } }
    //    }

    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    /// <param name="page">Die zugehörige Seite</param>
    //    /// <param name="id">Returns or sets the id.</param>
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

    //        if (string.IsNullOrWhiteSpace(Id))
    //        {
    //            AddParam("order", ParameterScope.Local);
    //            AddParam("column", ParameterScope.Local);
    //        }
    //        else
    //        {
    //            AddParam(Id + "_order", ParameterScope.Session);
    //            AddParam(Id + "_column", ParameterScope.Session);
    //        }

    //        var order = string.IsNullOrWhiteSpace(Id) ? GetParam("order") : GetParam(Id + "_order");
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

    //        var column = string.IsNullOrWhiteSpace(Id) ? GetParam("column") : GetParam(Id + "_column");
    //        try
    //        {
    //            SelectedColumnId = Convert.ToInt32(column);
    //        }
    //        catch
    //        {
    //            SelectedColumnId = -1;
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

    //        //if (string.IsNullOrWhiteSpace(Id))
    //        //{
    //        //    Columns.Add(new ControlLink(Page, null)
    //        //    {
    //        //        Text = name,
    //        //        Class = (SelectedColumnId == i ? Order == SortOrder.Ascending ? "sort_up" : Order == SortOrder.Descending ? "sort_down" : string.Empty : string.Empty),
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
    //        //        Class = (SelectedColumnId == i ? Order == SortOrder.Ascending ? "sort_up" : Order == SortOrder.Descending ? "sort_down" : string.Empty : string.Empty),
    //        //        Params = Parameter.Create
    //        //        (
    //        //            new Parameter(Id + "_order", Order == SortOrder.Ascending ? 'd' : Order == SortOrder.Descending ? 'a' : 'a') { Scope = ParameterScope.Session },
    //        //            new Parameter(Id + "_column", i) { Scope = ParameterScope.Session }
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
