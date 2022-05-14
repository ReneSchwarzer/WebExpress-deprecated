/**
 * Tabelle
 */
class tableCtrl extends events {
    _table = $("<table class='table table-hover mb-2'/>");
    _col = $("<colgroup/>");
    _head = $("<thead/>");
    _body = $("<tbody>");
    _columns = [];

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(settings) {
        super();

        let id = settings.ID;
        let css = settings.CSS;

        if (id !== undefined) {
            this._table.id = id;
        }

        this._table.addClass(css);
        this._table.append(this._col);
        this._table.append(this._head);
        this._table.append(this._body);
    }

    /**
     * Löscht alle Zeilen aus der Tabelle
     */
    clear() {
        this._body.children().remove();
    }

    /**
     * Fügt eine Zeile ein
     * @param row Ein Objekt mit den Werten der Spalten
     */
    add(row) {
        let th = $("<tr/>");

        this._columns.forEach(function (column) {
            if (column.render !== undefined) {
                let cell = $("<td/>");
                cell.append(column.render(cell, row));
                th.append(cell);
            }
        });

        this._body.append(th);
    }

    /**
     * Fügt mehrere Zeilen ein
     * @param data Ein Array mit Objekten der Zellen
     */
    addRange(data) {
        let columns = this._columns;
        let rows = [];

        data.forEach(function (row) {
            let th = $("<tr/>");

            columns.forEach(function (column) {
                if (column.Render !== undefined && column.Render != null && (typeof column.Render === 'string' || column.Render instanceof String)) {
                    let cell = $("<td/>");
                    let render = Function("cell", "item", column.Render);
                    let renderResult = render(cell, row);
                    if (renderResult !== undefined && renderResult != null) {
                        cell.append(renderResult);
                    }
                    th.append(cell);
                } else if (column.Render !== undefined && column.Render != null) {
                    let cell = $("<td/>");
                    let renderResult = render(cell, row);
                    if (renderResult !== undefined && renderResult != null) {
                        cell.append(renderResult);
                    }
                    th.append(cell);
                }
            });

            rows.push(th);
        });
        this._body.append(rows);
    }

    /**
     * Setzt die Spaltendefinitionen
     * @param columns Die Spalten Array aus Objekten { Label, Icon, Width}
     */
    set columns(columns) {
        this._columns = columns;

        let head_col = [];
        let head_row = $("<tr/>");
                
        this._columns.forEach(function (column) {
            let label = column.Label;
            let icon = column.Icon;
            let width = column.Width !== undefined && column.Width != null ? column.Width + "%" : "auto";

            let col = $("<col span='1' style='width: " + width + ";'>");
            let th = $("<th/>");

            if (icon !== undefined && icon != null && (typeof icon === 'string' || icon instanceof String)) {

                th.append($("<i class='" + icon + " me-2'/>"));
                th.append(label);
            } else if (icon !== undefined && icon != null) {
                icon.addClass("me-2");
                th.append(icon);
                th.append(label);
            } else {
                th.append(label);
            }

            head_col.push(col);
            head_row.append(th)
        });

        this._col.children().remove();
        this._col.append(head_col);
        this._head.children().remove();
        this._head.append(head_row);

        this.trigger('webexpress.ui.change.columns');
    }

    /**
     * Gibt Spaltendefinitionen zurück
     */
    get columns() {
        return this._columns;
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._table;
    }
}