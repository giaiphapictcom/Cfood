//$(function(){
//	//initBlockTab();
//    //accordSlideInit();
//    //initMenu();
//    //generalFunc();
//    //redirectSearchPage();
//    //initAccountMenu();
//    //backPreviousPage();
//    //saveCurrentCart();
//    //initSelectLocation();
//});

function initMenu(){
    $('.js-toggle-menu').click(function(){
        $('.js-menu-panel').toggleClass('showMenu');
        $('body').toggleClass('hidden');
    });
    //$('.js-toggle-cart').click(function(){
    //    $('.dr-shopping-cart-page').toggleClass('showCart');
    //    $('body').toggleClass('hidden');
    //});

    //$('.js-toggle-info-menu').click(function(){
    //    $('.js-info-menu-rotate').toggleClass('rotate');
    //    $('.js-info-menu-content').toggleClass('hide');
    //}); 

    //$('.js-menu > .menu-item .fa').click(function(){
    //    var dataId = $(this).attr('data-id');
    //    $('.js-menu > .menu-item-container[data-id="'+dataId+'"]').toggleClass('show');
    //    $(this).toggleClass('js-info-menu-rotate');
    //    return false;
    //});
}

$(document).ready(function () {
    initMenu();
});


function initBlockTab(){
	if ($('.js-block-tab').length <= 0) {
        return;
    };

    $('.js-block-tab').each(function(){
        var tabCommunity        = $(this);
        var tabCommunityHeader  = tabCommunity.find('.js-tab-header');
        tabCommunityHeader.click(function(){
            var dataId = $(this).attr('data-id');
            tabCommunity.find('.js-tab-header.active').removeClass('active');
            $(this).addClass('active');
            tabCommunity.find('.js-tab-container.active').removeClass('active');
            tabCommunity.find('.js-tab-container[data-id="'+dataId+'"]').addClass('active');
        })
    })	
}
/*  Custom scroll 
-   Nếu chạy trên thiết bị di động thì dùng scroll mặc định
-   Nếu chạy trên desktop thì scroll custom
*/
function scrollMenu(){
    /*if(isiPhone() == true){
        $('html, body').addClass('device');
        return;
    }*/
    setTimeout(function(){
        $('.is-mn').each(function(){
            if(!$(this).hasClass('done')) {
                var isMn        = $(this);
                var callBack    = isMn.data('call-back');
                isMn.mCustomScrollbar({
                    theme           : "minimal-dark",
                    scrollInertia   : 200,
                    scrollEasing    : "linear",
                    callbacks:{
                        whileScrolling  : function(){
                                
                        }
                    }
                });
                $(this).addClass('done');
            }
        });
    }, 1000);
}
/*  IOS Slide */
function iosSlideInit(){
    if($('.is-iosld').length <= 0 && $('.is-iosld-vtl').length <= 0){
        return;
    }
    $('.is-iosld').each(function(){
        var isIosld = $(this);

        if (isIosld.hasClass('has-init') == false) {
            isIosld.addClass('has-init');
            var isWaitHover = isIosld.hasClass('is-wait-hover') ? !0 : !1;
            var dataMinSlide = isIosld.data('min-slide');
            if(typeof(dataMinSlide) == 'undefined')
                dataMinSlide = 0;
            var sumSlide = isIosld.find('.is-item-sld').length;
            if(typeof(sumSlide) == 'undefined')
                sumSlide = 0;
            /*  Nếu số lượng slide nhỏ hơn số lượng tối thiểu thì ko khởi tạo slide */
            if(sumSlide < dataMinSlide ) {
                isIosld.find('.prv-sld, .nxt-sld').remove();
            }
            var pagiSld = false;
            if (isIosld.find('.pagi-sld').length > 0) {
                pagiSld = true;
                var atv = 'atv';
                for (var i = 0; i < sumSlide; i++) {
                    isIosld.find('.pagi-sld').append('<div class="item-pagi-sld '+atv+'"></div>');
                    atv = '';
                };
            };
            var callBack = isIosld.attr('data-call-back');
            var autoSlide = isIosld.attr('data-auto');
            if(typeof(autoSlide) == 'undefined'){
                autoSlide = true;
            }
            var TransTimer = isIosld.attr('data-transtimer');
            if(typeof(TransTimer) == 'undefined'){
                TransTimer = 2000;
            }
            var drag = isIosld.attr('data-drag');
            if(typeof(drag) == 'undefined'){
                drag = true;
            }else{
                drag = false;
            }

            if (isWaitHover) {
                isIosld.unbind('mouseenter').mouseenter(function() {
                    initIosSlide({'objSlide': isIosld, 'sumSlide': sumSlide, 'dataMinSlide': dataMinSlide, 'pagiSld': pagiSld, 'callback' : callBack, 'autoSlide' : autoSlide, 'TransTimer' : TransTimer, 'drag' : drag});
                    isIosld.unbind('mouseenter');
                });
            }else{
                initIosSlide({'objSlide': isIosld, 'sumSlide': sumSlide, 'dataMinSlide': dataMinSlide, 'pagiSld': pagiSld, 'callback' : callBack, 'autoSlide' : autoSlide, 'TransTimer' : TransTimer, 'drag' : drag});
            }
        };
    });
    $('.is-iosld-vtl').each(function(){
        var isIosld = $(this);
        isIosld.find('.sld').iosSliderVertical ({
            snapToChildren      : true,
            infiniteSlider      : true,
            desktopClickDrag    : true,
            autoSlide           : true,
            autoSlideTimer      : 5000,
            autoSlidesTimer     : 2000,
            navPrevSelector     : isIosld.find('.prv-sld'),
            navNextSelector     : isIosld.find('.nxt-sld')
        });
    });

    if (typeof(finishInitSlide) == 'function') {
        finishInitSlide();
    };
}
function initIosSlide(obj){
    var slider = obj['objSlide'].find('.sld');
    slider.iosSlider({
        snapToChildren      : true,
        infiniteSlider      : (obj['sumSlide'] - obj['dataMinSlide'] > 1)? true:false,
        desktopClickDrag    : obj['drag'],
        autoSlide           : obj['autoSlide'],
        autoSlideTimer      : 5000,
        autoSlideTransTimer : obj['TransTimer'],
        autoSlideHoverPause :true,
        navPrevSelector     : obj['objSlide'].find('.prv-sld'),
        navNextSelector     : obj['objSlide'].find('.nxt-sld'),
        onSlideChange      : function(args){
            if (obj['pagiSld']) {
                var index = args.currentSlideNumber - 1;
                obj['objSlide'].find('.item-pagi-sld.atv').removeClass('atv');
                obj['objSlide'].find('.item-pagi-sld').eq(index).addClass('atv');
            };
            if(typeof obj['callback'] != 'undefined') {
                window[obj['callback']]({
                    'obj' : obj['objSlide'],
                    'next' : obj['objSlide'].find('.nxt-sld'),
                    'prev' : obj['objSlide'].find('.prev-sld'),
                    'index' : args.currentSlideNumber - 1
                });
            };
            obj['objSlide'].find("img.lazy").each(function () {
              var src = $(this).attr("data-original");
              if (!empty(src))
                $(this).attr("src", src).removeAttr("data-original");
           });
        }
    });

    if (obj['pagiSld']) {
        obj['objSlide'].find('.item-pagi-sld').click(function(){
            var index = $(this).index() + 1;
            obj['objSlide'].find('.sld').iosSlider('goToSlide', index);
        });
    }
}
/*  Accord slide */
function accordSlideInit(){
    if ($('.js-accord-sld').length <= 0) {
        return;
    };
    $('.js-accord-sld').each(function(){
        var idAccordSld = '#' + $(this).attr('id');
        $(idAccordSld).zAccordion({
            timeout: 5000,
            slideWidth: 350,
            speed: 500,
            trigger: "mouseover",
            width: 480,
            height: 200,
            slideClass: 'slider'
        });
    });
}
function radioButtonCustom(){
    if($('.is-radio-box').length <= 0){
        return;
    }
    $('.is-radio-box').each(function(){
        var box = $(this);
        var rdo = box.find('.is-radio-button');
        var lb = box.find('.is-radio-label');

        rdo.unbind('click').click(function(){
            var rdc = $(this);
            var id = $(this).data('id');
            var callBack    = rdc.data('call-back');
            
            box.find('.is-radio-button.atv').removeClass('fa-check-circle').removeClass('atv').addClass('fa-circle-o');
            
            rdc.removeClass('fa-circle-o').addClass('fa-check-circle').addClass('atv');

            if(typeof callBack != 'undefined') {
                window[callBack]({
                    'id'        : id,
                });
            };
        });
        lb.unbind('click').click(function(){
            var lbl = $(this);
            var id = $(this).data('id');
            
            box.find('.is-radio-button[data-id="' + id +'"]').trigger('click');
        });
    });
}
function showSmartAlertOld(content, title, type, close)
{
    if ($('.overlay-popup').length == 0) {
        html = '<div class="full-site"></div><div class="overlay-popup"></div>';
        $('body').append(html);
    }
    else {
        // reset popup content
        $('.overlay-popup').html('');
    }
    // load popup content.
    html = '<div class="new-popup" style="width: 390px">';
    if (typeof(type) == 'undefined')
        type = 'message';
    if (typeof(title) == 'undefined' || title == '')
        title = 'Thông báo hệ thống';
    if (typeof(close) == 'undefined')
        close = 1;
         
    var hclass = '';
    var icon = '';
    switch(type) {
        case 'success': 
            hclass = 'background-green';
            icon = 'icon-check';
            break;
        case 'message':
            hclass = 'background-green';
            icon = 'icon-check';
            break;
        case 'error':
            hclass = 'background-red';
            icon = 'icon-error';
            break;
        case 'warning':
            hclass = 'background-green';
            icon = 'icon-warning';
            break;
    }
    html += '<div class="header-popup '+hclass+'"><div class="icon '+icon+'"></div><div class="title">'+title+'</div>';
    if (close) {
        html += '<div class="close-popup"><span class="fa fa-close"></span></div>';
    }
    html += '</div><div class="content-popup"><div class="inner-conntent smart-alert">'+content+'</div></div>';
    $('.overlay-popup').html(html);
    closeSmartAlert();
}

function showSmartAlert(content, title, type, close, footer)
{
    if ($('#notify-popup').length == 0) {
        html = '<div id="notify-popup"></div>';
        $('body').append(html);
    }
    else {
        // reset popup content
        $('#notify-popup').html('');
    }
    // load popup content.
    html = '<div class="ngdialog-overlay"></div><div class="notify">';
    if (typeof(type) == 'undefined')
        type = 'message';
    if (typeof(title) == 'undefined' || title == '')
        title = 'Thông báo!';
    if (typeof(close) == 'undefined')
        close = 1;
    if (typeof(footer) == 'undefined')
        footer = 0;
    var hclass = '';
    var icon = '';
    switch(type) {
        case 'success': 
            hclass = 'txt-green';
            icon = 'icon-check';
            title = 'Thành công!';
            break;
        case 'message':
            hclass = 'txt-green';
            icon = 'icon-check';
            title = 'Thông báo!';
            break;
        case 'error':
            hclass = 'txt-red';
            icon = 'icon-error';
            title = 'Thông báo!';
            break;
        case 'warning':
            hclass = 'txt-green';
            icon = 'icon-warning';
            title = 'Cảnh báo!';
            break;
    }
    html += '<label class="txt-green '+hclass+'">'+ title +'</label>' +
            '<div class="ntf-content">'+content+'</div>';
    if (close && !footer) {
        html += '<a href="javascript:void(0);" class="btn-close js-btn-close-notify">Đóng</a>';
    }
    else if (footer) {
        html += '<div class="footer-popup">';
        html += '<a href="javascript:void(0);" class="btn js-agree-btn-popup">Đồng ý</a>';
        html += '<a href="javascript:void(0);" class="btn btn-white js-btn-close-notify js-cancel-btn-popup">Hủy</a>';
        html += '</div>';
    }
    html += '</div>';
    $('#notify-popup').html(html);
    closeSmartAlert();
}
function closeSmartAlert()
{
    $('#notify-popup .js-btn-close-notify').unbind('click').click(function(){
         $('#notify-popup').remove();
    });
}
function closeSmartAlertOld()
{
    $('.overlay-popup .close-popup').unbind('click').click(function(){
         $('.overlay-popup').remove();
         $('.full-site').remove();
    });
}
function showUpgradeMessage()
{
    content = '<div class="row-poup" style="text-align:center">Tính năng đang được nâng cấp.</div>';
    showSmartAlert(content, 'Thông Báo', 'success', true);
}
function initAccountMenu()
{
    $('#js-user-wall').unbind('click').click(function(){
        var link = '/trang_ca_nhan.html';
        window.location = link;      
    });
    /*
        hàm xử lý tương tác nhũng account menu:
        Nguyên tắt: goi ajax để lẫy dữ liệu về (mà ko chuyển trang)
    */
    $('.js-acc-menu-m').unbind('click').click(function(){
        var type = $(this).attr('data-type');
        var aMapType = {};
        aMapType['user_account'] = {};
        aMapType['user_account']['id'] = 'js-block-info-account';
        aMapType['user_account']['title'] = 'Thông tin tài khoản';
        aMapType['history_order'] = {};
        aMapType['history_order']['id'] = 'js-block-history-order';
        aMapType['history_order']['title'] = 'Lịch sử mua hàng';
        aMapType['history_trans'] = {};
        aMapType['history_trans']['id'] = 'js-block-history-trans';
        aMapType['history_trans']['title'] = 'Lịch sử giao dịch';
        aMapType['recharge'] = '';
        aMapType['favorite'] = '';
        aMapType['notice'] = {};
        aMapType['notice']['id'] = 'js-block-notice';
        aMapType['notice']['title'] = 'Thông báo của tôi <span id="js-cnt-total-notice"><span>';
        aMapType['ask_answer'] = '';
        
        if (typeof(aMapType[type]) == 'undefined' || empty(type) || type == 'ask_answer' || type == 'recharge' || type == 'favorite') {
            return;
        }
        //Load khung ban đầu trước khi gọi ajax
        var html = '<div class="dr-shopping-cart-page" id="'+aMapType[type]['id']+'">' +
                        '<div class="menu-overlay js-toggle-cart"></div>' +
                        '<div class="shopping-cart-page-header">' +
                            '<i class="fa fa-angle-left js-btn-back"></i>' +
                            '<label for="">'+aMapType[type]['title']+'</label>' +
                        '</div>' +
                        '<div class="shopping-cart-page-body is-mn" id="js-data-container">' +
                        '</div>' +
                    '</div>';
        $(html).insertAfter('.js-menu-panel');
        //tạo hiệu ứng lướt qua và hiển thị nội dung
        $('.js-menu-panel').toggleClass('showMenu');
        $('body').toggleClass('hidden');
        
        //hiển thị nội dung 
        $('#' + aMapType[type]['id']).toggleClass('showCart');
        $('body').toggleClass('hidden');
        $('.js-btn-back').unbind('click').click(function(){
            $('#' + aMapType[type]['id']).toggleClass('showCart');
            $('body').toggleClass('hidden');
            //xóa luôn cả html nội dung đó
            $('#' + aMapType[type]['id']).remove();
        });
        
        //call ajax to load block
        var sParams = '&'+ getParam('sGlobalTokenName') + '[call]=core.loadBlockAccountMenu' + '&val[type]=' + type;
        $.ajaxCall({
            obj: 'load',
            data: sParams,
            dataType: 'script',
            type: 'POST',
            timeout: 8000,
            callback: function(data){
            }
        });
    });
}

function showContentBlockMenu(data, type)
{
    if (empty(data)) {
        showSmartAlert('Lỗi hệ thống', 'Thông Báo', 'error', true);
        return false;
    }
    $('#js-data-container').append(data);
    
    if (type == '#js-block-history-trans') {
        $('.js-item-trans').unbind('click').click(function(){
            //Lấy thông tin
            var transCode = $(this).attr('data-code');
            var transType = $(this).attr('data-type');
            var transTime = $(this).attr('data-time');
            var transNote = $(this).attr('data-note');
            var transPayemt = $(this).attr('data-payment');
            var transMoney = $(this).attr('data-money')*1;
            var transCoin = $(this).attr('data-coin')*1;
            //var transCode = $(this).attr('data-code');
            if (transPayemt == 'diem') {
                transPayemt = 'Tài khoản DST';
            }
            else {
                transPayemt = 'Cổng thanh toán';
            }
            if (transType == 'purchase') {
                transMoney = '- ' + currencyFormat(transMoney) + ' VNĐ';
                transCoin = '- ' + currencyFormat(transCoin) + ' Xu';
            }
            else {
                transMoney = '+ ' + currencyFormat(transMoney) + ' VNĐ';
                transCoin = '+ ' + currencyFormat(transCoin) + ' Xu';
            }
            //tạo giao diện chi tiết
            var html = '<div class="dr-shopping-cart-page" id="js-block-trans-dt">' +
                            '<div class="menu-overlay js-toggle-cart"></div>' +
                            '<div class="shopping-cart-page-header">' +
                                '<i class="fa fa-angle-left js-btn-back-trans-dt"></i>' +
                                '<label for="">Chi tiết giao dịch</label>' +
                            '</div>' +
                            '<div class="shopping-cart-page-body m-body-trans-dt is-mn">' +
                                '<div class="m-row-sty-1">' +
                                    '<label class="m-row-title-sty-1">Mã giao dịch</label>' +
                                    '<input class="m-input-sty-1 form-input" value="'+transCode+'" disabled="disabled"/>' +
                                '</div>' +
                                '<div class="m-row-sty-1">' +
                                    '<label class="m-row-title-sty-1">Ngày giờ giao dịch</label>' +
                                    '<input class="m-input-sty-1 form-input" value="'+transTime+'" readonly/>' +
                                '</div>' +
                                '<div class="m-row-sty-1">' +
                                    '<label class="m-row-title-sty-1">Hình thức giao dịch</label>' +
                                    '<input class="m-input-sty-1 form-input" value="'+transPayemt+'" readonly/>' +
                                '</div>' +
                                '<div class="m-row-sty-1">' +
                                    '<label class="m-row-title-sty-1">Nội dung</label>' +
                                    '<input class="m-input-sty-1 form-input" value="'+transNote+'" readonly/>' +
                                '</div>' +
                                '<div class="m-row-sty-1">' +
                                    '<label class="m-row-title-sty-1">Số tiền giao dịch</label>' +
                                    '<input class="m-input-sty-1 form-input" value="'+transMoney+'" readonly/>' +
                                '</div>' +
                                '<div class="m-row-sty-1">' +
                                    '<label class="m-row-title-sty-1">Số tiền Xu giao dịch</label>' +
                                    '<input class="m-input-sty-1 form-input" value="'+transCoin+'" readonly/>' +
                                '</div>' +
                                '' +
                            '</div>' +
                        '</div>';
            
            $(html).insertAfter('.js-menu-panel');
            $('#js-block-trans-dt').toggleClass('showCart');
            $('#js-block-history-trans').toggleClass('showCart');
            $('.js-btn-back-trans-dt').unbind('click').click(function(){
                $('#js-block-trans-dt').toggleClass('showCart');
                $('#js-block-history-trans').toggleClass('showCart');
                $('#js-block-trans-dt').remove();
            });
        });
    }
    else if (type == '#js-block-info-account') {
        initUpdateInfoUserM();
    }
    else if (type == '#js-block-history-order') {
        initLoadOrderList();
    }
    else if (type == '#js-block-notice') {
        var cnt = $('#js-count-total-notice').html()*1;
        if (cnt < 1) {
            cnt = 0;
        }
        $('#js-cnt-total-notice').html('('+cnt+')');
    }
}
function initUpdateInfoUserM()
{
    $('#m-check-change-pass').change(function(){
        if (this.checked) {
            //show
            $('.m-box-change-pass').show();
        }
        else {
            //remove
            $('.m-box-change-pass').hide();
        }
    });
    $('#js-info-edit-save').unbind('click').click(function(){
        var action = "$(this).ajaxSiteCall('user.updateUserInfo', 'updateInfoCallbackM(data)'); return false;";
        $('#js-edit-user-info').attr('onsubmit', action);
        $('#js-edit-user-info').submit();
        $('#js-edit-user-info').removeAttr('onsubmit');
        //$('#js-info-edit-save').unbind('click');
    });
    
    $('.m-datetime-sty-1').each(function(){
        var id = '#' + $(this).attr('id');
        $(id).datetimepicker({
            timepicker:false,
            format:'d/m/Y',
            scrollMonth:false,
            scrollTime:false,
            scrollInput:false
        });
    });
}
function updateInfoCallbackM(data)
{
    if(data.status == 'error') {
        $('.m-error-box').html(data.message);
        $('.m-error-box').show();
        $('m-error-box').focus();
        //var messg = data.message.replace("<br />", "");
//        alert(messg);
        return false;
    }
    if(data.status == 'success') {
        $('.m-error-box').hide();
        showSmartAlert('Cập nhật thành công', 'Thông Báo', 'success', true);
        return false;
    }
}
function initLoadOrderList()
{
    $('.js-item-order').unbind('click').click(function(){
        //load detail order
        var id = $(this).attr('data-id')*1;
        var code = $(this).attr('data-code');
        if (id > 0 && !empty(code)) {
            var html = '<div class="dr-shopping-cart-page" id="js-block-order-dt">' +
                    '<div class="shopping-cart-page-header">' +
                        '<i class="fa fa-angle-left" id="js-back-order-dt"></i>' +
                        '<label for="">Đơn hàng '+code+'</label>' +
                    '</div>' +
                    '<div class="m-order-dt shopping-cart-page-body is-mn" id="js-block-info-order-dt">' +
                    '</div>' +
               '</div>';
            $(html).insertAfter('#js-block-history-order');
            $('#js-block-order-dt').toggleClass('showCart');
            $('#js-block-history-order').toggleClass('showCart');
            //back
            $('#js-back-order-dt').unbind('click').click(function(){
                $('#js-block-order-dt').toggleClass('showCart');
                $('#js-block-history-order').toggleClass('showCart');
                //xóa luôn html mới tạo
                $('#js-block-order-dt').remove();
            });
            //ajax loaing
            var sParams = '&'+ getParam('sGlobalTokenName') + '[call]=user.showDetailOrderM';
            sParams += '&code=' + code;
            var locationId = getCookie('area')*1;
            sParams += '&area=' + locationId;
            $.ajaxCall({
                obj: 'load',
                data: sParams,
                dataType: 'script',
                type: 'POST',
                timeout: 8000,
                callback: function(data){
                }
            });
        }
        else {
            showSmartAlert('Không có đơn hàng được chọn', 'Thông Báo', 'error', true);
        }
        //
    });
}

function showDetailOrderM(data)
{
    if (!empty(data)) {
        $('#js-block-info-order-dt').append(data);
    }
}

//re-buy
function reBuy(code)
{
    //get data detail order
    if (code == '') {
        showSmartAlert('Thông tin đơn hàng không hợp lệ!', 'Thông Báo', 'error', true);
    }
    showAjaxLoading($('html'), 'page');
    // call ajax to check info
    sParams = '&'+ getParam('sGlobalTokenName') + '[call]=user.getDataDetailOrder' + '&code=' + code;
    $.ajaxCall({
        data: sParams,
        obj: 'get_detail_order',
        dataType: 'json',
        type: "GET",
        callback: function(data){
            closeAjaxLoading($('html'));
            if (data.status == 'error') {
                showSmartAlert(data.message, 'Thông Báo', 'error', true);
                return false;
            }
            else {
                data = data.data.aShopOrderDt;
                
                if (typeof(data) == 'undefined' || Object.keys(data).length < 1) {
                    showSmartAlert('Đơn hàng không có sản phẩm nào!', 'Thông Báo', 'error', true);
                    return;
                }
                
                
                var nameparent = 'san_pham';
                var tmps = {};
                if (typeof(tmps[nameparent]) == 'undefined') {
                    tmps[nameparent] = {};
                    tmps[nameparent]['id'] = [];
                    tmps[nameparent]['sku'] = [];
                    tmps[nameparent]['so_luong'] = [];
                    tmps[nameparent]['name'] = [];
                    tmps[nameparent]['avatar'] = [];
                }
                for (v in data) {
                    for (i in data[v]) {
                        //add product to list
                        tmps[nameparent]['id'].push(data[v][i].id);
                        tmps[nameparent]['sku'].push(data[v][i].sku);
                        tmps[nameparent]['so_luong'].push(data[v][i].quantity);
                        tmps[nameparent]['name'].push(data[v][i].name);
                        tmps[nameparent]['avatar'].push(data[v][i].image_path);
                    }
                }
                
                js_dat_mua(tmps);
                showSmartAlert('Đã thêm vào giỏ hàng thành công!</br>Hệ thống sẽ tự động chuyển sang trang thanh toán trong 2 giây', 'Thông Báo', 'success', false);
                //alert('Đã thêm vào giỏ hàng thành công');
                //chuyển tới trang shop_chi_tiet sau 3s
                setInterval(function(){
                    window.location = '/shop_chi_tiet.html';
                }, 2000);
            }
            
        }
    });
}

function backPreviousPage()
{
    $('.js-back-pre-page').unbind('click').click(function(){
        window.history.back();
    });
}

/* Những hàm GODBAL */
function deleteProductInCart(id)
{
    $('#div_gio_hang_tong_tien').addClass('is-button-wait');
    $('#div_gio_hang_so_luong').addClass('is-button-wait');
    //hoverCartAfterClick();
        
    // remove item
    sParams = '&'+ getParam('sGlobalTokenName') + '[call]=shop.saveCart';
    sParams += '&val[act]=del' +'&val[q]='+ unescape(id);
    $.ajaxCall({
        data        : sParams,
        obj         : 'shop_checkout',
        dataType    : 'json',
        callback    : function(dataReturn){
            if (dataReturn.status =='success') {
                reloadProductCart();
            }
            else 
                showSmartAlert(dataReturn.message, 'Thông Báo', 'error', true);
        }
    });
}
function reloadProductCart()
{
    // remove old product data in local storage.
    localStorage.removeItem('shopcart');
    // insert to localstorage for new data 
    if (oParams['sController'] == 'shop.view') {
        showpage = 1;
    }
    capNhatGioHang({auto: 0});
}
var timeSever;
var afterLoginProcess = [];
var bSelectPurchasePage = false;

function generalFunc() {
    $('.button-login-facebook').unbind('click').click(function(){
       //showUpgradeMessage();
       return false;
    });
    $('.link-check-order').unbind('click').click(function(){
       //showUpgradeMessage();
       return false; 
    });
    loadActionAllPage();
    lazyLoadImage();
    iosSlideInit();
    //hoverShow();
    //scrollMenu();
    //changeTab();
    //showNav();
    //selectPlace();
    selectMarket();
    //windowResize();
    ratingProcess();
    var cart = getLocalStorage('shopcart');
    showProdcutCartPopup(getLocalStorage('shopcart'));
    //scrollAllPage();
    //initEllipse();
    //iosSlideInit();
    //accordSlideInit();
    //initQuantilyGroup();
    //removeProductCart();
    //keepAlive();
    //scrollToTop();
    //registerNews();
    //showInfoVendor();
    //loadAds();
    //initSelect2();
    //$(window).bind('scroll', checkScrollingForWidget);
}

/*  Hàm xử lý chọn siêu thị 
1.  Khi chọn 1 siêu thị thì đổi tên combo Box 
2.  Sau khi chọn tiến hành addClass hide để ẩn danh sách
3.  Remove class hide sau khi có hành động ennter / leave. Xử lý xong unbind luôn
*/
function selectMarket(){
    $('.cbo-mk-head').click(function(){
        sessionStorage.setItem('firstvisit', 1);
        /*  click Chọn đi siêu thị */
        if($('.cbo-mk').hasClass('hover')){
            /*  Trường hợp tắt popup chọn siêu thị: Remove CLass hover*/
            $('.cbo-mk').removeClass('hover');
            /*  unbind sự kiện chọn siêu thị, giải phóng bộ nhớ */
            $('.sl-mk-itm').unbind('click');
            $('.cbo-sl-mk').removeClass('hover');
        }else{
            /*  Trường hợp mở popup chọn siêu thị: Add class hover */
            $('.cbo-mk').addClass('hover');
            /*  Bắt sự kiện mở khung chọn siêu thị */
            $('.sl-mk-head').click(function(){
                /*  Mở khung chọn siêu thị: add class hover*/
                $('.cbo-sl-mk').addClass('hover');
                /*  Click item để chọn siêu thị */
                $('.sl-mk-itm').click(function(){
                    if(!$('.cbo-sl-mk').hasClass('hover')){
                        return;
                    }
                    $('.cbo-sl-mk').removeClass('hover');
                    var dataMk      = $(this).data('mk');
                    var path        = $(this).data('path');
                    var data        = $(this).html();

                    $('.sl-mk-head .vl').html(data);
                    $('.sl-mk-head .vl').attr('data-mk', dataMk);
                    $('.sl-mk-head .vl').attr('data-path', path);
                        
                });
            });

            $('.js-close-slmk').click(function(){
                $('.cbo-mk').removeClass('hover');
                $(this).unbind('click');
                $('.cbo-sl-mk').removeClass('hover');
            });
        }
    });
    $('.sl-mk-btn').unbind('click').click(function(){
        console.log($('.sl-mk-head .vl').attr('data-mk'));
        if (typeof($('.sl-mk-head .vl').attr('data-mk')) == 'undefined' || $('.sl-mk-head .vl').attr('data-mk') == '' ) {
            showSmartAlert('Vui lòng chọn Siêu thị!', 'Thông Báo', 'error', true);
            return false;
        };
        $('.cbo-mk').removeClass('hover');

        $('.id-search').val($('.sl-mk-head .vl').attr('data-mk'));
        $('.mk-nm').html($('.sl-mk-head .vl').html());

        var pnlMk = $('.pnl-mk');
        pnlMk.css('pointer-events','none');
        pnlMk.hide();
        setTimeout(function(){
            pnlMk.removeAttr('style');
        }, 500);

        window.location = $('.sl-mk-head .vl').data('path');
    });
    if (oParams['sController'] == 'core.index') {
        var firstvisit = sessionStorage.getItem('firstvisit');
        if (typeof(firstvisit) == 'undefined' || !firstvisit) {
            $('.cbo-mk-head').click();
        }
    }
}

function initSelect2(id){
    if(typeof id == 'undefined') {
        $('.js-select-2').each(function(){
               var placeHolder = $(this).attr('placeholder');
               var id = $(this).attr('id');
               $(this).select2();

               if ($(this).find('option[selected]').length > 0 ) {
                   var _obj = $(this).find('option[selected]');
                   $('#select2-'+id+'-container').html(_obj.html());
                   $('#select2-'+id+'-container').attr('title', _obj.html());
                   $(this).val(_obj.attr('value'));
               }else{
                   $('#select2-'+id+'-container').html(placeHolder);
                   $('#select2-'+id+'-container').attr('title', placeHolder);
                   $("#" + id).val([]);
               }
        });
    }else {
        $('#' + id).val([]);
        var placeHolder = $('#' + id).attr('placeholder');
        $('#' + id).select2();
        if ($('#' + id + ' option[selected]').length > 0 ) {
            var _obj = $('#' + id +' option[selected]');
            $('#select2-'+id+'-container').html(_obj.html());
            $('#select2-'+id+'-container').attr('title', _obj.html());
               $('#' + id).val(_obj.attr('value'));
        }else{
            $('#select2-'+id+'-container').html(placeHolder);
            $('#select2-'+id+'-container').attr('title', placeHolder);
            $('#' + id).val([]);
        }
    }
}
function replaceAll(find, replace, str) {
  return str.replace(new RegExp(find, 'g'), replace);
}

function checkPhoneNumber(phone){
    if(phone == '')
        return true;
    if (phone.match(/\d/g) == null) {
        return false;
    };
    if(phone.match(/\d/g).length===10 || phone.match(/\d/g).length===11 && phone.match(/\d/g).length == phone.length){
        return true;
    }
    return false;
}
function selectCountry()
{
    // load city
    console.log('country');
}
function selectCity()
{
    $('.js-city-from').change(function(){
        id = $(this).val();
        $('select.js-district-from').empty();
        $('select.js-ward-from').empty();
        $('select.js-district-from').append('<option value="">Chọn Quận/Huyện</option>');
        $('select.js-ward-from').append('<option value="">Chọn Phường/Xã</option>');
        //$('select.js-district-from').parents('.r').find('span.select2-selection__rendered').html('Chọn Quận/Huyện');
        //$('select.js-ward-from').parents('.r').find('span.select2-selection__rendered').html('Chọn Phường/Xã');
        //$('select.js-district-from').parents('.r').find('span.select2-container').addClass('select2-wait');
        //$('select.js-ward-from').parents('.r').find('span.select2-container').addClass('select2-wait');
        
        if (empty(id)) {
            //$('select.js-district-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
            //$('select.js-ward-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
            return false;
        }
        
        
        sParams = '&'+ getParam('sGlobalTokenName') + '[call]=core.loadDistrict';
        sParams += '&city='+id;
        $.ajaxCall({
            data        : sParams,
            obj         : 'load_district',
            dataType    : 'json',
            callback    : function(data){
                if (data.status == 'error') {
                    $('select.js-district-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
                    $('select.js-ward-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
                    content = '<div class="row-poup">'+data.message+'</div>';
                    showSmartAlert(content, 'Lỗi thao tác', 'error');
                    return false;
                }
                else {
                    data = data.data;
                    var html = '';
                    for (var i in data) {
                        html += '<option value="'+ data[i].id+'">'+ data[i].name+'</option>';
                    }
                    $('select.js-district-from').empty();
                    $('select.js-district-from').append('<option value="">Chọn Quận/Huyện</option>');
                    $('select.js-district-from').append(html);
                    $('select.js-ward-from').empty();
                    $('select.js-ward-from').append('<option value="">Chọn Phường/Xã</option>');
                    //$('select.js-district-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
                    //$('select.js-ward-from').parents('.r').find('span.select2-container').removeClass('select2-wait');;
                    //initSelect2('select-district');
                    //initSelect2('select-ward');
                }
            }
        });
    });
}
function selectDistrict()
{
    $('.js-district-from').change(function(){
        id = $(this).val();
        $('select.js-ward-from').empty();
        $('select.js-ward-from').append('<option value="">Chọn Phường/Xã</option>');
        $('select.js-ward-from').parents('.r').find('span.select2-selection__rendered').html('Chọn Phường/Xã');
        $('#select2-select-ward-container').html('Chọn Phường/Xã');
        $('select.js-ward-from').parents('.r').find('span.select2-container').addClass('select2-wait');
        if (empty(id)) {
            $('select.js-ward-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
            return false;
        }
        sParams = '&'+ getParam('sGlobalTokenName') + '[call]=core.loadWard';
        sParams += '&district='+id;
        $.ajaxCall({
            data        : sParams,
            obj         : 'load_ward',
            dataType    : 'json',
            callback    : function(data){
                if (data.status == 'error') {
                    showSmartAlert(data.message, 'Thông Báo', 'error', true);
                    $('select.js-ward-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
                    return false;
                }
                else {
                    data = data.data;
                    var html = '';
                    for (var i in data) {
                        html += '<option value="'+ data[i].id+'">'+ data[i].name+'</option>';
                    }
                    $('select.js-ward-from').empty();
                    $('select.js-ward-from').append('<option value="">Chọn Phường/Xã</option>');
                    $('select.js-ward-from').append(html);
                    //$('select.js-ward-from').parents('.r').find('span.select2-container').removeClass('select2-wait');
                    //initSelect2('select-ward');
                }
            }
        });
    });
}

function showAjaxLoading(obj, type)
{
    obj.addClass('is-button-wait');
    if (type == 'page')
        obj.addClass('load-all-page');
    if (type == 'block')
        obj.addClass('loading-block');
}
function closeAjaxLoading(obj)
{
    setTimeout(function(){
        if(typeof(obj) == 'undefined') {
            $('#container').removeClass('is-button-wait');
            return false;
        }
        obj.removeClass('is-button-wait').removeClass('load-all-page');
        obj.removeClass('loading-block');
    }, 300);
}
var result = '';
$.fn.ajaxSiteCall = function(sCall, sSuccessCall, sExtra, bNoForm, sType)
{
    if (empty(sType))
    {
        sType = 'POST';
    }
    var sUrl = getParam('sJsAjax');
    
    if (!bNoForm) {
        var fdata= false;
        if (window.FormData) {
             fdata = new FormData(this.get(0));
        }
        $.each($('input[type="file"]') , function(i, input) {
            var name = input.getAttribute('name');
            $.each(input.files, function(j, file) {
                fdata.append(name, file);
            });
        });
        fdata.append(getParam('sGlobalTokenName') + '[ajax]', true);
        fdata.append(getParam('sGlobalTokenName') + '[call]', sCall);
        fdata.append(getParam('sGlobalTokenName') + '[security_token]', oCore['log.security_token']);
        xhr = $.ajax({
            crossDomain:true,
            xhrFields: {
                withCredentials: true
            },
            type: "POST",
            url: sUrl,
            dataType: "json",
            cache:false,
            contentType:false,
            processData:false,
            data:fdata,
            success:function(data){
                result = data;
                eval(sSuccessCall);
            },
            error:function(){
                console.log('Có lỗi xảy ra.');
                return false;
            }
        });
        requests.push(xhr);
    }
    else {
        var sParams = '&' + getParam('sGlobalTokenName') + '[ajax]=true&' + getParam('sGlobalTokenName') + '[call]=' + sCall;
        if (sExtra)
        {
            sParams += '&' + ltrim(sExtra, '&');
        }
        if (!sParams.match(/\[security_token\]/i))
        {
            sParams += '&' + getParam('sGlobalTokenName') + '[security_token]=' + oCore['log.security_token'];
        }
         
        xhr = $.ajax({
            crossDomain:true,
            xhrFields: {
                withCredentials: true
            },
            type: sType,
            url: sUrl,//getParam('sJsStatic') + "ajax.php",
            dataType: "json",    
            data: sParams,
            success: function(data){
                result = data;
                eval(sSuccessCall);
            }
        });
        requests.push(xhr);
    }
    
}
$.ajaxSiteCall = function(sCall, sSuccessCall, sExtra, sType)
{
    return $.fn.ajaxSiteCall(sCall, sSuccessCall, sExtra, true, sType);
};
$.fn.getForm = function()
{
    var objForm = this.get(0);    
    var prefix = "";
    var submitDisabledElements = false;
    
    if (arguments.length > 1 && arguments[1] == true) {
        submitDisabledElements = true;
    }
    
    if(arguments.length > 2) {
        prefix = arguments[2];
    }

    var sXml = '';
    if (objForm && objForm.tagName == 'FORM') {
        var formElements = objForm.elements;        
        for(var i=0; i < formElements.length; i++) {
            if (!formElements[i].name) {
                continue;
            }
            
            if (formElements[i].name.substring(0, prefix.length) != prefix) {
                continue;
            }
            
            if (formElements[i].type && (formElements[i].type == 'radio' || formElements[i].type == 'checkbox') && formElements[i].checked == false) {
                continue;
            }
            
            if (formElements[i].disabled && formElements[i].disabled == true && submitDisabledElements == false) {
                continue;
            }
            
            var name = formElements[i].name;
            if (name) {                
                sXml += '&';
                if(formElements[i].type=='select-multiple') {
                    for (var j = 0; j < formElements[i].length; j++) {
                        if (formElements[i].options[j].selected == true) {
                            sXml += name+"="+encodeURIComponent(formElements[i].options[j].value)+"&";
                        }
                    }
                }
                else {
                    sXml += name+"="+encodeURIComponent(formElements[i].value);
                }
            }
        }
    }
    
    if ( !sXml && objForm) {
        sXml += "&" + objForm.name + "="+ encodeURIComponent(objForm.value);
    }    
    
    return sXml;
}
var Base64={_keyStr:"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",encode:function(e){var t="";var n,r,i,s,o,u,a;var f=0;e=Base64._utf8_encode(e);while(f<e.length){n=e.charCodeAt(f++);r=e.charCodeAt(f++);i=e.charCodeAt(f++);s=n>>2;o=(n&3)<<4|r>>4;u=(r&15)<<2|i>>6;a=i&63;if(isNaN(r)){u=a=64}else if(isNaN(i)){a=64}t=t+this._keyStr.charAt(s)+this._keyStr.charAt(o)+this._keyStr.charAt(u)+this._keyStr.charAt(a)}return t},decode:function(e){var t="";var n,r,i;var s,o,u,a;var f=0;e=e.replace(/[^A-Za-z0-9\+\/\=]/g,"");while(f<e.length){s=this._keyStr.indexOf(e.charAt(f++));o=this._keyStr.indexOf(e.charAt(f++));u=this._keyStr.indexOf(e.charAt(f++));a=this._keyStr.indexOf(e.charAt(f++));n=s<<2|o>>4;r=(o&15)<<4|u>>2;i=(u&3)<<6|a;t=t+String.fromCharCode(n);if(u!=64){t=t+String.fromCharCode(r)}if(a!=64){t=t+String.fromCharCode(i)}}t=Base64._utf8_decode(t);return t},_utf8_encode:function(e){e=e.replace(/\r\n/g,"\n");var t="";for(var n=0;n<e.length;n++){var r=e.charCodeAt(n);if(r<128){t+=String.fromCharCode(r)}else if(r>127&&r<2048){t+=String.fromCharCode(r>>6|192);t+=String.fromCharCode(r&63|128)}else{t+=String.fromCharCode(r>>12|224);t+=String.fromCharCode(r>>6&63|128);t+=String.fromCharCode(r&63|128)}}return t},_utf8_decode:function(e){var t="";var n=0;var r=c1=c2=0;while(n<e.length){r=e.charCodeAt(n);if(r<128){t+=String.fromCharCode(r);n++}else if(r>191&&r<224){c2=e.charCodeAt(n+1);t+=String.fromCharCode((r&31)<<6|c2&63);n+=2}else{c2=e.charCodeAt(n+1);c3=e.charCodeAt(n+2);t+=String.fromCharCode((r&15)<<12|(c2&63)<<6|c3&63);n+=3}}return t}}

function callbackUserInfo(data)
{
    dataUser = data;
    if(data === null || data['uid'] < 1)
    {
        //setTimeout(initGetWishlist(), 3000);
    }
    else {
        //initGetWishlist();
    }
    
    if (!dataUser['uid'] || dataUser['uid'] < 0 || dataUser['uid'] == null) {
        $('.not_login').css('display','block');
        $('.login').css('display','none');
    }
    else {
        //Cập nhật thông tin user
        if (typeof(dataUser['uimage']) == 'undefined' || empty(dataUser['uimage'])) {
            dataUser['uimage'] = oParams['sJsImage'] + '/styles/mobile/4038/image/user-unlogin.jpg';
        }
        $('#js-avatar-user').attr('src', dataUser['uimage']);
        var locationId = getCookie('area')*1;
        var oCity = {};
        oCity['723'] = {
            'id': 723,
            'name': 'Hà Nội',
        };
        oCity['753'] = {
            'id': 753,
            'name': 'TP. Hồ Chí Minh',
        };
        var html = '';
        html +=     '<div class="info-top">';
        html +=     '<span class="username" id="js-user-wall" data-type="user_wall">'+ dataUser['uname'] +'</span>';
        html +=     '<i class="fa fa-angle-down js-show-menu-account"></i>';
        html +=     '</div>';
        html +=     '<div class="select-location">';
        html +=     '<span class="fa fa-map-marker js-change-location"></span>';
        if (locationId > 0) {
            html +=     '<span class="address-username js-change-location">'+oCity[locationId].name+'</span>';
        }
        else {
            html +=     '<span class="address-username js-change-location">Chọn địa điểm</span>';
        }
        
        html +=     '<span class="des-location js-change-location">Đổi địa điểm</span>';
        html +=     '</div>';
        $('#js-info-user').html(html);
        
        //html = '<i class="fa fa-angle-down js-show-menu-account"></i>';
        //$('.usr-ifo-lst').removeClass('new-menu-acc');
        //$('#js-toggle-info-menu').append(html);
        //$('.yeu_cau_dang_nhap').removeClass('yeu_cau_dang_nhap');
        //$('.usr-ifo-ttl').html(dataUser['uname']);
        html = '<a class="js-acc-menu-m fst-child" data-type="user_account" href="javascript:void(0);");" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/thongtin.png\')no-repeat 10px">Thông tin tài khoản</a>';
        html += '<a class="js-acc-menu-m" data-type="history_order" href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px 8px">lịch sử đơn hàng</a>';
        html += '<a class="js-acc-menu-m" data-type="history_trans" href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px -33px">lịch sử giao dịch</a>';
        html += '<a class="js-acc-menu-m" data-type="recharge" href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px -76px">nạp tiền vào tài khoản</a>';
        html += '<a class="js-acc-menu-m" data-type="favorite" href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px -116px">danh sách yêu thích</a>';
        html += '<a class="js-acc-menu-m" data-type="notice" href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px -162px">Thông báo</a>';
        html += '<a class="js-acc-menu-m" data-type="ask_answer" href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px -162px">hỏi đáp</a>';
        html += '<a href="javascript:void(0);" style="background: transparent url(\''+ oParams['sJsImage'] +'/styles/mobile/4038/image/nav-menu-icon.png\') no-repeat 0px -202px" class="js_logout">đăng xuất</a>';
        
        $('#js-info-menu-content').html(html);
        $('.js-show-menu-account').click(function(){
            $('.js-show-menu-account').toggleClass('rotate');
            $('#js-info-menu-content').toggleClass('hide');
        });
        initCallLogout();
        initAccountMenu();
        initSelectLocation();
    }
}
function initCallLogout()
{
    $('.js_logout').unbind('click').click(function(){
        //$Core.box('user.logout', 400, '', 'Đăng xuất');
        //Chuyển thành call ajax logout
        sParams = '&'+ getParam('sGlobalTokenName') + '[call]=user.ApiLogout';
        $.ajax({
            crossDomain:true,
            xhrFields: {
                withCredentials: true
            },
            url: getParam('sJsAjax'),
            type: "POST",
            data: sParams,
            timeout: 15000,
            cache:false,
            dataType: 'json',
            error: function(jqXHR, status, errorThrown){
                showSmartAlert('Lỗi hệ thống', 'Thông Báo', 'error', true);
            },
            success: function (result) {
                if(isset(result.status) && result.status == 'success') {
                    logoutCallback();
                }
                else {
                    var messg = isset(result.message) ? result.message : 'Lỗi hệ thống';
                    showSmartAlert(messg, 'Thông Báo', 'error', true);
                }
            }
        });
        return false;
    });
}

function logoutCallback()
{
    /* refesh page */
    localStorage.removeItem('userinfo');
    // xóa nội dung chat cũ. 
    localStorage.removeItem('datachat');
    localStorage.removeItem('shopcart');
    window.location.reload(false);
}

var ptype_call = '';
var pstatus = 1;
function callAjaxLogin(type)
{
    //parent.window.location = oParams['sJsHome']+'dang_nhap.html?refer='+refer;
    // call login popop.
    closeAjaxLoading($('html'));
    ptype_call = type;
    var pwidth = 470;
    var ptitle = 'Đăng Nhập';
    var pparam = '';
    if (oParams['sController'] == 'shop.view') {
        pwidth = 840;
        ptitle = 'Đặt hàng';
        pparam = 'chechout=1'
    }
    //$Core.box('user.callLoginPopup', pwidth, pparam, ptitle);
}
function initPopupLogin()
{
    pstatus = 1;
    // remove nút close
    //$('.control-close-pop-up').hide();
    $('#js-lp-bt').unbind('click').click(function(){
        $('.error_popup').hide();
        $('.message_popup').hide();
        /*  Kiểm tra điều kiện đăng nhập */
        if($('#ten_truy_cap').val() == '') {
            $('.error_popup').html('Vui lòng điền tên truy cập');   
            $('#ten_truy_cap').focus();
            $('.error_popup').show();
            return false;
        }
        if($('#mat_khau').val() == '') {
            $('.error_popup').html('Vui lòng điền mật khẩu');   
            $('#mat_khau').focus();
            $('.error_popup').show();
            return false;
        }

        sParams = '&'+ getParam('sGlobalTokenName') + '[call]=user.login';
        sParams += '&val[email]=' + encodeURIComponent($('#ten_truy_cap').val());
        sParams += '&val[passwd]=' + encodeURIComponent($('#mat_khau').val());
        sParams += '&val[remember]=1';
        
        showAjaxLoading($('#js-lp-bt'), 'button');
        $.ajaxCall({
            data: sParams,
            obj: 'user_plogin',
            dataType: 'json',
            callback: function(data){
                ploginStatus(data);
            }
        });
        return false;
    });
    $('#js-qlg-popup').unbind('click').click(function(){
        $('.error_popup').hide();
        $('.message_popup').hide();
        /*  Kiểm tra điều kiện đăng nhập */
        if($('#js-full-name').val() == '') {
            showSmartAlert('Vui lòng nhập họ tên người nhận.', 'Thông Báo', 'error', true);
            //$('#js-full-name').focus();
            return false;
        }
        if($('#js-phone_number').val() == '') {
            showSmartAlert('Vui lòng điền số điện thoại.', 'Thông Báo', 'error', true);
            //$('#js-phone_number').focus();
            return false;
        }
        if (!checkPhoneNumber($('#js-phone_number').val())) {
            showSmartAlert('Số điện thoại người nhận hàng không hợp lệ!', 'Thông Báo', 'error', true);
            return false;
        }
        email = $('#js-dia-chi').val();
        if(email == '') {
            showSmartAlert('Vui lòng điền địa chỉ email.', 'Thông Báo', 'error', true);
            //$('#js-dia-chi').focus();
            return false;
        }
        
        var parternEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (!parternEmail.test(email)){
            showSmartAlert('Email của bạn không thích hợp.', 'Thông Báo', 'error', true);
            //$('#js-dia-chi').focus();
            return false;
        }
        
        sParams = '&'+ getParam('sGlobalTokenName') + '[call]=user.quickBuy';
        sParams += '&val[phone_number]='+encodeURIComponent($('#js-phone_number').val());
        sParams += '&val[email]='+ encodeURIComponent($('#js-dia-chi').val());
        sParams += '&val[fullname]='+ encodeURIComponent($('#js-full-name').val());
        showAjaxLoading($('#js-qlg-popup'), 'button');
        $.ajaxCall({
            data: sParams,
            obj: 'user_qlogin',
            dataType: 'json',
            callback: function(data){
                if (data.status == 'error') {
                    content = '<div class="row-poup">'+data.message+'</div>';
                    showSmartAlert(content, 'Lỗi thao tác', 'error');
                    return false;
                }
                else {
                    // lưu thông tin data xuống local storage để load lên.
                    localStorage.removeItem('buy-without-login');
                    user = data.data;
                    localStorage.setItem('buy-without-login', JSON.stringify(user));
                    // tiếp tục thực hiện các bước như sau khi đăng nhập.
                    // close popup login.
                    closeAjaxLoading($('#js-lp-bt'));
                    pstatus = 0;
                    $('.popup-close').click();
                    for (var i in afterLoginProcess) {
                        eval(afterLoginProcess[i]);
                    }
                    if (oParams['sController'] != 'shop.view') {
                        window.location = '/shop_chi_tiet.html';
                    }
                }
            }
        });
        return false; 
    });
    $('.popup-close').click(function(){
        if (pstatus == 0)
            return false;
        if (ptype_call == 'reload') {
            window.location.reload();
            return false;
        }
        if (ptype_call == 'redirect') {
            window.location = '/';
        }
    });
    $('#js-forgot-pass-pop').unbind('click').click(function(){
        window.location = '/quen_mat_khau.html';
        return false; 
    });
    $('#js-lp-reg').unbind('click').click(function(){
        var refer = $(this).attr('data-link');
        window.location = refer; 
        return false;
    });
    $('.button-login-fb').unbind('click').click(function(){
         showUpgradeMessage();
         return false; 
    });
}
function ploginStatus(data)
{
    if (data.status == 'error') {
        closeAjaxLoading($('#js-lp-bt'));
        closeAjaxLoading($('#js-qlg-popup'));
        $('.error_popup').html(data.message);
        $('.error_popup').show();
        return false;
    }
    if (data.status == 'success') {
        $('.error_popup').hide();
        $('.message_popup').html('Đăng nhập thành công');
        $('.message_popup').show();
        // remove local storage.
        localStorage.removeItem('userinfo');
        localStorage.removeItem('buy-without-login');
        // lấy thông tin thành viên để thay đổi header.
        data = data.data;
        user = data.info;
        localStorage.setItem('userinfo', JSON.stringify(user));
        xu_ly_thong_ke(user);
        // close popup login.
        closeAjaxLoading($('#js-lp-bt'));
        pstatus = 0;
        $('.popup-close').click();
        for (var i in afterLoginProcess) {
            eval(afterLoginProcess[i]);
        }
        if (oParams['sController'] == 'user.register' || oParams['sController'] == 'user.login' || oParams['sController'] == 'user.forgot') {
            window.location = '/';
        }
        if (bSelectPurchasePage && oParams['sController'] != 'shop.view') {
            window.location = '/shop_chi_tiet.html';
        }
    }
}

function loadActionAllPage()
{
    //View list vendor
    $('.js-view-vendors').unbind('click').click(function(){
        if ($('#l-list-market').length < 1) {
            var html = '<section id="l-list-market" class="container">' +
                            '<div class="shopping-cart-page-header">' +
                                '<i class="fa fa-angle-left js-back-vendor-list"></i>' +
                                '<label for="" class="js-header-title">Chọn siêu thị</label>' +
                            '</div>' +
                            '<div id="js-vendors-container">' +
                            '</div>' +
                        '</section>';
            $(html).insertAfter('.dr-shopping-cart-page');
        }
        $('#l-list-market').toggleClass('show');
        $('body').toggleClass('hidden');
        
        $('.js-back-vendor-list').unbind('click').click(function(){
            $('#l-list-market').toggleClass('show');
            $('body').toggleClass('hidden');
            $('#l-list-market').remove();
        });
        //show ajax loading
        var sParams = '&'+ getParam('sGlobalTokenName') + '[call]=vendor.loadVendorList';
        var locationId = getCookie('area')*1;
        sParams += '&area=' + locationId;
        $.ajaxCall({
            obj: 'load',
            data: sParams,
            dataType: 'text',
            type: 'POST',
            timeout: 8000,
            callback: function(data){
                if (!empty(data)) {
                    $('#js-vendors-container').html(data);
                    $('.js-select-vendor').unbind('click').click(function(){
                        //đẩy qua trang view chi tiết 1 siêu thị
                        
                        var vendor_name = $(this).find('.name-market').html();
                        var vendor_id = $(this).attr('data-id')*1;
                        if (vendor_id < 1) {
                            showSmartAlert('Không chọn được siêu thị', 'Thông Báo', 'error', true);
                            return false;
                        }
                        var html = '<div class="container" id="m-detail">' +
                                        '<div class="shopping-cart-page-header">' +
                                            '<i class="fa fa-angle-left js-back-vendor"></i>' +
                                            '<label for="" class="js-header-title">'+ vendor_name +'</label>' +
                                        '</div>' +
                                        '<div id="js-vendors-detail">' +
                                        '</div>' +
                                    '</div>';
                        
                        $(html).insertAfter('#l-list-market');
                        $('#l-list-market').toggleClass('show');
                        $('#m-detail').toggleClass('show');
                        
                        //back event
                        $('.js-back-vendor').unbind('click').click(function(){
                            $('#l-list-market').toggleClass('show');
                            $('#m-detail').toggleClass('show');
                            $('#m-detail').remove();
                        });
                        
                        //show ajax loading
                        var sParams = '&'+ getParam('sGlobalTokenName') + '[call]=vendor.showInfoVendor' + '&vid=' + vendor_id;
                        var locationId = getCookie('area')*1;
                        sParams += '&area=' + locationId;
                        $.ajaxCall({
                            obj: 'load',
                            data: sParams,
                            dataType: 'text',
                            type: 'POST',
                            timeout: 8000,
                            callback: function(data){
                                if (!empty(data)) {
                                    $('#js-vendors-detail').html(data);
                                }
                                else {
                                    showSmartAlert('Lỗi hệ thống', 'Thông Báo', 'error', true);
                                }
                            }
                        });
                    });
                }
                else {
                    showSmartAlert('Lỗi hệ thống', 'Thông Báo', 'error', true);
                }
            }
        });
        
        
        
        return;
        window.location  = '/vendor/';
    });
    if (oParams['sController'] == 'vendor.list') {
        return;
        $('footer.container').hide();
        $('.js-select-vendor').unbind('click').click(function(){
            //đẩy qua trang view chi tiết 1 siêu thị
            
            var vendor_name = $(this).find('.name-market').html();
            var vendor_id = $(this).attr('data-id')*1;
            if (vendor_id < 1) {
                showSmartAlert('Không chọn được siêu thị', 'Thông Báo', 'error', true);
                return false;
            }
            var html = '<div class="container" id="m-detail"></div>';
            
            $(html).insertAfter('#l-list-market');
            $( "#m-detail" ).animate({
                left: 0,
                height: "toggle"
            }, 500);
            $( "#l-list-market" ).animate({
                left: '-480px',
                height: "toggle"
            }, 500);
            var objBack = $('.left-container').find('.js-toggle-menu');
            if (objBack) {
                objBack.hide();
                $('.left-container').prepend('<i class="fa fa-angle-left js-back-vendor"></i>');
            }
            if (vendor_name) {
                $('.left-container .head-title-center').html(vendor_name);
            }
            //back event
            $('.js-back-vendor').unbind('click').click(function(){
                $( "#l-list-market" ).animate({
                    left: 0,
                    height: "toggle"
                }, 500);
                $( "#m-detail" ).animate({
                    left: '-480px',
                    height: "toggle"
                }, 500);
                $('#m-detail').remove();
                
                $(this).remove();
                $('.left-container').find('.js-toggle-menu').show();
                $('.left-container .head-title-center').html('Chọn siêu thị');
            });
            
            //show ajax loading
            var sParams = '&'+ getParam('sGlobalTokenName') + '[call]=vendor.showInfoVendor' + '&vid=' + vendor_id;
            var locationId = getCookie('area')*1;
            sParams += '&area=' + locationId;
            $.ajaxCall({
                obj: 'load',
                data: sParams,
                dataType: 'text',
                type: 'POST',
                timeout: 8000,
                callback: function(data){
                    if (!empty(data)) {
                        $('#m-detail').html(data);
                    }
                    else {
                        showSmartAlert('Lỗi hệ thống', 'Thông Báo', 'error', true);
                    }
                }
            });
        });
    }
    
    /* save cart */
    $('#js-save-cart').click(function(){
        is_allow = $(this).hasClass('atv')*1;
        if (is_allow == 0) {
            return;
        }
        //Kiểm tra đăng nhập bẳng js
        var data = dataUser;
        
        if(data === null || data['uid'] < 1)
        {
            refer = '';
            callAjaxLogin(refer);
            return;
        }
        
        //Kiểm tra giỏ hàng có sản phẩm hay không trước khi hiện popup
        if(checkLocalStorage('shopcart') == false){
            showSmartAlert('Chưa có sản phẩm nào trong giỏ hàng!', 'Thông Báo', 'error', true);
            return;
        }
        $Core.box('shop.showPopupSaveCart', 600, '', 'Lưu giỏ hàng');
    });

    $('.js-purchase').unbind('click').click(function(){
        // kiểm tra xem có sản phẩm trong giỏ hàng hay không.
        shopcart = getLocalStorage('shopcart');
        if (empty(shopcart) && $('.is-row-product-pop').length < 1) {
            content = '<div class="row-poup center">Vui lòng cho sản phẩm vào giỏ trước khi đặt mua.</div>';
            showSmartAlert(content, 'Lỗi thao tác', 'error'); 
            return false;  
        }
        if (!empty(shopcart) && (!isset(shopcart.product) || shopcart.product.length < 1)) {
            content = '<div class="row-poup center">Vui lòng cho sản phẩm vào giỏ trước khi đặt mua.</div>';
            showSmartAlert(content, 'Lỗi thao tác', 'error'); 
            return false;  
        }
        // lấy thông tin từ local storage.
        pwidth = 840;
        ptitle = 'Đặt hàng';
        pparam = 'chechout=1';
        userinfo =  getLocalStorage('userinfo');
        if (!isset(userinfo.uid) || !userinfo.uid) {
            userinfo = localStorage.getItem('buy-without-login');
            if (empty(userinfo)) {
                // gọi đăng nhập
                bSelectPurchasePage = true;
                //$Core.box('user.callLoginPopup', pwidth, pparam, ptitle);
                
                window.location = '/dang_nhap.html';
                return false;
            }
        }
        window.location = '/shop_chi_tiet.html';
    });
    $('#js-post-cart').click(function(){
        is_allow = $(this).hasClass('atv')*1;
        if (is_allow == 0) {
            return;
        }
        //Kiểm tra đăng nhập bẳng js
        var data = dataUser;
        if(data === null || data['uid'] < 1)
        {
            refer = '';
            callAjaxLogin(refer);
            return;
        }
        //Kiểm tra giỏ hàng có sản phẩm hay không trước khi hiện popup
        if(checkLocalStorage('shopcart') == false){
            showSmartAlert('Chưa có sản phẩm nào trong giỏ hàng!', 'Thông Báo', 'error', true);
            return;
        }
        
        $Core.box('shop.showPopupSaveCart', 600, '', 'Lưu giỏ hàng');
    });
    // hàm chạy ajax cập nhật các dữ liệu không phải cache cho các page.
    var param = {};
    if (oParams['sController'] == 'core.index') {
        param['block'] = {};
        param['block']['#js-block-view-product'] = 'view_product';
        //param['block']['#js-top-product-home'] = 'top_product_home';
        //param['block']['#js-top-user'] = 'top_user';
        //param['block']['#js-top-buy'] = 'top_buy';
    }
    
    if (oParams['sController'] == 'cart.view') {
        param['block'] = {};
        param['block']['#js-block-view-product'] = 'view_product';
        param['block']['#js-block-cart-container'] = 'cart_food_relate';
        param['block']['#js-block-food-container'] = 'cart_latest';
        param['val'] = {};
        param['val']['cart_food_relate'] = {};
        param['val']['cart_food_relate']['item_id'] = $('#cart_id').val()*1;
    }
    
    if (oParams['sController'] == 'article.index') {
        param['block'] = {};
        param['block']['#js-block-view-product'] = 'view_product';
        param['data'] = {};
        param['data']['article'] = $('#product_id').val();
        param['block']['#js-block-cart-container'] = 'get_by_cart';
        param['block']['#js-block-food-container'] = 'get_by_food';
        param['val'] = {};
        param['val']['get_by_cart'] = {};
        param['val']['get_by_cart']['product_id'] = $('#product_id').val()*1;
        param['val']['get_by_food'] = {};
        param['val']['get_by_food']['product_id'] = $('#product_id').val()*1;
        
        var search = location.search;
        var vendor = 0;
        if (!empty(search)) {
            match = search.match('vendor=([0-9]+)');
            if (!empty(match))
                vendor = match[1]
        }
        if(!empty(vendor))
            param['data']['vendor'] = vendor;
        
        if ($('#community-food-detail').length > 0) {
            param['block']['#js-top-user'] = 'top_user';
            param['block']['#js-community-food-relate'] = 'food_relate';
            param['block']['#js-food-favorite'] = 'food_favorite';
            param['val'] = {};
            param['val']['food_relate'] = {};
            param['val']['food_relate']['cat_id'] = $('#js-food-category-id').val()*1;
            param['val']['food_relate']['food_id'] = $('#product_id').val()*1;
        }
        
        //get info mkt
        var a_sku = [];
        $('.js-info-mkt').each(function(){
            var tmp_sku = $(this).attr('data-sku');
            if (tmp_sku != '') {
                a_sku.push(tmp_sku);
            }
        });
        if (a_sku.length > 0) {
            param['data']['marketing'] = a_sku.join();;
        }
    }
    
    if (oParams['sController'] == 'vendor.index') {
        var current_vendor_id = $('#js-view-products').attr('data-id')*1;
        if (current_vendor_id > 0) {
            //param['block'] = {};
            //param['block']['#js-top-product-home'] = 'top_product_home';
            //param['val'] = {};
            //param['val']['top_product_home'] = {};
            //param['val']['top_product_home']['vendor'] = current_vendor_id;
        }
    }
    
    if (oParams['sController'] == 'category.index') {
        //param['block'] = {};
        //param['block']['#js-top-product-home'] = 'top_product_home';
        
        //show menu cha nếu đang trong menu con
        var parent_id = $('#l-home').attr('data-parent')*1;
        if (parent_id > 0) {
            $('.menu-item').find("i[data-id='"+ parent_id +"']").click();
        }
    }
    
    if (typeof(param['block'] != 'undefined')) {
        for (var i in param['block']) {
            showAjaxLoading($(i), 'block');
        }
    }
    
    //if (empty(param))
        //return false;
    sparam = JSON.stringify(param);
    
    // gọi ajax để cập nhật thông tin.
    var sParams = '&'+ getParam('sGlobalTokenName') + '[call]=core.loadActionM';
    closeAjaxLoading($('.button_submit_log'));
    sParams += '&param=' + encodeURIComponent(sparam);
    $.ajaxCall({
        obj: 'load-action',
        data: sParams,
        type: 'POST',
        timeout: 8000,
        callback: function(actiondata){
            loadActionCallback(actiondata);
        }
    });
}
/*  Hảm xử lý lazy load hình ảnh */
function lazyLoadImage() {
    //$('img.lazy').lazyLoadXT();
    $("img.lazy").lazyload();
    $('.is-lazy-horizontal').each(function(){
        var objContainer = $(this);
        objContainer.find('img.lazy').lazyload({
            container: objContainer,
        });
    });
}
function loadActionCallback(actiondata)
{
    //console.log(actiondata);
    if (actiondata.status == 'error') {
        // close all ajaxloading and empty data.
        $('.is-button-wait.loading-block').html('');
        $('.is-button-wait.loading-block').removeClass('is-button-wait');
        $('.loading-block').removeClass('loading-block');
        showSmartAlert(actiondata.message, 'Thông Báo', 'error', true);
        return false;
    }
    if (actiondata.status == 'success') {
        data = actiondata.data;
        // load block content.
        if (typeof(data.block) != 'undefined') {
            var action = data.block;
            for (var i in action) {
                closeAjaxLoading($(i));
                $(i).parents('.js-block').html(action[i]);
            }
            //hoverShow();
            //scrollMenu();
            //changeTab();
            //iosSlideInit();
            lazyLoadImage();
            
            initBlockTab();
        }
        // load ads
        if (typeof(data.data) != 'undefined') {
            
        }
        // 
        if (typeof(data.data) != 'undefined') {
            for (var i in data.data) {
                if (i == 'article') {
                    $('#js-article-rate').attr('data-value', data.data[i].rating);
                    $('#js-article-tora').html(data.data[i].total_rate);
                    $('#js-total-buy').html(data.data[i].total_buy);
                    $('#js-total-view').html(data.data[i].total_view);
                }
                else if (i == 'marketing') {
                    var oMktTxt = data.data[i];
                    console.log(oMktTxt);
                    $('.js-info-mkt').each(function(){
                        var tmp_sku = $(this).attr('data-sku');
                        var obj_mkt = $(this);
                        if (typeof (oMktTxt[tmp_sku]) != 'undefined' && typeof(oMktTxt[tmp_sku].mkt_info) != 'undefined') {
                            var oTxt = oMktTxt[tmp_sku].mkt_info;
                            if (oTxt.length > 0) {
                                
                                var html_mkt = '';
                                for (var k in oTxt) {
                                    html_mkt += '<div class="mkt-txt">';
                                    html_mkt += '<span class="fa fa-check"></span>';
                                    html_mkt += oTxt[k];
                                    html_mkt += '</div>';
                                }
                                if (html_mkt != '') {
                                    obj_mkt.addClass('atv');
                                    obj_mkt.html(html_mkt);
                                }
                            }
                            
                        }
                    });
                }
            }
        }
    }
}

/*  Hàm xử lý khi check 1 group siêu thị trong Check out */
function checkGroupMart(data){
    var subIsCheckBox = $('.is-check-box[data-id="' + data.id + '"]').parents('.j8').find('.x31 .is-check-box');
    if(data.state == 1){
        subIsCheckBox.removeClass('fa-square-o');
        subIsCheckBox.addClass('fa-check-square-o');
    }else{
        subIsCheckBox.removeClass('fa-check-square-o');
        subIsCheckBox.addClass('fa-square-o');
    }

    subIsCheckBox = $('.is-check-box[data-id="' + data.id + '"]').parents('.j8').find('.x3 .is-check-box');
    if(data.state == 1){
        subIsCheckBox.removeClass('fa-square-o');
        subIsCheckBox.addClass('fa-check-square-o');
    }else{
        subIsCheckBox.removeClass('fa-check-square-o');
        subIsCheckBox.addClass('fa-square-o');
    }

    totalPriceCheckOut();
}
function checkGroupCart(data){
    var subIsCheckBox = $('.is-check-box[data-id="' + data.id + '"]').parents('.x30').find('.x3 .is-check-box');
    if(data.state == 1){
        subIsCheckBox.removeClass('fa-square-o');
        subIsCheckBox.addClass('fa-check-square-o');
    }else{
        subIsCheckBox.removeClass('fa-check-square-o');
        subIsCheckBox.addClass('fa-square-o');
    }
    
    var rowMart = $('.is-check-box[data-id="' + data.id + '"]').parents('.j8');
    if(rowMart.find('.x3 .is-check-box.fa-check-square-o').length > 0){
        rowMart.find('.x1 .is-check-box').removeClass('fa-square-o');
        rowMart.find('.x1 .is-check-box').addClass('fa-check-square-o');
    }else{
        rowMart.find('.x1 .is-check-box').removeClass('fa-check-square-o');
        rowMart.find('.x1 .is-check-box').addClass('fa-square-o');
    }

    totalPriceCheckOut();   
}
/*  Callback check row */
function checkRowMart(data){
     var rowMart = $('.is-check-box[data-id="' + data.id + '"]').parents('.x30');
    if(rowMart.find('.x3 .is-check-box.fa-check-square-o').length > 0){
        rowMart.find('.x31 .is-check-box').removeClass('fa-square-o');
        rowMart.find('.x31 .is-check-box').addClass('fa-check-square-o');
    }else{
        rowMart.find('.x31 .is-check-box').removeClass('fa-check-square-o');
        rowMart.find('.x31 .is-check-box').addClass('fa-square-o');
    }

    rowMart = $('.is-check-box[data-id="' + data.id + '"]').parents('.j8');
    if(rowMart.find('.x3 .is-check-box.fa-check-square-o').length > 0){
        rowMart.find('.x1 .is-check-box').removeClass('fa-square-o');
        rowMart.find('.x1 .is-check-box').addClass('fa-check-square-o');
    }else{
        rowMart.find('.x1 .is-check-box').removeClass('fa-check-square-o');
        rowMart.find('.x1 .is-check-box').addClass('fa-square-o');
    }

    totalPriceCheckOut();   
}
/*  Hàm đóng mở Group expand */
function expandGroup(){
    $('.is-expand-group').each(function(){
        var isExpandGroup   = $(this);
        var idGroup         = isExpandGroup.data('id');
        var isExpandNav     = isExpandGroup.find('.is-expand-nav[data-id="' + idGroup + '"]');
        var isExpandPanel   = isExpandGroup.find('.is-expand-panel[data-id="' + idGroup + '"]');
        var outExpandPanel  = isExpandGroup.parents('.is-expand-panel');

        isExpandPanel.attr('height-expand', isExpandPanel.height());
        /*isExpandPanel.height(isExpandPanel.height());*/
        isExpandNav.unbind('click').click(function(){
            if(isExpandNav.hasClass('fa-angle-down') || isExpandNav.hasClass('fa-angle-up')) {
                if(isExpandNav.hasClass('fa-angle-down')){
                    outExpandPanel.removeAttr('style');
                    isExpandNav.removeClass('fa-angle-down');
                    isExpandNav.addClass('fa-angle-up');
                    isExpandPanel.hide();
                    setTimeout(function(){
                        outExpandPanel.attr('height-expand', outExpandPanel.height());
                        /*outExpandPanel.height(outExpandPanel.height());*/
                    }, 500);
                }else{
                    outExpandPanel.removeAttr('style');
                    isExpandNav.removeClass('fa-angle-up');
                    isExpandNav.addClass('fa-angle-down');
                    isExpandPanel.show();
                    setTimeout(function(){
                        outExpandPanel.attr('height-expand', outExpandPanel.height());
                        /*outExpandPanel.height(outExpandPanel.height());*/
                    }, 500);
                }
            }else{
                if(isExpandNav.hasClass('fa-minus-circle')){
                    outExpandPanel.removeAttr('style');
                    isExpandNav.removeClass('fa-minus-circle');
                    isExpandNav.addClass('fa-plus-circle');
                    isExpandPanel.hide();
                    setTimeout(function(){
                        outExpandPanel.attr('height-expand', outExpandPanel.height());
                        /*outExpandPanel.height(outExpandPanel.height());*/
                    }, 500);
                }else{
                    outExpandPanel.removeAttr('style');
                    isExpandNav.removeClass('fa-plus-circle');
                    isExpandNav.addClass('fa-minus-circle');
                    isExpandPanel.show();
                    setTimeout(function(){
                        outExpandPanel.attr('height-expand', outExpandPanel.height());
                        /*outExpandPanel.height(outExpandPanel.height());*/
                    }, 500);
                }
            }
        });
    });
}
function remByVal(obj, val) {
    for (var i = 0; i < obj.length; i++) {
        if (obj[i] === val) {
            obj.splice(i, 1);
            i--;
        }
    }
    return obj;
}

/*  Hàm Insert data popup Giỏ hàng */
function insertProductCartPopup(data){
    if(typeof(data) == 'undefined') return false;
    listMart = data.vendor;
    listCart = data.cart;
    listProduct = data.product;
    if(typeof(listMart) == 'undefined' || typeof(listCart) == 'undefined' || typeof(listProduct) == 'undefined')
        return false;
    var shopCart;
    if(checkLocalStorage('shopcart') == true){
        shopCart = getLocalStorage('shopcart');
        if(!shopCart)
            return false;
        if(listMart.length > 0) {
            $.each(listMart, function(key, value) {
                var check = false;
                $.each(shopCart.vendor, function(keyLS, valueLS) {
                    if(value.id == valueLS.id){
                        check = true;
                        return false;
                    }
                });
                if(check == false) {
                    shopCart['vendor'].push(value);
                }
            });
        }

        if(listCart.length > 0) {
            $.each(listCart, function(key, value) {
                var check = false;
                $.each(shopCart.cart, function(keyLS, valueLS) {
                    if(value.id == valueLS.id){
                        check = true;
                        return false;
                    }
                });     
                if(check == false) {
                    shopCart['cart'].push(value);
                }
            });
        }

        $.each(listProduct, function(key, value) {
            var check = false;
            $.each(shopCart.product, function(keyLS, valueLS) {
                if(value.id == valueLS.id){
                    check = true;
                    if(value.quantity == 0){
                        shopCart.product = remByVal(shopCart.product, keyLS);
                    }else{
                        shopCart.product[keyLS].quantity = valueLS.quantity * 1 + value.quantity;
                    }
                    shopCart.product[keyLS].unit_price = value.unit_price;
                    return false;
                }
            });     
            if(check == false) {
                shopCart['product'].push(value);
            }
        });
        setLocalStorage('shopcart', shopCart);
    }
    else {
        shopCart = {
            'vendor'    : listMart,
            'cart'      : listCart,
            'product'   : listProduct
        };
        setLocalStorage('shopcart', shopCart);
    }

    if(listMart.length > 0 && listCart.length > 0)
        showProdcutCartPopup(shopCart);
}

/*  hiển thị sản phẩm */
function showProdcutCartPopup(shopCart){
    if(!shopCart) 
        return false;
    var content = '';
    if(typeof(shopCart.vendor) != 'undefined' &&  typeof(shopCart.cart) != 'undefined' &&  typeof(shopCart.product) != 'undefined' ) {
        for(var i = 0; i < shopCart.cart.length; i++) {
            for(var j = 0; j < shopCart.vendor.length; j++){
                if(shopCart.vendor[j].cart_id == shopCart.cart[i].id) {
                    content +=      '<div class="shopping-cart-market-item is-expand-group" data-id="group-mart-' + shopCart.vendor[j].id + '">\
                                        <div class="shopping-market-header">\
                                            <i class="fa fa-minus-circle is-expand-nav" data-id="group-mart-' + shopCart.vendor[j].id + '"></i>\
                                            <label for="" class="is-label-check-box" data-id="mart-' + shopCart.vendor[j].id + '">'+shopCart.vendor[j].name+'</label>\
                                        </div>\
                                        <div class="shopping-market-body is-expand-panel" data-id="group-mart-' + shopCart.vendor[j].id + '">';
                    for(var k = 0; k < shopCart.product.length; k++){
                        if((shopCart.product[k].cart_id == shopCart.cart[i].id) && (shopCart.product[k].vendor_id == shopCart.vendor[j].id)) {
                            content += '<div class="shopping-item is-row-product-pop" data-id="' + shopCart.product[k].id +'" data-vendor="'+ shopCart.product[k].vendor_id +'" data-sku="'+ shopCart.product[k].sku +'">\
                                            <div class="m-block block-image">\
                                                <a href="//'+ oParams['sJsHostname']+ shopCart.product[k].detail_path+'?vendor='+shopCart.vendor[j].id+'" class="t7">\
                                                    <img src="' + shopCart.product[k].image_path + '">\
                                                </a>\
                                            </div>\
                                            <div class="m-block block-info">\
                                                <div>\
                                                    <a href="//'+ oParams['sJsHostname']+ shopCart.product[k].detail_path+'?vendor='+shopCart.vendor[j].id+'" title="'+shopCart.product[k].name+'">'
                                                        + shopCart.product[k].name +
                                                    '</a>\
                                                </div>\
                                                <div>'
                                                    + shopCart.product[k].unit_value + ' ' + shopCart.product[k].unit.name +
                                                '</div>\
                                                <div>\
                                                    <span>\
                                                        <span class="vl is-money" data-money="'+ (shopCart.product[k].price_sell - shopCart.product[k].price_discount) +'">'+(shopCart.product[k].price_sell - shopCart.product[k].price_discount)+'</span>\
                                                        <span class="t8a">đ</span>\
                                                    </span>\
                                                </div>\
                                                <div class="remove-product is-remove-product-pop"><i class="fa fa-times"></i>bỏ sản phẩm</div>\
                                            </div>\
                                            <div class="m-block block-option-quantity">\
                                                <div class="option-quantity">\
                                                    <span class="option-button down-button is-change-quanlity-pop"><i class="fa fa-minus"></i></span>\
                                                    <input disabled="disabled" type="text" class="t83" value="'+ shopCart.product[k].quantity +'" min="1">\
                                                    <span class="option-button up-button is-change-quanlity-pop"><i class="fa fa-plus"></i></span>\
                                                </div>\
                                                <div class="price is-money is-money-ajax wait" \
                                                    data-quantity="'+shopCart.product[k].quantity+'"\
                                                    data-id="'+shopCart.product[k].id+'" \
                                                    data-vendor="'+shopCart.vendor[j].id+'"\
                                                    id="pop-'+shopCart.product[k].id+'"\
                                                    data-sku="'+shopCart.product[k].sku+'"\
                                                    data-money="'+shopCart.product[k].quantity * (shopCart.product[k].price_sell - shopCart.product[k].price_discount) +'"></div><span class="price"> đ</span>\
                                            </div>\
                                        </div>';
                        }
                    }
                    content +=  '</div></div>';
                }
            }
            content +=  '</div>';
        }
    }
    $('.dr-shopping-cart-page .shopping-cart-page-body').html(content);
    //$('.dr-shopping-cart-page .product-count').html('('+cntProduct+')');
    //$('.count-product').html('('+shopCart.product.length+')');
    /*
    $('.is-content-cart').mCustomScrollbar({
        theme           : "minimal-dark",
        scrollInertia   : 200,
        scrollEasing    : "linear"
    }); */
    //$('.is-shop-header').addClass('show');
    //    setTimeout(function(){
    //        $('.is-shop-header').removeClass('show');
    //    }, 1500);
    /* Cập nhật button lưu giỏ hàng */
    is_save_cart = localStorage.getItem('save_cart');
    if (is_save_cart == null || is_save_cart == 0) {
        //allow save cart
        setStatusSaveCartON();
    }
    else {
        is_re_buy_cart = localStorage.getItem('re_buy_cart');
        if (is_re_buy_cart != 1){
            is_re_buy_cart = 0;
        }
        setStatusSaveCartOFF(is_save_cart, is_re_buy_cart);
    }
    calcPriceRowProductPop();
    changeQuanlityProductPop(); 
    removeProductPop();
    //scrollMenu();
    initRemoveCart();
    expandGroup();
}

function initRemoveCart()
{
    $('#js-clear-product-cart').unbind('click').click(function(){
        $('.is-shop-header').addClass('show');
        content = 'Quý khách có chắc muốn xóa toàn bộ sản phẩm trong giỏ?';
        showSmartAlert(content, 'Thông Báo', 'message', 1, 1);
        $('.js-agree-btn-popup').unbind('click').click(function(){
            hoverCartAfterClick();
            showAjaxLoading($('#js-clear-product-cart'), 'button');
            sParams = '&'+ getParam('sGlobalTokenName') + '[call]=shop.removeCart';
            $.ajaxCall({
                data: sParams,
                obj: 'remove_cart_product',
                dataType: 'json',
                type: "GET",
                callback: function(data){
                    removeCartCallback(data);
                }
            });
            $('.js-cancel-btn-popup').click();
        });
        return false;
    });
}
function removeCartCallback(data)
{
    closeAjaxLoading($('#js-clear-product-cart'));
    if (data.status == 'error') {
        showSmartAlert(data.message, 'Thông Báo', 'error', true);
        return false;
    }
    if (data.status == 'success') {
        reloadProductCart();
    }
}
function calcPriceRowProductPop(idRowProduct){
    if(typeof idRowProduct == 'undefined') {
        $('.is-row-product-pop').each(function(){
            idRowProduct = $(this).data('id');
            calcPricePop(idRowProduct);
        });
    }else{
        calcPricePop(idRowProduct);
    }

    totalPricePop();

}
function calcPricePop(idRowProduct){
    var rowProduct  = $('.is-row-product-pop[data-id="' + idRowProduct + '"]');
    rowProduct.find('.is-money').each(function(){
        var value = $(this).data('money');
        $(this).html(currencyFormat(Number(value)));
    });
}
function currencyFormat (num) {
    num *= 1;
    if (typeof(num) != 'number' || num == null || num == 'undefined' || num < 0 || isNaN(num)) {
        num = 0;
    };
    return num.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.")
}

function totalPricePop(){
    var sum = 0;
    var totalMoney = 0;
    $('.is-row-product-pop').each(function(){
        var isRowProductPop = $(this);
        sum = sum * 1 + isRowProductPop.find('.t83').val() * 1;
        totalMoney = totalMoney * 1 + isRowProductPop.find('.block-option-quantity .is-money').data('money');
    });
    $('.is-sum-product-pop').html(sum);
    $('.js-notify-cart').html('('+sum+')');
    $('.is-total-money-pop').html(currencyFormat(Number(totalMoney)));
    $('#div_gio_hang_so_luong.is-button-wait').removeClass('is-button-wait');
    $('#div_gio_hang_tong_tien.is-button-wait').removeClass('is-button-wait');
}
function changeQuanlityProductPop_old(){
    $('.is-row-product-pop').each(function(){
        var isRowProduct    = $(this);
        var idRow           = isRowProduct.data('id');
        var sku           = isRowProduct.find('.t84 span').data('sku');
        var quanlity        = isRowProduct.find('input.t83').val();
        var changeQuanlity  = isRowProduct.find('.is-change-quanlity-pop');
        var priceRowVal     = isRowProduct.find('.t8 .vl').data('money');
        /*var sumPrice        = isRowProduct.find('.t84 .vl');*/

        var data;
        var quanlityTimeOut, quanlityInterval, ajaxMoneyTimeout;
        var curChangeBtn;
        var valChange = 0;
        changeQuanlity.unbind('mousedown').mousedown(function(){
            showAjaxLoading($(this).parents('.is-row-product-pop').find('.t84 span'), 'button');
            if (!$('#div_gio_hang_tong_tien').hasClass('is-button-wait')) {
                showAjaxLoading($('#div_gio_hang_tong_tien'), 'button');
            }
            if (!$('#div_gio_hang_so_luong').hasClass('is-button-wait')) {
                showAjaxLoading($('#div_gio_hang_so_luong'), 'button');
            }
            changeQuanlity.unbind('mouseup').mouseup(function() {
                clearTimeout(quanlityTimeOut);
                clearTimeout(ajaxMoneyTimeout);
                clearInterval(quanlityInterval);
                ajaxMoneyTimeout = setTimeout(function(){
                    getMoneyAjax(idRow, sku, valChange);
                }, 600);
                return false;
            });
            changeQuanlity.unbind('mouseleave').mouseleave(function() {
                clearTimeout(quanlityTimeOut);
                clearInterval(quanlityInterval);
                return false;
            });

            clearTimeout(ajaxMoneyTimeout);
            curChangeBtn    = $(this);
            tmp = 1;
            if(curChangeBtn.hasClass('fa-minus')){
                tmp = -1;
            }
            
            quanlity = quanlity * 1 + tmp;
            if(quanlity <= 0){
                quanlity = 1;
                showSmartAlert('Số lượng tối thiếu là 1!', 'Thông Báo', 'error', true);
                return false;
            }
            valChange = valChange + tmp;

            isRowProduct.find('input').val(quanlity);
            /*sumPrice.data('money', quanlity * priceRowVal);*/
            isRowProduct.find('.is-money-ajax').attr('data-quantity',quanlity);
            isRowProduct.find('.is-money-ajax').addClass('wait');
            quanlityTimeOut = setTimeout(function(){
                quanlityInterval = setInterval(function(){
                    tmp = 1;
                    if(curChangeBtn.hasClass('fa-minus')){
                        tmp = -1;
                    }
                    
                    quanlity = quanlity * 1 + tmp;
                    if(quanlity <= 0){
                        quanlity = 1;
                        clearTimeout(quanlityTimeOut);
                        clearInterval(quanlityInterval);
                        showSmartAlert('Số lượng tối thiếu là 1!', 'Thông Báo', 'error', true);
                        return false
                    }
                    valChange = valChange + tmp;
                    isRowProduct.find('input').val(quanlity);
                    /*sumPrice.data('money', quanlity * priceRowVal);*/
                    isRowProduct.find('.is-money-ajax').attr('data-quantity',quanlity);
                    isRowProduct.find('.is-money-ajax').addClass('wait');
                }, 20);
            }, 500);
            
            $('#js_change_value').val(1);
            // call ajax to update cart
            return false;
        });
    });
}
function changeQuanlityProductPop(){
    /*  
        Các biến timeout chạy chung
        Dùng CheckSync để đồng bộ giữa việc thay đổi số lượng của các 
        */
    var quanlityTimeOut, quanlityInterval, ajaxMoneyTimeout;
    var checkSync = 0;
    $('.is-change-quanlity-pop').mousedown(function(){
        /*  Khởi tạo các biến cần thiết */
        var oChangeQuanlity = $(this);
        var oRowProduct = oChangeQuanlity.parents('.is-row-product-pop');
        var id = oRowProduct.attr('data-id');
        var sku = oRowProduct.find('.block-option-quantity .is-money').data('sku');
        var quantity = oRowProduct.find('input.t83').val() * 1;

        /*  Kiểm tra nếu có 1 timeout đang chờ lấy giá thì sẽ return*/
        if(checkSync != 0 && checkSync != id)
            return;
        /*  Mối khi click thì tất cả timeout đều clear để tính lại từ đầu */
        clearTimeout(ajaxMoneyTimeout);
        clearTimeout(quanlityTimeOut);
        clearInterval(quanlityInterval);
        
        /*  Số lượng thay đổi được lưu trong data-change
            Số lượng ban đầu được lưu trong data-curr 
            */
        if(oRowProduct.attr('data-change') == ' ' || oRowProduct.attr('data-change') == '' || typeof(oRowProduct.attr('data-change')) == 'undefined'){
            oRowProduct.attr('data-change', 0); 
        }
        if(oRowProduct.attr('data-curr') == ' ' || oRowProduct.attr('data-curr') == '' || typeof(oRowProduct.attr('data-curr')) == 'undefined'){
            oRowProduct.attr('data-curr', quantity); 
        }
        if(oRowProduct.attr('data-currquantity') == ' ' || oRowProduct.attr('data-currquantity') == '' || typeof(oRowProduct.attr('data-currquantity')) == 'undefined'){
            oRowProduct.attr('data-currquantity', quantity); 
        }
        if(oRowProduct.attr('data-currmoney') == ' ' || oRowProduct.attr('data-currmoney') == '' || typeof(oRowProduct.attr('data-currmoney')) == 'undefined'){
            oRowProduct.attr('data-currmoney', oRowProduct.find('.t84 .is-money').html()); 
        }
        if(oRowProduct.attr('data-allprd') == ' ' || oRowProduct.attr('data-allprd') == '' || typeof(oRowProduct.attr('data-allprd')) == 'undefined'){
            oRowProduct.attr('data-allprd', $('.is-sum-product-pop').html()); 
        }
        if(oRowProduct.attr('data-totalmoney') == ' ' || oRowProduct.attr('data-totalmoney') == '' || typeof(oRowProduct.attr('data-totalmoney')) == 'undefined'){
            oRowProduct.attr('data-totalmoney', $('.is-total-money-pop').html()); 
        }
        var tmp = 1;
        
        if(oChangeQuanlity.hasClass('down-button')){
            tmp = -1 * 1;
            
            if(oRowProduct.attr('data-curr') * 1 + oRowProduct.attr('data-change') * 1 <= 1){
                clearTimeout(quanlityTimeOut);
                clearInterval(quanlityInterval);
                oChangeQuanlity.unbind('mouseup mouseleave');

                checkSync = id;
                ajaxMoneyTimeout = setTimeout(function(){
                    returnTransactionPop(id);
                    getMoneyAjax(id, sku, oRowProduct.attr('data-change') * 1, 0);
                    oRowProduct.attr('data-change', ' ');
                    oRowProduct.attr('data-curr', ' ');
                    clearTimeout(ajaxMoneyTimeout);
                    checkSync = 0;
                }, 600);
                return;
            }
        }
        
        /*  Thêm các icon loading */
        showAjaxLoading(oRowProduct.find('.t84 .vl'), 'button');
        showAjaxLoading($('.is-sum-product-pop'), 'button');
        showAjaxLoading($('.is-total-money-pop'), 'button');

        /*  Cập nhật hiển thị số lượng */
        oRowProduct.attr('data-change', oRowProduct.attr('data-change') * 1 + tmp * 1);
        oRowProduct.find('input.t83').val(oRowProduct.attr('data-change') * 1 + oRowProduct.attr('data-curr') * 1);

        /*  Xử lý cho quá trình nhấn giữ */
        quanlityTimeOut = setTimeout(function(){
            quanlityInterval = setInterval(function(){
                tmp = 1;
                if(oChangeQuanlity.hasClass('down-button')){
                    tmp = -1;
                    if(oRowProduct.attr('data-curr') * 1 + oRowProduct.attr('data-change') * 1 <= 1){
                        clearTimeout(quanlityTimeOut);
                        clearInterval(quanlityInterval);
                        oChangeQuanlity.unbind('mouseup mouseleave');

                        checkSync = id;
                        ajaxMoneyTimeout = setTimeout(function(){
                            returnTransactionPop(id);
                            getMoneyAjax(id, sku, oRowProduct.attr('data-change') * 1, 1);
                            oRowProduct.attr('data-change', ' ');
                            oRowProduct.attr('data-curr', ' ');
                            clearTimeout(ajaxMoneyTimeout);
                            checkSync = 0;
                        }, 600);
                        return;
                    }
                }
                
                oRowProduct.attr('data-change', oRowProduct.attr('data-change') * 1 + tmp * 1);
                oRowProduct.find('input.t83').val(oRowProduct.attr('data-change') * 1 + oRowProduct.attr('data-curr') * 1);
            }, 20);
        }, 500);
        oChangeQuanlity.bind('mouseup mouseleave',function(){
            clearTimeout(quanlityTimeOut);
            clearInterval(quanlityInterval);
            oChangeQuanlity.unbind('mouseup mouseleave');

            checkSync = id;
            ajaxMoneyTimeout = setTimeout(function(){
                returnTransactionPop(id);
                getMoneyAjax(id, sku, oRowProduct.attr('data-change') * 1, 1);
                oRowProduct.attr('data-change', ' ');
                oRowProduct.attr('data-curr', ' ');
                clearTimeout(ajaxMoneyTimeout);
                checkSync = 0;
            }, 600);
        });
    });
}
/*  Hàm để khôi phục dữ liệu khi xảy ra lỗi trong quá trình lấy giá */
function returnTransactionPop(id){
    setTimeout(function(){
        var oRowProduct = $('.is-row-product-pop[data-id="' +id+'"]');
        if(!oRowProduct.find('.t84 .vl').hasClass('is-button-wait')){
            oRowProduct.attr('data-currquantity', ' ');
            oRowProduct.attr('data-currmoney', ' ');
            oRowProduct.attr('data-allprd', ' ');
            oRowProduct.attr('data-totalmoney', ' ');
            return false;
        }
        oRowProduct.find('input.t83').val(oRowProduct.attr('data-currquantity'));
        oRowProduct.find('.t84 .vl').html(oRowProduct.attr('data-currmoney'));
        oRowProduct.find('.t84 .vl').removeClass('is-button-wait');

        $('.is-sum-product-pop').html(oRowProduct.attr('data-allprd'));
        $('.is-total-money-pop').html(oRowProduct.attr('data-totalmoney'));
        $('.is-sum-product-pop, .is-total-money-pop').removeClass('is-button-wait');
    }, 2000);

}
function getMoneyAjax(id, sku, change, loadpage){
    if(typeof(loadpage) == 'undefined')
        loadpage = 0;
    var tmps = {
        san_pham: {
            id: [id],
            sku: [sku],
            so_luong: [change],
            name: [''],
            avatar: [''],
            shop: loadpage
        },
    };
    js_dat_mua(tmps);
    return false;
}
function removeProductPop()
{
    $('.is-row-product-pop').each(function(){
        var isRowProduct    = $(this);
        var idRow           = isRowProduct.data('id');
        var removeRow       = isRowProduct.find('.is-remove-product-pop');
        var cartProduct     = isRowProduct.parent('.t5a');
        var martProduct     = isRowProduct.parents('.t5d');
        removeRow.unbind('click').click(function(){
            // call ajax to delete product in cart
            var product_id = isRowProduct.attr('data-id');
            var product_vendor = isRowProduct.attr('data-vendor');
            var product_sku = isRowProduct.attr('data-sku');
            $('.is-shop-header').addClass('show');
            content = 'Quý khách có chắc muốn bỏ sản phẩm này ra khỏi giỏ hàng?';
            showSmartAlert(content, 'Thông Báo', 'message', 1, 1);
            $('.js-agree-btn-popup').unbind('click').click(function(){
                deleteProductInCart(product_sku);
                $('.is-shop-header').addClass('show');
                isRowProduct.fadeOut('500', function() {
                    isRowProduct.remove();
                    if(cartProduct.find('.is-row-product-pop').length <= 0){
                        cartProduct.fadeOut('500', function() {
                            cartProduct.remove();
                            if(martProduct.find('.t5a').length <= 0){
                                martProduct.fadeOut('500', function() {
                                    martProduct.remove();
                                });
                            }
                        });
                    }
                });
                $('.js-cancel-btn-popup').click();
            });
        });
    });
}

//Local storage
function checkLocalStorage(dataKey){
    var data = localStorage.getItem(dataKey);
    if(data === null) {
        return false;
    }
    return true;
}
function getLocalStorage(dataKey){
    return JSON.parse(localStorage.getItem(dataKey));
}
function setLocalStorage(dataKey, dataValue){
    localStorage.setItem(dataKey, JSON.stringify(dataValue));
}


function initLoadSaveCart()
{
    $('#js-continue-save-cart').click(function(){
        $('.error_popup').hide();
        showAjaxLoading($('#js-continue-save-cart'), 'button');
        name = $('#js-name-cart').val();
        data = '&val[name]='+name;
        
        $.ajax({
            crossDomain:true,
            xhrFields: {
                withCredentials: true
            },
            url: setupPathCode(setupPath('www') + '/includes/ajax.php?=&core[call]=shop.saveCurrentCart'),
            type:'POST',
            timeout: 10000,
            cache:false,
            data: data,
            dataType: 'json',
            beforeSend: function ( xhr ) {
                
            },
            error: function(jqXHR, status, errorThrown){
                
                if(status == 'timeout')
                {
                    showSmartAlert('Connect server fail.Please try again later.', 'Thông Báo', 'error', true);
                }
            } ,
            success: function (data) {
                //check return;
                if (data.status == 'success') {
                    //Thay đổi trang thái của button lưu giỏ hàng (hoặc bỏ button đăng lên giỏ hàng tại chi tiết giỏ hàng)
                    setStatusSaveCartOFF(data.id, 0);
                    showSmartAlert('Lưu giỏ hàng thành công!', 'Thông Báo', 'success', true);
                }
                else if (data.status == 'error'){
                    showSmartAlert(data.message, 'Thông Báo', 'error', true);
                }
                $('.popup-close').click();
            }
        });
    });
    
    $('#js-skip-save-cart').click(function(){
        $('#js-name-cart').val('');
        $('#js-continue-save-cart').click();
    });
}

//Thiết lập những giá trị và trạng thái cần thiết khi giở hàng đang cho phép lưu
function setStatusSaveCartON()
{
    localStorage.setItem('save_cart', 0);
    localStorage.setItem('re_buy_cart', 0);
    
    $('#js-save-cart').find('.js-icon').removeClass('fa-check');
    $('#js-save-cart').find('.js-icon').addClass('fa-download');
    $('#js-save-cart').find('.js-btn-save').html('Lưu giỏ');
    $('#js-save-cart').addClass('atv');
    $('#js-post-cart').addClass('atv');
    
}

//Thiết lập những giá trị và trạng thái cần thiết khi giở hàng đang không cho phép lưu
function setStatusSaveCartOFF(cart_id, re_buy)
{
    localStorage.setItem('save_cart', cart_id);
    localStorage.setItem('re_buy_cart', re_buy);
    $('#js-save-cart').find('.js-icon').removeClass('fa-download');
    $('#js-save-cart').find('.js-icon').addClass('fa-check');
    $('#js-save-cart').find('.js-btn-save').html('Giỏ đã lưu');
    $('#js-save-cart').removeClass('atv');
    
    $('#js-post-cart').removeClass('atv');
}

/* end save cat */

function redirectSearchPage()
{
    $('#js-btn-search-global').unbind('click').click(function(){
        $('#global-search').toggleClass('show');
        $('body').toggleClass('hidden');
        //tương tác
        $('#js-btn-cancel-search-gb').unbind('click').click(function(){
            $('.js-input-search').val('');
            $('#global-search').toggleClass('show');
            $('body').toggleClass('hidden');
        });
        $('#js-btn-del-gb').unbind('click').click(function(){
            $('.js-input-search').val('');
        });
        
        $('.m-search-page-body').unbind('click').click(function(){
            $('#js-btn-cancel-search-gb').click();
        });
        
        $('#js-btn-search-gb').unbind('click').click(function(){
            var keyword = $('.js-input-search').val();
            if (keyword.length < 2) {
                showSmartAlert('Tù khóa cần tìm phải có ít nhất 2 ký tự', 'Thông Báo', 'error', true);
                return false;
            }
            $('#js-frm-search-global').submit();
        });
        
        return;
        //window.location = '/search/';
    });
}

/*  hàm xử lý Rating */
function ratingProcess(){
    if($('.is-rating').length <= 0){
        return;
    }
    $('.is-rating').each(function(){
        var isRating    = $(this);
        var star        = isRating.find('span');
        /*  curValue cho biết ngôi sao đang được active */
        var curValue    = isRating.attr('data-value');
        var inputVale   = isRating.parent().find('.js-value-rating');
        if (!(curValue >= 1 && curValue <= 5)) {
            curValue = 0;
        };
        
        /*  Khi rê chuột vào thì sẽ lấy giá trị curValue ra
            Đồng thời removeClass atv, để ngôi sao có thể đổi màu khi rê chuột 
            Sau khi rê chuột ra thì add CLass atv vào lại, nhờ vào curValue
            */
        isRating.hover(function(){
            /*curValue = isRating.find('.atv').index();
            if (curValue>=0 && curValue < 5) {
                star.removeClass('atv');
            };*/
        }, function(){
            /*star.removeClass('atv');
            if (curValue>=0 && curValue < 5) {
                star.eq(curValue).addClass('atv');
            };*/
        });
        /*  Khi click ngôi sao thì thiết lập lại giá trị cho curValue 
            Nếu curValue ==  index của ngôi sao click thì curValue = -1
            */
        star.click(function(){
            var newValue = 5 - $(this).index() * 1;

            if(newValue == curValue){
                curValue = 0;
                isRating.attr('data-value', 0);
                inputVale.val(0);
            }else{
                curValue = newValue;
                isRating.attr('data-value', newValue);
                inputVale.val(newValue);
            }
        });
    });
}
/* End GODBAL */

//Thiết lập những giá trị và trạng thái cần thiết khi giở hàng đang cho phép lưu
function setStatusSaveCartON()
{
    localStorage.setItem('save_cart', 0);
    localStorage.setItem('re_buy_cart', 0);
    
    $('#js-save-cart').find('.js-icon').removeClass('fa-check');
    $('#js-save-cart').find('.js-icon').addClass('fa-download');
    $('#js-save-cart').find('.js-btn-save').html('Lưu giỏ');
    $('#js-save-cart').addClass('atv');
    
    $('#js-post-cart').addClass('atv');
    
}

//Thiết lập những giá trị và trạng thái cần thiết khi giở hàng đang không cho phép lưu
function setStatusSaveCartOFF(cart_id, re_buy)
{
    localStorage.setItem('save_cart', cart_id);
    localStorage.setItem('re_buy_cart', re_buy);
    $('#js-save-cart').find('.js-icon').removeClass('fa-download');
    $('#js-save-cart').find('.js-icon').addClass('fa-check');
    $('#js-save-cart').find('.js-btn-save').html('Giỏ đã lưu');
    $('#js-save-cart').removeClass('atv');
    
    $('#js-post-cart').removeClass('atv');
}
//hàm lưu giỏ hàng
function saveCurrentCart()
{
    $('.cart-saving-button').unbind('click').click(function(){
        //check exist product
        if ($('.is-row-product-pop').length > 0) {
            var html = '';
            //showSmartAlert('Email không chính xác, vui lòng nhập lại', 'tiêu đề', 'error');
        }
        else {
            showSmartAlert('Giỏ hàng chưa có sàn phẩm nào!', '', 'error');
        }
    });
}

function initExpandContent()
{
    $('.is-expand-btn').unbind('click').click(function(){
        var hasRotate = $(this).hasClass('rotate');
        var objType = $(this).attr('data-obj');
        if (empty(objType)) {
            return false;
        }
        $(this).toggleClass('rotate');
        $('.is-expand-content[data-obj="'+ objType +'"]').toggleClass('hide');
    });
}

function showQuickBuyWithoutLogin()
{
    var html = '<section id="l-login-panel" class="container m-quick-buy">' +
                    '<div class="login-container">' +
                        '<a href="/">' +
                        '<div class="main-logo" alt="disieuthi.vn" style="background: transparent url(\'//img.'+oParams['sJsHostname'] +'/styles/mobile/4038/image/logo.png\') no-repeat scroll center center / 100% auto; background-size: auto 100%;"></div>' +
                        '</a>' +
                        '<div class="phone-group">' +
                            '<div class="content_err error_popup"></div>' +
                            '<label class="label-qb-1">Mua nhanh</label>' +
                            '<input id="js-full-name" type="text" class="login-input" placeholder="Họ và tên"/>' +
                            '<input id="js-dia-chi" type="text" class="login-input" placeholder="Email"/>' +
                            '<input id="js-phone_number" type="text" class="login-input" placeholder="Số điện thoại"/>' +
                            '<a id="js-qlg-popup" class="send-require-button button" href="javascript:void(0)">Mua nhanh</a>' +
                            '<label class="label-qb-2">Bạn đã có tài khoản?</label>' +
                            '<a href="/dang_nhap.html" class="button register-button">Đăng nhập</a>' +
                        '</div>' +
                    '</div>' +
                '</section>';
    $('header.container').hide();
    $('footer.container').hide();
    $(html).insertAfter('#l-login-panel');
    $('.body').toggleClass('hide');
    $('.m-quick-buy').toggleClass('show');
    initPopupLogin();
}
$(function(){
    var cart = $('.js-notify-cart').html();
    if(cart >= 1){
        $('.cart-notify-mobile').css('display','block');
    }
});
function initSelectLocation()
{
    var locationId = getCookie('area')*1;
    if (locationId > 0) {
        /*Đã chọn location => Xử lý*/
    }
    else {
        showSelectLocation(0);
    }
    $('.js-change-location').unbind('click').click(function(){
        $('.js-toggle-menu').click();
        showSelectLocation(locationId);
    });
}
function showSelectLocation(locationId){
    if (typeof (locationId) == 'undefined') {
        locationId = 0;
    }
    // load popup content.
    if ($('#notify-popup').length == 0) {
        html = '<div id="notify-popup"></div>';
        $('body').append(html);
    }
    else {
        // reset popup content
        $('#notify-popup').html('');
    }
    // load popup content.
    html = '<div class="ngdialog-overlay"></div><div class="notify is-log-box">';
    var oCity = {};
    oCity['723'] = {
        'id': 723,
        'name': 'Hà Nội',
    };
    oCity['753'] = {
        'id': 753,
        'name': 'TP. Hồ Chí Minh',
    };
    var class_atv_hn = '';
    var class_atv_hcm = '';
    html += '<div class="ntf-header">';
    html += '<div class="ntf-title">Chọn địa điểm nhận hàng</div>';
    html += '<div class="ntf-sub-title">Disieuthi.vn chỉ cung cấp dịch vụ tại nội thành Hà Nội và TP. Hồ Chí Minh</div>';
    html += '</div>';
    html += '<div class="ntf-content">';
    html += '<div class="ntf-gr-btn">';
    for (i in oCity) {
        var class_atv = '';
        var class_check = 'fa-circle-o';
        if (locationId > 0) {
            if (oCity[i].id == locationId) {
                class_atv = 'atv';
                class_check = 'fa-check-circle';
            } 
        }
        html += '<div class="ntf-gr-btn-log js-is-location '+ class_atv +'" data-id="'+oCity[i].id+'"><span class="fa is-radio-button '+ class_check +' bg-icon '+ class_atv +'" ></span><span class="log-txt is-radio-label">'+ oCity[i].name +'</span></div>';
    }
    //html += '<div class="ntf-gr-btn-log">Hà nội</div>';
    //html += '<div class="ntf-gr-btn-log">Tp. HCM</div>';
    html += '';
    html += '';
    html += '</div>';
    html += '<div class="ntf-note">*Quý khách cần hỗ trợ thêm vui lòng gọi 1900 2075';
    html += '</div>';
    html += '';
    html += '</div>';
    
    html += '</div>';
    $('#notify-popup').html(html);
    
    $('.js-is-location').unbind('click').click(function(){
        var id_select = $(this).attr('data-id')*1;
        if (id_select > 0) {
            $('#notify-popup').remove();
            var bIsChange = false;
            if (locationId != id_select) {
                bIsChange = true;
                if (locationId > 0) {
                    /* Kiem tra gio hang co chua san pham hay không */
                    if(checkLocalStorage('shopcart') == true){
                        bIsChange = false;
                        content = 'Giỏ hàng của quý khách sẽ bị xóa khi quý khách chuyển thành phố';
                        showSmartAlert(content, 'Thông Báo', 'message', 1, 1);
                        $('.js-agree-btn-popup').unbind('click').click(function(){
                            showAjaxLoading($(this), 'button');
                            sParams = '&'+ getParam('sGlobalTokenName') + '[call]=shop.removeCart';
                            $.ajaxCall({
                                data: sParams,
                                obj: 'remove_cart_product',
                                dataType: 'json',
                                type: "GET",
                                callback: function(data){
                                    if (data.status == 'error') {
                                        content = '<div class="row-poup center">'+data.message+'</div>';
                                        showSmartAlert(data.message, 'Lỗi thao tác', 'error');
                                        return false;
                                    }
                                    if (data.status == 'success') {
                                        setCookie('area', id_select, 30);
                                        reloadProductCart();
                                        /* re-direct home page */
                                        window.location = '/' ;
                                    }
                                }
                            });
                        });
                        $('.js-cancel-btn-popup').unbind('click').click(function(){
                            $('#notify-popup').remove();
                        });
                    }
                }
            }
            
            if (bIsChange) {
                setCookie('area', id_select, 30);
                window.location.reload(); 
            }
            
        }
    });
}
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    var expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires + ";domain=."+oParams['sJsHostname']+";path=/";;
}
