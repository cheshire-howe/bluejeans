(function($, _app) {

    var video = {};

    video.showAccordian = function() {
        $('#accordion').accordion();
    };

    $(function() {
        video.showAccordian();
    });

    _app.video = video;

})(jQuery, window.app || (window.app = {}));