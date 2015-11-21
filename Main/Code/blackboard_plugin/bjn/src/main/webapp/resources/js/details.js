(function($, _app) {
	var details = {};
	
	details.init = function() {
		$('#meetingInfoTrigger').click(function() {
			$('#meetingInfo').toggle();
		});
	}
	
	_app.details = details;
})(jQuery.noConflict(), window.app || (window.app = {}));
