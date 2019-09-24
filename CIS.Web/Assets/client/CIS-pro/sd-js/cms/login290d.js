function changefocuscolor(col1,col2,element){

	if(jQuery(element).hasClass('error')){
	
		jQuery(element).focus(function(){
	
				jQuery(this).css('background',col1);
	
		})
	
		jQuery(element).blur(function(){
	
				jQuery(this).css('background','#FFA6A6');
	
		})
	
	}else{
	
		jQuery(element).focus(function(){
	
				jQuery(this).css('background',col1);
	
		})
	
		jQuery(element).blur(function(){
	
				jQuery(this).css('background',col2);
	
		})
	
	}

}

jQuery(document).ready(function(){
	
	jQuery('input#loginform-email').focus();

	//jQuery('input#loginform-email').css('background','#fff');

	jQuery('input#loginform-password').css('background','#fff');
	
	changefocuscolor('#FFFF9B','#FFFFFF','input#loginform-password');
	changefocuscolor('#FFFF9B','#FFFFFF','input#loginform-email');



	if(jQuery('input#loginform-password').hasClass('error')){

		jQuery('input#loginform-password').css('background','#FFA6A6');

	}

	if(jQuery('input#loginform-email').hasClass('error')){

		jQuery('input#loginform-email').css('background','#FFA6A6');

	}

})
