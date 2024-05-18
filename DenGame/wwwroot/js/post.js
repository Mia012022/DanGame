// 輸入圖片路徑 顯示照片
$(document).ready(function () {
    $('#imageInput').on('change', function (e) {
        var file = e.target.files[0];
        var reader = new FileReader();

        reader.onload = function (e) {
            var imageData = e.target.result;
            $('#imageInput').after('<div id="imageDisplayArea" style="width: 100%;"></div>');
            $('#imageDisplayArea').html('<img src="' + imageData + '" alt="Selected Image" style="width:100%;object-fit:cover;">');
        };

        reader.readAsDataURL(file);
    });
    // 監聽字數
    $('#textInput').on('input', function () {
        var currentLength = $(this).val().length;
        $('#charCount').text(currentLength + '/200');
    });
    // 更換顏色
    $('#changeColor').on('click', function (e) {
        e.preventDefault();
        $('#contentInput').css('color', 'red');
    })

});

ClassicEditor
    .create(document.querySelector('#editor'), {

    }

    )
    .then(editor => {
        console.log(editor);
    })
    .catch(error => {
        console.error(error);
    });