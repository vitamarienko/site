function downloadImages(imagePlaceholders, fadeInTimeout, deferred) {
    if (!imagePlaceholders.length) {

        return;
    }

    var img = new Image(),
        current = imagePlaceholders.get(0);

    imagePlaceholders.splice(0, 1);

    img.onload = function () {

        var $img = $('img', current);
        $img.attr('src', img.src);

        $img.fadeIn();

        if (!imagePlaceholders.length && deferred && deferred.resolve) {

            deferred.resolve();
        }
    };

    img.src = current.dataset.src;

    setTimeout(function () {
        return downloadImages(imagePlaceholders, fadeInTimeout, deferred);
    }, fadeInTimeout)
}

$(document).ready(function () {

    if (window.pageCategory) {

        $(function () {
            var $div = $('.title-image');
            var width = $div.width();

            $div.css('height', width);
            $('img', $div).css('height', width);
        });

        downloadImages($('.image-placeholder'), 0);
    }

    if (window.pageView) {

        var deferred = $.Deferred();

        downloadImages($('.image-placeholder'), 200, deferred);

        deferred.done(function () {
            $('#indicator').hide();
        });
    }
});