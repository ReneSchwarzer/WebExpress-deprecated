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