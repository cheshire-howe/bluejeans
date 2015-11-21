define(['jquery'], function($) {
    var lib = {};

    lib.toggleMeetingInfo = function() {
        $('#meeting-info-trigger').click(function() {
            $('#meeting-info').toggle();
        });
    };

    return lib;
});