/**
 * Ein modulares Fenster/ Dialog
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.close
 */
webexpress.ui.modalFormularCtrl = class extends webexpress.ui.events {
    _uri = null;
    _container = $("<div class='modal modalformular fade' data-bs-backdrop='static' data-bs-keyboard='false' tabindex='-1' aria-hidden='true'></div>");

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die ID des Steuerelements
     *        - close Der Name der Schließenschaltfläche
     *        - uri Die Url des Formulars
     *        - size Die Größe des Modals (small, default, large, extralarge)
     */
    constructor(settings) {
        super();
        
        let id = settings.id;
        let close = settings.close ?? "Close";
        let uri = settings.uri ?? "";
        let size = settings.size ?? "";
        let dialog = $("<div class='modal-dialog modal-dialog-scrollable'></div>");

        this._container.attr("id", id ?? "");
        this._uri = uri;
        
        this._container.append(dialog);

        if (size == "small") {
            dialog.addClass("modal-sm");
        } else if (size == "large") {
            dialog.addClass("modal-lg");
        } else if (size == "extralarge") {
            dialog.addClass("modal-xl");
        }

        let update = function (response) {
            let parser = new DOMParser();
            let doc = parser.parseFromString(response, 'text/html');

            let title = $("title", doc).text();
            let main = $("#webexpress\\.webapp\\.content\\.main\\.primary", doc);
            let form = $("form", main);
            let formContent = form?.children(":not('footer')");
            let formFooter = $('footer', form);
            let header = $("<div class='modal-header'><h5 class='modal-title'>" + title + "</h5><button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='" + close + "'></button></div>");
            let body = $("<div class='modal-body'></div>");
            let footer = $("<div class='modal-footer'></div>");
            let content = $("<div class='modal-content'></div>");

            if (form.length > 0) {
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
                            body.append(response);
                        }.bind(this)
                    });
                }.bind(this));

                body.append(formContent);
                footer.append(formFooter.children());
                footer.append($("<a class='btn' data-bs-dismiss='modal'>" + close + "</a>"));

                form.children().remove();
                dialog.children().remove();
                content.append(header);
                content.append(body);
                content.append(footer);
                form.append(content);
                dialog.append(form);
            } else if (this._modal != null) { 
                this._modal.hide();
            }
        }.bind(this);

        $(document).ready(function () {
            $.get(this._uri, function (response) {
                update(response);
            }.bind(this))
                .fail(function (e) {
                    modal.dispose();
                    this._container.remove();
                });
        }.bind(this));

        let modal = bootstrap.Modal.getOrCreateInstance(this.getCtrl);

        this._container.on('shown.bs.modal', function (event) {
            $('script', dialog).each(function (index, script) {
                $.globalEval(script.text || script.textContent || script.innerHTML || '');
            });
        });

        this._container.on('hidden.bs.modal', function (event) {
            modal.dispose();
            this._container.remove();
        }.bind(this));

        modal.show();
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}