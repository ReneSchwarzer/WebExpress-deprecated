/**
 * Fortschrittsbalken einer Aufgabe (WebTask)
 * Folgende Events werden ausgelöst:
 * - webexpress.webapp.finish mit Parameter id
 */
webexpress.webapp.taskProgressBarCtrl = class extends webexpress.ui.events {
    _restUri = "";
    _container = $("<div class='taskprogressbar'/>");
    _progress = $("<div class='progress'><div class='progress-bar progress-bar-striped progress-bar-animated' role='progressbar' style='width: 0%' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100'></div></div>");
    _message = $("<div class='text-secondary'/>");
    _interval = null;

    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die Id des Steuerelements
     *        - resturi Die Uri der REST-API-Schnittstelle, welche die Daten ermittelt
     *        - intervall Das Intervall bestimmt den Zeitpunkt der REST-API-Anfragen
     */
    constructor(settings) {
        super();
        
        let id = settings.id;
        let interval = settings.interval ?? 15000;
        this._restUri = settings.resturi;

        this._container.attr("id", id ?? "");

        this._interval = setInterval(function () {
            this.receiveData();
        }.bind(this), interval);
        
        this._container.append(this._progress);
        this._container.append(this._message);

        this.receiveData();
    }

    /**
      * Daten aus REST-Schnitstelle abrufen
      */
    receiveData() {        
        $.ajax({ type: "GET", url: this._restUri, dataType: 'json', }).then(function (data) {
            let progress = data.Progress ?? 0;
            let type = data.Type ?? "bg-primary";
            let message = data.Message ?? "";

            this._progress.children().first().width(Math.min(Math.max(progress, 0), 100) + "%");
            this._progress.children().first().css("progress-bar progress-bar-striped progress-bar-animated" + type);
            this._message.html(message);
            
            if (data.State == 3) {
                clearInterval(this._interval);
                this.trigger('webexpress.webapp.finish', data.Id);
            }
            
        }.bind(this));
    }

    /**
    * Gibt das Steuerelement zurück
    */
    get getCtrl() {
        return this._container;
    }
}