class events {
    _listeners = new Map();
    
    /**
     * Konstruktor
     */
    constructor() {
    }
    
    /**
     * Registriert ein Eventhandler für ein Event
     * @param label Das Event-Label
     * @param callback Die Rückrufsfunktion, wenn das Event gefeuert wird
     */
    on(label, callback) {
        this._listeners.has(label) || this._listeners.set(label, []);
        this._listeners.get(label).push(callback);
    }

    /**
     * Feuert ein Event
     * @param Das Event-Label
     * @param args Die Argumente
     */
    trigger(label, ...args) {
        let res = false;

        let _trigger = (inListener, label, ...args) => {
            let listeners = inListener.get(label);
            if (listeners && listeners.length) {
                listeners.forEach((listener) => {
                    listener(...args);
                });
                res = true;
            }
        };
        _trigger(this._listeners, label, ...args);

        return res;
    }
}

/**
 * Seitennavigationssteuerelement
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.page mit Parameter page
 */
class paginationCtrl extends events {
    _container = $("<ul class='pagination'/>");
    _currentpage = 0;
    _pagecount = 0;
    _id;
    _css = "";

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *                - ID Die ID des Steuerelements
     *                - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(settings) {
        super();
        
        this._id = settings.ID;
        this._css = settings.CSS;

        if (this._id !== undefined) {
            this._container.id = this._id;
        }
    }

    /**
     * Setzt die Seite 
     * @param currentpage Die Seitennummer der aktuellen Seite
     * @param pagecount Die Anzahl der Seiten
     */
    page(currentpage, pagecount) {
        this._currentpage = currentpage;
        this._pagecount = pagecount;
        
        let predecessor = $("<li class='page-item'><a class='page-link' href='#'><span class='fas fa-angle-left'/></a></li>");
        let successor = $("<li class='page-item'><a class='page-link' href='#'><span class='fas fa-angle-right'/></a></li>");
        function onclick(page) { 
            this.trigger('webexpress.ui.change.page', page);
        };

        this._container.addClass(this._css);
        predecessor.click(function () { this.trigger('webexpress.ui.change.page', Math.max(currentpage - 1, 0)); }.bind(this));
        successor.click(function () { this.trigger('webexpress.ui.change.page', Math.min(currentpage + 1, pagecount - 1)); }.bind(this));

        this._container.children().remove();

        if (pagecount <= 0) {
            return;
        }

        this._container.append(predecessor);

        if (pagecount < 10) {
            for (let i = 0; i < pagecount; i++) {
                let page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                page.click(function () { this.trigger('webexpress.ui.change.page', i); }.bind(this));

                if (i == currentpage) {
                    page.toggleClass("active");
                }

                this._container.append(page);
            }
        } else {
            if (currentpage <= 3) {
                for (let i = 0; i < 7; i++) {
                    let page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                    page.click(function () { this.trigger('webexpress.ui.change.page', i); }.bind(this));

                    if (i == currentpage) {
                        page.toggleClass("active");
                    }

                    this._container.append(page);
                }

                let placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                let lastpage = $("<li class='page-item'><a class='page-link' href='#'>" + pagecount + "</a></li>");
                lastpage.click(function () { this.trigger('webexpress.ui.change.page', pagecount - 1); }.bind(this));
                this._container.append(lastpage);

            } else if (pagecount - currentpage < 6) {
                let firstpage = $("<li class='page-item'><a class='page-link' href='#'>1</a></li>");
                firstpage.click(function () { this.trigger('webexpress.ui.change.page', 0); }.bind(this));
                this._container.append(firstpage);

                let placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                for (let i = pagecount - 7; i < pagecount; i++) {
                    let page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                    page.click(function () { this.trigger('webexpress.ui.change.page', i); }.bind(this));

                    if (i == currentpage) {
                        page.toggleClass("active");
                    }

                    this._container.append(page);
                }

            } else {
                let firstpage = $("<li class='page-item'><a class='page-link' href='#'>1</a></li>");
                firstpage.click(function () { this.trigger('webexpress.ui.change.page', 0); }.bind(this));
                this._container.append(firstpage);

                let placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                for (let i = Math.max(currentpage - 2, 0); i < Math.min(currentpage + 3, pagecount); i++) {
                    let page = $("<li class='page-item'><a class='page-link' href='#'>" + (i + 1) + "</a></li>");
                    page.click(function () { this.trigger('webexpress.ui.change.page', i); }.bind(this));

                    if (i == currentpage) {
                        page.toggleClass("active");
                    }

                    this._container.append(page);
                }

                placeholder = $("<li class='page-item disabled'><a class='page-link' href='#'>...</a></li>");
                this._container.append(placeholder);

                let lastpage = $("<li class='page-item'><a class='page-link' href='#'>" + pagecount + "</a></li>");
                lastpage.click(function () { this.trigger('webexpress.ui.change.page', pagecount - 1); }.bind(this));
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
 * Ein Container zum verstecken von Inhalten
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.visibility 
 */
class expandCtrl extends events {
    _container = $("<span class='expand'>");
    _content = $("<div/>");
    _expandicon = $("<a class='fas fa-angle-right text-primary' href='#'/>");
    _expand = true;

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Header Der Überschriftsext
     */
    constructor(settings) {
        let id = settings.ID;
        let css = settings.CSS;
        let header = settings.Header !== undefined ? settings.Header : "";
        
        let expandheader = $("<span class='text-primary' aria-label='" + header + "'>" + header + "</span>");
        
        super();
        
        this._expandicon.click(function () {
            this.expand = !this.expand;
        }.bind(this));
        
        expandheader.click(function () {
            this.expand = !this.expand;
        }.bind(this));

        this._container.append(this._expandicon);
        this._container.append(expandheader);
        this._container.append(this._content);
        
        this.expand = true;
    }
    
     /**
     * Aktualisierung des Steuerelementes
     */
    update() {
        this._content.toggleClass("hide");
    }
    
    /**
     * Ermittelt, ob das Steuerelement aufgeplappt ist
     */
    get expand() {
        return this._expand;
    }
    
    /**
     * Klappt das Stuerelement auf oder schließt es
     * @param value Ein Wert, welcher bestimmt ob das Steuerelement auf- oder zugeklappt ist
     */
    set expand(value) {
        if (this._expand != value) {
            this.trigger('webexpress.ui.change.visibility', value);
            this._expand = value;
        }
        
        if (this._expand) {
            this._content.removeClass("hide");
            this._expandicon.addClass("expand-angle-down");
        } else {
            this._content.addClass("hide");
            this._expandicon.removeClass("expand-angle-down");
        }
    }

    /**
     * Gibt den Inhalt zurück
     */
    get content() {
        return this._content.children();
    }
    
    /**
     * Setzt den Inhalt
     * @param content Ein Array mit Inhalten
     */
    set content(content) {
        this._content.children().remove();

        this._content.append(content);
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
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.filter mit Parameter filter
 */
class searchCtrl extends events {
    _container = $("<span class='search form-control'>");

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - Icon Die Icon-Klasse des Suchsymbols
     */
    constructor(settings) {
        let id = settings.ID;
        let css = settings.CSS;
        let placeholder = settings.Placeholder !== undefined ? settings.Placeholder : "";
        let icon = settings.Icon !== undefined ? settings.Icon : "fas fa-search";

        let searchicon = $("<label><i class='" + icon + "'/></label>");
        let searchinput = $("<input type='text' placeholder='" + placeholder + "' aria-label='" + placeholder + "'/>");
        let searchappend = $("<span><i class='fas fa-times'/><span>");
        
        super();

        if (id !== undefined) {
            this._container.id = id;
        }
        
        searchinput.keyup(function () { 
            this.trigger('webexpress.ui.change.filter', searchinput.val());
            
        }.bind(this));
        
        searchappend.click(function () {
            searchinput.val('');
            this.trigger('webexpress.ui.change.filter', '');
        }.bind(this));

        this._container.addClass(css);
        
        this._container.append(searchicon);
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
     * @param options Die Menüeinräge Array von { CSS: "", Icon: "", Color: "", Label: "", Url: "", OnClick: ""}
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - MenuCSS CSS-Klasse zur Gestaltung des Popupmenüs
     *        - Label Der Text
     *        - Icon Die Icon-Klasse des Steuerelements
     */
    constructor(options, settings) {
        let id = settings.ID;
        let css = settings.CSS;
        let menuCSS = settings.MenuCSS;
        let label = settings.Label !== undefined ? settings.Label : "";
        let icon = settings.Icon !== undefined ? settings.Icon : "fas fa-ellipsis-h";

        let button = $("<button class='btn' type='button' data-bs-toggle='dropdown' aria-expanded='false'><i class='" + icon + " " + (label != "" ? "me-2" : "") + "'></i><span>" + label + "</span></button>");
        let ul = $("<ul class='dropdown-menu'/>");

        if (menuCSS !== undefined) {
            ul.addClass(menuCSS);
        }

        options.forEach(function (option) {
            let css = option.CSS !== undefined && option.CSS != null ? option.CSS : "dropdown-options";
            let icon = option.Icon;
            let color = option.Color;
            let label = option.Label;
            let url = option.Url !== undefined ? option.Url : "#";
            let onclick = option.OnClick;

            let li = $("<li/>");

            li.addClass(css);

            if (css == "dropdown-item") {
                let a = $("<a class='link " + color + "' href='#'/>");
                if (icon !== undefined) {
                    let span = $("<span class='me-2 " + icon + "'/>");
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
                    let span = $("<span class='me-2 " + icon + "'/>");
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
                    cell.append(render(cell, row));
                    th.append(cell);
                } else if (column.Render !== undefined && column.Render != null) {
                    let cell = $("<td/>");
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

/**
 * Auswahlfeld zum Aktivieren von Optionen
 * - webexpress.ui.change.value mit Parameter value
 */
class selectionMoveCtrl extends events {
    _container = $("<div class='selection-move'/>");
    _selectedList = $("<ul class='list-group list-group-flush'/>");
    _availableList = $("<ul class='list-group list-group-flush'/>");
    _buttonToSelectedAll = $("<button class='btn btn-primary btn-block' type='button'/>");
    _buttonToSelected = $("<button class='btn btn-primary btn-block' type='button'/>");
    _buttonToAvailable = $("<button class='btn btn-primary btn-block' type='button'/>");
    _buttonToAvailableAll = $("<button class='btn btn-primary btn-block' type='button'/>");
    _hidden = $("<input type='hidden'/>");
    _options = [];
    _values = [];
    _selectedoptions = new Map(); // Key=Ctrl, Value=options
    _availableoptions = new Map(); // Key=Ctrl, Value=options
    
    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - Name Der Steuerelementenname
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Header Überschrift { Selected, Available }
     *        - Buttons Schaltflächenbeschriftung { ToSelectedAll, ToSelected, ToAvailable, ToAvailableAll }
     */
    constructor(settings) {
        super();

        let id = settings.ID;
        let name = settings.Name;
        let css = settings.CSS;
        let header = settings.Header;
        let buttons = settings.Buttons;
        let selectedContainer = $("<div class='selection-move-list'/>");
        let selectedHeader = $("<span class='text-muted'>" + header.Selected + "</span>");
        let availableContainer = $("<div class='selection-move-list'/>");
        let availableHeader = $("<span class='text-muted'>" + header.Available + "</span>");
        let buttonContainer = $("<div class='selection-move-button d-grid gap-2'/>");
        
        if (id !== undefined) {
            this._container.attr("id", id);
        }

        if (css !== undefined) {
            this._container.addClass(css);
        }

        if (name !== undefined && name != null) {
            this._hidden.attr("name", name);
        }
        
        this._buttonToSelectedAll.html(buttons.ToSelectedAll);
        this._buttonToSelected.html(buttons.ToSelected);
        this._buttonToAvailable.html(buttons.ToAvailable);
        this._buttonToAvailableAll.html(buttons.ToAvailableAll);
        
        selectedContainer.append(selectedHeader);
        selectedContainer.append(this._selectedList);
        availableContainer.append(availableHeader);
        availableContainer.append(this._availableList);
        buttonContainer.append(this._buttonToSelectedAll);
        buttonContainer.append(this._buttonToSelected);
        buttonContainer.append(this._buttonToAvailable);
        buttonContainer.append(this._buttonToAvailableAll);

        selectedContainer.on('dragenter', function(e) {
            e.preventDefault();
            this._selectedList.addClass('drag-over');
        }.bind(this));
        
        selectedContainer.on('dragover', function(e) {
            e.preventDefault();
            this._selectedList.addClass('drag-over');
        }.bind(this));
        
        selectedContainer.on('dragleave', function() {
            this._selectedList.removeClass('drag-over');
        }.bind(this));
        
        selectedContainer.on('drop', function(e) {
            this._selectedList.removeClass('drag-over');
            this.moveToSelected();
            e.preventDefault(); 
        }.bind(this));
        
        availableContainer.on('dragenter', function(e) {
            e.preventDefault();
            this._availableList.addClass('drag-over');
        }.bind(this));
        
        availableContainer.on('dragover', function(e) {
            e.preventDefault();
            this._availableList.addClass('drag-over');
        }.bind(this));
        
        availableContainer.on('dragleave', function() {
            this._availableList.removeClass('drag-over');
        }.bind(this));
        
        availableContainer.on('drop', function(e) {
            this._availableList.removeClass('drag-over');
            this.moveToAvailable();
            e.preventDefault();
        }.bind(this));
        
        this._buttonToSelectedAll.click(function() {    
            this.moveToSelectedAll();
        }.bind(this));

        this._buttonToSelected.click(function() {
            this.moveToSelected();
        }.bind(this));
        
        this._buttonToAvailableAll.click(function() {
            this.moveToAvailableAll();
        }.bind(this));

        this._buttonToAvailable.click(function() {  
            this.moveToAvailable();
        }.bind(this));
        
        this._container.append(selectedContainer);
        this._container.append(buttonContainer);
        this._container.append(availableContainer);

        if (name !== undefined && name != null) {
            this._container.append(this._hidden);
        }
        
        this.update();
    }
    
    /**
     * Verschiebe alle Einträge nach links (selected)
     */
    moveToSelectedAll() {
        this.value = this._options.map(element => element.ID);

        this.update();
    }
    
    /**
     * Verschiebt ein einzelnen Eintrag nach links (selected)
     */
    moveToSelected() {
        this.value = this._values.concat(Array.from(this._availableoptions.values()).filter(elem => elem != null).map(elem => elem.ID));
        
        this.update();
    }

    /**
     * Verschiebe alle Einträge nach rechts (available)
     */
    moveToAvailableAll() {
        this.value = [];

        this.update();
    }

    /**
     * Verschiebt ein einzelnen Eintrag nach rechts (available)
     */
    moveToAvailable() {
        this.value = this._values.filter(b => !Array.from(this._selectedoptions.values()).filter(elem => elem != null).map(elem => elem.ID).includes(b));
        
        this.update();
    }
   
    /**
     * Aktualisierung des Steuerelementes
     */
    update() {
        let values = this._values !== undefined && this._values != null ? this._values : [];
        let comparison = (a, b) => a === b.ID;
        let relativeComplement = this._options.filter(b => values.every(a => !comparison(a, b)));
        let intersection = this._options.filter(b => values.includes(b.ID));
        
        this._selectedList.children().remove();
        this._availableList.children().remove();
        this._selectedoptions.clear();
        this._availableoptions.clear();

        function updateselection() {
            this._selectedoptions.forEach(function(value, key) {
                if (value != null) {
                    key.addClass("bg-primary");
                    key.children().addClass("text-white");
                } else {
                    key.removeClass("bg-primary");
                    key.children().removeClass("text-white");
                }
            });
            this._availableoptions.forEach(function(value, key) {
                if (value != null) {
                    key.addClass("bg-primary");
                    key.children().addClass("text-white");
                } else {
                    key.removeClass("bg-primary");
                    key.children().removeClass("text-white");
                }
            });
            
            if (Array.from(this._availableoptions.values()).filter(elem => elem != null).length == 0) {
                this._buttonToSelected.addClass("disabled");
                this._buttonToSelected.prop("disabled", true);
            } else {
                this._buttonToSelected.removeClass("disabled");
                this._buttonToSelected.prop("disabled", false);
            }
            
            if (Array.from(this._selectedoptions.values()).filter(elem => elem != null).length == 0) {
                this._buttonToAvailable.addClass("disabled");
                this._buttonToAvailable.prop("disabled", true);
            } else {
                this._buttonToAvailable.removeClass("disabled");
                this._buttonToAvailable.prop("disabled", false);
            }
        }

        intersection.forEach(function(currentValue) {   
            let li = $("<li class='list-group-item' draggable='true'/>");
            let img = $("<img title='' src='" + currentValue.Image + "' draggable='false'/>");
            let icon = $("<i class='text-primary " + currentValue.Icon + "' draggable='false'/>");
            let a = $("<a class='link' href='javascript:void(0)' draggable='false'>" + "".concat(currentValue.Label) + "</a>");
            if (currentValue.Icon !== undefined && currentValue.Icon != null) {
                li.append(icon);
            }
            if (currentValue.Image !== undefined && currentValue.Image != null) {
                li.append(img);
            }
            li.append(a);
            this._selectedoptions.set(li, null);
                        
            li.click(function() {   
                if (event.ctrlKey) {
                    if (!Array.from(this._selectedoptions.values()).some(elem => elem === currentValue)) {
                        this._selectedoptions.set(li, currentValue);
                    } else {
                        this._selectedoptions.set(li, null);
                    }
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                } else {
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                    this._selectedoptions.set(li, currentValue);
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                }
                updateselection.call(this);
            }.bind(this, a)).dblclick(function() {  
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._selectedoptions.set(li, currentValue);
                this._availableoptions.forEach((value, key, map) => map.set(key, null));

                this.moveToAvailable();
            }.bind(this, a)).keyup(function() { 
                if (event.keyCode === 32) {
                    if (!Array.from(this._selectedoptions.keys()).some(elem => elem === currentValue)) {
                        this._selectedoptions.set(li, currentValue);
                    } else {
                        this._selectedoptions.set(li, null);
                    }
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                    updateselection.call(this);
                }
            }.bind(this, a));
            
            li.on('dragstart', function dragStart(e) {
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._selectedoptions.set(li, currentValue);
                this._availableoptions.forEach((value, key, map) => map.set(key, null));    
                updateselection.call(this);             
            }.bind(this));

            this._selectedList.append(li);
        }.bind(this));

        relativeComplement.forEach(function(currentValue) { 
            let li = $("<li class='list-group-item' draggable='true'/>");
            let img = $("<img title='' src='" + currentValue.Image + "' draggable='false'/>");
            let icon = $("<i class='text-primary " + currentValue.Icon + "' draggable='false'/>");
            let a = $("<a class='link' href='javascript:void(0)' draggable='flase'>" + "".concat(currentValue.Label) + "</a>");
            if (currentValue.Icon !== undefined && currentValue.Icon != null) {
                li.append(icon);
            }
            if (currentValue.Image !== undefined && currentValue.Image != null) {
                li.append(img);
            }
            li.append(a);
            this._availableoptions.set(li, null);
            
            li.click(function() {   
                if (event.ctrlKey) {
                    if (!Array.from(this._availableoptions.values()).some(elem => elem === currentValue)) {
                        this._availableoptions.set(li, currentValue);
                    } else {
                        this._availableoptions.set(li, null);
                    }
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                } else {
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                    this._availableoptions.set(li, currentValue);
                }
                                
                updateselection.call(this);
            }.bind(this, a)).dblclick(function() {  
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.set(li, currentValue);

                this.moveToSelected();
            }.bind(this, a)).keyup(function() { 
                if (event.keyCode === 32) {
                    if (!Array.from(this._availableoptions.keys()).some(elem => elem === currentValue)) {
                        this._availableoptions.set(li, currentValue);
                    } else {
                        this._availableoptions.set(li, null);
                    }
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                    updateselection.call(this);
                }
            }.bind(this, a));
            
            li.on('dragstart', function dragStart(e) {
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.set(li, currentValue);
                updateselection.call(this);
            }.bind(this));

            this._availableList.append(li);
        }.bind(this));
        
        if (relativeComplement.length == 0) {
            this._buttonToSelectedAll.addClass("disabled");
            this._buttonToSelectedAll.prop("disabled", true);
        } else {
            this._buttonToSelectedAll.removeClass("disabled");
            this._buttonToSelectedAll.prop("disabled", false);
        }

        if (values.length == 0) {
            this._buttonToAvailableAll.addClass("disabled");
            this._buttonToAvailableAll.prop("disabled", true);
        } else {
            this._buttonToAvailableAll.removeClass("disabled");
            this._buttonToAvailableAll.prop("disabled", false);
        }
        
        updateselection.call(this);
    }

    /**
     * Gibt alle Optionen zurück
     */
    get options() {
        return this._options;
    }

    /**
     * Setzt die Optionen
     * @param options Ein Array mit Optionen { ID, Label, Icon, Image }
     */
    set options(options) {
        this._options = options;

        this.update();
    }
    
    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get value() {
        return this._values;
    }
    
    /**
     * Setzt die ausgewählten Optionen
     * @param values Ein Array mit ObjektIDs
     */
    set value(values) {
        if (this._values != values) {
            this.trigger('webexpress.ui.change.value', values);
        }

        this._values = values;
        this._hidden.val(this._values.map(element => element).join(';'));
        
        this.update();
    }
    
    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}

/**
 * Ein Auswahlfeld
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.filter mit Parameter filter
 * - webexpress.ui.change.value mit Parameter value
 */
class selectionCtrl extends events {
    _container = $("<span class='selection form-control' />");
    _selection = $("<span/>");
    _hidden = $("<input type='hidden'/>");
    _dropdownmenu = $("<div class='dropdown-menu'/>");
    _dropdownoptions = $("<ul/>");
    _options = [];
    _value = null;
    _filter = '';
    _placeholder = null;

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - HasEmptyValue Bestimmt, ob Nullwerte zugelassen sind
     */
    constructor(settings) {
        let id = settings.ID;
        let name = settings.Name;
        let css = settings.CSS;
        let placeholder = settings.Placeholder !== undefined ? settings.Placeholder : null;
        let emptyvalue = settings.HasEmptyValue !== undefined ? settings.HasEmptyValue : false;

        let dropdown = $("<span data-bs-toggle='dropdown' aria-expanded='false'/>");
        let expand = $("<ul/>");
        let reset = $("<li><a class='fas fa-times' href='#'/></li>");
        let toggle = $("<li><a class='fas fa-angle-down' href='#'/></li>");
        let filter = $("<input type='text'/>");

        super();

        if (id !== undefined) {
            this._container.attr("id", id);
        }

        if (css !== undefined) {
            this._container.addClass(css);
        }

        if (name !== undefined && name != null) {
            this._hidden.attr("name", name);
        }
        
        this._container.on('show.bs.dropdown', function () {
            let width = this._container.width();
            this._dropdownmenu.width(width);
        }.bind(this));
        
        this._container.on('shown.bs.dropdown', function () {
            filter.focus();
            this.update();
        }.bind(this));
        
        filter.keyup(function () {
            this._filter = filter.val();
            this.trigger('webexpress.ui.change.filter', this._filter);
            this.update();
        }.bind(this));

        reset.click(function () {
            this.value = null;
        }.bind(this));

        this._placeholder = placeholder;
        this._dropdownmenu.append(filter);
        this._dropdownmenu.append(this._dropdownoptions);

        if (emptyvalue == true) {
            expand.append(reset);
        }
        expand.append(toggle);
        
        dropdown.append(this._selection);
        dropdown.append(expand);

        this._container.append(dropdown);
        this._container.append(this._dropdownmenu);
        this._container.append(this._hidden);
        
        this.value = null;
    }
    
    /**
     * Aktualisierung des Steuerelementes
     */
    update() {
        this._dropdownoptions.children().remove();

        this._options.forEach(function (options) {
            let id = options.ID !== undefined && options.ID != null ? options.ID : null;
            let label = options.Label !== undefined && options.Label != null ? options.Label : null;
            
            if (id == null && (label == null || label == '-')) {
                let li = $("<li class='dropdown-divider'/>");
                this._dropdownoptions.append(li);
            } else if (id == null && label != null) {
                let li = $("<li class='dropdown-header'>" + label + "</li>"); 
                this._dropdownoptions.append(li);
            } else {
                let description = options.Description !== undefined && options.Description != null && options.Description.length > 0 ? options.Description : null;
                let image = options.Image !== undefined && options.Image != null ? options.Image : null;
                let color = options.Color !== undefined && options.Color != null ? options.Color : 'text-dark';
                let li = $("<li class='dropdown-item'/>"); 
                let a = $("<a class='link " + options.Color + "' href='javascript:void(0)'>" + options.Label + "</a>");
                let p = $("<p class='small text-muted'>" + description + "</p>");
                
                if (image != null) {
                    let span = $("<span/>");
                    let box = $("<span/>");
                    let img = $("<img src='" + image + "' alt=''/>");

                    box.append(img);
                    box.append(a);
                    span.append(box);
                    if (description != null) {
                        span.append(p);
                    }
                    li.append(span);
                } else {
                    li.append(a);
                    if (description != null) {
                        li.append(p);
                    }
                }
                
                if (id == this.value) {
                    li.addClass("active");
                    a.removeClass();
                    a.addClass("link text-white");
                    p.removeClass();
                    p.addClass("small text-white");
                }

                li.click(function () {
                    this.value = options.ID;
                }.bind(this));
                 
                if (options.Label.toLowerCase().startsWith(this._filter.toLowerCase())) {
                    this._dropdownoptions.append(li);
                }
            }
        }.bind(this));

        this._selection.children().remove();

        if (this._value !== undefined && this._value != null) {
            let options = this._options.find(elem => elem.ID == this._value);
            if (options != null) {
                let label = options.Label !== undefined && options.Label != null ? options.Label : null;
                let image = options.Image !== undefined && options.Image != null ? options.Image : null;
                let color = options.Color !== undefined && options.Color != null ? options.Color : 'text-dark';
                let a = $("<a class='link " + color + "' href='javascript:void(0)'>" + options.Label + "</a>");

                if (image != null) {
                    let span = $("<span/>");
                    let img = $("<img src='" + image + "' alt=''/>");

                    span.append(img);
                    span.append(a);
                    this._selection.append(span);
                } else {
                    this._selection.append($("<span>" + label + "</span>"));
                }
            }
        } else {
            if (this._placeholder != null) {
                this._selection.append($("<span class='text-muted'>" + this._placeholder + "</span>"));
            }
        }
    }
    
    /**
     * Gibt die Optionen zurück
     */
    get options() {
        return this._options;
    }
    
    /**
     * Setzt die Optionen
     * @param data Ein Array mit ObjektIDs
     */
    set options(options) {
        this._options = options;
        
        this.update();
    }
    
    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get value() {
        return this._value;
    }
    
    /**
     * Setzt die ausgewählten Optionen
     * @param value Die ID des ausgewählten Eintrages
     */
    set value(value) {
        if (this._value != value) {
            this.trigger('webexpress.ui.change.value', value);
        }
        
        this._value = value;

        this.update();

        if (this._value !== undefined && this._value != null) {
            this._hidden.val(this._value);
        } else {
            this._hidden.val(null);
        }
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}

/**
 * Ein Auswahlfeld
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.filter mit Parameter filter
 * - webexpress.ui.change.value mit Parameter value
 */
class selectionMultiCtrl extends events {
    _container = $("<span class='selection-multi form-control' />");
    _selection = $("<ul/>");
    _hidden = $("<input type='hidden'/>");
    _dropdownmenu = $("<div class='dropdown-menu'/>");
    _dropdownoptions = $("<ul/>");
    _options = [];
    _values = []; // Arry mit ausgewählten IDs aus _options
    _filter = '';
    _placeholder = null;

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - HasEmptyValue Bestimmt, ob Nullwerte zugelassen sind
     */
    constructor(settings) {
        let id = settings.ID;
        let name = settings.Name;
        let css = settings.CSS;
        let placeholder = settings.Placeholder !== undefined ? settings.Placeholder : null;
        let emptyvalue = settings.HasEmptyValue !== undefined ? settings.HasEmptyValue : false;

        let dropdown = $("<span data-bs-toggle='dropdown' aria-expanded='false'/>");
        let expand = $("<a class='fas fa-angle-down' href='#'/>");
        let filter = $("<input type='text'/>");

        super();

        if (id !== undefined) {
            this._container.attr("id", id);
        }

        if (css !== undefined) {
            this._container.addClass(css);
        }

        if (name !== undefined && name != null) {
            this._hidden.attr("name", name);
        }
        
        this._container.on('show.bs.dropdown', function () {
            let width = this._container.width();
            this._dropdownmenu.width(width);
        }.bind(this));
        
        this._container.on('shown.bs.dropdown', function () {
            filter.focus();
            this.update();
        }.bind(this));
        
        filter.keyup(function () {
            this._filter = filter.val();
            this.trigger('webexpress.ui.change.filter', this._filter);
            this.update();
        }.bind(this));

        this._placeholder = placeholder;
        this._dropdownmenu.append(filter);
        this._dropdownmenu.append(this._dropdownoptions);
        
        dropdown.append(this._selection);
        dropdown.append(expand);

        this._container.append(dropdown);
        this._container.append(this._dropdownmenu);
        this._container.append(this._hidden);
        
        this.value = [];
    }
    
    /**
     * Aktualisierung des Steuerelementes
     */
    update() {
        this._dropdownoptions.children().remove();

        this._options.forEach(function (option) {
            let id = option.ID !== undefined && option.ID != null ? option.ID : null;
            let label = option.Label !== undefined && option.Label != null ? option.Label : null;
            
            if (id == null && (label == null || label == '-')) {
                let li = $("<li class='dropdown-divider'/>");
                this._dropdownoptions.append(li);
            } else if (id == null && label != null) {
                let li = $("<li class='dropdown-header'>" + label + "</li>"); 
                this._dropdownoptions.append(li);
            } else {
                let description = option.Description !== undefined && option.Description != null && option.Description.length > 0 ? option.Description : null;
                let image = option.Image !== undefined && option.Image != null ? option.Image : null;
                let color = option.Color !== undefined && option.Color != null ? option.Color : 'text-dark';
                let li = $("<li class='dropdown-item'/>"); 
                let a = $("<a class='link " + option.Color + "' href='javascript:void(0)'>" + option.Label + "</a>");
                let p = $("<p class='small text-muted'>" + description + "</p>");
                
                if (image != null) {
                    let box = $("<span/>");
                    let span = $("<span/>");
                    let img = $("<img src='" + image + "' alt=''/>");

                    box.append(img);
                    box.append(a);
                    span.append(box);
                    if (description != null) {
                        span.append(p);
                    }
                    li.append(span);
                } else {
                    li.append(a);
                    if (description != null) {
                        li.append(p);
                    }
                }
                
                if (id == this.value) {
                    li.addClass("active");
                    a.removeClass();
                    a.addClass("link text-white");
                    p.removeClass();
                    p.addClass("small text-white");
                }

                li.click(function () {
                    if (!this._values.includes(option.ID)) {
                        this.value.push(option.ID);
                    }
                    this.update();
                }.bind(this));
                 
                if (option.Label.toLowerCase().startsWith(this._filter.toLowerCase())) {
                    this._dropdownoptions.append(li);
                }
            }
        }.bind(this));
        
        this._selection.children().remove();
        this._values.forEach(function (value) {
            let option = this._options.find(elem => elem.ID == value);
            if (option != null) {
                let label = option.Label !== undefined && option.Label != null ? option.Label : null;
                let image = option.Image !== undefined && option.Image != null ? option.Image : null;
                let color = option.Color !== undefined && option.Color != null ? option.Color : 'text-dark';
                let a = $("<a class='link " + color + "' href='javascript:void(0)'>" + option.Label + "</a>");
                let close = $("<a class='fas fa-times' href='#'/>");
                let li = $("<li/>");
                
                close.click(function () {
                    this.value = this._values.filter(item => item !== value);
                }.bind(this));
                
                if (image != null) {
                    let img = $("<img src='" + image + "' alt=''/>");
                    li.append(img);
                    li.append(a);
                    li.append(close);
                    this._selection.append(li);
                } else {
                    li.append($("<span>" + label + "</span>"));
                    li.append(close);
                    this._selection.append(li);
                }
            }
        }.bind(this));
    }
    
    /**
     * Gibt die Optionen zurück
     */
    get options() {
        return this._options;
    }
    
    /**
     * Setzt die Optionen
     * @param data Ein Array mit ObjektIDs
     */
    set options(options) {
        this._options = options;
        
        this.update();
    }
    
    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get value() {
        return this._values;
    }
    
    /**
     * Setzt die ausgewählten Optionen
     * @param values Die ID des ausgewählten Eintrages
     */
    set value(values) {
        if (this._values != values) {
            this.trigger('webexpress.ui.change.value', values);
        }
        
        this._values = values;

        this.update();
        this._hidden.val(this._values.map(element => element).join(';'));
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}

/**
 * Base on https://github.com/kurtobando/simple-tags
 */
 /*
function Tags(element, id, listOfTags)
{
    let arrayOfList = listOfTags;
    let DOMParent = document.querySelector(element);
    let DOMList;
    let DOMInput;
    let hidden;
    
    function DOMCreate()
    {
        let ul = document.createElement('ul');
        let li = document.createElement('li');
        let input = document.createElement('input');
        
        hidden = document.createElement('input');
        hidden.setAttribute("type", "hidden");
        hidden.setAttribute("name", id);
        
        DOMParent.appendChild(ul);
        DOMParent.appendChild(hidden);
        DOMParent.appendChild(input);
        DOMList = DOMParent.firstElementChild;
        DOMInput = DOMParent.lastElementChild;
    }

    function DOMRender()
    {
        DOMList.innerHTML = '';
        arrayOfList.forEach
        (
            function(currentValue, index)
            {   
                let li=document.createElement('li');
                li.innerHTML = "".concat(currentValue.toLowerCase(), "<a>&times;</a>");
                li.querySelector('a').addEventListener('click', function()
                {   
                    onDelete(index);
                    
                    return false;
                });
                
                DOMList.appendChild(li);
            }
        );

        hidden.setAttribute("value", arrayOfList.map(element => element.toLowerCase()).join(';'));
    }

    function onKeyUp()
    {
        DOMInput.addEventListener('keyup', function(event)
        {
            let text = this.value;
            if(text.endsWith(',') || text.endsWith(';') || text.endsWith(' '))
            {
                let replace = text.replace(',', '').replace(';', '').replace(' ', '').toLowerCase();
                if(replace != '')
                {
                    arrayOfList.push(replace);
                }

                this.value = '';
            }

            DOMRender();
        });
    }
    
    function onDelete(id)
    {
        arrayOfList = arrayOfList.filter(function(currentValue, index)
        {
            if(index == id)
            {
                return false;
            }

            return currentValue;
        });
        
        DOMRender();
    }

    DOMCreate();
    DOMRender();
    onKeyUp();
}*/