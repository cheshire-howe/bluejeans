define(['jquery', 'jqueryui'], function($) {
    var videoaccordion = {};

    videoaccordion.init = function() {
        $('#accordion').accordion();
    };

    return videoaccordion;
});