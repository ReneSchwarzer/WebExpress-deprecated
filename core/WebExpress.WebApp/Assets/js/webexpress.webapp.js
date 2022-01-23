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
class crudTable {
    _container = $("<div/>");

    /**
     * Konstruktor
     * @param restURL Die REST-API-Schnittestelle zur ermittlung und Manipulation der Daten
     * @param options Optionen zur Gestaltung des Steuerelementes
     *        - Id Die ID des Steuerelements
     *        - Search Die Einstellungen zur Suche
     *        - Table Die Einstellungen der Tabelle
     *        - Pagination Die Einstellungen der Seitennavigation
     * @return Das Steuerelement
     */
    constructor(restURL, options) {

        var id = options.ID;
        var searchOptions = options.Search;
        var tableOptions = options.Table;
        var paginationOptions = options.Pagination;
        var editorsOptions = options.Editors;
        var filter = "";

        var search = new searchCtrl(searchFunction, searchOptions);
        var table = new tableCtrl(tableOptions);
        var pagination = new paginationCtrl(function callback(page) { receiveData(restURL, filter, page); }, paginationOptions);

        // Spaltendefinition aus REST-Schnitstelle abrufen
        function receiveColumns(url) {

            $.ajax({ type: "GET", url: url + "?columns", dataType: 'json', }).then(function (response) {
                var columns = response;
                columns.push({
                    Label: '',
                    Width: 1,
                    Render: function (cell, item) {
                        var dropdownitems = editorsOptions;
                        var dropdown = new moreCtrl(dropdownitems, { Icon: "fas fa-ellipsis-h", MenuCSS: "dropdown-menu-lg-end" });

                        return dropdown.getCtrl;
                    }
                });

                table.columns = columns;
            });
        }

        // Daten aus REST-Schnitstelle abrufen
        function receiveData(url, filter, page) {

            $.ajax({ type: "GET", url: url + "?search=" + filter + "&page=" + page, dataType: 'json', }).then(function (response) {
                var data = response.Data;
                table.clear();
                table.addRange(data);

                var page = response.Pagination;
                pagination.page(page.PageNumber, page.Totalpage);
            });
        }

        function searchFunction(searchterm) {

            if (searchterm !== undefined) {
                filter = searchterm.toUpperCase();

                receiveData(restURL, filter, 0);
            } else {
                receiveData(restURL, "", 0);
            }
        }

        this._container.append(search.getCtrl);
        this._container.append(table.getCtrl);
        this._container.append(pagination.getCtrl);

         
           /*
                        if (columns.length > 0) {
                            var cell = $("<td/>");
                            var dropdownitems = [];
                        
                            editors.forEach(function (editor) {
                                if (editor.Label != "-") {
                                    dropdownitems.push({ css: "dropdown-item", icon: editor.Icon, color: editor.Color, label: editor.Label});
                                }
                                else {
                                    dropdownitems.push({ css: "dropdown-divider" });
                                }
                            });
    
                            var dropdown = new moreCtrl(dropdownitems, { Icon: "fas fa-ellipsis-h", MenuCSS: "dropdown-menu-lg-end" });
    
                            cell.append(dropdown);
                            row.append(cell);
                        }
    
                        body.append(row);
                    });
        */
        receiveColumns(restURL);
        receiveData(restURL, filter, 0);
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}