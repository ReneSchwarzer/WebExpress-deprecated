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