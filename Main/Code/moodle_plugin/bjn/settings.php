<?php

$settings->add(new admin_setting_heading(
    'headerconfig',
    get_string('headerconfig', 'block_bjn'),
    get_string('descconfig', 'block_bjn')
));

// To call: $allowHTML = get_config('bjn', 'Allow_HTML');
/*$settings->add(new admin_setting_configcheckbox(
    'bjn/Allow_HTML',
    get_string('labelallowhtml', 'block_bjn'),
    get_string('descallowhtml', 'block_bjn'),
    '0'
));*/

$settings->add(new admin_setting_configtext(
    'bjn/Url',
    get_string('labelurl', 'block_bjn'),
    get_string('descurl', 'block_bjn'),
    'http://localhost:8112/'
));

$settings->add(new admin_setting_configtext(
    'bjn/UserId',
    get_string('labeluserid', 'block_bjn'),
    get_string('descuserid', 'block_bjn'),
    ''
));