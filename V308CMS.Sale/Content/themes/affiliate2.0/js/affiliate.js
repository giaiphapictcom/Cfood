$(document).ready(function () {
    homevideo();
    bannerform.imgMargin();
    commentHome.userclick();
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