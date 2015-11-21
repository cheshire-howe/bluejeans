define(['jquery'], function($) {
    var success = {};

    success.init = function(success) {
        console.log(success);
        if (success == 1) {
            $('#bjnmessage').text('Success').show();
            setTimeout(function() {
                $('#bjnmessage').slideUp('slow');
            }, 2000);
        } else if (success == 2) {
            $('#bjnmessage').text('Error. You must have scheduled the class to edit it.').show();
            $('#bjnmessage').removeClass('alert-success');
            $('#bjnmessage').addClass('alert-danger');
            setTimeout(function() {
                $('#bjnmessage').slideUp('slow');
            }, 2000);
        } else if (success == 3) {
            $('#bjnmessage').text('Error. You must have scheduled the class to delete it.').show();
            $('#bjnmessage').removeClass('alert-success');
            $('#bjnmessage').addClass('alert-danger');
            setTimeout(function() {
                $('#bjnmessage').slideUp('slow');
            }, 2000);
        }
    };

    return success;
})