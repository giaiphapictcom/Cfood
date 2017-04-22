jQuery('.btn-menu-canvas').click(function(){
  	if(jQuery('#offcanvas').hasClass('active')){
		jQuery('body').removeClass('off-canvas-active');
      	jQuery('#offcanvas').removeClass('active');
      	jQuery('.wrapper').removeClass('offcanvas-push');
      }else{ 
        jQuery('body').addClass('off-canvas-active');
        jQuery('#offcanvas').addClass('active');
        jQuery('.wrapper').addClass('offcanvas-push');
      }  
  });
  jQuery('#off-canvas-button').click(function(){
  		jQuery('#offcanvas').removeClass('active');
      	jQuery('.wrapper').removeClass('offcanvas-push');
  });
  
    jQuery(document).mouseup(function (e){
    
     var container = jQuery("#offcanvas");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        jQuery('#offcanvas').removeClass('active');
      	jQuery('.wrapper').removeClass('offcanvas-push');
    }

  });
  
      jQuery("#offcanvas .navbar-nav ul").hide();
        jQuery("#offcanvas .navbar-nav li h3 i").addClass("accordion-show");
   
        jQuery("#offcanvas .navbar-nav li h3 i").click(function(){
            if(jQuery(this).parent().next().is(":visible")){
                jQuery(this).addClass("accordion-show");
            }else{
                jQuery(this).removeClass("accordion-show");
            }
            jQuery(this).parent().next().toggle(400);
          if(jQuery(this).hasClass("icon_plus")){
              jQuery(this).removeClass("icon_plus");
              jQuery(this).addClass("icon_minus-06");
           }else{
              jQuery(this).removeClass("icon_minus-06");
     		  jQuery(this).addClass("icon_plus");
          	}
        });