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

            this._values = values;

            this.update();
            this._hidden.val(this._values.map(element => element).join(';'));

            this.trigger('webexpress.ui.change.value', values);
        }
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}