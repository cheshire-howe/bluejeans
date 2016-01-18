(function($, _app) {
    var recurring = {};

    recurring.init = function() {
        if (window.location.href.indexOf("Create") > -1) {
            $('#lengthOfClass').val(120);
        }

        $('#endDatePicker').datetimepicker({
            timepicker: false,
            format: 'd/m/Y'
        });

        $('#id_repeat_meeting_day_of_month').val(moment().format("D"));

        tinymce.init({
            selector: '#description',
            menubar: false,
            statusbar: false,
            height: 300
        });

        $('#repeatMeetingType').change(function() {
            var str = "";
            $("#repeatMeetingType option:selected").each(function() {
                str = $(this).text();
            });
            switch (str) {
                case 'Daily':
                    $('#div_daily_options').show();
                    $('#div_weekly_options, #frequency_monthly_weekly').hide();
                    $('#div_monthly_options, #id_repeat_meeting_day_of_month, #frequency_monthly_monthly').hide();
                    $('#frequency_unit').text('days');
                    break;
                case 'Weekly':
                    $('#div_daily_options').hide();
                    $('#div_weekly_options, #frequency_monthly_weekly').show();
                    $('#div_monthly_options, #id_repeat_meeting_day_of_month, #frequency_monthly_monthly').hide();
                    $('#frequency_unit').text('weeks');
                    break;
                case 'Monthly':
                    $('#div_daily_options').hide();
                    $('#div_weekly_options, #frequency_monthly_weekly').hide();
                    $('#div_monthly_options, #id_repeat_meeting_day_of_month, #frequency_monthly_monthly').show();
                    checkMonthOption();
                    $('#frequency_unit').text('months');
                    break;
            }
        }).change();

        $('#use_repeat_meeting').click(function() {
            if ($('#repeat:checked').val()) {
                $('#reccurrence_options').show();
            } else {
                $('#reccurrence_options').hide();
            }
        });

        if ($('#repeat:checked').val()) {
            $('#reccurrence_options').show();
        }

        $("#div_recurrence_ending").on("click", function() {
            switch ($("input[name='recurssion_ending']:checked").val()) {
                case 'NEVER':
                    $('#id_recurssion_ending_sub_options_occurrences').prop('disabled', true);
                    $('#id_recurssion_ending_sub_options_on_date').prop('disabled', true);
                    $('#endDatePicker').prop('disabled', true);
                    break;
                case 'OCCURRENCES':
                    $('#id_recurssion_ending_sub_options_occurrences').prop('disabled', false);
                    $('#id_recurssion_ending_sub_options_on_date').prop('disabled', true);
                    $('#endDatePicker').prop('disabled', true);
                    break;
                case 'ON':
                    $('#id_recurssion_ending_sub_options_occurrences').prop('disabled', true);
                    $('#id_recurssion_ending_sub_options_on_date').prop('disabled', true);
                    $('#endDatePicker').prop('disabled', false);
            }
        });

        $('#bjnform').submit(function() {
            populateForm();
        });
    };

    function checkMonthOption() {
        $('#id_repeat_meeting_week_of_month').change(function() {
            var str = "";
            $("#id_repeat_meeting_week_of_month option:selected").each(function() {
                str = $(this).text();
            });
            switch (str) {
                case 'Day':
                    $('#id_repeat_meeting_day_of_month').show();
                    $('#id_repeat_meeting_day_of_week_monthly').hide();
                    break;
                default:
                    $('#id_repeat_meeting_day_of_month').hide();
                    $('#id_repeat_meeting_day_of_week_monthly').show();
            }
        }).change();
    }

    function populateForm() {
        if ($('#repeat').prop('checked')) {

            // Recurrence type
            if ($('#id_repeat_meeting').val() == 'DAILY' &&
		        $('#id_repeat_meeting_sub_options_daily_weekdays').prop('checked')) {

                $('input[name="recurrencePattern.recurrenceType"]').val('WEEKLY');

            } else {
                $('input[name="recurrencePattern.recurrenceType"]').val($('#id_repeat_meeting').val());
            }

            // End Date or Recurrence Count
            switch ($('input[name="recurssion_ending"]:checked').val()) {
                case 'NEVER':
                    $('input[name="recurrencePattern.endDate"]').val(null);
                    $('input[name="recurrencePattern.recurrenceCount"]').val('0');
                    break;
                case 'OCCURRENCES':
                    $('input[name="recurrencePattern.endDate"]').val(null);
                    $('input[name="recurrencePattern.recurrenceCount"]').val($('#id_recurssion_ending_sub_options_occurrences').val());
                    break;
                case 'ON':
                    $('input[name="recurrencePattern.endDate"]').val(Date.parse($('#endDatePicker').val()));
                    $('input[name="recurrencePattern.recurrenceCount"]').val('0');
                    break;
            }

            // Frequency
            $('input[name="recurrencePattern.frequency"]').val($('#id_repeat_meeting_frequency').val());

            // Day of the Week Mask
            $('input[name="recurrencePattern.daysOfWeekMask"]').val(mask());

            // Day of the month
            $('input[name="recurrencePattern.dayOfMonth"]').val(dayOfMonth());


            // Week of the Month
            $('input[name="recurrencePattern.weekOfMonth"]').val(weekOfTheMonth());

            // Month of the Year
            $('input[name="recurrencePattern.monthOfYear"]').val('NONE');

        } else {
            $('input[name="recurrencePattern.recurrenceType"]').val(null);
            $('input[name="recurrencePattern.endDate"]').val(null);
            $('input[name="recurrencePattern.weekOfMonth"]').val(null);
            $('input[name="recurrencePattern.monthOfYear"]').val(null);
        }

        // Start time / End time
        var unixtimeStart = moment($('#startDateTime').val(), "YYYY/MM/DD HH:mm").unix() * 1000;
        var unixtimeEnd = unixtimeStart + ($('#lengthOfClass').val() * 1000 * 60); // add minutes to start time

        $('input[name="start"]').val(unixtimeStart);
        $('input[name="end"]').val(unixtimeEnd);

        // Put descriptiontext into description
        $('input[name="description"]').val($('#descriptiontext_ifr').contents().find('#tinymce').html())
    }

    // Day of the Week Mask
    var mask = function() {
        var total = 0;
        if ($('#id_repeat_meeting').val() == 'DAILY') {

            if ($('#id_repeat_meeting_sub_options_daily_weekdays').prop('checked')) {
                return 62;
            }

        } else if ($('#id_repeat_meeting').val() == 'WEEKLY') {

            $('#weeklyOptions input[type="checkbox"]').each(function() {
                if ($(this).prop('checked')) {
                    total += parseInt($(this).val());
                }
            });

        } else if ($('#id_repeat_meeting').val() == 'MONTHLY' && !($('#id_repeat_meeting_week_of_month').val() == '0')) {

            total = parseInt($('#id_repeat_meeting_day_of_week_monthly').val());

        }
        return total;
    };

    // Day of the Month func
    var dayOfMonth = function() {
        if ($('#id_repeat_meeting').val() == 'MONTHLY' &&
	        $('#id_repeat_meeting_week_of_month').val() == 0) {

            return $('#id_repeat_meeting_day_of_month').val();
        }

        return 0;
    };

    // Week of the Month func
    var weekOfTheMonth = function() {
        if ($('#id_repeat_meeting').val() == 'MONTHLY' && !($('#id_repeat_meeting_week_of_month').val() == '0')) {
            return $('#id_repeat_meeting_week_of_month').val();
        }

        return 'NONE';
    };

    _app.recurring = recurring;

    recurring.init();
})(jQuery, window.app || (window.app = {}));
