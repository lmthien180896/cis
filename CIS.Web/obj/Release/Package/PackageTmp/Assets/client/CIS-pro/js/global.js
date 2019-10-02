jQuery(function(){

	// Index slideshow
	jQuery("#myCarousel").carousel({
		speed: 100,
		timeout: 100
	});

	// Contact form slide down
	(function(){
		var button = jQuery("#head-contact-switch"),
			form = jQuery("#head-contact-wrap");
		button.click(function(){
			button.toggleClass("is-open");
			form.slideToggle();
		});
	})();
/*	
	$(function(){
		$('#myCarousel').carousel({
      		interval: 5000
    	});
		//$('.carousel-control.right').trigger('click');
	});
*/

	// Inherit Child Link
	function childLink(a,b){
		jQuery(a).click(function(){
			link = jQuery(this).find(b).attr("href");
			window.location = link;
		});
	}
	childLink("#index-banners .span3", "a");
	childLink("#info-box", "a");
	childLink("#meet-team-box", "a");
	childLink("#sidenav li", "a");
	childLink(".news-item", "a");
	childLink(".news-content-wrap", "a");


    jQuery(function(){
    	var wrapper = jQuery("#clients-wrap"),
    	boxItem = wrapper.find(".clients-drop-box-wrap"),
    	boxTitle = boxItem.find(".clients-drop-box-title"),
    	boxContent = boxItem.find(".clients-drop-box-content");
     
    	boxTitle.click(function(){
    		if(jQuery(this).parent().hasClass("active")){
    			
    		} else {
    			boxItem.removeClass("active");
    			jQuery(this).parent().addClass("active");
    		}
    	});
    	boxContent.click(function(){
    		if(jQuery(this).parent().hasClass("active")){
    			boxItem.removeClass("active");
    		} else {
    			
    		}
    	});
    	
    });
    
    
    jQuery(function(){
    	var wrapper = jQuery("#faq-wrap"),
    	faqBoxItem = wrapper.find(".faq-drop-box-wrap"),
    	faqBoxTitle = faqBoxItem.find(".faq-drop-box-title"),
    	faqBoxContent = faqBoxItem.find(".faq-drop-box-content");
     
    	faqBoxTitle.click(function(){
    		if(jQuery(this).parent().hasClass("active")){
    			faqBoxItem.removeClass("active");
    		} else {
    			faqBoxItem.removeClass("active");
    			jQuery(this).parent().addClass("active");
    		}
    	});
    });

	// product lightbox - videos
	jQuery("#index-featured-video").find("a").colorbox({
		iframe:true, innerWidth:640, innerHeight:390
	});

	// Function to auto-clear and repopulate a text input
	function autoclear(a){
		var inputs = jQuery(a);
		inputs.each(function(){
			var input = jQuery(this),
				value = input.attr("value");
			input.attr("title", value);
			var title = input.attr("title");
			input.focus(function(){
				var newVal = input.attr("value"),
					newTitle = input.attr("title");
				if (newVal == newTitle){
					input.attr("value", "");
				} else if (newVal != newTitle) {
					return false;
				}
			});
			input.blur(function(){
				var newVal = input.attr("value"),
					newTitle = input.attr("title");
				if (newVal == newTitle || newVal == ""){
					input.attr("value", newTitle);
				} else {
					return false;
				}
			});
		});
	}

	// Calls the function, replace ".autoclear" for the class attached to designated inputs
	autoclear(".autoclear");

});

/*
magnific popup
*/
(function(){
	if (jQuery().magnificPopup) {
		
		var galleries = [];
		jQuery('[data-gallery]').each(function(){
			var i = galleries.length;
			var gallery = jQuery(this).data('gallery');
			if (jQuery.inArray(gallery, galleries) > -1) return;
			galleries[i] = gallery;
			jQuery('[data-gallery="'+gallery+'"]').magnificPopup({type:'image', gallery:{enabled:true} });
		});
		
		jQuery('[data-magnific="iframe"]').each(function(){
			jQuery(this).magnificPopup({type:'iframe'});
		});
		
	}
})();

/* Responsive menu accordion */
jQuery( "#top-nav li" ).children("ul").after( "<span class='dropdown-arrow'><i>v</i></span>" );


jQuery('body').on('click', '.dropdown-arrow', function() {
    jQuery(this).parent().find('ul').first().toggleClass('responsive-show');
	if (jQuery(this).parent().find('ul').first().is(':visible')) {
		jQuery(this).html("<i>^</i>");
	} else {
		jQuery(this).html("<i>v</i>");
	}
	return false;
});


jQuery(function(){
	
	jQuery('header #menu').eq(0).find('a').each(function(){
		
		
		if(jQuery(this).attr('href') == '#'){
			
			
			jQuery(this).removeAttr('href');
			
		}
		
		
	});
	
	var wrapper = jQuery("#drop-wrap"),
		boxItem = wrapper.find(".drop-box-wrap"),
		boxTitle = boxItem.find(".drop-box-title"),
		boxContent = boxItem.find(".drop-box-content");
		boxHide = boxItem.find(".show-less");

	boxTitle.click(function(){
		if(jQuery(this).parent().hasClass("active")){
			boxItem.removeClass("active");
		} else {
			boxItem.removeClass("active");
			jQuery(this).parent().addClass("active");
		}
	});
	
	boxHide.click(function(){
		if(jQuery(this).parent().hasClass("active")){
			boxItem.removeClass("active");
		} else {
			boxItem.removeClass("active");
			jQuery(this).parent().addClass("active");
		}
	});
});

