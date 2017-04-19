jQuery(document).ready(function($){
	var currentdevice = '';
	var bootstrap_elements = $('[class*="span"]');
	// Build data
	bootstrap_elements.each ( function(){
		var $this = $(this);
		// With attr data-*
		$this.data();
		// Make the source better view in inspector
    	$this.removeAttr ('data-default data-wide data-normal data-tablet data-stablet data-mobile');
		// For element no attr data-default
		if (!$this.data('default')) 
			$this.data('default', $this.attr('class'));
	
	});
	function updateBootstrapElementClass(newdevice){
  		if (newdevice == currentdevice) return ;
		bootstrap_elements.each(function(){
			var $this = $(this);
			// Default
			if ( !$this.data('default') || (!$this.data(newdevice) && (!currentdevice || !$this.data(currentdevice))) )
				return;
			// Remove current
			if ($this.data(currentdevice)) $this.removeClass($this.data(currentdevice));
			else $this.removeClass ($this.data('default'));
			// Add new
			if ($this.data(newdevice)) $this.addClass ($this.data(newdevice));
			else $this.addClass ($this.data('default'));
		});
    	currentdevice = newdevice;
	};
	
	function detectDevice () {
		var width = $(window).width(); //alert(width);
		if( width > 1200 ){
			return 'wide';
		}else if( width >= 980 ){
			return 'normal';
		}else if( width > 640 && width < 980 ){
			return 'tablet';
		}else if(  width > 0 ){
			return 'mobile';
		}
		/*
		Mobile portrait (320x480)
		Mobile landscape (480x320)
		Small tablet portrait (600x800)
		Small tablet landscape (800x600)
		Tablet portrait (768x1024)
		Tablet landscape (1024x768)
		*/
	}
  	updateBootstrapElementClass (detectDevice());
  
  	// With window resize 
  	$(window).resize(function(){ 
    	if ($.data(window, 'detect-device-time'))
      		clearTimeout($.data(window, 'detect-device-time'));
			
    	$.data(window, 'detect-device-time', 
      		setTimeout(function(){
        		updateBootstrapElementClass (detectDevice());
      		}, 200)
    	)
  	})
});	

