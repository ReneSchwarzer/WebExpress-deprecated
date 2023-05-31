/**
 * Popup-Benachrichtigungen
 * Folgende Events werden ausgelöst:
 * - webexpress.webapp.close mit Parameter id
 */
webexpress.webapp.popupNotificationCtrl = class extends webexpress.ui.events {
    _restUri = "";
    _container = $("<div class='popupnotification'/>");
    _activeNotifications = new Map();

    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Returns or sets the id. des Steuerelements
     *        - resturi Die Uri der REST-API-Schnittstelle, welche die Daten ermittelt
     *        - intervall Das Intervall bestimmt den Zeitpunkt der REST-API-Anfragen
     */
    constructor(settings) {
        super();
        
        let id = settings.id;
        let interval = settings.interval ?? 10000;
        this._restUri = settings.resturi;

        this._container.attr("id", id ?? "");

        setInterval(function () {
            this.receiveData();

        }.bind(this), interval);

        this.receiveData();
    }

    /**
      * Daten aus REST-Schnitstelle abrufen
      */
    receiveData() {
        let interval = null;
        
        let percents = function (created, durability) { 
            let till = created.valueOf() + durability;
            let now = new Date().valueOf();
            let p = Math.round((till - now) * 100 / durability);
            p = Math.min(Math.max(p, 0), 100);
            return p;
        };
        
        let updateProgress = function (progress, created, durability, data) {
            if (progress >= 0 && progress < 100) {
                data.progressbar.width(progress + "%");
            } else if (durability > 0) {
                data.progressbar.width(percents(new Date(created), durability) + "%");
                interval = setInterval(function () {
                    let p = percents(new Date(created), durability);
                    data.progressbar.width(p + "%");
                    if (p <= 0) {
                        data.alert.alert('close');
                        this._activeNotifications.delete(data.id);
                        clearInterval(interval);
                        this.trigger('webexpress.webapp.close', data.id);
                    }
                }.bind(this), 333);
            }
        }.bind(this);
        
        $.ajax({ type: "GET", url: this._restUri, dataType: 'json', }).then(function (data) {
            let newnotifications = data.filter(notification => !this._activeNotifications.has(notification.id));
            newnotifications.forEach(notification => {
                let id = notification.id ?? "notification" + new Date().valueOf();
                let created = notification.created ?? new Date().toString();
                let durability = notification.durability ?? -1;
                let progress = notification.progress ?? -1;
                let type = notification.type ?? "alert-primary";
                let heading = $("<h5>" + (notification.heading ?? "") + "</h5>");
                let icon = $("<div/>");
                let message = $("<div>" + (notification.message ?? "") + "</div>");
                let progressbar = $("<div class='progress-bar progress-bar-striped bg-info' role='progressbar' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100' style='width:" + (progress >= 0 && progress < 100 ? 0 : percents(new Date(created), durability)) + "%'></div>");
                let alert = $("<div class='alert " + type + " alert-dismissible fade show' role='alert'></div");
                let button = $("<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>");
                let content = $("<div class='d-flex justify-content-start'/>");
                                
                if (notification.Icon != null) {
                    icon = $("<img src='" + notification.icon + "' alt='" + (notification.heading ?? "") + "'/>");
                }

                button.click(function () {
                    this._activeNotifications.delete(id);
                    clearInterval(interval);
                    $.ajax({ type: "DELETE", url: this._restUri + "/" + id, dataType: 'json' });
                    this.trigger('webexpress.webapp.close', id);
                }.bind(this));

                content.append(icon);
                content.append(message);
                
                alert.append(button);
                alert.append(heading);
                alert.append(content);
                if (progress >= 0 || durability >= 0) {
                    alert.append($("<div class='progress mt-2'></div>").append(progressbar));
                }
                
                this._container.append(alert);
                
                if (!this._activeNotifications.has(id)) {
                    let data = { 
                        id: id, 
                        type: type,
                        heading: heading, 
                        icon: icon, 
                        message: message, 
                        progressbar: progressbar, 
                        content: content,
                        alert: alert,
                        notification: notification
                    };
                
                    this._activeNotifications.set(id, data);
                    
                    updateProgress(progress, created, durability, data);
                }
            });
            
            let oldnotifications = data.filter(notification => this._activeNotifications.has(notification.id));
            oldnotifications.forEach(notification => {
                let id = notification.id ?? "notification" + new Date().valueOf();
                let data = this._activeNotifications.get(id);
                
                if (notification.type ?? "alert-primary" != data.notification.type ?? "alert-primary") {
                    data.alert.removeClass(data.notification.type ?? "alert-primary");
                    data.alert.addClass(notification.type ?? "alert-primary");
                }
                if (notification.heading != data.notification.heading) {
                    data.heading.empty().append(notification.heading ?? "");
                }
                if (notification.message != data.notification.message) {
                    data.message.empty().append(notification.message ?? "");
                }
                
                if (notification.progress != data.notification.progress) {
                    data.progressbar.width(notification.progress + "%");
                    if (notification.progress >= 100) {
                        updateProgress(notification.progress ?? -1, notification.created ?? new Date().toString(), notification.durability ?? -1, data);
                    }
                }
                                
                if (notification.Icon != data.notification.icon) {
                    data.content.children("img").remove();
                    if (notification.icon != null) {
                        data.icon = $("<img src='" + notification.icon + "' alt='" + (notification.heading ?? "") + "'/>");
                    } else {
                        data.icon = $("<div/>");
                    }
                    data.content.prepend(data.icon);
                }
                    
                data.notification = notification;
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