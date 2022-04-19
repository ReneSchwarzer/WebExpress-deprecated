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