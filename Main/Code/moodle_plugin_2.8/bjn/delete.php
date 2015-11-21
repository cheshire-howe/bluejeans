<?php
require_once('../../config.php');
require_once('connector.php');

global $USER;

$connector = new connector();

$courseid = required_param('courseid', PARAM_INT);
$id = optional_param('id', 0, PARAM_INT);
$meetingid = optional_param('meetingid', 0, PARAM_INT);
$confirm = optional_param('confirm', 0, PARAM_INT);

if (!$course = $DB->get_record('course', array('id' => $courseid))) {
    print_error('invalidcourse', 'block_bjn', $courseid);
}

require_login($course);
require_capability('block/bjn:managepages', context_course::instance($courseid));

if (!$bjnpage = $DB->get_record('block_bjn', array('id' => $id))) {
    print_error('nomeeting', 'block_bjn', '', $id);
}

if ($bjnpage->email != $USER->email) {
    $classurl = new moodle_url('/blocks/bjn/view.php', array(
        'blockid' => $blockid,
        'courseid' => $courseid,
        'id' => $id,
        'meetingid' => $meetingid,
        'viewpage' => 1,
        'success' => 3
    ));
    redirect($classurl);
}

$site = get_site();
$PAGE->set_url('/blocks/bjn/view.php', array('id' => $id, 'courseid' => $courseid));
$heading = $site->fullname .' :: '. $course->shortname .' :: '. $bjnpage->meetingtitle;
$PAGE->set_heading($heading);

if (!$confirm) {
    echo $OUTPUT->header();
    $optionsno = new moodle_url('/course/view.php', array('id' => $courseid));
    $optionsyes = new moodle_url('/blocks/bjn/delete.php', array(
        'id' => $id,
        'meetingid' => $meetingid,
        'courseid' => $courseid,
        'confirm' => 1,
        'sesskey' => sesskey()
    ));
    echo $OUTPUT->confirm(get_string('deletepage', 'block_bjn', $bjnpage->meetingtitle), $optionsyes, $optionsno);
} else {
    if (confirm_sesskey()) {
        if ($response = $connector->delete_meeting($meetingid)) {
            print_object($response);
            print_error('apierror', 'block_bjn');
        } else {
            if (!$DB->delete_records('block_bjn', array('id' => $id))) {
                print_error('deleteerror', 'block_bjn');
            }
        }
    } else {
        print_error('sessionerror', 'block_bjn');
    }

    $url = new moodle_url('/course/view.php', array('id' => $courseid));
    redirect($url);
}

echo $OUTPUT->footer();