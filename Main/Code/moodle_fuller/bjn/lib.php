<?php

function get_fullnames_from($enrolled_users) {
    $fullnames = array();
    foreach ($enrolled_users as $user) {
        $fullnames[$user->email] = $user->firstname .' '. $user->lastname;
    }
    return $fullnames;
}

function get_user_info($enrolled_users) {
    $user_info = array();
    foreach ($enrolled_users as $user) {
        $user_info[$user->id] = array(
            'email' => $user->email,
            'name' => $user->firstname .' '. $user->lastname,
            'picurl' => '/user/pix.php/'. $user->id .'/f1.jpg'
        );
    }
    return $user_info;
}

function block_bjn_print_page($bjn, $bjn_id, $mod_passcode, $return=false){
    global $OUTPUT, $COURSE;

    $context = context_course::instance($COURSE->id);

    $display = html_writer::start_tag('div', array('class' => 'bjn meeting bjn-box'));

    $display .= $OUTPUT->heading($bjn->title);

    $bjnpicurl = new moodle_url('/blocks/bjn/pix/BJN_logowtext@2x.png');
    $display .= html_writer::div(
        '<img src="'. $bjnpicurl .'">',
        'bjn-branding bjn-logo-header'
    );

    if (has_capability('block/bjn:managepages', $context) && $bjn_id['meeting']) {
        // Edit
        $editurl = new moodle_url('/blocks/bjn/view.php', array(
            'id' => $bjn_id['id'],
            'courseid' => $bjn_id['course'],
            'blockid' => $bjn_id['block'],
            'meetingid' => $bjn_id['meeting'])
        );
        $display .= html_writer::link($editurl, 'Edit Class');
        $display .= ' | ';

        // Delete
        $deleteurl = new moodle_url('/blocks/bjn/delete.php', array(
            'courseid' => $bjn_id['course'],
            'id' => $bjn_id['id'],
            'meetingid' => $bjn_id['meeting']
        ));
        $display .= html_writer::link($deleteurl, 'Delete Class');
    }

    $display .= html_writer::tag('hr', '');

    $display .= html_writer::div('', 'alert alert-success', array('id' => 'bjnmessage','style' => 'display:none;'));

    $display .= $OUTPUT->box_start();
    $display .= html_writer::start_tag('div', array('class' => 'bjnmeeting'));

    $display .= html_writer::start_div('left');

    // Meeting times
    $display .= html_writer::start_div('bjnmeeting times');
    $display .= html_writer::tag('h2', 'Time of Class');

    // Recurring
    if (property_exists($bjn, 'recurrencePattern')) {
        $display .= html_writer::start_div('bjnmeeting recurring');
        if ($bjn->recurrencePattern->recurrenceType == 'DAILY') {
            $display .= html_writer::start_tag('em');
            $display .= 'Every day';
            $display .= html_writer::end_tag('em');
        } else if ($bjn->recurrencePattern->recurrenceType == 'WEEKLY') {
            if ($bjn->recurrencePattern->daysOfWeekMask == '62') {
                $display .= html_writer::start_tag('em');
                $display .= 'Weekdays';
                $display .= html_writer::end_tag('em');
            } else {
                if ($bjn->recurrencePattern->frequency == '1') {
                    $display .= html_writer::start_tag('em');
                    $display .= 'Every ';
                    $display .= decode_day_mask($bjn->recurrencePattern->daysOfWeekMask);
                    $display .= html_writer::end_tag('em');
                } else {
                    $display .= html_writer::start_tag('em');
                    $display .= 'Every '. number_to_words($bjn->recurrencePattern->frequency) .' ';
                    $display .= decode_day_mask($bjn->recurrencePattern->daysOfWeekMask);
                    $display .= html_writer::end_tag('em');
                }
            }
        } else if ($bjn->recurrencePattern->recurrenceType == 'MONTHLY') {
            if ($bjn->recurrencePattern->frequency == '1' && $bjn->recurrencePattern->daysOfWeekMask != '0') {
                $display .= html_writer::start_tag('em');
                $display .= 'Every '. caps_to_words($bjn->recurrencePattern->weekOfMonth) .' ';
                $display .= decode_day_mask($bjn->recurrencePattern->daysOfWeekMask);
                $display .= ' of the month';
                $display .= html_writer::end_tag('em');
            } else if ($bjn->recurrencePattern->frequency != '1') {
                $display .= html_writer::start_tag('em');
                $display .= 'Every '. caps_to_words($bjn->recurrencePattern->weekOfMonth) .' ';
                $display .= decode_day_mask($bjn->recurrencePattern->daysOfWeekMask);
                $display .= ' of every '. number_to_words($bjn->recurrencePattern->frequency) .' month';
                $display .= html_writer::end_tag('em');
            } else {
                $display .= html_writer::start_tag('em');
                $display .= 'Every '. number_to_words($bjn->recurrencePattern->dayOfMonth) .' ';
                $display .= ' of the month';
                $display .= html_writer::end_tag('em');
            }
        }
        $display .= html_writer::end_div();
    }

    $display .= 'Next Class - ';
    $display .= userdate(decode_date($bjn->next->start)). ' / ';
    $display .= duration($bjn->next->start, $bjn->next->end);
    $display .= html_writer::end_div(); // End Meeting Times

    if ($bjn->description) {
        $display .= html_writer::tag('h2', 'Message');
        $display .= $bjn->description;
    }

    $display .= html_writer::end_div();

    $display .= html_writer::start_div('right');

    if ($bjn->attendees) {
        $display .= html_writer::start_tag('div', array('class' => 'bjnmeeting attendees'));
        $display .= html_writer::tag('h2', 'Invitees');
        $display .= html_writer::start_tag('ul');
        foreach ($bjn->attendees as $attendee) {
            $display .= html_writer::tag('li', $attendee->email);
        }
        $display .= html_writer::end_tag('ul');
        $display .= html_writer::end_tag('div');
    }

    $display .= html_writer::end_div();

    $display .= html_writer::div('', 'clear');

    // Location
    $display .= html_writer::start_div('bjnmeeting location');
    $display .= html_writer::tag('h2', 'Location');
    $display .= html_writer::tag('h5', 'BlueJeans Video Conference');
    $display .= html_writer::end_div();

    // Join meeting button
    if (has_capability('block/bjn:managepages', $context) && $bjn_id['meeting']) {
        $display .= html_writer::start_tag('div', array('class' => 'bjnmeeting info'));

        $display .= html_writer::start_tag('div', array(
            'id' => 'btn-join',
            'class' => 'btn btn-success'
        ));
        $display .= html_writer::link('https://bluejeans.com/'.
            $bjn->numericMeetingId .'/' .
            $mod_passcode,
            'Join Meeting as Moderator',
            array('target' => '_blank'));

        $display .= html_writer::end_tag('div'); // End Join button

        // Recordings
        $display .= html_writer::start_div('bjnmeeting recordings');
        $display .= html_writer::tag('h2', 'Recordings');
        $display .= html_writer::end_div();

        // Video button link
        $display .= html_writer::start_tag('div', array(
            'id' => 'btn-video',
            'class' => 'btn btn-success'
        ));
        $display .= html_writer::link(new moodle_url('/blocks/bjn/video.php', array(
            'numericmeetingid' => $bjn->numericMeetingId,
            'courseid' => $COURSE->id
        )), get_string('videolink', 'block_bjn'));
        $display .= html_writer::end_tag('div'); // End recordings

        // Meeting Info Click to open thingy
        $display .= html_writer::div('<h2>Meeting Information</h2>', '', array(
            'id' => 'meeting-info-trigger',
            'style' => 'cursor: pointer;'
        ));

        $display .= html_writer::start_div('', array(
            'id' => 'meeting-info',
            'style' => 'display: none;'
        ));
        if ($bjn->addAttendeePasscode) {
            // Meeting Info
            $display .= html_writer::tag('dt', 'Participant Passcode');
            $display .= html_writer::tag('dd', $bjn->attendeePasscode);
            /*$display .= html_writer::tag('dt', 'Moderator Passcode');
            $display .= html_writer::tag('dd', $bjn->?);*/
            $display .= html_writer::tag('dt', 'Meeting URL');
            $display .= html_writer::tag('dd', 'https://bluejeans.com/'.
                $bjn->numericMeetingId .'/'.
                $bjn->attendeePasscode);
            $display .= html_writer::end_tag('dd');
            $display .= html_writer::end_tag('dl');
        } else {
            // Meeting Info
            /*$display .= html_writer::tag('dt', 'Moderator Passcode');
            $display .= html_writer::tag('dd', $bjn->?);*/
            $display .= html_writer::tag('dt', 'Meeting URL');
            $display .= html_writer::tag('dd', 'https://bluejeans.com/'. $bjn->numericMeetingId);
            $display .= html_writer::end_tag('dd');
            $display .= html_writer::end_tag('dl');
        }
        $display .= html_writer::end_div();

        $display .= html_writer::end_tag('div');
    } else {
        $display .= html_writer::start_tag('div', array('class' => 'bjnmeeting info'));
        $display .= html_writer::start_tag('div', array(
            'id' => 'btn-join',
            'class' => 'btn btn-success'
        ));
        if ($bjn->addAttendeePasscode) {
            $display .= html_writer::link('https://bluejeans.com/'.
                $bjn->numericMeetingId .'/'.
                $bjn->attendeePasscode,
                'Join Meeting as Student',
                array('target' => '_blank'));
        } else {
            $display .= html_writer::link('https://bluejeans.com/'.
                $bjn->numericMeetingId .'/',
                'Join Meeting as Student',
                array('target' => '_blank'));
        }
        $display .= html_writer::end_tag('div');

        // Recordings
        $display .= html_writer::start_div('bjnmeeting recordings');
        $display .= html_writer::tag('h2', 'Recordings');
        $display .= html_writer::end_div();

        // Video button link
        $display .= html_writer::start_tag('div', array(
            'id' => 'btn-video',
            'class' => 'btn btn-success'
        ));
        $display .= html_writer::link(new moodle_url('/blocks/bjn/video.php', array(
            'numericmeetingid' => $bjn->numericMeetingId,
            'courseid' => $COURSE->id
        )), get_string('videolink', 'block_bjn'));
        $display .= html_writer::end_tag('div'); // End recordings

        $display .= html_writer::end_tag('div');
    }

    $display .= html_writer::end_tag('div');
    $display .= $OUTPUT->box_end();

    $display .= html_writer::end_tag('div');

    if ($return) {
        return $display;
    } else {
        echo $display;
    }
}

function convert_to_moodle_format($formdata, $meetingid) {
    return array(
        'id' => $formdata->id,
        'blockid' => $formdata->blockid,
        'meetingtitle' => $formdata->meetingtitle,
        'meetingid' => $meetingid,
        'courseid' => $formdata->courseid,
        'email' => $formdata->email,
        'starttime' => $formdata->starttime,
        'endtime' => $formdata->starttime + $formdata->classlength
    );
}

function convert_response_to_form_data($response) {
    $adv = $response->advancedMeetingOptions;
    return array(
        'meetingtitle' => $response->title,
        'starttime' => decode_date($response->start),
        'classlength' => decode_date($response->end - $response->start),
        'repeat' => false,
        'passcode' => $response->addAttendeePasscode,
        'attendees' => decode_attendee_list($response->attendees),
        'meetingdesc' => array(
            'text' => $response->description,
            'format' => 1,
        ),
        'encrypt' => decode_encryption_string($adv->encryptionType),
        'allow720' => $response->allow720p,
        'moderatorless' => $adv->moderatorLess,
        'disallowchat' => $adv->disallowChat,
        'muteparticipants' => $adv->muteParticipantsOnEntry
    );
}

function encode_attendee_list($attendees) {
    $attendee_arr = explode(',', $attendees);
    $attendee_arr_clean = array();

    foreach ($attendee_arr as $attendee) {
        $attendee_element = array(
            'email' => $attendee,
        );
        array_push($attendee_arr_clean, $attendee_element);
    }
    return $attendee_arr_clean;
}

function decode_attendee_list($attendee_arr) {
    /*$attendees = array();*/
    $attendees = "";
    foreach ($attendee_arr as $attendee_element) {
        $attendees .= $attendee_element->email .",";
        /*array_push($attendees, $attendee_element->email);*/
    }
    $attendees = rtrim($attendees, ',');
    return $attendees;
}

function encode_encryption_string($encrypt) {
    switch ($encrypt) {
        case "0":
            return "NO_ENCRYPTION";
        case "1":
            return "ENCRYPTED_ONLY";
        case "2":
            return "ENCRYPTED_OR_PSTN_ONLY";
    }
}

function decode_encryption_string($encrypt) {
    switch ($encrypt) {
        case "NO_ENCRYPTION":
            return "0";
        case "ENCRYPTED_ONLY":
            return "1";
        case "ENCRYPTED_OR_PSTN_ONLY":
            return "2";
    }
}

function encode_date($date) {
    $date_str = (string)$date;
    $date_str .= "000";
    return $date_str;
}

function decode_date($date) {
    $date_str = (string)$date;
    $date_str = substr($date_str, 0, -3);
    return $date_str;
}

function duration($start, $end) {
    if (($end - $start) / 3600000 >= 1) {
        $dur = ($end - $start) / 3600000;
        if ($dur == 1) {
            return $dur .' hr';
        } else {
            return $dur .' hrs';
        }
    } else {
        $dur = ($end - $start) / 60000;
        if ($dur == 1) {
            return $dur .' min';
        } else {
            return $dur .' mins';
        }
    }
}

function get_end_date($value, $date) {
    if ($value == 'DATE') {
        return encode_date($date);
    }

    return null;
}

function decode_day_mask($mask) {
    $result = "";
    $day_dict = array(
        0 => 'Saturday',
        1 => 'Friday',
        2 => 'Thursday',
        3 => 'Wednesday',
        4 => 'Tuesday',
        5 => 'Monday',
        6 => 'Sunday'
    );

    $mask_arr = str_split(str_pad(decbin($mask), 7, "0", STR_PAD_LEFT));

    for ($i = 6; $i >= 0; $i--) {
        if ($mask_arr[$i] == '1') {
            $result .= $day_dict[$i] .', ';
        }
    }

    return substr($result, 0, -2);
}

function number_to_words($number) {
    switch ($number) {
        case '1':
            return '1st';
        case '2':
            return '2nd';
        case '3':
            return '3rd';
        default:
            return $number .'th';
    }
}

function caps_to_words($caps) {
    switch ($caps) {
        case 'FIRST':
            return '1st';
        case 'SECOND':
            return '2nd';
        case 'THIRD':
            return '3rd';
        case 'FOURTH':
            return '4th';
        case 'FIFTH':
            return '5th';
    }
}