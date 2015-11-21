<?php
require_once('../../config.php');
require_once('connector.php');
global $DB, $OUTPUT, $PAGE;
$connector = new connector();

$courseid = required_param('courseid', PARAM_INT);
$numericmeetingid = required_param('numericmeetingid', PARAM_INT);

if (!$course = $DB->get_record('course', array('id' => $courseid))) {
    print_error('invalidcourse', 'block_bjn', $courseid);
}

require_login($course);

$PAGE->set_url('/blocks/bjn/video.php', array('id' => $courseid));
$PAGE->set_pagelayout('standard');
$PAGE->set_title('Virtual Classroom');
$PAGE->requires->css('/blocks/bjn/vendor/jquery-ui.min.css');
$PAGE->requires->css('/blocks/bjn/vendor/jquery-ui.theme.min.css');
$PAGE->requires->js_call_amd('block_bjn/videoaccordion', 'init');

$downloadurls = $connector->get_download_urls($numericmeetingid);

echo $OUTPUT->header();

$display = $OUTPUT->heading(get_string('videopagetitle', 'block_bjn'));

$bjnpicurl = new moodle_url('/blocks/bjn/pix/BJN_logowtext@2x.png');
$display .= html_writer::div(
    '<img src="'. $bjnpicurl .'">',
    'bjn-branding bjn-logo-header'
);

if ($downloadurls) {
    $display .= '<div id="accordion">';
    $i = 1;
    foreach ($downloadurls as $url) {
        $display .= '<h3>Video '. $i .'</h3>';
        $display .= '<div>';
        $display .= '<p>';

        $display .= '<video controls="controls" width="640" height="360">';
        $display .= '<source src="'. $url .'" type="video/mp4" />';
		$display .= '<span title="No video playback capabilities, please download the video below">Big Buck Bunny</span>';
	    $display .= '</object>';
        $display .= '</video>';
        $display .= '<p>';
	    $display .= '<strong>Download video:</strong> <a href="'. $url .'">MP4 format</a>';
        $display .= '</p>';

        $display .= '</p>';
        $display .= '</div>';
        $i++;
    }
    $display .= '</div>';
} else {
    $display .= "No video recordings for this class";
}

echo $display;

echo $OUTPUT->footer();