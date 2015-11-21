<?php

$settings->add(new admin_setting_heading(
    'headerconfig',
    get_string('headerconfig', 'block_bjn'),
    get_string('descconfig', 'block_bjn')
));

$settings->add(new admin_setting_configtext(
    'bjn/Url',
    get_string('labelurl', 'block_bjn'),
    get_string('descurl', 'block_bjn'),
    'http://dev.threepointturn.com:8179/'
));

