require('bootstrap');
require("bootstrap/dist/css/bootstrap.min.css");
var $ = require('jquery');
const ui = import('./ui');

window.rust = window.rust || {};
window.rust.greet = function() {};

ui.then(m => {
    window.rust.greet = m.greet;
});

$('.random').ready(function() {
    window.rust.greet("Alex");
});
$('#pushme').click(function(e) {
    e.preventDefault();
    window.rust.greet("Alex");
});
