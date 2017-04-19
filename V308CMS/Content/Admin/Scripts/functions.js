// Side Navigation Menu Slide
$(document).ready(function () {
   
    var aObj = document.getElementById('nav').getElementsByTagName('a');
    var position = 0;
    //------------------------------------------
    var count;
    //-----------------------------------------
    var objLi = null
    var objA1 = null;
    var objL1 = null;
    var child = null;


    if (true) {

    }
    //-- Menu san pham
    if (document.getElementById('ul1') != null) {
        objA1 = document.getElementById('ul1').getElementsByTagName('a');
        objL1 = document.getElementById('ul1');
        child = document.getElementById('ul1').getElementsByTagName('li');
        objLi = document.getElementById('1');
    }
    //-- Menu tin tuc
    var objLi2 = null;
    var objA2 = null;
    var child2 = null;
    if (document.getElementById('ul2') != null) {
        objLi2 = document.getElementById('2');
        objA2 = document.getElementById('ul2').getElementsByTagName('a');
        child2 = document.getElementById('ul2').getElementsByTagName('li');
    }

    // Menu tim kiem
    var objLi3 = null;
    var objA3 = null;
    var child3 = null;
    if (document.getElementById('ul3') != null) {
        objLi3 = document.getElementById('3');
        objA3 = document.getElementById('ul3').getElementsByTagName('a');
        child3 = document.getElementById('ul3').getElementsByTagName('li');
    }
    // Menu khach hang
    var objLi4 = null;
    var objA4 = null;
    var objchild4 = null;
    if (document.getElementById('ul4') != null) {
        objLi4 = document.getElementById('4');
        objA4 = document.getElementById('ul4').getElementsByTagName('a');
        child4 = document.getElementById('ul4').getElementsByTagName('li');
    }
    // Menu hinh anh
    var objLi5 = null;
    var objA5 = null;
    var child5 = null;
    if (document.getElementById('ul5') != null) {
        objLi5 = document.getElementById('5');
        objA5 = document.getElementById('ul5').getElementsByTagName('a');
        child5 = document.getElementById('ul5').getElementsByTagName('li');
    }
    // Menu file upload
    var objLi6 = null;
    var objA6 = null;
    var child6 = null;
    if (document.getElementById('ul6') != null) {
        objLi6 = document.getElementById('6');
        objA6 = document.getElementById('ul6').getElementsByTagName('a');
        child6 = document.getElementById('ul6').getElementsByTagName('li');
    }
    // menu tai khoan
    var objLi7 = null;
    var objA7 = null;
    var child7 = null;
    if (document.getElementById('ul7') != null) {
        objLi7 = document.getElementById('7');
        objA7 = document.getElementById('ul7').getElementsByTagName('a');
        child7 = document.getElementById('ul7').getElementsByTagName('li');
    }
    // menu he thong
    var objLi8 = null;
    var objA8 = null;
    var child8 = null;
    if (document.getElementById('ul8') != null) {
        objLi8 = document.getElementById('8');
        objA8 = document.getElementById('ul8').getElementsByTagName('a');
        child8 = document.getElementById('ul8').getElementsByTagName('li');
    }
    // menu thung rac
    var objLi9 = null;
    var objA9 = null;
    var child9 = null;
    if (document.getElementById('ul9') != null) {
        objLi9 = document.getElementById('9');
        objA9 = document.getElementById('ul9').getElementsByTagName('a');
        child9 = document.getElementById('ul9').getElementsByTagName('li');
    }
    var objLi10 = null;
    var objA10 = null;
    var child10 = null;
    if (document.getElementById('ul10') != null) {
        objLi10 = document.getElementById('10');
        objA10 = document.getElementById('ul10').getElementsByTagName('a');
        child10 = document.getElementById('ul10').getElementsByTagName('li');
    }
    var objLi11 = null;
    var objA11 = null;
    var child11 = null;
    if (document.getElementById('ul11') != null) {
        objLi11 = document.getElementById('11');
        objA11 = document.getElementById('ul11').getElementsByTagName('a');
        child11 = document.getElementById('ul11').getElementsByTagName('li');
    }
    var objLi12 = null;
    var objA12 = null;
    var child12 = null;
    if (document.getElementById('ul12') != null) {
        objLi12 = document.getElementById('12');
        objA12 = document.getElementById('ul12').getElementsByTagName('a');
        child12 = document.getElementById('ul12').getElementsByTagName('li');
    }
    var objLi13 = null;
    var objA13 = null;
    var child13 = null;
    if (document.getElementById('ul13') != null) {
        objLi13 = document.getElementById('13');
        objA13 = document.getElementById('ul13').getElementsByTagName('a');
        child13 = document.getElementById('ul13').getElementsByTagName('li');
    }


    var sPath = window.location.pathname;
    //var sPage = sPath.substring(sPath.lastIndexOf('\\') + 1);
    var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
    //alert(sPage);
    for (j = 0; j < aObj.length; j++) {
        if (objA1 != null) {

            for (i = 0; i < objA1.length; i++) {
                // id 1
                if (objA1[i].href == document.location.href || sPage == "ProductDetail.aspx" || sPage == "StatusProduct.aspx" || sPage == "ImageProduct.aspx" || sPage == "ProductSearch.aspx" || sPage == "ProductAttribute.aspx" ) {
                    position = 1;
                    count = i;
                    if (objA1[i].href == document.location.href) {
                        objA1[i].className = "hello";
                        child[i].innerHTML = objA1[i].innerHTML;
                        child[i].className = "heading selected";
                        $('.hello').remove();
                    }
                   
                }
            }
        }


        if (objA2 != null) {
            for (i = 0; i < objA2.length; i++) {
                // id 2
                if (objA2[i].href == document.location.href) {
                    position = 2;
                    objA2[i].className = "hello";
                    child2[i].innerHTML = objA2[i].innerHTML;
                    child2[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA3 != null) {
            for (i = 0; i < objA3.length; i++) {
                // id 3
                if (objA3[i].href == document.location.href) {
                    position = 3;
                    objA3[i].className = "hello";
                    child3[i].innerHTML = objA3[i].innerHTML;
                    child3[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA4 != null) {
            for (i = 0; i < objA4.length; i++) {
                // id 4
                if (objA4[i].href == document.location.href) {
                    position = 4;
                    objA4[i].className = "hello";
                    child4[i].innerHTML = objA4[i].innerHTML;
                    child4[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }

        if (objA5 != null) {
            for (i = 0; i < objA5.length; i++) {
                // id 5
                if (objA5[i].href == document.location.href) {
                    position = 5;
                    objA5[i].className = "hello";
                    child5[i].innerHTML = objA5[i].innerHTML;
                    child5[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }

        if (objA6 != null) {
            for (i = 0; i < objA6.length; i++) {
                // id 6
                if (objA6[i].href == document.location.href) {
                    position = 6;
                    objA6[i].className = "hello";
                    child6[i].innerHTML = objA6[i].innerHTML;
                    child6[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA7 != null) {
            for (i = 0; i < objA7.length; i++) {
                // id 7
                if (objA7[i].href == document.location.href) {
                    position = 7;
                    objA7[i].className = "hello";
                    child7[i].innerHTML = objA7[i].innerHTML;
                    child7[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA8 != null) {
            for (i = 0; i < objA8.length; i++) {
                // id 8
                if (objA8[i].href == document.location.href) {
                    position = 8;
                    objA8[i].className = "hello";
                    child8[i].innerHTML = objA8[i].innerHTML;
                    child8[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA9 != null) {
            for (i = 0; i < objA9.length; i++) {
                // id 8
                if (objA9[i].href == document.location.href) {
                    position = 9;
                    objA9[i].className = "hello";
                    child9[i].innerHTML = objA9[i].innerHTML;
                    child9[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }

        if (objA10 != null) {
            for (i = 0; i < objA10.length; i++) {
                // id 8
                if (objA10[i].href == document.location.href) {
                    position = 10;
                    objA10[i].className = "hello";
                    child10[i].innerHTML = objA10[i].innerHTML;
                    child10[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }

        if (objA11 != null) {
            for (i = 0; i < objA11.length; i++) {
                // id 8
                if (objA11[i].href == document.location.href) {
                    position = 11;
                    objA11[i].className = "hello";
                    child11[i].innerHTML = objA11[i].innerHTML;
                    child11[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA12 != null) {
            for (i = 0; i < objA12.length; i++) {
                // id 8
                if (objA12[i].href == document.location.href) {
                    position = 12;
                    objA12[i].className = "hello";
                    child12[i].innerHTML = objA12[i].innerHTML;
                    child12[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (objA13 != null) {
            for (i = 0; i < objA13.length; i++) {
                // id 8
                if (objA13[i].href == document.location.href) {
                    position = 13;
                    objA13[i].className = "hello";
                    child13[i].innerHTML = objA13[i].innerHTML;
                    child13[i].className = "heading selected";
                    $('.hello').remove();
                }
            }
        }
        if (position == 1) {
            objLi.className = 'expanded heading';

        }
        if (position == 2) {
            objLi2.className = 'expanded heading';

        }
        if (position == 3) {
            objLi3.className = 'expanded heading';

        }
        if (position == 4) {
            objLi4.className = 'expanded heading';

        }
        if (position == 5) {
            objLi5.className = 'expanded heading';

        }
        if (position == 6) {
            objLi6.className = 'expanded heading';

        }
        if (position == 7) {
            objLi7.className = 'expanded heading';

        }
        if (position == 8) {
            objLi8.className = 'expanded heading';

        }
        if (position == 9) {
            objLi9.className = 'expanded heading';

        }
        if (position == 10) {
            objLi10.className = 'expanded heading';

        }
        if (position == 11) {
            objLi11.className = 'expanded heading';

        }
        if (position == 12) {
            objLi12.className = 'expanded heading';

        }
        if (position == 13) {
            objLi13.className = 'expanded heading';

        }

        //        else  {
        //            objLi.className = 'heading collapsed';
        //        }
    }
});




$(document).ready(function() {
	$("#nav > li > a.collapsed + ul").slideToggle("medium");
	$("#nav > li > a").click(function() {
		$(this).toggleClass("expanded").toggleClass("collapsed").find("+ ul").slideToggle("medium");
	});
});

/************************************************************************
 * @name: bPopup
 * @author: Bjoern Klinggaard (http://dinbror.dk/bpopup)
 * @version: 0.4.1.min
 ************************************************************************/ 
(function(a){a.fn.bPopup=function(f,j){function s(){var b=a("input[type=text]",c).length!=0,k=o.vStart!=null?o.vStart:d.scrollTop()+g;c.css({left:d.scrollLeft()+h,position:"absolute",top:k,"z-index":o.zIndex}).appendTo(o.appendTo).hide(function(){b&&c.each(function(){c.find("input[type=text]").val("")});if(o.loadUrl!=null){o.contentContainer=o.contentContainer==null?c:a(o.contentContainer);switch(o.content){case "ajax":o.contentContainer.load(o.loadUrl);break;case "iframe":a('<iframe width="100%" height="100%"></iframe>').attr("src",
o.loadUrl).appendTo(o.contentContainer);break;case "xlink":a("a#bContinue").attr({href:o.loadUrl});a("a#bContinue .btnLink").text(a("a.xlink").attr("title"))}}}).fadeIn(o.fadeSpeed,function(){b&&c.find("input[type=text]:first").focus();a.isFunction(j)&&j()});t()}function i(){o.modal&&a("#bModal").fadeOut(o.fadeSpeed,function(){a("#bModal").remove()});c.fadeOut(o.fadeSpeed,function(){o.loadUrl!=null&&o.content!="xlink"&&o.contentContainer.empty()});o.scrollBar||a("html").css("overflow","auto");a("."+
o.closeClass).die("click");a("#bModal").die("click");d.unbind("keydown.bPopup");e.unbind(".bPopup");c.data("bPopup",null);return false}function u(){if(m){var b=[d.height(),d.width()];return{"background-color":o.modalColor,height:b[0],left:l(),opacity:0,position:"absolute",top:0,width:b[1],"z-index":o.zIndex-1}}else return{"background-color":o.modalColor,height:"100%",left:0,opacity:0,position:"fixed",top:0,width:"100%","z-index":o.zIndex-1}}function t(){a("."+o.closeClass).live("click",i);o.modalClose&&
a("#bModal").live("click",i).css("cursor","pointer");o.follow&&e.bind("scroll.bPopup",function(){c.stop().animate({left:d.scrollLeft()+h,top:d.scrollTop()+g},o.followSpeed)}).bind("resize.bPopup",function(){if(o.modal&&m){var b=[d.height(),d.width()];n.css({height:b[0],width:b[1],left:l()})}b=p(c,o.amsl);g=b[0];h=b[1];c.stop().animate({left:d.scrollLeft()+h,top:d.scrollTop()+g},o.followSpeed)});o.escClose&&d.bind("keydown.bPopup",function(b){b.which==27&&i()})}function l(){return e.width()<a("body").width()?
0:(a("body").width()-e.width())/2}function p(b,k){var q=(e.height()-b.outerHeight(true))/2-k,v=(e.width()-b.outerWidth(true))/2+l();return[q<20?20:q,v]}if(a.isFunction(f)){j=f;f=null}o=a.extend({},a.fn.bPopup.defaults,f);o.scrollBar||a("html").css("overflow","hidden");var c=a(this),n=a('<div id="bModal"></div>'),d=a(document),e=a(window),r=p(c,o.amsl),g=r[0],h=r[1],m=a.browser.msie&&parseInt(a.browser.version)==6&&typeof window.XMLHttpRequest!="object";this.close=function(){o=c.data("bPopup");i()};
return this.each(function(){if(!c.data("bPopup")){o.modal&&n.css(u()).appendTo(o.appendTo).animate({opacity:o.opacity},o.fadeSpeed);c.data("bPopup",o);s()}})};a.fn.bPopup.defaults={amsl:150,appendTo:"body",closeClass:"bClose",content:"ajax",contentContainer:null,escClose:true,fadeSpeed:250,follow:true,followSpeed:500,loadUrl:null,modal:true,modalClose:true,modalColor:"#000",opacity:0.7,scrollBar:true,vStart:null,zIndex:9999}})(jQuery);

// Notifications Pop-Up

	$(document).ready(function(){
	   		$("a.notifypop").bind('click', function(){
		 	 $("#notificationsbox").bPopup();
		  	 return false
			});
		});
	
// Charting script

$(document).ready(function(){
 	$('table.pie').visualize({type: 'pie', height: '300px', width: '620px'});
	$('table.bar').visualize({type: 'bar', height: '300px', width: '620px'});
	$('table.area').visualize({type: 'area', height: '300px', width: '620px'});
	$('table.line').visualize({type: 'line', height: '300px', width: '620px'});
});
		
// Tab Switching

	$(document).ready(function(){
		$("#tabs, #graphs").tabs();
	});

// Select all checkboxes

	$(document).ready(function(){
      $("#checkboxall").click(function()
      {
      var checked_status = this.checked;
      $("input[name=checkall]").each(function() {
      this.checked = checked_status;
      });
      });
      });

// Rich text editor/WYSIWYG

	$(document).ready(function() {
			$('#wysiwyg').wysiwyg();
		});