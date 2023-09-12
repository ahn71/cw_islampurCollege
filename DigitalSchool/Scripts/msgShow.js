$(document).ready(function () {
    if ($('#lblMessage').text().length > 1) {
        showMessage($('#lblMessage').text(), '');
    }
});