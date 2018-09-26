require('bootstrap');
require("bootstrap/dist/css/bootstrap.min.css");
var $ = require('jquery');
const ui = import('./ui');

window.rust = window.rust || {};
window.rust.ui_category_delete = function() {};
window.rust.ui_find_elem_by_id = function() {};

ui.then(m => {
    window.rust.ui_category_delete = m.ui_category_delete;
    window.rust.ui_find_elem_by_id = m.ui_find_elem_by_id;
});

$('#pushme').click(function(e) {
    e.preventDefault();
    window.rust.ui_find_elem_by_id("pushme", "data-category-id");
});
