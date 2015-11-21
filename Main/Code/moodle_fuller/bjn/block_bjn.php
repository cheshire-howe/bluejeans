<?php
require_once('connector.php');

class block_bjn extends block_base {
    public function init() {
        // Set Title
        $this->title = get_string('bjn', 'block_bjn');
    }

    public function get_required_javascript() {
        parent::get_required_javascript();

        $this->page->requires->jquery();
        $this->page->requires->jquery_plugin('ui');
        $this->page->requires->js('/blocks/bjn/vendor/bootstrap.min.js');
    }

    public function get_content() {
        global $COURSE, $DB, $PAGE;
        $connector = new connector();

        if ($this->content !== null) {
            return $this->content;
        }

        $this->content          = new stdClass();
        $this->content->text    = html_writer::tag('h4', $COURSE->fullname);

        $context = context_course::instance($COURSE->id);

        // Footer
        if (has_capability('block/bjn:managepages', $context)) {
            $url = new moodle_url('/blocks/bjn/view.php', array(
                'blockid' => $this->instance->id,
                'courseid' => $COURSE->id
            ));
            $this->content->footer = html_writer::link($url, get_string('addmeeting', 'block_bjn'));
        } else {
            $this->content->footer = '';
        }

        $bjnpicurl = new moodle_url('/blocks/bjn/pix/BJN_logowtext@2x.png');
        $this->content->footer .= html_writer::div(
            '<img src="'. $bjnpicurl .'">',
            'block_bjn bjn-branding'
        );
        // End Footer

        // Check to see if we are in editing mode
        $canmanage = has_capability('block/bjn:managepages', $context);
            // && $PAGE->user_is_editing($this->instance->id);
        $canview = has_capability('block/bjn:viewpages', $context);

        // Get Meetings
        if ($bjnmeetings = $DB->get_records('block_bjn', array('blockid' => $this->instance->id))) {

            // Get Current Meetings, if any
            if ($bjncurrentmeetings = $connector->get_current_meetings()) {
                foreach ($bjncurrentmeetings as $meeting) {
                    foreach ($bjnmeetings as $m) {
                        if ($meeting->Key == $m->meetingid) {
                            $this->content->text .= html_writer::link($meeting->Value,
                                '<div id="join-now" class="btn btn-large btn-success">Class in Progress:<br>'. $m->meetingtitle .'<br>Join Now!</div>');
                        }
                    }
                }
            }

            $this->content->text .= html_writer::start_tag('ul');

            // Get Meetings from Bluejeans
            $allmeetings = $connector->get_meetings();
            //print_object($allmeetings);

            foreach ($bjnmeetings as $bjnmeeting) {
                if ($canmanage) {
                    // Edit
                    $pageparam = array(
                        'blockid' => $this->instance->id,
                        'courseid' => $COURSE->id,
                        'id' => $bjnmeeting->id,
                        'meetingid' => $bjnmeeting->meetingid
                    );
                    $editurl = new moodle_url('/blocks/bjn/view.php', $pageparam);
                    $editpicurl = new moodle_url('/pix/t/edit.png');
                    $edit = html_writer::link($editurl, html_writer::tag('img', '', array(
                        'src' => $editpicurl,
                        'alt' => get_string('edit'),
                        'title' => get_string('edit', 'block_bjn')
                    )));

                    // Delete
                    $deleteparam = array(
                        'courseid' => $COURSE->id,
                        'id' => $bjnmeeting->id,
                        'meetingid' => $bjnmeeting->meetingid
                    );
                    $deleteurl = new moodle_url('/blocks/bjn/delete.php', $deleteparam);
                    $deletepicurl = new moodle_url('/pix/t/delete.png');
                    $delete = html_writer::link($deleteurl, html_writer::tag('img', '', array(
                        'src' => $deletepicurl,
                        'alt' => get_string('delete'),
                        'title' => get_string('delete', 'block_bjn')
                    )));
                } else {
                    $edit = '';
                    $delete = '';
                }

                $pageurl = new moodle_url('/blocks/bjn/view.php', array(
                    'blockid' => $this->instance->id,
                    'courseid' => $COURSE->id,
                    'id' => $bjnmeeting->id,
                    'meetingid' => $bjnmeeting->meetingid,
                    'viewpage' => '1'
                ));

                // Each meeting list item
                $this->content->text .= html_writer::start_tag('li');

                // Edit/Delete icons
                $this->content->text .= html_writer::start_div('bjn-block-left');
                $this->content->text .= $edit;
                $this->content->text .= $delete;
                $this->content->text .= html_writer::end_div();

                // Link to class and dates
                $this->content->text .= html_writer::start_div('bjn-block-right');
                if ($canview) {
                    $this->content->text .= html_writer::link($pageurl, $bjnmeeting->meetingtitle);
                } else {
                    $this->content->text .= html_writer::div($bjnmeeting->meetingtitle);
                }
                $this->content->text .= html_writer::start_div('dates');
                $this->content->text .= html_writer::start_tag('em');

                // Get Next Meeting
                $indexed_am = array();
                foreach ($allmeetings as $am) {
                    $indexed_am[$am->id] = $am;
                }

                if (!isset($indexed_am[$bjnmeeting->meetingid])) {
                    $this->content->text .= 'Ended';
                } else {
                    $next = $indexed_am[$bjnmeeting->meetingid]->next;
                    $this->content->text .= userdate(($next->start / 1000));
                    $this->content->text .= ' / '. $this->duration(($next->start / 1000),
                            ($next->end / 1000));
                }

                $this->content->text .= html_writer::end_tag('em');
                $this->content->text .= html_writer::end_div();

                $this->content->text .= html_writer::end_tag('li');

                $this->content->text .= html_writer::div('', 'clear');
            }

            $this->content->text .= html_writer::end_tag('ul');
        }

        $url = new moodle_url('/blocks/bjn/view.php', array(
            'blockid' => $this->instance->id,
            'courseid' => $COURSE->id
        ));

        return $this->content;
    }

    public function specialization() {
        if (isset($this->config)) {
            if (empty($this->config->title)) {
                $this->title = get_string('defaulttitle', 'block_bjn');
            } else {
                $this->title = $this->config->title;
            }

            if (empty($this->config->text)) {
                $this->config->text = get_string('defaulttext', 'block_bjn');
            }
        }
    }

    public function instance_config_save($data, $nolongerused = false) {
        if(get_config('bjn', 'Allow_HTML') == '1') {
            $data->text = strip_tags($data->text);
        }

        // And now forward to the default implementation defined in the parent class
        return parent::instance_config_save($data, $nolongerused = false);
    }

    public function instance_delete() {
        global $DB;
        $DB->delete_records('block_bjn', array('blockid' => $this->instance->id));
    }

    public function instance_allow_multiple() {
        return false;
    }

    public function html_attributes() {
        $attributes = parent::html_attributes(); // Get default values
        $attributes['class'] .= ' block_'. $this->name(); // Append our class to class attribute
        return $attributes;
    }

    public function applicable_formats() {
        return array(
            'site-index' => false,
            'course-view' => true,
            'course-view-social' => false,
            'mod' => false,
            'mod-quiz' => false,
        );
    }

    function has_config() { return true; }

    function duration($start, $end) {
        if (($end - $start) / 3600 >= 1) {
            $dur = ($end - $start) / 3600;
            if ($dur == 1) {
                return $dur .' hr';
            } else {
                return $dur .' hrs';
            }
        } else {
            $dur = ($end - $start) / 60;
            if ($dur == 1) {
                return $dur .' min';
            } else {
                return $dur .' mins';
            }
        }
    }
}