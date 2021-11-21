// Aktualisierung der Fortschrittsanzeige für Aufgaben (WebTask)
function updateTaskProgressBar(id, url, onFinish)
{
    var interval = setInterval(function ()
    {
        $.ajax({ url: url + '/' + id, dataType: 'json' }).then(function (data)
        {
            document.getElementById(id).firstElementChild.style.width = data.Progress + '%';

            if (data.State == 3)
            {
                clearInterval(interval);
                onFinish();
            }
        });

    }, 1000);
}

// Aktualisierung der des Modalen Dialoges für für Aufgaben (WebTask)
function updateTaskModal(id, url)
{
    var interval = setInterval(function ()
    {
        $.ajax({ url: url + '/' + id, dataType: 'json' }).then(function (data)
        {
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
function updatePopupNotification(id, url, confirmUrl)
{
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
            button.addEventListener("click", function (data)
            {
                $.ajax({ url: confirmUrl + "/" + message.ID, dataType: 'json' });
            });

            div.className = "alert alert-warning alert-dismissible fade show";
            button.className = "close";

            if (message.Durability > 0)
            {
                setTimeout(function ()
                {
                    $.ajax({ url: confirmUrl + "/" + message.ID, dataType: 'json' });

                    $(div).alert('close')

                }, message.Durability);
            }
        });
    };

    $.ajax({ url: url, dataType: 'json' }).then(update);

    var interval = setInterval(function ()
    {
        $.ajax({ url: url, dataType: 'json' }).then(update);

    }, 15000);
}