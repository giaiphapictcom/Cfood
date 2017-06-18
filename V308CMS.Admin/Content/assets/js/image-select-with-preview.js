$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');
    // Hover befor close the preview
    $('.image-preview').hover(
        
        function () {
            var id = $(this).attr('id');
            $('#' + id).popover('show');
        },
         function () {
             var id = $(this).attr('id');
             $('#' + id).popover('hide');
         }
    );
});
$(function () {
    // Create the close button
    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;'
    });
    closebtn.attr("class", "close pull-right");
    // Set the popover default content
    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Xem trước</strong>" + $(closebtn)[0].outerHTML,
        content: "Chưa chọn ảnh",
        placement: 'bottom'
    });
    // Clear event
    $('.image-preview-clear').click(function () {
        var imagePreviewClearId = $(this).attr('id');
        $('#' + imagePreviewClearId).parent().parent().attr("data-content", "").popover('hide');
        $('#' + imagePreviewClearId).prev().val("");
        
        $('#' + imagePreviewClearId).hide();
        $('#' +imagePreviewClearId).find('.image-preview-input input:file').val("");
        $('#' + imagePreviewClearId).find(".image-preview-input-title").text("Duyệt");
    });
    // Create the preview image
    $(".image-preview-input input:file").change(function () {
        var id = $(this).attr('id');       
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        // Set preview image into the popover data-content
        reader.onload = function (e) {
            $("#" + id ).parent().find(".image-preview-input-title").text("Chọn ảnh khác");
            $("#" + id).parent().prev().show();
            $("#" + id).parent().parent().prev().val(file.name);
            img.attr('src', e.target.result);
            $("#" + id).parent().parent().parent().attr("data-content", $(img)[0].outerHTML).popover("show");
        }
        reader.readAsDataURL(file);
    });
});