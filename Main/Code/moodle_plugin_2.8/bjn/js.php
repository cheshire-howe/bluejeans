<?php

class js
{
    private static $donefirst;
    
    public static function load($module) {
        global $PAGE;

        if (!self::$donefirst) {
//            $PAGE->requires->jquery();
//            $PAGE->requires->jquery_plugin('ui');
            $PAGE->requires->js(new \moodle_url('/blocks/bjn/amd/build/define.min.js'));
            self::$donefirst = true;
        }
        $PAGE->requires->js_init_code('format_bjn_add_pending("' . $module . '");');
        $PAGE->requires->js(new \moodle_url('/blocks/bjn/amd/src/' . $module . '.js'));
    }
    
    public static function call($module, $fn, array $params = array()) {
        global $PAGE;

        $jsparams = '';
        foreach ($params as $param) {
            if ($jsparams !== '') {
                $jsparams .= ',';
            }
            $jsparams .= json_encode($param);
        }
        $PAGE->requires->js_init_code(
                'window.format_bjn_modules.' . $module .'.' . $fn . '(' . $jsparams . ')');
    }
}