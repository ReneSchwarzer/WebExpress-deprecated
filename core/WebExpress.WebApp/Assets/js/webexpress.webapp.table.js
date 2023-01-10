/**
 * Eine Tabelle mit Funktionen für Create, Read, Update und Delate
 */
webexpress.webapp.tableCtrl = class extends webexpress.ui.tableCtrl {
    _restUri = "";
    _searchCtrl = null;
    _paginationCtrl = null;
    _filter = null;
    _page = null;

    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die ID des Steuerelements
     *        - css CSS-Klasse zur Gestaltung des Steuerelementes
     *        - placeholder Der Platzhaltertext
     *        - resturi Die Uri der REST-API-Schnittstelle, welche die Daten ermittelt
     */
    constructor(settings) {
        super(settings);

        this._restUri = settings.resturi;

        $.ajax({ type: "GET", url: this._restUri + "?columns=true", dataType: 'json', }).then(function (response) {
            var columns = response.Columns;
            this.columns = columns;
        }.bind(this));

        this._searchCtrl = new webexpress.ui.searchCtrl({ ID: settings.id + "-search" });
        this._searchCtrl.on('webexpress.ui.change.filter', function (key) { this._filter = key; this.receiveData(); }.bind(this));
        this._paginationCtrl = new webexpress.ui.paginationCtrl({ ID: settings.ID + "-pagination" });
        this._paginationCtrl.on('webexpress.ui.change.page', function (page) { this._page = page; this.receiveData(); }.bind(this));
    }

    /**
      * Daten aus REST-Schnitstelle abrufen
      */
    receiveData() {
        if (this._filter === undefined || this._filter == null) { this._filter = ""; }
        if (this._page === undefined || this._page == null) { this._page = 0; }
        $.ajax({ type: "GET", url: this._restUri + "?wql=" + this._filter + "&page=" + this._page, dataType: 'json', }).then(function (response) {
            var data = response.data;
            this.clear();
            this.addRange(data);
            this.trigger('webexpress.ui.receive.complete');
            var pagination = response.pagination;
            this._paginationCtrl.page(pagination.pagenumber, pagination.totalpage);
        }.bind(this));
    }

    /**
    * Gibt das Steuerelement zurück
    */
    get getCtrl() {
        let div = $("<div/>")
        div.append(this._searchCtrl.getCtrl);
        div.append(this._table);
        div.append(this._paginationCtrl.getCtrl);
        return div;
    }
}