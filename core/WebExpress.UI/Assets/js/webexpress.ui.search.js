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