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

    window.rust.then(x => x.ui_find_elem_by_id($('#pushme'), "categoryId"))
        .then(x => console.log("Deleted"))
        .catch(e => console.warn(e));
    window.rust.then(x => x.ui_parse_val($('#idField'), "dsadsa"))
        .then(x => console.log(x));
});
