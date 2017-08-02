$(document).ready(function () {
    homevideo();
    bannerform.imgMargin();
    commentHome.userclick();
    link.copy();
    uploadbutton.select();

    menu.hover();
    supporthuman.action();

    FillterForm.action();
});


homevideo = function () {
    $(".videos-thumb li").click(function () {
        var vid = $(this).attr("vid");
        var player = $(this).parents(".row").find("iframe");
        player.attr("src", "https://www.youtube.com/embed/" + vid + "?autoplay=1");
        $(".videos-thumb li").removeClass("selected");
        $(this).addClass("selected");
    });
}

bannerform = {
    imgMargin: function () {
        var img = $(".banner img");
        var area = img.parents(".img");
        img.css("margin-top", (area.height() - img.height())/2 );
    }
};

commentHome = {
    userclick: function () {
        $(".usercomment").click(function () {
            
            var ContentRow = $(this).parents(".row").next(".row");
            $(this).parents(".row").find(".usercomment").removeClass("actived");
            $(this).addClass("actived");
            ContentRow.find(".contentcomment").removeClass("show").addClass("hidden");
            jQuery("[comment=" + $(this).attr("taget")+"]", ContentRow).removeClass("hidden").addClass("show");

        });
    }
}

link = {
    copy: function () {
        jQuery("a.link_href_copy").click(function () {
            var $temp = $("<input>");
            $("body").append($temp);
            $temp.val(jQuery(this).prev("input[type=text]").val()).select();
            document.execCommand("copy");
            $temp.remove();
        });
    }
}

uploadbutton = {
    select: function () {
        jQuery("a.upload-button").click(function () {
            jQuery(this).parents(".form-group").find("input[type=file]").trigger('click');
        });
    }
}

var menu = {
    hover: function () {

    }
}

var supporthuman = {
    action: function () {
        jQuery("#side-overlay button.pull-right").click(function () {

            $("#side-overlay").css("right", 0);
        });
        jQuery("a.viewsupport").click(function () {

            $("#side-overlay").css("right", 310);
        });
        
    }
}

var FillterForm = {
    action: function () {
        $("form[name=fillter-form] select").change(function () {
            $(this).parents("form[name=fillter-form]").submit();
        });
        $("form[name=fillter-form] .submit").click(function () {
            $(this).parents("form[name=fillter-form]").submit();
        });
        
    }
};