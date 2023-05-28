/**
 * Ein Auswahlfeld
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.filter mit Parameter filter
 * - webexpress.ui.change.value mit Parameter value
 */
webexpress.ui.selectionCtrl = class extends webexpress.ui.events {
    _container = $("<span class='selection form-control' />");
    _selection = $("<ul/>");
    _hidden = $("<input type='hidden'/>");
    _dropdownmenu = $("<div class='dropdown-menu'/>");
    _dropdownoptions = $("<ul/>");
    _filter = $("<input type='text'/>");
    _options = [];
    _values = []; // Arry mit ausgewählten Ids aus _options
    _placeholder = null;
    _multiselect = false;
    _optionfilter = function (x, y) { return x?.toLowerCase().startsWith(y?.toLowerCase()); };

    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die Id des Steuerelements
     *        - css CSS-Klasse zur Gestaltung des Steuerelementes
     *        - placeholder Der Platzhaltertext
     *        - multiselect Allows you to select multiple items.
     */
    constructor(settings) {
        let id = settings.id;
        let name = settings.name;
        let css = settings.css;
        let multiselect = settings.multiselect;
        let placeholder = settings.placeholder !== undefined ? settings.placeholder : null;
                
        let dropdown = $("<span data-bs-toggle='dropdown' aria-expanded='false'/>");
        let expand = $("<a class='fas fa-angle-down' href='#'/>");

        super();

        this._container.attr("id", id ?? "");

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
            let id = option.id ?? null;
            let label = option.label ?? null;
            if (!this._values.includes(id)) {
                if (id == null && (label == null || label == '-')) {
                    let li = $("<li class='dropdown-divider'/>");
                    this._dropdownoptions.append(li);
                } else if (id == null && label != null) {
                    let li = $("<li class='dropdown-header'>" + label + "</li>");
                    this._dropdownoptions.append(li);
                } else {
                    let description = option.description != null && option.description.length > 0 ? option.description : null;
                    let image = option.image ?? null;
                    let color = option.color ?? 'text-dark';
                    let instruction = option.instruction != null ? "<small>(" + option.instruction + ")</small>": "";
                    let li = $("<li class='dropdown-item'/>");
                    let a = $("<a class='link " + color + "' href='javascript:void(0)'>" + option.label + "</a>" + instruction);
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

                        if (!this._values.includes(option.id)) {
                            let value = this.value.slice();
                            value.push(option.id);
                            this.value = value;
                            this._filter.val("");
                        }
                        this.update();
                    }.bind(this));

                    if (this._optionfilter(option.label, this._filter.val())) {
                        this._dropdownoptions.append(li);
                    }
                }
            }
        }.bind(this));

        this._selection.children("li").remove();
        this._values.forEach(function (value) {
            let option = this._options.find(elem => elem.id == value);
            if (option != null) {
                let label = option.label ?? null;
                let image = option.image ?? null;
                let color = option.color ?? 'text-dark';
                let a = $("<a class='link " + color + "' href='javascript:void(0)'>" + option.label + "</a>");
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
     * @param data Ein Array mit ObjektIds {id, label, description, image, color, instruction}
     */
    set options(options) {
        if (options != null) {
            // selektierte Optionen ermitteln
            let selectedOptions = this.selectedOptions;
            // entferne die selektierten Optionen, wenn diese in den übergebenen neuen Optionen enthalten sind
            selectedOptions = selectedOptions.filter(elem => !options.some(o => o.Id === elem.id));
            // Vereinige die selectierten und die neuen Optionen
            this._options = [...new Set([...options, ...selectedOptions])];
        } else {
            this._options = [];
        }
        this.update();
    }

    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get selectedOptions() {
        return this._options.filter(elem => this.value.includes(elem.id));
    }

    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get value() {
        return this._values;
    }

    /**
     * Setzt die ausgewählten Optionen
     * @param values Die Id des ausgewählten Eintrages
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