function downloadImages(imagePlaceholders, sync, deferred) {
    if (!imagePlaceholders.length) {

        if(deferred&&deferred.resolve){
            deferred.resolve();
        }

        return;
    }

    var img = new Image(),
        current = imagePlaceholders.get(0);

    imagePlaceholders.splice(0, 1);

    img.onload = function () {

        var $img = $('img', current);
        $img.attr('src', img.src);

        $img.fadeIn();

        if(sync){
            downloadImages(imagePlaceholders, sync, deferred);
        }
    };

    if (!sync) {
        downloadImages(imagePlaceholders, sync, deferred);
    }

    img.src = current.dataset.src;
}

$(document).ready(function () {

    if (window.pageCategory) {

        $(function () {
            var $div = $('.title-image');
            var width = $div.width();

            $div.css('height', width);
            $('img', $div).css('height', width);
        });

        downloadImages($('.image-placeholder'));
    }

    if (window.pageView) {

        var deferred = $.Deferred();

        downloadImages($('.image-placeholder'), true, deferred);

        deferred.done(function () {
            $('#indicator').hide();
        });
    }
});