/*
 * ------------------------------------------------------------------------
 * Copyright (C) 2009 - 2013 The YouTech JSC. All Rights Reserved.
 * @license - GNU/GPL, http://www.gnu.org/licenses/gpl.html
 * Author: The YouTech JSC
 * Websites: http://www.smartaddons.com - http://www.cmsportal.net
 * ------------------------------------------------------------------------
*/
/* Accordion Block */
jQuery(document).ready(function($) {
	$("ul.yt-accordion li").each(function() {
		if($(this).index() > 0) {
			$(this).children(".accordion-inner").css('display', 'none');
		}
		else {
			$(this).find(".accordion-heading").addClass('active');
		}
	
		$(this).children(".accordion-heading").bind("click", function() {
			$(this).addClass(function() {
				if($(this).hasClass("active")) return "";
				return "active";
			});
	
			$(this).siblings(".accordion-inner").slideDown(350);
			$(this).parent().siblings("li").children(".accordion-inner").slideUp(350);
			$(this).parent().siblings("li").find(".active").removeClass("active");
		});
	});
	
	/* Tooltip Block */
	
	$('a.boxtip').tooltip({placement:'top'});
	
	$("[data-placement='top']").tooltip({placement:'top'});
	$("[data-placement='bottom']").tooltip({placement:'bottom'});
	$("[data-placement='left']").tooltip({placement:'left'});
	$("[data-placement='right']").tooltip({placement:'right'});
	$("[data-placement]").hover(function(){
		$(this).css({"display":"inline-block"}); 
	});
	
	/* Quick show */
	var $modal= $( ".yt-modal" ).clone();
	$(".yt-modal").remove(); 
	$modal.appendTo( "body" );
	
});

/* Slider Carousel */
jQuery(document).ready(function($) {
	$(".yt-slider-carousel .item").each(function() {
		if($(this).index() == 0) {
			$(this).addClass('active');
		}
		
	});
	if (typeof jQuery != 'undefined') { 
		(function($) { 
			$(document).ready(function(){
				$('#yt-slider-carousel').each(function(index, element) {
					$(this)[index].slide = null;
				});
				$('#yt-slider-carousel1').each(function(index, element) {
					$(this)[index].slide = null;
				});
				$('#yt-slider-carousel2').each(function(index, element) {
					$(this)[index].slide = null;
				});
			});
		})(jQuery);
	}
});

	
/* Message box */
function closeMessage(el) {
	$(el).parent().parent().parent().animate({opacity: 0}, 350, function() {
		$(el).parent().parent().parent().hide();
	});
}

/* Gallery Block */
jQuery(document).ready(function($) {
	$(".gallery-item img").hover(function(){
		$(this).animate({ opacity: 0.55 }, 150);
	}, function(){
		$(this).animate({ opacity: 1 }, 150);
	});
});




/* Tabs Block */
jQuery(document).ready(function($) {
	var tabs = $('ul.nav-tabs');
	tabs.each(function(i) {
		var tab = $(this).find('> li > a');
		tab.click(function(e) {
			var contentLocation = $(this).attr('href');

			if(contentLocation.charAt(0)=="#") {
				e.preventDefault();
				tab.removeClass('active');
				$(this).addClass('active');
				$(contentLocation).show().addClass('active').siblings().hide().removeClass('active');
			}
		});
	});
});


/* Toggle Block */
jQuery(document).ready(function($) {
	$("ul.yt-toggle-box li").each(function() {
		$(this).children(".toggle-box-content").not(".active").css('display', 'none');
		
		$(this).children(".toggle-box-head").bind("click", function(){
			$(this).addClass(function(){
				if($(this).hasClass("active")){
					$(this).removeClass("active");
					return "";
				}
				return "active";
			});
			$(this).siblings(".toggle-box-content").slideToggle();
		});
		
		
	});
	
	
	
});


/* Divider Block */
jQuery(document).ready(function($) {
	$("a.scroll-top").click(function () {
		$("body,html").animate({
			scrollTop: 0
		}, 800);
	});
});
