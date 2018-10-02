require('bootstrap');
require("bootstrap/dist/css/bootstrap.min.css");
const $ = require('jquery');
const ui = import('./ui');

window.rust = ui.then(m => ({
    ui_category_delete: m.ui_category_delete,
    ui_find_elem_by_id: m.ui_find_elem_by_id,
    ui_parse_val: m.ui_parse_val,
}));

$('#pushme').click(function(e) {
    e.preventDefault();

    window.rust.then(x => x.ui_find_elem_by_id($('#pushme'), "categoryId"))
        .then(x => console.log("Deleted"))
        .catch(e => console.warn(e));
    window.rust.then(x => x.ui_parse_val($('#idField'), "dsadsa"))
        .then(x => console.log(x));
});
