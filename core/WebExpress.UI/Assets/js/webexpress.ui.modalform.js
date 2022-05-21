/**
 * Ein modulares Fenster/ Dialog
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.close
 */
webexpress.ui.modalFormCtrl = class extends webexpress.ui.events {
    _uri = null;
    _container = $("<div class='modal modalpage fade' data-bs-backdrop='static' data-bs-keyboard='false' tabindex='-1' aria-hidden='true'></div>");
    _modal = null;

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

        this._container.attr("id", id ?? "");
        this._uri = uri;
        
        this._container.append(dialog);

        let update = function (response) {
            let parser = new DOMParser();
            let doc = parser.parseFromString(response, 'text/html');

            let title = $("title", doc).text();
            let main = $("#webexpress\\.webapp\\.content\\.main\\.primary", doc);
            let form = $("form", main);
            let formContent = form?.children(":not('footer')");
            let formFooter = $('footer', form);
            let scripts = $('script', form);
            let header = $("<div class='modal-header'><h5 class='modal-title'>" + title + "</h5><button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='" + close + "'></button></div>");
            let body = $("<div class='modal-body'></div>");
            let footer = $("<div class='modal-footer'></div>");
            let content = $("<div class='modal-content'></div>");

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
               
                body.append(formContent);
                footer.append(formFooter.children());

                form.children().remove();
                dialog.children().remove();
                content.append(header);
                content.append(body);
                content.append(footer);
                form.append(content);
                dialog.append(form);

                //scripts.each(function (index, script) {
                //    $.globalEval(script.text || script.textContent || script.innerHTML || '', {}, dialog[0].ownerDocument);
                //});
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

        this._modal = bootstrap.Modal.getOrCreateInstance(this.getCtrl);
        this._container.on('shown.bs.modal', function (event) {
            $('script', dialog).each(function (index, script) {
                $.globalEval(script.text || script.textContent || script.innerHTML || '');
            });
        });

        this._container.on('hidden.bs.modal', function (event) {
            this._modal.dispose();
            this._container.remove();
        }.bind(this));
    }

    /**
     * Anzeige des modalen Dialogs
     */
    show() {
        this._modal.show();
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}