(function($, _app) {
    var details = {};

    details.init = function() {
        $('#meetingInfoTrigger').click(function() {
            $('#meetingInfo').toggle();
            $('#placeholder-dd').toggle();
        });
    };

    _app.details = details;
    details.init();
})(jQuery, window.app || (window.app = {}));