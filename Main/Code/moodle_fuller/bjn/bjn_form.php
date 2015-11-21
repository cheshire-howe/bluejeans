<?php
require_once("{$CFG->libdir}/formslib.php");
require_once("{$CFG->dirroot}/blocks/bjn/lib.php");

class bjn_form extends moodleform {

    function definition() {
        $mform =& $this->_form;

        $mform->registerRule('ensurenumeric', 'callback', 'ensure_numeric');

        function ensure_numeric($arr) {
            $numbers = '/^d+$/';
            if (!($arr['number'] > 0)) {
                return false;
            }
            return true;
        }

        $mform->registerRule('ensureinfuture', 'callback', 'ensure_in_future');

        function ensure_in_future($timeinput) {
            $unixtimeinput = make_timestamp($timeinput['year'],
                                            $timeinput['month'],
                                            $timeinput['day'],
                                            $timeinput['hour'],
                                            $timeinput['minute']);
            if (time() > $unixtimeinput) {
                return false;
            }
            return true;
        }

        $mform->addElement('header', 'displayinfo', $this->_customdata['heading']);

        /*$bjnpicurl = new moodle_url('/blocks/bjn/pix/bluejeans-logo-small.png');
        $mform->addElement('html', html_writer::tag('img', '', array(
            'src' => $bjnpicurl,
            'alt' => 'Bluejeans',
            'class' => 'bjnmeeting logo'
        )));*/

        $bjnpicurl = new moodle_url('/blocks/bjn/pix/BJN_logowtext@2x.png');
        $mform->addElement('html', html_writer::div(
            '<img src="'. $bjnpicurl .'">',
            'form bjn-branding'
        ));

        $mform->addElement('html', html_writer::start_tag('div', array(
            'class' => 'bjnmeeting formitems'
        )));

        // Meeting title
        $mform->addElement('text', 'meetingtitle', get_string('meetingtitle', 'block_bjn'));
        $mform->addRule('meetingtitle', null, 'required', null, 'client');
        $mform->setType('meetingtitle', PARAM_TEXT);

        // Start time
        $default_class_starts_from_now_in_seconds = time() + 900; // 15 minutes

        $mform->addElement('date_time_selector', 'starttime', get_string('starttime', 'block_bjn'));
        $mform->addRule('starttime', null, 'required', null, 'client');
        //$mform->addRule('starttime', get_string('starttime_future', 'block_bjn'), 'ensureinfuture');
        $mform->setDefault('starttime', $default_class_starts_from_now_in_seconds);
        $mform->setType('starttime', PARAM_INT);

        // Duration
        $default_class_length_in_seconds = 7200;

        $mform->addElement('duration', 'classlength', get_string('classlength', 'block_bjn'), array(
            'optional' => false,
            'defaultunit' => 3600
        ), array(
            'class' => 'classlength'
        ));
        $mform->addRule('classlength', get_string('classlength_required', 'block_bjn'), 'required', null, 'client');
        $mform->addRule('classlength', get_string('classlength_numeric', 'block_bjn'), 'ensurenumeric');
        $mform->setDefault('classlength', $default_class_length_in_seconds);
        $mform->setType('classlength', PARAM_INT);

        $mform->addElement('html', html_writer::start_tag('div', array(
            'class' => 'fitem selectall-attendees',
            'id' => 'selectall-attendees'
        )));

        $mform->addElement('html', html_writer::start_tag('div', array(
            'class' => 'fitemtitle'
        )));

        $mform->addElement('html', html_writer::label(get_string('attendees', 'block_bjn'), 'showAllStudents'));

        $mform->addElement('html', html_writer::end_tag('div'));

        $mform->addElement('html', html_writer::start_tag('fieldset', array(
            'class' => 'felement'
        )));
        $mform->addElement('html', html_writer::tag('div', 'Select Students', array(
            'id' => 'showAllStudents',
            'class' => 'select-student-btn',
            'data-toggle' => 'modal',
            'data-target' => '#attendeesModal'
        )));

        $mform->addElement('html', html_writer::end_tag('fieldset'));

        // Students Modal
        $modal = file_get_contents('templates/modal.html');
        $mform->addElement('html', $modal);

        $mform->addElement('html', html_writer::end_tag('div'));

        // Repeat?
        $mform->addElement('html', html_writer::start_div('recurring'));
        $mform->addElement('checkbox', 'use_repeat_meeting', 'Repeat Meeting');
        $mform->addElement('html',
        '<div id="reccurrence_options" class="recurrence_options_container" style="display: none;">
            <hr />
            <div class="meetingUseRepeatMeeting_error">

            </div>
            <div class="fieldWrapper fitem" id="div_repeat_meeting_type">
                <div class="repeatMeetingType_l fitemtitle">
                    <label for="id_repeat_meeting">Repeats:</label>
                </div>
                <div id="repeatMeetingType" class="felement">
                    <select name="repeat_meeting" id="id_repeat_meeting">
                        <option value="DAILY" selected="selected">Daily</option>
                        <option value="WEEKLY">Weekly</option>
                        <option value="MONTHLY">Monthly</option>
                    </select>
                <span id="frequency_monthly_weekly" style="display: none;">
                  every
                  &nbsp;<input name="repeat_meeting_frequency" value="1" maxlength="2" type="text" id="id_repeat_meeting_frequency" size="2" aria-label="Weekly Repeat Frequency">&nbsp;
                  <span id="frequency_unit"></span>

                </span>
                </div>
            </div>
            <div class="clear"></div>


            <div class="fieldWrapper fitem" id="div_daily_options" style="display: block;">
                <div class="dailyOptions_l fitemtitle">&nbsp;</div>
                <div id="dailyOptions" class="felement">
                    <input type="radio" id="id_repeat_meeting_sub_options_daily_weekdays" name="repeat_meeting_sub_options_daily_weekdays" value="True">
                    <label for="id_repeat_meeting_sub_options_daily_weekdays">Weekdays</label>
                    <input type="radio" id="id_repeat_meeting_sub_options_daily_weekdays_1" name="repeat_meeting_sub_options_daily_weekdays" checked="checked" value="False">
                    <label for="id_repeat_meeting_sub_options_daily_weekdays_1">every</label> <input name="repeat_meeting_frequency_daily" value="1" maxlength="3" type="text" id="id_repeat_meeting_frequency_daily" size="3" aria-label="Daily Repeat Frequency">&nbsp;days

                </div>
            </div>
            <div class="clear"></div>

            <div class="fieldWrapper fitem" id="div_weekly_options" style="display: none;">
                <div class="weeklyOptions_1 fitemtitle">
                    <label for="id_repeat_meeting_day_of_week_weekly_0">Repeats On:</label>
                </div>
                <div id="weeklyOptions" class="felement">
                    <ul>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_0"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="2" id="id_repeat_meeting_day_of_week_weekly_0"> Monday</label></li>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_1"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="4" id="id_repeat_meeting_day_of_week_weekly_1"> Tuesday</label></li>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_2"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="8" id="id_repeat_meeting_day_of_week_weekly_2"> Wednesday</label></li>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_3"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="16" id="id_repeat_meeting_day_of_week_weekly_3"> Thursday</label></li>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_4"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="32" id="id_repeat_meeting_day_of_week_weekly_4"> Friday</label></li>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_5"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="64" id="id_repeat_meeting_day_of_week_weekly_5"> Saturday</label></li>
                        <li><label for="id_repeat_meeting_day_of_week_weekly_6"><input type="checkbox" name="repeat_meeting_day_of_week_weekly" value="1" id="id_repeat_meeting_day_of_week_weekly_6"> Sunday</label></li>
                    </ul>
                </div>
            </div>
            <div class="clear"></div>

            <div class="fieldWrapper fitem" id="div_monthly_options" style="display: none;">
                <div class="monthlyOptions_1 fitemtitle">
                    <label for="id_repeat_meeting_week_of_month">On:</label>
                </div>
                <div id="monthlyOptions" class="felement">
                    <select name="repeat_meeting_week_of_month" id="id_repeat_meeting_week_of_month">
                        <option value="0">Day</option>
                        <option value="FIRST">First</option>
                        <option value="SECOND">Second</option>
                        <option value="THIRD">Third</option>
                        <option value="FOURTH">Fourth</option>
                        <option value="FIFTH">Last</option>
                    </select>
                    <input type="text" name="repeat_meeting_day_of_month" value="'. date('d', time()) .'" id="id_repeat_meeting_day_of_month" aria-label="">
                    <select name="repeat_meeting_day_of_week_monthly" id="id_repeat_meeting_day_of_week_monthly" aria-label="Repeat on Day of Week" style="display: none;">
                        <option value="1">Sunday</option>
                        <option value="2">Monday</option>
                        <option value="4">Tuesday</option>
                        <option value="8">Wednesday</option>
                        <option value="16">Thursday</option>
                        <option value="32">Friday</option>
                        <option value="64">Saturday</option>
                    </select>
                    &nbsp;of every month
                </div>
            </div>
            <div class="clear"></div>

            <hr />

            <div id="div_recurrence_ending">
                <div class="fieldWrapper fitem" id="div_recurrence_ending_1">
                    <div class="recurrenceEnding_1 fitemtitle">
                        <label for="id_recurssion_ending_0">Ends:</label>
                    </div>
                    <div id="recurrenceEnding_1" class="felement">
                        <input id="id_recurssion_ending_0" type="radio" name="recurssion_ending" value="NEVER" checked="checked">
                        <label for="id_recurssion_ending_0">Never</label>
                    </div>
                </div>
                <div class="clear"></div>

                <div class="fieldWrapper fitem" id="div_recurrence_ending_2">
                    <div class="recurrenceEnding_2 fitemtitle">
                        &nbsp;
                    </div>
                    <div id="recurrenceEnding_2" class="felement">
                        <input id="id_recurssion_ending_1" type="radio" name="recurssion_ending" value="OCCURRENCES">
                        <label for="id_recurssion_ending_1">After</label>
                        <input name="recurssion_ending_sub_options_occurrences" value="2" maxlength="3" type="text" id="id_recurssion_ending_sub_options_occurrences" size="3" aria-label="Number of Ocurrences" disabled=""> occurrences

                    </div>
                </div>
                <div class="clear"></div>

                <div class="fieldWrapper fitem" id="div_recurrence_ending_3">
                    <div class="recurrenceEnding_3 fitemtitle">
                        &nbsp;
                    </div>
                    <div id="recurrenceEnding_3" class="felement">
                        <div class="bjn-recurrence-left">

                        </div>
                            <div class="bjn-recurrence-right">
                                <input id="id_recurssion_ending_2" type="radio" name="recurssion_ending" value="ON"><label for="id_recurssion_ending_2">On</label>');
                                $mform->addElement('date_selector', 'enddate', '', null, array('disabled' => true));
                                $mform->setType('enddate', PARAM_INT);
                            $mform->addElement('html', '
                        </div>
                    </div>
                </div>
                <hr />
            </div>
            <div class="clear"></div>
        </div>'
        );
        $mform->addElement('html', html_writer::end_div());

        // Attendee Passcode?
        $mform->addElement('selectyesno', 'passcode', get_string('passcode', 'block_bjn'));

        // Show Participants in Email?
        $mform->addElement('selectyesno', 'participantsinemail', get_string('participantsinemail', 'block_bjn'));

        // Description
        $mform->addElement('editor', 'meetingdesc', get_string('meetingdesc', 'block_bjn'));
        $mform->setType('meetingdesc', PARAM_RAW);

        // Advanced Meeting Options
        // Encrypt?
        /*$mform->addElement('selectyesno', 'encrypt', get_string('encrypt', 'block_bjn'));
        $mform->setAdvanced('encrypt');

        // Crop?
        $mform->addElement('selectyesno', 'allow720', get_string('allow720', 'block_bjn'));
        $mform->setAdvanced('allow720');*/

        // Moderator-less Meeting?
        $mform->addElement('selectyesno', 'moderatorless', get_string('moderatorless', 'block_bjn'));
        $mform->setAdvanced('moderatorless');

        // Auto-record?
        $mform->addElement('selectyesno', 'autorecord', get_string('autorecord', 'block_bjn'));
        $mform->setAdvanced('autorecord');

        // Disable Chat?
        $mform->addElement('selectyesno', 'disallowchat', get_string('disallowchat', 'block_bjn'));
        $mform->setAdvanced('disallowchat');

        // Mute Participants on Entry?
        $mform->addElement('selectyesno', 'muteparticipants', get_string('muteparticipants', 'block_bjn'));
        $mform->setAdvanced('muteparticipants');

        $mform->addElement('html', html_writer::end_tag('div'));
        $mform->addElement('html', html_writer::tag('div', '', array('class' => 'clearfix')));

        // hidden elements
        $mform->addElement('hidden', 'blockid');
        $mform->setType('blockid', PARAM_INT);
        $mform->addElement('hidden', 'courseid');
        $mform->setType('courseid', PARAM_INT);
        $mform->addElement('hidden', 'id', 0);
        $mform->setType('id', PARAM_INT);
        $mform->addElement('hidden', 'meetingid', 0);
        $mform->setType('meetingid', PARAM_INT);
        $mform->addElement('hidden', 'timezone');
        $mform->setType('timezone', PARAM_RAW);
        $mform->addElement('hidden', 'attendees');
        $mform->setType('attendees', PARAM_RAW);
        $mform->addElement('hidden', 'email');
        $mform->setType('email', PARAM_RAW);

        // Recurring hidden elements
        $mform->addElement('hidden', 'recurrenceType');
        $mform->setType('recurrenceType', PARAM_RAW);
        $mform->addElement('hidden', 'endDate');
        $mform->setType('endDate', PARAM_RAW);
        $mform->addElement('hidden', 'recurrenceCount');
        $mform->setType('recurrenceCount', PARAM_RAW);
        $mform->addElement('hidden', 'frequency');
        $mform->setType('frequency', PARAM_RAW);
        $mform->addElement('hidden', 'daysOfWeekMask');
        $mform->setType('daysOfWeekMask', PARAM_RAW);
        $mform->addElement('hidden', 'dayOfMonth');
        $mform->setType('dayOfMonth', PARAM_RAW);
        $mform->addElement('hidden', 'weekOfMonth');
        $mform->setType('weekOfMonth', PARAM_RAW);
        $mform->addElement('hidden', 'monthOfYear');
        $mform->setType('monthOfYear', PARAM_RAW);

        // Submit/Cancel
        $this->add_action_buttons(true, $this->_customdata['submitlabel']);
    }
}