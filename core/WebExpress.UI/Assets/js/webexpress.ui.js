/**
 * Seitennavigationssteuerelement
 */
class paginationCtrl {
    _container = $("<ul class='pagination'/>");
    _currentpage = 0;
    _pagecount = 0;
    _id;
    _css = "";
    _callback;

    /**
     * Konstruktor
     * @param callback Callbackfunktion (function callback(page) {}) zur Reaktion auf ein Clickevent
     * @param options Optionen zur Gestaltung des Steuerelementes
     *                - ID Die ID des Steuerelements
     *                - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(callback, options) {

        this._id = options.ID;
        this._css = options.CSS;
        this._callback = callback;
    }

    /**
     * Setzt die Seite 
     * @param currentpage Die Seitennummer der aktuellen Seite
     * @param pagecount Die Anzahl der Seiten
     */
    page(currentpage, pagecount) {
        this._currentpage = currentpage;
        this._pagecount = pagecount;

        var callback = this._callback;
        var predecessor = $("<li class='page-item'><a class='page-link' href='#'><span class='fas fa-angle-left'/></a></li>");
        var successor = $("<li class='page-item'><a class='page-link' href='#'><span class='fas fa-angle-right'/></a></li>");
        function onclick(page) { return function () { callback(page); } }

        if (this._id !== undefined) {
            this._container.id = id;
        }

        this._container.addClass(this._css);
        predecessor.click(onclick(Math.max(currentpage - 1, 0)));
        successor.click(onclick(Math.min(currentpage + 1, pagecount - 1)));

        this._container.children().remove();

        if (pagecount <= 0) {
            return;
        }

        this._container.append(predecessor);

        if (pagecount < 10) {
            for (var i = 0; i < pagecount; i++) {
                var page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                page.click(onclick(i));

                if (i == currentpage) {
                    page.toggleClass("active");
                }

                this._container.append(page);
            }
        } else {
            if (currentpage <= 3) {
                for (var i = 0; i < 7; i++) {
                    var page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                    page.click(onclick(i));

                    if (i == currentpage) {
                        page.toggleClass("active");
                    }

                    this._container.append(page);
                }

                var placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                var lastpage = $("<li class='page-item'><a class='page-link' href='#'>" + pagecount + "</a></li>");
                lastpage.click(onclick(pagecount - 1));
                this._container.append(lastpage);

            } else if (pagecount - currentpage < 6) {
                var firstpage = $("<li class='page-item'><a class='page-link' href='#'>1</a></li>");
                firstpage.click(onclick(0));
                this._container.append(firstpage);

                var placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                for (var i = pagecount - 7; i < pagecount; i++) {
                    var page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                    page.click(onclick(i));

                    if (i == currentpage) {
                        page.toggleClass("active");
                    }

                    this._container.append(page);
                }

            } else {
                var firstpage = $("<li class='page-item'><a class='page-link' href='#'>1</a></li>");
                firstpage.click(onclick(0));
                this._container.append(firstpage);

                var placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                for (var i = Math.max(currentpage - 2, 0); i < Math.min(currentpage + 3, pagecount); i++) {
                    var page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                    page.click(onclick(i));

                    if (i == currentpage) {
                        page.toggleClass("active");
                    }

                    this._container.append(page);
                }

                placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                var lastpage = $("<li class='page-item'><a class='page-link' href='#'>" + pagecount + "</a></li>");
                lastpage.click(onclick(pagecount - 1));
                this._container.append(lastpage);
            }
        }
        
        this._container.append(successor);
    }

    /**
     * Gibt die Seitennummer der aktuellen Seite zurück
     */
    get currentpage() {
        return this._currentpage;
    }

    /**
     * Gibt die Anzahl der Seiten zurück
     */
    get pagecount() {
        return this._pagecount;
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}

/**
 * Ein Feld, inden Suchbefehle eingegeben werden können.
 */
class searchCtrl {
    _container = $("<div class='input-group mb-2'>");

    /**
     * Konstruktor
     * @param onsearch Callbackfunktion (function callback(searchitem) {}) zur Reaktion auf eine Suchanfrage
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - Icon Die Icon-Klasse des Suchsymbols
     */
    constructor(onsearch, options) { 
        var id = options.ID;
        var css = options.CSS;
        var placeholder = options.Placeholder !== undefined ? options.Placeholder : "";
        var icon = options.Icon !== undefined ? options.Icon : "fas fa-search";

        var searchinput = $("<input type='search' class='form-control' placeholder='" + placeholder + "' aria-label='" + placeholder + "'/>");
        var searchappend = $("<button class='btn btn-secondary' type='button'><span><span class='" + icon + "'></button>");

        if (id !== undefined) {
            container.id = id;
        }
        if (onsearch !== undefined) {
            searchinput.keyup(function () { onsearch(searchinput.val()); });
            searchappend.click(function () { onsearch(searchinput.val()); });
        }

        this._container.addClass(css);

        this._container.append(searchinput);
        this._container.append(searchappend);
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}

/**
 * Ein Dropdown, welches erweiterte Funktionen (Links) anbietet
 */
class moreCtrl {
    _container = $("<div class='dropdown'/>");

    /**
     * Konstruktor
     * @param items Die Menüeinräge Array von { css: "", icon: "", color: "", label: "", url: "", onclick: ""}
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - MenuCSS CSS-Klasse zur Gestaltung des Popupmenüs
     *        - Label Der Text
     *        - Icon Die Icon-Klasse des Steuerelements
     */
    constructor(items, options) {
        var id = options.ID;
        var css = options.CSS;
        var menuCSS = options.MenuCSS;
        var label = options.Label !== undefined ? options.Label : "";
        var icon = options.Icon !== undefined ? options.Icon : "fas fa-search";

        var button = $("<button class='btn' type='button' data-bs-toggle='dropdown' aria-expanded='false'><i class='fas fa-ellipsis-h'/>" + label + "</button>");
        var ul = $("<ul class='dropdown-menu'/>");

        if (menuCSS !== undefined) {
            ul.addClass(menuCSS);
        }

        items.forEach(function (item) {
            var css = item.CSS !== undefined && item.CSS != null ? item.CSS : "dropdown-item";
            var icon = item.Icon;
            var color = item.Color;
            var label = item.Label;
            var url = item.Url !== undefined ? item.Url : "#";
            var onclick = item.OnClick;

            var li = $("<li/>");

            li.addClass(css);

            if (css == "dropdown-item") {
                var a = $("<a class='link " + color + "' href='#'/>");
                if (icon !== undefined) {
                    var span = $("<span class='me-2 " + icon + "'/>");
                    a.append(span);
                }
                if (css !== undefined) {
                    li.addClass(css);
                }
                if (onclick !== undefined) {
                    a.click(onclick);
                }

                a.append($("<span href='" + url + "'>" + label + "</span>"));

                li.append(a);
            }
            else if (css == "dropdown-header") {
                if (icon !== undefined) {
                    var span = $("<span class='me-2 " + icon + "'/>");
                    li.append(span);
                }

                li.append($("<span>" + label + "</span>"));
            }
            else if (css == "dropdown-divider") {

            }

            ul.append(li);
        });

        if (id !== undefined) {
            this._container.id = id;
        }
        if (css !== undefined) {
            this._container.addClass(css);
        }

        this._container.append(button);
        this._container.append(ul);
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}

/**
 * Tabelle
 */
class tableCtrl {
    _table = $("<table class='table table-hover mb-2'/>");
    _col = $("<colgroup/>");
    _head = $("<thead/>");
    _body = $("<tbody>");
    _columns = [];

    /**
     * Konstruktor
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(options) {
        var id = options.ID;
        var css = options.CSS;

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
        var th = $("<tr/>");

        this._columns.forEach(function (column) {
            if (column.render !== undefined) {
                var cell = $("<td/>");
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
        var columns = this._columns;
        var rows = [];

        data.forEach(function (row) {
            var th = $("<tr/>");

            columns.forEach(function (column) {
                if (column.Render !== undefined && column.Render != null && (typeof column.Render === 'string' || column.Render instanceof String)) {
                    var cell = $("<td/>");
                    var render = Function("cell", "item", column.Render);
                    cell.append(render(cell, row));
                    th.append(cell);
                } else if (column.Render !== undefined && column.Render != null) {
                    var cell = $("<td/>");
                    cell.append(column.Render(cell, row));
                    th.append(cell);
                }
            });

            rows.push(th);
        });

        this._body.append(rows);
    }

    /**
     * Setzt die Spaltendefinitionen
     */
    set columns(columns) {
        this._columns = columns;

        var head_col = [];
        var head_row = $("<tr/>");
                
        this._columns.forEach(function (column) {
            var label = column.Label;
            var icon = column.Icon;
            var width = column.Width !== undefined && column.Width != null ? column.Width + "%" : "auto";

            var col = $("<col span='1' style='width: " + width + ";'>");
            var th = $("<th/>");

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

        //this._col.remove();
        this._col.append(head_col);
        //this._head.remove();
        this._head.append(head_row);
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