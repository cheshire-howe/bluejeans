<?php
require_once('lib.php');

class connector
{
    private $_user;

    public function __construct() {
        global $USER, $DB;
        $this->_user = $USER;
        $this->_db = $DB;
    }

    public function get_meetings() {
        $meetings = [];
        $records = $this->_db->get_records('block_bjn');
        foreach ($records as $record) {
            $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting/'. $record->meetingid . '?email=' . $record->email);
            curl_setopt_array($ch, array(
                CURLOPT_RETURNTRANSFER => true,
                CURLOPT_HTTPHEADER => array(
                    'Accept: application/json',
                    'Content-Type: application/json'
                ),
            ));
            array_push($meetings, json_decode(curl_exec($ch)));
        }
        return $meetings;
    }

    public function get_meeting($id) {
        $record = $this->_db->get_record('block_bjn', ['meetingid' => $id]);
        $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting/'. $id . '?email=' . $record->email);
        curl_setopt_array($ch, array(
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => array(
                'Accept: application/json',
                'Content-Type: application/json'
            ),
        ));
        return json_decode(curl_exec($ch));
    }

    public function schedule_meeting($data)
    {
        if (property_exists($data, 'attendees')) {
            $attendees = encode_attendee_list($data->attendees);
        } else {
            $attendees = array();
        }

        $payload = array(
            'id' => 0,
            'title' => $data->meetingtitle,
            'description' => $data->meetingdesc,
            'start' => encode_date($data->starttime),
            'end' => encode_date($data->starttime + $data->classlength),
            'timezone' => $data->timezone,
            "recurrencePattern" => array(
                "recurrenceType" => $data->recurrenceType,
                "endDate" => get_end_date($data->endDate, $data->enddate),
                "recurrenceCount" => $data->recurrenceCount,
                "frequency" => $data->frequency,
                "daysOfWeekMask" => $data->daysOfWeekMask,
                "dayOfMonth" => $data->dayOfMonth,
                "weekOfMonth" => $data->weekOfMonth,
                "monthOfYear" => $data->monthOfYear
            ),
            'advancedMeetingOptions' => array(
                'videoBestFit' => false,
                'publishMeeting' => false,
                'encryptionType' => "NO_ENCRYPTION",
                'moderatorLess' => $this->parse_bool($data->moderatorless),
                'allowStream' => false,
                'autoRecord' => $this->parse_bool($data->autorecord),
                'disallowChat' => $this->parse_bool($data->disallowchat),
                'muteParticipantsOnEntry' => $this->parse_bool($data->muteparticipants),
                'showAllAttendeesInMeetingInvite' => false,
            ),
            'moderator' => array(),
            'numericMeetingId' => null,
            'attendeePasscode' => null,
            'addAttendeePasscode' => $this->parse_bool($data->passcode),
            'deleted' => false,
            'allow720p' => false,
            'status' => null,
            'locked' => false,
            'sequenceNumber' => 0,
            'icsUid' => null,
            'endPointType' => 'Moodle',
            'endPointVersion' => '2.9',
            'attendees' => $attendees,
            'isLargeMeeting' => false,
            'created' => null,
            'lastModified' => null,
            'isExpired' => false,
            'first' => null,
            'last' => null,
            'next' => null,
            'nextStart' => null,
            'nextEnd' => null,
        );

        $json = json_encode($payload);

        $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting?email=' . $this->_user->email);
        curl_setopt_array($ch, array(
            CURLOPT_CUSTOMREQUEST => "POST",
            CURLOPT_POSTFIELDS => $json,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => array(
                'Accept: application/json',
                'Content-Type: application/json'
            ),
        ));

        return curl_exec($ch);
    }

    public function update_meeting($meetingid, $data)
    {
        if ($data->attendees) {
            $attendees = encode_attendee_list($data->attendees);
        } else {
            $attendees = array();
        }

        $payload = array(
            'title' => $data->meetingtitle,
            'description' => $data->meetingdesc,
            'start' => encode_date($data->starttime),
            'end' => encode_date($data->starttime + $data->classlength),
            'timezone' => $data->timezone,
            "recurrencePattern" => array(
                "recurrenceType" => $data->recurrenceType,
                "endDate" => get_end_date($data->endDate, $data->enddate),
                "recurrenceCount" => $data->recurrenceCount,
                "frequency" => $data->frequency,
                "daysOfWeekMask" => $data->daysOfWeekMask,
                "dayOfMonth" => $data->dayOfMonth,
                "weekOfMonth" => $data->weekOfMonth,
                "monthOfYear" => $data->monthOfYear
            ),
            'advancedMeetingOptions' => array(
                'videoBestFit' => false,
                'publishMeeting' => false,
                'encryptionType' => "NO_ENCRYPTION",
                'moderatorLess' => $this->parse_bool($data->moderatorless),
                'allowStream' => false,
                'autoRecord' => $this->parse_bool($data->autorecord),
                'disallowChat' => $this->parse_bool($data->disallowchat),
                'muteParticipantsOnEntry' => $this->parse_bool($data->muteparticipants),
                'showAllAttendeesInMeetingInvite' => false,
            ),
            'moderator' => array(),
            'numericMeetingId' => null,
            'attendeePasscode' => null,
            'addAttendeePasscode' => $this->parse_bool($data->passcode),
            'deleted' => false,
            'allow720p' => false,
            'status' => null,
            'locked' => false,
            'attendees' => $attendees,
            'isLargeMeeting' => false,
        );

        $json = json_encode($payload);

        $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting/'. $meetingid . '?email=' . $this->_user->email);
        curl_setopt_array($ch, array(
            CURLOPT_CUSTOMREQUEST => "PUT",
            CURLOPT_POSTFIELDS => $json,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => array(
                'Accept: application/json',
                'Content-Type: application/json'
            ),
        ));

        return curl_exec($ch);
    }

    public function delete_meeting($meetingid) {
        $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting/'. $meetingid . '?email=' . $this->_user->email);
        curl_setopt_array($ch, array(
            CURLOPT_CUSTOMREQUEST => "DELETE",
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => array(
                'Accept: application/json',
                'Content-Type: application/json'
            ),
        ));
        return curl_exec($ch);
    }

    public function get_download_urls($numericmeetingid) {
        $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting/'. $numericmeetingid .'/GetDownloadUrls?email=' . $this->_user->email);
        curl_setopt_array($ch, array(
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => array(
                'Accept: application/json',
                'Content-Type: application/json'
            ),
        ));
        return json_decode(curl_exec($ch));
    }

    public function get_current_meetings() {
        $meetings = [];
        $records = $this->_db->get_records_sql('SELECT DISTINCT email FROM {block_bjn}');
        foreach ($records as $record) {
            $ch = curl_init(get_config('bjn', 'Url'). 'api/Meeting/GetCurrentMeetings?email=' . $record->email);
            curl_setopt_array($ch, array(
                CURLOPT_RETURNTRANSFER => true,
                CURLOPT_HTTPHEADER => array(
                    'Accept: application/json',
                    'Content-Type: application/json'
                ),
            ));
            $userMeetings = json_decode(curl_exec($ch));
            foreach ($userMeetings as $userMeeting) {
                array_push($meetings, $userMeeting);
            }
        }
        return $meetings;
    }

    public function get_user_room_settings() {
        $ch = curl_init(get_config('bjn', 'Url'). 'api/User/RoomSettings?email=' . $this->_user->email);
        curl_setopt_array($ch, array(
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => array(
                'Accept: application/json',
                'Content-Type: application/json'
            ),
        ));
        return json_decode(curl_exec($ch));
    }

    function parse_bool($int)
    {
        return $int != 0;
    }
}