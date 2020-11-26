import { ajax } from "jquery";

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

//Ajax 表單傳送資料
function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        //通過驗證，準備傳資料
        let ajaxConfig = {
            type: 'POST',
            uel: form.action,
            data: new FormData(form),   //產生表單物件
            success: function (res) {
                if (res.success) {
                    $('#firstTab').html(res.html);
                    refreshAddNewTab($(form).attr('data_resetUrl'), true);
                    $.notify(res.message, "成功送出!!");
                    //顯示在datatable
                    if (typeof activateJQueryTable !== 'undefined' && $.isFunction(activateJQueryTable))
                        activateJQueryTable();
                } else {
                    //err.message
                    $.notify(res.message,"錯誤!!");
                }
            }
        }
        if ($(form).attr('enctype') == 'multipart/form-data') {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        //傳送ajax
        $.ajax(ajaxConfig);
    }
    return false;
}
//重新整理頁簽
function refreshAddNewTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (res) {
            $('#secondTab').html(res);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
        }
    });
}