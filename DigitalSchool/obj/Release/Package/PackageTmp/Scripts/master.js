var timeOutId = 0;


$(document).ready(function () {

    if ($('.display').html().length > 0) $('.display').tablesorter();

});



var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_initializeRequest(InitializeRequest);
prm.add_endRequest(EndRequest);
function InitializeRequest(sender, args) {

}

function EndRequest(sender, args) {

    if ($('#lblMessage').text().length > 1) {
        showMessage($('#lblMessage').text(), '');
    }
}

function showMessage(message, messageType) {

    try {
        $('#lblErrorMessage').hide();

        clearTimeout(timeOutId);

        var backColor = '#141614';
        var foreColor = '#FFF';
        var timeOut = 15000;


        if (message.indexOf('->') == -1) {
            if (messageType.length == 0) message = "info->" + message;
            else message = messageType + "->" + message;
        }

        var msg = message.split('->');
        messageType = msg[0];
        var msgBox = $('#lblErrorMessage');
        msgBox.css('width', 'auto');


        if (messageType == 'warning') {
            backColor = '#FFCD3C';
            foreColor = 'Black';
        }
        else if (messageType == 'success') {
            timeOut = 5000;
            backColor = '#5BD45B';
        }
        else if (messageType == 'error') backColor = '#EF494B';

        msgBox.css('background-color', backColor);
        msgBox.css('color', foreColor);

        if (msg[1].length == 0) {
            hideErrorMessage();
            return;
        }

        $('#lblErrorMessage p').html(msg[1]);


        msgBox.css('z-index', '999999999');
        if (msgBox.width() > 600) msgBox.css('width', '600px');


        if ($('.popBox:visible').length == 1) {
            var pos = $('.popBox:visible').offset();

            msgBox.css('position', 'absolute');
            msgBox.css('top', pos.top + 8);
            msgBox.css('right', '').css('left', pos.left + ($('.popBox:visible').width() / 2 - msgBox.width() / 2));
        }
        else {
            msgBox.css('position', 'fixed');
            msgBox.css('top', 37);
            msgBox.css('left', '50%');
            msgBox.css('margin-left', '-' + (msgBox.width() / 2) + "px");
        }

        msgBox.fadeIn(500);
        timeOutId = setInterval("hideErrorMessage()", timeOut);

        $('#lblMessage').text('');
    }
    catch (e) {
        console.log(e.message);
    }
}

function hideErrorMessage() {
    $('#lblErrorMessage').fadeOut('3000');
    clearTimeout(timeOutId);
}


function goURL(url) {
    document.location = url;
}