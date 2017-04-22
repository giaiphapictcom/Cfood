(function($) {


  jQuery( ".header-links .icons" ).append( '<i class="fa fa-user"></i>' );
  jQuery(".header-links .cusstom-link").hide();
  jQuery(".header-links .icons i").addClass("accordion-show");
  jQuery(".header-links .icons i").click(function(){
    if(jQuery(this).parent().next().is(":visible")){
      jQuery(this).addClass("accordion-show");
    }else{
      jQuery(this).removeClass("accordion-show");
    }
    jQuery(this).parent().next().toggle(400);
  });

  jQuery('#header-content .icon-search').click(function(){
    jQuery('.search-header').addClass('active');
    console.log('a');
  });
  jQuery('#header-content .search-close').click(function(){
    jQuery('.search-header').removeClass('active');
  });




  jQuery(window).scroll(function() {
    if (jQuery(this).scrollTop() > 200) {
      jQuery("#back-top").fadeIn()
    } else {
      jQuery("#back-top").fadeOut()
    }
  });

  
  
 jQuery( ".tabs-product-home .icon-bar" ).append( '  <i class="fa fa-bars" aria-hidden="true"></i>' );
  jQuery(".tabs-product-home ul.nav-tabs").hide();
  jQuery(".tabs-product-home .icon-bar i").addClass("show-tabs");
  jQuery(".tabs-product-home .icon-bar i").click(function(){
    if(jQuery(this).parent().next().is(":visible")){
      jQuery(this).addClass("show-tabs");
    }else{
      jQuery(this).removeClass("show-tabs");
    }
    jQuery(this).parent().next().toggle(400);
  });
  
  
  
  
  jQuery( ".footer-link h3.module-title" ).append( '<i class="arrow_carrot-down"></i>' );
  jQuery(".footer-link .content").hide();
  jQuery(".footer-link h3.module-title i").addClass("accordion-show");
  jQuery(".footer-link h3.module-title i").click(function(){
    if(jQuery(this).parent().next().is(":visible")){
      jQuery(this).addClass("accordion-show");
    }else{
      jQuery(this).removeClass("accordion-show");
    }
    jQuery(this).parent().next().toggle(400);
  });


  $(function () {
    $('[data-toggle="tooltip"]').tooltip()
  }); 
  $(document).ready(function(){
    $(window).load(function() {
      $('#loading').hide();
    });
  });

})(jQuery);