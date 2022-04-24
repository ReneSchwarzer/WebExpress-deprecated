/**
 * Eine Dopdown-Box mit Funktionen für Create, Read, Update und Delate
 */
class restSelectionCtrl extends selectionCtrl {
    _optionUri = "";

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
    }

     /**
      * Daten aus REST-Schnitstelle abrufen
      * @param filter Die Filtereinstellungen
      */
     receiveData(filter) {

         $.ajax({ type: "GET", url: this._optionUri + "?search=" + filter + "&page=0", dataType: 'json', }).then(function (response) {
             var data = response.Data;

             this.options = data;
             this.trigger('webexpress.ui.receive.complete');

             this.update();

         }.bind(this));
    }
}