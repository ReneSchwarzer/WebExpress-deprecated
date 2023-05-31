/**
 * Ein Container zum verstecken von Inhalten
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.visibility 
 */
webexpress.ui.expandCtrl = class extends webexpress.ui.events {
    _container = $("<span class='expand'>");
    _content = $("<div/>");
    _expandicon = $("<a class='expand-angle' href='#'/>");
    _expand = true;

    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Returns or sets the id. des Steuerelements
     *        - css CSS-Klasse zur Gestaltung des Steuerelementes
     *        - header Der Überschriftsext
     */
    constructor(settings) {
        let id = settings.id;
        let css = settings.css;
        let header = settings.header != null ? settings.header : "";
        
        let expandheader = $("<span class='text-primary' aria-label='" + header + "'>" + header + "</span>");
        
        super();
        
        this._container.attr("id", id ?? "");
        
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