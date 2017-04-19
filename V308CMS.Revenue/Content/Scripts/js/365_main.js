function mycarousel_initCallback(carousel)
{
    // Disable autoscrolling if the user clicks the prev or next button.
    carousel.buttonNext.bind('click', function() {
        carousel.startAuto(0);
    });

    carousel.buttonPrev.bind('click', function() {
        carousel.startAuto(0);
    });

    // Pause autoscrolling if the user moves with the cursor over the clip.
    carousel.clip.hover(function() {
        carousel.stopAuto();
    }, function() {
        carousel.startAuto();
    });
};
function mycarousel_initCallback1(carousel1)
{
    // Disable autoscrolling if the user clicks the prev or next button.
    carousel1.buttonNext.bind('click', function() {
        carousel1.startAuto(0);
    });

    carousel1.buttonPrev.bind('click', function() {
        carousel1.startAuto(0);
    });

    // Pause autoscrolling if the user moves with the cursor over the clip.
    carousel1.clip.hover(function() {
        carousel1.stopAuto();
    }, function() {
        carousel1.startAuto();
    });
};

function GetClock(){
	d = new Date();
	nday   = d.getDay();
	nmonth = d.getMonth();
	ndate  = d.getDate();
	nyear = d.getYear();
	nhour  = d.getHours();
	nmin   = d.getMinutes();
	nsec   = d.getSeconds();
	if(d.getDay() == 0){
		nday = "Chủ nhật";
	}
	if(d.getDay() > 0){
		nday = "Thứ :" + nday;
	}
	
	if(nyear<1000) nyear=nyear+1900;

		 if(nhour ==  0) {ap = " AM";nhour = 12;} 
	else if(nhour <= 11) {ap = " AM";} 
	else if(nhour == 12) {ap = " PM";} 
	else if(nhour >= 13) {ap = " PM";nhour -= 12;}

	if(nmin <= 9) {nmin = "0" +nmin;}
	if(nsec <= 9) {nsec = "0" +nsec;}


	document.getElementById('clockbox').innerHTML= nday+", "+(nmonth+1)+"/"+ndate+"/"+nyear;
	//+"  "+nhour+":"+nmin+":"+nsec+ap+""
	setTimeout("GetClock()", 1000);
	}
	window.onload=GetClock;
function slideTB(obj,autopPlay,duration,start,timeeffect,typeeffect,itemSpace){
	var current_slide = start;
	this.autopPlay = autopPlay;
	this.start = start;
	this.timeeffect = timeeffect;
	this.typeeffect = typeeffect;
	this.itemSpace  = itemSpace;
	if(isNaN(this.itemSpace)){this.itemSpace=0;}
	this.busy = false;
	this.initPro;
    this.interval = duration;
    this.obj = obj;
	this.width = obj.find(".preview_container").width();
	this.height = obj.find(".preview_container").height();
	this.leftPos = this.height + this.itemSpace;
	this.slide_number = obj.find("ul.item").find("li.itemLi").length;
	this.Initcomponent = function(){
		if(this.slide_number>0){
			//this.obj.find(".preview").find("ul").append("<li>" + this.obj.find(".container").find("li:eq(0)").html() + "</li>");
		}
	};
	this.startSlide = function(){
        initLR(this);
    };
	function initLR(instance){
		instance.initPro = setInterval(function(){
			if(instance.busy==false){
				instance.busy=true;
				if(instance.start==0){
					instance.start = instance.slide_number - 1;
				}
				else{
					instance.start = instance.start - 1;
				}
				set_slide("pre",instance,instance.start);
			}
		},instance.interval);
	}
	function set_slide(type,instance,index){
		instance.obj.find("ul.navigator").find("li").removeAttr("class");
		instance.obj.find("ul.navigator").find("li:eq("+index.toString()+")").attr("class","active");
		var add_value = '<li class="itemLi">'+instance.obj.find("ul.item").find("li.itemLi:eq("+index.toString()+")").html()+'</li>';
		if(type=="pre"){
			instance.obj.find("ul.preview").append(add_value);
			instance.obj.find("ul.preview").stop().animate({
                top: "-"+instance.leftPos.toString()+"px"
            },instance.timeeffect,instance.typeeffect,function(){
                instance.obj.find("ul.preview").find("li.itemLi:eq(0)").remove();
                instance.obj.find("ul.preview").css("top","0px");
				instance.busy=false;
				//initLR(instance);
            });
		}
		else{
			instance.obj.find("ul.preview").prepend(add_value);
            instance.obj.find("ul.preview").css("top","-"+instance.leftPos.toString()+"px");
            instance.obj.find("ul.preview").stop().animate({
                top: "0px"
            },instance.timeeffect,instance.typeeffect,function(){
                instance.obj.find("ul.preview").find("li.itemLi:eq(1)").remove();
				instance.busy=false;
				//initLR(instance);
            });
		}
	}
	if(this.autopPlay==true){this.startSlide();}
	var instance1 = this;
	
	this.obj.find("ul.navigator").find("li").click(function(){
		if(instance1.busy==false){
			instance1.busy=true;
			clearInterval(instance1.initPro);
			var index = instance1.obj.find("ul.navigator").find("li").index($(this));
			if(index>instance1.start){
				instance1.start = index;
				set_slide("pre",instance1,instance1.start);
			}
			if(index<instance1.start){
				instance1.start = index;
				set_slide("next",instance1,instance1.start);
			}
			initLR(instance1);
		}
	});
	
	this.obj.find(".button_bar").find("li.left").click(function(){
		if(instance1.busy==false){
			instance1.busy=true;
			clearInterval(instance1.initPro);
			if(instance1.start==0){
				instance1.start = instance1.slide_number-1;
			}
			else{
				instance1.start = instance1.start - 1;
			}
			set_slide("pre",instance1,instance1.start);
			initLR(instance1);
		}
	});
	this.obj.find(".button_bar").find("li.right").click(function(){
		if(instance1.busy==false){
			instance1.busy=true;
			clearInterval(instance1.initPro);
			if(instance1.start==instance1.slide_number-1){
				instance1.start = 0;
			}
			else{
				instance1.start = instance1.start + 1;
			}
			set_slide("next",instance1,instance1.start);
			initLR(instance1);
		}
	});
}	