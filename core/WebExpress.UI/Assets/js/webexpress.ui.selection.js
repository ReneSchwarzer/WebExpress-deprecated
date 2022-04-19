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