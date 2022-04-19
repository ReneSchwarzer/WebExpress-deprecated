// Aktualisierung der Fortschrittsanzeige für Aufgaben (WebTask)
function updateTaskProgressBar(id, url, onFinish) {
    var interval = setInterval(function () {
        $.ajax({ url: url + '/' + id, dataType: 'json' }).then(function (data) {
            document.getElementById(id).firstElementChild.style.width = data.Progress + '%';

            if (data.State == 3) {
                clearInterval(interval);
                onFinish();
            }
        });

    }, 1000);
}

// Aktualisierung der des Modalen Dialoges für für Aufgaben (WebTask)
function updateTaskModal(id, url) {
    var interval = setInterval(function () {
        $.ajax({ url: url + '/' + id, dataType: 'json' }).then(function (data) {
            var progressbar = document.getElementById('progressbar_' + id);
            var message = document.getElementById('message_' + id);

            progressbar.firstElementChild.style.width = data.Progress + '%';
            message.innerHTML = data.Message;

            if (data.State == 3) {
                clearInterval(interval);

                $('#' + id).modal('hide');
            }
        });

    }, 1000);
}

// Aktualisierung der Popup-Benachrichtigungen
function updatePopupNotification(id, url, confirmUrl) {
    var update = function (data) {
        var container = document.getElementById(id);
        container.innerHTML = "";

        data.forEach(message => {
            var div = document.createElement("div");
            var button = document.createElement("button");
            var text = document.createElement("span");
            var link = document.createElement("a");

            button.appendChild(document.createTextNode("x"));
            text.innerHTML = message.Message;

            link.setAttribute("type", "button");

            button.setAttribute("type", "button");
            button.setAttribute("data-dismiss", "alert");
            button.setAttribute("aria-label", "Close");
            button.setAttribute("style", "font-size: 1em;");

            div.setAttribute("role", "alert");

            container.appendChild(div);
            div.appendChild(text);
            div.appendChild(button);
            button.addEventListener("click", function (data) {
                $.ajax({ url: confirmUrl + "/" + message.ID, dataType: 'json' });
            });

            div.className = "alert alert-warning alert-dismissible fade show";
            button.className = "close";

            if (message.Durability > 0) {
                setTimeout(function () {
                    $.ajax({ url: confirmUrl + "/" + message.ID, dataType: 'json' });

                    $(div).alert('close')

                }, message.Durability);
            }
        });
    };

    $.ajax({ url: url, dataType: 'json' }).then(update);

    var interval = setInterval(function () {
        $.ajax({ url: url, dataType: 'json' }).then(update);

    }, 15000);
}

/**
 * Eine Tabelle mit Funktionen für Create, Read, Update und Delate
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

/**
 * Eine Tabelle mit Funktionen für Create, Read, Update und Delate
 */
class restTableCtrl extends tableCtrl {
    _restUri = "";
    _searchCtrl = null;
    _paginationCtrl = null;
    _filter = null;
    _page = null;

    /**
     * Konstruktor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - ID Die ID des Steuerelements
     *        - CSS CSS-Klasse zur Gestaltung des Steuerelementes
     *        - Placeholder Der Platzhaltertext
     *        - RestUri Die Uri der REST-API-Schnittstelle, welche die Daten ermittelt
     */
    constructor(settings) {
        super(settings);

        this._restUri = settings.RestUri;

        $.ajax({ type: "GET", url: this._restUri + "?columns=true", dataType: 'json', }).then(function (response) {
            var columns = response.Columns;
            this.columns = columns;
        }.bind(this));

        this._searchCtrl = new searchCtrl({ ID: settings.ID + "-search" });
        this._searchCtrl.on('webexpress.ui.change.filter', function (key) { this._filter = key; this.receiveData(); }.bind(this));
        this._paginationCtrl = new paginationCtrl({ ID: settings.ID + "-pagination" });
        this._paginationCtrl.on('webexpress.ui.change.page', function (page) { this._page = page; this.receiveData(); }.bind(this));
    }

    /**
      * Daten aus REST-Schnitstelle abrufen
      */
    receiveData() {
        if (this._filter === undefined || this._filter == null) { this._filter = ""; }
        if (this._page === undefined || this._page == null) { this._page = 0; }
        $.ajax({ type: "GET", url: this._restUri + "?search=" + this._filter + "&page=" + this._page, dataType: 'json', }).then(function (response) {
            var data = response.Data;
            this.clear();
            this.addRange(data);
            this.trigger('webexpress.ui.receive.complete');
            var pagination = response.Pagination;
            this._paginationCtrl.page(pagination.PageNumber, pagination.Totalpage);
        }.bind(this));
    }

    /**
    * Gibt das Steuerelement zurück
    */
    get getCtrl() {
        let div = $("<div/>")
        div.append(this._searchCtrl.getCtrl);
        div.append(this._table);
        div.append(this._paginationCtrl.getCtrl);
        return div;
    }
}