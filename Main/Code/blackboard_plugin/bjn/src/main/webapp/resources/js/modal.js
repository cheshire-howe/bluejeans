(function($, _app) {
	var modal = {};

	modal.init = function(users, attendees) {
	    $('#modal-toggle').click(function() {
	    	$('#attendeesModal').modal('toggle');
	    });

        $("#userlists .sortable-list").sortable({
            connectWith: '#userlists .sortable-list'
        });

        $.each(users, function(key, value) {
            if ($.inArray(value.email, attendees) > -1) {
                $('#going').append(construct_list_item(value));
            } else {
                $('#notgoing').append(construct_list_item(value));
            }
        });
	    
	    addGoingHiddenInputs();

	    if ($('#attendees :input').length) {
            $('.submit').prop('disabled', false);
        } else {
            $('.submit').prop('disabled', true);
        }

        $('#save').click(function(e) {
            e.preventDefault();
            
            $('#attendees').html('');
            
            addGoingHiddenInputs();

            $('#attendeesModal').modal('toggle');

            if ($('#attendees :input').length) {
                $('.submit').prop('disabled', false);
            } else {
                $('.submit').prop('disabled', true);
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
	}

	function addGoingHiddenInputs() {
        var goinglist = [];
        $('#going li').each(function() {
            goinglist.push($(this).data('email'));
        });

        $.each(goinglist, function(index, going) {
        	$('#attendees').append('<input class="attending" type="hidden" name="attendees[' + index + '].email" value="' + going + '">');
        });
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

    function construct_list_item(value) {
        return "<li class='sortable-item' data-email='"
            + value.email + "'>"
            + value.name + "</li>"
    }

	_app.modal = modal;
})(jQuery.noConflict(), window.app || (window.app = {}));
