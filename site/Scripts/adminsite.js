$(document).ready(function () {

    var $createSetFormStep1 = $('#form-create-set-step-1'),
        $createSetFormStep2 = $('#form-create-set-step-1'),
        $fileInput = $('input[type="file"]', $createSetFormStep1),
        $base64TitleImg = $('#Base64TitleImg');

    $fileInput.on('change', function () {

        var file = this.files[0];

        if (!isJpeg(file)) {
            
            alert('Расширение файла должно быть JPEG');

            $fileInput.val('');
        }

        var toBase64Promise = fileToBase64StrAsync(file);

        toBase64Promise.then(result => $base64TitleImg.val(result))
    });
});

function isJpeg(file) {
    return file.type === "image/jpeg";
}

function fileToBase64StrAsync(file) {

    return new Promise((resolve, reject) => {

        var reader = new FileReader();

        reader.readAsDataURL(file);

        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);

    });
}