require('bootstrap');
require("bootstrap/dist/css/bootstrap.min.css");
var $ = require('jquery');
const rust = import('./ui');

rust.then(m => m.greet("Alex"));

$('.random').ready(function() {
    console.log("djskaldjskald");
});
