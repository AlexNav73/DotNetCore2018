require('bootstrap');
require("bootstrap/dist/css/bootstrap.min.css");
require('jquery-validation');
require('jquery-validation-unobtrusive');
const $ = require('jquery');
const ui = import('./ui');

window.call_rust = function (callback, continuation = x => location.reload()) {
    ui.then(callback).then(continuation);
}

$('#pushme').click(function(e) {
    e.preventDefault();
    
    window.call_rust(x => x.ui_show_list($('#idField')), x => {});
});
