$(document).ready(function () {
    $.ajax('/api/Translation', {
        success: function (data) {
            window.t = function (key) {
                return data[key] || key;
            }
            $(document).trigger('translationLoaded');
        }
    })
})
