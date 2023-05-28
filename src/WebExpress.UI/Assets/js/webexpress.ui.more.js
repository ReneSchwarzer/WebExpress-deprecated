/**
 * Ein Dropdown, welches erweiterte Funktionen (Links) anbietet
 */
webexpress.ui.moreCtrl = class {
    _container = $("<div class='dropdown'/>");

    /**
     * Constructor
     * @param options Die Menüeinräge Array von { css: "", icon: "", color: "", label: "", url: "", onclick: "", item: null, disabled: false}
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die Id des Steuerelements
     *        - css CSS-Klasse zur Gestaltung des Steuerelementes
     *        - menucss CSS-Klasse zur Gestaltung des Popupmenüs
     *        - label The text.
     *        - icon Die Icon-Klasse des Steuerelements
     */
    constructor(options, settings) {
        let id = settings.id;
        let css = settings.css;
        let menuCSS = settings.menucss;
        let label = settings.label != null ? settings.label : "";
        let icon = settings.icon != null ? settings.icon : "fas fa-ellipsis-h";

        let button = $("<button class='btn' type='button' data-bs-toggle='dropdown' aria-expanded='false'><i class='" + icon + " " + (label != "" ? "me-2" : "") + "'></i><span>" + label + "</span></button>");
        let ul = $("<ul class='dropdown-menu'/>");

        if (menuCSS != null) {
            ul.addClass(menuCSS);
        }

        options.forEach(function (option) {
            let label = option.label;
            let css = option.css ?? "dropdown-item";
            let icon = option.icon;
            let color = option.color;
            let item = option.item;
            let disabled = option.disabled ?? "return false";
            
            let url = option.url != null ? option.url : "#";
            let onclick = option.onclick;

            let disabledFunction = Function("item", disabled == true ? "return true;" : disabled == false ? "return false" : disabled);
            disabled = disabledFunction(item) ?? false;

            let li = $("<li/>");

            li.addClass(css);

            if (css == "dropdown-item") {
                if (disabled == false) {
                    let a = $("<a class='link " + color + "' href='#'/>");
                    if (icon != null) {
                        let span = $("<span class='me-2 " + icon + "'/>");
                        a.append(span);
                    }
                    if (css != null) {
                        li.addClass(css);
                    }
                    if (onclick != null) {
                        let func = Function("option", "item", onclick);
                        a.click(function (e) { func(option, item); });
                    }
                    a.append($("<span href='" + url + "'>" + label + "</span>"));
                    li.append(a);
                } else {
                    let p = $("<span class='text-muted'/>");
                    if (icon != null) {
                        let span = $("<span class='me-2 " + icon + "'/>");
                        p.append(span);
                    }
                    p.append(label);
                    li.append(p);
                    li.addClass("disabled")
                }
            }
            else if (css == "dropdown-header") {
                if (icon != null) {
                    let span = $("<span class='me-2 " + icon + "'/>");
                    li.append(span);
                }

                li.append($("<span>" + label + "</span>"));
            }
            else if (css == "dropdown-divider") {

            }

            ul.append(li);
        });

        this._container.attr("id", id ?? "");
        if (css != null) {
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