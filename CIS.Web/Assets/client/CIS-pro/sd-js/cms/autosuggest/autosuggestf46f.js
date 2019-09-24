function populate(id,text){

	jQuery(id).attr('value',text);

}

tag = "";

function hoverbox(){



jQuery('.suggestionsBox  ul li').hover(function(){

jQuery(this).css('background-color','#919A9E');
jQuery(this).css('color','#000');
jQuery(this).find('a').css('color','#ffffff');
},function(){


jQuery(this).css('background-color','transparent');
jQuery(this).css('color','#fff');
jQuery(this).find('a').css('color','#fff');


});
}

function fill(thisValue,tag) {
	$('#tag'+tag).val(thisValue);
	$('#suggestions'+tag).fadeOut();
}

function suggest(inputString,tag){

	if(inputString.length == 0) {
		$('#suggestions'+tag).fadeOut();
	} else {
		$('#tag'+tag).addClass('load');
		$.post("/admin/structure/autosuggest/quickfind.php?tag="+inputString+"&queryString="+inputString, function(data){
			if(data.length >0) {
				$('#suggestions'+tag).fadeIn();
				$('#suggestionsList'+tag).html(data);
				$('#tag'+tag).removeClass('load');
				$('#main_wrapper').css('z-index','-1');
				hoverbox();
			}
		});
	}
}





jQuery(document).ready(function(){


	jQuery("#reject_area").hide();
	



	reason = jQuery("#posted-publisher_option").val();



	if(reason == -1){
	
		jQuery("#reject_area").show();
	
	}else{
	
		jQuery("#reject_area").hide();
	
	}

	jQuery("#posted-publisher_option").change(function(){
	
		reason = jQuery(this).val();
	

		if(reason == -1){
		
			jQuery("#reject_area").show();
		
		}else{
		
		
		jQuery("#reject_area").hide();
		
		}
	
	
	});


	jQuery(".feedback_wrapper").fadeIn(1000,function(){

			jQuery(".feedback").fadeIn(1000);
		
	});


jQuery('#suggestions'+tag).click(function(){


jQuery('#main_wrapper').css('z-index','-1');





})



jQuery('.suggest_div input').attr('value','');
jQuery('.suggest_div input').attr('value','Quick Find');


jQuery('.suggest_div input').click(function(){

	searchval = jQuery('.suggest_div input').val();

	if(searchval == 'Quick Find'){
	
		jQuery('.suggest_div input').attr('value','');
	
	}

})


jQuery('#suggestions'+tag).hover(function(){

		jQuery('#main').css('z-index','-1');
	
	},function(){
	
		jQuery('#main').css('z-index','100');

});

if(jQuery('#suggestions'+tag).css('display') == 'none'){


	jQuery('#main').css('z-index','100');

}

});


	
