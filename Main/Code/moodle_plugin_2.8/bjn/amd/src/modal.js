define(['jquery', 'jqueryui'], function($) {
    var modal = {};

    modal.init = function(users) {
        if ($('input[name=attendees]').val()) {
            $('#id_submitbutton').prop('disabled', false);
        } else {
            $('#id_submitbutton').prop('disabled', true);
        }
        var attending = $('input[name=attendees]').val().split(',');

        $.each(users, function(key, value) {
            if ($.inArray(value.email, attending) > -1) {
                $('#going').append(construct_list_item(value));
            } else {
                $('#notgoing').append(construct_list_item(value));
            }
        });

        $("#userlists .sortable-list").sortable({
            connectWith: '#userlists .sortable-list'
        });

        $('#save').click(function(e) {
            e.preventDefault();
            var goinglist = [];
            $('#going li').each(function() {
                goinglist.push($(this).data('email'));
            });

            $('input[name=attendees]').val(goinglist);
            $('#attendeesModal').modal('toggle');

            if ($('input[name=attendees]').val()) {
                $('#id_submitbutton').prop('disabled', false);
            } else {
                $('#id_submitbutton').prop('disabled', true);
            }
        });

        $('#select-all').click(function(e) {
            var $notgoing = $('#notgoing li');
            var $going = $('#going li');
            $notgoing.each(function(key, value) {
                if (key <= (4 - $going.length)) {
                    moveAnimate($(this), $('#going'), key);
                } else {
                    var $this = $(this);
                    $this.hide();
                    $this.appendTo($('#going'));
                    setTimeout(function() {
                        $this.show();
                    }, 250);
                }
            });
        });

        $('#deselect-all').click(function(e) {
            var $notgoing = $('#notgoing li');
            var $going = $('#going li');
            $going.each(function(key, value) {
                if (key <= (4 - $notgoing.length)) {
                    moveAnimate($(this), $('#notgoing'), key);
                } else {
                    var $this = $(this);
                    $this.hide();
                    $this.appendTo($('#notgoing'));
                    setTimeout(function() {
                        $this.show();
                    }, 250);
                }
            });
        });
    };

    function construct_list_item(value) {
        return "<li class='sortable-item' data-email='"
            + value.email + "'><img src='"
            + value.picurl + "' height='35' width='35'>"
            + value.name + "</li>"
    }

    function moveAnimate(element, newParent, multiplier){
        var oldOffset = element.offset();
        element.appendTo(newParent);
        var newOffset = element.offset();

        var temp = element.clone().appendTo('body');
        temp.css('position', 'absolute')
            .css('left', oldOffset.left)
            .css('top', oldOffset.top + (multiplier * 45))
            .css('zIndex', 12000);
        element.hide();
        temp.animate( {'top': newOffset.top + (multiplier * 45), 'left':newOffset.left}, 250, function(){
            element.show();
            temp.remove();
        });
    }

    return modal;
});
