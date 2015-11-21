/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

window.format_bjn_modules = {};
window.format_bjn_pending = [];
window.define = function(ignored, fn) {
    window.format_bjn_pending.push(fn($));
};
window.format_bjn_add_pending = function(module) {
    var next = window.format_bjn_pending.shift();
    window.format_bjn_modules[module] = next;
};
window.require = function(modules, fn) {
    var params = [];
    for (var i = 0; i < modules.length; i++) {
        params.push(window.format_bjn_modules[modules[i]]);
    }
    fn.call(this, params);
};
