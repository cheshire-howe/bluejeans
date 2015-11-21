define(['jquery', 'jqueryui'], function($) {
    var recurring = {};

    recurring.init = function() {
        $('#repeatMeetingType').change(function () {
            var str = "";
            $( "#repeatMeetingType option:selected" ).each(function() {
                str = $( this ).text();
            });
            switch(str) {
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

        $('#fitem_id_use_repeat_meeting').click(function() {
            if ($('#id_use_repeat_meeting:checked').val()) {
                $('#reccurrence_options').show();
                console.log('true');
            } else {
                $('#reccurrence_options').hide();
                console.log('false');
            }
        });

        $( "#div_recurrence_ending" ).on( "click", function() {
            switch($( "input[name='recurssion_ending']:checked" ).val()) {
                case 'NEVER':
                    $('#id_recurssion_ending_sub_options_occurrences').prop('disabled', true);
                    $('#id_recurssion_ending_sub_options_on_date').prop('disabled', true);
                    $('#id_enddate_day, #id_enddate_month, #id_enddate_year').prop('disabled', true);
                    break;
                case 'OCCURRENCES':
                    $('#id_recurssion_ending_sub_options_occurrences').prop('disabled', false);
                    $('#id_recurssion_ending_sub_options_on_date').prop('disabled', true);
                    $('#id_enddate_day, #id_enddate_month, #id_enddate_year').prop('disabled', true);
                    break;
                case 'ON':
                    $('#id_recurssion_ending_sub_options_occurrences').prop('disabled', true);
                    $('#id_recurssion_ending_sub_options_on_date').prop('disabled', true);
                    $('#id_enddate_day, #id_enddate_month, #id_enddate_year').prop('disabled', false);
            }
        });

        $('#mform1').submit(function() {
            populateForm();
        });
    };

    function checkMonthOption() {
        $('#id_repeat_meeting_week_of_month').change(function () {
            var str = "";
            $( "#id_repeat_meeting_week_of_month option:selected" ).each(function() {
                str = $(this).text();
            });
            switch(str) {
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
        // Recurrence type
        if ($('#id_repeat_meeting').val() == 'DAILY' &&
            $('#id_repeat_meeting_sub_options_daily_weekdays').prop('checked')) {

            $('input[name="recurrenceType"]').val('WEEKLY')

        } else {
            $('input[name="recurrenceType"]').val($('#id_repeat_meeting').val());
        }

        // End Date or Recurrence Count
        switch($('input[name="recurssion_ending"]:checked').val()) {
            case 'NEVER':
                $('input[name="endDate"]').val('0');
                $('input[name="recurrenceCount"]').val('0');
                break;
            case 'OCCURRENCES':
                $('input[name="endDate"]').val('0');
                $('input[name="recurrenceCount"]').val($('#id_recurssion_ending_sub_options_occurrences').val());
                break;
            case 'ON':
                $('input[name="endDate"]').val('DATE');
                $('input[name="recurrenceCount"]').val('0');
                break;
        }

        // Frequency
        $('input[name="frequency"]').val($('#id_repeat_meeting_frequency').val());

        // Day of the Week Mask
        $('input[name="daysOfWeekMask"]').val(mask());

        // Day of the month
        $('input[name="dayOfMonth"]').val(dayOfMonth());


        // Week of the Month
        $('input[name="weekOfMonth"]').val(weekOfTheMonth());

        // Month of the Year
        $('input[name="monthOfYear"]').val('NONE');
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

    return recurring;
});