<?php

require_once('../../config.php');
require_once('bjn_form.php');
require_once('connector.php');
require_once('lib.php');

global $DB, $OUTPUT, $PAGE, $USER;
$connector = new connector();

// Check for all required variables
$courseid = required_param('courseid', PARAM_INT);
$blockid = required_param('blockid', PARAM_INT);

// Look for optional variables.
$id = optional_param('id', 0, PARAM_INT);
$meetingid = optional_param('meetingid', 0, PARAM_INT);
$viewpage = optional_param('viewpage', false, PARAM_BOOL);
$success = optional_param('success', false, PARAM_INT);

$bjn_ids = array(
    'course' => $courseid,
    'block' => $blockid,
    'id' => $id,
    'meeting' => $meetingid
);

if (!$course = $DB->get_record('course', array('id' => $courseid))) {
    print_error('invalidcourse', 'block_bjn', $courseid);
}

require_login($course);

$PAGE->set_url('/blocks/bjn/view.php', array('id' => $courseid));
$PAGE->set_pagelayout('standard');
$PAGE->set_title('Virtual Classroom');

$context = context_course::instance($COURSE->id);
if (has_capability('block/bjn:managepages', $context) && $meetingid) {
    $settingsnode = $PAGE->settingsnav->add(get_string('bjnsettings', 'block_bjn'));
    $editurl = new moodle_url('/blocks/bjn/view.php', array('id' => $id, 'courseid' => $courseid, 'blockid' => $blockid, 'meetingid' => $meetingid));
    $editnode = $settingsnode->add(get_string('editpage', 'block_bjn'), $editurl);
    $editnode->make_active();
}

$heading = "";
$submitlabel = "";
if ($meetingid) {
    $heading = get_string('updatemeeting', 'block_bjn');
    $submitlabel = get_string('updatelabel', 'block_bjn');
} else {
    $heading = get_string('schedulemeeting', 'block_bjn');
    $submitlabel = get_string('createlabel', 'block_bjn');
}

$bjn = new bjn_form(null, array(
    'courseid' => $courseid,
    'users' => get_fullnames_from(get_enrolled_users($context)),
    'heading' => $heading,
    'submitlabel' => $submitlabel
));

// Hidden form items
$toform['blockid'] = $blockid;
$toform['courseid'] = $courseid;
$toform['id'] = $id;
$toform['meetingid'] = $meetingid;
$toform['email'] = $USER->email;
$toform['timezone'] = usertimezone();
$bjn->set_data($toform);

// If the user hits the cancel button on the form
if ($bjn->is_cancelled()) {
    // Cancelled forms redirect to the course main page
    $courseurl = new moodle_url('/course/view.php', array('id' => $courseid));
    redirect($courseurl);
// If the user tries to save data
} else if ($fromform = $bjn->get_data()) {
    $fromform->meetingdesc = $fromform->meetingdesc['text'];
    // Code to appropriately act on and store submitted data
    if ($fromform->id != 0) {
        // Update
        //print_object($fromform);
        $record = $DB->get_record('block_bjn', ['meetingid' => $meetingid]);
        if ($record->email != $USER->email) {
            $classurl = new moodle_url('/blocks/bjn/view.php', array(
                'blockid' => $blockid,
                'courseid' => $courseid,
                'id' => $id,
                'meetingid' => $meetingid,
                'viewpage' => 1,
                'success' => 2
            ));
            redirect($classurl);
        }

        if (!$response = $connector->update_meeting($meetingid, $fromform)) {
            print_error('apierror', 'bjn_block');
        } else {
            //print_object($response);
            $bjnmeeting = convert_to_moodle_format($fromform, json_decode($response)->id);

            if (!$DB->update_record('block_bjn', $bjnmeeting)) {
                print_error('updateerror', 'block_bjn');
            }
        }

        $classurl = new moodle_url('/blocks/bjn/view.php', array(
            'blockid' => $blockid,
            'courseid' => $courseid,
            'id' => $id,
            'meetingid' => $meetingid,
            'viewpage' => 1,
            'success' => 1
        ));
        redirect($classurl);
    } else {
        // Create
        //print_object($fromform);
        /*print_object(json_encode($bjn->get_data()));
        $response = $connector->schedule_meeting($fromform);
        print_object($response);*/
        if (!$response = $connector->schedule_meeting($fromform)) {
            print_error('apierror', 'block_bjn');
        } else {
            $bjnmeeting = convert_to_moodle_format($fromform, json_decode($response)->id);

            if (!$row = $DB->insert_record('block_bjn', $bjnmeeting)) {
                print_error('inserterror', 'block_bjn');
            }
        }

        $classurl = new moodle_url('/blocks/bjn/view.php', array(
            'blockid' => $blockid,
            'courseid' => $courseid,
            'id' => $row,
            'meetingid' => json_decode($response)->id,
            'viewpage' => 1,
            'success' => 1
        ));
        redirect($classurl);
    }
} else {
    $site = get_site();
    echo $OUTPUT->header();
    if ($id) {
        $bjnpage = $connector->get_meeting($meetingid);

        if ($viewpage) {
            // Get moderator passcode
            $userroom = $connector->get_user_room_settings();

            // View a scheduled meeting
            block_bjn_print_page($bjnpage, $bjn_ids, $userroom->moderatorPasscode);
            $PAGE->requires->js_call_amd('block_bjn/success', 'init', array($success));
            $PAGE->requires->js_call_amd('block_bjn/lib', 'toggleMeetingInfo');
        } else {
            // Display the update form
            $users = get_enrolled_users($context);
            $userinfo = get_user_info($users);
            $PAGE->requires->js_call_amd('block_bjn/modal', 'init', array($userinfo));
            $PAGE->requires->js_call_amd('block_bjn/recurring', 'init');
            $bjnpage = convert_response_to_form_data($bjnpage);
            //print_object($bjnpage);
            $bjn->set_data($bjnpage);
            $bjn->display();
        }
    } else {
        // Display the create form
        $users = get_enrolled_users($context);
        $userinfo = get_user_info($users);
        $PAGE->requires->js_call_amd('block_bjn/modal', 'init', array($userinfo));
        $PAGE->requires->js_call_amd('block_bjn/recurring', 'init');
        $bjn->display();
    }

    echo $OUTPUT->footer();
}