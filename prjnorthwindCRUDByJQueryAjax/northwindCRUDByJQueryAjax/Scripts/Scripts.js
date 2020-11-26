//頁面process
$(function () {
    $('#loaderbody').addClass('hide');

    $(document).bind('ajaxStart', function () {
        $('#loaderbody').removeClass('hide');
    }).bind('ajaxStop', function () {
        $('#loaderbody').addClass('hide');
    });
});

//檢視圖片上傳
function ShowImagePreview(imagesUploader, previewImage) {
    if (imagesUploader.files && imagesUploader.files[0]) {
        //有檔案就讀檔
        let reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imagesUploader.files[0]);
    }
}