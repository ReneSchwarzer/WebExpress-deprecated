/**
 * Ein modulares Fenster/ Dialog
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.close
 */
webexpress.ui.modalPageCtrl = class extends webexpress.ui.events {
    _uri = null;
    _container = $("<div class='modal modalpage fade' data-bs-backdrop='static' data-bs-keyboard='false' tabindex='-1' aria-hidden='true'></div>");
    
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
        let dialog = $("<div class='modal-dialog modal-xl modal-dialog-scrollable'></div>");
        let content = $("<div class='modal-content'></div>");
        
        this._container.attr("id", id ?? "");
        this._uri = uri;
        
        dialog.append(content);
        this._container.append(dialog);

        let update = function (response) {
            let parser = new DOMParser();
            let doc = parser.parseFromString(response, 'text/html');
            
            //let doc = $.parseHTML(response, null, true);
            let title = $("title", doc).text();
            let main = $("#webexpress\\.webapp\\.content\\.main\\.primary", doc);
            let form = $("form", main);
            let scripts = $('script', form);
            
            content.children().remove();
            content.append($("<div class='modal-header'><h5 class='modal-title'>" + title + "</h5><button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='" + close + "'></button></div>"));

            if (form != null) {
                let action = form.attr("action") ?? uri;

                form.submit(function (event) {
                    event.preventDefault();
                    $.ajax({
                        type: 'POST',
                        url: action,
                        data: form.serialize(),
                        success: function (response) {
                            update(response);
                        }.bind(this),
                        error: function (response) {
                            content.append(response);
                        }.bind(this)
                    });
                }.bind(this));

                form.addClass("modal-body");
                $("footer", form).addClass("modal-footer");

                content.append(form);

                scripts.each(function (index, script) {
                    $.globalEval(script.text || script.textContent || script.innerHTML || '');
                });
            } else {
                main.addClass("modal-body");
                content.append(main);
                content.append("<div class='modal-footer'><button type='button' class='btn btn-secondary' data-bs-dismiss='modal'>" + close + "</button></div>");
            }

        }.bind(this);

        $(document).ready(function () {
            $.get(this._uri, function (response) {
                update(response);
            }.bind(this));
        }.bind(this));
    }

    /**
     * Anzeige des modalen Dialogs
     */
    show() {
        let modal = new bootstrap.Modal(this.getCtrl, {});
        modal.show();
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}