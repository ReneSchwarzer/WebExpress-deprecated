/**
 * Eine Dopdown-Box mit Funktionen für Create, Read, Update und Delate
 */
webexpress.webapp.selectionCtrl = class extends webexpress.ui.selectionCtrl {
    _optionUri = "";
    _spinner = $("<div class='spinner-border spinner-border-sm text-secondary' role='status'/>");

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - OptionUri Die Uri der REST-API-Schnittstelle, welche die Optionen ermittelt
     */
    constructor(settings) {
        super(settings);

        this._optionUri = settings.OptionUri;
        this._optionfilter = function (x, y) { return true; };

        this._container.on('show.bs.dropdown', function () {
            this.receiveData(this._filter.val());
        }.bind(this));
    }

     /**
      * Daten aus REST-Schnitstelle abrufen
      * @param filter Die Filtereinstellungen
      */
    receiveData(filter) {

        filter = filter !== undefined || filter != null ? filter : "";
        this._selection.append(this._spinner);

         $.ajax({ type: "GET", url: this._optionUri + "?search=" + filter + "&page=0", dataType: 'json', }).then(function (response) {
             var data = response.Data;

             this.options = data;
             this.trigger('webexpress.ui.receive.complete');

             this.update();

             this._selection.children("div").remove();
         }.bind(this));
    }
}