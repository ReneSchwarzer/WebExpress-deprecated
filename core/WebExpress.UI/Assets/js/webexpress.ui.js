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
     * @param options Optionen zur Gestaltung des Steuerelementes
     *                - ID Die ID des Steuerelements
     *                - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(options) {
        super();
        
        this._id = options.ID;
        this._css = options.CSS;
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

        if (this._id !== undefined) {
            this._container.id = id;
        }

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
 * Ein Feld, inden Suchbefehle eingegeben werden können.
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.filter mit Parameter filter
 */
class searchCtrl extends events {
    _container = $("<span class='search form-control'>");

    /**
     * Konstruktor
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - Icon Die Icon-Klasse des Suchsymbols
     */
    constructor(options) { 
        let id = options.ID;
        let css = options.CSS;
        let placeholder = options.Placeholder !== undefined ? options.Placeholder : "";
        let icon = options.Icon !== undefined ? options.Icon : "fas fa-search";

        let searchicon = $("<label><i class='" + icon + "'/></label>");
        let searchinput = $("<input type='text' class='' placeholder='" + placeholder + "' aria-label='" + placeholder + "'/>");
        let searchappend = $("<span><i class='fas fa-times'/><span>");
        
        super();

        if (id !== undefined) {
            container.id = id;
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
     * @param items Die Menüeinräge Array von { css: "", icon: "", color: "", label: "", url: "", onclick: ""}
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - MenuCSS CSS-Klasse zur Gestaltung des Popupmenüs
     *        - Label Der Text
     *        - Icon Die Icon-Klasse des Steuerelements
     */
    constructor(items, options) {
        let id = options.ID;
        let css = options.CSS;
        let menuCSS = options.MenuCSS;
        let label = options.Label !== undefined ? options.Label : "";
        let icon = options.Icon !== undefined ? options.Icon : "fas fa-ellipsis-h";

        let button = $("<button class='btn' type='button' data-bs-toggle='dropdown' aria-expanded='false'><i class='" + icon + " " + (label != "" ? "me-2" : "") + "'></i><span>" + label + "</span></button>");
        let ul = $("<ul class='dropdown-menu'/>");

        if (menuCSS !== undefined) {
            ul.addClass(menuCSS);
        }

        items.forEach(function (item) {
            let css = item.CSS !== undefined && item.CSS != null ? item.CSS : "dropdown-item";
            let icon = item.Icon;
            let color = item.Color;
            let label = item.Label;
            let url = item.Url !== undefined ? item.Url : "#";
            let onclick = item.OnClick;

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
        let id = options.ID;
        let css = options.CSS;

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
    _items = [];
    _values = [];
    _selectedItems = [];
    _availableItems = [];
    
    /**
     * Konstruktor
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - Name Der Steuerelementenname
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Header Überschrift { Selected, Available }
     *        - Buttons Schaltflächenbeschriftung { ToSelectedAll, ToSelected, ToAvailable, ToAvailableAll }
     */
    constructor(options) {
        super();

        this._items = items;
        
        let id = options.ID;
        let name = options.Name;
        let css = options.CSS;
        let header = options.Header;
        let buttons = options.Buttons;
        let selectedContainer = $("<div class='selection-move-list'/>");
        let selectedHeader = $("<p class='text-muted'>" + header.Selected + "</p>");
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
        this.value = this._items.map(element => element.ID);
        this._selectedItems = [];
        this._availableItems = [];

		this.update();
	}
    
    /**
     * Verschiebt ein einzelnen Eintrag nach links (selected)
     */
	moveToSelected() {
        this.value = this._values.concat(this._availableItems.map(element => element.ID));
        this._selectedItems = [];
		this._availableItems = [];
        
        this.update();
	}

    /**
     * Verschiebe alle Einträge nach rechts (available)
     */
	moveToAvailableAll() {
        this.value = [];
        this._selectedItems = [];
        this._availableItems = [];

		this.update();
	}

    /**
     * Verschiebt ein einzelnen Eintrag nach rechts (available)
     */
	moveToAvailable() {
        this.value = this._values.filter(b => !this._selectedItems.map(element => element.ID).includes(b));
        this._selectedItems = [];
		this._availableItems = [];
        
        this.update();
	}
   
    /**
     * Aktualisierung der Steuerelementes
     */
    update() {
        let values = this._values !== undefined && this._values != null ? this._values : [];
		let comparison = (a, b) => a === b.ID;
		let relativeComplement = this._items.filter(b => values.every(a => !comparison(a, b)));
        let intersection = this._items.filter(b => values.includes(b.ID));
        
        this._selectedList.children().remove();
        this._availableList.children().remove();

		if (relativeComplement.length == 0) {
			this._buttonToSelectedAll.addClass("disabled");
			this._buttonToSelectedAll.prop("disabled", true);
		} else {
			this._buttonToSelectedAll.removeClass("disabled");
			this._buttonToSelectedAll.prop("disabled", false);
		}

		if (this._availableItems == null || this._availableItems.length == 0) {
			this._buttonToSelected.addClass("disabled");
			this._buttonToSelected.prop("disabled", true);
		} else {
			this._buttonToSelected.removeClass("disabled");
			this._buttonToSelected.prop("disabled", false);
		}

		if (values.length == 0) {
			this._buttonToAvailableAll.addClass("disabled");
			this._buttonToAvailableAll.prop("disabled", true);
		} else {
			this._buttonToAvailableAll.removeClass("disabled");
			this._buttonToAvailableAll.prop("disabled", false);
		}

		if (this._selectedItems == null || this._selectedItems.length == 0) {
			this._buttonToAvailable.addClass("disabled");
			this._buttonToAvailable.prop("disabled", true);
		} else {
			this._buttonToAvailable.removeClass("disabled");
			this._buttonToAvailable.prop("disabled", false);
		}

		intersection.forEach(function(currentValue) {	
            let li = $("<li class='list-group-item'/>");
            let img = $("<img title='' src='" + currentValue.Image + "'/>");
            let icon = $("<i class='text-primary " + currentValue.Icon + "'/>");
            let a = $("<a class='link' href='javascript:void(0)'>" + "".concat(currentValue.Label) + "</a>");
            if (currentValue.Icon !== undefined && currentValue.Icon != null) {
                li.append(icon);
            }
            if (currentValue.Image !== undefined && currentValue.Image != null) {
                li.append(img);
            }
            li.append(a);
            
            li.click(function() {	
                if (event.ctrlKey) {
                     if (!this._selectedItems.some(elem => elem === currentValue)) {
                        this._selectedItems.push(currentValue);
                    } else {
                        this._selectedItems = this._selectedItems.filter(elem => elem !== currentValue);
                    }
                    this._availableItems = [];
                } else {
                    this._selectedItems = [];
                    this._selectedItems.push(currentValue);
                    this._availableItems = [];
                }
                this.update();
                a.focus();
            }.bind(this, a)).dblclick(function() {	
                this._selectedItems = [];
                this._selectedItems.push(currentValue);
                this._availableItems = [];

                this.moveToAvailable();
                a.focus();
            }.bind(this, a)).keyup(function() {	
                if (event.keyCode === 32) {
                    if (!this._selectedItems.some(elem => elem === currentValue)) {
                        this._selectedItems.push(currentValue);
                    } else {
                        this._selectedItems = this._selectedItems.filter(elem => elem !== currentValue);
                    }
                    this._availableItems = [];
                    this.update();
                    a.focus();
                }
            }.bind(this, a));
            
            if (this._selectedItems.some(elem => elem === currentValue)) {
                li.addClass("bg-primary");
                a.addClass("text-white");
            }

            this._selectedList.append(li);
		}.bind(this));

		relativeComplement.forEach(function(currentValue) {	
            let li = $("<li class='list-group-item'/>");
            let img = $("<img title='' src='" + currentValue.Image + "'/>");
            let icon = $("<i class='text-primary " + currentValue.Icon + "'/>");
            let a = $("<a class='link' href='javascript:void(0)'>" + "".concat(currentValue.Label) + "</a>");
            if (currentValue.Icon !== undefined && currentValue.Icon != null) {
                li.append(icon);
            }
            if (currentValue.Image !== undefined && currentValue.Image != null) {
                li.append(img);
            }
            li.append(a);
            
            li.click(function() {	
                if (event.ctrlKey) {
                    if (!this._availableItems.some(elem => elem === currentValue)) {
                        this._availableItems.push(currentValue);
                    } else {
                        this._availableItems = this._availableItems.filter(elem => elem !== currentValue);
                    }
                    this._selectedItems = [];
                } else {
                    this._selectedItems = [];
                    this._availableItems = [];
                    this._availableItems.push(currentValue);
                }
                                
                this.update();
                a.focus();
            }.bind(this, a)).dblclick(function() {	
                this._selectedItems = [];
                this._availableItems = [];
                this._availableItems.push(currentValue);

                this.moveToSelected();
                a.focus();
            }.bind(this, a)).keyup(function() {	
                if (event.keyCode === 32) {
                    if (!this._availableItems.some(elem => elem === currentValue)) {
                        this._availableItems.push(currentValue);
                    } else {
                        this._availableItems = this._availableItems.filter(elem => elem !== currentValue);
                    }
                    this._selectedItems = [];
                    this.update();
                    a.focus();
                }
            }.bind(this, a));
               
            if (this._availableItems.some(elem => elem === currentValue)) {
                li.addClass("bg-primary");
                a.addClass("text-white");
            }

            this._availableList.append(li);
		}.bind(this));
    }

    /**
     * Gibt alle Optionen zurück
     */
    get items() {
        return this._items;
    }

    /**
     * Setzt die Optionen
     * @param value Ein Array mit Optionen { ID, Label, Icon, Image }
     */
    set items(value) {
        this._items = items;

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
    _container = $("<span class='selection input-group'/>");
    _selection = $("<span class='form-control' data-bs-toggle='dropdown' aria-expanded='false' readonly/>");
    _hidden = $("<input type='hidden'/>");
    _dropdown = $("<div class='dropdown-menu'/>");
    _dropdownitems = $("<ul/>");
    _items = [];
    _value = null;
    _filter = '';
    _placeholder = null;

    /**
     * Konstruktor
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     */
    constructor(options) { 
        let id = options.ID;
        let name = options.Name;
        let css = options.CSS;
        let placeholder = options.Placeholder !== undefined ? options.Placeholder : null;
        let emptyvalue = options.HasEmptyValue !== undefined ? options.HasEmptyValue : false;

        let reset = $("<button  type='button' class='btn btn-light'><i class='fas fa-times'/></button>");
        let toggle = $("<button type='button' class='btn btn-light dropdown-toggle selection-last' data-bs-toggle='dropdown' aria-expanded='false'/>");
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
       
        $(window).resize(function() {
            let width = this._container.width();
            this._dropdown.width(width);
        }.bind(this));
        
        this._container.on('show.bs.dropdown', function () {
            let width = this._container.width();
            this._dropdown.width(width);
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
        this._dropdown.append(filter);
        this._dropdown.append(this._dropdownitems);
        
        this._container.append(this._selection);
        if (emptyvalue == true) {
            this._container.append(reset);
        }
        this._container.append(toggle);
        this._container.append(this._dropdown);
        this._container.append(this._hidden);
        
        this.value = null;
    }
    
    /**
     * Aktualisierung der Steuerelementes
     */
    update() {
        this._dropdownitems.children().remove();

        this._items.forEach(function (item) {
            let id = item.ID !== undefined && item.ID != null ? item.ID : null;
            let label = item.Label !== undefined && item.Label != null ? item.Label : null;
            
            if (id == null && (label == null || label == '-')) {
                let li = $("li class='dropdown-divider'/>");
                this._dropdownitems.append(li);
            } else if (id == null && label != null) {
                let li = $("<li class='dropdown-header'>" + label + "</li>"); 
                this._dropdownitems.append(li);
            } else {
                let image = item.Image !== undefined && item.Image != null ? item.Image : null;
                let color = item.Color !== undefined && item.Color != null ? item.Color : 'text-dark';
                let li = $("<li class='dropdown-item'/>"); 
                let a = $("<a class='link " + item.Color + "' href='javascript:void(0)'>" + item.Label + "</a>");
                
                if (image != null) {
                    let span = $("<span/>");
                    let img = $("<img src='" + image + "' alt=''/>");

                    span.append(img);
                    span.append(a);
                    li.append(span);
                } else {
                    li.append(a);
                }
                
                if (id == this.value) {
                    li.addClass("active");
                    a.removeClass();
                    a.addClass("link text-white");
                }

                li.click(function () {
                    this.value = item.ID;
                }.bind(this));
                 
                if (item.Label.toLowerCase().startsWith(this._filter.toLowerCase())) {
                    this._dropdownitems.append(li);
                }
            }
        }.bind(this));
    }
    
    /**
     * Gibt Items zurück
     */
    get items() {
        return this._items;
    }
    
    /**
     * Setzt die Items
     * @param data Ein Array mit ObjektIDs
     */
    set items(items) {
        this._items = items;
        
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

        this._selection.children().remove();

        if (this._value !== undefined && this._value != null && this._value.length > 0) {
            this._hidden.val(this._value);
            this._selection.children().remove();  
            let item = this._items.find(elem => elem.ID == this._value);
            if (item != null) {
                let label = item.Label !== undefined && item.Label != null ? item.Label : null;
                let image = item.Image !== undefined && item.Image != null ? item.Image : null;
                let color = item.Color !== undefined && item.Color != null ? item.Color : 'text-dark';
                let a = $("<a class='link " + color + "' href='javascript:void(0)'>" + item.Label + "</a>");
                
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
            this._hidden.val(null);
            if (this._placeholder != null) {
                this._selection.append($("<span class='text-muted'>" + this._placeholder + "</span>"));    
            }
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