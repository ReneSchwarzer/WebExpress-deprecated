/**
 * Seitennavigationssteuerelement
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.page mit Parameter page
 */
webexpress.ui.paginationCtrl = class extends webexpress.ui.events {
    _container = $("<ul class='pagination'/>");
    _currentpage = 0;
    _pagecount = 0;
    _css = "";

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *                - id Die ID des Steuerelements
     *                - css CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(settings) {
        super();
        
        let id = settings.id;
        this._css = settings.css;

        this._container.attr("id", id ?? "");
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