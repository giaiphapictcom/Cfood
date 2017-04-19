var init_active_mn; var hover_mn = false; var time_delay_mn = 200; var time_delay_set_active = 1000;

$(document).ready(function () {

    init_active_mn = self.setInterval("set_mn_active()", 500);
    $("#mainMenuTop .root li").live("mouseover", function (e) {
        var count_li = $("#mainMenuTop").find("[name='parent']").length;
        var current = $("#mainMenuTop").find("[name='parent']").index($(this));
        if (current > 0) {
            var pre_index = current - 1;
            $("#mainMenuTop").find("[name='parent']:eq(" + pre_index + ")").find("span.space").attr("class", "space_hover");
        }
        if (current < count_li - 1) {
            var next_index = current;
            $("#mainMenuTop").find("[name='parent']:eq(" + next_index + ")").find("span.space").attr("class", "space_hover");
        }
        clearInterval(init_active_mn);
        hover_mn = true;
        //$(this).parent().find("li").removeAttr("class");
        if ($(this).find("ul").length > 0) {
            $(this).find("ul:first").show();
            /*var postleft = $(this).parent().offset().left;
            var m_width = $(this).find("ul").width()/2;
            var p_left = $(this).offset().left - $(this).parent().offset().left;
            if(p_left+m_width<=1000){
                p_left = p_left - m_width;
                if(p_left<=0){p_left = 0}
                $(this).find("ul").css("left",p_left+"px");
            }
            else{
                $(this).find("ul").css("right","0px");
            }*/
        }
        //alert($(this).parent().parent().get(0).tagName);
        if ($(this).parent().parent().get(0).tagName == "li") {
            $(this).parent().parent().attr("class", "active");
            alert($(this).parent().parent().html());
        }
    });
    $("#mainMenuTop .root li").live("mouseout", function (e) {
        var count_li = $("#mainMenuTop").find("[name='parent']").length;
        var current = $("#mainMenuTop").find("[name='parent']").index($(this));
        if (current > 0) {
            var pre_index = current - 1;
            $("#mainMenuTop").find("[name='parent']:eq(" + pre_index + ")").find("span.space").attr("class", "space");
        }
        if (current < count_li - 1) {
            var next_index = current;
            $("#mainMenuTop").find("[name='parent']:eq(" + next_index + ")").find("span.space").attr("class", "space");
            //$(this).find("span:last").attr("class","space");
        }
        if ($(this).find("ul").length > 0) {
            $(this).find("ul").hide();
        }
        init_active_mn = self.setInterval("set_mn_active()", time_delay_set_active);
        hover_mn = false;
    });
    $("#mainMenuTop .root li ul").live("mouseover", function (e) {
        if ($(this).parent().get(0).tagName == "LI") {
            $(this).parent().attr("class", "active");
        }
    });
    $("#mainMenuTop .root li ul").live("mouseout", function (e) {
        if ($(this).parent().get(0).tagName == "LI") {
            $(this).parent().removeAttr("class");
        }
    });
});
function set_mn_active() {
    if (hover_mn == false) {
        $("#mainMenuTop .root li").removeAttr("class");
        $("#mainMenuTop .root li").each(function (index) {
            if ($(this).attr("idata") == current_mn_active) {
                var current = $("#mainMenuTop").find("[name='parent']").index($(this));
                if (current > 0) {
                    var pre_index = current - 1;
                    $("#mainMenuTop").find("[name='parent']:eq(" + pre_index + ")").find("span.space").attr("class", "space_hover");
                }
                $(this).attr("class", "active");
                /*if($(this).find("ul").length>0){
					$(this).find("ul").show();
					var postleft = $(this).parent().offset().left;
					var m_width = $(this).find("ul").width()/2;
					var p_left = $(this).offset().left - $(this).parent().offset().left;
					if(p_left+m_width<=1000){
						p_left = p_left - m_width;
						if(p_left<=0){p_left = 0}
						$(this).find("ul").css("left",p_left+"px");
					}
					else{
						$(this).find("ul").css("right","0px");
					}
				}*/
            }
        });
    }
    clearInterval(init_active_mn);
}