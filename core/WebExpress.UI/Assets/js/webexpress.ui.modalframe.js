/**
 * Ein modulares Fenster/ Dialog
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.close
 */
webexpress.ui.modalFrameCtrl = class extends webexpress.ui.events {
    _uri = null;
    _container = $("<div class='modal modalframe fade' data-bs-backdrop='static' data-bs-keyboard='false' tabindex='-1' aria-hidden='true'></div>");
    
    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - Close Der Name der Schließenschaltfläche
     */
    constructor(settings) {
        super();
        
        let id = settings.ID;
        let close = settings.Close ?? "Close";
        let uri = settings.Uri ?? "";
        let dialog = $("<div class='modal-dialog'></div>");
        let content = $("<div class='modal-content'></div>");
        let header = $("<div class='modal-header'></div>");
        
        this._container.attr("id", id ?? "");
        this._uri = uri;
        
        dialog.append(content);
        this._container.append(dialog);
        
        let update = function(doc) {  
            $("form", doc).submit(function(event) {

                event.preventDefault(); 
                $(this).unbind('submit').submit(); 
                
            }.bind(this));
        
            $("header", doc).addClass("modal-header");
            $("header", doc).append($("<button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='" + close + "'></button>"));
            $("main", doc).addClass("modal-body");
            $("footer", doc).addClass("modal-footer");
            content.append($("form", doc));
            
            
            
        }.bind(this);
        
        $(document).ready(function() {
            $.get(this._uri, function(html) {
                let parser = new DOMParser();
                let doc = parser.parseFromString(html, 'text/html');
                update(doc);
            }.bind(this));
        }.bind(this));
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}