$(document).ready(function () {
    homevideo();
    bannerform.imgMargin();
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