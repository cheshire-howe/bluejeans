<?php

class block_bjn_edit_form extends block_edit_form {

    protected function specific_definition($mform) {

        // Section header title according to language file
        $mform->addElement('header', 'configheader', get_string('blocksettings', 'block_bjn'));

        $mform->addElement('text', 'config_title', get_string('blocktitle', 'block_bjn'));
        $mform->setDefault('config_title', get_string('bjn', 'block_bjn'));
        $mform->setType('config_title', PARAM_RAW);

        // A sample string variable with a default value
        $mform->addElement('text', 'config_text', get_string('blockstring', 'block_bjn'));
        $mform->setDefault('config_text', get_string('bjn', 'block_bjn'));
        $mform->setType('config_text', PARAM_RAW);
    }
}