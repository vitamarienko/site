
$(document).ready(function () {

    var initPromise = $.get('/api/sets');

    initPromise.done(function () {

        console.log(arguments);

    });

});