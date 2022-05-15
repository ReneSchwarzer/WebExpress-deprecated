/**
 * Aktualisierung der Popup-Benachrichtigungen
 */
class popupNotificationCtrl {
    _restUri = "";
    _confirmUri = "";
    _container = $("<div class='popupnotification'/>");

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - RestUri Die Uri der REST-API-Schnittstelle, welche die Daten ermittelt
     *        - Intervall Das Intervall bestimmt den Zeitpunkt der REST-API-Anfragen
     */
    constructor(settings) {

        let id = settings.ID;
        let interval = settings.Interval ?? 15000;
        this._restUri = settings.RestUri;

        if (id !== undefined) {
            this._container.attr("id", id);
        }

        setInterval(function () {
            this.receiveData();

        }.bind(this), interval);

        this.receiveData();
    }

    /**
      * Daten aus REST-Schnitstelle abrufen
      */
    receiveData() {
        $.ajax({ type: "GET", url: this._restUri, dataType: 'json', }).then(function (data) {
            data.forEach(message => {
                this._container.children().remove();

                let alert = $("<div class='alert alert-warning alert-dismissible fade show' role='alert'></div");
                let button = $("<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>");
                let progress = $("<div class='progress mt-2'></div>");
                let progressbar = $("<div class='progress-bar progress-bar-striped progress-bar-animated bg-info' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width: 100%'></div>");

                button.click(function () {
                    $.ajax({ type: "DELETE", url: this._restUri + "/" + message.ID, dataType: 'json' });
                }.bind(this));

                progress.append(progressbar);
                alert.append(message.Message);
                alert.append(button);
                
                if (message.Durability > 0) {
                    let till = new Date(message.Created).valueOf() + message.Durability;
                    let now = new Date().valueOf();
                    let p = Math.round((till - now) * 100 / message.Durability);
                    p = Math.max(p, 0);
                    p = Math.min(p, 100);
                    progressbar.width(p + "%");

                    setInterval(function () {
                        let now = new Date().valueOf();
                        let p = Math.round((till - now) * 100 / message.Durability);
                        p = Math.max(p, 0);
                        p = Math.min(p, 100);
                        progressbar.width(p + "%");

                        if (p <= 0) {
                            $(alert).alert('close');
                        }
                    }.bind(this), 250);

                    alert.append(progress);
                }

                this._container.append(alert);
            });
        }.bind(this));
    }

    /**
    * Gibt das Steuerelement zurück
    */
    get getCtrl() {
        return this._container;
    }
}