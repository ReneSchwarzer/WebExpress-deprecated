/**
 * Ein Auswahlfeld
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.filter mit Parameter filter
 * - webexpress.ui.change.value mit Parameter value
 */
class selectionCtrl extends events {
    _container = $("<span class='selection form-control' />");
    _selection = $("<ul/>");
    _hidden = $("<input type='hidden'/>");
    _dropdownmenu = $("<div class='dropdown-menu'/>");
    _dropdownoptions = $("<ul/>");
    _filter = $("<input type='text'/>");
    _options = [];
    _values = []; // Arry mit ausgewählten IDs aus _options
    _placeholder = null;
    _multiselect = false;
    _optionfilter = function (x, y) { return x?.toLowerCase().startsWith(y?.toLowerCase()); };

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - MultiSelect Erlaubt die Auswahl mehrerer Elmente
     */
    constructor(settings) {
        let id = settings.ID;
        let name = settings.Name;
        let css = settings.CSS;
        let multiselect = settings.MultiSelect;
        let placeholder = settings.Placeholder !== undefined ? settings.Placeholder : null;
                
        let dropdown = $("<span data-bs-toggle='dropdown' aria-expanded='false'/>");
        let expand = $("<a class='fas fa-angle-down' href='#'/>");

        super();

        if (id != null) {
            this._container.attr("id", id);
        }

        if (css != null) {
            this._container.addClass(css);
        }

        if (name != null) {
            this._hidden.attr("name", name);
        }

        if (multiselect != null) {
            this._multiselect = multiselect;
        }

        this._container.on('show.bs.dropdown', function () {
            let width = this._container.width();
            this._dropdownmenu.width(width);
        }.bind(this));

        this._container.on('shown.bs.dropdown', function () {
            this._filter.focus();
            this.update();
        }.bind(this));
        
        this._filter.keyup(function (e) {
            let filter = this._filter.val();
            e.stopPropagation();
            this.trigger('webexpress.ui.change.filter', filter !== undefined || filter != null ? filter : "");
            this.update();
            if (this._dropdownmenu.is(":hidden")) {
                dropdown.dropdown('toggle');
            }
        }.bind(this));

        this._placeholder = placeholder;

        this._dropdownmenu.append(this._filter);
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
            let id = option.ID != null ? option.ID : null;
            let label = option.Label != null ? option.Label : null;
            if (!this._values.includes(id)) {
                if (id == null && (label == null || label == '-')) {
                    let li = $("<li class='dropdown-divider'/>");
                    this._dropdownoptions.append(li);
                } else if (id == null && label != null) {
                    let li = $("<li class='dropdown-header'>" + label + "</li>");
                    this._dropdownoptions.append(li);
                } else {
                    let description = option.Description != null && option.Description.length > 0 ? option.Description : null;
                    let image = option.Image != null ? option.Image : null;
                    let color = option.Color != null ? option.Color : 'text-dark';
                    let instruction = option.Instruction !== undefined && option.Instruction != null ? "<small>(" + option.Instruction + ")</small>": "";
                    let li = $("<li class='dropdown-item'/>");
                    let a = $("<a class='link " + color + "' href='javascript:void(0)'>" + option.Label + "</a>" + instruction);
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

                    li.click(function () {
                        if (!this._multiselect) {
                            this.value = [];
                        }

                        if (!this._values.includes(option.ID)) {
                            let value = this.value.slice();
                            value.push(option.ID);
                            this.value = value;
                            this._filter.val("");
                        }
                        this.update();
                    }.bind(this));

                    if (this._optionfilter(option.Label, this._filter.val())) {
                        this._dropdownoptions.append(li);
                    }
                }
            }
        }.bind(this));

        this._selection.children("li").remove();
        this._values.forEach(function (value) {
            let option = this._options.find(elem => elem.ID == value);
            if (option != null) {
                let label = option.Label != null ? option.Label : null;
                let image = option.Image != null ? option.Image : null;
                let color = option.Color != null ? option.Color : 'text-dark';
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
     * @param data Ein Array mit ObjektIDs {ID, Label, Description, Image, Color, Instruction}
     */
    set options(options) {
        // selektierte Optionen ermitteln
        let selectedOptions = this.selectedOptions;
        // entferne die selektierten Optionen, wenn diese in den übergebenen neuen Optionen enthalten sind
        selectedOptions = selectedOptions.filter(elem => !options.some(o => o.ID === elem.ID));
        // Vereinige die selectierten und die neuen Optionen
        this._options = [...new Set([...options, ...selectedOptions])];

        this.update();
    }

    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get selectedOptions() {
        return this._options.filter(elem => this.value.includes(elem.ID));
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